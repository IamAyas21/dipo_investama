using DIPO_INVESTAMA.Entity;
using DIPO_INVESTAMA.Models;
using DIPO_INVESTAMA.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DIPO_INVESTAMA.Logic
{
    public class HistoryBusinessLogic
    {
        DIPO_INVESTAMAEntities _db = new DIPO_INVESTAMAEntities();
        static HistoryBusinessLogic historyBusinessLogic = null;
        public static HistoryBusinessLogic getInstance()
        {
            if (historyBusinessLogic == null)
            {
                historyBusinessLogic = new HistoryBusinessLogic();
                return historyBusinessLogic;
            }
            else
            {
                return historyBusinessLogic;
            }
        }

        public List<HistoryViewModels> TodaysJournal(HistoryViewModels model)
        {
            List<HistoryViewModels> list = new List<HistoryViewModels>();

            try
            {
                string account = model.Account, bankAccount = model.BankAccount, sortBy = model.SortBy;
                DateTime? startDate = null, endDate = null;
                if(!String.IsNullOrEmpty(model.Date))
                {
                    startDate = Convert.ToDateTime(model.Date.Split('-')[0].Trim());
                    endDate = Convert.ToDateTime(model.Date.Split('-')[1].Trim());
                }
                var journalList = _db.sp_HistoryJournal(SessionManager.userId(),  startDate, endDate,model.Account,model.BankAccount,model.SortBy);
                foreach (var item in journalList)
                {
                    model = new HistoryViewModels();
                    DateTime? trxDate = item.TransactionDate;
                    string strTrxDate = trxDate.Value.ToString("dd MMM yyyy");

                    model.PettyCashId = item.PettyCashId;
                    model.Date = strTrxDate;
                    model.Account = item.AccountName;
                    model.BankAccount = item.FacilityName;
                    model.Description = item.Description;
                    model.CashIn = item.Cash_In.ToString();
                    model.CashOut = item.Cash_Out.ToString();
                    model.Balance = item.Balance.ToString();
                    model.Maker = item.Maker;
                    model.Department = item.DepartmentName;
                    list.Add(model);
                }
                //model.Date = string.Format("{0} - {1}", startDate, endDate);
                //model.Account = account;
                //model.BankAccount = bankAccount;
                //model.SortBy = sortBy;
            }
            catch (Exception e)
            {
                Logging.getInstance().CreateLogError(e);
                throw e;
            }
            return list;
        }

        public DataTable getSortByDDL()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("ID", typeof(string));
            tbl.Columns.Add("NAME", typeof(string));
            foreach (var item in _db.sp_AccountDDL())
            {
                DataRow row = tbl.NewRow();
                row["ID"] = item.ID;
                row["NAME"] = item.NAME;
                tbl.Rows.Add(row);
            }

            return tbl;
        }
    }
}