using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoaringApi.Models
{
    public class SideBar
    {
        [Key]
        public int SideBarId { get; set; }
        public string Content { get; set; }
    }
}
