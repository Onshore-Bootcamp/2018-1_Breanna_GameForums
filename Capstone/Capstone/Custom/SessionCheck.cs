using System;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Custom
{
    public class SessionCheck : ActionFilterAttribute
    {
        public SessionCheck(params int[] Roles)
        {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
                HttpSessionStateBase session = filterContext.HttpContext.Session;
                int sessionRole = 0;
                if (session["RoleID"] != null)
                {
                    int.TryParse(session["RoleID"].ToString(), out sessionRole);
                }
                if (sessionRole == 0)
                {
                    filterContext.Result = new RedirectResult("/Account/Login", false);
                }
                base.OnActionExecuting(filterContext);
        }
    }
}