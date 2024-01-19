using GuessBender_2024.Application.Features.Mediator.Results.MatchResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Queries.MatchQueries
{
	public class GetMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserIdQuery : IRequest<List<GetMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserIdQueryResult>>
	{
		public DateTime Date { get; set; }
        public string UserId { get; set; }
		public GetMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserIdQuery(DateTime date, string userId)
		{
			Date = date;
			UserId = userId;
		}

	}
}
