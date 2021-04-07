using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserManagement.Data;
using static UserManagement.Controllers.Dto;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserManagementDataContext dc;
        public UsersController(UserManagementDataContext dc)
        {
            this.dc = dc;
        }

        [HttpGet("me")]
        public async Task<ActionResult<SimplifiedUser>> GetUserAsync()
        {
            var userIdentifier = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var user = await dc.Users.Where(u => u.NameIdentifier == userIdentifier).FirstOrDefaultAsync();

            var resultUser = new SimplifiedUser(user.Id, user.NameIdentifier, user.Email, user.FirstName, user.LastName);
            return Ok(resultUser);
        }

        [HttpGet]
        public async Task<ActionResult<List<SimplifiedUser>>> GetUsers()
        {
            var users = await dc.Users.ToListAsync();
            var resultUsers = new List<SimplifiedUser>();

            foreach (var user in users)
            {
                resultUsers.Add(new SimplifiedUser(user.Id, user.NameIdentifier, user.Email, 
                                                   user.FirstName, user.LastName));
            }

            return Ok(resultUsers);
        }
    }
}