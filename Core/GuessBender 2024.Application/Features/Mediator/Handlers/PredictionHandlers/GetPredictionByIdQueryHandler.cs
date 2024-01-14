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
    public class GetPredictionByIdQueryHandler : IRequestHandler<GetPredictionByIdQuery, GetPredictionByIdQueryResult>
    {
        private readonly IRepository<Prediction> _repository;

        public GetPredictionByIdQueryHandler(IRepository<Prediction> repository)
        {
            _repository = repository;
        }

        public async Task<GetPredictionByIdQueryResult> Handle(GetPredictionByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            return new GetPredictionByIdQueryResult
            {
                AwayScore = values.AwayScore,
                HomeScore = values.HomeScore,
                Id = request.Id,
                MatchId = values.MatchId,
                MS1 = values.MS1,
                MS2 = values.MS2,
                MSX = values.MSX,
                PredictionTime = values.PredictionTime,
                UserId = values.UserId
            };
        }
    }
}
