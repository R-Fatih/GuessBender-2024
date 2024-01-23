using GuessBender_2024.Application.Interfaces.LeagueStandingInterfaces;
using GuessBender_2024.Domain;
using GuessBender_2024.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Persistance.Repositories.LeagueStandingRepositories
{
    public class LeagueStandingRepository : ILeagueStandingRepository
    {
        private readonly GuessBenderContext _context;

        public LeagueStandingRepository(GuessBenderContext context)
        {
            _context = context;
        }

        public List<LeagueStanding> GeteagueStandingByLeagueId(int leagueId)
        {
            return _context.Database.SqlQueryRaw<LeagueStanding>($"SELECT   Team.Name,LeagueId,Team.LogoId,   SUM(CASE WHEN HomeMS > AwayMS THEN 3 WHEN HomeMS = AwayMS THEN 1 ELSE 0 END) AS Points,   SUM(CASE WHEN HomeMS > AwayMS THEN 1 ELSE 0 END) AS Wins,   SUM(CASE WHEN HomeMS = AwayMS THEN 1 ELSE 0 END) AS Draws,   SUM(CASE WHEN HomeMS < AwayMS THEN 1 ELSE 0 END) AS Loses,   SUM(HomeMS) AS GoalsFor,   SUM(AwayMS) AS GoalsAgainst,   (SUM(HomeMS) -SUM(AwayMS)) AS Average FROM (   SELECT HomeTeamId, HomeMS, AwayMS,LeagueId,t1.LogoId FROM match inner join Team t1 on t1.Id=match.HomeTeamId  UNION ALL   SELECT AwayTeamId, AwayMS, HomeMS,LeagueId,t2.LogoId FROM match inner join Team t2 on t2.Id=match.AwayTeamId) AS combined_matches inner join Team on Team.Id=combined_matches.HomeTeamId  where LeagueId={leagueId}  group by Team.Name,LeagueId,Team.LogoId ORDER BY Points DESC, Average DESC,GoalsFor DESC,GoalsAgainst ASC ;").ToList();
        }
    }
}
