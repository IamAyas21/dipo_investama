using DIPO_INVESTAMA.App_Start;
using DIPO_INVESTAMA.Entity;
using DIPO_INVESTAMA.Logic;
using DIPO_INVESTAMA.Models;
using DIPO_INVESTAMA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DIPO_INVESTAMA.Controllers
{
    public class MenuRestrictionsController : Controller
    {
        DIPO_INVESTAMAEntities _db = new DIPO_INVESTAMAEntities();
        // GET: MenuRestrictions
        [CheckAuthorize(Roles = "Menu Restriction")]
        public ActionResult Index()
        {
            return View(MenuRestrictionsBusinessLogic.getInstance().ListMenuRestriction());
        }
        [CheckAuthorize(Roles = "Menu Restriction")]
        public ActionResult Create()
        {
            PrivilegeViewModels model = new PrivilegeViewModels();
            model.Parent = null;
            model.Menu = MenuList("0", SessionManager.userId());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PrivilegeViewModels Privilege)
        {
            //check data in database if already exist skip
            var CheckData = _db.Roles.Where(x => x.RoleName == Privilege.Parent.RoleName).ToList();
            if (CheckData.Count() == 0)
            {
                if (ModelState.IsValid)
                {
                    MenuRestrictionsBusinessLogic.getInstance().InsertRole(Privilege.Parent.RoleName);
                    var privilegeId = _db.Roles.Where(a => a.RoleName == Privilege.Parent.RoleName).Single().RoleId;
                    MenuRestrictionsBusinessLogic.getInstance().SaveMenu(Privilege.Menu, privilegeId);
                    TempData["Success"] = "Success saving Data for " + Privilege.Parent.RoleName;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Error"] = "Name :" + Privilege.Parent.RoleName + " already exist!";
            }

            return View(Privilege);
        }
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrivilegeViewModels model = new PrivilegeViewModels();
            model.Parent = _db.Roles.Find(id);
            model.Menu = MenuEdit("0", id, SessionManager.userId());

            if (model.Parent == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PrivilegeViewModels Privilege)
        {
            if (Privilege.Parent.RoleName == null)
            {
                TempData["Error"] = "Name :" + Privilege.Parent.RoleName + " not exist!";
            }
            //check data in database if already exist skip
            var CheckData = _db.Roles.Where(x => x.RoleName == Privilege.Parent.RoleName && x.RoleId != Privilege.Parent.RoleId).ToList();
            if (CheckData.Count() == 0)
            {
                if (ModelState.IsValid)
                {
                    MenuRestrictionsBusinessLogic.getInstance().UpdateRole(Privilege.Parent.RoleId, Privilege.Parent.RoleName);
                    var dt = (from c in _db.MenuRestrictions
                              where c.RoleId == Privilege.Parent.RoleId
                              select c).ToList();
                    foreach (var item in dt)
                    {
                        MenuRestriction menuRestriction = new MenuRestriction();
                        menuRestriction.RestrictionId = item.RestrictionId;
                        menuRestriction.MenuId = item.RestrictionId;
                        menuRestriction.RoleId = item.RoleId;
                        menuRestriction.IsRead = item.IsRead;
                        MenuRestrictionsBusinessLogic.getInstance().UpdateMenuRestriction(menuRestriction);
                    }
                    MenuRestrictionsBusinessLogic.getInstance().SaveMenu(Privilege.Menu, Privilege.Parent.RoleId);
                    TempData["Success"] = "Success Editing Data for " + Privilege.Parent.RoleName;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Error"] = "Name :" + Privilege.Parent.RoleName + " already exist!";
            }

            return View(Privilege);
        }
        public List<MenuViewModels> MenuEdit(string parentId, string privilegeId, string userId)
        {
            List<MenuViewModels> listViewModel = new List<MenuViewModels>();
            List<Menu> listMenu = new List<Menu>();
            listMenu = MenuRestrictionsBusinessLogic.getInstance().GetPrivilegeTree(userId, privilegeId, parentId);
            foreach (Menu itemMenu in listMenu)
            {
                MenuRestrictionsController MeMC = new MenuRestrictionsController();
                MenuViewModels Menu = new MenuViewModels();
                Menu.Parent = itemMenu;
                Menu.Checked = Convert.ToBoolean(itemMenu.IsRead);
                Menu.Child = MeMC.MenuEdit(itemMenu.MenuId, privilegeId, userId);
                listViewModel.Add(Menu);
            }

            return listViewModel;
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