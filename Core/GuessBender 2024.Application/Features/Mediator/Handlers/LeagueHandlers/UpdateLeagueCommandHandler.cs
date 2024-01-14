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
    public class UpdateLeagueCommandHandler : IRequestHandler<UpdateLeagueCommand>
    {
        private readonly IRepository<League> _repository;

        public UpdateLeagueCommandHandler(IRepository<League> repository)
        {
            _repository = repository;
        }
        public async Task Handle(UpdateLeagueCommand request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.Id);
            values.Abbr = request.Abbr;
            values.CountryId=request.CountryId;
            values.Name = request.Name;
            await _repository.UpdateAsync(values);
        }
    }
}
