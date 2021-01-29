using BACKEND.Models;
using BACKEND.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BACKEND.Required;
using static BACKEND.Required.ValidatorsRequired;


namespace BACKEND.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _iuser;
        private ValidatorsRequired _validator = new ValidatorsRequired();
        

        public UserController(IUserService iuser)
        {
            _iuser = iuser;
        }
        //get all
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
            return Ok(u);
        }


        //create
        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult PostUser(User user)
        {       
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
                return NotFound($"User with Id: {id} was not found");
            }
            _iuser.DeleteUser(user);
            return Ok();

        }
    }
}
