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
    public class BankFacilityController : Controller
    {
        // GET: BankFacility
        public ActionResult Index()
        {
            return View(BankFacilityBusinessLogic.getInstance().ListBank());
        }

        public ActionResult Create()
        {
            ViewBag.BankAccountList = common.ToSelectList(BankFacilityBusinessLogic.getInstance().getBankDDL(), "ID", "NAME", string.Empty);
            return View();
        }

        [HttpPost]
        public ActionResult Create(sp_BankFacilitySelect_Result model)
        {
            if (BankFacilityBusinessLogic.getInstance().CreateBank(model) == -1)
            {
                TempData["Success"] = "Bank Facility was successfully inserted";
            }
            else
            {
                TempData["Error"] = "Bank Facility was unsuccessfully inserted";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            sp_BankFacilitySelect_Result model = new sp_BankFacilitySelect_Result();
            var bankData = BankFacilityBusinessLogic.getInstance().getBankById(id);
            model.BankId = bankData.BankId;
            model.BankFacilityId = bankData.BankFacilityId;
            model.Celling = bankData.Celling;
            model.CostMoney = bankData.CostMoney;
            model.FacilityName = bankData.FacilityName;
            ViewBag.BankAccountList = common.ToSelectList(BankFacilityBusinessLogic.getInstance().getBankDDL(), "ID", "NAME", model.BankId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(sp_BankFacilitySelect_Result model)
        {
            if (BankFacilityBusinessLogic.getInstance().UpdateBank(model) == -1)
            {
                TempData["Success"] = "Bank Facility was successfully updated";
            }
            else
            {
                TempData["Error"] = "Bank Facility was unsuccessfully updated";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (BankFacilityBusinessLogic.getInstance().DeleteBank(id) == -1)
            {
                TempData["Success"] = "Bank Facility was successfully deleted";
            }
            else
            {
                TempData["Error"] = "Bank Facility was unsuccessfully deleted";
            }
            return RedirectToAction("Index");
        }
    }
}