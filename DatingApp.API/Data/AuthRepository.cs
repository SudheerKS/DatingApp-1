using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        public Task<User> Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> register(User user, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UserExista(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}