using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data;
using static UserManagement.Controllers.Dto;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "administrator")]
    public class GroupsController : ControllerBase
    {
        private readonly UserManagementDataContext dc;

        public GroupsController(UserManagementDataContext dc)
        {
            this.dc = dc;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SimplifiedGroup>> GetById(int id)
        {
            var group = await dc.Groups.Where(g => g.Id == id).FirstOrDefaultAsync();
            if (group == null)
            {
                return NotFound();
            }
            var simplifiedGroup = new SimplifiedGroup(group.Id, group.Name);
            return Ok(simplifiedGroup);
        }

        [HttpGet]
        public async Task<ActionResult<List<SimplifiedGroup>>> GetGroups()
        {
            var groups = await dc.Groups.ToListAsync();
            var simplifiedGroups = new List<SimplifiedGroup>();

            foreach (var group in groups)
            {
                simplifiedGroups.Add(new SimplifiedGroup(group.Id, group.Name));
            }

            return Ok(simplifiedGroups);
        }
    }
}
