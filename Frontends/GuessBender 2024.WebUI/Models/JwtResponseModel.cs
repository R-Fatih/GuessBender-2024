namespace GuessBender_2024.WebUI.Models
{
    public class JwtResponseModel
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
