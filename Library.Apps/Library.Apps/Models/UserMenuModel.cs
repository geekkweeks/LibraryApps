using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Apps.Models
{
    public class UserMenuModel
    {
        public int roleId { get; set; }
        public string MethodName { get; set; }
        public string MenuName { get; set; }
        public string ControllerName { get; set; }
    }
}