using DIPO_INVESTAMA.Entity;
using DIPO_INVESTAMA.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIPO_INVESTAMA.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {
            return View(DepartmentBusinessLogic.getInstance().ListDepartment());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(sp_DepartmentSelect_Result model)
        {
            if (DepartmentBusinessLogic.getInstance().CreateDepartment(model) == -1)
            {
                TempData["Success"] = "Department was successfully inserted";
            }
            else
            {
                TempData["Error"] = "Department was unsuccessfully inserted";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            sp_DepartmentSelect_Result model = new sp_DepartmentSelect_Result();
            var deptData = DepartmentBusinessLogic.getInstance().getDepartmentById(id);
            model.DepartmentId = deptData.DepartmentId;
            model.DepartmentName = deptData.DepartmentName;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(sp_DepartmentSelect_Result model)
        {
            if (DepartmentBusinessLogic.getInstance().UpdateDepartment(model) == -1)
            {
                TempData["Success"] = "Department was successfully updated";
            }
            else
            {
                TempData["Error"] = "Department was unsuccessfully updated";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (DepartmentBusinessLogic.getInstance().DeleteDepartment(id) == -1)
            {
                TempData["Success"] = "Department was successfully deleted";
            }
            else
            {
                TempData["Error"] = "Department was unsuccessfully deleted";
            }
            return RedirectToAction("Index");
        }
    }
}