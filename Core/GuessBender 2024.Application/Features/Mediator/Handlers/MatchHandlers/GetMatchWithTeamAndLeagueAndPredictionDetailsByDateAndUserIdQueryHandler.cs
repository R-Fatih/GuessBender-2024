using GuessBender_2024.Application.Features.Mediator.Queries.MatchQueries;
using GuessBender_2024.Application.Features.Mediator.Results.MatchResults;
using GuessBender_2024.Application.Interfaces.MatchInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Handlers.MatchHandlers
{
	public class GetMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserIdQueryHandler : IRequestHandler<GetMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserIdQuery, List<GetMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserIdQueryResult>>
	{
		private readonly IMatchRepository _repository;

		public GetMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserIdQueryHandler(IMatchRepository repository)
		{
			_repository = repository;
		}

		public async Task<List<GetMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserIdQueryResult>> Handle(GetMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserIdQuery request, CancellationToken cancellationToken)
		{

			var values = _repository.GetMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserId(request.Date,request.UserId);
			return values.Select(x => new GetMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserIdQueryResult
			{
				Id = x.Id,
				AwayAbbr = x.AwayTeam.Abbr,
				AwayCountryId = x.AwayTeam.CountryId,
				AwayLogoId = x.AwayTeam.LogoId,
				AwayMS = x.AwayMS,
				AwayTeamId = x.AwayTeamId,
				AwayTeamName = x.AwayTeam.Name,
				Code = x.Code,
				Date = x.Date,
				Expired = x.Expired,
				HomeAbbr = x.HomeTeam.Abbr,
				HomeCountryId = x.HomeTeam.CountryId,
				HomeLogoId = x.HomeTeam.LogoId,
				HomeMS = x.HomeMS,
				HomeTeamId = x.HomeTeamId,
				HomeTeamName = x.HomeTeam.Name,
				LeagueAbbr = x.League.Abbr,
				LeagueCountryId = x.League.CountryId,
				LeagueId = x.League.Id,
				LeagueName = x.League.Name,
				AwayScorePrediction=x.Prediction.Where(x=>x.UserId==request.UserId).FirstOrDefault()?.AwayScore,
				HomeScorePrediction=x.Prediction.Where(x=>x.UserId==request.UserId).FirstOrDefault()?.HomeScore,
				MS1=x.Prediction.Where(x=>x.UserId==request.UserId).FirstOrDefault()?.MS1,
				MS2=x.Prediction.Where(x=>x.UserId==request.UserId).FirstOrDefault()?.MS2,
				MSX=x.Prediction.Where(x=>x.UserId==request.UserId).FirstOrDefault()?.MSX,
				PredictionTime= x.Prediction.Where(x => x.UserId == request.UserId).FirstOrDefault()?.PredictionTime,
			}).ToList();
		}
	}
	}

