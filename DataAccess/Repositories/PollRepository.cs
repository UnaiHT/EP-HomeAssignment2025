using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataContext;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class PollRepository
    {
        private PollDbContext _pollContext;

        public PollRepository(PollDbContext pollContext)
        {
            _pollContext = pollContext;
        }

        public IQueryable<Poll> GetPolls()
        {
            return _pollContext.Polls;
        }

        public void CreatePoll(Poll p)
        {
            p.DateCreated = DateTime.Now;
            _pollContext.Polls.Add(p);
            _pollContext.SaveChanges();
        }
    }
}
