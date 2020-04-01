using Library.Domain.Infrastructure;
using Library.Domain.Repositories;
using Library.Services.IServices.Book;
using Library.Services.Mapper;
using Library.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using table = Library.Domain.Db;

namespace Library.Services.Services.Book
{
    public class BookService : IBookService
    {
        private IBookRepository _bookRepository;
        private IUnitOfWork _unitOfWork;

        public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            this._bookRepository = bookRepository;
            this._unitOfWork = unitOfWork;
        }

        public void AddBook(BookServiceModel model)
        {
            var data = new table.MasterBook();
            data.BookTitle = model.BookTitle;
            data.Author = model.Author;
            data.CategoryId = model.CategoryId;
            data.Price = model.Price;
            data.IsAvailable = model.IsAvailable;
            _bookRepository.Add(data);
            _unitOfWork.Commit();
        }

        public void DeleteBook(int id)
        {
            table.MasterBook data = _bookRepository.GetNoTracking(w => w.BookId == id);
            _bookRepository.Delete(data);
            _unitOfWork.Commit();
        }

        public BookServiceModel GetBookById(int Id)
        {
            var res = _unitOfWork.SQLQuery<BookServiceModel>("DBO.SpGetMasterBook @bookId", new SqlParameter("bookId", System.Data.SqlDbType.Int) { Value = Id}).FirstOrDefault();
            return res;
        }

        public IEnumerable<BookServiceModel> GetBooks()
        {
            var res = _unitOfWork.SQLQuery<BookServiceModel>("DBO.SpGetMasterBook");
            return res;
        }

        public void UpdateBook(BookServiceModel model)
        {
            table.MasterBook data = _bookRepository.Get(w => w.BookId == model.BookId);
            data = BookMapper.ConvertModelToData(model);
            _bookRepository.Update(data);
            _unitOfWork.Commit();
        }
    }
}
