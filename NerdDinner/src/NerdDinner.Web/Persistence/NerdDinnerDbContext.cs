﻿using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using NerdDinner.Web.Models;

namespace NerdDinner.Web.Persistence
{
    public class NerdDinnerDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Dinner> Dinners { get; set; }

        public virtual DbSet<Rsvp> Rsvp { get; set; }

        public NerdDinnerDbContext()
        {
            Database.EnsureCreatedAsync().Wait();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add the one to many relationship between dinner and rsvp
            modelBuilder.Entity<Dinner>(d =>
            {
                d.Key(x => x.DinnerId);
                d.Property(x => x.DinnerId).GenerateValueOnAdd();
                d.HasMany(x => x.Rsvps).WithOne(x => x.Dinner).ForeignKey(x => x.DinnerId);
            });

            modelBuilder.Entity<Rsvp>(r =>
            {
                r.Key(x => x.RsvpId);
                r.Property(x => x.RsvpId).GenerateValueOnAdd();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
