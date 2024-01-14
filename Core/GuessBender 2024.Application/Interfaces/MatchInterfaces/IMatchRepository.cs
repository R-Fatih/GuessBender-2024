using GuessBender_2024.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Interfaces.TeamInterfaces
{
    public interface IMatchRepository
    {
        List<Match> GetMatchWithTeamAndLeagueDetails();
        List<Match> GetMatchWithTeamAndLeagueDetailsByDate(DateTime date);
        Match GetMatchByIdWithTeamAndLeagueDetails(int id);
    }
}
