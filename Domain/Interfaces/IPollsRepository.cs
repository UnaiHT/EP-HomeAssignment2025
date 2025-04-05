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

        Poll GetPoll(int id);

        void Vote(Poll poll);
    }
}
