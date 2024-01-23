using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Results.LeagueStandingResults
{
    public class GetLeagueStandingByLeagueIdQueryResult
    {
        public string Name { get; set; }
        public int LeagueId { get; set; }
        public int LogoId { get; set; }
        public int Points { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Loses { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int Average { get; set; }
    }
}
