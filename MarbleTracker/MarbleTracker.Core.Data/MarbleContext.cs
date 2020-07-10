using MarbleTracker.Core.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Core.Data
{
    public class MarbleContext : DbContext
    {
        public MarbleContext(DbContextOptions<MarbleContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<ChallengeResult> ChallengeResults { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
