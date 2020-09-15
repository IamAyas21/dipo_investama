﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DIPO_INVESTAMAEntities : DbContext
    {
        public DIPO_INVESTAMAEntities()
            : base("name=DIPO_INVESTAMAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountDetail> AccountDetails { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<BankFacility> BankFacilities { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuRestriction> MenuRestrictions { get; set; }
        public virtual DbSet<PettyCash> PettyCashes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<vw_PettyCash> vw_PettyCash { get; set; }
    
        public virtual ObjectResult<sp_AccountDDL_Result> sp_AccountDDL()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_AccountDDL_Result>("sp_AccountDDL");
        }
    
        public virtual ObjectResult<sp_BankFacilityDDL_Result> sp_BankFacilityDDL()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_BankFacilityDDL_Result>("sp_BankFacilityDDL");
        }
    
        public virtual int sp_InputJournal(Nullable<System.DateTime> date, string account, string bankAccount, Nullable<decimal> amount, string description, string origin, string userId)
        {
            var dateParameter = date.HasValue ?
                new ObjectParameter("date", date) :
                new ObjectParameter("date", typeof(System.DateTime));
    
            var accountParameter = account != null ?
                new ObjectParameter("account", account) :
                new ObjectParameter("account", typeof(string));
    
            var bankAccountParameter = bankAccount != null ?
                new ObjectParameter("bankAccount", bankAccount) :
                new ObjectParameter("bankAccount", typeof(string));
    
            var amountParameter = amount.HasValue ?
                new ObjectParameter("Amount", amount) :
                new ObjectParameter("Amount", typeof(decimal));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("description", description) :
                new ObjectParameter("description", typeof(string));
    
            var originParameter = origin != null ?
                new ObjectParameter("Origin", origin) :
                new ObjectParameter("Origin", typeof(string));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_InputJournal", dateParameter, accountParameter, bankAccountParameter, amountParameter, descriptionParameter, originParameter, userIdParameter);
        }
    
        public virtual ObjectResult<sp_OutputJournal_Result> sp_OutputJournal(string userId, Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate, string accountDetailId, string bankFacilityId, string sortBy)
        {
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(System.DateTime));
    
            var accountDetailIdParameter = accountDetailId != null ?
                new ObjectParameter("accountDetailId", accountDetailId) :
                new ObjectParameter("accountDetailId", typeof(string));
    
            var bankFacilityIdParameter = bankFacilityId != null ?
                new ObjectParameter("bankFacilityId", bankFacilityId) :
                new ObjectParameter("bankFacilityId", typeof(string));
    
            var sortByParameter = sortBy != null ?
                new ObjectParameter("sortBy", sortBy) :
                new ObjectParameter("sortBy", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_OutputJournal_Result>("sp_OutputJournal", userIdParameter, startDateParameter, endDateParameter, accountDetailIdParameter, bankFacilityIdParameter, sortByParameter);
        }
    
        public virtual ObjectResult<sp_TodaysJournal_Result> sp_TodaysJournal()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_TodaysJournal_Result>("sp_TodaysJournal");
        }
    
        public virtual ObjectResult<sp_UserLogin_Result> sp_UserLogin(string username, string password)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_UserLogin_Result>("sp_UserLogin", usernameParameter, passwordParameter);
        }
    
        public virtual ObjectResult<sp_BankSelect_Result> sp_BankSelect()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_BankSelect_Result>("sp_BankSelect");
        }
    
        public virtual int sp_BankUpdate(string bankId, string bankName, string accountNo, string userId)
        {
            var bankIdParameter = bankId != null ?
                new ObjectParameter("bankId", bankId) :
                new ObjectParameter("bankId", typeof(string));
    
            var bankNameParameter = bankName != null ?
                new ObjectParameter("bankName", bankName) :
                new ObjectParameter("bankName", typeof(string));
    
            var accountNoParameter = accountNo != null ?
                new ObjectParameter("accountNo", accountNo) :
                new ObjectParameter("accountNo", typeof(string));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_BankUpdate", bankIdParameter, bankNameParameter, accountNoParameter, userIdParameter);
        }
    
        public virtual int sp_BankCreate(string bankName, string accountNo, string userId)
        {
            var bankNameParameter = bankName != null ?
                new ObjectParameter("bankName", bankName) :
                new ObjectParameter("bankName", typeof(string));
    
            var accountNoParameter = accountNo != null ?
                new ObjectParameter("accountNo", accountNo) :
                new ObjectParameter("accountNo", typeof(string));
    
            var userIdParameter = userId != null ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_BankCreate", bankNameParameter, accountNoParameter, userIdParameter);
        }
    
        public virtual int sp_BankDelete(string bankId)
        {
            var bankIdParameter = bankId != null ?
                new ObjectParameter("bankId", bankId) :
                new ObjectParameter("bankId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_BankDelete", bankIdParameter);
        }
    
        public virtual ObjectResult<sp_BankFacilitySelect_Result> sp_BankFacilitySelect()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_BankFacilitySelect_Result>("sp_BankFacilitySelect");
        }
    
        public virtual int sp_BankFacilityCreate(string facilityName, Nullable<decimal> celling, Nullable<decimal> costMoney, string bankId, Nullable<System.DateTime> createdAt, string createdBy)
        {
            var facilityNameParameter = facilityName != null ?
                new ObjectParameter("FacilityName", facilityName) :
                new ObjectParameter("FacilityName", typeof(string));
    
            var cellingParameter = celling.HasValue ?
                new ObjectParameter("Celling", celling) :
                new ObjectParameter("Celling", typeof(decimal));
    
            var costMoneyParameter = costMoney.HasValue ?
                new ObjectParameter("CostMoney", costMoney) :
                new ObjectParameter("CostMoney", typeof(decimal));
    
            var bankIdParameter = bankId != null ?
                new ObjectParameter("BankId", bankId) :
                new ObjectParameter("BankId", typeof(string));
    
            var createdAtParameter = createdAt.HasValue ?
                new ObjectParameter("CreatedAt", createdAt) :
                new ObjectParameter("CreatedAt", typeof(System.DateTime));
    
            var createdByParameter = createdBy != null ?
                new ObjectParameter("CreatedBy", createdBy) :
                new ObjectParameter("CreatedBy", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_BankFacilityCreate", facilityNameParameter, cellingParameter, costMoneyParameter, bankIdParameter, createdAtParameter, createdByParameter);
        }
    
        public virtual int sp_BankFacilityDelete(string bankFacilityId)
        {
            var bankFacilityIdParameter = bankFacilityId != null ?
                new ObjectParameter("BankFacilityId", bankFacilityId) :
                new ObjectParameter("BankFacilityId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_BankFacilityDelete", bankFacilityIdParameter);
        }
    
        public virtual int sp_BankFacilityUpdate(string bankFacilityId, string facilityName, Nullable<decimal> celling, Nullable<decimal> costMoney, string bankId, Nullable<System.DateTime> updatedAt, string updatedBy)
        {
            var bankFacilityIdParameter = bankFacilityId != null ?
                new ObjectParameter("BankFacilityId", bankFacilityId) :
                new ObjectParameter("BankFacilityId", typeof(string));
    
            var facilityNameParameter = facilityName != null ?
                new ObjectParameter("FacilityName", facilityName) :
                new ObjectParameter("FacilityName", typeof(string));
    
            var cellingParameter = celling.HasValue ?
                new ObjectParameter("Celling", celling) :
                new ObjectParameter("Celling", typeof(decimal));
    
            var costMoneyParameter = costMoney.HasValue ?
                new ObjectParameter("CostMoney", costMoney) :
                new ObjectParameter("CostMoney", typeof(decimal));
    
            var bankIdParameter = bankId != null ?
                new ObjectParameter("BankId", bankId) :
                new ObjectParameter("BankId", typeof(string));
    
            var updatedAtParameter = updatedAt.HasValue ?
                new ObjectParameter("UpdatedAt", updatedAt) :
                new ObjectParameter("UpdatedAt", typeof(System.DateTime));
    
            var updatedByParameter = updatedBy != null ?
                new ObjectParameter("UpdatedBy", updatedBy) :
                new ObjectParameter("UpdatedBy", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_BankFacilityUpdate", bankFacilityIdParameter, facilityNameParameter, cellingParameter, costMoneyParameter, bankIdParameter, updatedAtParameter, updatedByParameter);
        }
    
        public virtual ObjectResult<sp_BankDDL_Result> sp_BankDDL()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_BankDDL_Result>("sp_BankDDL");
        }
    
        public virtual int sp_AccountCreate(string accountNo, string accountName, string createdBy)
        {
            var accountNoParameter = accountNo != null ?
                new ObjectParameter("AccountNo", accountNo) :
                new ObjectParameter("AccountNo", typeof(string));
    
            var accountNameParameter = accountName != null ?
                new ObjectParameter("AccountName", accountName) :
                new ObjectParameter("AccountName", typeof(string));
    
            var createdByParameter = createdBy != null ?
                new ObjectParameter("CreatedBy", createdBy) :
                new ObjectParameter("CreatedBy", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_AccountCreate", accountNoParameter, accountNameParameter, createdByParameter);
        }
    
        public virtual int sp_AccountDelete(string accountId)
        {
            var accountIdParameter = accountId != null ?
                new ObjectParameter("AccountId", accountId) :
                new ObjectParameter("AccountId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_AccountDelete", accountIdParameter);
        }
    
        public virtual ObjectResult<sp_AccountSelect_Result> sp_AccountSelect()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_AccountSelect_Result>("sp_AccountSelect");
        }
    
        public virtual int sp_AccountUpdate(string accountId, string accountNo, string accountName, string updatedBy)
        {
            var accountIdParameter = accountId != null ?
                new ObjectParameter("AccountId", accountId) :
                new ObjectParameter("AccountId", typeof(string));
    
            var accountNoParameter = accountNo != null ?
                new ObjectParameter("AccountNo", accountNo) :
                new ObjectParameter("AccountNo", typeof(string));
    
            var accountNameParameter = accountName != null ?
                new ObjectParameter("AccountName", accountName) :
                new ObjectParameter("AccountName", typeof(string));
    
            var updatedByParameter = updatedBy != null ?
                new ObjectParameter("UpdatedBy", updatedBy) :
                new ObjectParameter("UpdatedBy", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_AccountUpdate", accountIdParameter, accountNoParameter, accountNameParameter, updatedByParameter);
        }
    
        public virtual int sp_AccountDetailCreate(string no, string name, string accountId, string createdBy)
        {
            var noParameter = no != null ?
                new ObjectParameter("No", no) :
                new ObjectParameter("No", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var accountIdParameter = accountId != null ?
                new ObjectParameter("AccountId", accountId) :
                new ObjectParameter("AccountId", typeof(string));
    
            var createdByParameter = createdBy != null ?
                new ObjectParameter("CreatedBy", createdBy) :
                new ObjectParameter("CreatedBy", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_AccountDetailCreate", noParameter, nameParameter, accountIdParameter, createdByParameter);
        }
    
        public virtual ObjectResult<sp_AccountDetailSelect_Result> sp_AccountDetailSelect()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_AccountDetailSelect_Result>("sp_AccountDetailSelect");
        }
    
        public virtual int sp_AccountDetailUpdate(string accountDetailId, string no, string name, string accountId, string updatedBy)
        {
            var accountDetailIdParameter = accountDetailId != null ?
                new ObjectParameter("AccountDetailId", accountDetailId) :
                new ObjectParameter("AccountDetailId", typeof(string));
    
            var noParameter = no != null ?
                new ObjectParameter("No", no) :
                new ObjectParameter("No", typeof(string));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var accountIdParameter = accountId != null ?
                new ObjectParameter("AccountId", accountId) :
                new ObjectParameter("AccountId", typeof(string));
    
            var updatedByParameter = updatedBy != null ?
                new ObjectParameter("UpdatedBy", updatedBy) :
                new ObjectParameter("UpdatedBy", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_AccountDetailUpdate", accountDetailIdParameter, noParameter, nameParameter, accountIdParameter, updatedByParameter);
        }
    
        public virtual int sp_AccountDetailDelete(string accountDetailId)
        {
            var accountDetailIdParameter = accountDetailId != null ?
                new ObjectParameter("AccountDetailId", accountDetailId) :
                new ObjectParameter("AccountDetailId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_AccountDetailDelete", accountDetailIdParameter);
        }
    
        public virtual ObjectResult<sp_AccountDetailsDDL_Result> sp_AccountDetailsDDL()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_AccountDetailsDDL_Result>("sp_AccountDetailsDDL");
        }
    }
}
