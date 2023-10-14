using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using ConferenceRegistration.Controllers;
using ConferenceRegistration.Test;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ConferenceRegistration.Models
{
    public class Participant : IdentityUser
    {
        public string FullName { get; set; }
        public int Age { get; set; }

        public int RegionId {  get; set; }
        public DateTime EnrollmentDate {get; set;}

        public virtual Region Region { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Participant> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<Participant>
    {
        public ApplicationDbContext()
            : base("ConferenceDB", throwIfV1Schema: false)
        {
            //Sample Data
            if (!Users.Any() || !Regions.Any())
            {
                var testService = new TestService(this);
                testService.GenerateRegions();
                testService.RegisterUsers();
            }
        }

        public DbSet<Region> Regions { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}