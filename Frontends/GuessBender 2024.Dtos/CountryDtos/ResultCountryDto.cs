using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Dtos.CountryDtos
{
	public class ResultCountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Confederation { get; set; }
        public string LogoUrl { get; set; }
    }
}
