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
    public class AccountDetailsController : Controller
    {
        // GET: BankFacility
        public ActionResult Index()
        {
            return View(AccountDetailsBusinessLogic.getInstance().ListAccountDetails());
        }

        public ActionResult Create()
        {
            ViewBag.AccountList = common.ToSelectList(AccountDetailsBusinessLogic.getInstance().getAccountDDL(), "ID", "NAME", string.Empty);
            return View();
        }

        [HttpPost]
        public ActionResult Create(sp_AccountDetailSelect_Result model)
        {
            if (AccountDetailsBusinessLogic.getInstance().CreateAccountDetails(model) == -1)
            {
                TempData["Success"] = "Account Detail was successfully inserted";
            }
            else
            {
                TempData["Error"] = "Account Detail was unsuccessfully inserted";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            sp_AccountDetailSelect_Result model = new sp_AccountDetailSelect_Result();
            var accDtlData = AccountDetailsBusinessLogic.getInstance().getAccountDetailById(id);
            model.AccountDetailId = accDtlData.AccountDetailId;
            model.No = accDtlData.No;
            model.Name = accDtlData.Name;
            model.AccountId = accDtlData.AccountId;
            ViewBag.AccountList = common.ToSelectList(AccountDetailsBusinessLogic.getInstance().getAccountDDL(), "ID", "NAME", model.AccountId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(sp_AccountDetailSelect_Result model)
        {
            if (AccountDetailsBusinessLogic.getInstance().UpdateAccountDetails(model) == -1)
            {
                TempData["Success"] = "Account Detail was successfully updated";
            }
            else
            {
                TempData["Error"] = "Account Detail was unsuccessfully updated";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (AccountDetailsBusinessLogic.getInstance().DeleteAccountDetail(id) == -1)
            {
                TempData["Success"] = "Account Detail was successfully deleted";
            }
            else
            {
                TempData["Error"] = "Account Detail was unsuccessfully deleted";
            }
            return RedirectToAction("Index");
        }
    }
}