using Microsoft.AspNetCore.Mvc;
using SoaringApi.Data;
using SoaringApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoaringApi.Controllers
{
    [Route("api/[controller]")]
    public class SideBarController : Controller
    {
        private readonly SDCMSApiContext _context;

        public SideBarController(SDCMSApiContext context)
        {
            _context = context;
        }

        //Get api/sidebar
        public IActionResult GetSideBar()
        {
            SideBar sidebar = _context.tbl_SideBar.FirstOrDefault();
            return Json(sidebar);
        }

        //Post api/sidebar/edit
        [HttpPut("edit")]
        public IActionResult Edit([FromBody]SideBar sidebar)
        {
            _context.Update(sidebar);
            _context.SaveChanges();

            return Json("Sidebar Editted");
        }
    }
}
