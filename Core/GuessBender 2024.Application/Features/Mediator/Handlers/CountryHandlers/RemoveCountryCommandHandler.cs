using GuessBender_2024.Application.Features.Mediator.Commands.CountryCommands;
using GuessBender_2024.Application.Interfaces;
using GuessBender_2024.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Handlers.CountryHandlers
{
    public class RemoveCountryCommandHandler : IRequestHandler<RemoveCountryCommand>
    {
        private readonly IRepository<Country> _repository;

        public RemoveCountryCommandHandler(IRepository<Country> repository)
        {
            _repository = repository;
        }
        public async Task Handle(RemoveCountryCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.Id);
            await _repository.RemoveAsync(value);
        }
    }
    }

