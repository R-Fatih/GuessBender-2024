using GuessBender_2024.Application.Interfaces.AuthorizationInterfaces;
using GuessBender_2024.Domain.Entities;
using GuessBender_2024.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Persistance.Repositories.AuthorizationRepositories
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly GuessBenderContext _context;

        public AuthorizationRepository(GuessBenderContext context)
        {
            _context = context;
        }

        public List<UserRole> GetRoleByUserName(string username)
        {
            var user = _context.Users.Where(x => x.Username == username).FirstOrDefault();
            var value = _context.UserRoles.Include(x=>x.Role).Where(x => x.UserId == user.Id).ToList();
            return value;
        }

        public User GetUserByUserName(string username)
        {
            return _context.Users.Where(x => x.Username == username).FirstOrDefault();
        }
    }
}
