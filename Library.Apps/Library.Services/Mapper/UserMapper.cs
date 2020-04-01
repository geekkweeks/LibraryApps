using Library.Common;
using Library.Domain.Db;
using Library.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Mapper
{
    public static class UserMapper
    {
        public static UserModel ConvertDataToModel(User data)
        {
            var model = new UserModel
            {
                UserId = data.UserId,
                UserName = data.UserName,
                FullName = data.FullName,
                Password = Common.Helper.TxtDecrypt(data.Password),
                RoleId = data.RoleId.Value,
                IsActive = data.IsActive.Value
            };

            return model;
        }

        public static IEnumerable<UserModel> ConvertDataToListModel(
            this IEnumerable<User> data)
        {
            return data.Select(ConvertDataToModel).ToList();
        }

        public static User ConvertModelToData(UserModel model)
        {
            var data = new User
            {
                UserId = model.UserId,
                UserName = model.UserName,
                FullName = model.FullName,
                Password = Helper.TxtEncrypt(model.Password),
                RoleId = model.RoleId,
                IsActive = model.IsActive
            };

            return data;
        }

        public static IEnumerable<User> ConvertModelToLitData(
            this IEnumerable<UserModel> data)
        {
            return data.Select(ConvertModelToData).ToList();
        }
    }
}
