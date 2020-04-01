using Library.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.IServices.Book
{
    public interface IBookService
    {
        void AddBook(BookServiceModel model);

        BookServiceModel GetBookById(int Id);

        void UpdateBook(BookServiceModel model);

        void DeleteBook(int id);

        IEnumerable<BookServiceModel> GetBooks();
    }
}
