using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IPollsRepository
    {

        void CreatePoll(Poll myLog);

        IQueryable<Poll> GetPolls();

        void Vote(Poll poll, string id);

        IQueryable<Vote> GetVotes(int id);
    }
}
