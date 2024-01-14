using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Commands.MatchCommands
{
    public class RemoveMatchCommand:IRequest
    {
        public int Id { get; set; }

        public RemoveMatchCommand(int id)
        {
            Id = id;
        }
    }
}
