using GuessBender_2024.Application.Features.Mediator.Queries.StandingQueries;
using GuessBender_2024.Application.Features.Mediator.Results.StandingResults;
using GuessBender_2024.Application.Interfaces.StandingInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Handlers.StandingHandlers
{
    public class GetStandingMatchesWithDetailsByUsernameQueryHandler : IRequestHandler<GetStandingMatchesWithDetailsByUsernameQuery, List<GetStandingMatchesWithDetailsByUsernameQueryResult>>
    {
        private readonly IStandingRepository _repository;

        public GetStandingMatchesWithDetailsByUsernameQueryHandler(IStandingRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetStandingMatchesWithDetailsByUsernameQueryResult>> Handle(GetStandingMatchesWithDetailsByUsernameQuery request, CancellationToken cancellationToken)
        {
            var values = _repository.GetAllStandingMatchesByUsername(request.Username);
            return values.Select(x => new GetStandingMatchesWithDetailsByUsernameQueryResult
            {
                Username = x.Username,
                AwayMS = x.AwayMS,
                Awayscore = x.Awayscore,
                AwayTeamLogoId = x.AwayTeamLogoId,
                AwayTeamName = x.AwayTeamName,
                Date = x.Date,
                HomeMS = x.HomeMS,
                HomeScore = x.HomeScore,
                HomeTeamLogoId = x.HomeTeamLogoId,
                HomeTeamName = x.HomeTeamName,
                Ms1 = x.Ms1,
                Ms2 = x.Ms2,
                Msx = x.Msx,
                PredictionTime = x.PredictionTime,
                Total_Points = x.Total_Points,
                UserId = x.UserId,
            }).ToList();
        }
    }
}
