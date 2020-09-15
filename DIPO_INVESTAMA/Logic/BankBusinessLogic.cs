using DIPO_INVESTAMA.Entity;
using DIPO_INVESTAMA.Models;
using DIPO_INVESTAMA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIPO_INVESTAMA.Logic
{
    public class BankBusinessLogic
    {
        DIPO_INVESTAMAEntities _db = new DIPO_INVESTAMAEntities();
        static BankBusinessLogic bankBusinessLogic = null;
        public static BankBusinessLogic getInstance()
        {
            if (bankBusinessLogic == null)
            {
                bankBusinessLogic = new BankBusinessLogic();
                return bankBusinessLogic;
            }
            else
            {
                return bankBusinessLogic;
            }
        }

        public PagedList<sp_BankSelect_Result> ListBank()
        {
            var page = new PagedList<sp_BankSelect_Result>();
            page.Content = _db.sp_BankSelect().ToList();
            return page;
        }
        public int CreateBank(sp_BankSelect_Result model)
        {
            return _db.sp_BankCreate(model.BankName, model.AccountNo, SessionManager.userId());
        }
        public int UpdateBank(sp_BankSelect_Result model)
        {
            return _db.sp_BankUpdate(model.BankId, model.BankName, model.AccountNo, SessionManager.userId());
        }

        public Bank getBankById(string id)
        {
            return _db.Banks.Where(b => b.BankId.Equals(id)).FirstOrDefault();
        }

        public int DeleteBank(string id)
        {
            return _db.sp_BankDelete(id);
        }
    }
}