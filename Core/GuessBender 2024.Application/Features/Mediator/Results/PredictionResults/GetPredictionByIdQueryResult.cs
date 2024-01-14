using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Results.PredictionResults
{
    public class GetPredictionByIdQueryResult
    {
        public int? Id { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public int MatchId { get; set; }
        public string UserId { get; set; }
        public DateTime PredictionTime { get; set; }
        public bool? MS1 { get; set; }
        public bool? MS2 { get; set; }
        public bool? MSX { get; set; }
    }
}
