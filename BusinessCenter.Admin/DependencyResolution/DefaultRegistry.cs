// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using BusinessCenter.Admin.Common;
using BusinessCenter.Admin.Interface;
using BusinessCenter.Admin.Models;
using BusinessCenter.Data;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Implementation;
using BusinessCenter.Data.Interface;
using BusinessCenter.Email;
using BusinessCenter.Identity;
using BusinessCenter.Identity.IdentityModels;
using BusinessCenter.Identity.Implementation;
using BusinessCenter.Identity.Interfaces;
using BusinessCenter.Service.Implementation;
using BusinessCenter.Service.Interface;
using BussinessCenter.reCaptcha;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using System.Data.Entity;
using System.Web;

namespace BusinessCenter.Admin.DependencyResolution
{
    public class DefaultRegistry : Registry
    {
        #region Constructors and Destructors

        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.With(new ControllerConvention());
                });

            #region Email and reCaptcha

            For<IEmailTemplate>().Use<EmailTemplate>();
            For<IReCaptcha>().Use<ReCaptcha>();

            #endregion Email and reCaptcha
        }

        #endregion Constructors and Destructors
    }

    public class BussinessIdentityRegistry : Registry
    {
        public BussinessIdentityRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.With(new ControllerConvention());
                });

            #region Asp.netIdentity

            For<IUserManager>().Use<UserManager>();

            For<IUserRoleManager>().Use<UserRoleManager>();

            For<ApplicationUserManager>()
               .Use(() => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>());

            For<ApplicationRoleManager>()
             .Use(() => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationRoleManager>());

            For<ApplicationSignInManager>()
                .Use(() => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationSignInManager>());

            For<ApplicationDbContext>().Use<ApplicationDbContext>(() => new ApplicationDbContext());

            For<IAuthenticationManager>().Use(c => HttpContext.Current.GetOwinContext().Authentication);
            For<IUserStore<AppUser, string>>()
                .Use
                <
                    UserStore
                        <AppUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
                    >()
                .Ctor<ApplicationDbContext>();

            For<IRoleStore<ApplicationRole, string>>()
             .Use
             <
                 RoleStore<ApplicationRole, string, ApplicationUserRole>

                 >()
             .Ctor<ApplicationDbContext>();

            #endregion Asp.netIdentity
        }
    }

    public class BussinessRepositoryRegistry : Registry
    {
        public BussinessRepositoryRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.With(new ControllerConvention());
                });

            #region Repository Pattern

            For<DbContext>().Use(() => new IdentityConnectionContext());
            For<IUnitOfWork>().Use<UnitOfWork>();
            For<ISecurityRepository>().Use<SecurityRepository>();
            For<ISecurityQuestionsService>().Use<SecurityQuestionService>();
            For<ISearchRepository>().Use<SearchRepository>();
            For<ISearchService>().Use<SearchService>();
            For<IUserServicesRepository>().Use<UserServicesRepository>();
            For<IMyServiceDetails>().Use<MyServiceDetails>();
            For<ISearchKeywordService>().Use<SearchKeywordService>();
            For<ISearchKeywordRepository>().Use<SearchKeywordRepository>();
            For<IUserManagerRepository>().Use<UserManagerRepository>();
            For<IUserManagerService>().Use<UserManagerService>();
            For<IBBLAssociateService>().Use<BBLAssociateService>();
            For<IMasterBusinessActivityService>().Use<MasterBusinessActivityService>();
            For<IMasterPrimaryCategoryService>().Use<MasterPrimaryCategoryService>();
            For<IMasterSecondaryLicenseCategoryService>().Use<MasterSecondaryLicenseCategoryService>();

            For<ISubmissionCategoryService>().Use<SubmissionCategoryService>();
            For<ISubmissionMasterService>().Use<SubmissionMasterService>();
            For<ISubmissionQuestionService>().Use<SubmissionQuestionService>();
            For<IMasterCategoryPhysicalLocationService>().Use<MasterCategoryPhysicalLocationService>();
            For<IMasterCategoryDocumentRepository>().Use<MasterCategoryDocumentRepository>();
            For<IAbraRepository>().Use<AbraRepository>();
            For<IBblRepository>().Use<BblRepository>();
            For<ICbeRepository>().Use<CbeRepository>();
            For<IOplaRepository>().Use<OplaRepository>();
            For<ICorpRespository>().Use<CorpRespository>();
            For<IKeywordDetailsRepository>().Use<KeywordDetailsRepository>();
            For<IUserRepository>().Use<UserRepository>();
            For<IUserRoleRepository>().Use<UserRoleRepository>();
            //   For<IBBLPinRepository>().Use<BBLPinRepository>();
            For<IMasterBusinessActivityRepository>().Use<MasterBusinessActivityRepository>();
            For<IUserBBLServiceRepository>().Use<UserBBLServiceRepository>();
            For<IMasterPrimaryCategoryRepository>().Use<MasterPrimaryCategoryRepository>();
            For<IMasterSecondaryLicenseCategoryRepository>().Use<MasterSecondaryLicenseCategoryRepository>();
            //For<ISubmissionRepository>().Use<SubmissionRepository>();
            For<ISubmissionCategoryRepository>().Use<SubmissionCategoryRepository>();
            For<ISubmissionMasterRepository>().Use<SubmissionMasterRepository>();
            For<ISubmissionQuestionRepository>().Use<SubmissionQuestionRepository>();
            For<IMasterCategoryPhysicalLocationRepository>().Use<MasterCategoryPhysicalLocationRepository>();
            For<IFixFeeRepository>().Use<FixFeeRepository>();
            For<IOSubCategoryFeesRepository>().Use<OSubCategoryFeesRepository>();
            For<ISubmissionDocumentRepository>().Use<SubmissionDocumentRepository>();
            For<ISubmissionIndividualRepository>().Use<SubmissionIndividualRepository>();
            For<ISubmissionIndividualService>().Use<SubmissionIndividualService>();
            For<IMasterSubCategoryRepository>().Use<MasterSubCategoryRepository>();
            For<IMasterSubCategoryService>().Use<MasterSubCategoryService>();
            For<IMasterCategoryQuestionRepository>().Use<MasterCategoryQuestionRepository>();
            For<IFeeCodeMapRepository>().Use<FeeCodeMapRepository>();

            For<ISubmissionTaxRevenueRepository>().Use<SubmissionTaxRevenueRepository>();

            For<ISubmissionCofoHopeHopAddressRepository>().Use<SubmissionCofoHopeHopAddressRepository>();
            For<ISubmissionCofoHopeHopAddressService>().Use<SubmissionCofoHopeHopAddressService>();

            For<ISubmissionTaxRevenueService>().Use<SubmissionTaxRevenueService>();
            For<ISubmissionCorporationRepository>().Use<SubmissionCorporationRepository>();
            //For<IwebServiceData>().Use<WebServiceData>();
            For<ISubmissionMasterApplicationChcekListRepository>().Use<SubmissionMasterApplicationChcekListRepository>();

            For<IStreetTypesRepository>().Use<StreetTypesRepository>();
            //  For<ICofoHopDetailsRepository>().Use<CofoHopDetailsRepository>();
            For<ICofoHopDetailsService>().Use<CofoHopDetailsService>();
            For<IMastereHopEligibilityRepository>().Use<MastereHopEligibilityRepository>();
            For<IMastereHopEligibilityService>().Use<MastereHopEligibilityService>();

            For<IMastereHopEligibilityService>().Use<MastereHopEligibilityService>();
            //
            //  For<IMasterTaxRevenueService>().Use<MasterTaxRevenueService>();
            For<ISubmissionCofoHopeHopRepository>().Use<SubmissionCofoHopeHopRepository>();
            //For<IMasterTaxRevenueRepository>().Use<MasterTaxRevenueRepository>();
            For<IMasterRegisterAgentRepository>().Use<MasterRegisterAgentRepository>();
            For<ISubmissionCorporationAgentAddressRepository>().Use<SubmissionCorporationAgentAddressRepository>();
            For<ISubmissionEHOPEligibilityRepository>().Use<SubmissionEHOPEligibilityRepository>();
            For<ISubmissionVerficationRepository>().Use<SubmissionVerficationRepository>();
            For<IPaymentDetailsRepository>().Use<PaymentDetailsRepository>();
            For<IPaymentCardDetailsRepository>().Use<PaymentCardDetailsRepository>();
            For<IPaymentAddressDetailsRepository>().Use<PaymentAddressDetailsRepository>();
            For<IRenewRepository>().Use<RenewRepository>();
            // For<IMasterLicenseFEINRenewal>().Use<MasterLicenseFEINRenewal>();
            For<IRenewalService>().Use<RenewalService>();
            For<ISubmissionMasterRenewalRepository>().Use<SubmissionMasterRenewalRepository>();
            For<IDCBC_ENTITY_Cof_ORepository>().Use<DCBC_ENTITY_Cof_ORepository>();
            For<IDCBC_ENTITY_BBL_RenewalsRepository>().Use<DCBC_ENTITY_BBL_RenewalsRepository>();
            For<IDCBC_ENTITY_BBL_Renewal_InvoiceRepository>().Use<DCBC_ENTITY_BBL_Renewal_InvoiceRepository>();
            For<ISubmissionBblAssociationToUsersRepository>().Use<SubmissionBblAssociationToUsersRepository>();
            For<ISubmissionToAccelaRepository>().Use<SubmissionToAccelaRepository>();
            For<IFeeCodeMapService>().Use<FeeCodeMapService>();
            For<IOSubCategoryFeeService>().Use<OSubCategoryFeeService>();
            For<ISubmissionDocumentToAccelaRepository>().Use<SubmissionDocumentToAccelaRepository>();
            //For<ITokenAuthenticationRepository>().Use<TokenAuthenticationRepository>();
            For<ILookup_ExistingCategoriesRepository>().Use<Lookup_ExistingCategoriesRepository>();
            For<ILookup_BusinessStructureRepository>().Use<Lookup_BusinessStructureRepository>();
            For<IMasterCountryService>().Use<MasterCountryService>();
            For<IMasterCountryRepository>().Use<MasterCountryRepository>();
            For<IUserPasswordTrackingRepository>().Use<UserPasswordTrackingRepository>();
            For<IUserPasswordTrackingService>().Use<UserPasswordTrackingService>();
            For<IRefreshTokensRepository>().Use<RefreshTokensRepository>();
            For<ISubmissionSelfCertificationRepository>().Use<SubmissionSelfCertificationRepository>();
            For<ISubmissionSelfCertificationService>().Use<SubmissionSelfCertificationService>();
            //SubmissionSelfCertificationRepository

            For<IMasterStateRepository>().Use<MasterStateRepository>();
            For<IMasterStateService>().Use<MasterStateService>();
            For<IRoleRepository>().Use<RoleRepository>();

            For<IEtlAddressAndParcelRepository>().Use<EtlAddressAndParcelRepository>();
            For<IEtlAddressAndParcelService>().Use<EtlAddressAndParcelService>();

            For<IMasterEhopOptionType>().Use<MasterEhopOptionTypeRepository>();

            For<ISubmissionGeneratedDocumentRepository>().Use<SubmissionGeneratedDocumentRepository>();

            For<ISubmissionGeneratedDocumentService>().Use<SubmissionGeneratedDocumentService>();
            For<ISubmissionDocumentToAccelaService>().Use<SubmissionDocumentToAccelaService>();
            For<ISubmissionToAccelaService>().Use<SubmissionToAccelaService>();
            For<IBusinessInformationRepository>().Use<BusinessInformationRepository>();
            For<IBusinessInformationService>().Use<BusinessInformationService>();
            For<IDocumentsView4Repository>().Use<DocumentsView4Repository>();
            For<IDocumentsView4Service>().Use<DocumentsView4Service>();
            For<IRenewalView3Repository>().Use<RenewalView3Repository>();
            For<IRenewalView3Service>().Use<RenewalView3Service>();
            For<IPortal_Content_ErrorsRepository>().Use<Portal_Content_ErrorsRepository>();
            For<IPortal_Content_ErrorsService>().Use<Portal_Content_ErrorsService>();
            For<ISubmissionLicenseNumberCounterRepository>().Use<SubmissionLicenseNumberCounterRepository>();
            For<IMasterRenewalStatusFeeRepository>().Use<MasterRenewalStatusFeeRepository>();
            For<IPaymentHistoryDetailsRepository>().Use<PaymentHistoryDetailsRepository>();
            For<IMasterBblApplicationStatusRepository>().Use<MasterBblApplicationStatusRepository>();
            For<IMailTemplateRepository>().Use<MailTemplateRepository>();
            For<IMailTemplateService>().Use<MailTemplateService>();
            For<IBusinessCenterCommon>().Use<BusinessCenterCommon>();

            #endregion Repository Pattern

            // #region Repository Pattern

            // For<DbContext>().Use(() => new IdentityConnectionContext());
            // For<IUnitOfWork>().Use<UnitOfWork>();
            // For<ISecurityRepository>().Use<SecurityRepository>();
            // For<ISecurityQuestionsService>().Use<SecurityQuestionService>();
            // For<ISearchRepository>().Use<SearchRepository>();
            // For<ISearchService>().Use<SearchService>();
            // For<IUserServicesRepository>().Use<UserServicesRepository>();
            // For<IMyServiceDetails>().Use<MyServiceDetails>();
            // For<ISearchKeywordService>().Use<SearchKeywordService>();
            // For<ISearchKeywordRepository>().Use<SearchKeywordRepository>();
            // For<IUserManagerRepository>().Use<UserManagerRepository>();
            // For<IUserManagerService>().Use<UserManagerService>();

            // For<IAbraRepository>().Use<AbraRepository>();
            // For<IBblRepository>().Use<BblRepository>();
            // For<ICbeRepository>().Use<CbeRepository>();
            // For<IOplaRepository>().Use<OplaRepository>();
            // For<ICorpRespositry>().Use<CorpRespository>();

            // For<IUserRepository>().Use<UserRepository>();
            // For<IUserRoleRepository>().Use<UserRoleRepository>();
            // For<IGridBindings>().Use<GridBindings>();

            // // BBL
            // For<IBussinessActivityRepository>().Use<BussinessActivityRepository>();
            // For<IBBLBusinessActivityService>().Use<BBLBusinessActivityService>();

            // For<IPrimaryCategoryRepository>().Use<PrimaryCategoryRepository>();
            // For<IPrimaryCategoryService>().Use<PrimaryCategoryService>();

            // For<ICategoryphysicallocationRepository>().Use<CategoryphysicallocationRepository>();
            // For<ICategoryphysicallocationService>().Use<CategoryphysicallocationService>();

            // For<ISecondaryCategoryRepository>().Use<SecondaryCategoryRepository>();
            // For<ISecondaryCategoryService>().Use<SecondaryCategoryService>();

            // For<ICategoryQuestionRepository>().Use<CategoryQuestionRepository>();

            // For<IMasterCategoryDocumentRepository>().Use<MasterCategoryDocumentRepository>();
            // For<IOSubCategoryFeesRepository>().Use<OSubCategoryFeesRepository>();
            // For<IBBLAdminService>().Use<BBLAdminService>();

            // For<ISubmissionCategoryRepository>().Use<SubmissionCategoryRepository>();
            // For<IUserBBLServiceRepository>().Use<UserBBLServiceRepository>();
            // For<ISubmissionQuestionRepository>().Use<SubmissionQuestionRepository>();
            // For<IBblRepository>().Use<BblRepository>();
            //For<ICategoryphysicallocationRepository>().Use<CategoryphysicallocationRepository>();
            // For<ISubmissionChcekListAppRepository>().Use<SubmissionChcekListAppRepository>();
            // For<ISubmissionMasterRepository>().Use <SubmissionMasterRepository>();

            // For<ISubmissionMasterService>().Use<SubmissionMasterService>();
            // #endregion
        }
    }
}