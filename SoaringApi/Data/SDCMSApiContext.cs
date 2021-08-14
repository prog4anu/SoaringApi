using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoaringApi.Models;

namespace SoaringApi.Data
{
    public class SDCMSApiContext:DbContext
    {
        public SDCMSApiContext(DbContextOptions<SDCMSApiContext> options) : base(options)
        {

        }

        public DbSet<Pages> tbl_Pages { get; set; }
        public DbSet<SideBar> tbl_SideBar { get; set; }
        public DbSet<User> tbl_Users { get; set; }
        public DbSet<Menu> tbl_Menu { get; set; }
        public DbSet<SubMenu> tbl_SubMenu { get; set; }
        public DbSet<Footer> tbl_Footer { get; set; }
    }
}
