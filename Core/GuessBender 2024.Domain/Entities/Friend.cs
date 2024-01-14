using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Domain.Entities

{
    public class Friend
	{
        public int Id { get; set; }
        public string User1Id { get; set; }
        public string User2Id { get; set; }
        [NotMapped]
		public User? User1 { get; set; }
		[NotMapped]
		public User? User2 { get; set; }
	}
}
