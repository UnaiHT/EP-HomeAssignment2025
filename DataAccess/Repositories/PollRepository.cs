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

        public void UpdatePoll(Poll p)
        {
            var oldPoll = GetPoll(p.Id);
            oldPoll.Title = p.Title;
            oldPoll.Option1Text = p.Option1Text;
            oldPoll.Option2Text = p.Option2Text;
            oldPoll.Option3Text = p.Option3Text;
            oldPoll.Option1VotesCount = p.Option1VotesCount;
            oldPoll.Option2VotesCount = p.Option2VotesCount;
            oldPoll.Option3VotesCount = p.Option3VotesCount;
            _pollContext.SaveChanges();
        }
    }
}
