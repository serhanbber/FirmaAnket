using FirmaAnket.Dal;
using FirmaAnket.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmaAnket.Business
{
    public class BaseService
    {
        public static AnketDBEntities DbModel { get; set; }

        public BaseService()
        {
            DbModel = DBHelper.CreateDB();
        }
    }
}
