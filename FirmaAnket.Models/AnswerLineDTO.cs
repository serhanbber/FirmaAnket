using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmaAnket.Models
{
    public class AnswerLineDTO:BaseModel
    {
        public int AnswerID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Image { get; set; }
        
       
    }
}
