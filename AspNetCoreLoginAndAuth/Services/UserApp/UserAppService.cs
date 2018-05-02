using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreLoginAndAuth.Models;
using AspNetCoreLoginAndAuth.Data.IRepositories;
using AspNetCoreLoginAndAuth.Data.Repositories;

namespace AspNetCoreLoginAndAuth.Services.UserApp
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;//面向接口编程

        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User CheckUser(string userName, string password)
        {
            return _userRepository.CheckUser(userName, password);
        }
    }
}
