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
    public class CreateMatchCommandHandler : IRequestHandler<CreateMatchCommand>
    {
        private readonly IRepository<Match> _repository;

        public CreateMatchCommandHandler(IRepository<Match> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Match
            {
                AwayMS = request.AwayMS,
                AwayTeamId = request.AwayTeamId,
                Code = request.Code,
                Date = request.Date,
                Expired = request.Expired,
                HomeMS = request.HomeMS,
                HomeTeamId = request.HomeTeamId,
                LeagueId = request.LeagueId,

            });
        }
    }
}
