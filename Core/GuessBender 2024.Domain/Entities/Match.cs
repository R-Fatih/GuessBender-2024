using System.ComponentModel.DataAnnotations.Schema;

namespace GuessBender_2024.Domain.Entities
{
    public class Match
	{
		public int Id { get; set; }
		public int HomeTeamId { get; set; }
		public int AwayTeamId { get; set; }
		public int LeagueId { get; set; }
		public DateTime Date { get; set; }
		public int? HomeMS { get; set; }
		public int? AwayMS { get; set; }
        public List<Prediction>? Prediction { get; set; }
		public Team? HomeTeam { get; set; }
		public Team? AwayTeam { get; set; }
		public League? League { get; set; }
        public  bool? Expired { get; set; }
        public long Code { get; set; }
    }

}
