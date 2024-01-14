namespace GuessBender_2024.Domain.Entities
{
	public class Country
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Confederation { get; set; }
		public string LogoUrl { get; set; }
		List<League> Leagues { get; set; }
		List<Team> Teams { get; set; }
	}

}
