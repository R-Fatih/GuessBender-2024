using GuessBender_2024.Application.Features.Mediator.Commands.LeagueCommands;
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
    public class RemoveLeagueCommandHandler : IRequestHandler<RemoveLeagueCommand>
    {
        private readonly IRepository<League> _repository;

        public RemoveLeagueCommandHandler(IRepository<League> repository)
        {
            _repository = repository;
        }
        public async Task Handle(RemoveLeagueCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.Id);
            await _repository.RemoveAsync(value);
        }
    }
    }

