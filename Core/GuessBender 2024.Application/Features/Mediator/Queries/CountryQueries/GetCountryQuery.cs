using GuessBender_2024.Application.Features.Mediator.Results.CountryResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Queries.CountryQueries
{
    public class GetCountryQuery:IRequest<List<GetCountryQueryResult>>
    {
    }
}
