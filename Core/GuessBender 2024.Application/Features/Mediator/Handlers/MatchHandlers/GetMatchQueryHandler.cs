using GuessBender_2024.Application.Features.Mediator.Queries.MatchQueries;
using GuessBender_2024.Application.Features.Mediator.Results.MatchResults;
using GuessBender_2024.Application.Interfaces;
using GuessBender_2024.Application.Tools;
using GuessBender_2024.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Features.Mediator.Handlers.MatchHandlers
{
    public class GetMatchQueryHandler : IRequestHandler<GetMatchQuery, List<GetMatchQueryResult>>
    {
        private readonly IRepository<Match> _repository;

        public GetMatchQueryHandler(IRepository<Match> repostitory)
        {
            _repository = repostitory;
        }

        public async Task<List<GetMatchQueryResult>> Handle(GetMatchQuery request, CancellationToken cancellationToken)
        {
            HttpClient httpClient = new HttpClient();
            Time time = httpClient.GetFromJsonAsync<Time>("https://www.timeapi.io/api/Time/current/zone?timeZone=Europe/Istanbul").Result;
            var values = await _repository.GetAllAsync();
            return values.Select(x=>new GetMatchQueryResult
            {
                AwayMS=x.AwayMS,
                AwayTeamId=x.AwayTeamId,
                Code=x.Code,
                Date=x.Date,
                Expired = time.datetime > x.Date,
                HomeMS=x.HomeMS,
                HomeTeamId=x.HomeTeamId,
                Id=x.Id,
                LeagueId=x.LeagueId,
            }).ToList();
        }
    }
}
