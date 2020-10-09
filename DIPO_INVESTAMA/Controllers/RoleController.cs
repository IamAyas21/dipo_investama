using DIPO_INVESTAMA.App_Start;
using DIPO_INVESTAMA.Entity;
using DIPO_INVESTAMA.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIPO_INVESTAMA.Controllers
{
    public class RoleController : Controller
    {
        [CheckAuthorize(Roles = "Role")]
        public ActionResult Index()
        {
            return View(RoleBusinessLogic.getInstance().ListRole());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(sp_RoleSelect_Result model)
        {
            if (RoleBusinessLogic.getInstance().CreateRole(model) == -1)
            {
                TempData["Success"] = "Role was successfully inserted";
            }
            else
            {
                TempData["Error"] = "Role was unsuccessfully inserted";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            sp_RoleSelect_Result model = new sp_RoleSelect_Result();
            var roleData = RoleBusinessLogic.getInstance().getRoleById(id);
            model.RoleId = roleData.RoleId;
            model.RoleName = roleData.RoleName;
            model.Maker = roleData.Maker;
            model.Checker = roleData.Checker;
            model.Approval = roleData.Approval;
            model.SuperUser = roleData.SuperUser;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(sp_RoleSelect_Result model)
        {
            if (RoleBusinessLogic.getInstance().UpdateRole(model) == -1)
            {
                TempData["Success"] = "Role was successfully updated";
            }
            else
            {
                TempData["Error"] = "Role was unsuccessfully updated";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (RoleBusinessLogic.getInstance().DeleteRole(id) == -1)
            {
                TempData["Success"] = "Role was successfully deleted";
            }
            else
            {
                TempData["Error"] = "Role was unsuccessfully deleted";
            }
            return RedirectToAction("Index");
        }
    }
}