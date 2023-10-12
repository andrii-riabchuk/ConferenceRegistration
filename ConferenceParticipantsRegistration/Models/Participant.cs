using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using ConferenceParticipantsRegistration.Models;

namespace ConferenceParticipantsRegistration.Database
{
    public class Participant
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }

        public int? RegionalCenterId { get; set; }
        public virtual RegionalCenter RegionalCenter { get; set; }
    }

    public class ParticipantView
    {
        [Required(ErrorMessage = "{0} is required.")]
        public string FullName { get; set; }
        public int? Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        
        public string RegionalCenter { get; set; }

        public string[] RegionalCenters { get; set; }
    }

}