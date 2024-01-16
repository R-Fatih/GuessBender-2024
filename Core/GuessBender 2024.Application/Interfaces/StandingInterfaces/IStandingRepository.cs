using GuessBender_2024.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBender_2024.Application.Interfaces.StandingInterfaces
{
    public interface IStandingRepository
    {
        List<Standing> GetAllStanding();
        List<Standing> GetAllStandingByCountryId(int countryId);
    }
}
