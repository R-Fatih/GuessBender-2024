using GuessBender_2024.Application.Features.Mediator.Queries.CountryQueries;
using GuessBender_2024.Application.Features.Mediator.Results.CountryResults;
using GuessBender_2024.Application.Interfaces;
using GuessBender_2024.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Handlers.CountryHandlers
{
    public class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, List<GetCountryQueryResult>>
    {
        private readonly IRepository<Country> _repository;

        public GetCountryQueryHandler(IRepository<Country> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetCountryQueryResult>> Handle(GetCountryQuery request, CancellationToken cancellationToken)
        {
            var values =await _repository.GetAllAsync();
            return values.Select(x => new GetCountryQueryResult
            {
                Id = x.Id,
                Name = x.Name,
                LogoUrl = x.LogoUrl,
                Confederation=x.Confederation
            }).ToList();
        }
    }
}
