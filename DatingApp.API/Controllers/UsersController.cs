using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace DatingApp.API.Controllers
{

   [Authorize]
    public class UsersController : BaseApiController
    {
         private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

    [HttpGet]
    public  ActionResult<IEnumerable<User>> GetUsers()
    {
       return _context.Users.ToList();
    }

   [HttpGet("{id}")]
     public ActionResult<User> GetUser(int id)
    {
        return _context.Users.Find(id);
    }


    }
} 