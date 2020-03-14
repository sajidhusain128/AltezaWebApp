using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltezaWebApp.Models
{
    public class MenuModel
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuURL { get; set; }
        public int ParentId { get; set; }
        public string ParentMenuName { get; set; }
        public bool IsActiveWeb { get; set; }

    }
}
