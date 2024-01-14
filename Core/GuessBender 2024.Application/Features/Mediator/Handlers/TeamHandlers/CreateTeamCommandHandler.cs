using GuessBender_2024.Application.Features.Mediator.Commands.TeamCommands;
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
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand>
    {
        private readonly IRepository<Team> _repository;

        public CreateTeamCommandHandler(IRepository<Team> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Team
            {
              
                Name = request.Name,
                Abbr=request.Abbr,
                CountryId=request.CountryId,
                LogoId=request.LogoId,
                
            });
        }
    }
}
