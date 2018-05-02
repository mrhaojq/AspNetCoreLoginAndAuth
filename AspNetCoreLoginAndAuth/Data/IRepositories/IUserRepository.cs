using AspNetCoreLoginAndAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreLoginAndAuth.Data.IRepositories
{
  public  interface IUserRepository:IRepository<User>
    {
        //IUserRepository=>IRepository<TEntity>=> IRepository<TEntity, TPrimaryKey>
        //继承关系，因此IUserRepository中拥有其父类的父类IRepository<TEntity, TPrimaryKey>中定义的所有方法定义（需要在具体类中实现接口定义的方法）

        User CheckUser(string userName, string password);
    }
}
