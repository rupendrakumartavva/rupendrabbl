﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessCenter.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class IdentityConnectionContext : DbContext
    {
        public IdentityConnectionContext()
            : base("name=IdentityConnectionContext")
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.UseDatabaseNullSemantics = true;
            var objectContextAdapter = this as IObjectContextAdapter;
            objectContextAdapter.ObjectContext.ContextOptions.UseCSharpNullComparisonBehavior = true;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AuditRecord> AuditRecord { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<FeeCodeMap> FeeCodeMap { get; set; }
        public virtual DbSet<FixFee> FixFee { get; set; }
        public virtual DbSet<KeywordMaster> KeywordMaster { get; set; }
        public virtual DbSet<Lookup_BusinessStructure> Lookup_BusinessStructure { get; set; }
        public virtual DbSet<Lookup_ExistingCategories> Lookup_ExistingCategories { get; set; }
        public virtual DbSet<MailTemplate> MailTemplate { get; set; }
        public virtual DbSet<MasterBblApplicationStatus> MasterBblApplicationStatus { get; set; }
        public virtual DbSet<MasterBusinessActivity> MasterBusinessActivity { get; set; }
        public virtual DbSet<MasterCategory> MasterCategory { get; set; }
        public virtual DbSet<MasterCategoryDocument> MasterCategoryDocument { get; set; }
        public virtual DbSet<MasterCategoryPhysicalLocation> MasterCategoryPhysicalLocation { get; set; }
        public virtual DbSet<MasterCategoryQuestion> MasterCategoryQuestion { get; set; }
        public virtual DbSet<MasterCountry> MasterCountry { get; set; }
        public virtual DbSet<MastereHOPEligibility> MastereHOPEligibility { get; set; }
        public virtual DbSet<MasterEhopOptionType> MasterEhopOptionType { get; set; }
        public virtual DbSet<MasterPrimaryCategory> MasterPrimaryCategory { get; set; }
        public virtual DbSet<MasterRegisteredAgent> MasterRegisteredAgent { get; set; }
        public virtual DbSet<MasterRenewalStatusFee> MasterRenewalStatusFee { get; set; }
        public virtual DbSet<MasterSecondaryLicenseCategory> MasterSecondaryLicenseCategory { get; set; }
        public virtual DbSet<MasterState> MasterState { get; set; }
        public virtual DbSet<MasterSubCategory> MasterSubCategory { get; set; }
        public virtual DbSet<OSub_Category_Fees> OSub_Category_Fees { get; set; }
        public virtual DbSet<PaymentAddressDetails> PaymentAddressDetails { get; set; }
        public virtual DbSet<PaymentCardDetails> PaymentCardDetails { get; set; }
        public virtual DbSet<PaymentDetails> PaymentDetails { get; set; }
        public virtual DbSet<PaymentHistoryDetails> PaymentHistoryDetails { get; set; }
        public virtual DbSet<Portal_Content_Errors> Portal_Content_Errors { get; set; }
        public virtual DbSet<RefreshTokens> RefreshTokens { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<SecurityQuestion> SecurityQuestion { get; set; }
        public virtual DbSet<StreetTypes> StreetTypes { get; set; }
        public virtual DbSet<SubmissionBblAssociationToUsers> SubmissionBblAssociationToUsers { get; set; }
        public virtual DbSet<SubmissionCategory> SubmissionCategory { get; set; }
        public virtual DbSet<SubmissionCofo_Hop_Ehop> SubmissionCofo_Hop_Ehop { get; set; }
        public virtual DbSet<SubmissionCofo_Hop_Ehop_Address> SubmissionCofo_Hop_Ehop_Address { get; set; }
        public virtual DbSet<SubmissionCorporation_Agent> SubmissionCorporation_Agent { get; set; }
        public virtual DbSet<SubmissionCorporation_Agent_Address> SubmissionCorporation_Agent_Address { get; set; }
        public virtual DbSet<SubmissionDocument> SubmissionDocument { get; set; }
        public virtual DbSet<SubmissionDocumentToAccela> SubmissionDocumentToAccela { get; set; }
        public virtual DbSet<SubmissionEHOPEligibility> SubmissionEHOPEligibility { get; set; }
        public virtual DbSet<SubmissionGeneratedDocument> SubmissionGeneratedDocument { get; set; }
        public virtual DbSet<SubmissionIndividual> SubmissionIndividual { get; set; }
        public virtual DbSet<SubmissionLicenseNumberCounter> SubmissionLicenseNumberCounter { get; set; }
        public virtual DbSet<SubmissionMaster> SubmissionMaster { get; set; }
        public virtual DbSet<SubmissionMaster_ApplicationCheckList> SubmissionMaster_ApplicationCheckList { get; set; }
        public virtual DbSet<SubmissionMasterRenewal> SubmissionMasterRenewal { get; set; }
        public virtual DbSet<SubmissionQuestion> SubmissionQuestion { get; set; }
        public virtual DbSet<SubmissionSelfCertification> SubmissionSelfCertification { get; set; }
        public virtual DbSet<SubmissionTaxRevenue> SubmissionTaxRevenue { get; set; }
        public virtual DbSet<SubmissiontoAccela> SubmissiontoAccela { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserBBLService> UserBBLService { get; set; }
        public virtual DbSet<UserClaim> UserClaim { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }
        public virtual DbSet<UserLoginHistory> UserLoginHistory { get; set; }
        public virtual DbSet<UserPasswordTracking> UserPasswordTracking { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserService> UserService { get; set; }
        public virtual DbSet<DCBC_ENTITY_ABRA> DCBC_ENTITY_ABRA { get; set; }
        public virtual DbSet<DCBC_ENTITY_BBL> DCBC_ENTITY_BBL { get; set; }
        public virtual DbSet<DCBC_ENTITY_BBL_Renewal_Invoice> DCBC_ENTITY_BBL_Renewal_Invoice { get; set; }
        public virtual DbSet<DCBC_ENTITY_CBE> DCBC_ENTITY_CBE { get; set; }
        public virtual DbSet<DCBC_ENTITY_Cof_O> DCBC_ENTITY_Cof_O { get; set; }
        public virtual DbSet<DCBC_ENTITY_CORP> DCBC_ENTITY_CORP { get; set; }
        public virtual DbSet<DCBC_ENTITY_LOOKUP_INDEX> DCBC_ENTITY_LOOKUP_INDEX { get; set; }
        public virtual DbSet<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> DCBC_ENTITY_MultiColumn_LOOKUP_INDEX { get; set; }
        public virtual DbSet<DCBC_ENTITY_OPLA> DCBC_ENTITY_OPLA { get; set; }
        public virtual DbSet<DCBC_ENTITY_RESULTS_ABRA> DCBC_ENTITY_RESULTS_ABRA { get; set; }
        public virtual DbSet<DCBC_ENTITY_RESULTS_BBL> DCBC_ENTITY_RESULTS_BBL { get; set; }
        public virtual DbSet<DCBC_ENTITY_RESULTS_CBE> DCBC_ENTITY_RESULTS_CBE { get; set; }
        public virtual DbSet<DCBC_ENTITY_RESULTS_Corp> DCBC_ENTITY_RESULTS_Corp { get; set; }
        public virtual DbSet<DCBC_ENTITY_RESULTS_OPLA> DCBC_ENTITY_RESULTS_OPLA { get; set; }
        public virtual DbSet<OSub_Category_Checklist> OSub_Category_Checklist { get; set; }
        public virtual DbSet<TBL_ETL_Address_And_Parcel> TBL_ETL_Address_And_Parcel { get; set; }
        public virtual DbSet<BblLicenseView2> BblLicenseView2 { get; set; }
        public virtual DbSet<BblLicenseView4> BblLicenseView4 { get; set; }
        public virtual DbSet<BblLicenseView3> BblLicenseView3 { get; set; }
        public virtual DbSet<BblLicenseView> BblLicenseView { get; set; }
        public virtual DbSet<KeywordDetails> KeywordDetails { get; set; }
        public virtual DbSet<DCBC_ENTITY_BBL_Renewals> DCBC_ENTITY_BBL_Renewals { get; set; }
    }
}