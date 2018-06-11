using AspNetCoreLoginAndAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreLoginAndAuth.Data.IRepositories
{
 public   interface IRoleRepository: IRepository<Role>
    {
        /// <summary>
        /// 根据角色获取权限
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        List<Guid> GetAllMenuListByRole(Guid roleId);

        /// <summary>
        /// 更加角色权限关联关系
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="roleMenus">角色权限集合</param>
        /// <returns></returns>
        bool UpdateRoleMenu(Guid roleId, List<RoleMenu> roleMenus);
    }
}
