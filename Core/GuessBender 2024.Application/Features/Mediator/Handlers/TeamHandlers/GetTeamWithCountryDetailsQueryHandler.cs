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
    public class GetTeamWithCountryDetailsQueryHandler : IRequestHandler<GetTeamWithCountryDetailsQuery, List<GetTeamWithCountryDetailsQueryResult>>
    {
        private readonly IRepository<Team> _repository;

        public GetTeamWithCountryDetailsQueryHandler(IRepository<Team> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetTeamWithCountryDetailsQueryResult>> Handle(GetTeamWithCountryDetailsQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.Include(x=>x.Country);
            return values.Select(x => new GetTeamWithCountryDetailsQueryResult
            {
                Abbr = x.Abbr,
                CountryId = x.CountryId,
                CountryLogoUrl = x.Country.LogoUrl,
                CountryName = x.Country.Name,
                Id = x.Id,
                Name = x.Name,
                LogoId=x.LogoId
            }).ToList();
        }
    }
}
