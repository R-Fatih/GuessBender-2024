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
    public class UpdateMatchCommandHandler:IRequestHandler<UpdateMatchCommand>
    {
        private readonly IRepository<Match> _repository;

        public UpdateMatchCommandHandler(IRepository<Match> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateMatchCommand request, CancellationToken cancellationToken)
        {
            var values=await _repository.GetByIdAsync(request.Id);
            values.AwayMS = request.AwayMS;
            values.LeagueId = request.LeagueId;
            values.AwayTeamId = request.AwayTeamId;
            values.Code = request.Code;
            values.Date = request.Date;
            values.Expired = request.Expired;
            values.HomeMS = request.HomeMS;
            values.HomeTeamId = request.HomeTeamId;
            await _repository.UpdateAsync(values);
        }
    }
}
