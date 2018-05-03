using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AspNetCoreLoginAndAuth.Data.IRepositories;
using AspNetCoreLoginAndAuth.Models;

namespace AspNetCoreLoginAndAuth.Data.Repositories
{
    public abstract class FonourRepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        //定义数据库访问上下文对象
        protected readonly AspNetCoreLoginAndAuthDbContext _dbContext;

        /// <summary>
        /// 通过构造函数注入得到数据上下文对象实例
        /// </summary>
        /// <param name="dbcontext"></param>
        public FonourRepositoryBase(AspNetCoreLoginAndAuthDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        public List<TEntity> GetAllList()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        /// <summary>
        /// 根据lambda表达式条件获取实体集合
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).ToList();
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public TEntity Get(TPrimaryKey id)
        {
            //return _dbContext.Set<TEntity>().FirstOrDefault(e=>e.Id==id); TPrimaryKey 泛型无法用于==操作
            return _dbContext.Set<TEntity>().FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Insert(TEntity entity, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Add(entity);
            if (autoSave)
            {
                Save();
            }
            return entity;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Update(TEntity entity,bool autoSave=true)
        {
            //_dbContext.Set<TEntity>().Attach(entity);
            //_dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //return entity;

            var obj = Get(entity.Id);
            EntityToEntity(entity,obj);
            if (autoSave)
            { Save(); }
            return entity;
        }

        private void EntityToEntity<T>(T pTargetObjSrc, T pTargetObjDest)
        {
            foreach (var item in typeof(T).GetProperties())
            {
                item.SetValue(pTargetObjDest,item.GetValue(pTargetObjSrc,new object[] { }),null);
            }
        }

        /// <summary>
        /// 新增或更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity InsertOrUpdate(TEntity entity,bool autoSave=true)
        {
            if (Get(entity.Id) != null)
            {
                return Update(entity,autoSave);
            }
            return Insert(entity,autoSave);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void Delete(TEntity entity,bool autoSave=true)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            if (autoSave)
            {
                Save();
            }
        }

        public void Delete(TPrimaryKey id,bool autoSave=true)
        {
            _dbContext.Set<TEntity>().Remove(Get(id));
            if (autoSave)
            {
                Save();
            }
        }

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="where">lambda表达式条件</param>
        /// <param name="autoSave">是否自动保存</param>
        public void Delete(Expression<Func<TEntity,bool>> where,bool autoSave=true)
        {
            _dbContext.Set<TEntity>().Where(where).ToList().ForEach(it=>_dbContext.Set<TEntity>().Remove(it));
            if (autoSave)
            {
                Save();
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="startPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public IQueryable<TEntity> LoadPageList(int startPage,int pageSize,out int rowCount,Expression<Func<TEntity,bool>> where=null,Expression<Func<TEntity,object>> order=null)
        {
            var result = from p in _dbContext.Set<TEntity>()
                         select p;

            if (where!=null)
            {
                result = result.Where(where);
            }
               
            if (order!=null)
            {
                result = result.OrderBy(order);
            }
            else
            {
                result = result.OrderBy(m=>m.Id);
            }

            rowCount = result.Count();
            return result.Skip((startPage - 1) * pageSize).Take(pageSize);
        }


        /// <summary>
        /// 事务性保存
        /// </summary>
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// 根据主键构建判断表达式
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));
            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
       
    }

    public abstract class FonourRepositoryBase<TEntity>:FonourRepositoryBase<TEntity,Guid> where TEntity:Entity
    {
        public FonourRepositoryBase(AspNetCoreLoginAndAuthDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
