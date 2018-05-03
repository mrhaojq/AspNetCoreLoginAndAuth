using AspNetCoreLoginAndAuth.Data.IRepositories;
using AspNetCoreLoginAndAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreLoginAndAuth.Data.Repositories
{
    public class MenuRepository:FonourRepositoryBase<Menu>, IMenuRepository
    {
        //父类实现接口 IRepository 定义的通用方法
        //继承FonourRepositoryBase<Menu>持有父类的所有通用方法
        //实现IMenuRepository 添加Menu相关的方法

        public MenuRepository(AspNetCoreLoginAndAuthDbContext dbContext)
            :base(dbContext)
        {

        }
    }
}
