using GuessBender_2024.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Interfaces.AuthorizationInterfaces
{
    public interface IAuthorizationRepository
    {
        User GetUserByUserName(string username);
        List<UserRole> GetRoleByUserName(string username);
    }
}
