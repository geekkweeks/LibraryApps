using Library.Common;
using Library.Domain.Infrastructure;
using Library.Domain.Repositories;
using Library.Services.IServices.User;
using Library.Services.Mapper;
using Library.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using table = Library.Domain.Db;

namespace Library.Services.Services.User
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private IUnitOfWork _unitOfWork;

        public UserService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IUnitOfWork unitOfWork)
        {
            this._userRepository = userRepository;
            this._roleRepository = roleRepository;
            this._unitOfWork = unitOfWork;
;        }
        public void AddUser(UserModel model)
        {
            var data = new table.User();
            data.UserName = model.UserName;
            data.FullName = model.FullName;
            data.Password = Common.Helper.TxtEncrypt(model.Password);
            data.RoleId = model.RoleId;
            data.IsActive = model.IsActive == null ? false : model.IsActive;
            _userRepository.Add(data);
            _unitOfWork.Commit();
        }

        public void DeleteUser(int id)
        {
            table.User data = _userRepository.GetNoTracking(w => w.UserId == id);
            _userRepository.Delete(data);
            _unitOfWork.Commit();
        }

        public UserModel GetUserByUserID(int Id)
        {
            var model = new UserModel();
            IQueryable<table.User> res = _userRepository.GetAll().Where(w => w.UserId == Id);
            if (res.Any())
            {
                model = UserMapper.ConvertDataToModel(res.FirstOrDefault());
            }

            return model;
        }

        public UserModel GetUserByUserName(string username)
        {
            UserModel model = null;
            var data = _userRepository.Get(w => w.UserName == username);
            if (data != null)
                model = UserMapper.ConvertDataToModel(data);

            return model;
        }

        public UserModel GetUserByUserNameAndPassword(string userName, string password)
        {
            password = Helper.TxtEncrypt(password);
            UserModel modelUser = null;
            var user = _userRepository.Get(w => w.UserName == userName && w.Password == password);
            if(user != null)
            {
                modelUser = UserMapper.ConvertDataToModel(user);
                modelUser.RoleName = _roleRepository.Get(w => w.RoleId == modelUser.RoleId).RoleName;
            }

            return modelUser;
        }

        public IEnumerable<UserModel> GetUserWithRole()
        {
            var res = _unitOfWork.SQLQuery<UserModel>("DBO.SpGetUserWithRole");
            return res;
        }

        public void UpdateUser(UserModel model)
        {
            table.User dataFromTable = _userRepository.Get(w => w.UserId == model.UserId);
            dataFromTable = UserMapper.ConvertModelToData(model);
            _userRepository.Update(dataFromTable);
            _unitOfWork.Commit();
        }
    }
}
