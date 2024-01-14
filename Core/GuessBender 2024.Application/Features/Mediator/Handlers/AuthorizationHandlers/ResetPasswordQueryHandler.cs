using GuessBender_2024.Application.Features.Mediator.Commands.AuthorizationQueries;
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
    public class ResetPasswordQueryHandler : IRequestHandler<ResetPasswordQuery, ResetPasswordQueryResult>
    {
        private readonly IRepository<User> _repository;
        private readonly IAuthorizationRepository _repository2;
        private readonly int _iteration = 6;
        private readonly string _pepper = "quaresmakonyasporlularıniçindengeçtigolüattı";
        public ResetPasswordQueryHandler(IRepository<User> repository, IAuthorizationRepository repository2)
        {
            _repository = repository;
            _repository2 = repository2;
        }




        public async Task<ResetPasswordQueryResult> Handle(ResetPasswordQuery request, CancellationToken cancellationToken)
        {
            ResetPasswordQueryResult result = new();
            var user = _repository2.GetUserByUserName(request.Username);
            if (user != null)
            {

                var passwordHash = PasswordHasher.ComputeHash(request.OldPassword, user.PasswordSalt, _pepper, _iteration);
                if (passwordHash == user.PasswordHash)
                {
                    var newpasswordsalt = PasswordHasher.GenerateSalt();
                    user.PasswordSalt = newpasswordsalt;
                    user.PasswordHash = PasswordHasher.ComputeHash(request.NewPassword, newpasswordsalt, _pepper, _iteration);
                    await _repository.UpdateAsync(user);
                    result.Message = "Şifre başarıyla değiştirildi.";
                }
                else
                    result.Message = "Eski şifre hatalı";

            }
            result.Message = "Böyle bir kullanıcı bulunamadı";

            return result;
        }
    }
}
