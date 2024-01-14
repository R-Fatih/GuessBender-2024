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
    public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand>
    {
        private readonly IRepository<Team> _repository;

        public UpdateTeamCommandHandler(IRepository<Team> repository)
        {
            _repository = repository;
        }
        public async Task Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            values.Abbr = request.Abbr;
            values.CountryId=request.CountryId;
            values.Name = request.Name;
            values.LogoId = request.LogoId;
            await _repository.UpdateAsync(values);
        }
    }
}
