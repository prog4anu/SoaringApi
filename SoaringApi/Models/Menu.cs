using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoaringApi.Models
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }
        public string MenuName { get; set; }        
        public int MenuPageId { get; set; }
        public bool ShowMenu { get; set; }
        public bool ShowSubMenu { get; set; }
    }

    public class SubMenu
    {
        [Key]
        public int SubMenuID { get; set; }
        public string SubMenuName { get; set; }
        public int ParentMenuId { get; set; }
        public int SubMenuPageId { get; set; }
        public bool ShowSubMenu { get; set; }
    }
}
