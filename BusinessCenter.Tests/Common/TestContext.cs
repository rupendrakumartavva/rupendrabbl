using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data;
using BusinessCenter.Tests.Setup;

namespace BusinessCenter.Tests.Common
{
    public class TestContext : DbContext
    {
        public TestContext()
            : base("Name=TestContext")
        {

        }
        public TestContext(bool enableLazyLoading, bool enableProxyCreation)
            : base("Name=TestContext")
        {
            Configuration.ProxyCreationEnabled = enableProxyCreation;
            Configuration.LazyLoadingEnabled = enableLazyLoading;
        }
        public TestContext(DbConnection connection)
            : base(connection, true)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<DCBC_ENTITY_ABRA> DCBC_ENTITY_ABRA { get; set; }

        public DbSet<StreetTypes> StreetTypes { get; set; }
        public DbSet<DCBC_ENTITY_BBL> DCBC_ENTITY_BBL { get; set; }
        public DbSet<DCBC_ENTITY_CBE> DCBC_ENTITY_CBE { get; set; }
        public DbSet<DCBC_ENTITY_CORP> DCBC_ENTITY_CORP { get; set; }
        public DbSet<DCBC_ENTITY_OPLA> DCBC_ENTITY_OPLA { get; set; }
        public DbSet<DCBC_ENTITY_Cof_O> DCBC_ENTITY_Cof_O { get; set; }
        public DbSet<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> DCBC_ENTITY_MultiColumn_LOOKUP_INDEX { get; set; }

        public DbSet<MasterBusinessActivity> MasterBusinessActivity { get; set; }
        public DbSet<MasterPrimaryCategory> MasterPrimaryCategory { get; set; }
        public DbSet<MasterSecondaryLicenseCategory> MasterSecondaryLicenseCategory { get; set; }
        public DbSet<MasterSubCategory> MasterSubCategory { get; set; }
        public DbSet<MasterCategoryDocument> MasterCategoryDocument { get; set; }
        public DbSet<MasterCategoryQuestion> MasterCategoryQuestion { get; set; }
        public DbSet<MastereHOPEligibility> MastereHOPEligibility { get; set; }
        public DbSet<FixFee> FixFee { get; set; }
        //public DbSet<MasterTaxRevenue> MasterTaxRevenue { get; set; }
        public DbSet<SecurityQuestion> SecurityQuestion { get; set; }
        public DbSet<FeeCodeMap> FeeCodeMap { get; set; }
      //  public DbSet<MasterRegisteredAgent> MasterRegisteredAgent { get; set; }
        public DbSet<OSub_Category_Fees> OSub_Category_Fees { get; set; }
        public DbSet<DCBC_ENTITY_BBL_Renewals> DCBC_ENTITY_BBL_Renewals { get; set; }
      //  public DbSet<MasterLicense_Renewal_TaxRevenue> masterLicense_Renewal_TaxRevenue { get; set; }
        public DbSet<DCBC_ENTITY_BBL_Renewal_Invoice> DCBC_ENTITY_BBL_Renewal_Invoice { get; set; }
        public DbSet<UserBBLService> UserBBLService { get; set; }
        public DbSet<SubmissiontoAccela> SubmissiontoAccela { get; set; }
        public DbSet<SubmissionBblAssociationToUsers> SubmissionBblAssociationToUsers { get; set; }
        public DbSet<User> User { get; set; }

        public DbSet<SubmissionCategory> SubmissionCategory { get; set; }
      
        public DbSet<SubmissionCofo_Hop_Ehop> SubmissionCofo_Hop_Ehop { get; set; }
        public DbSet<SubmissionCofo_Hop_Ehop_Address> SubmissionCofo_Hop_Ehop_Address { get; set; }
        public DbSet<SubmissionCorporation_Agent> SubmissionCorporation_Agent { get; set; }
        public DbSet<SubmissionCorporation_Agent_Address> SubmissionCorporation_Agent_Address { get; set; }
        public DbSet<MasterCategoryPhysicalLocation> MasterCategoryPhysicalLocation { get; set; }


        public DbSet<SubmissionDocument> SubmissionDocument { get; set; }
        public DbSet<SubmissionDocumentToAccela> SubmissionDocumentToAccela { get; set; }
        public DbSet<SubmissionEHOPEligibility> SubmissionEHOPEligibility { get; set; }
        public DbSet<SubmissionIndividual> SubmissionIndividual { get; set; }
        public DbSet<SubmissionMaster> SubmissionMaster { get; set; }
        public DbSet<SubmissionMaster_ApplicationCheckList> SubmissionMaster_ApplicationCheckList { get; set; }


        public DbSet<SubmissionMasterRenewal> SubmissionMasterRenewal { get; set; }
        public DbSet<SubmissionQuestion> SubmissionQuestion { get; set; }
        public DbSet<SubmissionTaxRevenue> SubmissionTaxRevenue { get; set; }
        public DbSet<PaymentAddressDetails> PaymentAddressDetails { get; set; }
        public DbSet<PaymentCardDetails> PaymentCardDetails { get; set; }
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Lookup_ExistingCategories> Lookup_ExistingCategories { get; set; }
        public DbSet<Lookup_BusinessStructure> Lookup_BusinessStructure { get; set; }
        public DbSet<KeywordDetails> KeywordDetails { get; set; }

        public DbSet<KeywordMaster> KeywordMaster { get; set; }
        public DbSet<UserService> UserService { get; set; }

        public DbSet<UserPasswordTracking> UserPasswordTracking { get; set; }

        public DbSet<MasterCountry> MasterCountry { get; set; }

        public DbSet<SubmissionSelfCertification> SubmissionSelfCertification { get; set; }

        public DbSet<MasterState> MasterState { get; set; }
        public DbSet<UserLoginHistory> UserLoginHistory { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<BblLicenseView3> BblLicenseView3 { get; set; }

        public DbSet<BblLicenseView4> BblLicenseView4 { get; set; }

        public DbSet<SubmissionGeneratedDocument> SubmissionGeneratedDocument { get; set; }

        public DbSet<BblLicenseView> BblLicenseView { get; set; }

        public DbSet<MasterEhopOptionType> MasterEhopOptionType { get; set; }

        public DbSet<TBL_ETL_Address_And_Parcel> TBL_ETL_Address_And_Parcel { get; set; }

        public DbSet<RefreshTokens> RefreshTokens { get; set; }

        public DbSet<Portal_Content_Errors> Portal_Content_Errors { get; set; }
        public DbSet<SubmissionLicenseNumberCounter> SubmissionLicenseNumberCounter { get; set; }

        public DbSet<MasterRenewalStatusFee> MasterRenewalStatusFee { get; set; }
        public DbSet<MasterBblApplicationStatus> MasterBblApplicationStatus { get; set; }
        public DbSet<MailTemplate> MailTemplate { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Suppress code first model migration check          
            Database.SetInitializer<TestContext>(new AlwaysCreateInitializer());

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public void Seed(TestContext context)
        {
            //DCBC_ENTITY_ABRA
            /*var abraData = new AbraRepositoryData();
            context.DCBC_ENTITY_ABRA.AddRange(abraData.AbraEntitiesList);
            context.SaveChanges();*/
            
        }

        /*public class DropCreateIfChangeInitializer : DropCreateDatabaseIfModelChanges<TestContext>
        {
            protected override void Seed(TestContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }

        public class CreateInitializer : CreateDatabaseIfNotExists<TestContext>
        {
            protected override void Seed(TestContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }*/

        public class AlwaysCreateInitializer : DropCreateDatabaseAlways<TestContext>
        {
            protected override void Seed(TestContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }
 
    }
}
