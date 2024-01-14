﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Results.PredictionResults
{
    public class GetPredictionByUserIdQuery:IRequest< List<GetPredictionByUserIdQueryResult>>
    {
        public string UserId { get; set; }

        public GetPredictionByUserIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
