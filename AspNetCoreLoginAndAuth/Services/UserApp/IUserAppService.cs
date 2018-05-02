using AspNetCoreLoginAndAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreLoginAndAuth.Services.UserApp
{
   public interface IUserAppService
    {
        User CheckUser(string userName, string password);
    }
}
