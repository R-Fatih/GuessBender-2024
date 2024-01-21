using GuessBender_2024.Dtos.CountryDtos;
using GuessBender_2024.Dtos.MatchDtos;
using GuessBender_2024.Dtos.PredictionDtos;
using GuessBender_2024.Dtos.TeamDtos;
using GuessBender_2024.WebUI.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace GuessBender_2024.WebUI.Controllers
{
	[Authorize(Roles = "Admin, Comp")]
	public class DefaultController : BaseController
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILogger<DefaultController> _logger;
		public DefaultController(IHttpClientFactory httpClientFactory, ILogger<DefaultController> logger)
		{
			_httpClientFactory = httpClientFactory;
			_logger = logger;
		}
		[HttpGet]
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
					List<CreatePredictionDto> predictions = new List<CreatePredictionDto>();

					foreach (var item in values)
					{
						predictions.Add(new CreatePredictionDto
						{
							UserId = User.Claims.FirstOrDefault(x => x.Type == "userId").Value,
							MatchId = item.Id
						});
					}
					var valuess = (values, predictions);

					return View(valuess);
				}
				_logger.LogError($"There is a problem on Country request");

			}
			return View();
		}

		[HttpPost]
		[Route("Index")]
		[Route("Index/{dt}")]
		public async Task<IActionResult> Index([Bind(Prefix = "Item1")] List<ResultMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserIdDto> res1, [Bind(Prefix = "Item2")] List<CreatePredictionDto> createPredictionDtos)
		{
			var client1 = _httpClientFactory.CreateClient();
			Time time = client1.GetFromJsonAsync<Time>("https://www.timeapi.io/api/Time/current/zone?timeZone=Europe/Istanbul").Result;
			int count = 0;
			DateTime dt = DateTime.UtcNow.AddHours(3);
            var allmatches = await GetMatches(dt);
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{
				foreach (var item in createPredictionDtos)
				{
					if ((res1[count].HomeScorePrediction != null || res1[count].AwayScorePrediction != null || item.MSX != null || item.MS1 != null || item.MS2 != null))
					{
						if (!(time.datetime > res1[count].Date))
						{
							item.UserId = User.Claims.FirstOrDefault(x => x.Type == "userId").Value;
							item.MatchId =allmatches[count].Id;
							
                            item.HomeScore = res1[count].HomeScorePrediction;
							item.AwayScore = res1[count].AwayScorePrediction;
							
							item.PredictionTime = dt;
							if (allmatches[count].PredictionId == null)
							{

								var client = _httpClientFactory.CreateClient();
								client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
								var jsonData = JsonConvert.SerializeObject(item);
								StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
								var responseMessage = await client.PostAsync("https://localhost:7185/api/Predictions", stringContent);
								if (responseMessage.IsSuccessStatusCode)
								{
									//GitHubFileUploader.CreateFile(cre1.LogoId, cre2.Name, "LeagueLogos");
									_logger.LogInformation($"Prediction for {item.MatchId} created by {item.UserId} at {dt.ToString()}");

								}
								else
								{
									Alert("Bir hata oluştu", Enums.Enums.NotificationType.error);
									_logger.LogError($"Error on prediction for {item.MatchId} by {item.UserId} at {dt.ToString()}");

								}

							}
							else
							{
								var client = _httpClientFactory.CreateClient();
								client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
								var jsonData = JsonConvert.SerializeObject(new UpdatePredictionDto
								{
									AwayScore = item.AwayScore,
									HomeScore = item.HomeScore,
									Id = allmatches[count].PredictionId.Value,
									MatchId = item.MatchId,
									MS1 = item.MS1,
									MS2 = item.MS2,
									MSX = item.MSX,
									PredictionTime = item.PredictionTime,
									UserId = item.UserId,
								});
								StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
								var responseMessage = await client.PostAsync("https://localhost:7185/api/Predictions/Update", stringContent);
								if (responseMessage.IsSuccessStatusCode)
								{
									//GitHubFileUploader.CreateFile(cre1.LogoId, cre2.Name, "LeagueLogos");
									_logger.LogInformation($"Prediction {res1[count].PredictionId} for {item.MatchId} updated by {item.UserId} at {dt.ToString()}");

								}
								else
								{
									Alert("Bir hata oluştu", Enums.Enums.NotificationType.error);
									_logger.LogError($"Error update on {res1[count].PredictionId} prediction for {item.MatchId} by {item.UserId} at {dt.ToString()}");

								}
							}

						}
					}
					count++;
				}
				Alert("Maçlar başarıyla kaydedildi.", Enums.Enums.NotificationType.success);
				return RedirectToAction("Index", "Default");
			}

			return View();
		}


		public async Task<List<ResultMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserIdDto>> GetMatches(DateTime dt)
		{

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
					return values;
				}
				
			}
			return null;
		}
	}

}
