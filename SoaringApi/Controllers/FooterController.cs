using Microsoft.AspNetCore.Mvc;
using SoaringApi.Data;
using SoaringApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoaringApi.Controllers
{
    [Route("[controller]")]
    public class FooterController : Controller
    {
        private readonly SDCMSApiContext _context;

        public FooterController(SDCMSApiContext context)
        {
            _context = context;
        }

        //Get api/footer
        public IActionResult GetFooter()
        {
            Footer footer = _context.tbl_Footer.FirstOrDefault();
            return Json(footer);
        }

        //Post api/footer/edit
        [HttpPut("edit")]
        public IActionResult EditFooter([FromBody] Footer footer)
        {
            _context.Update(footer);
            _context.SaveChanges();

            return Json("Sidebar Editted");
        }
    }
}
