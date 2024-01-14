using GuessBender_2024.Application.Features.Mediator.Commands.LeagueCommands;
using GuessBender_2024.Application.Features.Mediator.Queries.LeagueQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuessBender_2024.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : ControllerBase
    {
        private readonly IMediator _mediator;


        public LeaguesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Admin, Comp")]
        [HttpGet]
        public async Task<IActionResult> GetAllLeagues()
        {
            return Ok(await _mediator.Send(new GetLeagueQuery()));
        }
        [Authorize(Roles = "Admin, Comp")]
        [HttpGet("GetLeagueWithCountryDetails")]
        public async Task<IActionResult> GetLeagueWithCountryDetails()
        {
            return Ok(await _mediator.Send(new GetLeagueWithCountryDetailsQuery()));
        }
        [Authorize(Roles = "Admin, Comp")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllLeagues(int id)
        {
            return Ok(await _mediator.Send(new GetLeagueByIdQuery(id)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateLeague(CreateLeagueCommand command)
        {
            await _mediator.Send(command);
            return Ok("Ülke başarıyla eklendi.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateLeague(UpdateLeagueCommand command)
        {
            await _mediator.Send(command);
            return Ok("Ülke başarıyla güncellendi.");
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> DeleteLeague(int id)
        {
            await _mediator.Send(new RemoveLeagueCommand(id));
            return Ok("Ülke başarıyla slindi");

        }
    }
}