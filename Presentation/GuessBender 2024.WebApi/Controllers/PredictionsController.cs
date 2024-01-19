using GuessBender_2024.Application.Features.Mediator.Queries.PredictionQueries;
using GuessBender_2024.Application.Features.Mediator.Results.PredictionResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuessBender_2024.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PredictionsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Admin, Comp")]
        [HttpGet("GetPredictionByUserId")]
        public async Task<IActionResult> GetPredictionByUserId(string userId)
        {
            return Ok(await _mediator.Send(new GetPredictionByUserIdQuery(userId)));
        }

        [Authorize(Roles = "Admin, Comp")]
        [HttpGet]
        public async Task<IActionResult> GetPredictions()
        {
            return Ok(await _mediator.Send(new GetPredictionQuery()));
        }

        [Authorize(Roles = "Admin, Comp")]
        [HttpGet("GetPredictionById")]
        public async Task<IActionResult> GetPredictionById(int id)
        {
            return Ok(await _mediator.Send(new GetPredictionByIdQuery(id)));
        }
        [Authorize(Roles = "Admin, Comp")]
        [HttpGet("GetPredictionByDate")]
        public async Task<IActionResult> GetPredictionByDate(DateTime date)
        {
            return Ok(await _mediator.Send(new GetPredictionByDateQuery(date)));
        }
        [Authorize(Roles = "Admin, Comp")]
        [HttpGet("GetPredictionCountByMatchId")]
        public async Task<IActionResult> GetPredictionCountByMatchId(int matchId)
        {
            return Ok(await _mediator.Send(new GetPredictionCountByMatchIdQuery(matchId)));
        }
    }
}
