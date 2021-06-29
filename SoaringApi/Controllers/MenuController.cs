using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoaringApi.Data;
using SoaringApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SoaringApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MenuController : Controller
    {
        private readonly SDCMSApiContext _context;

        public MenuController(SDCMSApiContext context)
        {
            _context = context;
        }

        //Get api/menus
        public IActionResult Index()
        {
            List<Menu> pages = _context.tbl_Menu.ToList();
            return Json(pages);
        }

        //Get api/menus/menuId
        [HttpGet("GetMenuById/{menuId}")]
        public IActionResult GetMenuById(int menuId)
        {
            Menu menu = _context.tbl_Menu.ToList().Where(x => x.MenuId == menuId).FirstOrDefault();

            if (menu == null)
            {
                return Json("PageNotFound");
            }

            return Json(menu);
        }

        //Get api/menus/menuId
        [HttpGet("{menuId}")]
        public IActionResult GetMenu(int menuId)
        {
            var menu = _context.tbl_SubMenu.ToList().Where(x => x.ParentMenuId == menuId);

            if (menu == null)
            {
                return Json("PageNotFound");
            }

            return Json(menu);
        }

        //Get api/menus/menuId
        [HttpGet("GetSubMenuById/{submenuId}")]
        public IActionResult GetSubMenuById(int submenuId)
        {
            SubMenu menu = _context.tbl_SubMenu.ToList().Where(x => x.SubMenuID == submenuId).FirstOrDefault();

            if (menu == null)
            {
                return Json("PageNotFound");
            }

            return Json(menu);
        }

        //Post api/menu/createmenu
        [HttpPost("createmenu")]
        public IActionResult CreateMenu([FromBody] Menu menu)
        {
            menu.ShowMenu = menu.ShowMenu ? menu.ShowMenu : false;
            menu.ShowSubMenu = menu.ShowSubMenu ? menu.ShowSubMenu : false;

            var slug = _context.tbl_Menu.FirstOrDefault(x => x.MenuName == menu.MenuName);

            if (slug != null)
            {
                return Json("Menu Already Exists");
            }
            else
            {
                _context.tbl_Menu.Add(menu);
                _context.SaveChanges();

                return Json("Menu Added");
            }
        }

        //Post api/menu/createsubmenu
        [HttpPost("createsubmenu")]
        public IActionResult CreateSubMenu([FromBody] SubMenu submenu)
        {
            submenu.ShowSubMenu = submenu.ShowSubMenu ? submenu.ShowSubMenu : false;

            var slug = _context.tbl_SubMenu.FirstOrDefault(x => x.SubMenuName == submenu.SubMenuName && x.ParentMenuId == submenu.ParentMenuId);

            if (slug != null)
            {
                return Json("Sub Menu Already Exists for this Menu");
            }
            else
            {
                _context.tbl_SubMenu.Add(submenu);
                _context.SaveChanges();

                return Json("Sub Menu Added");
            }
        }

        //Post api/menu/edit/menuId
        [HttpPut("edit")]
        public IActionResult EditMainMenu([FromBody] Menu menu)
        {
            menu.ShowMenu = menu.ShowMenu ? menu.ShowMenu : false;
            menu.ShowSubMenu = menu.ShowSubMenu ? menu.ShowSubMenu : false;

            var p = _context.tbl_Menu.FirstOrDefault(x => x.MenuId != menu.MenuId && x.MenuName == menu.MenuName);

            if (p != null)
            {
                return Json("Menu Already Exists");
            }
            else
            {
                _context.Update(menu);
                _context.SaveChanges();

                return Json("Menu Added");
            }
        }

        //Post api/menu/submenu/edit/submenuid
        [HttpPut("editsubmenu")]
        public IActionResult EditSubMenu([FromBody] SubMenu submenu)
        {
            submenu.ShowSubMenu = submenu.ShowSubMenu ? submenu.ShowSubMenu : false;

            var p = _context.tbl_SubMenu.FirstOrDefault(x => x.SubMenuID != submenu.SubMenuID && x.SubMenuName == submenu.SubMenuName);

            if (p != null)
            {
                return Json("Sub Menu Already Exists");
            }
            else
            {
                _context.Update(submenu);
                _context.SaveChanges();

                return Json("Sub Menu Added");
            }
        }

        //Post api/menu/deletemenu/menuId
        [HttpDelete("deletemenu/{menuId}")]
        public IActionResult DeleteMenu(int menuId)
        {
            SubMenu submenu = _context.tbl_SubMenu.SingleOrDefault(x => x.ParentMenuId == menuId);
            if (submenu != null)
            {
                _context.Remove(submenu);
                _context.SaveChanges();
            }
           

            Menu menu = _context.tbl_Menu.SingleOrDefault(x => x.MenuId == menuId);
            _context.Remove(menu);
            _context.SaveChanges();

            return Json("Page Deleted");
        }

        //Post api/menu/deletesubmenu/submenuId
        [HttpDelete("deletesubmenu/{submenuId}")]
        public IActionResult DeleteSubMenu(int submenuId)
        {
            SubMenu submenu = _context.tbl_SubMenu.SingleOrDefault(x => x.SubMenuID == submenuId);
            _context.Remove(submenu);
            _context.SaveChanges();

            return Json("Page Deleted");
        }

    }
}
