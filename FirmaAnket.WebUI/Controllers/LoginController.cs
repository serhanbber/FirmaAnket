using FirmaAnket.Business.Services;
using FirmaAnket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FirmaAnket.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly PersonelService _personelService;
        public LoginController()
        {
            _personelService = new PersonelService();
        }
        // GET: Login
        public ActionResult SignIn(PersonelDTO model)
        {
            //if (kullanici.Code==null)
            //{
            //    return View();
            //}
            //else
            //{
            //    var kul = _personelService.Login(kullanici);
            //    if (kul != null)
            //    {
            //        Session["Code"] = kul.Code;
            //        Session["NameSurname"] = kul.NameSurname;

            //        return RedirectToAction("Index", "Personal");
            //    }
            //    else
            //    {
            //        ViewBag.Error = "Kullanıcı Kodu Yada şifre hatalı";
            //        return View();
            //    }
            //}

            if (model.Code == null)
            {
                return View();
            }
            else
            {
                var loginedUser = _personelService.Login(model);
                if (loginedUser != null)
                {
                    FormsAuthentication.SetAuthCookie(loginedUser.Code, true);
                    Session["_loginedUser"] = loginedUser;
                    Session["_loginedUserCode"] = loginedUser.Code;
                    Session["_loginedUserName"] = loginedUser.NameSurname;

                    if (loginedUser.isAdmin==true)
                    {
                        Session["Admin"] = "Admin";
                    }

                    //model.CreatedBy = loginedUser.NameSurname;
                    
                    return RedirectToAction("Create", "Answer");
                }
                else
                {
                    ViewBag.Error = "Kullanıcı Kodu Yada şifre hatalı";
                    return View();
                }
            }
           
            
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("SignIn","Login");
        }
    }
}