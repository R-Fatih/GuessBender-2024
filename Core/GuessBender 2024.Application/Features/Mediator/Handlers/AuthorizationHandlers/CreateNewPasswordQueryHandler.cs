using GuessBender_2024.Application.Features.Mediator.Queries.AuthorizationQueries;
using GuessBender_2024.Application.Features.Mediator.Results.AuthorizationResults;
using GuessBender_2024.Application.Interfaces.AuthorizationInterfaces;
using GuessBender_2024.Application.Interfaces;
using GuessBender_2024.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuessBender_2024.Application.Tools;

namespace GuessBender_2024.Application.Features.Mediator.Handlers.AuthorizationHandlers
{
    public class CreateNewPasswordQueryHandler : IRequestHandler<CreateNewPasswordQuery, CreateNewPasswordQueryResult>
    {
        private readonly IRepository<User> _repository;
        private readonly IAuthorizationRepository _repository2;
        private readonly int _iteration = 6;
        private readonly string _pepper = "quaresmakonyasporlularıniçindengeçtigolüattı";
        public CreateNewPasswordQueryHandler(IRepository<User> repository, IAuthorizationRepository repository2)
        {
            _repository = repository;
            _repository2 = repository2;
        }
        public async Task<CreateNewPasswordQueryResult> Handle(CreateNewPasswordQuery request, CancellationToken cancellationToken)
        {
            var message = "";
            var newpassword = "";
            var user = _repository2.GetUserByUserName(request.Username);
            if (user != null)
            {
                message = "Success";
                var newpasswordsalt = PasswordHasher.GenerateSalt();
                 newpassword = PasswordGenerator.GeneratePassword();
                var passwordHash = PasswordHasher.ComputeHash(newpassword, newpasswordsalt, _pepper, _iteration);

                user.PasswordSalt = newpasswordsalt;
                user.PasswordHash = passwordHash;
                await _repository.UpdateAsync(user);

            }
            else
                message = "Böyle bir kullanıcı bulunamadı.";
            return new CreateNewPasswordQueryResult
            {
                NewPassword = newpassword,
                Message= message
            };
        }
    }
}
