using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Commands.TeamCommands
{
    public class CreateTeamCommand:IRequest
    {
        public string Name { get; set; }
        public string Abbr { get; set; }
        public int CountryId { get; set; }
        public int LogoId { get; set; }
    }
}
