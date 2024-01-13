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
    public class GetCountryByIdQueryHandler:IRequestHandler<GetCountryByIdQuery, GetCountryByIdQueryResult>
    {
        private readonly IRepository<Country> _repository;

        public GetCountryByIdQueryHandler(IRepository<Country> repository)
        {
            _repository = repository;
        }

        public async Task<GetCountryByIdQueryResult> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            var values =await _repository.GetByIdAsync(request.Id);
            return new GetCountryByIdQueryResult
            {
                Id = values.Id,
                Name = values.Name
            };
        }
    }
}
