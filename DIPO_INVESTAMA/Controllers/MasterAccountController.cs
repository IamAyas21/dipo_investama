using DIPO_INVESTAMA.Entity;
using DIPO_INVESTAMA.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIPO_INVESTAMA.Controllers
{
    public class MasterAccountController : Controller
    {
        // GET: MasterAccount
        public ActionResult Index()
        {
            return View(AccountBusinessLogic.getInstance().ListMasterAccount());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(sp_AccountSelect_Result model)
        {
            if (AccountBusinessLogic.getInstance().CreateAccount(model) == -1)
            {
                TempData["Success"] = "Account was successfully inserted";
            }
            else
            {
                TempData["Error"] = "Account was unsuccessfully inserted";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            sp_AccountSelect_Result model = new sp_AccountSelect_Result();
            var accountData = AccountBusinessLogic.getInstance().getAccountById(id);
            model.AccountId = accountData.AccountId;
            model.AccountName = accountData.AccountName;
            model.AccountNo = accountData.AccountNo;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(sp_AccountSelect_Result model)
        {
            if (AccountBusinessLogic.getInstance().UpdateAccount(model) == -1)
            {
                TempData["Success"] = "Account was successfully updated";
            }
            else
            {
                TempData["Error"] = "Account was unsuccessfully updated";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (AccountBusinessLogic.getInstance().DeleteAccount(id) == -1)
            {
                TempData["Success"] = "Account was successfully deleted";
            }
            else
            {
                TempData["Error"] = "Account was unsuccessfully deleted";
            }
            return RedirectToAction("Index");
        }
    }
}