using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Text;
using DatingApp.API.Dtos;
using DatingApp.API.Interfaces;

namespace DatingApp.API.Controllers 
{
    public class AccountController : BaseApiController
    {
         private readonly DataContext _context;
          private readonly ITokenService _TokenService;
        public AccountController(DataContext  context, ITokenService tokenService)
        {
            _context = context;
            _TokenService = tokenService;
        }

         [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(UserForRegisterDto userForRegisterDto)
        {
           userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

           if(await UserExist(userForRegisterDto.Username))
             return BadRequest("Username already exists");

             using var hmac = new HMACSHA512(); 

             var userToCreate = new User
             {
                UserName = userForRegisterDto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userForRegisterDto.Password)),
                PasswordSalt = hmac.Key
             };

             _context.Users.Add(userToCreate);
             await _context.SaveChangesAsync();

            return new UserDto
            {
                 Username = userToCreate.UserName,
                 Token = _TokenService.CreateToken(userToCreate)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] UserLoginDto userLoginDto)
        {
               var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == userLoginDto.Username);

               if(user == null) return Unauthorized("Invalid Credentials");

               using var hmac = new HMACSHA512(user.PasswordSalt); 

               var computerhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userLoginDto.Password));

              for(int i = 0; i < computerhash.Length;i++)
              {
                  if(computerhash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
              }

              return new UserDto
              {
                  Username = user.UserName,
                  Token = _TokenService.CreateToken(user)
              };
        }

         public async Task<bool> UserExist(string userName)
         {
               return await _context.Users.AnyAsync(x => x.UserName == userName);
         }

    }
}