using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Results.PredictionResults
{
    public class GetPredictionByIdQuery:IRequest<GetPredictionByIdQueryResult>
    {
        public int Id { get; set; }

        public GetPredictionByIdQuery(int id)
        {
            Id = id;
        }
    }
}
