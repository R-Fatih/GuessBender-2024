using GuessBender_2024.Application.Features.Mediator.Queries.MatchQueries;
using GuessBender_2024.Application.Features.Mediator.Results.MatchResults;
using GuessBender_2024.Application.Interfaces;
using GuessBender_2024.Application.Tools;
using GuessBender_2024.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Handlers.MatchHandlers
{
    public class GetMatchByIdQueryHandler : IRequestHandler<GetMatchByIdQuery, GetMatchByIdQueryResult>
    {
        private readonly IRepository<Match> _repository;

        public GetMatchByIdQueryHandler(IRepository<Match> repostitory)
        {
            _repository = repostitory;
        }

        public async Task<GetMatchByIdQueryResult> Handle(GetMatchByIdQuery request, CancellationToken cancellationToken)
        {
            HttpClient httpClient = new HttpClient();
            Time time = httpClient.GetFromJsonAsync<Time>("https://www.timeapi.io/api/Time/current/zone?timeZone=Europe/Istanbul").Result;
            var values=await _repository.GetByIdAsync(request.Id);
            return new GetMatchByIdQueryResult
            {
                AwayMS = values.AwayMS,
                AwayTeamId = values.AwayTeamId,
                Code = values.Code,
                Date = values.Date,
                Expired = time.datetime > values.Date,
                HomeMS = values.HomeMS,
                HomeTeamId = values.HomeTeamId,
                Id = values.Id,
                LeagueId = values.LeagueId

            };
        }
    }
}
