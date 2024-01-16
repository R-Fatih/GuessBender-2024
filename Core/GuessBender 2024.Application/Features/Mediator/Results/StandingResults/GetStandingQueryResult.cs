using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Results.StandingResults
{
    public class GetStandingQueryResult
    {
        public string Username { get; set; }
        public int Total_Points { get; set; }
        public int Score_Points { get; set; }
        public int ScoreEyt_Points { get; set; }
        public int MS_Points { get; set; }
        public int MSEyt_Points { get; set; }
        public int Total_Prediction { get; set; }
    }
}
