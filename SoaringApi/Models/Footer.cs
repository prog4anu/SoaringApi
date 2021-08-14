using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoaringApi.Models
{
    public class Footer
    {
        [Key]
        public int FooterId { get; set; }
        public string Content { get; set; }
        public string SiteName { get; set; }
        public string BgColor { get; set; }
    }
}
