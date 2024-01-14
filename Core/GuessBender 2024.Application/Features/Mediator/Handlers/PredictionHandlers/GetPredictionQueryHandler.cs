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
    public class GetPredictionQueryHandler:IRequestHandler<GetPredictionQuery,List<GetPredictionQueryResult>>
    {
        private readonly IRepository<Prediction> _repository;

        public GetPredictionQueryHandler(IRepository<Prediction> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetPredictionQueryResult>> Handle(GetPredictionQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetPredictionQueryResult
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
