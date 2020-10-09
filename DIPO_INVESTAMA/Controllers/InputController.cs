using DIPO_INVESTAMA.App_Start;
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
    public class InputController : Controller
    {
        // GET: Input
        [CheckAuthorize(Roles = "Input")]
        public ActionResult Index()
        {
            InputViewModels model = new InputViewModels();
            model.TodaysJournal = Journals();
            ViewBag.AccountList = common.ToSelectList(AccountDetailsBusinessLogic.getInstance().getAccountDetailDDL(),"ID", "NAME",string.Empty);
            ViewBag.BankAccountList = common.ToSelectList(BankFacilityBusinessLogic.getInstance().getBankFacilityDDL(), "ID", "NAME",string.Empty);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(InputViewModels model)
        {
            if(InputBusinessLogic.getInstance().InputJournal(model)  == -1)
            {
                TempData["Success"] = "Successfully was inserted";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Unsuccessfully was inserted";
            }

            ViewBag.AccountList = common.ToSelectList(AccountDetailsBusinessLogic.getInstance().getAccountDetailDDL(), "ID", "NAME", model.Account);
            ViewBag.BankAccountList = common.ToSelectList(BankFacilityBusinessLogic.getInstance().getBankFacilityDDL(), "ID", "NAME", model.BankAccount);
            return View(model);
        }

        private PagedList<InputViewModels> Journals()
        {
            var page = new PagedList<InputViewModels>();
            page.Content = InputBusinessLogic.getInstance().TodaysJournal();
            return page;
        }
    }
}