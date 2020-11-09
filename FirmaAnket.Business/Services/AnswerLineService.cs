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
    public class AnswerLineService : BaseService, IService<AnswerLineDTO>
    {
        public bool Delete(int ID, out string message)
        {
            message = "Soru Silindi";
            try
            {
                var que = DbModel.AnswerLine.Find(ID);
                if (que == null)
                    throw new Exception("Silinecek Soru Bulunamadı");
                DbModel.AnswerLine.Remove(que);
                DbModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                message = "Hata" + ex.Message;
                return false;
            }
        }

        public List<AnswerLineDTO> GetList()
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

        public IQueryable<AnswerLineDTO> GetQueryable()
        {
            return from an in DbModel.AnswerLine
                   select new AnswerLineDTO()
                   {
                       ID = an.ID,
                       AnswerID = an.AnswerID,
                       Answer = an.Answer,
                       Question = an.Question,
                       Image=an.Image

                   };
        }

        public AnswerLineDTO GetSingle(int entityID)
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

        public bool Save(AnswerLineDTO entity, out string message)
        {
            message = "Soru Kaydedildi";
            try
            {
                AnswerLine an = new AnswerLine()
                {
                    AnswerID=entity.AnswerID,
                    Answer=entity.Answer,
                    Question=entity.Question,
                    Image=entity.Image
                    
                };
                DbModel.AnswerLine.Add(an);
                DbModel.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                message = "Hata" + ex.Message;
                return false;
            }
        }

        public bool Update(AnswerLineDTO entity, out string message)
        {
            message = "Soru Güncellendi";
            try
            {
                var model = DbModel.AnswerLine.Find(entity.ID);
                if (model == null)
                    throw new Exception("Belirtilen Soru Bulunamadı");
                model.ID = entity.ID;
                model.AnswerID = entity.AnswerID;
                model.Answer = entity.Answer;
                model.Image = entity.Image;
                model.Question = entity.Question;

                DbModel.SaveChanges();
                return true;


            }
            catch (Exception ex)
            {
                message = "Hata" + ex.Message;
                return false;
            }
        }
    }
}
