using FirmaAnket.Business.Interfaces;
using FirmaAnket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirmaAnket.Dal.Model;

namespace FirmaAnket.Business.Services
{
    public class AnswerService : BaseService, IService<AnswerDTO>
    {
        public bool Delete(int ID, out string message)
        {
            message = "Kayıt Silindi";
            try
            {
                var konu = DbModel.Answer.Find(ID);
                if (konu == null)
                    throw new Exception("Silinecek Cevap Bulunamadı");
                DbModel.Answer.Remove(konu);
                DbModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                message = "Hata" + ex.Message;
                return false;
            }
        }

        public List<AnswerDTO> GetList()
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

        public IQueryable<AnswerDTO> GetQueryable()
        {
            return from a in DbModel.Answer
                   select new AnswerDTO()
                   {
                       ID=a.ID,
                       PersonalCode=a.PersonalCode,
                       PersonalName=a.PersonalName,
                       UserCode=a.UserCode,
                       Score=a.Score,
                       IsComplete=a.IsComplete,
                       CreateDate=a.CreateDate,
                       CreatedBy=a.CreatedBy
                   };
        }

        public AnswerDTO GetSingle(int entityID)
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

        public bool Save(AnswerDTO entity, out string message)
        {
            message = "Soru Kayıt edildi";
            try
            {
                Answer a = new Answer()
                {
                    PersonalCode=entity.PersonalCode,
                    PersonalName=entity.PersonalName,
                    UserCode=entity.UserCode,
                    Score=entity.Score,
                    IsComplete=entity.IsComplete,
                    CreateDate=entity.CreateDate,
                    CreatedBy=entity.CreatedBy
                };
                DbModel.Answer.Add(a);
                DbModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                message = "Hata" + ex.Message;
                return false;
            }
        }

        public bool Update(AnswerDTO entity, out string message)
        {
            message = "Güncelleme Başarılı";
            try
            {
                var model = DbModel.Answer.Find(entity.ID);
                if (model == null)
                    throw new Exception("Belirtilen Cevap Bulunamadı");

                model.ID = entity.ID;
                model.PersonalCode = entity.PersonalCode;
                model.PersonalName = entity.PersonalName;
                model.UserCode = entity.UserCode;
                model.Score = entity.Score;
                model.IsComplete = entity.IsComplete;
                model.CreateDate = entity.CreateDate;
                model.CreatedBy = entity.CreatedBy;

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
