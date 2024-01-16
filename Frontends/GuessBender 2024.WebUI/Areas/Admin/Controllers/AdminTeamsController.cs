using GuessBender_2024.Dtos.CountryDtos;
using GuessBender_2024.Dtos.TeamDtos;
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
	[Route("Admin/AdminTeams")]
	public class AdminTeamsController : BaseController
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILogger<AdminTeamsController> _logger;
		public AdminTeamsController(IHttpClientFactory httpClientFactory, ILogger<AdminTeamsController> logger)
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
				var responseMessage = await client.GetAsync("https://localhost:7185/api/Teams/GetTeamWithCountryDetails");
				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<List<ResultTeamWithCountryDetailsDto>>(jsonData);
					_logger.LogInformation($"Teams visited by {User.Identity?.Name}");

					return View(values);
				}
				_logger.LogError($"There is a problem on Teams request");


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
		[Route("CreateTeam")]
		public async Task<IActionResult> CreateTeam()
		{
			_logger.LogInformation($"Create new team visited by {User.Identity?.Name}");
			var value = (new CreateTeamDto(), new CreateTeamDto());
			ViewBag.countries = await GetCountries();
			return View(value);
		}

		[HttpPost]
		[Route("CreateTeam")]
		public async Task<IActionResult> CreateTeam([Bind(Prefix = "Item1")] CreateTeamDto cre1, [Bind(Prefix = "Item2")] CreateTeamDto cre2)
		{
			//var d = cre2;

			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{
				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var jsonData = JsonConvert.SerializeObject(cre1);
				StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var responseMessage = await client.PostAsync("https://localhost:7185/api/Teams", stringContent);
				if (responseMessage.IsSuccessStatusCode)
				{
					GitHubFileUploader.CreateFile(cre1.LogoId, cre2.Name, "TeamLogos");
					Alert("Takım başarıyla oluşturuldu", Enums.Enums.NotificationType.success);
					_logger.LogInformation($"Team {cre1.Name} created by {User.Identity?.Name}");
					return RedirectToAction("Index", "AdminTeams", new { area = "Admin" });
				}
				else
				{
					Alert("Bir hata oluştu", Enums.Enums.NotificationType.error);
					_logger.LogError($"There is a problem on Teams creation.");

				}
			}
			ViewBag.countries = await GetCountries();

			return View();
		}
		[Route("RemoveTeam/{id}")]

		public async Task<IActionResult> RemoveTeam(int id)
		{
			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{
				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var responseMessage = await client.GetAsync($"https://localhost:7185/api/Teams/Delete/{id}");
				if (responseMessage.IsSuccessStatusCode)
				{
					Alert("Takım başarıyla silindi", Enums.Enums.NotificationType.success);
					_logger.LogInformation($"Team with {id} id removed by {User.Identity?.Name}");

					return RedirectToAction("Index", "AdminTeams", new { area = "Admin" });

				}
				_logger.LogError($"There is a problem on Teams deletion.");

			}

			return View();

		}

		[HttpGet]
		[Route("UpdateTeam/{id}")]
		public async Task<IActionResult> UpdateTeam(int id)
		{
			ViewBag.countries = await GetCountries();
			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{


				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var responseMessage = await client.GetAsync($"https://localhost:7185/api/Teams/{id}");
				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<UpdateTeamDto>(jsonData);
					_logger.LogInformation($"Team {values.Name} update page visited by {User.Identity?.Name}");

					return View(values);
				}
				_logger.LogError($"There is a problem on team update request.");


			}

			return View();
		}
		[HttpPost]
		[Route("UpdateTeam/{id}")]
		public async Task<IActionResult> UpdateTeam(UpdateTeamDto updateTeamDto)
		{
			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{
				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var jsonData = JsonConvert.SerializeObject(updateTeamDto);
				StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var responseMessage = await client.PostAsync("https://localhost:7185/api/Teams/Update", stringContent);
				if (responseMessage.IsSuccessStatusCode)
				{
					Alert("Takım başarıyla güncellendi", Enums.Enums.NotificationType.success);
					_logger.LogInformation($"Team {updateTeamDto.Name} updated by {User.Identity?.Name}");

					return RedirectToAction("Index", "AdminTeams", new { area = "Admin" });
				}
				_logger.LogError($"There is a problem on team updating.");

			}
			return View();
		}
		[HttpGet]
		[Route("CreateTeamMultiple")]
		public async Task<IActionResult> CreateTeamMultiple()
		{
			_logger.LogInformation($"Create new team multiple visited by {User.Identity?.Name}");
			ViewBag.countries = await GetCountries();
			return View();
		}
		[HttpPost]
		[Route("CreateTeamMultiple")]
		public async Task<IActionResult> CreateTeamMultiple(CreateTeamDto createTeamDto)
		{
			//var d = cre2;

			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{
				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				string[] lines = createTeamDto.Name.Split('\n'); 
				for (int i=0;i< lines.Length;i++)
				{
					string[] strings = lines[i].Split(',');
					CreateTeamDto createTeamDto1 = new CreateTeamDto {
						Name= strings[0],
						Abbr = strings[1],
						CountryId =Convert.ToInt32( strings[2]),
						LogoId = Convert.ToInt32(strings[3])
					};
				
					string logourl=strings[4];
					var jsonData = JsonConvert.SerializeObject(createTeamDto1);
					StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
					var responseMessage = await client.PostAsync("https://localhost:7185/api/Teams", stringContent);
					if (responseMessage.IsSuccessStatusCode)
					{
						GitHubFileUploader.CreateFile(createTeamDto1.LogoId, logourl, "TeamLogos");
						
					}
					else
					{
						Alert("Bir hata oluştu", Enums.Enums.NotificationType.error);
						_logger.LogError($"There is a problem on Teams creation.");

					}
				}
				Alert("Takımlar başarıyla oluşturuldu", Enums.Enums.NotificationType.success);
				_logger.LogInformation($"Teams created by {User.Identity?.Name}");
				return RedirectToAction("Index", "AdminTeams", new { area = "Admin" });
			}
			ViewBag.countries = await GetCountries();

			return View();
		}
	}
}
