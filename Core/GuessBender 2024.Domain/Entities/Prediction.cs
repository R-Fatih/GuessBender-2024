namespace GuessBender_2024.Domain.Entities
{
    public class Prediction
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
        public Match Match { get; set; }
        public User User { get; set; }
    }

}
