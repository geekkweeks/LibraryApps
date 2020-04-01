using Library.Domain.Db;
using Library.Services.IServices.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Domain;
using Library.Domain.Repositories;
using Library.Services.ServiceModels;
using Library.Services.IServices.Role;
using System.Web.Security;

namespace Library.Apps.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        private IRoleService _roleService;
        private string rolename = string.Empty;

        public UserController(IUserService userService, IRoleService roleService)
        {
            this._userService = userService;
            this._roleService = roleService;
            rolename = Roles.GetRolesForUser().FirstOrDefault();
        }


        // GET: User
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            var model = new UserModel();
            model.Roles = _roleService.GetAllRole().Select(s => new RoleServiceModel() { 
                RoleId = s.RoleId,
                RoleName = s.RoleName
            }).ToList();
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            var data = _userService.GetUserByUserNameAndPassword(model.UserName, model.Password);

            if (data != null)
            {
                if (!data.IsActive)
                {
                    ModelState.AddModelError("", "your account have not been activated by Administrator, please kindly to contact Administrator");
                    return View();
                }

                FormsAuthentication.SetAuthCookie(data.UserName, false);
                

                if (data.RoleName.ToUpper().Equals("ADMIN"))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("BookTransaction","Book");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
        }

        [Authorize]
        public ActionResult UnAuthorize()
        {
            return View();
        }

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","User");
        }

        public ActionResult Register(int? id)
        {
            var model = new UserModel();
            if (id.HasValue)
                model = _userService.GetUserByUserID(id.Value);
            return View(model);
        }

        public ActionResult GetUserByID(int id)
        {
            var model = _userService.GetUserByUserID(id);
            return Json(new { Data = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(UserModel model)
        {
            if (model.UserId != 0)
                _userService.UpdateUser(model);
            else
                _userService.AddUser(model);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Json(new { Message = "Data has been deleted" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { Message = "Something went wrong: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveData(UserModel model)
        {
            try
            {
                string message = string.Empty;
                if (model.UserId == 0)
                {
                    _userService.AddUser(model);
                    message = "Data has been inserted";
                }
                else
                {
                    _userService.UpdateUser(model);
                    message = "Data has been updated";
                }                    
                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { Message = "Something went wrong: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpGet]
        public ActionResult GetUserWithRole()
        {
            return Json(new { Data = _userService.GetUserWithRole().ToList() }, JsonRequestBehavior.AllowGet);
        }


    }
}