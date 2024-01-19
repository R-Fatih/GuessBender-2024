using GuessBender_2024.Application.Features.Mediator.Results.PredictionResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Queries.PredictionQueries
{
    public class GetPredictionCountByMatchIdQuery:IRequest<GetPredictionCountByMatchIdQueryResult>
    {
        public int MatchId { get; set; }

        public GetPredictionCountByMatchIdQuery(int matchId)
        {
            MatchId = matchId;
        }
    }
}
