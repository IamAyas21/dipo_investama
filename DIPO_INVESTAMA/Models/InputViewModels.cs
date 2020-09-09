using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIPO_INVESTAMA.Models
{
    public class InputViewModels
    {
        public string Date { get; set; }
        public string Account { get; set; }
        public string BankAccount { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Origin { get; set; }
        public string CashIn { get; set; }
        public string CashOut { get; set; }
        public PagedList<InputViewModels> TodaysJournal { get; set; }
    }
}