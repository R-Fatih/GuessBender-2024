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
    public class GetPredictionCountByMatchIdQueryHandler : IRequestHandler<GetPredictionCountByMatchIdQuery, GetPredictionCountByMatchIdQueryResult>
    {
        IRepository<Prediction> _repository;

        public GetPredictionCountByMatchIdQueryHandler(IRepository<Prediction> repository)
        {
            _repository = repository;
        }

        public async Task<GetPredictionCountByMatchIdQueryResult> Handle(GetPredictionCountByMatchIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByFilterListAsync(x => x.MatchId == request.MatchId);
            return new GetPredictionCountByMatchIdQueryResult
            {
                Count = values.Count
            };
        }
    }
}
