using BACKEND.Models;
using System.Collections.Generic;

namespace BACKEND.Services
{
    public interface IUserService
    {
        //service user
        List<User> getAllUser();

        User GetUser(int id);

        User PostUser(User user);

        User PatchUser(User user);

        void DeleteUser(User user);

        string Login(string username, string password);


    }
}
