using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreLoginAndAuth.Controllers
{
    public class FonourControllerBase: Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            byte[] result;
            context.HttpContext.Session.TryGetValue("CurrentUser",out result);
            if (result==null)
            {
                context.Result = new RedirectResult("/Login/Index");
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
