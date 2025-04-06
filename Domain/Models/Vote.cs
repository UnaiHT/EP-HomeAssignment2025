using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }

        public int PollFK { get; set; }

        //Navigational property
        [ForeignKey("PollFK")]
        public virtual Poll Poll { get; set; }

        public string UserFK { get; set; }

        //Navigational property
        [ForeignKey("UserFK")]
        public virtual CustomUser User { get; set; }
    }
}
