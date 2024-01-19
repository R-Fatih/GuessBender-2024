using GuessBender_2024.Application.Interfaces.TeamInterfaces;
using GuessBender_2024.Domain.Entities;
using GuessBender_2024.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GuessBender_2024.Persistance.Repositories.TeamRepositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly GuessBenderContext _context;

        public MatchRepository(GuessBenderContext context)
        {
            _context = context;
        }

        public Match GetMatchByIdWithTeamAndLeagueDetails(int id)
        {
            return _context.Match.Include(x => x.HomeTeam).Include(y => y.AwayTeam).Include(z => z.League).Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Match> GetMatchWithTeamAndLeagueDetails()
        {
            return _context.Match.Include(x => x.HomeTeam).Include(y => y.AwayTeam).Include(z => z.League).ToList();
        }

        public List<Match> GetMatchWithTeamAndLeagueDetailsByDate(DateTime date)
        {
            return _context.Match.Include(x => x.HomeTeam).Include(y => y.AwayTeam).Include(z => z.League).Where(x => x.Date.Date == date.Date).ToList();

        }

        public List<Match> GetMatchWithTeamAndLeagueDetailsByDatesBetween(DateTime date1, DateTime date2)
        {
            return _context.Match.Include(x => x.HomeTeam).Include(y => y.AwayTeam).Include(z => z.League).Where(x => x.Date.Date >= date1.Date && x.Date.Date < date2.Date).ToList();

        }
    }
}
