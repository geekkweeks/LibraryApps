using Library.Services.IServices.Role;
using Library.Services.IServices.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Library.Apps.Controllers.API
{
    [Route("api/[controller]")]
    public class UserProviderController : ApiController
    {
        private IUserService _userService;

        public UserProviderController(IUserService userService)
        {
            this._userService = userService;
        }

        [Route("GetUser")]
        public IHttpActionResult Get()
        {
            try
            {
                var result = _userService.GetUserWithRole();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
