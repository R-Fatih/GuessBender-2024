using GuessBender_2024.Dtos.CountryDtos;
using GuessBender_2024.Dtos.TeamDtos;
using GuessBender_2024.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace GuessBender_2024.WebUI.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	[Route("Admin/AdminTeams")]
	public class AdminTeamsController : BaseController
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public AdminTeamsController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
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
					return View(values);
				}

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
            ViewBag.countries = await GetCountries();
			return View();
		}
		
		[HttpPost]
        [Route("CreateTeam")]
        public async Task<IActionResult> CreateTeam(CreateTeamDto createTeamDto)
		{
			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{
				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var jsonData = JsonConvert.SerializeObject(createTeamDto);
				StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var responseMessage = await client.PostAsync("https://localhost:7185/api/Teams", stringContent);
				if (responseMessage.IsSuccessStatusCode)
				{
					Alert("Takım başarıyla oluşturuldu", Enums.Enums.NotificationType.success);
					return RedirectToAction("Index", "AdminTeams", new { area = "Admin" });
				}
				else
				{
					Alert("Bir hata oluştu", Enums.Enums.NotificationType.error);

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

					return RedirectToAction("Index", "AdminTeams", new { area = "Admin" });

				}
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
					return View(values);
				}

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
					return RedirectToAction("Index", "AdminTeams", new { area = "Admin" });
				}
			}
			return View();
		}
	}
}
