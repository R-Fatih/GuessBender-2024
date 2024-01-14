using System.ComponentModel.DataAnnotations;

namespace GuessBender_2024.Domain.Entities
{
    public class Team
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Abbr { get; set; }
		public int CountryId { get; set; }
		public int LogoId { get; set; }
        public Country Country { get; set; }
    }

}
