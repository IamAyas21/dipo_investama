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
    public class InputBusinessLogic
    {
        DIPO_INVESTAMAEntities _db = new DIPO_INVESTAMAEntities();
        static InputBusinessLogic inputBusinessLogic = null;
        public static InputBusinessLogic getInstance()
        {
            if (inputBusinessLogic == null)
            {
                inputBusinessLogic = new InputBusinessLogic();
                return inputBusinessLogic;
            }
            else
            {
                return inputBusinessLogic;
            }
        }


        public int InputJournal(InputViewModels model)
        {
            try
            {
                if((Convert.ToDecimal(model.Amount.Replace(".", "")) > Convert.ToDecimal(_db.sp_GetBalanceCeilingByBankFacilityId(model.BankAccount).FirstOrDefault().Value)) && model.Origin.Equals("Cash Out"))
                {
                    return 1;
                }

                decimal amt = Convert.ToDecimal(model.Amount.Replace(".", ""));
                return _db.sp_InputJournal(model.Date == string.Empty ? DateTime.Now : Convert.ToDateTime(model.Date), model.Account, model.BankAccount, amt, model.Description, model.Origin, SessionManager.userId());
            }
            catch (Exception e)
            {
                Logging.getInstance().CreateLogError(e);
                throw e;
            }
        }

        public List<InputViewModels> TodaysJournal()
        {
            var model = new InputViewModels();
            List<InputViewModels> list = new List<InputViewModels>();

            try
            {
                var journalList = _db.sp_TodaysJournal();
                foreach (var item in journalList)
                {
                    model = new InputViewModels();
                    DateTime? trxDate = item.TransactionDate;
                    string strTrxDate = trxDate.Value.ToString("dd MMM yyyy");

                    model.Date = strTrxDate;
                    model.Account = item.AccountName;
                    model.BankAccount = item.FacilityName;
                    model.Description = item.Description;
                    model.CashIn = item.Origin.Equals("Cash In") ? item.Amount.ToString() : string.Empty;
                    model.CashOut = item.Origin.Equals("Cash Out") ? item.Amount.ToString() : string.Empty;
                    list.Add(model);
                }
            }
            catch (Exception e)
            {
                Logging.getInstance().CreateLogError(e);
                throw e;
            }
            return list;
        }
    }
}