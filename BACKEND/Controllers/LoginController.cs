using BACKEND.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace BACKEND.Controllers
{
    
    [ApiController]
    public class LoginController : ControllerBase
    {
        
        private IUserService userService;
        public LoginController(IUserService iuser)
        {
            userService = iuser;
        }

        // login
        [HttpPost]
        [Route("api/[controller]/Authenticate")]
        public IActionResult Authenticate(String username, String password)
        {                       
            String token = userService.Login(username, password);
            if(!token.Equals(""))
            {
                return base.Content("Token: "+ token, "text/html", Encoding.UTF8);
            }         
            return NotFound("Username or password incorrect.");
        }

                  
        //[Authorize]
        [HttpGet]
        [Route("api/[controller]/getUserbyJWT")]
        public String getUserbyJWT()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userName = claim[0].Value;
            return "Welcome to: " + userName;
        }

        /* [HttpGet("get-my-id")]
         [Route("api/[Controller]/get-my-id")]
         public ActionResult<string> GetInformation()
         {
             //List<User> list = db.Users.ToList();
             var Claim = User.Claims.FirstOrDefault(x => x.Type.Equals("fullname"));
             if (Claim != null)
             {
                 return Ok($"This is your fullname: {Claim.Value}");
             }
             return BadRequest("Not Claim");
         }*/

        //[Authorize]
        [Route("api/[controller]/Secure")]       
        public IActionResult Secure() => Ok("Secure works");
    }
}
