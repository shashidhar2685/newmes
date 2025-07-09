using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Newmes.Models
{
    public class AdmitCardModel
    {
        public int CandidateId { get; set; }
        public string AdmitCardNo { get; set; }
        public DateTime ExamDate { get; set; }
        public string ExamCenter { get; set; }
    }
}