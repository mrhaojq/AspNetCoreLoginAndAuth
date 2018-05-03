using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreLoginAndAuth.Data.IRepositories;
using AspNetCoreLoginAndAuth.Models;
using AspNetCoreLoginAndAuth.Services.MenuApp.Dtos;
using AutoMapper;

namespace AspNetCoreLoginAndAuth.Services.MenuApp
{
    public class MenuAppService : IMenuAppService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuAppService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public List<MenuDto> GetAllList()
        {
            var menus = _menuRepository.GetAllList().OrderBy(it => it.SerialNumber);
            return Mapper.Map<List<MenuDto>>(menus);
        }

        public List<MenuDto> GetMenusByParent(Guid parentId, int startPage, int pageSize, out int rowCount)
        {
            var menus = _menuRepository.LoadPageList(startPage, pageSize, out rowCount, it => it.ParentId == parentId, it => it.SerialNumber);
            return Mapper.Map<List<MenuDto>>(menus);
        }

        public bool InsertOrUpdate(MenuDto dto)
        {
            var menu = _menuRepository.InsertOrUpdate(Mapper.Map<Menu>(dto));
            return menu == null ? false : true;
        }

        public void DeleteBatch(List<Guid> ids)
        {
            _menuRepository.Delete(it => ids.Contains(it.Id));
            //void Delete(Expression<Func<TEntity, bool>> where, bool autoSave = true);
        }

        public void Delete(Guid id)
        {
            _menuRepository.Delete(id);
        }

        public MenuDto Get(Guid id)
        {
            return Mapper.Map<MenuDto>(_menuRepository.Get(id));
        }

    }
}
