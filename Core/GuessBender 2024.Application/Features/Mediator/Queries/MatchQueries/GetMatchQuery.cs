﻿using GuessBender_2024.Application.Features.Mediator.Results.MatchResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Queries.MatchQueries
{
    public class GetMatchQuery:IRequest<List<GetMatchQueryResult>>
    {
    }
}
