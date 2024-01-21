using GuessBender_2024.Application.Features.Mediator.Queries.CountryQueries;
using GuessBender_2024.Application.Features.Mediator.Queries.StandingQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuessBender_2024.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StandingsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Admin, Comp")]
        [HttpGet]
        public async Task<IActionResult> GetAllStandings()
        {
            return Ok(await _mediator.Send(new GetStandingQuery()));
        }
        [Authorize(Roles = "Admin, Comp")]
        [HttpGet("GetStandingByCountryId")]
        public async Task<IActionResult> GetStandingByCountryId(int id)
        {
            return Ok(await _mediator.Send(new GetStandingByCountryIdQuery(id)));
        }
        [Authorize(Roles = "Admin, Comp")]
        [HttpGet("GetStandingMatchesByUsername")]
        public async Task<IActionResult> GetStandingMatchesByUsername(string username)
        {
            return Ok(await _mediator.Send(new GetStandingMatchesWithDetailsByUsernameQuery(username)));
        }
    }
}
