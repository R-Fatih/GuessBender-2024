using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Domain.Entities
{
    public class Role
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<UserRole> UserRoles { get; set; }

    }
}
