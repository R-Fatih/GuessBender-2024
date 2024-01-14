using GuessBender_2024.Application.Features.Mediator.Commands.CountryCommands;
using GuessBender_2024.Application.Features.Mediator.Queries.CountryQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuessBender_2024.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Admin, Comp")]
        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            return Ok(await _mediator.Send(new GetCountryQuery()));
        }

        [Authorize(Roles = "Admin, Comp")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllCountries(int id)
        {
            return Ok(await _mediator.Send(new GetCountryByIdQuery(id)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCountry(CreateCountryCommand command)
        {
            await _mediator.Send(command);
            return Ok("Ülke başarıyla eklendi.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateCountry(UpdateCountryCommand command)
        {
            await _mediator.Send(command);
            return Ok("Ülke başarıyla güncellendi.");
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            await _mediator.Send(new RemoveCountryCommand(id));
            return Ok("Ülke başarıyla slindi");

        }
    }
}