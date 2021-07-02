using Microsoft.AspNetCore.Mvc;
using SoaringApi.Data;
using SoaringApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoaringApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly SDCMSApiContext _context;

        public UsersController(SDCMSApiContext context)
        {
            _context = context;
        }

        //Post api/users/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            var username = _context.tbl_Users.FirstOrDefault(x => x.UserName == user.UserName);

            if (username != null)
            {
                return Json("User Already Exists");
            }
            else
            {
                _context.tbl_Users.Add(user);
                _context.SaveChanges();

                return Json("User Registered Successfully");
            }
        }

        //Post api/users/register
        [HttpPost("Login")]
        public IActionResult Login([FromBody] User user)
        {
            try
            {
                var username = _context.tbl_Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);

                if (username != null)
                {
                    return Json(username.UserName);
                }
                else
                {
                    _context.tbl_Users.Add(user);
                    _context.SaveChanges();

                    return Json("InvalidUser");
                }
            }
            catch (Exception e)
            {
                throw e;
            }


        }
    }
}
