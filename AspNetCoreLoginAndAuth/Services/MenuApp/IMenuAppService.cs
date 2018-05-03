using AspNetCoreLoginAndAuth.Services.MenuApp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreLoginAndAuth.Services.MenuApp
{
   public interface IMenuAppService
    {
        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <returns></returns>
        List<MenuDto> GetAllList();

        /// <summary>
        /// 根据父级ID获取功能列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="startPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        List<MenuDto> GetMenusByParent(Guid parentId,int startPage,int pageSize,out int rowCount);

        /// <summary>
        /// 新增或修改功能
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool InsertOrUpdate(MenuDto dto);
        
        /// <summary>
        /// 根据ID集合批量删除
        /// </summary>
        /// <param name="ids"></param>
        void DeleteBatch(List<Guid> ids);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        void Delete(Guid id);

        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MenuDto Get(Guid id);
    }
}
