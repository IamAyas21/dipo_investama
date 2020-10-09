using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIPO_INVESTAMA.Models
{
    public class BankFacilityViewModels
    {
        public string BankFacilityId { get; set; }
        public string FacilityName { get; set; }
        public string Celling { get; set; }
        public Nullable<decimal> CostMoney { get; set; }
        public string BankId { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string BankName { get; set; }
    }
}