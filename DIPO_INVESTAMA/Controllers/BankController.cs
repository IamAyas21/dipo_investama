using DIPO_INVESTAMA.Entity;
using DIPO_INVESTAMA.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIPO_INVESTAMA.Controllers
{
    public class BankController : Controller
    {
        // GET: Bank
        public ActionResult Index()
        {
            return View(BankBusinessLogic.getInstance().ListBank());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(sp_BankSelect_Result model)
        {
            if (BankBusinessLogic.getInstance().CreateBank(model) == -1)
            {
                TempData["Success"] = "Bank was successfully inserted";
            }
            else
            {
                TempData["Error"] = "Bank was unsuccessfully inserted";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            sp_BankSelect_Result model = new sp_BankSelect_Result();
            var bankData = BankBusinessLogic.getInstance().getBankById(id);
            model.BankId = bankData.BankId;
            model.BankName = bankData.BankName;
            model.AccountNo = bankData.AccountNo;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(sp_BankSelect_Result model)
        {
            if (BankBusinessLogic.getInstance().UpdateBank(model) == -1)
            {
                TempData["Success"] = "Bank was successfully updated";
            }
            else
            {
                TempData["Error"] = "Bank was unsuccessfully updated";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (BankBusinessLogic.getInstance().DeleteBank(id) == -1)
            {
                TempData["Success"] = "Bank was successfully deleted";
            }
            else
            {
                TempData["Error"] = "Bank was unsuccessfully deleted";
            }
            return RedirectToAction("Index");
        }
    }
}