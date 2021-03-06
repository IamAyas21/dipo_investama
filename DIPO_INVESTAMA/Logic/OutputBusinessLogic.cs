﻿using DIPO_INVESTAMA.Entity;
using DIPO_INVESTAMA.Models;
using DIPO_INVESTAMA.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DIPO_INVESTAMA.Logic
{
    public class OutputBusinessLogic
    {
        DIPO_INVESTAMAEntities _db = new DIPO_INVESTAMAEntities();
        static OutputBusinessLogic outputBusinessLogic = null;
        public static OutputBusinessLogic getInstance()
        {
            if (outputBusinessLogic == null)
            {
                outputBusinessLogic = new OutputBusinessLogic();
                return outputBusinessLogic;
            }
            else
            {
                return outputBusinessLogic;
            }
        }

        public List<OutputViewModels> TodaysJournal(OutputViewModels model)
        {
            List<OutputViewModels> list = new List<OutputViewModels>();

            try
            {
                string account = model.Account, bankAccount = model.BankAccount, sortBy = model.SortBy;
                DateTime? startDate = null, endDate = null;
                if(!String.IsNullOrEmpty(model.Date))
                {
                    startDate = Convert.ToDateTime(model.Date.Split('-')[0].Trim());
                    endDate = Convert.ToDateTime(model.Date.Split('-')[1].Trim());
                }
                var journalList = _db.sp_OutputJournal(SessionManager.userId(),  startDate, endDate,model.Account,model.BankAccount,model.SortBy);
                foreach (var item in journalList)
                {
                    model = new OutputViewModels();
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
                    model.CheckedBy = item.CheckedBy;
                    model.ApprovalBy = item.ApprovalBy;
                    model.RejectedBy = item.RejectedBy;
                    model.CountCheckedBy = _db.sp_PrivilegeByUserId(SessionManager.userId(), "Checker").FirstOrDefault().Value.ToString();
                    model.CountApprovalBy = _db.sp_PrivilegeByUserId(SessionManager.userId(), "Approval").FirstOrDefault().Value.ToString();
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

        public int Checked(string id)
        {
            return _db.sp_CheckedPettyCash(id,SessionManager.userId());
        }

        public int Approved(string id)
        {
            return _db.sp_ApprovedPettyCash(id, SessionManager.userId());
        }

        public int Rejected(string id)
        {
            return _db.sp_RejectedPettyCash(id, SessionManager.userId());
        }
    }
}