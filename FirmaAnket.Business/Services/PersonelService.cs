using FirmaAnket.Business.Interfaces;
using FirmaAnket.Dal.Model;
using FirmaAnket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmaAnket.Business.Services
{
    public class PersonelService : BaseService, IService<PersonelDTO>
    {
        public bool Delete(int ID, out string message)
        {
            message = "Personel Silindi";
            try
            {
                var personal = DbModel.Personal.Find(ID);
                if (personal == null)
                    throw new Exception("Silinecek Cevap Bulunamadı");
                DbModel.Personal.Remove(personal);
                DbModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                message = "Hata" + ex.Message;
                return false;
            }
        }

        public List<PersonelDTO> GetList()
        {
            try
            {
                return GetQueryable().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IQueryable<PersonelDTO> GetQueryable()
        {
            return from p in DbModel.Personal
                   select new PersonelDTO()
                   {
                       ID=p.ID,
                       NameSurname=p.NameSurname,
                       Code=p.Code,
                       Password=p.Password,
                       CreatedDate=p.CreatedDate,
                       CreatedBy=p.CreatedBy,
                       ModifyDate=p.ModifyDate,
                       ModifiedBy=p.ModifiedBy,
                       isAdmin=p.IsAdmin,
                      
                   };
        }

        public PersonelDTO GetSingle(int entityID)
        {
            try
            {
                return GetQueryable().FirstOrDefault(x=>x.ID==entityID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Save(PersonelDTO entity, out string message)
        {
            message = "Personel Kayıt edildi";
            try
            {
                Personal p = new Personal()
                {
                    NameSurname = entity.NameSurname,
                    Code = entity.Code,
                    Password=entity.Password,
                    CreatedDate=DateTime.Now,
                    //CreatedBy = entity.CreatedBy,
                    //CreatedBy = entity.NameSurname,
                    //CreatedBy = cokieName,
                    ModifyDate=entity.ModifyDate,
                    ModifiedBy=entity.ModifiedBy,
                    IsAdmin=entity.isAdmin
                };
                DbModel.Personal.Add(p);
                DbModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                message = "Hata" + ex.Message;
                return false;
            }
        }

        public bool Update(PersonelDTO entity, out string message)
        {
            message = "Güncelleme Başarılı";
         
            try
            {
                var model = DbModel.Personal.Find(entity.ID);
                if (model == null)
                    throw new Exception("Belirtilen Kişi Bulunamadı");
                
                model.ID = entity.ID;
                model.NameSurname = entity.NameSurname;
                model.Code = entity.Code;
                model.Password = entity.Password;
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = entity.NameSurname;
                model.IsAdmin = entity.isAdmin;

                //DbModel.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                //DbModel.Entry(entity).Property(e => e.CreatedBy).IsModified = false;
                //DbModel.Entry(entity).Property(e => e.CreatedDate).IsModified = false;

                model.ModifyDate = DateTime.Now;
                model.ModifiedBy = "System Modify";

                DbModel.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                message = "HATA" + ex.Message;
                return false;

            }
        }

        public PersonelDTO Login(PersonelDTO kullanici)
        {
            try
            {
                return GetQueryable().FirstOrDefault(x => x.Code == kullanici.Code && x.Password == kullanici.Password);
               
            }
            catch (Exception ex)
            {
                return null;
            }
        }

       
        public bool Save2(PersonelDTO entity, string cokieName, out string message)
        {
            message = "Personel Kayıt edildi";
            try
            {
                Personal p = new Personal()
                {
                    NameSurname = entity.NameSurname,
                    Code = entity.Code,
                    Password = entity.Password,
                    CreatedDate = DateTime.Now,
                    //CreatedBy = entity.CreatedBy,
                    //CreatedBy = entity.NameSurname,
                    CreatedBy = cokieName,
                    ModifyDate = entity.ModifyDate,
                    ModifiedBy = entity.ModifiedBy,
                    IsAdmin=entity.isAdmin
                };
                DbModel.Personal.Add(p);
                DbModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                message = "Hata" + ex.Message;
                return false;
            }
        }

        public bool Update2(PersonelDTO entity,string cokieName ,out string message)
        {
            message = "Güncelleme Başarılı";

            try
            {
                var model = DbModel.Personal.Find(entity.ID);
                if (model == null)
                    throw new Exception("Belirtilen Kişi Bulunamadı");

                model.ID = entity.ID;
                model.NameSurname = entity.NameSurname;
                model.Code = entity.Code;
                model.Password = entity.Password;
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = cokieName;

                //DbModel.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                //DbModel.Entry(entity).Property(e => e.CreatedBy).IsModified = false;
                //DbModel.Entry(entity).Property(e => e.CreatedDate).IsModified = false;

                model.ModifyDate = DateTime.Now;
                model.ModifiedBy = cokieName;

                model.IsAdmin = entity.isAdmin;

                DbModel.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                message = "HATA" + ex.Message;
                return false;

            }
        }


    }
}
