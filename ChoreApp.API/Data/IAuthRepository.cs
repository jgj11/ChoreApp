using System.Threading.Tasks;
using ChoreApp.API.Models;

namespace ChoreApp.API.Data
{
    // THIS IS MY REPOSITORY for the DB

    //this is like a contract
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);
    }
}