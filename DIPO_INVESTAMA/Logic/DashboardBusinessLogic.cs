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

        public List<sp_ReportSummaryBank_Result> getReportSummaryBank()
        {
            return _db.sp_ReportSummaryBank().ToList();
        }
        public List<sp_ReportChartBankFacility_Result> getReportChartBank()
        {
            return _db.sp_ReportChartBankFacility().ToList();
        }

        public List<sp_ReportOfBank_Result> getReportOfBank(DashboardViewModels model)
        {
            if(!string.IsNullOrEmpty(model.PeriodeReportBank))
            {
                string[] strDate = model.PeriodeReportBank.Split('-');
                return _db.sp_ReportOfBank(model.FilterReportBank, strDate[0].Trim(), strDate[1].Trim(), model.ViewByReportBank).ToList();
            }
           else
            {
                return _db.sp_ReportOfBank(model.FilterReportBank, string.Empty, string.Empty, model.ViewByReportBank).ToList();
            }
        }

        public List<sp_ReportOfAccount_Result> getReportOfAccount(DashboardViewModels model)
        {
            if (!string.IsNullOrEmpty(model.PeriodeReportAccount))
            {
                string[] strDate = model.PeriodeReportAccount.Split('-');
                return _db.sp_ReportOfAccount(model.FilterReportAccount, strDate[0].Trim(), strDate[1].Trim(), model.ViewByReportAccount).ToList();
            }
            else
            {
                return _db.sp_ReportOfAccount(model.FilterReportAccount, string.Empty, string.Empty, model.ViewByReportAccount).ToList();
            }
        }

        public List<sp_ReportOfAccountCashIn_Result> getReportOfAccountCashIn(DashboardViewModels model)
        {
            if (!string.IsNullOrEmpty(model.PeriodeReportCashIn))
            {
                string[] strDate = model.PeriodeReportCashIn.Split('-');
                return _db.sp_ReportOfAccountCashIn(model.FilterReportCashIn, strDate[0].Trim(), strDate[1].Trim(), model.ViewByReportCashIn).ToList();
            }
            else
            {
                return _db.sp_ReportOfAccountCashIn(model.FilterReportAccount, string.Empty, string.Empty, model.ViewByReportCashIn).ToList();
            }
        }

        public List<sp_ReportOfAccountCashOut_Result> getReportOfCashOut(DashboardViewModels model)
        {
            if (!string.IsNullOrEmpty(model.PeriodeReportCashOut))
            {
                string[] strDate = model.PeriodeReportCashOut.Split('-');
                return _db.sp_ReportOfAccountCashOut(model.FilterReportCashOut, strDate[0].Trim(), strDate[1].Trim(), model.ViewByReportCashOut).ToList();
            }
            else
            {
                return _db.sp_ReportOfAccountCashOut(model.FilterReportCashOut, string.Empty, string.Empty, model.ViewByReportCashOut).ToList();
            }
        }
    }
}