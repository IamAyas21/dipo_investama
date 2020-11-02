using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIPO_INVESTAMA.Models
{
    public class OutputViewModels
    {
        public string PettyCashId { get; set; }
        public string Date { get; set; }
        public string Account { get; set; }
        public string BankAccount { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Origin { get; set; }
        public string CashIn { get; set; }
        public string CashOut { get; set; }
        public string Balance { get; set; }
        public string SortBy { get; set; }
        public string Maker { get; set; }
        public string Department { get; set; }
        public string CheckedBy { get; set; }
        public string ApprovalBy { get; set; }
        public string RejectedBy { get; set; }
        public string CountCheckedBy { get; set; }
        public string CountApprovalBy { get; set; }
        public PagedList<OutputViewModels> TodaysJournal { get; set; }
    }
}