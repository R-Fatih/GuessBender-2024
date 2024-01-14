using GuessBender_2024.Application.Features.Mediator.Results.PredictionResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Queries.PredictionQueries
{
    public class GetPredictionByDateQuery:IRequest<List<GetPredictionByDateQueryResult>>
    {
        public DateTime Date { get; set; }

        public GetPredictionByDateQuery(DateTime date)
        {
            Date = date;
        }
    }
}
