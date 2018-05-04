using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreLoginAndAuth.Services.MenuApp;
using AspNetCoreLoginAndAuth.Models.ViewModels;
using AspNetCoreLoginAndAuth.Services.MenuApp.Dtos;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreLoginAndAuth.Controllers
{
    public class MenuController : FonourControllerBase
    {

        private readonly IMenuAppService _menuAppService;

        public MenuController(IMenuAppService menuAppService)
        {
            _menuAppService = menuAppService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetMenuTreeData()
        {
            var menus = _menuAppService.GetAllList();
            List<TreeModel> treeModels = new List<TreeModel>();
            foreach (var menu in menus)
            {
                treeModels.Add(new TreeModel() { Id = menu.Id.ToString(), Text = menu.Name, Parent = menu.ParentId == Guid.Empty ? "#" : menu.ParentId.ToString() });
            }
            return Json(treeModels);
        }

        //[HttpGet]
        public IActionResult GetMenusByParent(Guid parentId, int startPage, int pageSize)
        {
            int rowCount = 0;
            var result = _menuAppService.GetMenusByParent(parentId, startPage, pageSize, out rowCount);
            return Json(new
            {
                rowCount = rowCount,
                pageCount = Math.Ceiling(Convert.ToDecimal(rowCount) /pageSize),
                rows = result
            });
        }

        public IActionResult Get(Guid id)
        {
            var dto = _menuAppService.Get(id);
            return Json(dto);
        }

        public IActionResult Edit(MenuDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new {
                    Result="Faild",
                    Message=GetModelStateError()
                });
            }

            if (_menuAppService.InsertOrUpdate(dto))
            {
                return Json(new { Result="Success"});
            }

            return Json(new { Result="Faild"});
        }

        public IActionResult Delete(Guid id)
        {
            try
            {
                _menuAppService.Delete(id);
                return Json(new { Result="Success"});
            }
            catch (Exception ex)
            {
                return Json(new {
                    Result="Faild",
                    Message=ex.Message
                });
            }
        }

        public IActionResult DeleteMulti(string ids)
        {
            try
            {
                string[] idArray = ids.Split(',');
                List<Guid> delIds = new List<Guid>();
                foreach (var id in idArray)
                {
                    delIds.Add(Guid.Parse(id));
                }

                _menuAppService.DeleteBatch(delIds);
                return Json(new { Result = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Result = "Faild",
                    Message = ex.Message
                });
            }
        }
    }
}
