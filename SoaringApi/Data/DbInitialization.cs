using SoaringApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoaringApi.Data
{
    public class DbInitialization
    {
        public static void Init(SDCMSApiContext context)
        {
            context.Database.EnsureCreated();

            if (context.tbl_Pages.Any())
            {
                return;
            }

            var pages = new Pages[]
            {
                new Pages{ Title = "Home",Slug="home",Content="Home Content",HasSideBar=false},
                new Pages{ Title = "About",Slug="about",Content="About Content",HasSideBar=false},
                new Pages{ Title = "Services",Slug="services",Content="Services Content",HasSideBar=false},
                new Pages{ Title = "Contact",Slug="contact",Content="Contact Content",HasSideBar=false}
            };

            foreach (Pages page in pages)
            {
                context.tbl_Pages.Add(page);
            }

            context.SaveChanges();

            var sidebar = new SideBar
            {
                Content = "Sidebar Content"
            };

            context.tbl_SideBar.Add(sidebar);

            context.SaveChanges();

            var user = new User
            {
                UserName = "Admin",
                Password = "test",
                IsAdmin = false
            };

            context.tbl_Users.Add(user);

            context.SaveChanges();
        }
    }
}
