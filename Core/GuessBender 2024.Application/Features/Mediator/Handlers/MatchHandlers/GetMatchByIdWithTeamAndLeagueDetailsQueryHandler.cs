using GuessBender_2024.Application.Features.Mediator.Queries.MatchQueries;
using GuessBender_2024.Application.Features.Mediator.Results.MatchResults;
using GuessBender_2024.Application.Interfaces;
using GuessBender_2024.Application.Interfaces.MatchInterfaces;
using GuessBender_2024.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Handlers.MatchHandlers
{
    public class GetMatchByIdWithTeamAndLeagueDetailsQueryHandler:IRequestHandler<GetMatchByIdWithTeamAndLeagueDetailsQuery,GetMatchByIdWithTeamAndLeagueDetailsQueryResult>
    {
        private readonly IMatchRepository _repository;

        public GetMatchByIdWithTeamAndLeagueDetailsQueryHandler(IMatchRepository repostitory)
        {
            _repository = repostitory;
        }

        public async Task<GetMatchByIdWithTeamAndLeagueDetailsQueryResult> Handle(GetMatchByIdWithTeamAndLeagueDetailsQuery request, CancellationToken cancellationToken)
        {
            var values =  _repository.GetMatchByIdWithTeamAndLeagueDetails(request.Id);
            return new GetMatchByIdWithTeamAndLeagueDetailsQueryResult
            {
                Id = request.Id,
                AwayAbbr = values.AwayTeam.Abbr,
                AwayCountryId = values.AwayTeam.CountryId,
                AwayLogoId = values.AwayTeam.LogoId,
                AwayMS = values.AwayMS,
                AwayTeamId = values.AwayTeamId,
                AwayTeamName = values.AwayTeam.Name,
                Code = values.Code,
                Date = values.Date,
                Expired = values.Expired,
                HomeAbbr = values.HomeTeam.Abbr,
                HomeCountryId = values.HomeTeam.CountryId,
                HomeLogoId = values.HomeTeam.LogoId,
                HomeMS = values.HomeMS,
                HomeTeamId = values.HomeTeamId,
                HomeTeamName = values.HomeTeam.Name,
                LeagueAbbr = values.League.Abbr,
                LeagueCountryId = values.League.CountryId,
                LeagueId = values.League.Id,
                LeagueName = values.League.Name
            };
        }
    }
}
