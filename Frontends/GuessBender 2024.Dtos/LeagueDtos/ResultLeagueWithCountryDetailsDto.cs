using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Dtos.LeagueDtos
{
	public class ResultLeagueWithCountryDetailsDto
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Abbr { get; set; }
        public string CountryName { get; set; }
        public string CountryLogoUrl { get; set; }

    }
}
