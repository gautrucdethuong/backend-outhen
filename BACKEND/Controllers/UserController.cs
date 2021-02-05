using BACKEND.Models;
using BACKEND.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BACKEND.Required;
using BACKEND.Data;
using System.Linq;


namespace BACKEND.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private DBContext db;
        private IUserService userService;
        private ValidatorsRequired validatorsRequired = new ValidatorsRequired();
        

        public UserController(IUserService iuser, DBContext dbContext)
        {
            userService = iuser;
            db = dbContext;
        }


        //get all list
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult ListUser()
        {
            return Ok(userService.getAllUser());
        }


        //get by id
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetUser(int id)
        {
            var user = userService.GetUser(id);                   
            return validatorsRequired.CheckUserExist(user);           
        }

        //edit
        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public IActionResult PatchUser(int id, User u)
        {
            var checkexist = userService.GetUser(id);
            
            if (checkexist != null)
            {
                u.user_id = checkexist.user_id;
                userService.PatchUser(u);
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
                    return base.Content("Username " + user.username + " is already exist. Please enter a different username.");
                }
                else if (db.Users.Any(x => x.email == user.email))
                {
                    return base.Content("Email " + user.email + " is already exist. Please enter a different email.");
                }
                else if (db.Users.Any(x => x.phone == user.phone))
                {
                    return base.Content("Number phone " + user.phone + " is already exist. Please enter a different number phone.");
                }
            }
            userService.PostUser(user);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + user.user_id, user);
        }
      

        //delete
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = userService.GetUser(id);
            if (user == null)
            {
                return NotFound($" User with Id: {id} was not found");
            }
            userService.DeleteUser(user);           
            return Ok("Delete successful.");

        }      



    }
}
