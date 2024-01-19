using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Dtos
{
	public class TokenResponseDto
	{
		public TokenResponseDto(string token, DateTime expireDate, string userId, string userName)
		{
			Token = token;
			ExpireDate = expireDate;
			UserId = userId;
			UserName = userName;
		}

		public string Token { get; set; }
		public string UserName { get; set; }
		public string UserId { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
