using Library.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.IServices.BookTransaction
{
    public interface IBookTransactionService
    {
        void AddTransaction(BookTransactionServiceModel model, string createdby);

        BookTransactionServiceModel GetTransactionByID(int transactionId);
        IEnumerable<BookTransactionServiceModel> GetTransactionByCustomer(int roleid, string borrower);

        void UpdateTransaction(BookTransactionServiceModel model);

        void DeleteTransaction(int id);

        IEnumerable<BookTransactionServiceModel> GetTransactions();
    }
}
