using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmaAnket.Models
{
    public class AnswerDTO:BaseModel
    {
        public string PersonalCode { get; set; }
        public string PersonalName { get; set; }
        public string UserCode { get; set; }
        public string Score { get; set; }
        public bool? IsComplete { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
