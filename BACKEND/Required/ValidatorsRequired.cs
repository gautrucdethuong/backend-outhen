using BACKEND.Data;
using BACKEND.Models;
using BACKEND.Repo;
using BACKEND.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;

namespace BACKEND.Required
{
    public class ValidatorsRequired : ControllerBase
    {
        private UserRepo obj = new UserRepo();
        private DBContext db;
        private IUserService _iuser;

        public ValidatorsRequired(DBContext dBContext, IUserService iuser)
        {
            db = dBContext;
        }

        public ValidatorsRequired()
        {
        }

        public IActionResult CheckUserExist(User user)
        {
            if (user != null){
                return Ok(user);
            }
                return NotFound("User with Id was not found");
        }

        public void doesEmailExist(string UserEmail)
        {
            var user = db.Users.FirstOrDefault(u => u.email.Equals(UserEmail));
            if (user != null)
            {
                ModelState.AddModelError("UserEmail", "User email already exists. Please enter a different email.");
            }
        }

        public IActionResult CheckUserExist2(User user)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(x => x.username == user.username))
                {
                    return base.Content("Username " + user.username + " is already exist. Please enter a different username.", "text/html", Encoding.UTF8);
                    //ModelState.AddModelError("UserEmail", "User email already exists. Please enter a different email.");
                }
                else if (db.Users.Any(x => x.email == user.email))
                {
                    return base.Content("Email " + user.email + " is already exist. Please enter a different email.", "text/html", Encoding.UTF8);
                }
                else if (db.Users.Any(x => x.phone == user.phone))
                {
                    return base.Content("Number phone " + user.phone + " is already exist. Please enter a different number phone.", "text/html", Encoding.UTF8);
                }
            }
            _iuser.PostUser(user);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + user.user_id, user);
        }




    }
}

