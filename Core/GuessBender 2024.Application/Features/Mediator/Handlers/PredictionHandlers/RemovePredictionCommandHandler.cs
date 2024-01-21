using GuessBender_2024.Application.Features.Mediator.Commands.PredictionCommands;
using GuessBender_2024.Application.Interfaces;
using GuessBender_2024.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Handlers.PredictionHandlers
{
    public class RemovePredictionCommandHandler:IRequestHandler<RemovePredictionCommand>
    {
        private readonly IRepository<Prediction> _repository;

        public RemovePredictionCommandHandler(IRepository<Prediction> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemovePredictionCommand request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            await _repository.RemoveAsync(values);
        }
    }
}
