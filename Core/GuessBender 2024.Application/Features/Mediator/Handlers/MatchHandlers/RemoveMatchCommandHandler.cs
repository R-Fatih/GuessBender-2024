using GuessBender_2024.Application.Features.Mediator.Commands.MatchCommands;
using GuessBender_2024.Application.Interfaces;
using GuessBender_2024.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Handlers.MatchHandlers
{
    public class RemoveMatchCommandHandler:IRequestHandler<RemoveMatchCommand>
    {
        private readonly IRepository<Match> _repository;

        public RemoveMatchCommandHandler(IRepository<Match> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveMatchCommand request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            await _repository.RemoveAsync(values);
        }
    }
}
