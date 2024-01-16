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
    public class GetStandingQueryHandler : IRequestHandler<GetStandingQuery, List<GetStandingQueryResult>>
    {
        private readonly IStandingRepository _repository;

        public GetStandingQueryHandler(IStandingRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetStandingQueryResult>> Handle(GetStandingQuery request, CancellationToken cancellationToken)
        {
            var values = _repository.GetAllStanding();
            return values.Select(x => new GetStandingQueryResult
            {
                MSEyt_Points = x.MSEyt_Points,
                MS_Points = x.MS_Points,
                ScoreEyt_Points = x.ScoreEyt_Points,
                Score_Points = x.Score_Points,
                Total_Points = x.Total_Points,
                Total_Prediction = x.Total_Prediction,
                Username = x.Username
            }).ToList();
        }
    }
}
