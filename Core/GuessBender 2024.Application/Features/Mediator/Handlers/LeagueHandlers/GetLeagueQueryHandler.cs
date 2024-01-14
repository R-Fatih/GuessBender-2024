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
    public class GetLeagueQueryHandler : IRequestHandler<GetLeagueQuery, List<GetLeagueQueryResult>>
    {
        private readonly IRepository<League> _repository;

        public GetLeagueQueryHandler(IRepository<League> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetLeagueQueryResult>> Handle(GetLeagueQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetLeagueQueryResult
            {
                Abbr = x.Abbr,
                CountryId = x.CountryId,
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }
    }
}
