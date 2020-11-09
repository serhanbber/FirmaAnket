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
    public class QuestionService : BaseService, IService<QuestionDTO>
    {
        public bool Delete(int ID, out string message)
        {
            message = "Soru Silindi";
            try
            {
                var que = DbModel.Question.Find(ID);
                if (que == null)
                    throw new Exception("Silinecek Soru Bulunamadı");
                DbModel.Question.Remove(que);
                DbModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                message = "Hata" + ex.Message;
                return false;
            }
        }

        public List<QuestionDTO> GetList()
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

        public IQueryable<QuestionDTO> GetQueryable()
        {
            return from q in DbModel.Question
                   select new QuestionDTO()
                   {
                       ID=q.ID,
                       Question = q.QuestionLine,
                       CreatedDate = q.CreatedDate,
                       CreatedBy = q.CreatedBy,
                       ModifyDate = q.ModifyDate,
                       ModifyBy = q.ModifyBy


                   };
        }

        public QuestionDTO GetSingle(int entityID)
        {
            try
            {
                return GetQueryable().FirstOrDefault(x => x.ID == entityID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Save(QuestionDTO entity, out string message)
        {
            message = "Soru Kaydedildi";
            try
            {
                Question q = new Question()
                {
                    QuestionLine = entity.Question,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "System",
                    ModifyDate = entity.ModifyDate,
                    ModifyBy = entity.ModifyBy
                };
                DbModel.Question.Add(q);
                DbModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                message = "Hata" + ex.Message;
                return false;
            }
        }

        public bool Update(QuestionDTO entity, out string message)
        {
            message = "Güncelleme Başarılı";

            try
            {
                var model = DbModel.Question.Find(entity.ID);
                if (model == null)
                    throw new Exception("Belirtilen Cevap Bulunamadı");

                model.ID = entity.ID;
                model.QuestionLine = entity.Question;
              
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = "System";

                //DbModel.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                //DbModel.Entry(entity).Property(e => e.CreatedBy).IsModified = false;
                //DbModel.Entry(entity).Property(e => e.CreatedDate).IsModified = false;

                model.ModifyDate = DateTime.Now;
                model.ModifyBy = "Sero";

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
