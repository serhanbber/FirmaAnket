using FirmaAnket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmaAnket.Business.Interfaces
{
    public interface IService<T> where T :BaseModel
    {
        List<T> GetList();
        T GetSingle(int entityID);
        bool Save(T entity, out string message);
        bool Update(T entity, out string message);
        bool Delete(int ID, out string message);
        IQueryable<T> GetQueryable();

    }
}
