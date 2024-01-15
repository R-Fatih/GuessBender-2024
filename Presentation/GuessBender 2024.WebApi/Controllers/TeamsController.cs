using GuessBender_2024.Application.Features.Mediator.Commands.TeamCommands;
using GuessBender_2024.Application.Features.Mediator.Queries.TeamQueries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuessBender_2024.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly IMediator _mediator;


        public TeamsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Admin, Comp")]
        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            return Ok(await _mediator.Send(new GetTeamQuery()));
        }
        [Authorize(Roles = "Admin, Comp")]
        [HttpGet("GetTeamWithCountryDetails")]
        public async Task<IActionResult> GetTeamWithCountryDetails()
        {
            return Ok(await _mediator.Send(new GetTeamWithCountryDetailsQuery()));
        }
        [Authorize(Roles = "Admin, Comp")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllTeams(int id)
        {
            return Ok(await _mediator.Send(new GetTeamByIdQuery(id)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateTeam(CreateTeamCommand command)
        {
            await _mediator.Send(command);
            return Ok("Ülke başarıyla eklendi.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateTeam(UpdateTeamCommand command)
        {
            await _mediator.Send(command);
            return Ok("Ülke başarıyla güncellendi.");
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            await _mediator.Send(new RemoveTeamCommand(id));
            return Ok("Ülke başarıyla slindi");

        }
    }
}