using BACKEND.Models;
using BACKEND.Repo;
using BACKEND.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BACKEND.Required
{
    public class ValidatorsRequired : ControllerBase
    {
        private UserRepo obj = new UserRepo();
        public IActionResult CheckUserExist(User user)
        {
            if (user != null){
                return Ok(user);
            }
                return NotFound("User with Id was not found");
        }


        //[HttpPost]
        /*public async Task<ActionResult<User>> CreateUser(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }
                var useremail = obj.GetUserByEmail(user.email);
                if(useremail != null)
                {
                    ModelState.AddModelError("email", "User email already in use.");
                    return BadRequest(ModelState);
                }
                var created =  obj.PostUser(user);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + user.user_id, user);
            }
            catch (System.Exception)
            {
                throw;            }
        }*/


        public IActionResult ValidationDelete(User user)
        {
            if (user == null)
            {
                return NotFound("User with Id was not found");
            }
            return Ok();
            //_iuser.DeleteUser(user);
            
        }


    }
}

