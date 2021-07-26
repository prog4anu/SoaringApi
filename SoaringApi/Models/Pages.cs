using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoaringApi.Models
{
    public class Pages
    {
        [Key]
        public int PageId { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public string PageLink { get; set; }
        public bool HasSideBar { get; set; }
    }
}
