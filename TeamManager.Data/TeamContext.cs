using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TeamManager.Data.Models;

namespace TeamManager.Data
{
    public class TeamContext : DbContext
    {
        public TeamContext()
        {
        }

        public TeamContext(DbContextOptions options)
        : base(options)
        { }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Player> Players { get; set; }
    }
}
