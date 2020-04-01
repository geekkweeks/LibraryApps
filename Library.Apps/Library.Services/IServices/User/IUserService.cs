using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain;
using Library.Services.ServiceModels;

namespace Library.Services.IServices.User
{
    public interface IUserService
    {
        void AddUser(UserModel model);

        UserModel GetUserByUserID(int Id); 
        UserModel GetUserByUserName(string username);

        UserModel GetUserByUserNameAndPassword(string userName, string password);

        void UpdateUser(UserModel model);

        void DeleteUser(int id);

        IEnumerable<UserModel> GetUserWithRole();

    }
}
