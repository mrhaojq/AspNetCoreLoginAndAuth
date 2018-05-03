using AspNetCoreLoginAndAuth.Data.IRepositories;
using AspNetCoreLoginAndAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreLoginAndAuth.Data.Repositories
{
    public class UserRepository:FonourRepositoryBase<User>,IUserRepository
    {
        //父类实现接口 IRepository 定义的通用方法
        //FonourRepositoryBase<User> 继承父类 拥有通用方法 
        //实现IUserRepository接口 添加User的专有方法


        public UserRepository(AspNetCoreLoginAndAuthDbContext dbContext)
            : base(dbContext)
        {

        }

        public User CheckUser(string userName, string password)
        {
            return _dbContext.Set<User>().FirstOrDefault(it=>it.UserName==userName&&it.Password==password);
        }
    }
}
