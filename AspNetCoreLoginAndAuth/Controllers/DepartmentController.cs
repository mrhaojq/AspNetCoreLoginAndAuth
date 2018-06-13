using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreLoginAndAuth.Services.DepartmentApp;
using AspNetCoreLoginAndAuth.Models.ViewModels;
using AspNetCoreLoginAndAuth.Services.DepartmentApp.Dtos;

namespace AspNetCoreLoginAndAuth.Controllers
{
    public class DepartmentController : FonourControllerBase
    {
        private readonly IDepartmentAppService _departmentAppService;

        public DepartmentController(IDepartmentAppService departmentAppService)
        {
            _departmentAppService = departmentAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetTreeData()
        {
            var dtos = _departmentAppService.GetAllList();
            List<TreeModel> treeModels = new List<TreeModel>();
            foreach (var dto in dtos)
            {
                treeModels.Add(new TreeModel() { Id = dto.Id.ToString(), Text = dto.Name, Parent = dto.ParentId == Guid.Empty ? "#" : dto.ParentId.ToString() });
            }
            return Json(treeModels);
        }

        [HttpGet]
        public IActionResult GetChildrenByParent(Guid parentId, int startPage, int pageSize)
        {
            int rowCount = 0;
            var result = _departmentAppService.GetChildrenByParent(parentId, startPage, pageSize, out rowCount);
            return Json(new
            {
                rowCount = rowCount,
                pageSize = Math.Ceiling(Convert.ToDecimal(rowCount) / pageSize),
                rows = result
            });
        }

        [HttpGet]
        public IActionResult Get(Guid id)
        {
            var dto = _departmentAppService.Get(id);
            return Json(dto);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new
                    {
                        Result = "Faild",
                        Message = base.GetModelStateError()
                    });
                }

                if (_departmentAppService.InsertOrUpdate(dto))
                {
                    return Json(new { Result = "success" });
                }

                return Json(new { Result = "Faild" });
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

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _departmentAppService.Delete(id);
                return Json(new { Result = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { result = "faild", Message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DeleteMuti(string ids)
        {
            try
            {
                string[] idArray = ids.Split(',');
                List<Guid> delIds = new List<Guid>();
                foreach (string  id in idArray)
                {
                    delIds.Add(Guid.Parse(id));
                }
                _departmentAppService.DeleteBatch(delIds);
                return Json(new { Result = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "faild", Message = ex.Message });
            }
        }
    }
}