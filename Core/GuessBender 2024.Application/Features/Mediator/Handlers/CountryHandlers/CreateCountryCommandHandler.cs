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
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand>
    {
        private readonly IRepository<Country> _repository;

        public CreateCountryCommandHandler(IRepository<Country> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Country
            {
                Confederation = request.Confederation,
                LogoUrl = request.LogoUrl,
                Name = request.Name
            });
        }
    }
}
