using System.Data.SqlClient;
using System.Web.Mvc;
using ConferenceRegistration.Models;
using ConferenceRegistration.Services;

namespace ConferenceParticipantsRegistration.Controllers
{
    public class HomeController : Controller
    {
        private ParticipantsService _participantsService;
        private const int _defaultPageSize = 9;
        public HomeController()
        {
            _participantsService = new ParticipantsService();
        }

        [Authorize]
        public ActionResult Index()
        {
            return RedirectToAction("Participants");
        }

        [Authorize]
        public ActionResult Participants(int page = 1, int pageSize = _defaultPageSize)
        {
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

        public ActionResult LoadParticipants(int page = 1,int pageSize = _defaultPageSize, int? sortBy = null, bool ascending = true)
        {
            page -= 1;

            var participants = _participantsService.GetParticipantsForPage(pageSize, page, sortBy, ascending);

            return PartialView("_ParticipantsPartial", participants);
        }
    }
}