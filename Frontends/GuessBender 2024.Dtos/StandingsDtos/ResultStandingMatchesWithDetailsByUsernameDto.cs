using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Dtos.StandingsDtos
{
    public class ResultStandingMatchesWithDetailsByUsernameDto
    {
        public string Username { get; set; }
        public string UserId { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public int HomeTeamLogoId { get; set; }
        public int AwayTeamLogoId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? PredictionTime { get; set; }
        public int? HomeMS { get; set; }
        public int? AwayMS { get; set; }
        public int? HomeScore { get; set; }
        public int? Awayscore { get; set; }
        public bool? Ms1 { get; set; }
        public bool? Msx { get; set; }
        public bool? Ms2 { get; set; }
        public int Total_Points { get; set; }
                                
    }
}
