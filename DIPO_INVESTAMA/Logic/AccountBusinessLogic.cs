using DIPO_INVESTAMA.Entity;
using DIPO_INVESTAMA.Models;
using DIPO_INVESTAMA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIPO_INVESTAMA.Logic
{
    public class AccountBusinessLogic
    {
        DIPO_INVESTAMAEntities _db = new DIPO_INVESTAMAEntities();
        static AccountBusinessLogic accountBusinessLogic = null;
        public static AccountBusinessLogic getInstance()
        {
            if (accountBusinessLogic == null)
            {
                accountBusinessLogic = new AccountBusinessLogic();
                return accountBusinessLogic;
            }
            else
            {
                return accountBusinessLogic;
            }
        }

        public PagedList<sp_AccountSelect_Result> ListMasterAccount()
        {
            var page = new PagedList<sp_AccountSelect_Result>();
            page.Content = _db.sp_AccountSelect().ToList();
            return page;
        }
        public int CreateAccount(sp_AccountSelect_Result model)
        {
            return _db.sp_AccountCreate(model.AccountNo, model.AccountName, SessionManager.userId());
        }
        public int UpdateAccount(sp_AccountSelect_Result model)
        {
            return _db.sp_AccountUpdate(model.AccountId, model.AccountNo, model.AccountName, SessionManager.userId());
        }

        public Account getAccountById(string id)
        {
            return _db.Accounts.Where(b => b.AccountId.Equals(id)).FirstOrDefault();
        }

        public int DeleteAccount(string id)
        {
            return _db.sp_AccountDelete(id);
        }
    }
}