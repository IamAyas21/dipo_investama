//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DIPO_INVESTAMA.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw_PettyCash
    {
        public Nullable<long> No { get; set; }
        public string PettyCashId { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public string AccountName { get; set; }
        public string FacilityName { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Cash_In { get; set; }
        public Nullable<decimal> Cash_Out { get; set; }
        public string Maker { get; set; }
        public string DepartmentName { get; set; }
        public Nullable<System.DateTime> CheckedAt { get; set; }
        public string CheckedBy { get; set; }
        public Nullable<System.DateTime> ApprovalAt { get; set; }
        public string ApprovalBy { get; set; }
        public Nullable<System.DateTime> RejectedAt { get; set; }
        public string RejectedBy { get; set; }
        public string BankFacilityId { get; set; }
        public Nullable<decimal> Celling { get; set; }
        public string AccountDetailId { get; set; }
        public string DepartmentId { get; set; }
    }
}
