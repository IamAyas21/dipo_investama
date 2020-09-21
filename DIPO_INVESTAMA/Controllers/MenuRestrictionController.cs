using DIPO_INVESTAMA.Entity;
using DIPO_INVESTAMA.Logic;
using DIPO_INVESTAMA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIPO_INVESTAMA.Controllers
{
    public class MenuRestrictionController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            return View(MenuRestrictionBusinessLogic.getInstance().ListMenuRestriction());
        }

        public ActionResult Create()
        {
            ViewBag.RoleList = common.ToSelectList(RoleBusinessLogic.getInstance().getRoleDDL(), "ID", "NAME", string.Empty);
            ViewBag.MenuList = common.ToSelectList(MenuRestrictionBusinessLogic.getInstance().getMenuDDL(""), "ID", "NAME", string.Empty);
            return View();
        }

        [HttpPost]
        public ActionResult Create(sp_MenuRestrictionSelect_Result model)
        {
            if (MenuRestrictionBusinessLogic.getInstance().CreateMenuRestriction(model) == -1)
            {
                TempData["Success"] = "Menu Restriction was successfully inserted";
            }
            else
            {
                TempData["Error"] = "Menu Restriction was unsuccessfully inserted";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            sp_MenuRestrictionSelect_Result model = new sp_MenuRestrictionSelect_Result();
            var menuData = MenuRestrictionBusinessLogic.getInstance().getMenuById(id);
            model.RestrictionId = menuData.RestrictionId;
            model.MenuId = menuData.MenuId;
            model.RoleId = menuData.RoleId;

            ViewBag.RoleList = common.ToSelectList(RoleBusinessLogic.getInstance().getRoleDDL(), "ID", "NAME", model.RoleId);
            ViewBag.MenuList = common.ToSelectList(MenuRestrictionBusinessLogic.getInstance().getMenuDDL(model.RoleId), "ID", "NAME", model.MenuId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(sp_MenuRestrictionSelect_Result model)
        {
            if (MenuRestrictionBusinessLogic.getInstance().UpdateMenuRestriction(model) == -1)
            {
                TempData["Success"] = "Menu Restriction was successfully updated";
            }
            else
            {
                TempData["Error"] = "Menu Restriction was unsuccessfully updated";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (MenuRestrictionBusinessLogic.getInstance().DeleteMenuRestriction(id) == -1)
            {
                TempData["Success"] = "Menu Restriction was successfully deleted";
            }
            else
            {
                TempData["Error"] = "Menu Restriction was unsuccessfully deleted";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult MenuDDL(string roleId)
        {
            var menuList = common.ToSelectList(MenuRestrictionBusinessLogic.getInstance().getMenuDDL(roleId), "ID", "NAME", "");
            return Json(menuList,JsonRequestBehavior.AllowGet);
        }
    }
}  