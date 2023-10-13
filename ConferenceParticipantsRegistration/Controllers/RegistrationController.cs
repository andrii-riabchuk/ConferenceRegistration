using ConferenceParticipantsRegistration.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ConferenceParticipantsRegistration.Controllers
{
    public class RegistrationController: Controller
    {
        private bool isEmailUsed(string email) {

            using (var dbContext = new DatabaseContext())
            {
                if (dbContext.Participants.Any(x => x.Email == email))
                    return true;
            }

            return false;
        }

        [HttpPost]
        public ActionResult Register(ParticipantView participantView) {

            if (isEmailUsed(participantView.Email))
            {
                ModelState.AddModelError("Email", "Email is already used");
            }

            using (var dbContext = new DatabaseContext())
            {
                var participant = new Participant
                {
                    FullName = participantView.FullName,
                    Email = participantView.Email,
                    Phone = participantView.Phone,
                    Age = 20,
                    Password = participantView.Password,
                    EnrollmentDate = DateTime.Now
                };
                dbContext.Participants.Add(participant);
                dbContext.SaveChanges();
            }

            participantView.RegionalCenters = new string[] { "Lviv", "Kyiv" };
            
            if (!ModelState.IsValid)
                return View("Index", participantView);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            return View(new ParticipantView { RegionalCenters = new string[] { "Lviv", "Kyiv" } });
        }
    }
}