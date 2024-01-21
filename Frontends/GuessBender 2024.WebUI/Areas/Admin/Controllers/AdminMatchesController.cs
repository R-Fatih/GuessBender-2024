using GuessBender_2024.Dtos.CountryDtos;
using GuessBender_2024.Dtos.MatchDtos;
using GuessBender_2024.Dtos.PredictionDtos;
using GuessBender_2024.Dtos.TeamDtos;
using GuessBender_2024.WebUI.Controllers;
using GuessBender_2024.WebUI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.Globalization;
using System.IO;
using System.Text;

namespace GuessBender_2024.WebUI.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	[Route("Admin/AdminMatches")]
	public class AdminMatchesController : BaseController
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILogger<AdminMatchesController> _logger;
		public AdminMatchesController(IHttpClientFactory httpClientFactory, ILogger<AdminMatchesController> logger)
		{
			_httpClientFactory = httpClientFactory;
			_logger = logger;
		}
		[Route("Index")]
		[Route("Index/{dt}")]
		public async Task<IActionResult> Index(DateTime dt)
		{
			ViewBag.date = dt.Year + "-" + dt.ToString("MM") + "-" + dt.ToString("dd");
			List<ResultPredictionCountByMatchIdDto> listof = new List<ResultPredictionCountByMatchIdDto>();

			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{

				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var responseMessage = await client.GetAsync("https://localhost:7185/api/Matches/GetMatchWithTeamAndLeagueDetailssByDate?date=" + dt.ToString("d", new CultureInfo("en-US")));
				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<List<ResultMatchWithTeamAndLeagueDetailsDto>>(jsonData);
					foreach (var item in values)
					{
						listof.Add(await GetPredictions(item.Id));
					}
					_logger.LogInformation($"Matches visited by {User.Identity?.Name}");
					var value = (values, listof);
					return View(value);
				}
				_logger.LogError($"There is a problem on Matches request");


			}
			return View();
		}

		public async Task<List<SelectListItem>> GetLeagues()
		{
			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{


				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var responseMessage = await client.GetAsync("https://localhost:7185/api/Leagues");
				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<List<ResultCountryDto>>(jsonData);
					List<SelectListItem> countryValues = (from x in values
														  select new SelectListItem
														  {
															  Text = x.Name,
															  Value = x.Id.ToString()
														  }).ToList();
					return countryValues;
				}


			}
			return null;
		}

		[HttpGet]
		[Route("CreateMatch")]
		public async Task<IActionResult> CreateMatch()
		{
			_logger.LogInformation($"Create new Match visited by {User.Identity?.Name}");
			ViewBag.leagues = await GetLeagues();
			return View();
		}

		[HttpPost]
		[Route("CreateMatch")]
		public async Task<IActionResult> CreateMatch(CreateMatchDto cre1)
		{
			//var d = cre2;

			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{
				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var jsonData = JsonConvert.SerializeObject(cre1);
				StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var responseMessage = await client.PostAsync("https://localhost:7185/api/Matches", stringContent);
				if (responseMessage.IsSuccessStatusCode)
				{
					//GitHubFileUploader.CreateFile(cre1.LogoId, cre2.Name, "MatchLogos");
					Alert("Maç başarıyla oluşturuldu", Enums.Enums.NotificationType.success);
					_logger.LogInformation($"Match {cre1.HomeTeamId - cre1.AwayTeamId} created by {User.Identity?.Name}");
					return RedirectToAction("Index", "AdminMatches", new { area = "Admin" });
				}
				else
				{
					Alert("Bir hata oluştu", Enums.Enums.NotificationType.error);
					_logger.LogError($"There is a problem on Matches creation.");

				}
			}
			ViewBag.leagues = await GetLeagues();

			return View();
		}
		[Route("RemoveMatch/{id}")]

		public async Task<IActionResult> RemoveMatch(int id)
		{
			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{
				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var responseMessage = await client.GetAsync($"https://localhost:7185/api/Matches/Delete/{id}");
				if (responseMessage.IsSuccessStatusCode)
				{
					Alert("Maç başarıyla silindi", Enums.Enums.NotificationType.success);
					_logger.LogInformation($"Match with {id} id removed by {User.Identity?.Name}");

					return RedirectToAction("Index", "AdminMatches", new { area = "Admin" });

				}
				_logger.LogError($"There is a problem on Matches deletion.");

			}

			return View();

		}

		[HttpGet]
		[Route("UpdateMatch/{id}")]
		public async Task<IActionResult> UpdateMatch(int id)
		{
			ViewBag.leagues = await GetLeagues();
			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{


				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var responseMessage = await client.GetAsync($"https://localhost:7185/api/Matches/{id}");
				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<UpdateMatchDto>(jsonData);
					_logger.LogInformation($"Match {values.Id} update page visited by {User.Identity?.Name}");

					return View(values);
				}
				_logger.LogError($"There is a problem on Match update request.");


			}

			return View();
		}
		[HttpPost]
		[Route("UpdateMatch/{id}")]
		public async Task<IActionResult> UpdateMatch(UpdateMatchDto updateMatchDto)
		{
			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{
				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var jsonData = JsonConvert.SerializeObject(updateMatchDto);
				StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var responseMessage = await client.PostAsync("https://localhost:7185/api/Matches/Update", stringContent);
				if (responseMessage.IsSuccessStatusCode)
				{
					Alert("Maç başarıyla güncellendi", Enums.Enums.NotificationType.success);
					_logger.LogInformation($"Match {updateMatchDto.Id} updated by {User.Identity?.Name}");

					return RedirectToAction("Index", "AdminMatches", new { area = "Admin" });
				}
				_logger.LogError($"There is a problem on Match updating.");

			}
			return View();
		}
		[Route("UpdateMatches")]
		[Route("UpdateMatches/{dt}")]
		public async Task<IActionResult> UpdateMatches(DateTime dt)
		{

			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{


				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var responseMessage = await client.GetAsync("https://localhost:7185/api/Matches/GetMatchWithTeamAndLeagueDetailssByDate?date=" + dt.ToString("d", new CultureInfo("en-US")));
				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<List<ResultMatchWithTeamAndLeagueDetailsDto>>(jsonData);
					_logger.LogInformation($"Matches visited by {User.Identity?.Name}");

					foreach (var item in values)
					{
						var responseMessage2 = await client.GetAsync("https://localhost:7185/api/Mackoliks/get?adres=" + item.Code);
						var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
						var values2 = JsonConvert.DeserializeObject<UpdateMatchDto>(jsonData2);
					await	Update(new UpdateMatchDto
						{
							AwayTeamId = item.AwayTeamId,
							Code = item.Code,
							Expired = item.Expired,
							HomeTeamId = item.HomeTeamId,
							Id = item.Id,
							LeagueId = item.LeagueId,
							AwayMS = values2.AwayMS,
							HomeMS = values2.HomeMS,
							Date = values2.Date
						});

					}
					Alert("Başarıyla güncellendi", Enums.Enums.NotificationType.success);

				}
				_logger.LogError($"There is a problem on Matches request");


			}
			return RedirectToAction("Index", "AdminMatches", new { area = "Admin" });
		}

		[HttpGet]
		[Route("UpdateMatchesMultiple")]
		[Route("UpdateMatchesMultiple/{dt}/{dt2}")]
		public async Task<IActionResult> UpdateMatchesMultiple()
		{

			var values = (new DateTime(), new DateTime());

			return View(values);
		}
		[HttpPost]
		[Route("UpdateMatchesMultiple")]
		[Route("UpdateMatchesMultiple/{dt}/{dt2}")]
		public async Task<IActionResult> UpdateMatchesMultiple([Bind(Prefix ="Item1")] DateTime dt, [Bind(Prefix = "Item2")] DateTime dt2)
		{
			int countt = 0;
			int count = 0;
			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{


				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var responseMessage = await client.GetAsync("https://localhost:7185/api/Matches/GetMatchWithTeamAndLeagueDetailssByDatesBetween?date1=" + dt.ToString("d", new CultureInfo("en-US")) + "&date2=" + dt2.ToString("d", new CultureInfo("en-US")));
				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<List<ResultMatchWithTeamAndLeagueDetailsDto>>(jsonData);
					_logger.LogInformation($"Matches visited by {User.Identity?.Name}");

					foreach (var item in values)
					{
						try
						{

						var responseMessage2 = await client.GetAsync("https://localhost:7185/api/Mackoliks/get?adres=" + item.Code);
						var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
						var values2 = JsonConvert.DeserializeObject<UpdateMatchDto>(jsonData2);
						var a = await Update(new UpdateMatchDto
						{
							AwayTeamId = item.AwayTeamId,
							Code = item.Code,
							Expired = item.Expired,
							HomeTeamId = item.HomeTeamId,
							Id = item.Id,
							LeagueId = item.LeagueId,
							AwayMS = values2.AwayMS,
							HomeMS = values2.HomeMS,
							Date = values2.Date
						});
						if (a)
							count++;

						countt++;

						}
						catch (Exception ex)
						{
							_logger.LogError($"There is a problem on Matches request"+ex.Message+" "+ item.Code);


						}
					}
					Alert($"Toplam:{countt}, Başarılı: {count}", Enums.Enums.NotificationType.success);

				}
				_logger.LogError($"There is a problem on Matches request");


			}
			return RedirectToAction("Index","AdminMatches",new {area="Admin"});
		}
		public async Task<bool> Update(UpdateMatchDto updateMatchDto)
		{
			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{
				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var jsonData = JsonConvert.SerializeObject(updateMatchDto);
				StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var responseMessage = await client.PostAsync("https://localhost:7185/api/Matches/Update", stringContent);
				if (responseMessage.IsSuccessStatusCode)
				{
					return true;
				}
				return false;

			}
			return false;

		}


		public async Task<ResultPredictionCountByMatchIdDto> GetPredictions(int matchId)
		{


			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{


				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var responseMessage = await client.GetAsync("https://localhost:7185/api/Predictions/GetPredictionCountByMatchId?matchId=" + matchId);
				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<ResultPredictionCountByMatchIdDto>(jsonData);
					_logger.LogInformation($"Match prediction count getted. {User.Identity?.Name}");
					return values;
				}
				_logger.LogError($"There is a problem on Matches request");


			}
			return null;

		}
		//[HttpGet]
		//[Route("CreateMatchMultiple")]
		//public async Task<IActionResult> CreateMatchMultiple()
		//{
		//	_logger.LogInformation($"Create new Match multiple visited by {User.Identity?.Name}");
		//	ViewBag.countries = await GetCountries();
		//	return View();
		//}
		//[HttpPost]
		//[Route("CreateMatchMultiple")]
		//public async Task<IActionResult> CreateMatchMultiple(CreateMatchDto createMatchDto)
		//{
		//	//var d = cre2;

		//	var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
		//	if (token != null)
		//	{
		//		var client = _httpClientFactory.CreateClient();
		//		client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
		//		string[] lines = createMatchDto.Name.Split('\n'); 
		//		for (int i=0;i< lines.Length;i++)
		//		{
		//			string[] strings = lines[i].Split(',');
		//			CreateMatchDto createMatchDto1 = new CreateMatchDto {
		//				Name= strings[0],
		//				Abbr = strings[1],
		//				CountryId =Convert.ToInt32( strings[2]),
		//			};

		//			string logourl=strings[4];
		//			var jsonData = JsonConvert.SerializeObject(createMatchDto1);
		//			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
		//			var responseMessage = await client.PostAsync("https://localhost:7185/api/Matches", stringContent);
		//			if (responseMessage.IsSuccessStatusCode)
		//			{
		//				//GitHubFileUploader.CreateFile(createMatchDto1., logourl, "MatchLogos");

		//			}
		//			else
		//			{
		//				Alert("Bir hata oluştu", Enums.Enums.NotificationType.error);
		//				_logger.LogError($"There is a problem on Matches creation.");

		//			}
		//		}
		//		Alert("Maçlar başarıyla oluşturuldu", Enums.Enums.NotificationType.success);
		//		_logger.LogInformation($"Matches created by {User.Identity?.Name}");
		//		return RedirectToAction("Index", "AdminMatches", new { area = "Admin" });
		//	}
		//	ViewBag.countries = await GetCountries();

		//	return View();
		//}


	}
}
