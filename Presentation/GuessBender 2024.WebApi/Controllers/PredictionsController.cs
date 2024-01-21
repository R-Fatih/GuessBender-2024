using GuessBender_2024.Application.Features.Mediator.Commands.MatchCommands;
using GuessBender_2024.Application.Features.Mediator.Commands.PredictionCommands;
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetPredictions()
        {
            return Ok(await _mediator.Send(new GetPredictionQuery()));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetPredictionById")]
        public async Task<IActionResult> GetPredictionById(int id)
        {
            return Ok(await _mediator.Send(new GetPredictionByIdQuery(id)));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetPredictionByDate")]
        public async Task<IActionResult> GetPredictionByDate(DateTime date)
        {
            return Ok(await _mediator.Send(new GetPredictionByDateQuery(date)));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetPredictionCountByMatchId")]
        public async Task<IActionResult> GetPredictionCountByMatchId(int matchId)
        {
            return Ok(await _mediator.Send(new GetPredictionCountByMatchIdQuery(matchId)));
        }
		[Authorize(Roles = "Admin, Comp")]
		[HttpPost]
		public async Task<IActionResult> CreatePrediction(CreatePredictionCommand command)
		{
			await _mediator.Send(command);
			return Ok("Tahmin başarıyla eklendi.");
		}

		[Authorize(Roles = "Admin, Comp")]
		[HttpPost("Update")]
		public async Task<IActionResult> UpdatePrediction(UpdatePredictionCommand command)
		{
			await _mediator.Send(command);
			return Ok("Tahmin başarıyla güncellendi.");
		}
		[Authorize(Roles = "Admin")]
		[HttpGet("Delete/{id}")]
		public async Task<IActionResult> DeletePrediction(int id)
		{
			await _mediator.Send(new RemovePredictionCommand(id));
			return Ok("Tahmin başarıyla slindi");

		}
	}
}
