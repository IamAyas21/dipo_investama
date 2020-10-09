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
    public class BankFacilityBusinessLogic
    {
        DIPO_INVESTAMAEntities _db = new DIPO_INVESTAMAEntities();
        static BankFacilityBusinessLogic bankFacilityBusinessLogic = null;
        public static BankFacilityBusinessLogic getInstance()
        {
            if (bankFacilityBusinessLogic == null)
            {
                bankFacilityBusinessLogic = new BankFacilityBusinessLogic();
                return bankFacilityBusinessLogic;
            }
            else
            {
                return bankFacilityBusinessLogic;
            }
        }

        public PagedList<sp_BankFacilitySelect_Result> ListBank()
        {
            var page = new PagedList<sp_BankFacilitySelect_Result>();
            page.Content = _db.sp_BankFacilitySelect().ToList();
            return page;
        }
        public int CreateBank(BankFacilityViewModels model)
        {
            decimal celling = Convert.ToDecimal(model.Celling.Replace(".", ""));
            decimal? costMoney = model.CostMoney == null?0:model.CostMoney * Convert.ToDecimal(100);
            return _db.sp_BankFacilityCreate(model.FacilityName, celling,costMoney,model.BankId,DateTime.Now, SessionManager.userId());
        }
        public int UpdateBank(BankFacilityViewModels model)
        {
            decimal celling = Convert.ToDecimal(model.Celling.Replace(".", ""));
            decimal? costMoney = model.CostMoney == null ? 0 : model.CostMoney * Convert.ToDecimal(100);
            return _db.sp_BankFacilityUpdate(model.BankFacilityId, model.FacilityName, celling, costMoney, model.BankId, DateTime.Now, SessionManager.userId());
        }

        public BankFacility getBankById(string id)
        {
            return _db.BankFacilities.Where(b => b.BankFacilityId.Equals(id)).FirstOrDefault();
        }

        public int DeleteBank(string id)
        {
            return _db.sp_BankFacilityDelete(id);
        }

        public DataTable getBankDDL()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("ID", typeof(string));
            tbl.Columns.Add("NAME", typeof(string));
            foreach (var item in _db.sp_BankDDL())
            {
                DataRow row = tbl.NewRow();
                row["ID"] = item.ID;
                row["NAME"] = item.NAME;
                tbl.Rows.Add(row);
            }

            return tbl;
        }

        public DataTable getBankFacilityDDL()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("ID", typeof(string));
            tbl.Columns.Add("NAME", typeof(string));
            foreach (var item in _db.sp_BankFacilityDDL())
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