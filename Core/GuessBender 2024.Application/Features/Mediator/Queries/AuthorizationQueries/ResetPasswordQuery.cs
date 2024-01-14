using GuessBender_2024.Application.Features.Mediator.Results.AuthorizationResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Commands.AuthorizationQueries
{
    public class ResetPasswordQuery:IRequest<ResetPasswordQueryResult>
    {
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
