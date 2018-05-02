using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreLoginAndAuth.Models
{
    public class Menu:Entity
    {
        public Guid ParentId { get; set; }
        public int SerialNumber { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        /// <summary>
        /// 0导航类型 1操作按钮
        /// </summary>
        public int Type { get; set; }
        public string Icon { get; set; }
        public string Remarks { get; set; }
    }
}
