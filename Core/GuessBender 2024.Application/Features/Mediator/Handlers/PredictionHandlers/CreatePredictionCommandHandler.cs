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
	public class CreatePredictionCommandHandler : IRequestHandler<CreatePredictionCommand>
	{
		private readonly IRepository<Prediction> _repository;

		public CreatePredictionCommandHandler(IRepository<Prediction> repository)
		{
			_repository = repository;
		}

		public async Task Handle(CreatePredictionCommand request, CancellationToken cancellationToken)
		{
			await _repository.CreateAsync(new Prediction
			{
				AwayScore = request.AwayScore,
				HomeScore = request.HomeScore,
				MatchId = request.MatchId,
				MS1 = request.MS1,
				MS2 = request.MS2,
				MSX = request.MSX,
				PredictionTime = request.PredictionTime,
				UserId = request.UserId
				
			});

		}
	}
}
