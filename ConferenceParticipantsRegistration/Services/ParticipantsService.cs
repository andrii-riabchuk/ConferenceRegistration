using ConferenceParticipantsRegistration.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConferenceParticipantsRegistration.Services
{
    public class ParticipantsService
    {
        public IEnumerable<Participant> GetParticipantsForPage(int pageSize, int page)
        {
            using (var dbContext = new DatabaseContext())
            {
                var participants = dbContext.Participants.OrderByDescending(x => x.EnrollmentDate);
                var currentPage = participants.Skip(pageSize * page).Take(pageSize).ToList();

                return currentPage;
            }
        }

        public int CalculatePagesCount(int pageSize)
        {
            using (var dbContext = new DatabaseContext())
            {
                var pages = (double)dbContext.Participants.Count() / pageSize;
                return Convert.ToInt32(Math.Ceiling(pages));
            }
        }
    }
}