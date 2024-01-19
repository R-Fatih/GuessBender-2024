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
	public class UpdatePredictionCommandHandler
	: IRequestHandler<UpdatePredictionCommand>
	{
		private readonly IRepository<Prediction> _repository;

		public UpdatePredictionCommandHandler(IRepository<Prediction> repository)
		{
			_repository = repository;
		}

		public async Task Handle(UpdatePredictionCommand request, CancellationToken cancellationToken)
		{
			var value = await _repository.GetByIdAsync(request.Id);
			value.MSX=request.MSX;
			value.MS1=request.MS1;
			value.MS2=request.MS2;
			value.AwayScore=request.AwayScore;
			value.HomeScore=request.HomeScore;
			value.MatchId=request.MatchId;
			value.PredictionTime=request.PredictionTime;
			value.UserId=request.UserId;
			await _repository.UpdateAsync(value);
		}
	}
}
