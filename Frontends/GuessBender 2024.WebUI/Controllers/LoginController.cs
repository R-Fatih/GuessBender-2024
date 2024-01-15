using GuessBender_2024.Dtos.LoginDtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using GuessBender_2024.WebUI.Models;

namespace GuessBender_2024.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(ResultLoginDto resultLoginDto)
        {
            var client = _httpClientFactory.CreateClient();
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(resultLoginDto), Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7185/api/Authenticate", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var tokenModel = System.Text.Json.JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                if (tokenModel != null)
                {
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(tokenModel.Token);
                    var claims = token.Claims.ToList();

                    if (tokenModel.Token != null)
                    {
                        claims.Add(new Claim("accessToken", tokenModel.Token));
                        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                        var autProps = new AuthenticationProperties
                        {
                            ExpiresUtc = tokenModel.ExpireDate,
                            IsPersistent = true
                        };
                        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), autProps);
                        return RedirectToAction("Index", "Default");
                    }
                }
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}
