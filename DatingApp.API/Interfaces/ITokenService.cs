using DatingApp.API.Models;

namespace DatingApp.API.Interfaces
{
    public interface ITokenService 
    {
        string CreateToken(User user);
    }
}