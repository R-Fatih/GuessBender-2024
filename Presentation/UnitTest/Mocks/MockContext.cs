using GuessBender_2024.Domain.Entities;
using GuessBender_2024.Persistance.Context;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Mocks
{
    public static class MockContext
    {
        public static Mock<GuessBenderContext> GetDbContext()
        {
            List<Team> todoLists = new List<Team>
            {
                new Team{ Id = 1, Name = "List 1"},
                new Team{ Id = 2, Name = "List 2"}
            };
            var mockDbContext = new Mock<GuessBenderContext>();
            mockDbContext.Setup(c => c.Team);
            return mockDbContext;
        }
    }
}
