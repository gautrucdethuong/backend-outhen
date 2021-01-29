using BACKEND.Models;
using BACKEND.Services;
using System.Collections.Generic;
using System.Linq;
using BACKEND.Data;
using Microsoft.Extensions.Caching.Memory;

namespace BACKEND.Repo
{
    public class UserRepo : IUserService 
    {
        private DBContext db;
        private readonly IMemoryCache _memoryCache;
        public UserRepo(DBContext dbContext, IMemoryCache memoryCache)
        {
            db = dbContext;
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
                edituser.username = user.username;
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





        /*private User AuthenticateUser(string username, string password)
        {
            var user = db.Users.SingleOrDefault(x => x.username == username && x.password == password);
            if (user == null)
            {
                return null;
            }
            return user;
        }*/
    }
}
