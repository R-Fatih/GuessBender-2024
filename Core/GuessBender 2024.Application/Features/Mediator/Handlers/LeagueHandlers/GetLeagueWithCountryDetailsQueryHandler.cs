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
    public class GetLeagueWithCountryDetailsQueryHandler : IRequestHandler<GetLeagueWithCountryDetailsQuery, List<GetLeagueWithCountryDetailsQueryResult>>
    {
        private readonly IRepository<League> _repository;

        public GetLeagueWithCountryDetailsQueryHandler(IRepository<League> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetLeagueWithCountryDetailsQueryResult>> Handle(GetLeagueWithCountryDetailsQuery request, CancellationToken cancellationToken)
        {
            var values =await _repository.Include(x=>x.Country);
            return values.Select(x => new GetLeagueWithCountryDetailsQueryResult
            {
                Abbr = x.Abbr,
                CountryId = x.CountryId,
                CountryLogoUrl = x.Country.LogoUrl,
                CountryName = x.Country.Name,
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}
