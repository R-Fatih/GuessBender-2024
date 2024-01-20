using GuessBender_2024.Dtos.CountryDtos;
using GuessBender_2024.Dtos.MatchDtos;
using GuessBender_2024.WebUI.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GuessBender_2024.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DefaultController : Controller
	{
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<DefaultController> _logger;
        public DefaultController(IHttpClientFactory httpClientFactory, ILogger<DefaultController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        [Route("Index")]
        [Route("Index/{dt}")]
        public async Task<IActionResult> Index(DateTime dt)
        {
            ViewBag.date = dt.Year + "-" + dt.ToString("MM") + "-" + dt.ToString("dd");

            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
            if (token != null)
            {


                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync($"https://localhost:7185/api/Matches/GetMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserId?date1={dt.Year + "-" + dt.ToString("MM") + "-" + dt.ToString("dd")}&userId={User.Claims.FirstOrDefault(x => x.Type == "userId").Value}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserIdDto>>(jsonData);
                    _logger.LogInformation($"Countries visited by {User.Identity?.Name}");
                    return View(values);
                }
                _logger.LogError($"There is a problem on Country request");

            }
            return View();
        }

    }
}
