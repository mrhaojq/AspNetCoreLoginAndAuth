using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreLoginAndAuth.Services.MenuApp.Dtos
{
    public class MenuDto
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public int SerialNumber { get; set; }
        [Required(ErrorMessage = "功能名称不能为空。")]
        public string Name { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public int Type { get; set; }
        public string Icon { get; set; }
        public string Remarks { get; set; }
    }
}
