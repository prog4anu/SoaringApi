using Microsoft.AspNetCore.Mvc;
using SoaringApi.Data;
using SoaringApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace SoaringApi.Controllers
{
    [Route("[controller]")]
    public class PagesController : Controller
    {
        private readonly SDCMSApiContext _context;

        public PagesController(SDCMSApiContext context)
        {
            _context = context;
        }

        //Get api/pages
        public IActionResult Index()
        {
            List<Pages> pages = _context.tbl_Pages.ToList();
            return Json(pages);
        }

        //Get api/pages/slug
        [HttpGet("{slug}")]
        public IActionResult Index(string slug)
        {
            var page = _context.tbl_Pages.SingleOrDefault(x => x.PageLink.ToLower() == slug.ToLower());

            if (page == null)
            {
                page = _context.tbl_Pages.SingleOrDefault(x => x.Slug == slug);
            }

            if (page == null)
            {
                return Json("PageNotFound");
            }

            return Json(page);
        }

        //Get api/pages/ediit/id
        [HttpGet("edit/{pageId}")]
        public IActionResult Edit(int pageId)
        {
            var page = _context.tbl_Pages.SingleOrDefault(x => x.PageId == pageId);

            if (page == null)
            {
                return Json("PageNotFound");
            }

            return Json(page);
        }

        //Post api/pages/create
        [HttpPost("create")]
        public IActionResult Create([FromBody]Pages page)
        {
            page.Slug = page.Title.Replace(" ", "-").ToLower();
            page.HasSideBar = page.HasSideBar ? page.HasSideBar : false;

            var slug = _context.tbl_Pages.FirstOrDefault(x => x.Slug == page.Slug);

            if (slug != null)
            {
                return Json("Page Already Exists");
            }
            else
            {
                page.Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(page.Title);

                if (string.IsNullOrEmpty(page.PageLink))
                {
                    page.PageLink = page.Slug.ToLower().Replace(" ", "-");
                }

                if (string.IsNullOrEmpty(page.MetaTitle))
                {
                    page.MetaTitle = page.Title;
                }

                _context.tbl_Pages.Add(page);
                _context.SaveChanges();

                return Json("Page Added");
            }
        }

        //Post api/pages/edit/pageId
        [HttpPut("edit/{pageId}")]
        public IActionResult Edit(int pageId, [FromBody] Pages page)
        {
            page.Slug = page.Title.Replace(" ", "-").ToLower();
            page.HasSideBar = page.HasSideBar ? page.HasSideBar : false;

            var p = _context.tbl_Pages.FirstOrDefault(x => x.PageId != pageId && x.Slug == page.Slug);

            if (p != null)
            {
                return Json("Page Already Exists");
            }
            else
            {
                page.Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(page.Title);

                if (string.IsNullOrEmpty(page.MetaTitle))
                {
                    page.MetaTitle = page.Title;
                }

                if (string.IsNullOrEmpty(page.PageLink))
                {
                    page.PageLink = page.Slug.ToLower().Replace(" ", "-");
                }

                _context.Update(page);
                _context.SaveChanges();

                return Json("Page Added");
            }
        }

        //Post api/pages/delete/pageId
        [HttpDelete("delete/{pageId}")]
        public IActionResult Delete(int pageId)
        {
            Pages page = _context.tbl_Pages.SingleOrDefault(x => x.PageId == pageId);
            _context.Remove(page);
            _context.SaveChanges();

            return Json("Page Deleted");
        }
    }
}
