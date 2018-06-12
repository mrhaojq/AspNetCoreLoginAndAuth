using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreLoginAndAuth.Data.IRepositories;
using AspNetCoreLoginAndAuth.Models;
using AspNetCoreLoginAndAuth.Services.DepartmentApp.Dtos;
using AutoMapper;

namespace AspNetCoreLoginAndAuth.Services.DepartmentApp
{
    public class DepartmentAppService:IDepartmentAppService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentAppService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public List<DepartmentDto> GetAllList()
        {
            var dtos = _departmentRepository.GetAllList(it=>it.Id!=Guid.Empty).OrderBy(it=>it.Code);
            return Mapper.Map<List<DepartmentDto>>(dtos);
        }

        public List<DepartmentDto> GetChildrenByParent(Guid parentId, int startPage, int pageSize, out int rowCount)
        {
            var dtos = _departmentRepository.LoadPageList(startPage, pageSize, out rowCount, it => it.ParentId == parentId, it => it.Code);
            return Mapper.Map<List<DepartmentDto>>(dtos);
        }

        public DepartmentDto Get(Guid id)
        {
            var dto = _departmentRepository.Get(id);
            return Mapper.Map<DepartmentDto>(dto);
        }

        public bool InsertOrUpdate(DepartmentDto dto)
        {
            var department = _departmentRepository.InsertOrUpdate(Mapper.Map<Department>(dto));
            return department == null ? false : true;
        }
    }
}
