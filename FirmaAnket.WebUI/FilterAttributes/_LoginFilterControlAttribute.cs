
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirmaAnket.WebUI.FilterAttributes
{
    public class _LoginFilterControlAttribute : ActionFilterAttribute
    {
      
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            if (HttpContext.Current.Session["_loginedUserCode"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "action", "SignIn" }, { "controller", "Login" } });
            }
           
        }
    }
}