using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Commands.TeamCommands
{
    public class RemoveTeamCommand:IRequest
    {
        public int Id { get; set; }

        public RemoveTeamCommand(int id)
        {
            Id = id;
        }
    }
}
