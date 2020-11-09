using FirmaAnket.Business.Services;
using FirmaAnket.Models;
using FirmaAnket.WebUI.FilterAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirmaAnket.WebUI.Controllers
{
    [_LoginFilterControl]
    public class QuestionController : Controller
    {
        private readonly QuestionService _questionService;
        public QuestionController()
        {
            _questionService = new QuestionService();
        }
        // GET: Question
        public ActionResult Index()
        {
            if (Session["Admin"]==null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            else
            {
                var model = _questionService.GetList();
                return View(model);
            }
           
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(QuestionDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string msg = "";
                    _questionService.Save(model, out msg);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        public ActionResult Edit(int id)
        {
            var model = _questionService.GetSingle(id);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(QuestionDTO model)
        {
            try
            {
                string msg = "";
                _questionService.Update(model,out msg);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(model);
            }

        }

        public ActionResult Delete(int id)
        {
            string msg = "";
            var model = _questionService.GetSingle(id);
            if (model == null)
                return HttpNotFound();
            else
                _questionService.Delete(model.ID,out msg);
            return RedirectToAction("Index");
        }
    }
}