using GuessBender_2024.Application.Features.Mediator.Queries.LeagueStandingQueries;
using GuessBender_2024.Application.Features.Mediator.Results.LeagueStandingResults;
using GuessBender_2024.Application.Interfaces.LeagueStandingInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Handlers.LeagueStandingHandlers
{
    public class GetLeagueStandingByLeagueIdQueryHandler : IRequestHandler<GetLeagueStandingByLeagueIdQuery, List<GetLeagueStandingByLeagueIdQueryResult>>
    {
        private readonly ILeagueStandingRepository _repository;

        public GetLeagueStandingByLeagueIdQueryHandler(ILeagueStandingRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetLeagueStandingByLeagueIdQueryResult>> Handle(GetLeagueStandingByLeagueIdQuery request, CancellationToken cancellationToken)
        {
            var values = _repository.GeteagueStandingByLeagueId(request.LeagueId);
            return values.Select(x => new GetLeagueStandingByLeagueIdQueryResult
            {
                Average = x.Average,
                LeagueId = x.LeagueId,
                Draws = x.Draws,
                GoalsAgainst = x.GoalsAgainst,
                GoalsFor = x.GoalsFor,
                LogoId = x.LogoId,
                Loses = x.Loses,
                Name = x.Name,
                Points = x.Points,
                Wins = x.Wins
            }).ToList();
        }
    }
}
