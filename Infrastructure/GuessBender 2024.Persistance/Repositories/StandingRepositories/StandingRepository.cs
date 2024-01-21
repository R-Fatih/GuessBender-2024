using GuessBender_2024.Application.Interfaces.StandingInterfaces;
using GuessBender_2024.Domain.Entities;
using GuessBender_2024.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Persistance.Repositories.StandingRepositories
{
    public class StandingRepository:IStandingRepository
    {
        private readonly GuessBenderContext _context;

        public StandingRepository(GuessBenderContext context)
        {
            _context = context;
        }

        public List<Standing> GetAllStanding()
        {
            return _context.Database.SqlQueryRaw<Standing>("SELECT \r\n    Username,\r\n    SUM(\r\n\t\t\tCASE \r\n\t\t\tWHEN m.homems = p.HomeScore AND m.awayms = p.AwayScore THEN 6\r\n             WHEN m.HomeMS!=m.awayms and m.homems = p.HomeScore AND ABS(m.awayms - p.AwayScore) = 1 THEN 2\r\n\t\t\t WHEN m.HomeMS!=m.awayms and m.AwayMS = p.AwayScore AND ABS(m.HomeMS - p.HomeScore) = 1 THEN 2\r\n\t\t\t WHEN m.HomeMS>m.AwayMS and p.MS1=1 and MSX is Null THEN 3\r\n\t\t\t WHEN m.HomeMS<m.AwayMS and p.MS2=1 and MSX is Null THEN 3\r\n\t\t\t  WHEN m.HomeMS=m.AwayMS and p.MSX=1 and MS1 is NULL AND MS2 is NULL THEN 3\r\n\t\t\t WHEN m.HomeMS!=m.awayms and m.HomeMS>m.AwayMS and p.MS1=1 and MSX=1 THEN 1\r\n\t\t\t WHEN m.HomeMS!=m.awayms and m.HomeMS<m.AwayMS and p.MS2=1 and MSX=1 THEN 1\r\n\t\t\t\r\n             ELSE 0 END) AS Total_Points,\r\n\t\t\t SUM(CASE WHEN m.homems = p.HomeScore AND m.awayms = p.AwayScore THEN 6 Else 0 End) as 'Score_Points',\r\n\t\t\t SUM(CASE WHEN m.homems = p.HomeScore AND ABS(m.awayms - p.AwayScore) = 1 THEN 2 WHEN m.AwayMS = p.AwayScore AND ABS(m.HomeMS - p.HomeScore) = 1 THEN 2 Else 0 End) as 'ScoreEyt_Points',\r\n\t\t\t SUM(CASE  WHEN m.HomeMS>m.AwayMS and p.MS1=1 and MSX is Null THEN 3 WHEN m.HomeMS<m.AwayMS and p.MS2=1 and MSX is Null THEN 3 WHEN m.HomeMS=m.AwayMS and p.MSX=1 and MS1 is NULL AND MS2 is NULL THEN 3 Else 0 End) as 'MS_Points',\r\n\t\t\t  SUM(CASE  WHEN m.HomeMS>m.AwayMS and p.MS1=1 and MSX=1 THEN 1 WHEN m.HomeMS<m.AwayMS and p.MS2=1 and MSX=1 THEN 1 Else 0 End) as 'MSEyt_Points',\r\n\t\t\t Count(*) 'Total_Prediction'\r\nFROM match m\r\nJOIN prediction p ON m.Id = p.MatchId join Users on UserId=Users.Id\r\nGROUP BY Username order by Total_Points desc;\r\n").ToList();
        }

        public List<Standing> GetAllStandingByCountryId(int countryId)
        {
            return _context.Database.SqlQueryRaw<Standing>($"SELECT \r\n    Username,\r\n    SUM(\r\n\t\t\tCASE \r\n\t\t\tWHEN m.homems = p.HomeScore AND m.awayms = p.AwayScore THEN 6\r\n             WHEN m.HomeMS!=m.awayms and m.homems = p.HomeScore AND ABS(m.awayms - p.AwayScore) = 1 THEN 2\r\n\t\t\t WHEN m.HomeMS!=m.awayms and m.AwayMS = p.AwayScore AND ABS(m.HomeMS - p.HomeScore) = 1 THEN 2\r\n\t\t\t WHEN m.HomeMS>m.AwayMS and p.MS1=1 and MSX is Null THEN 3\r\n\t\t\t WHEN m.HomeMS<m.AwayMS and p.MS2=1 and MSX is Null THEN 3\r\n\t\t\t  WHEN m.HomeMS=m.AwayMS and p.MSX=1 and MS1 is NULL AND MS2 is NULL THEN 3\r\n\t\t\t WHEN m.HomeMS!=m.awayms and m.HomeMS>m.AwayMS and p.MS1=1 and MSX=1 THEN 1\r\n\t\t\t WHEN m.HomeMS!=m.awayms and m.HomeMS<m.AwayMS and p.MS2=1 and MSX=1 THEN 1\r\n\t\t\t\r\n             ELSE 0 END) AS Total_Points,\r\n\t\t\t SUM(CASE WHEN m.homems = p.HomeScore AND m.awayms = p.AwayScore THEN 6 Else 0 End) as 'Score_Points',\r\n\t\t\t SUM(CASE WHEN m.homems = p.HomeScore AND ABS(m.awayms - p.AwayScore) = 1 THEN 2 WHEN m.AwayMS = p.AwayScore AND ABS(m.HomeMS - p.HomeScore) = 1 THEN 2 Else 0 End) as 'ScoreEyt_Points',\r\n\t\t\t SUM(CASE  WHEN m.HomeMS>m.AwayMS and p.MS1=1 and MSX is Null THEN 3 WHEN m.HomeMS<m.AwayMS and p.MS2=1 and MSX is Null THEN 3 WHEN m.HomeMS=m.AwayMS and p.MSX=1 and MS1 is NULL AND MS2 is NULL THEN 3 Else 0 End) as 'MS_Points',\r\n\t\t\t  SUM(CASE  WHEN m.HomeMS>m.AwayMS and p.MS1=1 and MSX=1 THEN 1 WHEN m.HomeMS<m.AwayMS and p.MS2=1 and MSX=1 THEN 1 Else 0 End) as 'MSEyt_Points',\r\n\t\t\t Count(*) 'Total_Prediction'\r\nFROM match m \r\nJOIN prediction p ON m.Id = p.MatchId join Users on UserId=Users.Id join League on league.Id=m.LeagueId where CountryId={countryId}\r\nGROUP BY Username order by Total_Points desc;\r\n").ToList();
        }

        public List<StandingMatch> GetAllStandingMatchesByUsername(string username)
        {
            return _context.Database.SqlQueryRaw<StandingMatch>($"SELECT * \r\nFROM (\r\n    SELECT \r\n        Username,\r\n\t\tUserId,\r\n        t1.name as homeTeamName,\r\n        t2.name as awayTeamName,\r\n\t\tt1.LogoId as homeTeamLogoId,\r\n\t\tt2.LogoId as awayTeamLogoId,\r\n        m.date,\r\n        p.PredictionTime,\r\n        m.homeMS,\r\n        m.awayMS,\r\n\t\tp.homeScore,\r\n\t\tp.awayscore,\r\n\t\tp.ms1,\r\n\t\tp.msx,\r\n\t\tp.ms2,\r\n        SUM(\r\n            CASE \r\n            WHEN m.homems = p.HomeScore AND m.awayms = p.AwayScore THEN 6\r\n            WHEN m.HomeMS!=m.awayms and m.homems = p.HomeScore AND ABS(m.awayms - p.AwayScore) = 1 THEN 2\r\n            WHEN m.HomeMS!=m.awayms and m.AwayMS = p.AwayScore AND ABS(m.HomeMS - p.HomeScore) = 1 THEN 2\r\n            WHEN m.HomeMS>m.AwayMS and p.MS1=1 and MSX is Null THEN 3\r\n            WHEN m.HomeMS<m.AwayMS and p.MS2=1 and MSX is Null THEN 3\r\n            WHEN m.HomeMS=m.AwayMS and p.MSX=1 and MS1 is NULL AND MS2 is NULL THEN 3\r\n            WHEN m.HomeMS!=m.awayms and m.HomeMS>m.AwayMS and p.MS1=1 and MSX=1 THEN 1\r\n            WHEN m.HomeMS!=m.awayms and m.HomeMS<m.AwayMS and p.MS2=1 and MSX=1 THEN 1\r\n            ELSE 0 END) [Total_Points]\r\n    FROM match m \r\n    JOIN prediction p ON m.Id = p.MatchId \r\n    JOIN Users on UserId=Users.Id \r\n    JOIN League on league.Id=m.LeagueId \r\n\tJoin Team t1 on m.HomeTeamId=t1.Id\r\n\tJoin Team t2 on m.awayteamId=t2.Id\r\n    WHERE UserName='{username}'\r\n    GROUP BY Username,UserId,t1.Name,    t2.name,t1.LogoId,t2.LogoId,\r\n        m.date,\r\n        p.PredictionTime,m.homeMS,\r\n        m.awayMS ,p.homeScore,\r\n\t\tp.awayscore,\r\n\t\tp.ms1,\r\n\t\tp.msx,\r\n\t\tp.ms2\r\n) AS t\r\nWHERE t.Total_Points != 0 \r\nORDER BY t.date DESC ;").ToList();
        }
    }
}
