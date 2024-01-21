using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Dtos.MatchDtos
{
    public class ResultMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserIdDto
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
        public string HomeTeamName { get; set; }
        public string HomeAbbr { get; set; }
        public int HomeCountryId { get; set; }
        public int HomeLogoId { get; set; }
        public string AwayTeamName { get; set; }
        public string AwayAbbr { get; set; }
        public int AwayCountryId { get; set; }
        public int AwayLogoId { get; set; }
        public int LeagueCountryId { get; set; }
        public string LeagueName { get; set; }
        public string LeagueAbbr { get; set; }
		public int? HomeScorePrediction { get; set; }
		public int? AwayScorePrediction { get; set; }
		public bool? MS1 { get; set; }
		public bool? MS2 { get; set; }
		public bool? MSX { get; set; }
		public DateTime? PredictionTime { get; set; }
		public int? PredictionId { get; set; }


	}
}
