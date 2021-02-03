using BACKEND.Models;
using BACKEND.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BACKEND.Required;
using BACKEND.Data;
using System.Linq;
using System.Text;

namespace BACKEND.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private DBContext db;
        private IUserService _iuser;
        private ValidatorsRequired _validator = new ValidatorsRequired();
        

        public UserController(IUserService iuser, DBContext dbContext)
        {
            _iuser = iuser;
            db = dbContext;
        }


        //get all list
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult ListUser()
        {
            return Ok(_iuser.getAllUser());
        }


        //get by id
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _iuser.GetUser(id);                   
            return _validator.CheckUserExist(user);           
        }

        //edit
        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public IActionResult PatchUser(int id, User u)
        {
            var checkexist = _iuser.GetUser(id);
            
            if (checkexist != null)
            {
                u.user_id = checkexist.user_id;
                _iuser.PatchUser(u);
            }
            return Ok("Update successful.");
        }


        //create
        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult PostUser(User user)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(x => x.username == user.username))
                {
                    return base.Content("Username " + user.username + " is already exist. Please enter a different username.", "text/html", Encoding.UTF8);
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
      

        //delete
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _iuser.GetUser(id);
            if (user == null)
            {
                return NotFound($" User with Id: {id} was not found");
            }
            _iuser.DeleteUser(user);           
            return Ok("Delete successful.");

        }      



    }
}
