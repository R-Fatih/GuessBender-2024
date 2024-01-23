using GuessBender_2024.Application.Features.Mediator.Results.LeagueStandingResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Queries.LeagueStandingQueries
{
    public class GetLeagueStandingByLeagueIdQuery:IRequest<List<GetLeagueStandingByLeagueIdQueryResult>>
    {
        public int LeagueId { get; set; }

        public GetLeagueStandingByLeagueIdQuery(int leagueId)
        {
            LeagueId = leagueId;
        }
    }
}
