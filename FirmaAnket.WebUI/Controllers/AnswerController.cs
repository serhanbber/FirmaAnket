using FirmaAnket.Business.Services;
using FirmaAnket.Models;
using FirmaAnket.WebUI.FilterAttributes;
using FirmaAnket.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirmaAnket.WebUI.Controllers
{
    [_LoginFilterControl]
    public class AnswerController : Controller
    {
        private readonly PersonelService _personelService;
        private readonly AnswerService _answerService;
        private readonly QuestionService _questionService;
        private readonly AnswerLineService _answerLineService;
        public AnswerController()
        {
            _personelService = new PersonelService();
            _answerService = new AnswerService();
            _questionService = new QuestionService();
            _answerLineService = new AnswerLineService();
        }
        // GET: Answer 
        public ActionResult Index()
        {
            string userCode = Session["_loginedUserCode"].ToString();
            var model = _answerService.GetList().Where(m=>m.UserCode==userCode);
            return View(model);
        }
      
        public ActionResult Create(string Code)
        {
            string userCode = Session["_loginedUserCode"].ToString();
            if (Code == null)
            {
                List<SelectListItem> personList = (from person in _personelService.GetList()
                                                   where person.Code != userCode
                                                   select new SelectListItem
                                                   {
                                                       Text = person.NameSurname,
                                                       Value = person.Code.ToString()
                                                   }).ToList();

                ViewBag.Person = new SelectList(personList.OrderBy(m => m.Text), "Value", "Text");
                var que = _questionService.GetList();
                return View(que);
            }
            else
            {
                CalculateScore(Code);
                return RedirectToAction("Index");
            }

        }

        public void CalculateScore(string code)
        {
            double Yes = 0,No = 0,Result = 0;
            string userCode = Session["_loginedUserCode"].ToString();

            var answer = _answerService.GetList().FirstOrDefault(m => m.PersonalCode == code && m.UserCode == userCode);

            var answerline = _answerLineService.GetList().Where(m => m.AnswerID == answer.ID);

            foreach (var item in answerline)
            {
                if (item.Answer == Constants.AnswerType.Yes)
                {
                    Yes++;
                }
                else
                {
                    No++;
                }
                Result = (Yes / (Yes + No)) * 100;
                if (Result > 79)
                {
                    answer.IsComplete = true;
                }
                else
                {
                    answer.IsComplete = false;
                }
                answer.Score = Result.ToString();

                string msg = "";
                _answerService.Save(answer,out msg);
            }
        }

        //[HttpPost]
        //public ActionResult Create(AnswerLineDTO model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            string msg = "";
        //            var ktg = _answerLineService.GetList().Where(p => p.ID == model.ID).FirstOrDefault();

        //            //_answerLineService.Save(model, out msg);
        //            return RedirectToAction("Index");
        //        }
        //        else
        //            return View(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        return View(model);
        //    }
        //}


        public String SendData(AnswerVM model)
        {
            string msg = "";
            //var result = _answerService.Save(model,out msg);

            string userCode = Session["_loginedUserCode"].ToString();
            string userName = Session["_loginedUserName"].ToString();

            var mod = _answerService.GetList().FirstOrDefault(m => m.PersonalCode == model.Code && m.UserCode == userCode);

            if (mod != null)
            {
                AnswerLineDTO b = new AnswerLineDTO();
                b.AnswerID = mod.ID;
                b.Answer = model.Answer;
                b.Question = model.Question;
                SaveAnswerLine(b);
            }
            else
            {
                AnswerDTO answer = new AnswerDTO()
                {
                    PersonalCode = model.Code,
                    PersonalName = model.NameSurname,
                    UserCode = userCode,
                    CreateDate = DateTime.Now,
                    CreatedBy = userName

                };

                _answerService.Save(answer, out msg);
                AnswerLineDTO b = new AnswerLineDTO();
                b.AnswerID = answer.ID;
                b.Answer = model.Answer;
                b.Question = model.Question;
                SaveAnswerLine(b);
            }



            return "True";
        }

        public void SaveAnswerLine(AnswerLineDTO model)
        {
            string msg = "";

            var mod = _answerLineService.GetList().FirstOrDefault(m => m.AnswerID == model.AnswerID && m.Question == model.Question);

            if (mod != null)
            {
                mod.Answer = model.Answer;
            }

            _answerLineService.Save(model, out msg);



        }


        public ActionResult Detail(int? id)
        {
            var model = _answerLineService.GetList().Where(m=>m.AnswerID==id);
            return View(model);
        }

    }
}