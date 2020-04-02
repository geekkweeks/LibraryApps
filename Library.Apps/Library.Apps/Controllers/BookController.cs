using Library.Domain.Infrastructure;
using Library.Services.IServices.Book;
using Library.Services.IServices.BookTransaction;
using Library.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Library.Apps.CalculatorService;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.html.simpleparser;

namespace Library.Apps.Controllers
{
    public class BookController : Controller
    {
        private IBookService _bookService;
        private IBookTransactionService _bookTransactionService;
        private IUnitOfWork _unitOfWork;
        private string rolename = string.Empty;

        public BookController(IBookService bookService,IBookTransactionService bookTransactionService, IUnitOfWork unitOfWork)
        {
            this._bookService = bookService;
            this._bookTransactionService = bookTransactionService;
            this._unitOfWork = unitOfWork;
            rolename = Roles.GetRolesForUser().FirstOrDefault();
        }
        // GET: Book
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CutomerTransaction()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Customer")]
        public ActionResult BookTransaction()
        {
            ViewBag.RoleUser = rolename;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult GetTransactionDetailByID(int id)
        {
            var data = _bookTransactionService.GetTransactionByID(id);
            //data.StartDate = JsonConvert.SerializeObject(data.StartDate);
            return Json(new { Data =  data}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult GetTransactionDetail()
        {
            if (rolename.ToUpper().Equals("ADMIN"))
            {
                return Json(new { Data = _bookTransactionService.GetTransactions() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Data = _bookTransactionService.GetTransactionByCustomer(2,User.Identity.Name)}, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Customer")]
        [ValidateAntiForgeryToken]
        public ActionResult GetBooks()
        {
            var data = _bookService.GetBooks();

            if (rolename.ToUpper().Equals("ADMIN"))
                return Json(new { Data = data }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { Data = data.Where(w => w.IsAvailable == true) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult SaveMasterBook(BookServiceModel model)
        {
            try
            {
                string message = string.Empty;
                if (model.BookId == 0)
                {
                    _bookService.AddBook(model);
                    message = "Data has been inserted";
                }
                else
                {
                    _bookService.UpdateBook(model);
                    message = "Data has been updated";
                }
                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Something went wrong: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBook(int id)
        {
            try
            {
                _bookService.DeleteBook(id);
                return Json(new { Message = "Data has been deleted" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Something went wrong: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Customer")]
        [ValidateAntiForgeryToken]
        public ActionResult GetBookById(int id)
        {
            return Json(new { Data = _bookService.GetBookById(id) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetTotal(double price, string startDate, string endDate)
        {
            var totalDays = Common.Helper.GetTotalDays(startDate, endDate);
            var serviceCalculator = new Calculator();
            var total = (double)serviceCalculator.Multiply(Convert.ToInt32(price), totalDays);
            return Json(new { Data =  total}, JsonRequestBehavior.AllowGet);
        }

        #region transaction by customer
        [HttpPost]
        [Authorize(Roles = "Customer")]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTransaction(BookTransactionServiceModel model)
        {
            try
            {
                if (model.TransactionId == null)
                {
                    _bookTransactionService.AddTransaction(model, User.Identity.Name);
                    return Json(new { Message = "Data has been inserted" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    _bookTransactionService.UpdateTransaction(model);
                    return Json(new { Message = "Data has been updated" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Message = "Something went wrong: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTransaction(int id)
        {
            try
            {
                _bookTransactionService.DeleteTransaction(id);
                return Json(new { Message = "Data transaction has been deleted" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Something went wrong: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region export
        [Authorize(Roles = "Admin,Customer")]
        public void ExportExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            try
            {
                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Cells[1, 1].Value = "Book Title";
                workSheet.Cells[1, 2].Value = "Author";
                workSheet.Cells[1, 3].Value = "Book Category";
                workSheet.Cells[1, 4].Value = "Price";
                workSheet.Cells[1, 5].Value = "Days";
                workSheet.Cells[1, 6].Value = "Borrower ID";
                workSheet.Cells[1, 7].Value = "Borrower Name";
                workSheet.Cells[1, 8].Value = "Total(Rp)";
                var list = new List<BookTransactionServiceModel>();
                
                if (rolename.ToUpper().Equals("ADMIN"))
                    list = _bookTransactionService.GetTransactions().ToList();
                else
                    list = _bookTransactionService.GetTransactionByCustomer(2, User.Identity.Name).ToList();

                int recordIndex = 2;
                double grandTotal = 0;
                foreach (var item in list)
                {
                    workSheet.Cells[recordIndex, 1].Value = item.BookTitle;
                    workSheet.Cells[recordIndex, 2].Value = item.Author;
                    workSheet.Cells[recordIndex, 3].Value = item.CategoryName;
                    workSheet.Cells[recordIndex, 4].Value = item.Price;
                    workSheet.Cells[recordIndex, 5].Value = item.Days;
                    workSheet.Cells[recordIndex, 6].Value = item.CreatedBy;
                    workSheet.Cells[recordIndex, 7].Value = item.FullName;
                    workSheet.Cells[recordIndex, 8].Value = Common.Helper.ConvertToCurrency(item.Total);
                    grandTotal += item.Total;
                    recordIndex++;
                }
                //assign value for grand total
                workSheet.Cells[recordIndex, 7].Value = "Grand Total";
                workSheet.Cells[recordIndex, 8].Value = Common.Helper.ConvertToCurrency(grandTotal);

                string excelName = "Transaction_History" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
                workSheet.Cells["A:AZ"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment: filename=" + excelName);
                Response.BinaryWrite(excel.GetAsByteArray());
                Response.End();

            }
            catch (Exception ex)
            {

            }
        }

        //[Authorize(Roles = "Admin,Customer")]
        //public void ExportPDF()
        //{
        //    try
        //    {
        //        var list = new List<BookTransactionServiceModel>();
        //        if (rolename.ToUpper().Equals("ADMIN"))
        //            list = _bookTransactionService.GetTransactions().ToList();
        //        else
        //            list = _bookTransactionService.GetTransactionByCustomer(2, User.Identity.Name).ToList();
                
        //        PdfPTable pdfTable = new PdfPTable(8);
        //        PdfPCell pdfCell = new PdfPCell(new iTextSharp.text.Phrase("Book Title"));
        //        pdfTable.AddCell(pdfCell);
        //        pdfCell = new PdfPCell(new iTextSharp.text.Phrase("Author"));
        //        pdfTable.AddCell(pdfCell);
        //        pdfCell = new PdfPCell(new iTextSharp.text.Phrase("Book Category"));
        //        pdfTable.AddCell(pdfCell);
        //        pdfCell = new PdfPCell(new iTextSharp.text.Phrase("Price"));
        //        pdfTable.AddCell(pdfCell);
        //        pdfCell = new PdfPCell(new iTextSharp.text.Phrase("Days"));
        //        pdfTable.AddCell(pdfCell);
        //        pdfCell = new PdfPCell(new iTextSharp.text.Phrase("Borrower ID"));
        //        pdfTable.AddCell(pdfCell);
        //        pdfCell = new PdfPCell(new iTextSharp.text.Phrase("Borrower Name"));
        //        pdfTable.AddCell(pdfCell);
        //        pdfCell = new PdfPCell(new iTextSharp.text.Phrase("Total(Rp)"));
        //        pdfTable.AddCell(pdfCell);
               

        //        double grandTotal = 0;
        //        //int row = 1;
        //        //foreach (var item in list)
        //        //{
        //        //    //book title
        //        //    pdfCell = new PdfPCell(new iTextSharp.text.Phrase(item.BookTitle));
        //        //    pdfTable.AddCell(pdfCell);
        //        //    //author
        //        //    pdfCell = new PdfPCell(new iTextSharp.text.Phrase(item.Author));
        //        //    pdfTable.AddCell(pdfCell);
        //        //    //book category
        //        //    pdfCell = new PdfPCell(new iTextSharp.text.Phrase(item.CategoryName));
        //        //    pdfTable.AddCell(pdfCell);
        //        //    //price
        //        //    pdfCell = new PdfPCell(new iTextSharp.text.Phrase(Common.Helper.ConvertToCurrency(item.Price.Value)));
        //        //    pdfTable.AddCell(pdfCell);
        //        //    //days
        //        //    pdfCell = new PdfPCell(new iTextSharp.text.Phrase(item.Days.Value.ToString()));
        //        //    pdfTable.AddCell(pdfCell);
        //        //    //borrower id
        //        //    pdfCell = new PdfPCell(new iTextSharp.text.Phrase(item.CreatedBy));
        //        //    pdfTable.AddCell(pdfCell);
        //        //    //borrower name
        //        //    pdfCell = new PdfPCell(new iTextSharp.text.Phrase(item.FullName));
        //        //    pdfTable.AddCell(pdfCell);
        //        //    //total
        //        //    pdfCell = new PdfPCell(new iTextSharp.text.Phrase(Common.Helper.ConvertToCurrency(item.Total)));
        //        //    pdfTable.AddCell(pdfCell);                   
        //        //    grandTotal += item.Total;
        //        //    row++;
        //        //}

        //        for(int x = 0; x <= list.Count; x++)
        //        {
        //            if(x < list.Count)
        //            {
        //                //book title
        //                pdfCell = new PdfPCell(new iTextSharp.text.Phrase(list[x].BookTitle));
        //                pdfTable.AddCell(pdfCell);
        //                //author
        //                pdfCell = new PdfPCell(new iTextSharp.text.Phrase(list[x].Author));
        //                pdfTable.AddCell(pdfCell);
        //                //book category
        //                pdfCell = new PdfPCell(new iTextSharp.text.Phrase(list[x].CategoryName));
        //                pdfTable.AddCell(pdfCell);
        //                //price
        //                pdfCell = new PdfPCell(new iTextSharp.text.Phrase(Common.Helper.ConvertToCurrency(list[x].Price.Value)));
        //                pdfTable.AddCell(pdfCell);
        //                //days
        //                pdfCell = new PdfPCell(new iTextSharp.text.Phrase(list[x].Days.Value.ToString()));
        //                pdfTable.AddCell(pdfCell);
        //                //borrower id
        //                pdfCell = new PdfPCell(new iTextSharp.text.Phrase(list[x].CreatedBy));
        //                pdfTable.AddCell(pdfCell);
        //                //borrower name
        //                pdfCell = new PdfPCell(new iTextSharp.text.Phrase(list[x].FullName));
        //                pdfTable.AddCell(pdfCell);
        //                //total
        //                pdfCell = new PdfPCell(new iTextSharp.text.Phrase(Common.Helper.ConvertToCurrency(list[x].Total)));
        //                pdfTable.AddCell(pdfCell);
        //                grandTotal += list[x].Total;
        //            }
        //            else
        //            {
        //                pdfCell = new PdfPCell(new iTextSharp.text.Phrase("Grand Total"));
        //                pdfTable.AddCell(pdfCell);
        //                pdfCell = new PdfPCell(new iTextSharp.text.Phrase(Common.Helper.ConvertToCurrency(grandTotal)));
        //                pdfTable.AddCell(pdfCell);
        //            }
        //        }


        //        //pdfCell = new PdfPCell(new iTextSharp.text.Phrase("Grand Total"));
        //        //pdfTable.AddCell(pdfCell);
        //        //pdfCell = new PdfPCell(new iTextSharp.text.Phrase(Common.Helper.ConvertToCurrency(grandTotal)));
        //        //pdfTable.AddCell(pdfCell);

        //        Document pdfDocument = new Document(PageSize.A4,10f,10f,10f,10f);
        //        PdfWriter.GetInstance(pdfDocument, Response.OutputStream);

        //        pdfDocument.Open();
        //        pdfDocument.Add(pdfTable);
        //        pdfDocument.Close();

        //        string pdfName = "Transaction_History" + DateTime.Now.ToString("ddMMyyyy") + ".pdf";
        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("Content-disposition", "attachment; filename=" + pdfName);
        //        Response.Write(pdfDocument);
        //        Response.End();
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //}

        [Authorize(Roles = "Admin,Customer")]
        public void ExportPDF()
        {
            try
            {                

                DataTable dt = new DataTable("Transaction");
                dt.Columns.Add("BookTitle", typeof(string));
                dt.Columns[0].Caption = "Book Title";

                dt.Columns.Add("Author", typeof(string));
                dt.Columns[1].Caption = "Author";

                dt.Columns.Add("BookCategory", typeof(string));
                dt.Columns[2].Caption = "Book Category";

                dt.Columns.Add("Price", typeof(string));
                dt.Columns[3].Caption = "Price";

                dt.Columns.Add("Days", typeof(string));
                dt.Columns[4].Caption = "Days";

                dt.Columns.Add("BorrowerID", typeof(string));
                dt.Columns[5].Caption = "Borrower ID";

                dt.Columns.Add("BorrowerName", typeof(string));
                dt.Columns[6].Caption = "Borrower Name";

                dt.Columns.Add("Total", typeof(string));
                dt.Columns[7].Caption = "Total (Rp)";

                var list = new List<BookTransactionServiceModel>();
                if (rolename.ToUpper().Equals("ADMIN"))
                    list = _bookTransactionService.GetTransactions().ToList();
                else
                    list = _bookTransactionService.GetTransactionByCustomer(2, User.Identity.Name).ToList();

                double grantotal = 0;
                foreach (var item in list)
                {
                    dt.Rows.Add(
                        item.BookTitle,
                        item.Author,
                        item.CategoryName,
                        Common.Helper.ConvertToCurrency(item.Price.Value),
                        item.Days.ToString(),
                        item.CreatedBy,
                        item.FullName,
                        Common.Helper.ConvertToCurrency(item.Total)
                    );
                    grantotal += item.Total;
                }
                dt.Rows.Add(
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "Grand Total",
                    Common.Helper.ConvertToCurrency(grantotal)
                );

               PdfPTable pdfTable = new PdfPTable(dt.Columns.Count);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PdfPCell pdfCell = new PdfPCell(new iTextSharp.text.Phrase(dt.Columns[i].Caption));
                    pdfTable.AddCell(pdfCell);
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        var valrow = dt.Rows[i].Field<string>(dt.Columns[j].ColumnName);
                        PdfPCell pdfCell = new PdfPCell(new iTextSharp.text.Phrase(valrow));
                        pdfTable.AddCell(pdfCell);
                    }
                }

                Document pdfDocument = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                PdfWriter.GetInstance(pdfDocument, Response.OutputStream);

                pdfDocument.Open();
                pdfDocument.Add(pdfTable);
                pdfDocument.Close();

                string pdfName = "Transaction_History" + DateTime.Now.ToString("ddMMyyyy") + ".pdf";
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-disposition", "attachment; filename=" + pdfName);
                Response.Write(pdfDocument);
                Response.End();
            }
            catch (Exception ex)
            {

            }
        }

        [Authorize(Roles = "Admin")]
        public void ExportMasterBookExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            try
            {
                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Cells[1, 1].Value = "Book Title";
                workSheet.Cells[1, 2].Value = "Author";
                workSheet.Cells[1, 3].Value = "Book Category";
                workSheet.Cells[1, 4].Value = "Price";
                workSheet.Cells[1, 5].Value = "is Available";

                var list = new List<BookServiceModel>();
                list = _bookService.GetBooks().ToList();
                
                int recordIndex = 2;
                foreach (var item in list)
                {
                    workSheet.Cells[recordIndex, 1].Value = item.BookTitle;
                    workSheet.Cells[recordIndex, 2].Value = item.Author;
                    workSheet.Cells[recordIndex, 3].Value = item.CategoryName;
                    workSheet.Cells[recordIndex, 4].Value = item.Price;
                    workSheet.Cells[recordIndex, 5].Value = item.IsAvailable;
                    recordIndex++;
                }
               
                string excelName = "MasterBook_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";
                workSheet.Cells["A:AZ"].AutoFitColumns();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment: filename=" + excelName);
                Response.BinaryWrite(excel.GetAsByteArray());
                Response.End();

            }
            catch (Exception ex)
            {

            }
        }

        [Authorize(Roles = "Admin")]
        public void ExportMasterBookPDF()
        {
            try
            {
                var list = new List<BookServiceModel>();
                list = _bookService.GetBooks().ToList();

                PdfPTable pdfTable = new PdfPTable(5);
                PdfPCell pdfCell = new PdfPCell(new iTextSharp.text.Phrase("Book Title"));
                pdfTable.AddCell(pdfCell);
                pdfCell = new PdfPCell(new iTextSharp.text.Phrase("Author"));
                pdfTable.AddCell(pdfCell);
                pdfCell = new PdfPCell(new iTextSharp.text.Phrase("Book Category"));
                pdfTable.AddCell(pdfCell);
                pdfCell = new PdfPCell(new iTextSharp.text.Phrase("Price"));
                pdfTable.AddCell(pdfCell);
                pdfCell = new PdfPCell(new iTextSharp.text.Phrase("Is Available"));
                pdfTable.AddCell(pdfCell);

                double grandTotal = 0;
                int row = 1;
                foreach (var item in list)
                {
                    //book title
                    pdfCell = new PdfPCell(new iTextSharp.text.Phrase(item.BookTitle));
                    pdfTable.AddCell(pdfCell);
                    //author
                    pdfCell = new PdfPCell(new iTextSharp.text.Phrase(item.Author));
                    pdfTable.AddCell(pdfCell);
                    //book category
                    pdfCell = new PdfPCell(new iTextSharp.text.Phrase(item.CategoryName));
                    pdfTable.AddCell(pdfCell);
                    //price
                    pdfCell = new PdfPCell(new iTextSharp.text.Phrase(Common.Helper.ConvertToCurrency(item.Price.Value)));
                    pdfTable.AddCell(pdfCell);
                    //is available
                    pdfCell = new PdfPCell(new iTextSharp.text.Phrase(item.IsAvailable.ToString()));
                    pdfTable.AddCell(pdfCell);
                }
              

                Document pdfDocument = new Document();
                PdfWriter.GetInstance(pdfDocument, Response.OutputStream);

                pdfDocument.Open();
                pdfDocument.Add(pdfTable);
                pdfDocument.Close();

                string pdfName = "MasterBook_" + DateTime.Now.ToString("ddMMyyyy") + ".pdf";
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-disposition", "attachment; filename=" + pdfName);
                Response.Write(pdfDocument);
                Response.End();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion


    }
}