using GuessBender_2024.Application.Features.Mediator.Results.StandingResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Queries.StandingQueries
{
    public class GetStandingMatchesWithDetailsByUsernameQuery:IRequest<List<GetStandingMatchesWithDetailsByUsernameQueryResult>>
    {
        public string Username { get; set; }

        public GetStandingMatchesWithDetailsByUsernameQuery(string userName)
        {
            Username = userName;
        }
    }
}
