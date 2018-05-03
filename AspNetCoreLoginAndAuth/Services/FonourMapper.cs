using AspNetCoreLoginAndAuth.Models;
using AspNetCoreLoginAndAuth.Services.MenuApp.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreLoginAndAuth.Services
{
    /// <summary>
    /// Entity与Dto映射
    /// </summary>
    public class FonourMapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Menu,MenuDto>();
                cfg.CreateMap<MenuDto,Menu>();
            });
        }
    }
}
