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
        private string _filename;
        public PollFileRepository(IConfiguration configuration)
        {
            _filename = configuration["PollsFileName"];

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

            System.IO.File.WriteAllText(_filename, contents);
        }

        public IQueryable<Poll> GetPolls()
        {
            if (System.IO.File.Exists(_filename) == false)
            {
                return new List<Poll>().AsQueryable();
            }
            else
            {
                string contents = System.IO.File.ReadAllText(_filename);

                var listOfPolls = JsonConvert.DeserializeObject<List<Poll>>(contents);

                return listOfPolls.AsQueryable();
            }
        }

        public Poll GetPoll(int id)
        {
            if (System.IO.File.Exists(_filename) == false)
            {
                return new Poll();
            }
            else
            {
                string contents = System.IO.File.ReadAllText(_filename);

                var polls = JsonConvert.DeserializeObject<List<Poll>>(contents);

                var poll = polls.SingleOrDefault(x => x.Id == id);

                return poll;
            }
        }

        public void Vote(Poll poll)
        {
            var polls = GetPolls();

            var oldPoll = polls.SingleOrDefault(x => x.Id == poll.Id);

            oldPoll.Option1VotesCount += poll.Option1VotesCount;
            oldPoll.Option2VotesCount += poll.Option2VotesCount;
            oldPoll.Option3VotesCount += poll.Option3VotesCount;

            string contents = JsonConvert.SerializeObject(polls);

            System.IO.File.WriteAllText(_filename, contents);
        }
    }
}
