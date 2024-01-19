//using GuessBender_2024.Application.Features.Mediator.Commands.MatchCommands;
using GuessBender_2024.Application.Features.Mediator.Commands.MatchCommands;
using GuessBender_2024.Application.Features.Mediator.Queries.MatchQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuessBender_2024.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly IMediator _mediator;


        public MatchesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllMatches()
        {
            return Ok(await _mediator.Send(new GetMatchQuery()));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetMatchWithTeamAndLeagueDetails")]
        public async Task<IActionResult> GetMatchWithTeamAndLeagueDetails()
        {
            return Ok(await _mediator.Send(new GetMatchWithTeamAndLeagueDetailsQuery()));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetMatchWithTeamAndLeagueDetailssByDate")]
        public async Task<IActionResult> GetMatchWithTeamAndLeagueDetailssByDate(DateTime date)
        {
            return Ok(await _mediator.Send(new GetMatchWithTeamAndLeagueDetailsByDateQuery(date)));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetMatchWithTeamAndLeagueDetailssByDatesBetween")]
        public async Task<IActionResult> GetMatchWithTeamAndLeagueDetailssByDatesBetween(DateTime date1,DateTime date2)
        {
            return Ok(await _mediator.Send(new GetMatchWithTeamAndLeagueDetailsByDatesBetweenQuery(date1,date2)));
        }
		[Authorize(Roles = "Admin, Comp")]
		[HttpGet("GetMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserId")]
		public async Task<IActionResult> GetMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserId(DateTime date1, string userId)
		{
			return Ok(await _mediator.Send(new GetMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserIdQuery(date1, userId)));
		}
		[Authorize(Roles = "Admin")]
        [HttpGet("GetMatchByIdWithTeamAndLeagueDetails")]
        public async Task<IActionResult> GetMatchByIdWithTeamAndLeagueDetails(int id)
        {
            return Ok(await _mediator.Send(new GetMatchByIdWithTeamAndLeagueDetailsQuery(id)));
        }
         [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllMatches(int id)
        {
            return Ok(await _mediator.Send(new GetMatchByIdQuery(id)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateMatch(CreateMatchCommand command)
        {
            await _mediator.Send(command);
            return Ok("Ülke başarıyla eklendi.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateMatch(UpdateMatchCommand command)
        {
            await _mediator.Send(command);
            return Ok("Ülke başarıyla güncellendi.");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            await _mediator.Send(new RemoveMatchCommand(id));
            return Ok("Ülke başarıyla slindi");

        }
    }
}