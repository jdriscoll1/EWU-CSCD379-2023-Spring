﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Reflection.Metadata;

namespace Wordle.Api.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Word> Words => Set<Word>();
        public DbSet<Play> Plays => Set<Play>();
        public DbSet<AppUser> Users => Set<AppUser>();

        public DbSet<Game> Games => Set<Game>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)  //needs work 
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Word>()
                .HasIndex(f => f.WordId)
                .IsUnique();

        }
    }
}
