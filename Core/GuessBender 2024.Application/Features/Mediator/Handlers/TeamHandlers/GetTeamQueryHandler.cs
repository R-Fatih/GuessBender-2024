using GuessBender_2024.Application.Features.Mediator.Queries.TeamQueries;
using GuessBender_2024.Application.Features.Mediator.Results.TeamResults;
using GuessBender_2024.Application.Interfaces;
using GuessBender_2024.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Handlers.TeamHandlers
{
    public class GetTeamQueryHandler : IRequestHandler<GetTeamQuery, List<GetTeamQueryResult>>
    {
        private readonly IRepository<Team> _repository;

        public GetTeamQueryHandler(IRepository<Team> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetTeamQueryResult>> Handle(GetTeamQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetTeamQueryResult
            {
                Abbr = x.Abbr,
                CountryId = x.CountryId,
                Id = x.Id,
                Name = x.Name,
                LogoId= x.LogoId
            }).ToList();
        }
    }
}
