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
        public DbSet<UserGroupRelationship> UserGroupRelationships { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Wager> Wagers { get; set; }
    }
}
