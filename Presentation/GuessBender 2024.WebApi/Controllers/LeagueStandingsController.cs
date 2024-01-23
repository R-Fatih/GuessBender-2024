using GuessBender_2024.Application.Features.Mediator.Queries.LeagueStandingQueries;
using GuessBender_2024.Application.Features.Mediator.Queries.StandingQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuessBender_2024.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeagueStandingsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public LeagueStandingsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Admin, Comp")]
        [HttpGet]
        public async Task<IActionResult> GetLeagueStandingByLeagueId(int leagueId)
        {
            return Ok(await _mediator.Send(new GetLeagueStandingByLeagueIdQuery(leagueId)));
        }
    }
}
