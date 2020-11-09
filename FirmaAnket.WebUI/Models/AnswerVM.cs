using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmaAnket.WebUI.Models
{
    public class AnswerVM
    {//sonradan ekledim jquery ile dolduruyorum
        public string Question { get; set; }
        public string Answer { get; set; }
        public string NameSurname { get; set; }
        public string Code { get; set; }
    }
}