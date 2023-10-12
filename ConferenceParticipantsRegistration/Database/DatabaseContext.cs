using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ConferenceParticipantsRegistration.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("ConferenceDB")
        {
        }

        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Participant>()
            //   .HasRequired(p => p.RegionalCenter)
            //   .WithMany()
            //   .HasForeignKey(p => p.RegionalCenterId);
        }
    }
}

