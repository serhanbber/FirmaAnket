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
    public class PersonalController : Controller
    {
        private readonly PersonelService _personelService;
        public PersonalController()
        {
            _personelService = new PersonelService();
        }
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("SignIn", "Login");
            }
            var model = _personelService.GetList();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(PersonelDTO model,string Answer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Answer==Constants.AnswerType.Yes)
                    {
                        model.isAdmin = true;
                    }
                    else
                    {
                        model.isAdmin = false;
                    }
                    string msg = "";
                    string userName = Session["_loginedUserName"].ToString();
                    _personelService.Save2(model,userName, out msg);
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
            var per = _personelService.GetSingle(id);
            if (per == null)
                return HttpNotFound();
            return View(per);

        }
        [HttpPost]
        public ActionResult Edit(PersonelDTO model,string Answer)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    if (Answer == Constants.AnswerType.Yes)
                    {
                        model.isAdmin = true;
                    }
                    else
                    {
                        model.isAdmin = false;
                    }

                    string msg = "";
                    string userName = Session["_loginedUserName"].ToString();
                    _personelService.Update2(model,userName,out msg);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        public ActionResult Delete(int id)
        {
            string msg = "";
            var model = _personelService.GetSingle(id);
            if (model == null)
                return HttpNotFound();
            else
                _personelService.Delete(model.ID, out msg);
            return RedirectToAction("Index");
        }


    }
}