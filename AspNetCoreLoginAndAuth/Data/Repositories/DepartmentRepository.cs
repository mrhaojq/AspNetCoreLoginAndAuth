using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreLoginAndAuth.Models;
using AspNetCoreLoginAndAuth.Data.IRepositories;

namespace AspNetCoreLoginAndAuth.Data.Repositories
{
    public class DepartmentRepository:FonourRepositoryBase<Department>,IDepartmentRepository
    {
        public DepartmentRepository(AspNetCoreLoginAndAuthDbContext dbContext)
            :base(dbContext)
        {

        }
    }
}
