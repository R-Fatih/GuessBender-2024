using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Commands.CountryCommands
{
    public class CreateCountryCommand:IRequest
    {
        public string Name { get; set; }
        public string Confederation { get; set; }
        public string LogoUrl { get; set; }
    }
}
