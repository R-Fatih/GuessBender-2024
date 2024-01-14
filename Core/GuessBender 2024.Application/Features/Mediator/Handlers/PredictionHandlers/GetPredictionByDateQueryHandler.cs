using GuessBender_2024.Application.Features.Mediator.Queries.PredictionQueries;
using GuessBender_2024.Application.Features.Mediator.Results.PredictionResults;
using GuessBender_2024.Application.Interfaces;
using GuessBender_2024.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Handlers.PredictionHandlers
{
    public class GetPredictionByDateQueryHandler : IRequestHandler<GetPredictionByDateQuery, List<GetPredictionByDateQueryResult>>
    {
        private readonly IRepository<Prediction> _repository;

        public GetPredictionByDateQueryHandler(IRepository<Prediction> repository)
        {
            _repository = repository;
        }
        public async Task<List<GetPredictionByDateQueryResult>> Handle(GetPredictionByDateQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByFilterListAsync(x => x.PredictionTime.Date == request.Date.Date);
            return values.Select(x => new GetPredictionByDateQueryResult
            {
                AwayScore = x.AwayScore,
                HomeScore = x.HomeScore,
                Id = x.Id,
                MatchId = x.MatchId,
                MS1 = x.MS1,
                MS2 = x.MS2,
                MSX = x.MSX,
                PredictionTime = x.PredictionTime,
                UserId = x.UserId,
            }).ToList();
        }
    }
}
