using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreLoginAndAuth.Services.UserApp;
using AspNetCoreLoginAndAuth.Models;
using AspNetCoreLoginAndAuth.Models.ViewModels;
using AspNetCoreLoginAndAuth.Utility;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreLoginAndAuth.Controllers
{
    public class LoginController : Controller
    {
        private IUserAppService _userAppService;

        public LoginController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
           // var user = _userAppService.CheckUser("admin", "123456");
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userAppService.CheckUser(model.UserName,model.Password);
                if (user!=null)
                {
                    //记录Session
                    HttpContext.Session.Set("CurrentUser",ByteConvertHelper.ObjectToBytes(user));
                    //跳转到系统首页
                    return RedirectToAction(nameof(Index),"Home");
                }
                ViewBag.ErrorInfo = "用户名或密码错误。";
                return View();
            }
            //ModelState.AddModelError("", "用户名或密码错误。");
            ViewBag.ErrorInfo = "用户名或密码错误";
            return View(model);
        }
    
    }
}
