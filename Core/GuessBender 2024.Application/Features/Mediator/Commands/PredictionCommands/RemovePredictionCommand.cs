using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Commands.PredictionCommands
{
	public class RemovePredictionCommand:IRequest
	{
		public int Id { get; set; }

		public RemovePredictionCommand(int id)
		{
			Id = id;
		}
	}
}
