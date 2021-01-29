using BACKEND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BACKEND.Services
{
    public interface IUserService
    {
        List<User> getAllUser();
        User GetUser(int id);
        User PostUser(User user);
        User PatchUser(User user);
        void DeleteUser(User user);
       // User Authenticate(string username, string password);


    }
}
