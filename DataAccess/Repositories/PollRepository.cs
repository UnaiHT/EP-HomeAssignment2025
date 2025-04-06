using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataContext;
using Domain.Models;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

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

        public void CreatePoll(Poll p)
        {
            p.DateCreated = DateTime.Now;
            p.Option1VotesCount = 0;
            p.Option2VotesCount = 0;
            p.Option3VotesCount = 0;
            _pollContext.Polls.Add(p);
            _pollContext.SaveChanges();
        }

        public void Vote(Poll p, string id)
        {

            var oldPoll = GetPolls().SingleOrDefault(x => x.Id == p.Id);;
            oldPoll.Option1VotesCount += p.Option1VotesCount;
            oldPoll.Option2VotesCount += p.Option2VotesCount;
            oldPoll.Option3VotesCount += p.Option3VotesCount;

            var vote = new Vote();
            vote.PollFK = oldPoll.Id;
            vote.UserId = id;
            _pollContext.Votes.Add(vote);
            _pollContext.SaveChanges();
        }

        public IQueryable<Vote> GetVotes(int id)
        {
            return _pollContext.Votes.Where(x=>x.PollFK==id);
        }
    }
}
