using GuessBender_2024.Application.Features.Mediator.Queries.LeagueQueries;
using GuessBender_2024.Application.Features.Mediator.Results.LeagueResults;
using GuessBender_2024.Application.Interfaces;
using GuessBender_2024.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Handlers.LeagueHandlers
{
    public class GetLeagueByIdQueryHandler : IRequestHandler<GetLeagueByIdQuery, GetLeagueByIdQueryResult>
    {
        private readonly IRepository<League> _repository;

        public GetLeagueByIdQueryHandler(IRepository<League> repository)
        {
            _repository = repository;
        }

        public async Task<GetLeagueByIdQueryResult> Handle(GetLeagueByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            return new GetLeagueByIdQueryResult
            {
                Id = request.Id,
                Abbr = values.Abbr,
                CountryId = values.CountryId,
                Name = values.Name
            };
        }
    }
}
