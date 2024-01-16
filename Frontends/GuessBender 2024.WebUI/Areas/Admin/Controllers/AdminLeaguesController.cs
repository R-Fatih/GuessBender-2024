using GuessBender_2024.Dtos.CountryDtos;
using GuessBender_2024.Dtos.LeagueDtos;
using GuessBender_2024.WebUI.Controllers;
using GuessBender_2024.WebUI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;

namespace GuessBender_2024.WebUI.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	[Route("Admin/AdminLeagues")]
	public class AdminLeaguesController : BaseController
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILogger<AdminLeaguesController> _logger;
		public AdminLeaguesController(IHttpClientFactory httpClientFactory, ILogger<AdminLeaguesController> logger)
		{
			_httpClientFactory = httpClientFactory;
			_logger = logger;
		}
		[Route("Index")]
		public async Task<IActionResult> Index()
		{
			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{


				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var responseMessage = await client.GetAsync("https://localhost:7185/api/Leagues/GetLeagueWithCountryDetails");
				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<List<ResultLeagueWithCountryDetailsDto>>(jsonData);
					_logger.LogInformation($"Leagues visited by {User.Identity?.Name}");

					return View(values);
				}
				_logger.LogError($"There is a problem on Leagues request");


			}
			return View();
		}

		public async Task<List<SelectListItem>> GetCountries()
		{
			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{


				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var responseMessage = await client.GetAsync("https://localhost:7185/api/Countries");
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
		[Route("CreateLeague")]
		public async Task<IActionResult> CreateLeague()
		{
			_logger.LogInformation($"Create new League visited by {User.Identity?.Name}");
			ViewBag.countries = await GetCountries();
			return View();
		}

		[HttpPost]
		[Route("CreateLeague")]
		public async Task<IActionResult> CreateLeague(CreateLeagueDto cre1)
		{
			//var d = cre2;

			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{
				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var jsonData = JsonConvert.SerializeObject(cre1);
				StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var responseMessage = await client.PostAsync("https://localhost:7185/api/Leagues", stringContent);
				if (responseMessage.IsSuccessStatusCode)
				{
					//GitHubFileUploader.CreateFile(cre1.LogoId, cre2.Name, "LeagueLogos");
					Alert("Takım başarıyla oluşturuldu", Enums.Enums.NotificationType.success);
					_logger.LogInformation($"League {cre1.Name} created by {User.Identity?.Name}");
					return RedirectToAction("Index", "AdminLeagues", new { area = "Admin" });
				}
				else
				{
					Alert("Bir hata oluştu", Enums.Enums.NotificationType.error);
					_logger.LogError($"There is a problem on Leagues creation.");

				}
			}
			ViewBag.countries = await GetCountries();

			return View();
		}
		[Route("RemoveLeague/{id}")]

		public async Task<IActionResult> RemoveLeague(int id)
		{
			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{
				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var responseMessage = await client.GetAsync($"https://localhost:7185/api/Leagues/Delete/{id}");
				if (responseMessage.IsSuccessStatusCode)
				{
					Alert("Takım başarıyla silindi", Enums.Enums.NotificationType.success);
					_logger.LogInformation($"League with {id} id removed by {User.Identity?.Name}");

					return RedirectToAction("Index", "AdminLeagues", new { area = "Admin" });

				}
				_logger.LogError($"There is a problem on Leagues deletion.");

			}

			return View();

		}

		[HttpGet]
		[Route("UpdateLeague/{id}")]
		public async Task<IActionResult> UpdateLeague(int id)
		{
			ViewBag.countries = await GetCountries();
			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{


				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var responseMessage = await client.GetAsync($"https://localhost:7185/api/Leagues/{id}");
				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<UpdateLeagueDto>(jsonData);
					_logger.LogInformation($"League {values.Name} update page visited by {User.Identity?.Name}");

					return View(values);
				}
				_logger.LogError($"There is a problem on League update request.");


			}

			return View();
		}
		[HttpPost]
		[Route("UpdateLeague/{id}")]
		public async Task<IActionResult> UpdateLeague(UpdateLeagueDto updateLeagueDto)
		{
			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{
				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var jsonData = JsonConvert.SerializeObject(updateLeagueDto);
				StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var responseMessage = await client.PostAsync("https://localhost:7185/api/Leagues/Update", stringContent);
				if (responseMessage.IsSuccessStatusCode)
				{
					Alert("Takım başarıyla güncellendi", Enums.Enums.NotificationType.success);
					_logger.LogInformation($"League {updateLeagueDto.Name} updated by {User.Identity?.Name}");

					return RedirectToAction("Index", "AdminLeagues", new { area = "Admin" });
				}
				_logger.LogError($"There is a problem on League updating.");

			}
			return View();
		}
		[HttpGet]
		[Route("CreateLeagueMultiple")]
		public async Task<IActionResult> CreateLeagueMultiple()
		{
			_logger.LogInformation($"Create new League multiple visited by {User.Identity?.Name}");
			ViewBag.countries = await GetCountries();
			return View();
		}
		[HttpPost]
		[Route("CreateLeagueMultiple")]
		public async Task<IActionResult> CreateLeagueMultiple(CreateLeagueDto createLeagueDto)
		{
			//var d = cre2;

			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{
				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				string[] lines = createLeagueDto.Name.Split('\n'); 
				for (int i=0;i< lines.Length;i++)
				{
					string[] strings = lines[i].Split(',');
					CreateLeagueDto createLeagueDto1 = new CreateLeagueDto {
						Name= strings[0],
						Abbr = strings[1],
						CountryId =Convert.ToInt32( strings[2]),
					};
				
					string logourl=strings[4];
					var jsonData = JsonConvert.SerializeObject(createLeagueDto1);
					StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
					var responseMessage = await client.PostAsync("https://localhost:7185/api/Leagues", stringContent);
					if (responseMessage.IsSuccessStatusCode)
					{
						//GitHubFileUploader.CreateFile(createLeagueDto1., logourl, "LeagueLogos");
						
					}
					else
					{
						Alert("Bir hata oluştu", Enums.Enums.NotificationType.error);
						_logger.LogError($"There is a problem on Leagues creation.");

					}
				}
				Alert("Takımlar başarıyla oluşturuldu", Enums.Enums.NotificationType.success);
				_logger.LogInformation($"Leagues created by {User.Identity?.Name}");
				return RedirectToAction("Index", "AdminLeagues", new { area = "Admin" });
			}
			ViewBag.countries = await GetCountries();

			return View();
		}
	}
}
