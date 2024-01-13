using Microsoft.AspNetCore.Identity;
using GuessBender_2024.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Domain.Entities
{
    public class LeagueData
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CountryId { get; set; }
        [NotMapped]
        public ApplicationUser? User { get; set; }
        public Country? Country { get; set; }
    }
}
