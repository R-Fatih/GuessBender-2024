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
    public class CreateLeagueCommandHandler : IRequestHandler<CreateLeagueCommand>
    {
        private readonly IRepository<League> _repository;

        public CreateLeagueCommandHandler(IRepository<League> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateLeagueCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new League
            {
              
                Name = request.Name,
                Abbr=request.Abbr,
                CountryId=request.CountryId,
                
            });
        }
    }
}
