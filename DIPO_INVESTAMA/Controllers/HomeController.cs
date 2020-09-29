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
    public class HomeController : Controller
    {
        [CheckAuthorizeAttribute()]
        public ActionResult Index()
        {
            ViewBag.FilterBankList = common.ToSelectList(BankFacilityBusinessLogic.getInstance().getBankFacilityDDL(), "ID", "NAME", string.Empty);
            ViewBag.FilterAccountList = common.ToSelectList(BankFacilityBusinessLogic.getInstance().getBankFacilityDDL(), "ID", "NAME", string.Empty);
            ViewBag.FilterCashInList = common.ToSelectList(BankFacilityBusinessLogic.getInstance().getBankFacilityDDL(), "ID", "NAME", string.Empty);
            ViewBag.FilterCashOutList = common.ToSelectList(BankFacilityBusinessLogic.getInstance().getBankFacilityDDL(), "ID", "NAME", string.Empty);
            return View();
        }

        [HttpPost]
        public ActionResult Index(DashboardViewModels model)
        {
            ViewBag.FilterBankList = common.ToSelectList(BankFacilityBusinessLogic.getInstance().getBankFacilityDDL(), "ID", "NAME", model.FilterReportBank);
            ViewBag.FilterAccountList = common.ToSelectList(BankFacilityBusinessLogic.getInstance().getBankFacilityDDL(), "ID", "NAME", model.FilterReportAccount);
            ViewBag.FilterCashInList = common.ToSelectList(BankFacilityBusinessLogic.getInstance().getBankFacilityDDL(), "ID", "NAME", model.FilterReportCashIn);
            ViewBag.FilterCashOutList = common.ToSelectList(BankFacilityBusinessLogic.getInstance().getBankFacilityDDL(), "ID", "NAME", model.FilterReportCashOut);
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportOfBank(DashboardViewModels model)
        {
            try
            {
                return Json(DashboardBusinessLogic.getInstance().getReportOfBank(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Logging.getInstance().CreateLogError(e);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ReportOfAccount(DashboardViewModels model)
        {
            try
            {
                return Json(DashboardBusinessLogic.getInstance().getReportOfAccount(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Logging.getInstance().CreateLogError(e);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ReportOfAccountCashIn(DashboardViewModels model)
        {
            try
            {
                return Json(DashboardBusinessLogic.getInstance().getReportOfAccountCashIn(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Logging.getInstance().CreateLogError(e);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ReportOfAccountCashOut(DashboardViewModels model)
        {
            try
            {
                return Json(DashboardBusinessLogic.getInstance().getReportOfCashOut(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Logging.getInstance().CreateLogError(e);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}