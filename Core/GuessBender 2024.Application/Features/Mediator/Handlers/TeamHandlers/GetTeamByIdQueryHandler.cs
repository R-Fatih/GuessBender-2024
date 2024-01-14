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
    public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, GetTeamByIdQueryResult>
    {
        private readonly IRepository<Team> _repository;

        public GetTeamByIdQueryHandler(IRepository<Team> repository)
        {
            _repository = repository;
        }

        public async Task<GetTeamByIdQueryResult> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            return new GetTeamByIdQueryResult
            {
                Id = request.Id,
                Abbr = values.Abbr,
                CountryId = values.CountryId,
                Name = values.Name,
                LogoId=values.LogoId,
                
            };
        }
    }
}
