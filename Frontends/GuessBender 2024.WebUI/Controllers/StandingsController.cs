using GuessBender_2024.Dtos.MatchDtos;
using GuessBender_2024.Dtos.PredictionDtos;
using GuessBender_2024.Dtos.StandingsDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GuessBender_2024.WebUI.Controllers
{
    [Authorize(Roles = "Admin, Comp")]

    public class StandingsController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<StandingsController> _logger;
        public StandingsController(IHttpClientFactory httpClientFactory, ILogger<StandingsController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
            if (token != null)
            {


                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync($"https://localhost:7185/api/Standings");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultStandingDto>>(jsonData);
                    _logger.LogInformation($"Standings visited by {User.Identity?.Name}");



                    return View(values);
                }
                _logger.LogError($"There is a problem on Country request");

            }
            return View();
        }

        [HttpPost]
        public Task<IActionResult> Index(string username)
        {
            TempData["username"] = username;
            var a = "";

            return Task.FromResult<IActionResult>(RedirectToAction("MatchDetails", "Standings"));
        }

        [HttpGet]
        public async Task<IActionResult> MatchDetails()
        {
            var username = TempData["username"];
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
            if (token != null)
            {


                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync($"https://localhost:7185/api/Standings/GetStandingMatchesByUsername?username="+username);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultStandingMatchesWithDetailsByUsernameDto>>(jsonData);
                    _logger.LogInformation($"Match details visited by {User.Identity?.Name}");



                    return View(values);
                }
                _logger.LogError($"There is a problem on Match details request");

            }
            return View();
        }
    }
}
