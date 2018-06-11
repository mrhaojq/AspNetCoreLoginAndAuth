using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreLoginAndAuth.Data.IRepositories;
using AspNetCoreLoginAndAuth.Models;
using AspNetCoreLoginAndAuth.Services.RoleApp.Dtos;
using AutoMapper;

namespace AspNetCoreLoginAndAuth.Services.RoleApp
{
    public class RoleAppService:IRoleAppService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleAppService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public void Delete(Guid id)
        {
            _roleRepository.Delete(id);
        }

        public void DeleteBatch(List<Guid> ids)
        {
            _roleRepository.Delete(it => ids.Contains(it.Id));
        }

        public RoleDto Get(Guid id)
        {
            return Mapper.Map<RoleDto>(_roleRepository.Get(id));
        }

        public List<RoleDto> GetAllList()
        {
            return Mapper.Map<List<RoleDto>>(_roleRepository.GetAllList().OrderBy(it=>it.Code));
        }


        public List<RoleDto> GetAllPageList(int startPage, int pageSize, out int rowCount)
        {
            return Mapper.Map<List<RoleDto>>(_roleRepository.LoadPageList(startPage,pageSize,out rowCount,null,it=>it.Code));
        }

        public bool InsertOrUpdate(RoleDto dto)
        {
            var menu = _roleRepository.InsertOrUpdate(Mapper.Map<Role>(dto));
            return menu == null ? false : true;
        }

        public List<Guid> GetAllMenuListByRole(Guid roleId)
        {
            return _roleRepository.GetAllMenuListByRole(roleId);
        }

        public bool UpdateRoleMenu(Guid roleId, List<RoleMenuDto> roleMenus)
        {
            return _roleRepository.UpdateRoleMenu(roleId,Mapper.Map<List<RoleMenu>>(roleMenus));
        }
    }
}
