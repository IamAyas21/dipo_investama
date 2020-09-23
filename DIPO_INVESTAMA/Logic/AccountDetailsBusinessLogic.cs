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
    public class AccountDetailsBusinessLogic
    {
        DIPO_INVESTAMAEntities _db = new DIPO_INVESTAMAEntities();
        static AccountDetailsBusinessLogic accountDetailsBusinessLogic = null;
        public static AccountDetailsBusinessLogic getInstance()
        {
            if (accountDetailsBusinessLogic == null)
            {
                accountDetailsBusinessLogic = new AccountDetailsBusinessLogic();
                return accountDetailsBusinessLogic;
            }
            else
            {
                return accountDetailsBusinessLogic;
            }
        }

        public PagedList<sp_AccountDetailSelect_Result> ListAccountDetails()
        {
            var page = new PagedList<sp_AccountDetailSelect_Result>();
            page.Content = _db.sp_AccountDetailSelect().ToList();
            return page;
        }
        public int CreateAccountDetails(sp_AccountDetailSelect_Result model)
        {
            return _db.sp_AccountDetailCreate(model.No, model.Name,model.AccountId, SessionManager.userId());
        }
        public int UpdateAccountDetails(sp_AccountDetailSelect_Result model)
        {
            return _db.sp_AccountDetailUpdate(model.AccountDetailId, model.No, model.Name, model.AccountId, SessionManager.userId());
        }

        public AccountDetail getAccountDetailById(string id)
        {
            return _db.AccountDetails.Where(b => b.AccountDetailId.Equals(id)).FirstOrDefault();
        }

        public int DeleteAccountDetail(string id)
        {
            return _db.sp_AccountDetailDelete(id);
        }

        public DataTable getAccountDDL()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("ID", typeof(string));
            tbl.Columns.Add("NAME", typeof(string));
            foreach (var item in _db.sp_AccountDetailsDDL())
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