using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Commands.MatchCommands
{
    public class UpdateMatchCommand : IRequest
    {
        public int Id { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int LeagueId { get; set; }
        public DateTime Date { get; set; }
        public int? HomeMS { get; set; }
        public int? AwayMS { get; set; }
        public bool? Expired { get; set; }
        public long Code { get; set; }
    }
}
