using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Newmes.Models
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string HallTicketNo { get; set; }
        public string Password { get; set; }
    }
}