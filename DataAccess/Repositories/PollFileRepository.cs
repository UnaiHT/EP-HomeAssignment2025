using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace DataAccess.Repositories
{
    public class PollFileRepository : IPollsRepository
    {
        private string _pollFilename;
        private string _voteFilename;
        public PollFileRepository(IConfiguration configuration)
        {
            _pollFilename = configuration["PollsFileName"];
            _voteFilename = configuration["VotesFileName"];
        }
        public void CreatePoll(Poll p)
        {
            var listOfPolls = GetPolls().ToList();

            p.DateCreated = DateTime.Now;
            p.Option1VotesCount = 0;
            p.Option2VotesCount = 0;
            p.Option3VotesCount = 0;

            listOfPolls.Add(p);

            string contents = JsonConvert.SerializeObject(listOfPolls);

            System.IO.File.WriteAllText(_pollFilename, contents);
        }

        public IQueryable<Poll> GetPolls()
        {
            if (System.IO.File.Exists(_pollFilename) == false)
            {
                return new List<Poll>().AsQueryable();
            }
            else
            {
                string contents = System.IO.File.ReadAllText(_pollFilename);

                var listOfPolls = JsonConvert.DeserializeObject<List<Poll>>(contents);

                return listOfPolls.AsQueryable();
            }
        }

        public void Vote(Poll poll, string id)
        {
            var polls = GetPolls();

            var oldPoll = polls.SingleOrDefault(x => x.Id == poll.Id);

            oldPoll.Option1VotesCount += poll.Option1VotesCount;
            oldPoll.Option2VotesCount += poll.Option2VotesCount;
            oldPoll.Option3VotesCount += poll.Option3VotesCount;

            var votes = GetVotes(oldPoll.Id).ToList();
            var vote = new Vote();
            vote.PollFK = oldPoll.Id;
            vote.UserId = id;
            votes.Add(vote);

            string pollContents = JsonConvert.SerializeObject(polls);
            string voteContents = JsonConvert.SerializeObject(votes);

            System.IO.File.WriteAllText(_pollFilename, pollContents);
            System.IO.File.WriteAllText(_voteFilename, voteContents);
        }

        public IQueryable<Vote> GetVotes(int id)
        {
            if (System.IO.File.Exists(_voteFilename) == false)
            {
                return new List<Vote>().AsQueryable();
            }
            else
            {
                string contents = System.IO.File.ReadAllText(_voteFilename);

                var listOfVotes = JsonConvert.DeserializeObject<List<Vote>>(contents).Where(x=>x.PollFK==id);

                return listOfVotes.AsQueryable();
            }
        }
    }
}
