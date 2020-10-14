using DIPO_INVESTAMA.Entity;
using DIPO_INVESTAMA.Logic;
using DIPO_INVESTAMA.Models;
using DIPO_INVESTAMA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIPO_INVESTAMA.Controllers
{
    public class MenuRestrictionsController : Controller
    {
        DIPO_INVESTAMAEntities _db = new DIPO_INVESTAMAEntities();
        // GET: MenuRestrictions
        public ActionResult Index()
        {
            return View(MenuRestrictionsBusinessLogic.getInstance().ListMenuRestriction());
        }

        public ActionResult Create()
        {
            PrivilegeViewModels model = new PrivilegeViewModels();
            model.Parent = null;
            model.Menu = MenuList("0", SessionManager.userId());
            return View(model);
        }

        public List<MenuViewModels> MenuList(string parentId, string userId)
        {

            List<MenuViewModels> model = new List<MenuViewModels>();
            List<Menu> listMenu = new List<Menu>();
            var menuData = (from c in _db.Menus
                       join b in _db.MenuRestrictions on c.MenuId equals b.MenuId
                       join d in _db.Roles on b.RoleId equals d.RoleId
                       join e in _db.Users on b.RoleId equals e.RoleId
                       where c.ParentId == parentId && e.UserId == userId
                       select new { c, b }).OrderBy(x => x.c.Opt).ToList();

            foreach (var itm in menuData)
            {
                var m = new Menu
                {
                    MenuId = itm.c.MenuId,
                    Action = itm.c.Action,
                    Controller = itm.c.Controller,
                    Descriptions = itm.c.Descriptions,
                    Icon = itm.c.Icon,
                    MenuName = itm.c.MenuName,
                    ParentId = itm.c.ParentId,
                    ShowMenu = itm.c.ShowMenu,
                    Opt = itm.c.Opt
                };
                listMenu.Add(m);
            }

            foreach (Menu itemMenu in listMenu)
            {
                MenuRestrictionsController MeMC = new MenuRestrictionsController();
                MenuViewModels menuViewModels = new MenuViewModels();
                menuViewModels.Checked = false;
                menuViewModels.Parent = itemMenu;
                menuViewModels.Child = MeMC.MenuList(itemMenu.MenuId, userId);
                model.Add(menuViewModels);
            }

            return model;
        }
    }
}