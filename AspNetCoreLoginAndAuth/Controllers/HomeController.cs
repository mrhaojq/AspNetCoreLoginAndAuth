using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreLoginAndAuth.Controllers
{
    public class HomeController : FonourControllerBase
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            //throw new Exception("测试系统提供的错误处理机制");
            return View();
        }
    }
}
