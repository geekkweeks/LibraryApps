using Library.Common;
using Library.Domain.Infrastructure;
using Library.Domain.Repositories;
using Library.Services.IServices.BookTransaction;
using Library.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using table = Library.Domain.Db;

namespace Library.Services.Services.BookTransaction
{
    public class BookTransactionService : IBookTransactionService
    {
        private IUnitOfWork _unitOfWork;
        private IBookTransactionRepository _bookTransactionRepository;

        public BookTransactionService(IUnitOfWork unitOfWork, IBookTransactionRepository bookTransactionRepository)
        {
            this._unitOfWork = unitOfWork;
            this._bookTransactionRepository = bookTransactionRepository;
        }
        public void AddTransaction(BookTransactionServiceModel model, string createdby)
        {
            var data = new table.BookTransaction();
            data.BookId = model.BookId;
            data.StatusId = model.StatusId;
            data.StartDate = model.StartDate;
            data.EndDate = model.EndDate;
            data.Days = Helper.GetTotalDays(model.StartDate.Value, model.EndDate.Value);
            data.Price = model.Price;
            data.Total = model.Total;
            data.CreatedTime = DateTime.UtcNow;
            data.UpdatedTime = DateTime.UtcNow;
            data.CreatedBy = createdby;
            _bookTransactionRepository.Add(data);
            _unitOfWork.Commit();
        }

        public void DeleteTransaction(int id)
        {
            table.BookTransaction data = _bookTransactionRepository.GetNoTracking(w => w.TransactionId == id);
            _bookTransactionRepository.Delete(data);
            _unitOfWork.Commit();
        }

        public BookTransactionServiceModel GetTransactionByID(int transactionId)
        {
            var res = _unitOfWork.SQLQuery<BookTransactionServiceModel>("SpGetTransactiondETAILByID @TransactionId", 
                new SqlParameter("TransactionId", System.Data.SqlDbType.Int) { Value = transactionId }).FirstOrDefault();
            return res;
        }

        public IEnumerable<BookTransactionServiceModel> GetTransactionByCustomer(int roleid, string borrower)
        {
            var res = _unitOfWork.SQLQuery<BookTransactionServiceModel>("DBO.SpGetTransactionDetail @roleid,@borrower",
                new SqlParameter("roleid", System.Data.SqlDbType.Int) { Value = roleid }
                , new SqlParameter("borrower", System.Data.SqlDbType.NVarChar) { Value = borrower });
            return res;
        }

        public IEnumerable<BookTransactionServiceModel> GetTransactions()
        {
            var res = _unitOfWork.SQLQuery<BookTransactionServiceModel>("DBO.SpGetTransactionDetail");
            return res;
        }

        public void UpdateTransaction(BookTransactionServiceModel model)
        {
            table.BookTransaction data = _bookTransactionRepository.Get(w => w.TransactionId == model.TransactionId);
            data.BookId = model.BookId;
            data.StatusId = model.StatusId;
            data.StartDate = model.StartDate;
            data.EndDate = model.EndDate;
            data.Days = model.Days;
            data.Price = model.Price;
            data.Total = model.Total;
            data.UpdatedTime = DateTime.UtcNow;
            _bookTransactionRepository.Update(data);
            _unitOfWork.Commit();

        }
    }
}
