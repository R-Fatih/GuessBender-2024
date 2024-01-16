using GuessBender_2024.Application.Features.Mediator.Results.StandingResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Queries.StandingQueries
{
    public class GetStandingByCountryIdQuery : IRequest<List<GetStandingQueryResult>>
    {
        public int CountryId { get; set; }

        public GetStandingByCountryIdQuery(int countryId)
        {
            CountryId = countryId;
        }
    }
}
