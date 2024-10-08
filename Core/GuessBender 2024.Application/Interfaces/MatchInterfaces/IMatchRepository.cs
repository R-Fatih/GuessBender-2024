﻿using GuessBender_2024.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Interfaces.MatchInterfaces
{
    public interface IMatchRepository
    {
        List<Match> GetMatchWithTeamAndLeagueDetails();
        List<Match> GetMatchWithTeamAndLeagueDetailsByDate(DateTime date);
        List<Match> GetMatchWithTeamAndLeagueAndPredictionDetailsByDateAndUserId(DateTime date,string userId);
        List<Match> GetMatchWithTeamAndLeagueDetailsByDatesBetween(DateTime date1,DateTime date2);
        Match GetMatchByIdWithTeamAndLeagueDetails(int id);
    }
}
