using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Commands.LeagueCommands
{
    public class RemoveLeagueCommand:IRequest
    {
        public int Id { get; set; }

        public RemoveLeagueCommand(int id)
        {
            Id = id;
        }
    }
}
