﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreLoginAndAuth.Services.UserApp;
using AspNetCoreLoginAndAuth.Models;


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
            var user = _userAppService.CheckUser("admin", "123456");
            return View();
        }
    }
}
