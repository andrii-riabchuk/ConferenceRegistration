using ConferenceParticipantsRegistration.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConferenceParticipantsRegistration.Models
{
    public class ParticipantsPage
    {
        public IEnumerable<Participant> Participants { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
    }
}