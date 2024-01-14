using GuessBender_2024.Application.Features.Mediator.Queries.MatchQueries;
using GuessBender_2024.Application.Features.Mediator.Results.MatchResults;
using GuessBender_2024.Application.Interfaces.TeamInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Handlers.MatchHandlers
{
    public class GetMatchWithTeamAndLeagueDetailsQueryHandler:IRequestHandler<GetMatchWithTeamAndLeagueDetailsQuery,List<GetMatchWithTeamAndLeagueDetailsQueryResult>>
    {
        private readonly IMatchRepository _repository;

        public GetMatchWithTeamAndLeagueDetailsQueryHandler(IMatchRepository repostitory)
        {
            _repository = repostitory;
        }

        public async Task<List<GetMatchWithTeamAndLeagueDetailsQueryResult>> Handle(GetMatchWithTeamAndLeagueDetailsQuery request, CancellationToken cancellationToken)
        {
            var values = _repository.GetMatchWithTeamAndLeagueDetails();
            return values.Select(x => new GetMatchWithTeamAndLeagueDetailsQueryResult
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
                LeagueName = x.League.Name
            }).ToList();
        }
    }
}
