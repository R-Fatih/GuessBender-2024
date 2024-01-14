using GuessBender_2024.Application.Features.Mediator.Queries.AuthorizationQueries;
using GuessBender_2024.Application.Features.Mediator.Results.AuthorizationResults;
using GuessBender_2024.Application.Interfaces;
using GuessBender_2024.Application.Interfaces.AuthorizationInterfaces;
using GuessBender_2024.Application.Tools;
using GuessBender_2024.Domain.Entities;
using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Handlers.AuthorizationHandlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginQueryResult>
    {
        private readonly IRepository<User> _appUserRepository;
        private readonly IAuthorizationRepository _appRoleRepository;
        private readonly int _iteration = 6;
        private readonly string _pepper = "quaresmakonyasporlularıniçindengeçtigolüattı";

        public LoginQueryHandler(IRepository<User> appUserRepository, IAuthorizationRepository appRoleRepository)
        {
            _appUserRepository = appUserRepository;
            _appRoleRepository = appRoleRepository;
        }

        public async Task<LoginQueryResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var username = await _appUserRepository.GetByFilterAsync(x => x.Username == request.UserName);
            var values = new LoginQueryResult();
            if (username != null)
            {

                var passwordHash = PasswordHasher.ComputeHash(request.Password, username.PasswordSalt, _pepper, _iteration);


                var user = await _appUserRepository.GetByFilterAsync(x => x.Username == request.UserName && x.PasswordHash == passwordHash);
                if (user == null)
                    values.IsExist = false;
                else
                {
                    values.IsExist = true;
                    values.UserName = user.Username;
                    values.Role = _appRoleRepository.GetRoleByUserName(request.UserName).First().Role.RoleName;

                    values.Id = user.Id;
                }
            }
            else
                values.IsExist=false;
            return values;
        }
    }
}
