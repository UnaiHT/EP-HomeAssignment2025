﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace DataAccess.DataContext
{
    public class PollDbContext : DbContext
    {

        public PollDbContext(DbContextOptions<PollDbContext> options) : base(options)
        {
        }

        public DbSet<Poll> Polls { get; set; }
    }
}
