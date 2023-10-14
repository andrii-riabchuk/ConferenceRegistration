using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using ConferenceRegistration.Controllers;
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
        private void GenerateRegions()
        {
            if (!Regions.Any())
            {
                var ukrainianRegions = new List<Region>
                {
                    new Region { Name = "Kyiv" },
                    new Region { Name = "Kharkiv" },
                    new Region { Name = "Lviv" },
                    new Region { Name = "Dnipro" },
                    new Region { Name = "Odesa" },
                    // Add more regions as needed
                };
                Regions.AddRange(ukrainianRegions);
                SaveChanges();
            }
        }

        private List<Participant> GenerateUsers()
        {
            var random = new Random();
           
            var userNames = new List<string>
                {
                    "Rene Descartes", "John Locke", "Immanuel Kant", "David Hume", "George Berkeley",
                    "Baruch Spinoza", "Gottfried Leibniz", "Thomas Hobbes", "John Stuart Mill",
                    "Friedrich Nietzsche", "Soren Kierkegaard", "Karl Marx", "Georg Wilhelm Friedrich Hegel", "Arthur Schopenhauer",
                    "Blaise Pascal", "Martin Heidegger", "Jean-Paul Sartre", "Albert Camus", "Emmanuel Levinas"
                };

            var moreNames = new List<string> {
                "Voltaire", "Friedrich Engels", "Simone de Beauvoir", "John Dewey", "William James",
                "Thomas Paine", "Edmund Burke", "Jean-Jacques Rousseau"
            };
            userNames = userNames.Concat(moreNames).ToList();

            var participants = new List<Participant> { };

            for (int i = 0; i < userNames.Count; i++)
            {
                var fullName = userNames[i];
                // Generate a unique email.
                string email = $"{fullName.Replace(" ", "_").Replace("-", "_")}@example.com";
                
                participants.Add(new Participant
                {
                    UserName = email, // Set UserName to the generated email.
                    FullName = fullName,
                    Age = random.Next(18, 80), // Age between 18 and 80.
                    Email = email,
                    PhoneNumber = random.Next(100000, 999999).ToString(), // Random 6-digit phone number.
                    RegionId = random.Next(1, 5), // RegionId between 1 and 3.
                    EnrollmentDate = DateTime.Now.Date.AddDays(random.Next(-2, 3)), // EnrollmentDate within 2 days of today.
                });
            }

            return participants;
        }

        private void RegisterUsers(List<Participant> participants)
        {
            if (!Users.Any())
            {
                var userStore = new UserStore<Participant>(this);
                var userManager = new ParticipantManager(userStore);

                foreach (var user in participants)
                {
                    try
                    {
                        var result = userManager.Create(user, "22041980lL@");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }


        public ApplicationDbContext()
            : base("ConferenceDB", throwIfV1Schema: false)
        {
            //Sample Data
            GenerateRegions();
            RegisterUsers(GenerateUsers()) ;
        }

        public DbSet<Region> Regions { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}