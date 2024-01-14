﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Commands.LeagueCommands
{
    public class CreateLeagueCommand:IRequest
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Abbr { get; set; }
    }
}
