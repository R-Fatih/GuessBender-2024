using GuessBender_2024.Dtos.CountryDtos;
using GuessBender_2024.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace GuessBender_2024.WebUI.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	[Route("Admin/AdminCountries")]
	public class AdminCountriesController : BaseController
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly ILogger<AdminCountriesController> _logger;
		public AdminCountriesController(IHttpClientFactory httpClientFactory, ILogger<AdminCountriesController> logger)
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
				var responseMessage = await client.GetAsync("https://localhost:7185/api/Countries");

				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<List<ResultCountryDto>>(jsonData);
					_logger.LogInformation($"Countries visited by {User.Identity?.Name}");
					return View(values);
				}
				_logger.LogError($"There is a problem on Country request");

			}
			return View();
		}


		[HttpGet]
		[Route("CreateCountry")]
		public async Task<IActionResult> CreateCountry()
		{
			_logger.LogInformation($"Create new country visited by {User.Identity?.Name}");

			return View();
		}
		
		[HttpPost]
        [Route("CreateCountry")]
        public async Task<IActionResult> CreateCountry(CreateCountryDto createCountryDto)
		{
			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{
				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var jsonData = JsonConvert.SerializeObject(createCountryDto);
				StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var responseMessage = await client.PostAsync("https://localhost:7185/api/Countries", stringContent);
				if (responseMessage.IsSuccessStatusCode)
				{
					Alert("Ülke başarıyla oluşturuldu", Enums.Enums.NotificationType.success);
					_logger.LogInformation($"Country {createCountryDto.Name} added by {User.Identity?.Name}");
					return RedirectToAction("Index", "AdminCountries", new { area = "Admin" });
				}
				else
				{
					Alert("Bir hata oluştu", Enums.Enums.NotificationType.error);
					_logger.LogError($"There is a problem while country adding");

				}
			}

			return View();
		}
        [Route("RemoveCountry/{id}")]

        public async Task<IActionResult> RemoveCountry(int id)
		{
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{
				var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync($"https://localhost:7185/api/Countries/Delete/{id}");
				if (responseMessage.IsSuccessStatusCode)
				{
					Alert("Ülke başarıyla silindi", Enums.Enums.NotificationType.success);
					_logger.LogInformation($"County removed by {User.Identity?.Name}");

					return RedirectToAction("Index", "AdminCountries", new { area = "Admin" });

				}
				_logger.LogError($"There is a problem while country removing");

			}

			return View();

		}

		[HttpGet]
		[Route("UpdateCountry/{id}")]
		public async Task<IActionResult> UpdateCountry(int id)
		{
			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{


				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var responseMessage = await client.GetAsync($"https://localhost:7185/api/Countries/{id}");
				if (responseMessage.IsSuccessStatusCode)
				{
					var jsonData = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<UpdateCountryDto>(jsonData);
					_logger.LogInformation($"Country {values.Name} update page visited by {User.Identity?.Name}");

					return View(values);
				}
				_logger.LogError($"There is a problem while country update page loading.");

			}

			return View();
		}
		[HttpPost]
		[Route("UpdateCountry/{id}")]
		public async Task<IActionResult> UpdateCountry(UpdateCountryDto updateCountryDto)
		{
			var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken").Value;
			if (token != null)
			{
				var client = _httpClientFactory.CreateClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var jsonData = JsonConvert.SerializeObject(updateCountryDto);
				StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var responseMessage = await client.PostAsync("https://localhost:7185/api/Countries/Update", stringContent);
				if (responseMessage.IsSuccessStatusCode)
				{
					Alert("Ülke başarıyla güncellendi", Enums.Enums.NotificationType.success);
					_logger.LogInformation($"Country {updateCountryDto.Name} updated by {User.Identity?.Name}");

					return RedirectToAction("Index", "AdminCountries", new { area = "Admin" });
				}
				_logger.LogError($"There is a problem while country updating.");

			}
			return View();
		}
	}
}
