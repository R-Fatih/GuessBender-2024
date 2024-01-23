using GuessBender_2024.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Interfaces.LeagueStandingInterfaces
{
    public interface ILeagueStandingRepository
    {
        List<LeagueStanding> GeteagueStandingByLeagueId(int leagueId);
    }
}
