using FirmaAnket.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmaAnket.Dal
{
    public class DBHelper
    {
        public DBHelper()
        {

        }
        private static AnketDBEntities DB { get; set; }

        public static AnketDBEntities CreateDB()
        {
            DB = DB ?? new AnketDBEntities();
            return DB;
        }
    }
}
