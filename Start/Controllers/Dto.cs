using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Controllers
{
    public class Dto
    {
        public record SimplifiedUser(int id, string nameIdentifier, string email, 
                                     string? firstName, string? lastName);

        public record SimplifiedGroup(int id, string name);
    }
}
