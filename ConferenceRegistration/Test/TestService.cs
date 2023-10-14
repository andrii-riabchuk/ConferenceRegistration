using ConferenceRegistration.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConferenceRegistration.Test
{
    public class TestService
    {
        private ApplicationDbContext _dbContext;

        public TestService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void GenerateRegions()
        {
            if (!_dbContext.Regions.Any())
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
                _dbContext.Regions.AddRange(ukrainianRegions);
                _dbContext.SaveChanges();
            }
        }

        public void RegisterUsers()
        {
            var participants = GenerateUsers();

            if (!_dbContext.Users.Any())
            {
                var userStore = new UserStore<Participant>(_dbContext);
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
    }
}