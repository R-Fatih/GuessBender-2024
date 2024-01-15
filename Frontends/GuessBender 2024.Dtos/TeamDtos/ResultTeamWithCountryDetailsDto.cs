﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Dtos.TeamDtos
{
    public class ResultTeamWithCountryDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbr { get; set; }
        public int CountryId { get; set; }
        public int LogoId { get; set; }
        public string CountryName { get; set; }
        public string CountryLogoUrl { get; set; }
    }
}
