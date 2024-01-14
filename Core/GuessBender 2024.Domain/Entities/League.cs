namespace GuessBender_2024.Domain.Entities
{
	public class League
	{
		public int Id { get; set; }
		public int CountryId { get; set; }
		public string Name { get; set; }
		public string Abbr { get; set; }
        public Country Country { get; set; }
    }

}
