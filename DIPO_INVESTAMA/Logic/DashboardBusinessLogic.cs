using DIPO_INVESTAMA.Entity;
using DIPO_INVESTAMA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DIPO_INVESTAMA.Logic
{
    public class DashboardBusinessLogic
    {
        DIPO_INVESTAMAEntities _db = new DIPO_INVESTAMAEntities();
        static DashboardBusinessLogic dashboardBusinessLogic = null;
        public static DashboardBusinessLogic getInstance()
        {
            if (dashboardBusinessLogic == null)
            {
                dashboardBusinessLogic = new DashboardBusinessLogic();
                return dashboardBusinessLogic;
            }
            else
            {
                return dashboardBusinessLogic;
            }
        }

        public  List<sp_ReportOfBank_Result> getReportOfBank(DashboardViewModels model)
        {
            //string[] strDate = model.PeriodeReportBank.Split('-');
            //return _db.sp_ReportOfBank(model.FilterReportBank, strDate[0].Trim(), strDate[1].Trim(), model.ViewByReportBank).ToList();
            return _db.sp_ReportOfBank(model.FilterReportBank, model.PeriodeReportBank, model.PeriodeReportBank, model.ViewByReportBank).ToList();
        }

        public List<sp_ReportOfAccount_Result> getReportOfAccount(DashboardViewModels model)
        {
            //string[] strDate = model.PeriodeReportBank.Split('-');
            //return _db.sp_ReportOfBank(model.FilterReportBank, strDate[0].Trim(), strDate[1].Trim(), model.ViewByReportBank).ToList();
            return _db.sp_ReportOfAccount(model.FilterReportBank, model.PeriodeReportBank, model.PeriodeReportBank, model.ViewByReportBank).ToList();
        }

        public List<sp_ReportOfAccountCashIn_Result> getReportOfAccountCashIn(DashboardViewModels model)
        {
            //string[] strDate = model.PeriodeReportBank.Split('-');
            //return _db.sp_ReportOfBank(model.FilterReportBank, strDate[0].Trim(), strDate[1].Trim(), model.ViewByReportBank).ToList();
            return _db.sp_ReportOfAccountCashIn(model.FilterReportBank, model.PeriodeReportBank, model.PeriodeReportBank, model.ViewByReportBank).ToList();
        }

        public List<sp_ReportOfAccountCashOut_Result> getReportOfCashOut(DashboardViewModels model)
        {
            //string[] strDate = model.PeriodeReportBank.Split('-');
            //return _db.sp_ReportOfBank(model.FilterReportBank, strDate[0].Trim(), strDate[1].Trim(), model.ViewByReportBank).ToList();
            return _db.sp_ReportOfAccountCashOut(model.FilterReportBank, model.PeriodeReportBank, model.PeriodeReportBank, model.ViewByReportBank).ToList();
        }
    }
}