using GuessBender_2024.Application.Features.Mediator.Results.MatchResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Queries.MatchQueries
{
    public class GetMatchWithTeamAndLeagueDetailsByDatesBetweenQuery:IRequest<List<GetMatchWithTeamAndLeagueDetailsByDatesBetweenQueryResult>>
    {
        public GetMatchWithTeamAndLeagueDetailsByDatesBetweenQuery(DateTime date1, DateTime date2)
        {
            Date1 = date1;
            Date2 = date2;
        }

        public DateTime Date1 { get; set; }
        public DateTime Date2 { get; set; }

       
    }
}
