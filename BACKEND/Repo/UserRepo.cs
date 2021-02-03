using BACKEND.Models;
using BACKEND.Services;
using System.Collections.Generic;
using System.Linq;
using BACKEND.Data;
using Microsoft.Extensions.Caching.Memory;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;
using OAuth2;

namespace BACKEND.Repo
{
    public class UserRepo : IUserService 
    {
        private DBContext db;
        private readonly IMemoryCache _memoryCache;
        
        public UserRepo(DBContext dbContext, IMemoryCache memoryCache)
        {
            db = dbContext;
            _memoryCache = memoryCache;

        }

        public UserRepo() { }

        //get list
        public List<User> getAllUser()
        {
            return db.Users.ToList();
        }

        //get by id
        public User GetUser(int id)
        {
            var user = db.Users.Find(id);
            return user;
        }

        //update 
        public User PatchUser(User user)
        {
            var edituser = db.Users.Find(user.user_id);
            if (edituser != null)
            {
                db.Users.Update(edituser);
                edituser.fullname = user.fullname;
                edituser.email = user.email;                
                edituser.password = user.password;
                edituser.phone = user.phone;
                db.SaveChanges();
            }
            return user;
        }

        //create
        public User PostUser(User user)
        {           
            db.Add(user);
            db.SaveChanges();
            return user;
        }


        //delete
        public void DeleteUser(User user)
        {
            db.Users.Remove(user);
            db.SaveChanges();
        }


        //login
        public String Login(string username, string password)
        {
            List<User> list = db.Users.ToList();

            if (list != null)
            {
                foreach (var user in list)
                {
                    if (user.username == username && user.password == password)
                    {
                       return GenerateJSONWebToken(user);
                    }                                  
                }               
            }
            return "";
        }

        // generate Json
        private String GenerateJSONWebToken(User user)
        {
            
            // claims lay thong tin user 
            var claims = new List<Claim>();            
            claims.Add(new Claim("fullname", user.fullname));
            claims.Add(new Claim("email", user.email));
            claims.Add(new Claim("username", user.username));           
            claims.Add(new Claim("phone", user.phone));
            //claims.Add(new Claim("password", user.password));


            var secretBytes = Encoding.UTF8.GetBytes(Constant.Secret);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorthm = SecurityAlgorithms.HmacSha256;

            var signingCredentials = new SigningCredentials(key, algorthm);
            var token = new JwtSecurityToken(
                Constant.Issuer,
                Constant.Audiance,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(2),
                signingCredentials);
            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
                return tokenJson;            
        }       




    }
}
