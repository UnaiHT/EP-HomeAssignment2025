using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataContext;
using Domain.Models;
using Domain.Interfaces;

namespace DataAccess.Repositories
{
    public class PollRepository: IPollsRepository
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

        public Poll GetPoll(int id)
        {
            return _pollContext.Polls.SingleOrDefault(x => x.Id == id);
        }

        public void CreatePoll(Poll p)
        {
            p.DateCreated = DateTime.Now;
            p.Option1VotesCount = 0;
            p.Option2VotesCount = 0;
            p.Option3VotesCount = 0;
            _pollContext.Polls.Add(p);
            _pollContext.SaveChanges();
        }

        public void Vote(Poll p)
        {
            var oldPoll = GetPoll(p.Id);
            oldPoll.Option1VotesCount += p.Option1VotesCount;
            oldPoll.Option2VotesCount += p.Option2VotesCount;
            oldPoll.Option3VotesCount += p.Option3VotesCount;
            _pollContext.SaveChanges();
        }
    }
}
