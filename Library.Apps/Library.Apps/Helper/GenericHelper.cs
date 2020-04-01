using Library.Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Apps.Helper
{
    public static class GenericHelper
    {
        public static List<UserMenuModel> GetUserMenu(string  username)
        {
            var list = new List<UserMenuModel>();
            using (var ctx = new Domain.Db.LibraryDBContext())
            {
                var role = ctx.Users.Where(w => w.UserName == username).FirstOrDefault().RoleId; 
                list = ctx.MasterMenus.Where(w => w.RoleId == role)
                    .Select(s => new UserMenuModel() { ControllerName = s.ControllerName, MenuName = s.MenuName, MethodName = s.MethodName }).ToList();
            }
            return list;
        }
    }
}