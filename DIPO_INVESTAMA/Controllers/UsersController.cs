using DIPO_INVESTAMA.App_Start;
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
    public class UsersController : Controller
    {
        // GET: Users
        [CheckAuthorize(Roles = "Users")]
        public ActionResult Index()
        {
            return View(UserBusinessLogic.getInstance().ListUsers());
        }

        public ActionResult Create()
        {
            ViewBag.RoleList = common.ToSelectList(RoleBusinessLogic.getInstance().getRoleDDL(), "ID", "NAME", string.Empty);
            ViewBag.DepartmentList = common.ToSelectList(DepartmentBusinessLogic.getInstance().getDepartmentDDL(), "ID", "NAME", string.Empty);
            return View();
        }

        [HttpPost]
        public ActionResult Create(sp_UserSelect_Result model)
        {
            if (UserBusinessLogic.getInstance().CreateUser(model) == -1)
            {
                TempData["Success"] = "User was successfully inserted";
            }
            else
            {
                TempData["Error"] = "User was unsuccessfully inserted";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            sp_UserSelect_Result model = new sp_UserSelect_Result();
            var userData = UserBusinessLogic.getInstance().getUserById(id);
            model.UserId = userData.UserId;
            model.UserName = userData.UserName;
            model.Name = userData.Name;
            model.Password = userData.Password;
            model.RoleId = userData.RoleId;
            model.DepartmentId = userData.DepartmentId;
            ViewBag.RoleList = common.ToSelectList(RoleBusinessLogic.getInstance().getRoleDDL(), "ID", "NAME", model.RoleId);
            ViewBag.DepartmentList = common.ToSelectList(DepartmentBusinessLogic.getInstance().getDepartmentDDL(), "ID", "NAME", model.DepartmentId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(sp_UserSelect_Result model)
        {
            if (UserBusinessLogic.getInstance().UpdateUser(model) == -1)
            {
                TempData["Success"] = "User was successfully updated";
            }
            else
            {
                TempData["Error"] = "User was unsuccessfully updated";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (UserBusinessLogic.getInstance().DeleteUser(id) == -1)
            {
                TempData["Success"] = "User was successfully deleted";
            }
            else
            {
                TempData["Error"] = "User was unsuccessfully deleted";
            }
            return RedirectToAction("Index");
        }
    }
}  