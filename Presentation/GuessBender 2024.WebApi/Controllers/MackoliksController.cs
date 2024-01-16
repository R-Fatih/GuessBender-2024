using GuessBender_2024.Application.Features.Mediator.Commands.MatchCommands;
using GuessBender_2024.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GuessBender_2024.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]

    [Route("api/[controller]")]
	[ApiController]
	public class MackoliksController : ControllerBase
	{
		// GET: api/<MackoliksController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}
		[HttpGet("get")]
		public async Task<ActionResult<UpdateMatchCommand>> scrape(string adres)
		{
			List<string> list = new List<string>
			{
				"//*[@id=\"match-details\"]/div/div[1]/div/div[1]/div[2]",
					"//*[@id=\"dvScoreText\"]",
					"//*[@id=\"match-details\"]/div/div[1]/div/div[2]/div[1]/div[1]/a",
					"//*[@id=\"match-details\"]/div/div[1]/div/div[2]/div[3]/div[1]/a"
			};

			UpdateMatchCommand match = new UpdateMatchCommand();
			HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
			HtmlAgilityPack.HtmlDocument document = web.Load("https://arsiv.mackolik.com/Match/Default.aspx?id="+adres);
			var date = document.DocumentNode.SelectNodes(list[0]);
			var skor = document.DocumentNode.SelectNodes(list[1]);
			var h = document.DocumentNode.SelectNodes(list[2]);
			var a = document.DocumentNode.SelectNodes(list[3]);

			match.Date = Convert.ToDateTime(date[0].InnerText.Replace("Tarih : ", ""),new CultureInfo("tr"));
			//try
			//{


			//	match.HomeScore = Convert.ToInt32(skor[0].InnerText.Split('-')[0]);
			//	match.AwayScore = Convert.ToInt32(skor[0].InnerText.Split('-')[1]);
			//}
			//catch (Exception)
			//{

			//}
			try
			{

			
			match.HomeMS = Convert.ToInt32(skor[0].InnerText.Split('-')[0]);
			match.AwayMS = Convert.ToInt32(skor[0].InnerText.Split('-')[1]);
            }
            catch (Exception)
            {


            }
            return match;
			//for (int i = 0; i < takımlar.Count; i++)
			//{
			//    dataGridView1.Rows.Add(i+1,takımlar[i], puanlar[i]);
			//}
		}

		// GET api/<MackoliksController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<MackoliksController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<MackoliksController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<MackoliksController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
