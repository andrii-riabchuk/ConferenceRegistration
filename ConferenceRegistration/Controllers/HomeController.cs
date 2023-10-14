using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using ConferenceRegistration.Models;
using ConferenceRegistration.Services;
using Microsoft.AspNet.Identity.Owin;

namespace ConferenceParticipantsRegistration.Controllers
{
    public class HomeController : Controller
    {
        private ParticipantsService _participantsService;
        private const int _defaultPageSize = 9;
        public HomeController() { }

        public ParticipantsService ParticipantsService
        {
            get
            {
                if (_participantsService == null)
                {
                    var dbContext = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                    _participantsService = new ParticipantsService(dbContext);
                }
                return _participantsService;
            }
        }

        [Authorize]
        public ActionResult Index()
        {
            return RedirectToAction("Participants");
        }

        [Authorize]
        public ActionResult Participants(int page = 1, int pageSize = _defaultPageSize)
        {
            var participants = ParticipantsService.GetParticipantsForPage(pageSize, page - 1);
            var totalPages = ParticipantsService.CalculatePagesCount(pageSize);

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

            var participants = ParticipantsService.GetParticipantsForPage(pageSize, page, sortBy, ascending);

            return PartialView("_ParticipantsPartial", participants);
        }
    }
}