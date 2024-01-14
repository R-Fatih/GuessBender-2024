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
    public class RemoveTeamCommandHandler : IRequestHandler<RemoveTeamCommand>
    {
        private readonly IRepository<Team> _repository;

        public RemoveTeamCommandHandler(IRepository<Team> repository)
        {
            _repository = repository;
        }
        public async Task Handle(RemoveTeamCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.Id);
            await _repository.RemoveAsync(value);
        }
    }
    }

