using ConferenceParticipantsRegistration.Database;
using ConferenceParticipantsRegistration.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ConferenceParticipantsRegistration.Services;

namespace ConferenceParticipantsRegistration.Controllers
{
    public class HomeController : Controller
    {
        private ParticipantsService _participantsService;

        public HomeController() {
            _participantsService = new ParticipantsService();
        }

        public ActionResult Index()
        {
            return RedirectToAction("Participants");
        }

        public ActionResult Participants(int page = 1)
        {
            var pageSize = 5;
            var participants = _participantsService.GetParticipantsForPage(pageSize, page - 1);
            var totalPages = _participantsService.CalculatePagesCount(pageSize);

            var participantsPage = new ParticipantsPage
            {
                Participants = participants,
                Page = page,
                TotalPages = totalPages
            };

            return View(participantsPage);
        }

        public ActionResult LoadParticipants(int page = 1)
        {
            page -= 1;
            int pageSize = 5;
            
            var participants = _participantsService.GetParticipantsForPage(pageSize, page);
            
            return PartialView("_ParticipantsPartial", participants);
        }
    }
}