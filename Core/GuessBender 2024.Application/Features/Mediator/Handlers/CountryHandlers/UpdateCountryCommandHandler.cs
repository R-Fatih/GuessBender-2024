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
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand>
    {
        private readonly IRepository<Country> _repository;

        public UpdateCountryCommandHandler(IRepository<Country> repository)
        {
            _repository = repository;
        }
        public async Task Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            values.LogoUrl = request.LogoUrl;
            values.Confederation=request.Confederation;
            values.Name = request.Name;
            await _repository.UpdateAsync(values);
        }
    }
}
