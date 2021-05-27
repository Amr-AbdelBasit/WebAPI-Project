using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Web_API_Project.Models;

namespace Web_API_Project.Controllers
{
    public class AccountController : ApiController
    {
        public async Task<IHttpActionResult> Registration(UserModel userModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                ApplicationDbContext context = new ApplicationDbContext();
                UserStore<IdentityUser> store = new UserStore<IdentityUser>(context);
                UserManager<IdentityUser> manager = new UserManager<IdentityUser>(store);

                IdentityUser user = new IdentityUser();
                user.UserName = userModel.Name;
                user.PasswordHash = userModel.Password;

                IdentityResult result = await manager.CreateAsync(user, userModel.Password);

                if (result.Succeeded)
                {
                    return Created(string.Empty, $"Done Register {user.UserName}");
                }
                else
                {
                    return BadRequest(result.Errors.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
