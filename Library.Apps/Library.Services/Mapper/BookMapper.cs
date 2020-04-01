using Library.Domain.Db;
using Library.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Mapper
{
    public static class BookMapper
    {
        #region convert data to model
        public static BookServiceModel ConvertDataToModel(MasterBook data)
        {
            var model = new BookServiceModel
            {
                BookId = data.BookId,
                BookTitle = data.BookTitle,
                Author = data.Author,
                CategoryId = data.CategoryId.Value,
                Price = data.Price,
                IsAvailable = data.IsAvailable
            };

            return model;
        }

        public static IEnumerable<BookServiceModel> ConvertDataToListModel(this IEnumerable<MasterBook> data)
        {
            return data.Select(ConvertDataToModel).ToList();
        }
        #endregion

        #region convert model to data   
        public static MasterBook ConvertModelToData(BookServiceModel model)
        {
            var data = new MasterBook
            {
                BookId = model.BookId,
                BookTitle = model.BookTitle,
                Author = model.Author,
                CategoryId = model.CategoryId,
                Price = model.Price,
                IsAvailable = model.IsAvailable
            };

            return data;
        }

        public static IEnumerable<MasterBook> ConvertModelToListData(this IEnumerable<BookServiceModel> model)
        {
            return model.Select(ConvertModelToData).ToList();
        }
        #endregion 

    }
}
