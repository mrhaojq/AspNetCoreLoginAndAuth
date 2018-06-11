using AspNetCoreLoginAndAuth.Data.IRepositories;
using AspNetCoreLoginAndAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspNetCoreLoginAndAuth.Data.Repositories
{
    public class RoleRepository : FonourRepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(AspNetCoreLoginAndAuthDbContext dbContext)
            :base(dbContext)
        {

        }

        public List<Guid> GetAllMenuListByRole(Guid roleId)
        {
            var roleMenus = _dbContext.Set<RoleMenu>().Where(it => it.RoleId == roleId);
            var menuIds = from t in roleMenus
                          select t.MenuId;

            return menuIds.ToList();
        }

        public bool UpdateRoleMenu(Guid roleId, List<RoleMenu> roleMenus)
        {
            var oldDatas = _dbContext.Set<RoleMenu>().Where(it => it.RoleId == roleId).ToList();
            oldDatas.ForEach(it=>_dbContext.Set<RoleMenu>().Remove(it));
            _dbContext.SaveChanges();
            _dbContext.Set<RoleMenu>().AddRange(roleMenus);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
