using BusinessCenter.Common;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessCenter.Data.Implementation
{
    public class SubmissionMasterRepository : GenericRepository<SubmissionMaster>, ISubmissionMasterRepository
    {
        protected ISubmissionCategoryRepository SubmissionCategoryRepo;
        protected IUserBBLServiceRepository Userbblrepository;
        protected ISubmissionQuestionRepository SubmissionCategoryQuestionRepo;
        protected IBblRepository Bblrepo;
        protected IMasterCategoryPhysicalLocationRepository MasterCategoryPhysicalLocationRepo;
        protected IMasterPrimaryCategoryRepository MasterPrimaryCategoryRepo;
        protected ISubmissionMasterApplicationChcekListRepository SubmissionApplicationCheckListRepo;
        protected IUserRepository Userrepo;
        protected IMasterBusinessActivityRepository MasterBusinessActivityRepo;
        protected IDCBC_ENTITY_BBL_RenewalsRepository BblRenewalRepo;
        protected ISubmissionBblAssociationToUsersRepository SubmissionBblAssociationToUserRepo;
        protected ISubmissionToAccelaRepository SubmissionAccelaRepo;
        protected IMasterSecondaryLicenseCategoryRepository MasterSecondaryLicenseCategoryRepo;
        protected ILookup_ExistingCategoriesRepository LookupExistingCategoriesRepo;
        protected IMasterBblApplicationStatusRepository masterbblapplicationRepo;

        protected ISubmissionLicenseNumberCounterRepository _submissionLicenseNumberCounterRepository;

        private int _expirySoon = 0;
        private int _expired = 0;
        private int _active = 0;
        private int _drafts = 0;
        private int _renew = 0;
        private int _eHop = 0;
        private int _underReview = 0;
        private int _lapsed = 0;
        private int _renewNotallowed = 0;
        private int _resultExpireSoonCount = 0;

        public SubmissionMasterRepository(IUnitOfWork context, ISubmissionCategoryRepository submissioncategoryrepo,
            IBblRepository bblrepo,
            IUserBBLServiceRepository userbblrepo,
            ISubmissionQuestionRepository userquestionrepo,
            IMasterCategoryPhysicalLocationRepository catphysicalrepo,
            IMasterPrimaryCategoryRepository primaryRepository,
            ISubmissionMasterApplicationChcekListRepository subcheckrepository,
            IUserRepository userrepository,
            IDCBC_ENTITY_BBL_RenewalsRepository renewrepository,
            IMasterBusinessActivityRepository businessrepository,
            ISubmissionBblAssociationToUsersRepository trasferrepsitory,
            ISubmissionToAccelaRepository submissionAccelaRepo,
            IMasterSecondaryLicenseCategoryRepository secondaryrepository
            , ILookup_ExistingCategoriesRepository lookupExistingCategoriesRepository,
            ISubmissionLicenseNumberCounterRepository submissionLicenseNumberCounterRepository,
            IMasterBblApplicationStatusRepository masterbblapplicationRepository)
            : base(context)
        {
            SubmissionCategoryRepo = submissioncategoryrepo;
            Userbblrepository = userbblrepo;
            SubmissionCategoryQuestionRepo = userquestionrepo;
            Bblrepo = bblrepo;
            MasterCategoryPhysicalLocationRepo = catphysicalrepo;
            MasterPrimaryCategoryRepo = primaryRepository;
            SubmissionApplicationCheckListRepo = subcheckrepository;
            Userrepo = userrepository;
            MasterBusinessActivityRepo = businessrepository;
            BblRenewalRepo = renewrepository;
            SubmissionBblAssociationToUserRepo = trasferrepsitory;
            SubmissionAccelaRepo = submissionAccelaRepo;
            MasterSecondaryLicenseCategoryRepo = secondaryrepository;
            LookupExistingCategoriesRepo = lookupExistingCategoriesRepository;
            _submissionLicenseNumberCounterRepository = submissionLicenseNumberCounterRepository;
            masterbblapplicationRepo = masterbblapplicationRepository;
        }
        /// <summary>
        /// This method is used to get all submission master
        /// </summary>
        /// <returns>Return submission master</returns>
        public IEnumerable<SubmissionMaster> AllSubmissionMaster()
        {
            return GetAll().AsQueryable();
        }
        /// <summary>
        /// This method is used to get all users data
        /// </summary>
        /// <returns>Return Users</returns>
        public IEnumerable<User> FindByUser()
        {
            return Userrepo.GetUserLookupAll();
        }
        /// <summary>
        /// This method is used to get particular user data based on user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Return user data</returns>
        public IEnumerable<User> FindUserDetails(string userId)
        {
            var userDetails = Userrepo.FindByID(userId);
            return userDetails;
        }

        /// <summary>
        /// This method is used to Associated and Submission License list based on User Id.
        /// </summary>
        /// <param name="bblAsscoiateService"></param>
        /// <returns>Return BBlService</returns>
        public IEnumerable<BBlService> BBlServiceList(BblAsscoiateService bblAsscoiateService)
        {
            try
            {
                var bblService = new List<BBlService>();
                var serviceData = new List<BblServiceList>();
                var servicecount = new List<BBLServiceCount>();
                string username = "";

                var checkbblupdate = (from user in Userbblrepository.FindByUserStatusID(bblAsscoiateService.UserID)
                                      join
                                          submission in FindBy(x => x.UserID == bblAsscoiateService.UserID)
                                          on user.ServiceID.ToString() equals submission.UserBblAssociateId
                                      where
                                          user.ServiceID.ToString() == submission.UserBblAssociateId && user.Type.Trim().ToUpper() == "S"
                                      select new
                                      {
                                          user.ServiceID,
                                          user.SubmissionLicense,
                                          user.Type,
                                          user.B1_ALT_ID,
                                          submission.MasterId,
                                          mastersubmission = submission.SubmissionLicense
                                      }).AsEnumerable();

                var getDetails = (from x in checkbblupdate select x.SubmissionLicense).ToList();
                var getMasterbblapplicationRepo = masterbblapplicationRepo.GetAllStatus().ToList();
                var applicationbbldata =
                    (from bblrepositorydata in Bblrepo.FindBySingle(x => getDetails.Contains(x.APPLICATION_CAP)) where bblrepositorydata.B1_ALT_ID!=null
                     select
                         new
                         {
                             bblrepositorydata.B1_APPL_STATUS,
                             bblrepositorydata.B1_ALT_ID,
                             bblrepositorydata.Expiration_Date,
                             bblrepositorydata.APPLICATION_CAP,
                             bblrepositorydata.DCBC_ENTITY_ID
                         }).ToList();
                foreach (var appbbldata in applicationbbldata)
                {
                    var submissionmasterid = checkbblupdate.FirstOrDefault(x => x.SubmissionLicense == appbbldata.APPLICATION_CAP).MasterId;
                    var applicationStatus = GetStringValue(appbbldata.B1_APPL_STATUS).Trim();
                    var status =
                        getMasterbblapplicationRepo.Where(
                            x => x.Application_Cap_Status.ToUpper().Trim() == applicationStatus.ToUpper().Trim());
                    if (status.Any())
                    {
                        applicationStatus = GetStringValue(status.FirstOrDefault().Changed_Cap_Status).Trim();
                    }
                    if (!string.IsNullOrEmpty(applicationStatus))
                    {
                        UpdateLicenseFromAccela(submissionmasterid, appbbldata.B1_ALT_ID,
                            applicationStatus,
                            Convert.ToDateTime(appbbldata.Expiration_Date), appbbldata.DCBC_ENTITY_ID.ToString());
                    }
                }
                var userService = (from user in Userbblrepository.FindByUserStatusID(bblAsscoiateService.UserID)
                                   join
                                       submission in FindBy(x => x.UserID == bblAsscoiateService.UserID)
                                       on user.SubmissionLicense equals submission.SubmissionLicense
                                   where user.ServiceID.ToString() == submission.UserBblAssociateId && user.Type.Trim().ToUpper() == "A"
                                   select new
                                   {
                                       user.ServiceID,
                                       user.SubmissionLicense,
                                       user.Type,
                                       user.B1_ALT_ID,
                                       submission.MasterId,
                                       user.LicenseExpirationDate,
                                       mastersubmission = submission.SubmissionLicense
                                   }).ToList();

                foreach (var userserivce in userService)
                {
                    string serviceid = userserivce.ServiceID.ToString().Trim();
                    string submssionlicense = userserivce.SubmissionLicense.ToString().Trim();

                    if (userserivce.Type.Trim().ToUpper() == "A")
                    {
                        submssionlicense = userserivce.B1_ALT_ID.ToString().Trim();
                    }
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    var bbldetails = Bblrepo.FindBySingle(x => x.B1_ALT_ID.Trim() == submssionlicense.Trim());
                    if (bbldetails.Any())
                    {
                        var getActiveBblData = bbldetails.FirstOrDefault();
                        string expiryStatus =
                            textInfo.ToTitleCase(
                                ServiceSubmissionExpiryStatus(Convert.ToDateTime(getActiveBblData.Expiration_Date)))
                                .Trim();
                        if (expiryStatus.ToUpper().Trim() != "ACTIVE" && userserivce.LicenseExpirationDate != Convert.ToDateTime(getActiveBblData.Expiration_Date))
                        {
                            CheckExpirySoon(submssionlicense, bblAsscoiateService.UserID, serviceid,
                               userserivce.MasterId,
                                getActiveBblData.DCBC_ENTITY_ID.ToString(), getActiveBblData.Expiration_Date);
                        }
                        else if (expiryStatus.ToUpper().Trim() == "ACTIVE" && userserivce.Type.ToUpper().Trim() == "A" && userserivce.LicenseExpirationDate != Convert.ToDateTime(getActiveBblData.Expiration_Date))
                        {
                            var renewallicenseUpdate = FindBy(x => x.MasterId.Trim() == userserivce.MasterId).Single();
                            renewallicenseUpdate.Status = "Active";
                            Update(renewallicenseUpdate, renewallicenseUpdate.MasterId);
                            Save();
                        }
                    }
                }

                var bbluserService = Userbblrepository.FindByUserStatusID(bblAsscoiateService.UserID).ToList();
                var getUserSubmissionMaster = FindBy(x => x.UserID == bblAsscoiateService.UserID).ToList();
                var getBblEntityIds = (from x in bbluserService where x.Type.Trim() == "A" select x.DCBC_ENTITY_ID).ToList();
                var getUserAssociateBbls = Bblrepo.FindBySingle(x => getBblEntityIds.Contains(x.DCBC_ENTITY_ID.ToString())).ToList();
                var getSubmissionBblEntityIds = (from x in bbluserService where x.Type.Trim() == "S" select x.DCBC_ENTITY_ID).ToList();
                var getUserSubmissionBbls = Bblrepo.FindBySingle(x => getSubmissionBblEntityIds.Contains(x.DCBC_ENTITY_ID.ToString())).ToList();
                foreach (var userserivce in bbluserService)
                {
                    var submissionCategoryList = new SubmissionCategoryList();
                    var servicelist = new BblServiceList { UserName = username };

                    switch (userserivce.Type.ToUpper().Trim())
                    {
                        case "A":

                            #region Association

                            servicelist = BindRenewalDetails(userserivce.DCBC_ENTITY_ID, userserivce.UserID, userserivce.SubmissionLicense, userserivce.ServiceID,
                               Convert.ToDateTime(userserivce.LicenseExpirationDate), userserivce.Type, servicelist, getUserSubmissionMaster, getUserAssociateBbls, userserivce.B1_ALT_ID);

                            #endregion Association

                            break;

                        case "S":

                            #region Submission

                            if (userserivce.SubmissionLicense.StartsWith("DAPP"))
                            {
                                servicelist = BindSubmissionWithDappDetails(bblAsscoiateService.UserID, userserivce.SubmissionLicense, "S", getUserSubmissionMaster);
                            }
                            else
                            {
                                var bblEntityData =
                                    getUserSubmissionMaster.FirstOrDefault(x => x.SubmissionLicense == userserivce.SubmissionLicense);

                                if (bblEntityData != null)
                                {
                                   // int submissionCofoid = 0;
                                    var chcekMaster =
                                        getUserSubmissionMaster.Where(
                                            x =>
                                                x.SubmissionLicense == userserivce.SubmissionLicense &&
                                                x.UserID == bblAsscoiateService.UserID);
                                    string masterid;
                                    var submissionMasters = chcekMaster as SubmissionMaster[] ?? chcekMaster.ToArray();
                                    if (submissionMasters.Count() != 0)
                                    {
                                        masterid = submissionMasters.FirstOrDefault().MasterId;
                                        servicelist.TradeName = GetStringValue(submissionMasters.FirstOrDefault().TradeName).Trim();
                                        if (submissionMasters.FirstOrDefault().Updatedate != null)
                                        {
                                            servicelist.PaymentDate = Convert.ToDateTime(submissionMasters.FirstOrDefault().Updatedate).ToString("MM/dd/yyyy");
                                        }
                                        if (submissionMasters.FirstOrDefault().IseHOP == true)
                                        {
                                            var validateEhop = SubmissionApplicationCheckListRepo.FindByMasterId(masterid);
                                            var submissionMasterApplicationCheckLists = validateEhop as IList<SubmissionMaster_ApplicationCheckList> ?? validateEhop.ToList();
                                            if (validateEhop != null && submissionMasterApplicationCheckLists.Any())
                                            {
                                                servicelist.ChcekEhopAllow = "NO";
                                                var submissionMasterApplicationCheckList = submissionMasterApplicationCheckLists.FirstOrDefault();
                                                if (submissionMasterApplicationCheckList != null)
                                                {
                                                    var getDetEhopdetails = submissionMasterApplicationCheckList.IsSubmissioneHop;
                                                    if (getDetEhopdetails == true)
                                                    {
                                                        servicelist.ChcekEhopAllow = "YES";
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            servicelist.ChcekEhopAllow = "NO";
                                        }
                                    }
                                    else
                                    {
                                        masterid = string.Empty;
                                        servicelist.TradeName = string.Empty;
                                    }

                                    servicelist.LrenNumber = "";
                                    servicelist.MasterId = masterid;

                                    servicelist.EntityId = GetStringValue(bblEntityData.SubmissionLicense).Trim();
                                    servicelist.APP_Type = GetStringValue(bblEntityData.App_Type).Trim();
                                    servicelist.GrandTotal = bblEntityData.GrandTotal ?? 0;
                                    servicelist.UserAssociateType = userserivce.Type.ToUpper().Trim();
                                    servicelist.LicNumber = GetStringValue(bblEntityData.SubmissionLicense).Trim();
                                    servicelist.ExpDate = string.Empty;
                                    servicelist.LicenseeFirstLastName = bblEntityData.BusinessName ?? "";
                                    servicelist.BusinessName = bblEntityData.BusinessName ?? "";
                                    servicelist.PremiseAddress = bblEntityData.PremisesAddress ?? "";
                                    var multiplelicenses = SubmissionCategoryRepo.SubmissionCategoryListWithStatus(submissionCategoryList, masterid);

                                    servicelist.CategoryStatus = multiplelicenses.Status;
                                    servicelist.ShowActivePdf = multiplelicenses.IsPDFShow == false ? "NO" : "YES";

                                    if (!string.IsNullOrEmpty(multiplelicenses.CategoryName))
                                    {
                                        if (multiplelicenses.CategoryName.Split(',').Count() > 1)
                                        {
                                            servicelist.LicTypes = "Multiple";
                                            servicelist.MultipleLicense = multiplelicenses.CategoryName;
                                            servicelist.MultipleLicense = multiplelicenses.CategoryName + " ";
                                        }
                                        else
                                        {
                                            servicelist.LicTypes = multiplelicenses.CategoryName;
                                            servicelist.LicTypes = multiplelicenses.CategoryName + " ";
                                        }
                                    }
                                    else
                                    {
                                        servicelist.LicTypes = "";
                                    }
                                    servicelist.SubCategory = multiplelicenses.SubCategory ?? "NA";
                                    var bbldetails =
                                            getUserSubmissionBbls.Where(
                                                x => x.B1_ALT_ID.Trim() == servicelist.LicNumber.Trim()).ToList();
                                    servicelist.AppStatusdate = GetDateTimeValue(bbldetails.Any() ? Convert.ToDateTime(bbldetails.FirstOrDefault().B1_APPL_STATUS_DATE) : Convert.ToDateTime(bblEntityData.Updatedate));
                                    if (bblEntityData.Status.ToUpper() == GenericEnums.ApplicationValidateStatus.Draft.ToString().ToUpper())
                                    {
                                        servicelist.Status = GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Draft).Trim();
                                    }
                                    else if (bblEntityData.Status.ToUpper() == GenericEnums.ApplicationValidateStatus.Underreview.ToString().ToUpper())
                                    {
                                        _underReview = _underReview + 1;
                                        servicelist.AppStatusdate = servicelist.PaymentDate;
                                        servicelist.Status = GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Underreview).Trim();
                                    }
                                    else if (bblEntityData.Status.ToUpper() == GenericEnums.ApplicationValidateStatus.Active.ToString().ToUpper())
                                    {
                                        if (submissionMasters.Count() != 0)
                                        {
                                            int day =
                                                Convert.ToDateTime(submissionMasters.FirstOrDefault().ExpirationDate).Day;
                                            if (day == 1 || day == 01)
                                            {
                                                servicelist.ExpDate = submissionMasters.FirstOrDefault().ExpirationDate ==
                                                                      null
                                                    ? DateTime.Now.ToString("MM/dd/yyyy")
                                                    : GetDateTimeValue(
                                                        Convert.ToDateTime(submissionMasters.FirstOrDefault().ExpirationDate)
                                                            .AddDays(-1));
                                            }
                                            else
                                            {
                                                servicelist.ExpDate = submissionMasters.FirstOrDefault().ExpirationDate ==
                                                                      null
                                                    ? DateTime.Now.ToString("MM/dd/yyyy")
                                                    : GetDateTimeValue(
                                                        Convert.ToDateTime(submissionMasters.FirstOrDefault().ExpirationDate));
                                            }
                                        }
                                        var getActiveBblData = bbldetails.FirstOrDefault();

                                        servicelist.Status = GenericEnums.ApplicationStatus.Active.ToString().Trim();
                                        if (bbldetails.Any())
                                        {
                                            string liveMultiplelicenses = GetStringValue(getActiveBblData.License_Category).Trim().Replace("|", ",");

                                            servicelist.IsEhop = false;
                                            servicelist.EhopNumber = string.Empty;
                                            if (!string.IsNullOrEmpty(liveMultiplelicenses))
                                            {
                                                if (liveMultiplelicenses.Split(',').Count() > 1)
                                                {
                                                    servicelist.LicTypes =
                                                        GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.Multiple)
                                                            .Trim();
                                                    servicelist.MultipleLicense = liveMultiplelicenses;
                                                    servicelist.MultipleLicense = liveMultiplelicenses + " ";
                                                    if (servicelist.MultipleLicense.EndsWith(", "))
                                                    {
                                                        servicelist.MultipleLicense = servicelist.MultipleLicense.Substring(0,
                                                            servicelist.MultipleLicense.Length - 2).Replace(",", ", ");
                                                    }
                                                }
                                                else
                                                {
                                                    servicelist.LicTypes = liveMultiplelicenses.Replace(",", ", ");
                                                    servicelist.MultipleLicense = string.Empty;
                                                }
                                            }
                                            else
                                            {
                                                servicelist.LicTypes = "NA";
                                            }

                                            servicelist.ExpirationDate =
                                                getActiveBblData.Expiration_Date ==
                                                null
                                                    ? DateTime.Now.ToString("MM/dd/yyyy")
                                                    : GetDateTimeValue(Convert.ToDateTime(getActiveBblData.Expiration_Date));
                                            servicelist.ExpDate = getActiveBblData.Expiration_Date == null ? DateTime.Now.ToString("MM/dd/yyyy")
                                                : GetDateTimeValue(Convert.ToDateTime(getActiveBblData.Expiration_Date));

                                            servicelist.AppStatusdate = GetDateTimeValue(Convert.ToDateTime(getActiveBblData.B1_APPL_STATUS_DATE));
                                            if (submissionMasters.Count() != 0)
                                            {
                                                servicelist.PaymentDate =
                                                    GetDateTimeValue(
                                                        Convert.ToDateTime(submissionMasters.FirstOrDefault().Updatedate)) ??
                                                    "";
                                            }
                                            servicelist.PremiseAddress = GetPremisisAddress(bbldetails).ToString().Trim()
                                                                             .Replace("  ", " ")
                                                                             .Replace("  ", " ");

                                            servicelist.Status = GetStringValue(getActiveBblData.B1_APPL_STATUS).Trim();

                                            servicelist.TradeName = GetStringValue(getActiveBblData.Attr_TRADE_NAME).Trim();

                                            servicelist.BusinessName = GetStringValue(getActiveBblData.OwnrApplicant_BUSINESS_NAME).Trim();

                                            servicelist.LicenseeFirstLastName = (GetStringValue(getActiveBblData.Contact_FirstName).Trim()) +
                                                                                " " +
                                                                                (GetStringValue(getActiveBblData.Contact_LastName).Trim());
                                        }

                                        servicelist.Status = GenericEnums.ApplicationStatus.Active.ToString().Trim();

                                        _active = _active + 1;
                                    }
                                    else
                                    {
                                        if (bbldetails.Any())
                                        {
                                            var getActiveBblData = bbldetails.FirstOrDefault();
                                            servicelist.PremiseAddress = GetPremisisAddress(bbldetails)
                                                .ToString()
                                                .Trim()
                                                .Replace("  ", " ")
                                                .Replace("  ", " ");

                                            servicelist.Status =
                                                GetStringValue(getActiveBblData.B1_APPL_STATUS).Trim();
                                            if (servicelist.Status.ToUpper() == "ACTIVE")
                                            { _active = _active + 1; }
                                            servicelist.TradeName =
                                                GetStringValue(getActiveBblData.Attr_TRADE_NAME).Trim();

                                            servicelist.BusinessName =
                                                GetStringValue(getActiveBblData.OwnrApplicant_BUSINESS_NAME).Trim();
                                            servicelist.LicenseeFirstLastName = (GetStringValue(getActiveBblData.Contact_FirstName).Trim()) +
                                                                              " " +
                                                                              (GetStringValue(getActiveBblData.Contact_LastName).Trim());
                                        }
                                    }
                                    string strtus = string.Empty;
                                    if (servicelist.Status != null)
                                    {
                                        servicelist.Status = servicelist.Status;
                                    }
                                    else
                                    {
                                        servicelist.Status = bblEntityData.Status;
                                    }

                                    var status =
                                        getMasterbblapplicationRepo.Where(
                                            x =>
                                                x.Application_Cap_Status.ToUpper().Trim() ==
                                                servicelist.Status.ToUpper().Trim());
                                    if (status.Any())
                                    {
                                        servicelist.Status = status.FirstOrDefault().Changed_Cap_Status ?? "";
                                    }
                                }
                            }

                            #endregion Submission

                            break;
                    }
                    serviceData.Add(servicelist);
                }

                var serviceListData = serviceData.ToList().Where(x => x.Status != null);

                var bblServiceLists = serviceListData as IList<BblServiceList> ?? serviceListData.ToList();
                var newResultData =
                    bblServiceLists.OrderByDescending(
                        x =>
                            x.Status.ToUpper().Trim() ==
                            GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Expired)
                                .ToUpper().Trim()).ThenByDescending(x => x.Status.ToUpper().Trim() ==
                                                      GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Lapsed).ToUpper().Trim())
                                                          .ThenByDescending(x => x.Status.ToUpper().Trim() ==
                                                      GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.ExpiringSoon).ToUpper().Trim())
                                                       .ThenByDescending(x => x.Status.ToUpper().Trim() ==
                                                      GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Active).ToUpper().Trim())
                                                        .ThenByDescending(x => x.Status.ToUpper().Trim() ==
                                                      GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Underreview).ToUpper().Trim())
                                                       .ThenByDescending(x => x.Status.ToUpper().Trim() ==
                                                      GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Draft).ToUpper().Trim())
                                                        .ThenByDescending(x => x.Status.ToUpper().Trim() ==
                                                      GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Renewalnotallowed).ToUpper().Trim())
                                                       .ThenByDescending(x => (x.Status.ToUpper() !=
                                                 GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Renewalnotallowed)
                                                     .ToUpper()
                                                     .Trim()) && (x.Status.ToUpper() !=
                                                 GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Underreview)
                                                     .ToUpper()
                                                     .Trim())
                                                    && (x.Status.ToUpper() !=
                                                 GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Active)
                                                     .ToUpper()
                                                     .Trim())
                                                      && (x.Status.ToUpper() !=
                                                 GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.ExpiringSoon)
                                                     .ToUpper()
                                                     .Trim())
                                                      && (x.Status.ToUpper() !=
                                                 GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Lapsed)
                                                     .ToUpper()
                                                     .Trim()) &&
                                                     (x.Status.ToUpper() !=
                                                 GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Expired)
                                                     .ToUpper()
                                                     .Trim())
                                                     &&
                                                     (x.Status.ToUpper() !=
                                                 GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Draft)
                                                     .ToUpper()
                                                     .Trim()))
                                                      ;

                //var getOtherStatusDetails = (from x in bblServiceLists
                //                             where
                //                                 ((x.Status.ToUpper() !=
                //                                 GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Renewalnotallowed)
                //                                     .ToUpper()
                //                                     .Trim()) && (x.Status.ToUpper() !=
                //                                 GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Underreview)
                //                                     .ToUpper()
                //                                     .Trim())
                //                                    && (x.Status.ToUpper() !=
                //                                 GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Active)
                //                                     .ToUpper()
                //                                     .Trim())
                //                                      && (x.Status.ToUpper() !=
                //                                 GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.ExpiringSoon)
                //                                     .ToUpper()
                //                                     .Trim())
                //                                      && (x.Status.ToUpper() !=
                //                                 GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Lapsed)
                //                                     .ToUpper()
                //                                     .Trim()) &&
                //                                     (x.Status.ToUpper() !=
                //                                 GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Expired)
                //                                     .ToUpper()
                //                                     .Trim())
                //                                     &&
                //                                     (x.Status.ToUpper() !=
                //                                 GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Draft)
                //                                     .ToUpper()
                //                                     .Trim())
                //                                     )
                //                             select x);
                //Renew = getOtherStatusDetails.Count();
                //     int bblTotalCount = Drafts + Active + Expired + _expirySoon + Lapsed + UnderReview + RenewNotallowed;
                _active = newResultData.Count(x => x.Status.ToUpper() ==
                                                 GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Active)
                                                     .ToUpper()
                                                     .Trim());
                var bblServiceCount = new BBLServiceCount
                {
                    Draft = _drafts,
                    Active = _active,
                    Expired = _expired,
                    ExpirySoon = _expirySoon + _expired + _lapsed,
                    UnderReview = _underReview,
                    Renew = _renew,
                    eHOP = _eHop,
                    Lapsed = _lapsed,
                    RenewNotallowed = _renewNotallowed,
                    Total = bblServiceLists.Count()
                };
                servicecount.Add(bblServiceCount);
                var servicelistData = new BBlService
                {
                    BblServiceList = newResultData.ToList(),
                    BBLServiceCount = servicecount
                };
                bblService.Add(servicelistData);
                return bblService;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in BBL list binding", ex);
            }
        }
        /// <summary>
        /// This method is used to get bbl data based for the premises address based on bbl data
        /// </summary>
        /// <param name="bbldata"></param>
        /// <returns>Return bbl data as string</returns>
        public string GetPremisisAddress(ICollection<DCBC_ENTITY_BBL> bbldata)
        {
            var getBblEntityTableData = bbldata.FirstOrDefault();
            var sbPremisesAddress = new StringBuilder();
            sbPremisesAddress.Append(
                GetintValue(getBblEntityTableData.B1_HSE_NBR_START ?? 99999999).ToUpper().Trim());
            if (GetintValue(getBblEntityTableData.B1_HSE_NBR_END ?? 99999999) != string.Empty)
            {
                sbPremisesAddress.Append("-");
                sbPremisesAddress.Append(
                    GetintValue(getBblEntityTableData.B1_HSE_NBR_END ?? 99999999).Trim());
            }
            if (GetStringValue(getBblEntityTableData.B1_HSE_FRAC_NBR_START) != string.Empty)
            {
                sbPremisesAddress.Append(" ");
                sbPremisesAddress.Append(
                   GetStringValue(getBblEntityTableData.B1_HSE_FRAC_NBR_START).ToUpper().Trim());
            }
            if (GetStringValue(getBblEntityTableData.B1_UNIT_START) != string.Empty)
            {
                sbPremisesAddress.Append(" ");
                sbPremisesAddress.Append(
                   GetStringValue(getBblEntityTableData.B1_UNIT_START).ToUpper().Trim());
            }
            if (GetStringValue(getBblEntityTableData.B1_STR_NAME) != string.Empty)
            {
                sbPremisesAddress.Append(" ");
                sbPremisesAddress.Append(
                   GetStringValue(getBblEntityTableData.B1_STR_NAME).ToUpper().Trim());
            }
            if (GetStringValue(getBblEntityTableData.B1_STR_SUFFIX) != string.Empty)
            {
                sbPremisesAddress.Append(" ");
                sbPremisesAddress.Append(
                   GetStringValue(getBblEntityTableData.B1_STR_SUFFIX).ToUpper().Trim());
            }
            if (GetStringValue(getBblEntityTableData.B1_STR_SUFFIX_DIR) != string.Empty)
            {
                sbPremisesAddress.Append(" ");
                sbPremisesAddress.Append(
                   GetStringValue(getBblEntityTableData.B1_STR_SUFFIX_DIR).ToUpper().Trim());
            }
            if (GetStringValue(getBblEntityTableData.B1_SITUS_CITY) != string.Empty)
            {
                sbPremisesAddress.Append(" ");
                sbPremisesAddress.Append(
                   GetStringValue(getBblEntityTableData.B1_SITUS_CITY).ToUpper().Trim());
            }
            if (GetStringValue(getBblEntityTableData.B1_SITUS_STATE) != string.Empty)
            {
                sbPremisesAddress.Append(" ");
                sbPremisesAddress.Append(
                   GetStringValue(getBblEntityTableData.B1_SITUS_STATE).ToUpper().Trim());
            }
            if (GetStringValue(getBblEntityTableData.B1_SITUS_ZIP) != string.Empty)
            {
                sbPremisesAddress.Append(" ");
                sbPremisesAddress.Append(
                   GetStringValue(getBblEntityTableData.B1_SITUS_ZIP).ToUpper().Trim());
            }
            return sbPremisesAddress.ToString();
        }
        /// <summary>
        /// This methos is used to get bbl service list based on user inputs
        /// </summary>
        /// <param name="dcbcEntityId"></param>
        /// <param name="userId"></param>
        /// <param name="submissionLicense"></param>
        /// <param name="serviceId"></param>
        /// <param name="licenseExpirationDate"></param>
        /// <param name="type"></param>
        /// <param name="servicelist"></param>
        /// <param name="submissionMasterDetails"></param>
        /// <param name="dcbcEntityBbls"></param>
        /// <param name="b1AltId"></param>
        /// <returns>Retrun bbl service list</returns>
        private BblServiceList BindRenewalDetails(string dcbcEntityId, string userId, string submissionLicense,
            int serviceId, DateTime licenseExpirationDate, string type, BblServiceList servicelist,
            ICollection<SubmissionMaster> submissionMasterDetails, ICollection<DCBC_ENTITY_BBL> dcbcEntityBbls,
            string b1AltId
            )
        {
            var bblEntityTableData = dcbcEntityBbls.Where(x => x.DCBC_ENTITY_ID == Convert.ToInt32(dcbcEntityId)).ToList();
            if (bblEntityTableData.Any())
            {
                var getBblEntityTableData = bblEntityTableData.FirstOrDefault();
                if (getBblEntityTableData != null)
                {
                    servicelist.GrandTotal = 0;
                    if (getBblEntityTableData.Expiration_Date != null)
                    {
                        servicelist.MasterId = getBblEntityTableData.DCBC_ENTITY_ID.ToString();
                        var getUserSubmissionMasterDetails =
                            submissionMasterDetails.Where(
                                x => x.SubmissionLicense == submissionLicense.ToString() && x.UserID == userId);

                        string masterid = string.Empty;
                        var userSubmissionMasterDetails = getUserSubmissionMasterDetails as IList<SubmissionMaster> ?? getUserSubmissionMasterDetails.ToList();
                        if (userSubmissionMasterDetails.Any())
                        {
                            var userSubmissionMasterDetailsOnRenewal = userSubmissionMasterDetails.FirstOrDefault();
                            if (userSubmissionMasterDetailsOnRenewal != null)
                            {
                                masterid = userSubmissionMasterDetailsOnRenewal.MasterId.Trim();
                                servicelist.MasterId = masterid;
                                if (userSubmissionMasterDetailsOnRenewal.Updatedate != null)
                                {
                                    servicelist.DocumentSubmitType = GetStringValue(userSubmissionMasterDetailsOnRenewal.DocSubmType).Trim();
                                    servicelist.GrandTotal = userSubmissionMasterDetailsOnRenewal.GrandTotal ?? 0;
                                }
                                servicelist.APP_Type = GetStringValue(userSubmissionMasterDetailsOnRenewal.App_Type).Trim();
                            }
                        }
                        else
                        { servicelist.APP_Type = ""; }
                        servicelist.ChcekEhopAllow = "NO";
                        servicelist.ShowActivePdf = "YES";
                        servicelist.UserBblServiceId = serviceId.ToString();
                        servicelist.UserAssociateType = type.ToUpper().Trim();
                        servicelist.EntityId = getBblEntityTableData.DCBC_ENTITY_ID.ToString();
                        servicelist.LicNumber = GetStringValue(getBblEntityTableData.B1_ALT_ID);

                        servicelist.ExpDate = Convert.ToDateTime(getBblEntityTableData.Expiration_Date).ToString("MM/dd/yyy").Trim();

                        servicelist.LicenseeFirstLastName = (GetStringValue(getBblEntityTableData.Contact_FirstName)) +
                                                            " " +
                                                            (GetStringValue(getBblEntityTableData.Contact_LastName));

                        servicelist.BusinessName = getBblEntityTableData.OwnrApplicant_BUSINESS_NAME == null
                            ? string.Empty : getBblEntityTableData.OwnrApplicant_BUSINESS_NAME.Trim();

                        servicelist.TradeName = getBblEntityTableData.Attr_TRADE_NAME == null
                            ? string.Empty : getBblEntityTableData.Attr_TRADE_NAME.Trim();

                        servicelist.PremiseAddress = GetPremisisAddress(bblEntityTableData).ToString().Trim()
                                                                             .Replace("  ", " ")
                                                                             .Replace("  ", " "); ;// sbPremisesAddress.ToString().Replace("  ", " ").Replace("  ", " ");
                        servicelist.Status = getBblEntityTableData.B1_APPL_STATUS == null
                            ? string.Empty : getBblEntityTableData.B1_APPL_STATUS.Trim();

                        var multiplelicenses = GetStringValue(getBblEntityTableData.License_Category).Trim().Replace("|", ",");
                        servicelist.AppStatusdate = GetDateTimeValue(Convert.ToDateTime(
                                              getBblEntityTableData.B1_APPL_STATUS_DATE));
                        servicelist.IsEhop = false;
                        servicelist.EhopNumber = string.Empty;
                        if (!string.IsNullOrEmpty(multiplelicenses))
                        {
                            if (multiplelicenses.Split(',').Count() > 1)
                            {
                                servicelist.LicTypes =
                                    GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.Multiple)
                                        .Trim();
                                servicelist.MultipleLicense = multiplelicenses;
                                servicelist.MultipleLicense = multiplelicenses + " ";
                                if (servicelist.MultipleLicense.EndsWith(", "))
                                {
                                    servicelist.MultipleLicense = servicelist.MultipleLicense.Substring(0,
                                        servicelist.MultipleLicense.Length - 2).Replace(",", ", ");
                                }
                            }
                            else
                            {
                                servicelist.LicTypes = multiplelicenses.Replace(",", ", ");
                            }
                        }
                        else
                        {
                            servicelist.LicTypes = "NA";
                        }
                        try
                        {
                            var dateTime = Convert.ToDateTime(getBblEntityTableData.Expiration_Date.Trim());
                            var difference = (TimeSpan)(licenseExpirationDate - dateTime);
                            int days = (int)difference.TotalDays;
                            if (days >= 1)
                            {
                                var userSubmissionMasterDetailsOnRenewal = userSubmissionMasterDetails.FirstOrDefault();
                                var status = submissionMasterDetails.Where(x => x.MasterId.Trim() == masterid.Trim());

                                if (status.Count() != 0)
                                {
                                    servicelist.Status = (status.FirstOrDefault().Status ?? "").Trim();
                                    if (servicelist.Status.ToUpper().Trim() == "ACTIVE")
                                    {
                                        if (userSubmissionMasterDetailsOnRenewal != null)
                                        {
                                            servicelist.ShowActivePdf = CheckPrimaryCategoryShowPdf(
                                              servicelist.LicTypes, servicelist.MultipleLicense);
                                            //string catgoryName;
                                            //if (servicelist.LicTypes ==
                                            //    GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.Multiple)
                                            //        .Trim())
                                            //{
                                            //    string getCategoryName = servicelist.MultipleLicense.Replace(", ", ",").Trim();
                                            //    var getResultname = getCategoryName.Split(',');
                                            //    catgoryName = getResultname[0].ToString().Trim();
                                            //}
                                            //else
                                            //{
                                            //    catgoryName = servicelist.LicTypes.Replace(", ", "").Trim();
                                            //}

                                            //var checkPdfShow = SubmissionCategoryRepo.CheckPrimaryCategoryShowPdf(catgoryName);
                                            //servicelist.ShowActivePdf = checkPdfShow == false ? "NO" : "YES";
                                            servicelist.ExpirationDate =
                                               userSubmissionMasterDetailsOnRenewal.ExpirationDate == null
                                                    ? DateTime.Now.ToString("MM/dd/yyyy")
                                                    : GetDateTimeValue(Convert.ToDateTime(
                                                        userSubmissionMasterDetailsOnRenewal.ExpirationDate)
                                                        .AddDays(-1));
                                            servicelist.ExpDate = userSubmissionMasterDetailsOnRenewal.ExpirationDate == null
                                                ? DateTime.Now.ToString("MM/dd/yyyy")
                                                : GetDateTimeValue(
                                                    Convert.ToDateTime(userSubmissionMasterDetailsOnRenewal.ExpirationDate));
                                            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                                            servicelist.Status =
                                                textInfo.ToTitleCase(GenericEnums.ApplicationStatus.Active.ToString().Trim()).Trim();
                                            servicelist.PaymentDate =
                                                GetDateTimeValue(
                                                    Convert.ToDateTime(userSubmissionMasterDetailsOnRenewal.Updatedate));
                                            _active = _active + 1;
                                        }
                                    }
                                    else
                                    {

                                        servicelist.ExpDate = null;

                                        if (userSubmissionMasterDetailsOnRenewal != null)
                                        {
                                            servicelist.Status =
                                           GenericEnums.GetEnumDescription(
                                               GenericEnums.ApplicationStatus.Underreview).Trim();
                                            servicelist.PaymentDate = GetDateTimeValue(Convert.ToDateTime(userSubmissionMasterDetailsOnRenewal.Updatedate));
                                        }

                                        _underReview = _underReview + 1;
                                    }
                                }
                                else
                                {
                                    if (userSubmissionMasterDetails.Any())
                                    {
                                        if (userSubmissionMasterDetailsOnRenewal != null)
                                        {
                                            servicelist.ExpDate = null;
                                            servicelist.Status =
                                                GenericEnums.GetEnumDescription(
                                                    GenericEnums.ApplicationStatus.Underreview)
                                                    .Trim();
                                            servicelist.PaymentDate =
                                                GetDateTimeValue(
                                                    Convert.ToDateTime(
                                                        userSubmissionMasterDetailsOnRenewal.Updatedate));
                                            _underReview = _underReview + 1;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                                servicelist.Status = textInfo.ToTitleCase(ServiceExpiryStatus(dateTime)).Trim();
                                if (servicelist.Status == "ACTIVE")
                                {
                                    if (userSubmissionMasterDetails.Count() != 0)
                                    {
                                        var userSubmissionMasterDetailsOnRenewal = userSubmissionMasterDetails.FirstOrDefault();
                                        if (userSubmissionMasterDetailsOnRenewal != null)
                                        {
                                            var submissionAppStatus = userSubmissionMasterDetailsOnRenewal.Status;
                                            var getActiveBblData = bblEntityTableData.FirstOrDefault();
                                            if (submissionAppStatus.ToUpper() == ((GenericEnums.GetEnumDescription(
                                                GenericEnums.ApplicationValidateStatus.UnderreviewSpace)
                                                .ToString()
                                                .ToUpper()
                                                .Trim())))
                                            {
                                                UpdateRenewalStatusAfterUnderReview(
                                                    userSubmissionMasterDetailsOnRenewal.MasterId, servicelist.Status);
                                            }
                                            servicelist.Status = GenericEnums.ApplicationStatus.Active.ToString().Trim();
                                            servicelist.ExpirationDate = getActiveBblData.Expiration_Date == null
                                                ? DateTime.Now.ToString("MM/dd/yyyy")
                                                : GetDateTimeValue(Convert.ToDateTime(getActiveBblData.Expiration_Date));
                                            servicelist.ExpDate = getActiveBblData.Expiration_Date == null
                                                ? DateTime.Now.ToString("MM/dd/yyyy")
                                                : GetDateTimeValue(Convert.ToDateTime(getActiveBblData.Expiration_Date));
                                            servicelist.PaymentDate =
                                                GetDateTimeValue(
                                                    Convert.ToDateTime(userSubmissionMasterDetailsOnRenewal.Updatedate));
                                            servicelist.Status = GetStringValue(getActiveBblData.B1_APPL_STATUS).Trim();
                                            servicelist.TradeName =
                                                GetStringValue(getActiveBblData.Attr_TRADE_NAME).Trim();
                                            servicelist.BusinessName =
                                                GetStringValue(getActiveBblData.OwnrApplicant_BUSINESS_NAME).Trim();
                                            servicelist.LicenseeFirstLastName =
                                                (GetStringValue(getActiveBblData.Contact_FirstName).Trim()) + " " +
                                                (GetStringValue(getActiveBblData.Contact_LastName).Trim());
                                            //string catgoryName;
                                            //if (servicelist.LicTypes ==
                                            //    GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.Multiple)
                                            //        .Trim())
                                            //{
                                            //    string getCategoryName = servicelist.MultipleLicense.Replace(", ", ",").Trim();
                                            //    var getResultname = getCategoryName.Split(',');
                                            //    catgoryName = getResultname[0].ToString().Trim();
                                            //}
                                            //else
                                            //{
                                            //    catgoryName = servicelist.LicTypes.Replace(", ", "").Trim();
                                            //}

                                            //var checkPdfShow = SubmissionCategoryRepo.CheckPrimaryCategoryShowPdf(catgoryName);
                                            servicelist.ShowActivePdf = CheckPrimaryCategoryShowPdf(
                                                servicelist.LicTypes, servicelist.MultipleLicense);
                                        }
                                    }
                                }
                            }
                            servicelist.SubCategory = "NA";

                            servicelist.LrenNumber = submissionLicense;
                            if (servicelist.Status.ToUpper().Trim() == "ACTIVE")
                            {
                                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                                servicelist.Status =
                                              textInfo.ToTitleCase(GenericEnums.ApplicationStatus.Active.ToString().Trim()).Trim();
                            }
                        }
                        catch (Exception ex)
                        {
                           throw new Exception("Exception occurs in Bind Renewal Data", ex);
                        }
                    }
                }
            }
            return servicelist;
        }
        /// <summary>
        /// This method is used to check the pdf shown or not using licensetypes and multiple licesne
        /// </summary>
        /// <param name="licTypes"></param>
        /// <param name="multipleLicense"></param>
        /// <returns>Retrun string value</returns>
        private string CheckPrimaryCategoryShowPdf(string licTypes, string multipleLicense)
        {
            string catgoryName;
            if (licTypes ==
                GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.Multiple)
                    .Trim())
            {
                string getCategoryName = multipleLicense.Replace(", ", ",").Trim();
                var getResultname = getCategoryName.Split(',');
                catgoryName = getResultname[0].ToString().Trim();
            }
            else
            {
                catgoryName = licTypes.Replace(", ", "").Trim();
            }

            var checkPdfShow = SubmissionCategoryRepo.CheckPrimaryCategoryShowPdf(catgoryName);
            return checkPdfShow == false ? "NO" : "YES";
        }
        /// <summary>
        /// This method is used to get bbl service list based on user inputs
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="submissionLicense"></param>
        /// <param name="type"></param>
        /// <param name="submissionMasterDetails"></param>
        /// <returns>Return bbl service list</returns>
        private BblServiceList BindSubmissionWithDappDetails(string userId, string submissionLicense,
            string type, List<SubmissionMaster> submissionMasterDetails)
        {
            var servicelist = new BblServiceList();
            var submissionCategoryList = new SubmissionCategoryList();
            var bblEntity =
                submissionMasterDetails.FirstOrDefault(x => x.SubmissionLicense == submissionLicense);
            if (bblEntity != null)
            {
                var chcekMaster =
                    submissionMasterDetails.Where(x => x.SubmissionLicense == submissionLicense && x.UserID == userId);
                string masterid = string.Empty;
                if (chcekMaster.Any())
                {
                    masterid = GetStringValue(chcekMaster.FirstOrDefault().MasterId).Trim();
                    servicelist.TradeName = GetStringValue(chcekMaster.FirstOrDefault().TradeName);
                }
                else
                {
                    masterid = string.Empty;
                    servicelist.TradeName = string.Empty;
                }

                servicelist.ShowActivePdf = "NO";
                servicelist.LrenNumber = "";
                servicelist.UserAssociateType = type.ToUpper().Trim();
                servicelist.MasterId = masterid;
                servicelist.EntityId = bblEntity.SubmissionLicense;
                servicelist.APP_Type = GetStringValue(bblEntity.App_Type).Trim();
                servicelist.GrandTotal = bblEntity.GrandTotal ?? 0;
                servicelist.LicNumber = GetStringValue(bblEntity.SubmissionLicense).Trim();
                servicelist.ExpDate = string.Empty;
                servicelist.DocumentSubmitType = GetStringValue(bblEntity.DocSubmType).ToUpper().Trim();
                servicelist.LicenseeFirstLastName = GetStringValue(bblEntity.BusinessName).Trim();
                servicelist.BusinessName = GetStringValue(bblEntity.BusinessName).Trim();
                servicelist.PremiseAddress = GetStringValue(bblEntity.PremisesAddress).Trim();
                var multiplelicenses = SubmissionCategoryRepo.SubmissionCategoryListWithStatus(submissionCategoryList, masterid);
                servicelist.CategoryStatus = multiplelicenses.Status;
                if (!string.IsNullOrEmpty(multiplelicenses.CategoryName))
                {
                    if (multiplelicenses.CategoryName.Split(',').Count() > 1)
                    {
                        servicelist.LicTypes =
                            GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.Multiple)
                                .Trim();
                        servicelist.MultipleLicense = multiplelicenses.CategoryName;
                        string d = ", ";
                        servicelist.MultipleLicense = multiplelicenses.CategoryName.Replace(",", d);
                    }
                    else
                    {
                        servicelist.LicTypes = multiplelicenses.CategoryName;
                    }
                }
                else
                {
                    servicelist.LicTypes = "NA";
                }
                servicelist.SubCategory = multiplelicenses.SubCategory ?? "NA";
                if (bblEntity.Status.ToUpper() == GenericEnums.ApplicationValidateStatus.Draft.ToString().ToUpper())
                {
                    servicelist.Status = GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Draft).Trim();
                }
                else if (bblEntity.Status.ToUpper() == GenericEnums.ApplicationValidateStatus.Underreview.ToString().ToUpper())
                {
                    servicelist.Status = GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Underreview).Trim();
                    _underReview = _underReview + 1;
                }
                else if (bblEntity.Status.ToUpper() == GenericEnums.ApplicationValidateStatus.Active.ToString().ToUpper())
                {
                    servicelist.Status = GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Active).ToUpper().Trim();
                }
                if (bblEntity.IseHOP == true)
                {
                    var validateEhop = SubmissionApplicationCheckListRepo.FindByMasterId(bblEntity.MasterId);
                    var submissionMasterApplicationCheckLists = validateEhop as IList<SubmissionMaster_ApplicationCheckList> ?? validateEhop.ToList();
                    if (validateEhop != null && submissionMasterApplicationCheckLists.Any())
                    {
                        servicelist.ChcekEhopAllow = "NO";
                        var submissionMasterApplicationCheckList = submissionMasterApplicationCheckLists.FirstOrDefault();
                        if (submissionMasterApplicationCheckList != null)
                        {
                            var getDetEhopdetails = submissionMasterApplicationCheckList.IsSubmissioneHop;
                            if (getDetEhopdetails == true)
                            {
                                servicelist.ChcekEhopAllow = "YES";
                            }
                        }
                    }
                }
                else
                {
                    servicelist.ChcekEhopAllow = "NO";
                }

                _drafts = _drafts + 1;
            }
            return servicelist;
        }
        /// <summary>
        /// This method is used to get string value based on input
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns>Return string value</returns>
        public string GetStringValue(string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                return string.Empty;
            }
            else
            {
                return stringValue == null
                    ? string.Empty
                    : stringValue.ToUpper().Trim() == "NA"
                        ? string.Empty
                        : stringValue.ToUpper().Trim() == "N/A" ? string.Empty : stringValue.Trim();
            }
        }
        /// <summary>
        /// This method is used to get string value based on input
        /// </summary>
        /// <param name="intValue"></param>
        /// <returns>Return string value</returns>
        public string GetintValue(int intValue)
        {
            return intValue == 99999999 ? string.Empty : intValue.ToString() == "NA" ? string.Empty : intValue.ToString().ToUpper().Trim() == "N/A" ? string.Empty : intValue.ToString().Trim();
        }
        /// <summary>
        ///  This method is used to get string value based on input
        /// </summary>
        /// <param name="datetimeValue"></param>
        /// <returns>Return string value</returns>
        public string GetDateTimeValue(DateTime datetimeValue)
        {
            return datetimeValue.ToString("MM/dd/yyyy");
        }

        /// <summary>
        /// This method is used to Current Status of Application based on Date Provided.
        /// </summary>
        /// <param name="dateofExpiry"></param>
        /// <returns>Retrun String Result</returns>
        public string ServiceExpiryStatus(DateTime dateofExpiry)
        {
            try
            {
                DateTime expiryDate = dateofExpiry;
                string time = DateTime.Now.ToString("HH:mm:ss");
                string hour = time.Substring(0, 2);
                int hourInt = int.Parse(hour);
                DateTime checkTime = DateTime.Now;
                if (hourInt == 0 && checkTime.Minute < 31)
                {
                    checkTime = checkTime.AddDays(-1);
                }
                double dayperiod = Math.Ceiling((expiryDate - checkTime).TotalDays);

                if (dayperiod >= -30 && dayperiod <= -1)
                {
                    _lapsed = _lapsed + 1;
                    return GenericEnums.ApplicationValidateStatus.Lapsed.ToString().Trim();
                }
                // ReSharper disable once RedundantIfElseBlock
                else if (dayperiod > 90)
                {
                    //   Active = Active + 1;
                    return GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Active).Trim();
                }
                // ReSharper disable once RedundantIfElseBlock
                else if (dayperiod >= 0 && dayperiod <= 90)
                {
                    _expirySoon = _expirySoon + 1;
                    return GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.ExpiringSoon).Trim();
                }
                // ReSharper disable once RedundantIfElseBlock
                else if (dayperiod < -30 && dayperiod >= -180)
                {
                    _expired = _expired + 1;
                    return GenericEnums.ApplicationValidateStatus.Expired.ToString().Trim();
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                {
                    return GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Renewalnotallowed).Trim();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occurs in Service Expiry Status",ex);
            }
        }
        /// <summary>
        /// This method is used to get status based on exipration date
        /// </summary>
        /// <param name="dateofExpiry"></param>
        /// <returns>Return string value</returns>
        public string ServiceSubmissionExpiryStatus(DateTime dateofExpiry)
        {
            try
            {
                DateTime expiryDate = dateofExpiry;
                string time = DateTime.Now.ToString("HH:mm:ss");
                string hour = time.Substring(0, 2);
                int hourInt = int.Parse(hour);
                DateTime checkTime = DateTime.Now;
                if (hourInt == 0 && checkTime.Minute < 31)
                {
                    checkTime = checkTime.AddDays(-1);
                }
                double dayperiod = Math.Ceiling((expiryDate - checkTime).TotalDays);

                if (dayperiod >= -30 && dayperiod <= -1)
                {
                    //Lapsed = Lapsed + 1;
                    return GenericEnums.ApplicationValidateStatus.Lapsed.ToString().Trim();
                }
                // ReSharper disable once RedundantIfElseBlock
                else if (dayperiod > 90)
                {
                    //  Active = Active + 1;
                    return GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Active).Trim();
                }
                // ReSharper disable once RedundantIfElseBlock
                else if (dayperiod >= 0 && dayperiod <= 90)
                {
                    // _expirySoon = _expirySoon + 1;
                    return GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.ExpiringSoon).Trim();
                }
                // ReSharper disable once RedundantIfElseBlock
                else if (dayperiod < -30 && dayperiod >= -180)
                {
                    // Expired = Expired + 1;
                    return GenericEnums.ApplicationValidateStatus.Expired.ToString().Trim();
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                {
                    return GenericEnums.GetEnumDescription(GenericEnums.ApplicationStatus.Renewalnotallowed).Trim();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Expiry Status", ex); 
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserBBLService> GetUserBblService()
        {
            return Userbblrepository.UserBblServicesList();
        }

        /// <summary>
        /// This method is used to Get All Submission of License based on User Id for Admin.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Retrun Submission Master Details</returns>
        //public IEnumerable<SubmissionMasterDetails> AllSubmissions(string userId)
        //{
        //    try
        //    {
        //        var submissionCategoryList = new SubmissionCategoryList();
        //        var submissionrecords = GetAll().OrderByDescending(x => x.CreatedDate).ToList();
        //        if (userId == string.Empty)
        //        {
        //            submissionrecords = submissionrecords.Where(x => x.Status.Replace(System.Environment.NewLine, "").ToUpper().ToString().Trim() ==
        //                GenericEnums.ApplicationValidateStatus.Underreview.ToString().ToUpper() ||
        //                x.Status.Replace(System.Environment.NewLine, "").ToUpper().ToString().Trim() == GenericEnums.ApplicationValidateStatus.Draft.ToString().ToUpper()
        //                 || x.Status.Replace(System.Environment.NewLine, "").ToUpper().ToString().Trim() == GenericEnums.ApplicationValidateStatus.Active.ToString().ToUpper()).ToList();
        //        }
        //        else
        //        {
        //            var bbluserService = Userbblrepository.FindByUserStatusID(userId).ToList();
        //            submissionrecords = submissionrecords.Where(x => (x.Status.Replace(System.Environment.NewLine, "").ToUpper().ToString().Trim()
        //                == GenericEnums.ApplicationValidateStatus.Underreview.ToString().ToUpper() ||
        //                x.Status.Replace(System.Environment.NewLine, "").ToUpper().ToString().Trim()
        //                == GenericEnums.ApplicationValidateStatus.Draft.ToString().ToUpper()
        //                 || x.Status.Replace(System.Environment.NewLine, "").ToUpper().ToString().Trim() == GenericEnums.ApplicationValidateStatus.Active.ToString().ToUpper()
        //                ) && x.UserID == userId).ToList();
        //        }
        //  var masterdata = new List<SubmissionMasterDetails>();
        //    if (submissionrecords.Any())
        //    {
        //        foreach (var submission in submissionrecords)
        //        {
        //            var smdetails = new SubmissionMasterDetails();
        //            var usernames = Userrepo.FindByID(submission.UserID).ToList();
        //            var categoryList = SubmissionCategoryRepo.FindbyMaster(submission.MasterId).ToList();
        //            smdetails.ActivityName = "";
        //            smdetails.ApplicationSubmitType = "";
        //            if (categoryList.Count != 0)
        //            {
        //                var primaryCategory = MasterPrimaryCategoryRepo.FindByCategoryID(categoryList.FirstOrDefault().CategoryTypeID).FirstOrDefault();
        //                if (primaryCategory != null && (primaryCategory.PrimaryID != ""))
        //                {
        //                   var categoryName  = SubmissionCategoryRepo.SubmissionCategoryListWithStatus(submissionCategoryList,submission.MasterId);
        //                    smdetails.ActivityName = categoryName.CategoryName;
        //                }
        //            }
        //            if (!string.IsNullOrEmpty(submission.UserBblAssociateId))
        //            {
        //                var getSubmitTypelist =
        //                    Userbblrepository.FindByID(Convert.ToInt32(submission.UserBblAssociateId)).ToList();
        //                if (getSubmitTypelist.Count != 0)
        //                {
        //                    smdetails.ApplicationSubmitType = getSubmitTypelist.FirstOrDefault().Type.Trim();
        //                    smdetails.SubmissionLicense = getSubmitTypelist.FirstOrDefault().B1_ALT_ID;
        //                }
        //            }
        //            smdetails.App_Type = submission.App_Type ?? "";
        //            smdetails.GrandTotal = submission.GrandTotal ?? 0;
        //            smdetails.MasterId = submission.MasterId ?? "";
        //            if (smdetails.ApplicationSubmitType == GenericEnums.GetEnumDescription(GenericEnums.ApplicationSubmissionType.FromSubmission).ToUpper()
        //                || smdetails.ApplicationSubmitType == "")
        //            {
        //                smdetails.SubmissionLicense = submission.SubmissionLicense ?? "";
        //            }
        //            //else
        //            //{
        //            //    int i = 0;
        //            //    var getBblList = Bblrepo.FindByID(Convert.ToInt32(submission.SubmissionLicense)).ToList();
        //            //    if ( getBblList.Count != 0)
        //            //    {
        //            //        smdetails.SubmissionLicense = getBblList.FirstOrDefault().B1_ALT_ID;
        //            //    }
        //            //}
        //            smdetails.Status = submission.Status ?? "";
        //            if (usernames.Count() != 0)
        //            {
        //                smdetails.UserName = usernames.First().UserName ?? "";
        //            }
        //            else
        //            {
        //                smdetails.UserName = "";
        //            }
        //            masterdata.Add(smdetails);
        //        }
        //    }
        //    return masterdata.AsEnumerable();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        /// <summary>
        /// This method is used to Get the Submission Details based on Application Unique Id.
        /// </summary>
        /// <param name="submissionMasterModel"></param>
        /// <returns></returns>
        public IEnumerable<SubmissionMaster> FindByID(SubmissionMasterModel submissionMasterModel)
        {
            var submissionMaster = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == submissionMasterModel.MasterId);
            return submissionMaster;
        }
        /// <summary>
        /// This method is used to get application review counts 
        /// </summary>
        /// <returns>Return application revew counts</returns>
        public ApplicationReviewCounts GetApplicationReviewCounts()
        {
            var applicationReviewCounts = new ApplicationReviewCounts
            {
                UnderReviewlistCount =
                    FindBy(x => x.Status.Replace(System.Environment.NewLine, "").ToUpper().ToString().Trim() ==
                                GenericEnums.ApplicationValidateStatus.Underreview.ToString().ToUpper()).Count(),
                DraftlistCount =
                    FindBy(x => x.Status.Replace(System.Environment.NewLine, "").ToUpper().ToString().Trim() ==
                                GenericEnums.ApplicationValidateStatus.Draft.ToString().ToUpper()).Count()
            };
            return applicationReviewCounts;
        }

        /// <summary>
        /// This method is used to Get specific Submission Details based on Submission License.
        /// </summary>
        /// <param name="submissionLicense"></param>
        /// <returns>Retrun Submission Master Details</returns>
        public IEnumerable<SubmissionMaster> FindByEntityID(string submissionLicense)
        {
            var submissionMaster = FindBy(x => x.SubmissionLicense.Replace(System.Environment.NewLine, "") == submissionLicense);
            return submissionMaster;
        }

        /// <summary>
        /// This method is used to Get specific Submission Details based on Application Unique Id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Retrun Submission Master Details</returns>
        public IEnumerable<SubmissionMaster> FindByMasterID(string masterId)
        {
            var submissionMaster = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId);
            return submissionMaster;
        }

        /// <summary>
        /// This method is used to get Specific Submission master data based on Enitity ID and User Id.
        /// </summary>
        /// <param name="entityid"></param>
        /// <param name="userid"></param>
        /// <returns>Retrun Submission Master Details</returns>
        public IEnumerable<SubmissionMaster> GetMasterId(string submissionLicense, string userid)
        {
            var submissionMaster = FindBy(x => x.SubmissionLicense.Replace(System.Environment.NewLine, "") == submissionLicense
                                               && x.UserID.Replace(System.Environment.NewLine, "") == userid).ToList();
            return submissionMaster;
        }

        /// <summary>
        /// This method is used to Update Submission Master based on Payment Details user Inputs
        /// </summary>
        /// <param name="pDetails"></param>
        /// <returns>Return Bool Result</returns>
        public bool UpdateSubmissionMaster(PaymentDetailsModel pDetails)
        {
            bool status = false;
            string documentType = string.Empty;
            try
            {
                if (pDetails.PaymentType.ToUpper() != GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.Renewal).ToUpper())
                {
                    var updateSubMaster = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == pDetails.MasterId).First();
                    int licenseDuration = Convert.ToInt32(updateSubMaster.LicenseDuration);
                 //   Random rnd = new Random();
                   // string doctype = string.Empty;
                    var apptype = string.Empty;
                    documentType = updateSubMaster.DocSubmType.Trim();
                    if (updateSubMaster.DocSubmType.Trim().ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.OnLine).ToUpper()
                        || (updateSubMaster.DocSubmType.Trim() == ""))
                    {
                         GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.OnlineNumber);
                    }
                    else if (updateSubMaster.DocSubmType.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.InPerson).ToUpper())
                    {
                         GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.InPersonNumber);
                    }

                    if (updateSubMaster.App_Type.Trim().ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.IndividualApp))
                    {
                        apptype = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.FinalSubmissionStartsWithIndividual);
                    }
                    else
                    {
                        apptype = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.FinalSubmissionStartsWithBusiness);
                    }
                    if (updateSubMaster.DocSubmType.Trim().ToUpper() != GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.OnLine).ToUpper()
                        && updateSubMaster.DocSubmType.ToUpper() != GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.InPerson).ToUpper())
                    {
                        var categorydata =
                            SubmissionCategoryRepo.FindbyMaster(pDetails.MasterId)
                                .Where(x => x.CategoryType.ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.PrimaryCategory).ToUpper().Trim()).ToList();
                        if (categorydata.Count() != 0)
                        {
                            var categorylist = categorydata.FirstOrDefault();
                            if (categorylist != null)
                            {
                                var cagtegorycode = MasterPrimaryCategoryRepo.FindByCategoryID(categorylist.CategoryTypeID).ToList();
                                if (cagtegorycode.Count != 0)
                                {
                                    var masterPrimaryCategory = cagtegorycode.FirstOrDefault();
                                    if (masterPrimaryCategory != null)
                                    {
                                        string category = masterPrimaryCategory.CategoryCode ?? masterPrimaryCategory.PrimaryID;

                                        pDetails.SubmissionLicense = LappLicenseGeneration(category);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        pDetails.SubmissionLicense = LappLicenseGeneration(apptype);
                    }
                    Userbblrepository.UpdateUserBBL(updateSubMaster.SubmissionLicense.Trim(), pDetails, updateSubMaster.UserID);
                    updateSubMaster.SubmissionLicense = pDetails.SubmissionLicense;
                    updateSubMaster.Status = GenericEnums.GetEnumDescription(GenericEnums.ApplicationValidateStatus.UnderreviewSpace);
                    updateSubMaster.Updatedate = DateTime.Now;
                    updateSubMaster.ExpirationDate = DateTime.Now.AddYears(licenseDuration);
                    Update(updateSubMaster, updateSubMaster.MasterId);
                    Save();
                }
                else
                {
                    UpdateSubmissionStatusWithRenewal(pDetails.MasterId);
                    pDetails.SubmissionLicense = GetLicenseNumberFromSubmission(pDetails.MasterId);
                }
                status = true;
                UpdateFinalSubmissionToAccela(pDetails.SubmissionLicense);
                if (documentType.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.OnLine).ToUpper())
                {
                }
            }
            catch (Exception )
            { status = false; }
            return status;
        }
        /// <summary>
        /// This methos is used to get lapp license generation data 
        /// </summary>
        /// <param name="apptype"></param>
        /// <returns>Return string value</returns>
        public string LappLicenseGeneration(string apptype)
        {
            string licensenumber;
            do
            {
                licensenumber = string.Empty;
                string sequence = string.Empty;
                string counter = string.Empty;
                SubmissionCounter submissioncounter = new SubmissionCounter();
                submissioncounter.Type = GenericEnums.SubmissionCounter.LAPP_IAPP.ToString().ToUpper();
                if (GetPresentMonth())
                {
                    submissioncounter.PhysicalYear = Convert.ToInt16((DateTime.Now.Year + 1).ToString().Substring(2, 2));
                }
                else
                {
                    submissioncounter.PhysicalYear = Convert.ToInt16((DateTime.Now.Year).ToString().Substring(2, 2));
                }
                _submissionLicenseNumberCounterRepository.InsertUpdateSubmissionCounter(submissioncounter);
                var recounterdata = _submissionLicenseNumberCounterRepository.FindBy(submissioncounter).ToList();
                if (recounterdata.Any())
                {
                    var recounterdatalist = recounterdata.FirstOrDefault();
                    if (recounterdatalist != null)
                    {
                        counter = recounterdatalist.Counter.ToString().Trim();
                        sequence = recounterdatalist.Sequence.ToString().Trim();
                    }
                }
                licensenumber = apptype + submissioncounter.PhysicalYear + sequence + LicenseAppend(counter.ToString().Trim(), 5) + counter;
            } while (CheckLappLicense(licensenumber));
            return licensenumber;
        }

        public bool CheckLappLicense(string licensenumber)
        {
            var bbldata = Bblrepo.FindBy(x => x.APPLICATION_CAP.Replace(System.Environment.NewLine, "").ToUpper().Trim() == licensenumber.ToUpper().Trim() ||
                x.B1_ALT_ID.Replace(System.Environment.NewLine, "").Trim().ToUpper() == licensenumber.ToUpper().Trim()).ToList();
            if (bbldata.Any())
            { return true; }
            else
            {
                //var bblLicenseCheck = Bblrepo.FindBy(x => x.B1_ALT_ID.Replace(System.Environment.NewLine, "").Trim().ToUpper() == licensenumber.ToUpper().Trim()).ToList();
                //if(bblLicenseCheck.Any())
                //{
                //    return true;
                //}
                return false;
            }
        }

        /// <summary>
        /// This method is used to geth the Submission License Number based on Application Unique Number.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Retrun String Result</returns>
        public string GetLicenseNumberFromSubmission(string masterId)
        {
            var licenseNumber = string.Empty;
            var license = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).ToList();
            if (license.Count() == 0) return licenseNumber;
            var getMasterRow = license.FirstOrDefault();
            if (getMasterRow != null) licenseNumber = getMasterRow.SubmissionLicense ?? "";
            return licenseNumber;
        }

        /// <summary>
        /// This method is used to Update Status of Submission Based on Application Unique Id.
        /// </summary>
        /// <param name="masterId"></param>
        public void UpdateSubmissionStatusWithRenewal(string masterId)
        {
            try
            {
                DateTime exiprydate = DateTime.Now;
                var updateSubMasterRenewal = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).ToList();
                if (updateSubMasterRenewal.Any())
                {
                    var updateSubMaster = updateSubMasterRenewal.First();
                    // int enitityid =Convert.ToInt32( updateSubMaster.SubmissionLicense);
                    //  var bbldetails = Bblrepo.FindByID(enitityid).ToList();
                    //if(bbldetails.Any())
                    //{
                    //    exiprydate = Convert.ToDateTime(bbldetails.FirstOrDefault().Expiration_Date);
                    //}
                    int licenseDuration = Convert.ToInt32(updateSubMaster.LicenseDuration);
                    string docstatus = updateSubMaster.DocSubmType ?? "";
                    //if (docstatus != "")
                    //{
                    //    updateSubMaster.Status = GenericEnums.GetEnumDescription(GenericEnums.ApplicationValidateStatus.UnderreviewSpace);
                    //}
                    //else
                    //{ updateSubMaster.Status = GenericEnums.GetEnumDescription(GenericEnums.ApplicationValidateStatus.Active);
                    updateSubMaster.Status = GenericEnums.GetEnumDescription(GenericEnums.ApplicationValidateStatus.UnderreviewSpace);
                    var currentDate = Convert.ToDateTime(exiprydate);
                    var currentmonth = currentDate.Month;
                    var currentYear = currentDate.Year;
                    //  var date = currentDate.Date;
                    var startOfMonth = new DateTime(currentYear, currentmonth, 1);
                    updateSubMaster.ExpirationDate = startOfMonth.AddYears(licenseDuration);
                    //  }
                    updateSubMaster.Updatedate = DateTime.Now;

                    Update(updateSubMaster, updateSubMaster.MasterId);
                    Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Updated SubmissionStatusWithRenewal", ex);
            }
        }

        /// <summary>
        /// This method is used to insert Final Submission of License Details to Accela System based on License Number
        /// </summary>
        /// <param name="licenseNumber"></param>
        private void UpdateFinalSubmissionToAccela(string licenseNumber)
        {
            //var checkBblData = Bblrepo.FindByID(Convert.ToInt32(licenseNumber));
            //if (checkBblData.Any())
            //    licenseNumber = (checkBblData.FirstOrDefault().B1_ALT_ID.ToString() ?? "").Trim();

            try
            {
                var subAccelaEntity = new SubmissiontoAccelaEntity
                {
                    SubmissiontoAccelaId = 0,
                    LicenseNumber = licenseNumber,
                    ApplicationCompleted = true,
                    ApplicationCreated = false,
                    ApplicationFeeMatched = false,
                    RenewalPaymentUpdated = false,
                    RenewalFeeMatched = false,
                    AllDocumentsUpdated = false,
                    EhopCreated = false,
                    ProcessCompleted = false
                };
                SubmissionAccelaRepo.AddSubmissionToAccelaRepository(subAccelaEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Updated FinalSubmissionToAccela", ex);
            }
        }

        /// <summary>
        /// This method is used to Gnerate License Number .
        /// </summary>
        /// <returns>Retrun String Result</returns>
        public string InitialSubmissionNumberGen()
        {
            try
            {
                Random rnd = new Random();
                StringBuilder submissionlic = new StringBuilder();
                submissionlic.Append(GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.FirstSubmissionStartsWith));
                //if (GetPresentMonth())
                //{
                //    submissionlic.Append((DateTime.Now.Year+1).ToString().Substring(2, 2));
                //}
                //else {
                //submissionlic.Append(DateTime.Now.Year.ToString().Substring(2, 2));
                //}
                //submissionlic.Append(GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.OnlineNumber));
                SubmissionCounter submissioncounter = new SubmissionCounter();
                submissioncounter.Type = GenericEnums.SubmissionCounter.DAPP.ToString().ToUpper();
                if (GetPresentMonth())
                {
                    submissioncounter.PhysicalYear = Convert.ToInt16((DateTime.Now.Year + 1).ToString().Substring(2, 2));
                    submissionlic.Append(submissioncounter.PhysicalYear);
                }
                else
                {
                    submissioncounter.PhysicalYear = Convert.ToInt16((DateTime.Now.Year).ToString().Substring(2, 2));
                    submissionlic.Append(submissioncounter.PhysicalYear);
                }
                _submissionLicenseNumberCounterRepository.InsertUpdateSubmissionCounter(submissioncounter);

                var counterdata = _submissionLicenseNumberCounterRepository.FindBy(submissioncounter).ToList();
                if (counterdata.Any())
                {
                    submissionlic.Append(LicenseAppend(counterdata.FirstOrDefault().Counter.ToString().Trim(), 6) + counterdata.FirstOrDefault().Counter.ToString().Trim());
                }
                //  submissionlic.Append(rnd.Next(11111, 99999));
                return submissionlic.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in InitialSubmissionNumberGen", ex);
            }
        }
        /// <summary>
        /// This method is used to license value increment 
        /// </summary>
        /// <param name="counter"></param>
        /// <param name="length"></param>
        /// <returns>Retrun string value</returns>
        public string LicenseAppend(string counter, int length)
        {
            StringBuilder builder = new StringBuilder();
            int counterlengh = counter.Length;
            int datalength = length - counterlengh;

            for (int i = 0; i < datalength; i++)
            {
                builder.Append("0");
            }
            return builder.ToString();
        }
        /// <summary>
        /// This method is used to get bool value based on the current month
        /// </summary>
        /// <returns>Return bool</returns>
        public bool GetPresentMonth()
        {
            bool status;
            switch (DateTime.Now.Month.ToString())
            {
                case "10":
                    status = true;
                    break;

                case "11":
                    status = true;
                    break;

                case "12":
                    status = true;
                    break;

                default:
                    status = false;
                    break;
            }
            return status;
        }

        /// <summary>
        /// This method is used to Get Location Business Status based on Question.
        /// </summary>
        /// <param name="submissionApp"></param>
        /// <param name="businessinDc"></param>
        /// <returns>Retrun String Result</returns>
        public string ValidateBusinessLocation(SubmissionApplication submissionApp, string businessinDc)
        {
            try
            {
                string bussinesslocation = string.Empty;
                var question = submissionApp.SubQuestion;
                var bussinessliense = question.Where(xx => xx.Question.ToUpper().ToString().ToUpper().Trim() ==
                       GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.LocationMustBeInDc).ToUpper()).ToList();
                if (bussinessliense.Count() != 0)
                {
                    var locationBusiness = bussinessliense.FirstOrDefault();
                    var screeningQuestion = bussinessliense.FirstOrDefault();
                    if (screeningQuestion != null)
                        bussinesslocation = locationBusiness.Answer == null ? "YES" : screeningQuestion.Answer.ToUpper();
                }
                else
                {
                    bussinesslocation = businessinDc;
                }
                return bussinesslocation;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Validate BusinessLocation", ex); ;
            }
        }

        /// <summary>
        /// This method is used to Get Home Based Status based on Question.
        /// </summary>
        /// <param name="submissionApp"></param>
        /// <returns>Retrun String Result</returns>
        public string ValidateIsHomeBased(SubmissionApplication submissionApp)
        {
            try
            {
                var question = submissionApp.SubQuestion;
                var homebased = question.FirstOrDefault(xx => xx.Question.ToUpper().ToString().Trim() ==
                   GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.IsHomeBased).ToUpper());
                var checkIsHomeBased = "No";
                if (homebased != null)
                {
                    checkIsHomeBased = homebased.Answer.ToUpper();
                }
                return checkIsHomeBased;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Validate IsHomeBased", ex);
            }
        }

        /// <summary>
        /// This method is used to Get eHop Status based on Question.
        /// </summary>
        /// <param name="submissionApp"></param>
        /// <returns>Return String Result</returns>
        public string ValidateIsEHopAllow(SubmissionApplication submissionApp)
        {
            var question = submissionApp.SubQuestion;
            var ehopAllowed = question.FirstOrDefault(xx => xx.Question.ToUpper().ToString().Trim() ==
              GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.IsEhopAllowed).ToUpper());
            var checkIsEhopAllowed = "No";
            if (ehopAllowed != null)
            {
                checkIsEhopAllowed = ehopAllowed.Answer.ToUpper();
            }
            return checkIsEhopAllowed;
        }

        /// <summary>
        /// This method is used to Get Business Structure  based on Question.
        /// </summary>
        /// <param name="submissionApp"></param>
        /// <returns>Retrun String Result</returns>
        public string ValidateBusinessStructure(SubmissionApplication submissionApp)
        {
            try
            {
                var resultResult = string.Empty;
                var question = submissionApp.SubQuestion;
                var result =
                    question.FirstOrDefault(xx => xx.Question.ToUpper().ToString().Trim() ==
                                              GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.BusinessStructure).ToUpper());
                if (result == null) return resultResult;
                var checkBisinessStructure = result.Answer == null ? "" : result.Answer;
                resultResult = checkBisinessStructure;
                return resultResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Validate BusinessStructure", ex);
            }
        }

        /// <summary>
        /// This method is used to Get Trade Name  based on Question.
        /// </summary>
        /// <param name="submissionApp"></param>
        /// <returns>Retrun String Result</returns>
        public string ValidateBusinessTradeName(SubmissionApplication submissionApp)
        {
            try
            {
                var resultResult = string.Empty;
                var question = submissionApp.SubQuestion;
                var result =
                  question.FirstOrDefault(xx => xx.Question.ToUpper().ToString().Trim() ==
                                            GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.TradeName).ToUpper());
                if (result == null) return resultResult;
                var checkBisinessTradeName = result.Answer == null ? "" : result.Answer == "" ? "NA" : result.Answer;
                // result.Answer.ToUpper();
                resultResult = checkBisinessTradeName;
                return resultResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Validate BusinessTradeName", ex);
            }
        }

        /// <summary>
        /// This method is used to Corporation Division Status based on Question.
        /// </summary>
        /// <param name="submissionApp"></param>
        /// <returns>Retrun bool Result</returns>
        public bool ValidateCorporationDivision(SubmissionApplication submissionApp)
        {
            try
            {
                var isCorporationDivision = false;
                var question = submissionApp.SubQuestion;
                var result =
                 question.FirstOrDefault(xx => xx.Question.ToUpper().ToString().Trim() ==
                                           GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.IsDcraRegisteredInCorporation).ToUpper());
                if (result == null) return false;
                var corpDivision = result.Answer == null ? "YES" : result.Answer.ToUpper();
                isCorporationDivision = corpDivision.ToUpper() == "YES";
                return isCorporationDivision;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Validate CorporationDivision", ex);
            }
        }

        /// <summary>
        /// This method is used to Insert Submission License Data based on User Inputs
        /// </summary>
        /// <param name="submissionApp"></param>
        /// <returns>Retrun Application Unique Id</returns>
        public string InsertAssociateBblService(SubmissionApplication submissionApp)
        {
            var guid = string.Empty;
            var noOfYears = 2;
            var question = submissionApp.SubQuestion;
            bool isSelfCertificationAllow = false;
            var getQ = question.Find(x => x.Answer.Contains("Four (4) year"));
            if (getQ != null)
            {
                noOfYears = 4;
            }
            try
            {
                var businessinDc = "NO";
                var isCofoallowed = "NO";
                var isHoPallowed = "NO";
                var hopcount = 0;
                var categorycount = 0;
                var businessmustbe = MasterCategoryPhysicalLocationRepo.FindByID(submissionApp).ToList();
                if (businessmustbe.Count() != 0)
                {
                    categorycount = categorycount + 1;
                    var validateBusinessMustBeInDc = businessmustbe.FirstOrDefault();
                    if (validateBusinessMustBeInDc != null)
                    {
                        businessinDc = validateBusinessMustBeInDc.BusinessMustBeInDC.Replace(System.Environment.NewLine, "").ToUpper().Trim();
                        isCofoallowed = validateBusinessMustBeInDc.COFORequired.Replace(System.Environment.NewLine, "").ToUpper().Trim();
                        isHoPallowed = validateBusinessMustBeInDc.HOP_EHOPAllowed.Replace(System.Environment.NewLine, "").ToUpper().Trim();
                    }
                    if (isHoPallowed.ToUpper().Trim() == "YES")
                    {
                        hopcount = hopcount + 1;
                    }
                }
                if (submissionApp.Secondary != null)
                {
                    var secondarycat = submissionApp.Secondary.Split(',');
                    foreach (var secondary in secondarycat)
                    {
                        if (secondary.Trim() != ",")
                        {
                            categorycount = categorycount + 1;
                            var secondaryname = MasterSecondaryLicenseCategoryRepo.FindBySecondaryID(secondary).ToList();
                            if (secondaryname.Count() != 0)
                            {
                                var primaryname = secondaryname.First().SecondaryLicenseCategory.Replace(System.Environment.NewLine, "") ?? "";
                                if (primaryname != "")
                                {
                                    var primaryid = MasterPrimaryCategoryRepo.SecondaryEndorsement(primaryname).ToList();
                                    if (primaryid.Count() != 0)
                                    {
                                        var primaryCategoryId = primaryid.FirstOrDefault();
                                        var idprimary = string.Empty;
                                        if (primaryCategoryId != null)
                                        {
                                            idprimary = primaryCategoryId.PrimaryID.Replace(System.Environment.NewLine, "") ?? "";
                                        }

                                        var mustindc = MasterCategoryPhysicalLocationRepo.FindCategoryID(idprimary).ToList();
                                        if (!mustindc.Any()) continue;
                                        var validateSecondaryBusinessMustBeInDc = mustindc.FirstOrDefault();
                                        if (validateSecondaryBusinessMustBeInDc == null) continue;
                                        if (validateSecondaryBusinessMustBeInDc.BusinessMustBeInDC.Replace(System.Environment.NewLine, "").ToUpper().Trim() == "YES")
                                        {
                                            businessinDc = validateSecondaryBusinessMustBeInDc.BusinessMustBeInDC.Replace(System.Environment.NewLine, "").ToUpper().Trim();
                                        }
                                        if (validateSecondaryBusinessMustBeInDc.COFORequired.Replace(System.Environment.NewLine, "").ToUpper().Trim() == "YES")
                                        {
                                            isCofoallowed = validateSecondaryBusinessMustBeInDc.COFORequired.Replace(System.Environment.NewLine, "").ToUpper().Trim();
                                        }
                                        if (validateSecondaryBusinessMustBeInDc.HOP_EHOPAllowed.Replace(System.Environment.NewLine, "").ToUpper().Trim() == "YES")
                                        {
                                            isHoPallowed = validateSecondaryBusinessMustBeInDc.HOP_EHOPAllowed.Replace(System.Environment.NewLine, "").ToUpper().Trim();
                                            hopcount = hopcount + 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                var bussinesslocation = ValidateBusinessLocation(submissionApp, businessinDc);
                if (businessinDc.ToUpper() == "YES"
                    || bussinesslocation.ToUpper() == "YES")
                {
                    if (isHoPallowed == "YES" && categorycount == hopcount)
                    {
                        submissionApp.IsBusinessMustbeinDC = true;
                        string bussinessanswer = ValidateIsHomeBased(submissionApp);
                        if (bussinessanswer.ToUpper() == "YES")
                        {
                            submissionApp.IsHomeBased = true;
                            var hoPsanswer = ValidateIsEHopAllow(submissionApp);
                            if (hoPsanswer.ToUpper() == "YES")
                            {
                                submissionApp.IseHOP = true;
                                submissionApp.IsCofo = false;
                            }
                            else
                            {
                                submissionApp.IseHOP = false;
                                submissionApp.IsCofo = false;
                            }
                        }
                        else if (isCofoallowed.ToUpper() == "YES")
                        {
                            submissionApp.IseHOP = false;
                            submissionApp.IsHomeBased = false;
                            submissionApp.IsCofo = true;
                        }
                        else
                        {
                            submissionApp.IseHOP = false;
                            submissionApp.IsHomeBased = false;
                            submissionApp.IsCofo = false;
                        }
                    }
                    else if (isCofoallowed.ToUpper() == "YES")
                    {
                        submissionApp.IseHOP = false;
                        submissionApp.IsBusinessMustbeinDC = true;
                        submissionApp.IsHomeBased = false;
                        submissionApp.IsCofo = true;
                    }
                    else
                    {
                        submissionApp.IseHOP = false;
                        submissionApp.IsBusinessMustbeinDC = true;
                        submissionApp.IsHomeBased = false;
                        submissionApp.IsCofo = false;
                    }
                }
                else
                {
                    submissionApp.IseHOP = false;
                    submissionApp.IsBusinessMustbeinDC = false;
                    submissionApp.IsHomeBased = false;
                    submissionApp.IsCofo = false;
                }

                //Here i am chceking SelfCertification Only for one familyRental----date:12/27/2015

                if ((submissionApp.LicenseCategory.Replace(System.Environment.NewLine, "").ToUpper() ==
                    "ONE FAMILY RENTAL") && (submissionApp.IseHOP == false) && (submissionApp.IsHomeBased == false) && (submissionApp.IsCofo == false))
                {
                    isSelfCertificationAllow = true;
                }
                string ownerName = string.Empty;
                //if (submissionApp.App_Type.ToUpper() == "B")
                //{
                var businessowner =
                     submissionApp.SubQuestion.Where(
                         x => x.Question.ToUpper().Contains("PLEASE ENTER THE FULL NAME OF THE BUSINESS OWNER (IF APPLYING FOR A BUSINESS LICENSE) OR FULL EMPLOYEE NAME( IF APPLYING FOR AN INDIVIDUAL LICENSE) AS IT WILL APPEAR ON THE BUSINESS LICENSE.  CANNOT BE A COMPANY OR TRADE NAME, MUST BE AN INDIVIDUAL."))
                         .ToList();
                if (businessowner.Count() != 0)
                {
                    ownerName = businessowner.FirstOrDefault().Answer ?? "";
                }
                submissionApp.IsCorporationDivision = ValidateCorporationDivision(submissionApp);
                //Sole Proprietorship
                submissionApp.BusinessStructure = submissionApp.App_Type.ToUpper() == "I" ? "Sole Proprietorship" : ValidateBusinessStructure(submissionApp);
                submissionApp.TradeName = submissionApp.App_Type.ToUpper() == "I" ? "NA" : ValidateBusinessTradeName(submissionApp);
                var subMaster = new SubmissionMaster();
                guid = Guid.NewGuid().ToString();
                submissionApp.MasterId = guid;
                subMaster.MasterId = (submissionApp.MasterId ?? "").Trim();
                submissionApp.SubmissionLicense = InitialSubmissionNumberGen();
                subMaster.SubmissionLicense = (submissionApp.SubmissionLicense ?? "").Trim();
                subMaster.UserID = (submissionApp.UserID ?? "").Trim();
                subMaster.ActivityID = (submissionApp.ActivityID ?? "").Trim();
                subMaster.ApplicationFee = submissionApp.Applicationfee;
                subMaster.RAOFee = submissionApp.RAOFee;
                subMaster.IseHOP = submissionApp.IseHOP;
                subMaster.eHOP = submissionApp.eHOP;
                subMaster.Status = "Draft";
                var currentdate = DateTime.Now;
                subMaster.ExpirationDate = currentdate.AddYears(noOfYears);
                subMaster.ApprovedBy = (submissionApp.ApprovedBy ?? "").Trim();
                subMaster.Description = (submissionApp.Description ?? "").Trim();
                subMaster.App_Type = (submissionApp.App_Type ?? "").Trim();
                subMaster.DocSubmType = "";
                subMaster.LicenseDuration = noOfYears;
                subMaster.IsBusinessMustbeinDC = submissionApp.IsBusinessMustbeinDC;
                subMaster.IsHomeBased = submissionApp.IsHomeBased;
                subMaster.IsCofo = submissionApp.IsCofo;
                subMaster.IsPhysicalLocationVerify = false;
                subMaster.GrandTotal = submissionApp.GrandTotal;
                subMaster.BusinessOwnerName = ownerName;
                subMaster.isCorporationDivision = submissionApp.IsCorporationDivision;
                subMaster.IsRaoFee_Applied = submissionApp.IsRaoFeeApplied;
                subMaster.BusinessStructure = submissionApp.BusinessStructure;
                subMaster.TradeName = submissionApp.TradeName;
                subMaster.UserSelectMailAddressType = "";
                subMaster.CreatedDate = DateTime.Now;
                subMaster.Updatedate = DateTime.Now;
                subMaster.IsCategorySelfCertification = isSelfCertificationAllow;
                Add(subMaster);
                Save();
                var result = Userbblrepository.InsertSubmissionBbl(submissionApp);
                if (result != 0)
                {
                    var updateUserBbl = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").Trim().ToUpper() == subMaster.MasterId.ToUpper().Trim()).First();
                    updateUserBbl.UserBblAssociateId = result.ToString().Trim();

                    Update(updateUserBbl, updateUserBbl.MasterId);
                    Save();
                }
                if (submissionApp.PrimaryID != string.Empty)
                {
                    if (SubmissionCategoryRepo.InsertPrimaryBbl(submissionApp))
                    {
                        string secondarycategory = submissionApp.Secondary == null ? "" : submissionApp.Secondary.Trim();
                        if (secondarycategory != "")
                        {
                            SubmissionCategoryRepo.InsertSecondaryBbl(submissionApp);
                        }
                        string subcategory = submissionApp.SubSubCategory == null ? "" : submissionApp.SubSubCategory.Trim();
                        if (subcategory != "")
                        {
                            SubmissionCategoryRepo.InsertSubSubCagteogryBbl(submissionApp);
                        }
                    }
                }
                SubmissionCategoryQuestionRepo.InsertQuestionBbl(submissionApp);
                SubmissionApplicationCheckListRepo.InsertSubmissionChecklist(submissionApp);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in AssociateBblService", ex);
            }
            return guid;
        }

        /// <summary>
        /// This method is used to Gent BBl Service Expire soon Count based on User Id.
        /// </summary>
        /// <param name="bblService"></param>
        /// <returns>Return Number Result</returns>
        public int GetUserAssociateBblListCount(BblAsscoiateService bblService)
        {
            try
            {
                var bbluserService = Userbblrepository.FindByUserID(bblService.UserID);
                if (bbluserService != null)
                {
                    foreach (var userserivce in bbluserService)
                    {
                        //  BblServiceList servicelist = new BblServiceList();
                        if (userserivce.Type.ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.ApplicationSubmissionType.FromAssociation).ToUpper())
                        {
                            var bblData = Bblrepo.FindByID(Convert.ToInt32(userserivce.DCBC_ENTITY_ID)).FirstOrDefault();
                            if (bblData != null && userserivce.LicenseExpirationDate != null)
                            {
                                var expiration = Convert.ToDateTime(userserivce.LicenseExpirationDate);
                                double dayperiod = (expiration - DateTime.Now).TotalDays;
                                if (dayperiod > 1 && dayperiod < 90)
                                {
                                    _resultExpireSoonCount = _resultExpireSoonCount + 1;
                                }
                            }
                        }
                    }
                }
                return _resultExpireSoonCount;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in User Associate BblList Count", ex); 
            }
        }
        /// <summary>
        /// This method is used to update submission documents based on master id and bbl doc.
        /// </summary>
        /// <param name="bbldoc"></param>
        /// <returns>Return bool value</returns>
        public bool UpdateDocumentType(BblDocuments bbldoc)
        {
            bool status = false;
            // int enitityid = Convert.ToInt32(bblassociate.EntityId);
            var userAssociate = (from submaster in (FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == bbldoc.MasterId)) select submaster).ToList();
            if (userAssociate.Any())
            {
                var userAssociateUpdate = userAssociate.First();
                userAssociateUpdate.DocSubmType = bbldoc.DocSubType;
                Update(userAssociateUpdate, userAssociateUpdate.MasterId);
                Save();
                status = true;
            }
            return status;
        }
        /// <summary>
        /// This method is used to update submission documents based on master id and bbl doc.
        /// </summary>
        /// <param name="bbldoc"></param>
        /// <returns>Return bool value</returns>
        public bool UpdateUserSelect(GeneralBusiness bbldoc)
        {
            try
            {
                // int enitityid = Convert.ToInt32(bblassociate.EntityId);
                var userAssociate = (from submaster in (FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == bbldoc.MasterId)) select submaster).ToList();
                if (userAssociate.Any())
                {
                    var userAssociateUpdate = userAssociate.First();
                    userAssociateUpdate.UserSelectMailAddressType = bbldoc.UserSelectTpe;
                    Update(userAssociateUpdate, userAssociateUpdate.MasterId);
                    Save();
                    SubmissionApplicationCheckListRepo.UpdateIsMailAddress(bbldoc);
                }
            }
            catch (Exception ex)
            { throw new Exception("Exception occurs in Update User Select", ex); }
            return true;
        }
        /// <summary>
        /// This method is used to get general bussiness based on master id .
        /// </summary>
        /// <param name="detailsModel"></param>
        /// <returns>Return general business</returns>
        public GeneralBusiness GetMailType(GeneralBusiness detailsModel)
        {
            var mailtype = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").ToString().Trim() == detailsModel.MasterId);
            if (mailtype != null)
            {
                detailsModel.UserType = mailtype.FirstOrDefault().UserSelectMailAddressType;
            }
            return detailsModel;
        }
        /// <summary>
        /// This method is used to update business structure,trade name on  submission master based on masterid
        /// </summary>
        /// <param name="detailsModel"></param>
        /// <returns></returns>
        public bool UpdateBusinessStructure(GeneralBusiness detailsModel)
        {
            try
            {
                // int enitityid = Convert.ToInt32(bblassociate.EntityId);
                var userAssociate = (from submaster in (FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == detailsModel.MasterId)) select submaster).First();
                userAssociate.BusinessStructure = detailsModel.BusinessStructure;
                userAssociate.TradeName = detailsModel.TradeName;
                Update(userAssociate, userAssociate.MasterId);
                Save();
                //subcheckrepo.UpdateIsMailAddress(detailsModel);
            }
            catch (Exception ex)
            { throw new Exception("Exception occurs in Update Business Structure", ex); }
            return true;
        }
        /// <summary>
        /// This method is used to update grand total on submission master based in master id
        /// </summary>
        /// <param name="grandTotal"></param>
        /// <param name="masterid"></param>
        /// <returns>Return bool value</returns>
        public bool UpdateEhopTotal(decimal grandTotal, string masterid)
        {
            try
            {
                var userAssociate = (from submaster in (FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterid)) select submaster).First();
                userAssociate.GrandTotal = grandTotal;
                Update(userAssociate, userAssociate.MasterId);
                Save();
            }
            catch (Exception ex)
            { throw new Exception("Exception occurs in Ehop Total", ex); }
            return true;
        }
        /// <summary>
        /// This method is used to update isehop on submission master based in master id
        /// </summary>
        /// <param name="cofoModel"></param>
        /// <returns>Return bool value</returns>
        public bool UpdateEhop(CofoHopDetailsModel cofoModel)
        {
            try
            {
                var userAssociate = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == cofoModel.MasterId).First();
                userAssociate.IseHOP = true;
                Update(userAssociate, userAssociate.MasterId);
                Save();
            }
            catch (Exception ex)
            { throw new Exception("Exception occurs in UpdateEhop", ex); }
            return true;
        }
        /// <summary>
        /// This method is used to insert renew to submission master based on user inputs
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns>Return string value</returns>
        public string InsertRenewData(RenewModel renewModel)
        {
            string status = string.Empty;
            var bblData = Bblrepo.FindByID(Convert.ToInt32(renewModel.EntityId)).FirstOrDefault();
            string expiry = Convert.ToDateTime(bblData.Expiration_Date).ToString("MM/dd/yyyy") ?? "01/01/1900";

            var dateTime = DateTime.ParseExact(expiry, "MM/dd/yyyy", null);
            renewModel.Extradays = ServiceExpiryStatus(dateTime);

            var renwaldata =
                FindBy(x => x.SubmissionLicense.Replace(System.Environment.NewLine, "").Trim() == renewModel.SubmissionLicense
                            && x.UserID.Replace(System.Environment.NewLine, "").Trim() == renewModel.UserId).ToList();
            if (renwaldata.Count() == 0)
            {
                string guid;
                Random rnd = new Random();
                try
                {
                    guid = Guid.NewGuid().ToString();
                    SubmissionMaster submaster = new SubmissionMaster();

                    renewModel.MasterId = guid;
                    submaster.MasterId = guid;
                    submaster.SubmissionLicense = renewModel.SubmissionLicense ?? "";
                    submaster.UserID = renewModel.UserId ?? "";
                    submaster.ActivityID = renewModel.ActivityId ?? "";
                    submaster.ApplicationFee = 70;
                    submaster.RAOFee = 0;
                    submaster.IseHOP = false;
                    submaster.eHOP = 0;
                    submaster.Status = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.Renew).Trim();
                    submaster.ExpirationDate = DateTime.Now;
                    submaster.ApprovedBy = string.Empty;
                    submaster.Description = "E" + DateTime.Now.Year.ToString().Substring(2, 2) + "8" +
                                            rnd.Next(1111, 9999);
                    submaster.App_Type = renewModel.App_Type ?? "B";
                    submaster.DocSubmType = string.Empty;
                    submaster.IsBusinessMustbeinDC = false;
                    submaster.IsHomeBased = false;
                    submaster.IsCofo = false;
                    submaster.IsPhysicalLocationVerify = false;
                    submaster.GrandTotal = renewModel.GrandTotalAmount;
                    submaster.isCorporationDivision = false;
                    submaster.BusinessStructure = string.Empty;
                    submaster.TradeName = string.Empty;
                    submaster.LicenseDuration = renewModel.LicenseDuration;
                    submaster.UserSelectMailAddressType = string.Empty;
                    submaster.UserBblAssociateId = renewModel.UserBblAssociateId;
                    submaster.CreatedDate = DateTime.Now;
                    submaster.Updatedate = DateTime.Now;
                    Add(submaster);
                    Save();
                    SubmissionApplicationCheckListRepo.InsertRenewChecklist(renewModel);
                    status = guid;
                    SubmissionCategoryRepo.InsertRenewalData(renewModel);
                    var updateSubMaster = (from paymentdetails in (
                          FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == renewModel.MasterId))
                                           select paymentdetails).First();
                    updateSubMaster.GrandTotal = renewModel.GrandTotalAmount;
                    Update(updateSubMaster, updateSubMaster.MasterId);
                    Save();
                }
                catch (Exception)
                {
                    status = "";
                }
            }
            else
            {
                status = renwaldata.FirstOrDefault().MasterId == null
                        ? ""
                        : renwaldata.FirstOrDefault().MasterId.Trim();
                renewModel.MasterId = status;
                SubmissionCategoryRepo.InsertRenewalData(renewModel);
                SubmissionApplicationCheckListRepo.InsertRenewChecklist(renewModel);
                SubmissionCategoryRepo.InsertRenewalData(renewModel);
                var updateSubMaster = (from paymentdetails in (
                          FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == renewModel.MasterId))
                                       select paymentdetails).First();
                updateSubMaster.GrandTotal = renewModel.GrandTotalAmount;
            }
            return status;
        }
        /// <summary>
        /// This method is used to get Status of the paritcular license 
        /// </summary>
        /// <param name="licenceNumber"></param>
        /// <returns>Return string value</returns>
        public string ValidateBblLicence(string licenseNumber)
        {
            var validate = Bblrepo.ValidateBblLicence(licenseNumber);
            return validate;
        }
        /// <summary>
        /// This method is used to update isehop on submission master based on master id
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return bool value</returns>
        public bool UpdateEhopNonSelect(string masterId)
        {
            var userAssociate = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).First();
            userAssociate.IseHOP = true;
            Update(userAssociate, userAssociate.MasterId);
            Save();
            return true;
        }
        /// <summary>
        /// his method is used to update document submission type on submission master based on master id
        /// </summary>
        /// <param name="documentCheck"></param>
        /// <returns>Return bool value</returns>
        public bool UpdateRenwalDocumentType(DocumentCheck documentCheck)
        {
            var userAssociate = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == documentCheck.MasterId).First();
            userAssociate.DocSubmType = documentCheck.DocType;
            Update(userAssociate, userAssociate.MasterId);
            Save();
            return true;
        }
        /// <summary>
        /// This method is used to submission Data based on master Id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Retrun submission data</returns>
        public SubmissionData MasterStatus(string masterId)
        {
            SubmissionData submissionData = new SubmissionData();
            //   string masterstatus = string.Empty;
            var userAssociate = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).ToList();
            if (userAssociate.Count() != 0)
            {
                var checklistdetails = SubmissionApplicationCheckListRepo.FindByMasterId(masterId).ToList();

                var masterdata = userAssociate.First();
                submissionData.Status = (masterdata.Status ?? "").Trim();
                submissionData.MasterId = (masterdata.MasterId ?? "").Trim();
                submissionData.TradeName = (masterdata.TradeName ?? "").Trim();
                submissionData.BusinessStructure = (masterdata.BusinessStructure ?? "").Trim();
                submissionData.IsCorporationDivision = masterdata.isCorporationDivision ?? false;
                submissionData.IsBusinessMustbeinDc = masterdata.IsBusinessMustbeinDC ?? false;
                submissionData.PremisesAddress = (masterdata.PremisesAddress ?? "").Trim();
                submissionData.IsFEIN = GetCleanHands_Fein_Ssn(masterdata.SubmissionLicense, masterdata.UserID);
                submissionData.AppType = masterdata.App_Type.Replace(System.Environment.NewLine, "").Trim().ToUpper();
                submissionData.DocSubmType = (masterdata.DocSubmType ?? "").Trim();
                submissionData.BusinessName = (masterdata.BusinessName ?? "").Trim();
                submissionData.BusinessOwnerName = (masterdata.BusinessName ?? "").Trim();
                submissionData.IsCategorySelfCertification = (masterdata.IsCategorySelfCertification ?? false);
                submissionData.SelectedMailType =
                    masterdata.UserSelectMailAddressType ?? "";
                if (submissionData.AppType == "I")
                { submissionData.IsIndividual = true; }
                else
                { submissionData.IsIndividual = false; }
                if (checklistdetails.Count() != 0)
                {
                    var submissioncheklistDetails = checklistdetails.FirstOrDefault();
                    submissionData.IsCoporateRegistration = submissioncheklistDetails.IsCorporateRegistration ?? false;
                    submissionData.IsResidentAgent = submissioncheklistDetails.IsResidentAgent ?? false;
                }
                //else
                //{
                //    submissionData.IsCoporateRegistration = false;
                //    submissionData.IsResidentAgent = false;
                //}
            }
            return submissionData;
        }
        /// <summary>
        /// This method is used to check clean hands data based in entity id and user id 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="userId"></param>
        /// <returns>Return bool value</returns>
        public bool GetCleanHands_Fein_Ssn(string entityId, string userId)
        {
            var getValue = Userbblrepository.CheckUserBBL(entityId, userId).ToList();
            if (getValue.Count > 0)
            {
                var getCleanHandsValue = getValue.FirstOrDefault().CleanHandsType_SSN_FEIN;
                return getCleanHandsValue == "FEIN";
            }
            return false;
        }
        /// <summary>
        /// This method is used to update user associate expiry date based on master id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return bool value</returns>
        public bool UpdateUserAssociateExpiryDate(string masterId)
        {
            bool finalResult = false;
            DateTime exiprydate = DateTime.Now;
            var submissionDetails = FindBy(x => x.MasterId == masterId).FirstOrDefault();

            var userdetails =
                Userbblrepository.CheckUserBBL(submissionDetails.SubmissionLicense, submissionDetails.UserID).ToList();
            if (userdetails.Any())
            {
                int enitityid = Convert.ToInt32(userdetails.FirstOrDefault().DCBC_ENTITY_ID.Trim());
                var bbldetails = Bblrepo.FindByID(enitityid).ToList();
                if (bbldetails.Any())
                {
                    exiprydate = Convert.ToDateTime(bbldetails.FirstOrDefault().Expiration_Date);
                }
                else
                { exiprydate = DateTime.Now; }
            }
            else
            { exiprydate = DateTime.Now; }
            if (submissionDetails != null)
            {
                if (submissionDetails.UserBblAssociateId != null)
                {
                    int licenseDuration = Convert.ToInt32(submissionDetails.LicenseDuration);
                    submissionDetails.DocSubmType = (submissionDetails.DocSubmType ?? "").Trim();
                    //if (submissionDetails.DocSubmType == "")
                    //{
                    var currentDate = Convert.ToDateTime(exiprydate);
                    var currentmonth = currentDate.Month;
                    var currentYear = currentDate.Year;
                    var startOfMonth = new DateTime(currentYear, currentmonth, 1);
                    finalResult = Userbblrepository.UpdateUserAssociateExpiryDate(Convert.ToInt32(submissionDetails.UserBblAssociateId),
                    startOfMonth.AddYears(licenseDuration).AddMonths(1).AddDays(-1));
                    //}
                }
            }

            return finalResult;
        }
        /// <summary>
        /// This method is used to update (transfer license) user id on submission master based on master id
        /// </summary>
        /// <param name="bbldoc"></param>
        /// <returns>Return bool value</returns>
        public bool TransferSubmissions(Submissiontransfer bbldoc)
        {
            var subtransfers = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == bbldoc.MasterId).ToList();
            if (subtransfers.Any())
            {
                var subtransfer = subtransfers.FirstOrDefault();
                subtransfer.UserID = bbldoc.ToUserId;
                Update(subtransfer, subtransfer.MasterId);
                Save();
                Userbblrepository.TransferSubmissions(bbldoc, subtransfer.SubmissionLicense.Trim());
            }
            else
            { Userbblrepository.TransferSubmissions(bbldoc, bbldoc.LicenseNumber.Trim()); }

            SubmissionBblAssociationToUserRepo.InsertTransferLicense(bbldoc);
            return true;
        }
        /// <summary>
        /// This method is used to get particular master id on submission master based on entity id and user id
        /// </summary>
        /// <param name="enitiyId"></param>
        /// <param name="userId"></param>
        /// <returns>Return master id as  string value</returns>
        public string GetmasterId(string entityId, string userId)
        {
            string masterId = string.Empty;
            var submissiondata =
                FindBy(x => x.SubmissionLicense.Replace(System.Environment.NewLine, "").Trim() == entityId.Trim() &&
                            x.UserID.Replace(System.Environment.NewLine, "").Trim() == userId.Trim()).ToList();
            if (submissiondata.Count() != 0)
            { masterId = submissiondata.FirstOrDefault().MasterId; }
            return masterId;
        }
        /// <summary>
        /// This method is used to update submission master based on master id
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Retrun bool value</returns>
        public bool UpdateNoDocStatus(string masterId)
        {
            bool finalResult = false;
            DateTime exiprydate = DateTime.Now;
            // int enitityid = Convert.ToInt32(bblassociate.EntityId);
            var userAssociate = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).First();
            int licenseDuration = Convert.ToInt32(userAssociate.LicenseDuration);
            userAssociate.Status = "Active";
            var currentDate = Convert.ToDateTime(exiprydate);
            var currentmonth = currentDate.Month;
            var currentYear = currentDate.Year;
            var startOfMonth = new DateTime(currentYear, currentmonth, 1);
            userAssociate.ExpirationDate = startOfMonth.AddYears(licenseDuration).AddDays(-1);
            userAssociate.Updatedate = DateTime.Now;
            Update(userAssociate, userAssociate.MasterId);
            Save();
          //  bool finalResult1 = false;

            var submissionDetails = FindBy(x => x.MasterId == masterId).FirstOrDefault();

            if (submissionDetails != null)
            {
                if (submissionDetails.UserBblAssociateId != null)
                {
                    finalResult = Userbblrepository.UpdateUserAssociateExpiryDate(Convert.ToInt32(submissionDetails.UserBblAssociateId),
                     startOfMonth.AddYears(licenseDuration).AddDays(-1));
                }
            }
            return finalResult;
        }
        /// <summary>
        /// This method is used to get user bbl service  based on user service id
        /// </summary>
        /// <param name="userServiceId"></param>
        /// <returns>Return user bbl service</returns>
        public IEnumerable<UserBBLService> GetBblServices(string userServiceId)
        {
            return Userbblrepository.FindByID(Convert.ToInt32(userServiceId)).ToList();
        }
        /// <summary>
        /// This method is used to get renewal data based on user inputs
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns>Return renew model</returns>
        public RenewModel GetRenewalSubmissionData(RenewModel renewModel)
        {
            var userdetails = Userbblrepository.FindByID(Convert.ToInt32(renewModel.UserBblAssociateId)).ToList();
            if (userdetails.Count() != 0)
            {
                var userdata = userdetails.FirstOrDefault();
                renewModel.MasterId = (renewModel.MasterId ?? "").Trim();
                var bbldata = Bblrepo.FindByID(Convert.ToInt32(renewModel.EntityId)).ToList();
                if (bbldata.Count() != 0)
                {
                    var DcbcBblData = bbldata.FirstOrDefault();
                    renewModel.CategoryName = (DcbcBblData.License_Category ?? "").Trim();

                    renewModel.BusinessOwnerName = DcbcBblData.Contact_Business_Name == null ? "" : DcbcBblData.Contact_Business_Name.ToUpper().Trim() == "NA" ? "" : DcbcBblData.Contact_Business_Name.ToUpper().Trim();

                    renewModel.NameofLicense = (DcbcBblData.Contact_FirstName == null ? "" : DcbcBblData.Contact_FirstName.ToUpper().Trim() == "NA" ? "" : DcbcBblData.Contact_FirstName.ToUpper().Trim()) +
                              " " + (DcbcBblData.Contact_MiddleName == null ? "" : DcbcBblData.Contact_MiddleName.ToUpper().Trim() == "NA" ? "" : DcbcBblData.Contact_MiddleName.ToUpper().Trim()) +
                              " " + (DcbcBblData.Contact_LastName == null ? "" : DcbcBblData.Contact_LastName.ToUpper().Trim() == "NA" ? "" : DcbcBblData.Contact_LastName.ToUpper().Trim());
                    renewModel.BblAddress = (DcbcBblData.Billing_Address1 == null ? "" : DcbcBblData.Billing_Address1.ToString().ToUpper().Trim() == "NA" ? "" : DcbcBblData.Billing_Address1.ToString().ToUpper().Trim()) + " " +
                       (DcbcBblData.Billing_Address2 == null ? "" : DcbcBblData.Billing_Address2.ToString().ToUpper().Trim() == "NA" ? "" : DcbcBblData.Billing_Address2.ToString().ToUpper().Trim()) + " " +
                       (DcbcBblData.Billing_Address3 == null ? "" : DcbcBblData.Billing_Address3.ToString().ToUpper().Trim() == "NA" ? "" : DcbcBblData.Billing_Address3.ToString().ToUpper().Trim());

                    renewModel.BillingAddress1 = DcbcBblData.Billing_Address1 == null ? "" : DcbcBblData.Billing_Address1.ToString().ToUpper().Trim() == "NA" ? "" : DcbcBblData.Billing_Address1.ToString().ToUpper().Trim();

                    renewModel.BillingAddress2 = DcbcBblData.Billing_Address2 == null ? "" : DcbcBblData.Billing_Address2.ToString().ToUpper().Trim() == "NA" ? "" : DcbcBblData.Billing_Address2.ToString().ToUpper().Trim();

                    renewModel.BillingAddress3 = DcbcBblData.Billing_Address3 == null ? "" : DcbcBblData.Billing_Address3.ToString().ToUpper().Trim() == "NA" ? "" : DcbcBblData.Billing_Address3.ToString().ToUpper().Trim();

                    renewModel.BblCity = DcbcBblData.Billing_CITY == null ? "" : DcbcBblData.Billing_CITY.ToUpper().Trim() == "NA" ? "" : (DcbcBblData.Billing_CITY ?? "").ToUpper().Trim();
                    renewModel.BblState = DcbcBblData.Billing_STATE == null ? "" : DcbcBblData.Billing_STATE.ToUpper().Trim() == "NA" ? "" : (DcbcBblData.Billing_STATE ?? "").ToUpper().Trim();
                    renewModel.BblZip = DcbcBblData.Billing_ZIP == null ? "" : DcbcBblData.Billing_ZIP.ToUpper().Trim() == "NA" ? "" : (DcbcBblData.Billing_ZIP ?? "").ToUpper().Trim();
                    renewModel.ContactFirstName = DcbcBblData.Contact_FirstName == null ? "" : DcbcBblData.Contact_FirstName.ToUpper().Trim() == "NA" ? "" : (DcbcBblData.Contact_FirstName ?? "").ToUpper().Trim();
                    renewModel.ContactMiddleName = DcbcBblData.Contact_MiddleName == null ? "" : DcbcBblData.Contact_MiddleName.ToUpper().Trim() == "NA" ? "" : (DcbcBblData.Contact_MiddleName ?? "").ToUpper().Trim();
                    renewModel.ContactLastName = DcbcBblData.Contact_LastName == null ? "" : DcbcBblData.Contact_LastName.ToUpper().Trim() == "NA" ? "" : (DcbcBblData.Contact_LastName ?? "").ToUpper().Trim();

                    var premisisData = BblRenewalRepo.FindBybblRenewBasedonLicensenumber
                        (userdata.B1_ALT_ID, userdata.SubmissionLicense).OrderBy(x => x.B1_APPL_STATUS_DATE).ToList();
                    if (premisisData.Count != 0)
                    {
                        var bblData = premisisData.FirstOrDefault();

                        renewModel.LrenNumber = bblData.b1_Alt_ID == null ? "" : bblData.b1_Alt_ID.ToUpper().Trim() == "NA" ? "" : (bblData.b1_Alt_ID ?? "").Trim();
                    }
                }
            }

            return renewModel;
        }

        //public RenewModel GetRenewalSubmissionData(RenewModel renewModel)
        //{
        //    var userdetails = Userbblrepository.FindByID(Convert.ToInt32(renewModel.UserBblAssociateId)).ToList();
        //    if (userdetails.Count() != 0)
        //    {
        //        var userdata = userdetails.FirstOrDefault();
        //        //       renewModel.EntityId=userdata.DCBC_ENTITY_ID.ToString();

        //        //       var masterdata = FindBy(x => x.SubmissionLicense.Trim() == renewModel.EntityId.Trim() && x.UserID.Trim() == userdata.UserID.Trim()).ToList();
        //        //        if (masterdata.Count() != 0)
        //        //{
        //        renewModel.MasterId = (renewModel.MasterId ?? "").Trim();
        //        var bbldata = Bblrepo.FindByID(Convert.ToInt32(renewModel.EntityId)).ToList();
        //        if (bbldata.Count() != 0)
        //        {
        //            var DcbcBblData = bbldata.FirstOrDefault();
        //            renewModel.CategoryName = (DcbcBblData.License_Category ?? "").Trim();

        //            var premisisData = BblRenewalRepo.FindBybblRenewBasedonLicensenumber
        //                (userdata.B1_ALT_ID, userdata.SubmissionLicense).OrderBy(x => x.B1_APPL_STATUS_DATE).ToList();
        //            if (premisisData.Count != 0)
        //            {
        //                var bblData = premisisData.FirstOrDefault();

        //                renewModel.BusinessOwnerName = bblData.OwnrApplicant_BUSINESS_NAME == null ? "" : bblData.OwnrApplicant_BUSINESS_NAME.ToUpper().Trim() == "NA" ? "" : bblData.OwnrApplicant_BUSINESS_NAME.ToUpper().Trim();
        //                renewModel.NameofLicense = (bblData.Contact_FirstName == null ? "" : bblData.Contact_FirstName.ToUpper().Trim() == "NA" ? "" : bblData.Contact_FirstName.ToUpper().Trim()) +
        //                      " " + (bblData.Contact_MiddleName == null ? "" : bblData.Contact_MiddleName.ToUpper().Trim() == "NA" ? "" : bblData.Contact_MiddleName.ToUpper().Trim()) +
        //                      " " + (bblData.Contact_LastName == null ? "" : bblData.Contact_LastName.ToUpper().Trim() == "NA" ? "" : bblData.Contact_LastName.ToUpper().Trim());
        //                renewModel.BblAddress = (bblData.Billing_Address1 == null ? "" : bblData.Billing_Address1.ToString().ToUpper().Trim() == "NA" ? "" : bblData.Billing_Address1.ToString().ToUpper().Trim()) + " " +
        //                  (bblData.Billing_Address2 == null ? "" : bblData.Billing_Address2.ToString().ToUpper().Trim() == "NA" ? "" : bblData.Billing_Address2.ToString().ToUpper().Trim()) + " " +
        //                  (bblData.Billing_Address3 == null ? "" : bblData.Billing_Address3.ToString().ToUpper().Trim() == "NA" ? "" : bblData.Billing_Address3.ToString().ToUpper().Trim());

        //                renewModel.BillingAddress1 = bblData.Billing_Address1 == null ? "" : bblData.Billing_Address1.ToString().ToUpper().Trim() == "NA" ? "" : bblData.Billing_Address1.ToString().ToUpper().Trim();

        //                renewModel.BillingAddress2 = bblData.Billing_Address2 == null ? "" : bblData.Billing_Address2.ToString().ToUpper().Trim() == "NA" ? "" : bblData.Billing_Address2.ToString().ToUpper().Trim();

        //                renewModel.BillingAddress3 = bblData.Billing_Address3 == null ? "" : bblData.Billing_Address3.ToString().ToUpper().Trim() == "NA" ? "" : bblData.Billing_Address3.ToString().ToUpper().Trim();

        //                renewModel.BblCity = bblData.Billing_CITY == null ? "" : bblData.Billing_CITY.ToUpper().Trim() == "NA" ? "" : (bblData.Billing_CITY ?? "").ToUpper().Trim();
        //                renewModel.BblState = bblData.Billing_STATE == null ? "" : bblData.Billing_STATE.ToUpper().Trim() == "NA" ? "" : (bblData.Billing_STATE ?? "").ToUpper().Trim();
        //                renewModel.BblZip = bblData.Billing_ZIP == null ? "" : bblData.Billing_ZIP.ToUpper().Trim() == "NA" ? "" : (bblData.Billing_ZIP ?? "").ToUpper().Trim();
        //                renewModel.ContactFirstName = bblData.Contact_FirstName == null ? "" : bblData.Contact_FirstName.ToUpper().Trim() == "NA" ? "" : (bblData.Contact_FirstName ?? "").ToUpper().Trim();
        //                renewModel.ContactMiddleName = bblData.Contact_MiddleName == null ? "" : bblData.Contact_MiddleName.ToUpper().Trim() == "NA" ? "" : (bblData.Contact_MiddleName ?? "").ToUpper().Trim();
        //                renewModel.ContactLastName = bblData.Contact_LastName == null ? "" : bblData.Contact_LastName.ToUpper().Trim() == "NA" ? "" : (bblData.Contact_LastName ?? "").ToUpper().Trim();

        //                //string gg=bblData.Contact_FirstName+
        //                //renewModel.BblAddress =
        //                //    (bblData.B1_HSE_NBR_START == null ? "" : bblData.B1_HSE_NBR_START.ToString().ToUpper().Trim() == "NA"? "": bblData.B1_HSE_NBR_START.ToString().ToUpper().Trim()) +
        //                // " "+   (bblData.B1_HSE_NBR_END == null ? "" : bblData.B1_HSE_NBR_END.ToString().ToUpper().Trim() == "NA"? "" : "-" + bblData.B1_HSE_NBR_END.ToString().ToUpper().Trim()) +
        //                // " " + (bblData.B1_HSE_FRAC_NBR_START == null ? "" : bblData.B1_HSE_FRAC_NBR_START.ToUpper().Trim() == "NA"? "": bblData.B1_HSE_FRAC_NBR_START.ToUpper().Trim()) +
        //                //    " " + (bblData.B1_UNIT_START == null ? "" : bblData.B1_UNIT_START.ToUpper().Trim() == "NA" ? "" : (bblData.B1_UNIT_START ?? "").ToUpper().Trim()) +
        //                // " " + (bblData.B1_STR_NAME == null ? "" : bblData.B1_STR_NAME.ToUpper().Trim() == "NA" ? "" : (bblData.B1_STR_NAME ?? "").ToUpper().Trim()) +
        //                //   " " + (bblData.B1_STR_SUFFIX == null ? "" : bblData.B1_STR_SUFFIX.ToUpper().Trim() == "NA" ? "" : (bblData.B1_STR_SUFFIX ?? "").ToUpper().Trim()) +
        //                //  " " + (bblData.B1_STR_SUFFIX_DIR == null ? "" : bblData.B1_STR_SUFFIX_DIR.ToUpper().Trim() == "NA" ? "" : (bblData.B1_STR_SUFFIX_DIR ?? "").ToUpper().Trim());
        //                //renewModel.BblCity = bblData.B1_SITUS_CITY == null ? "" : bblData.B1_SITUS_CITY.ToUpper().Trim() == "NA" ? "" : (bblData.B1_SITUS_CITY ?? "").ToUpper().Trim();
        //                //renewModel.BblState = bblData.B1_SITUS_STATE == null ? "" : bblData.B1_SITUS_STATE.ToUpper().Trim() == "NA" ? "" : (bblData.B1_SITUS_STATE ?? "").ToUpper().Trim();
        //                //renewModel.BblZip = bblData.B1_SITUS_ZIP == null ? "" : bblData.B1_SITUS_ZIP.ToUpper().Trim() == "NA" ? "" : (bblData.B1_SITUS_ZIP ?? "").ToUpper().Trim();
        //                renewModel.LrenNumber = bblData.b1_Alt_ID == null ? "" : bblData.b1_Alt_ID.ToUpper().Trim() == "NA" ? "" : (bblData.b1_Alt_ID ?? "").Trim();
        //            }
        //            else
        //            {
        //                renewModel.BblAddress = "";
        //                renewModel.BblCity = "";
        //                renewModel.BblState = "";
        //                renewModel.BblZip = "";
        //            }

        //            //var primaryId = MasterPrimaryCategoryRepo.SecondaryEndorsement(renewModel.CategoryName.Trim()).ToList();
        //            //if (primaryId.Count != 0)
        //            //{
        //            //    renewModel.CategoryId = primaryId.FirstOrDefault().PrimaryID ?? "";
        //            //}
        //            //else
        //            //{
        //            //    renewModel.CategoryId = string.Empty;
        //            //}
        //        }
        //    }
        //    // }
        //    return renewModel;
        //}
        /// <summary>
        /// This method is used to get submission master based on renew model
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns>Return submission master</returns>
        public IEnumerable<SubmissionMaster> GetRenewMasterUserAssociateID(RenewModel renewModel)
        {
            try
            {
                var userdetails = Userbblrepository.FindByID(Convert.ToInt32(renewModel.UserBblAssociateId)).ToList();
                if (userdetails.Count() != 0)
                {
                    var userdata = userdetails.FirstOrDefault();
                    renewModel.EntityId = userdata.SubmissionLicense.Trim();
                    renewModel.UserId = (userdata.UserID ?? "").Trim();
                }
                renewModel.EntityId = renewModel.EntityId ?? "";
                renewModel.UserId = renewModel.UserId ?? "";
                var masterdata = FindBy(x => x.SubmissionLicense.Trim() == renewModel.EntityId.Trim() && x.UserID.Trim() == renewModel.UserId).ToList();
                return masterdata;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Renew Master UserAssociateID", ex); ;
            }
        }
        /// <summary>
        /// This method is used to get tax revenue data based on master id.
        /// </summary>
        /// <param name="taxRevenueData"></param>
        /// <returns>Return tax revenue data</returns>
        public TaxRevenueData GetAddressDetails(TaxRevenueData taxRevenueData)
        {
            string fullAddress = string.Empty;
            var getMasterId = FindByMasterID(taxRevenueData.MasterId);
            if (getMasterId != null)
            {
                var firstOrDefault = getMasterId.FirstOrDefault();
                if (firstOrDefault != null)
                {
                    taxRevenueData.CurrentYear = (DateTime.Now.Year.ToString());
                    taxRevenueData.CreatedDate = (DateTime.Now.ToString("MM/dd/yyyy"));
                    var submissionLicense = firstOrDefault.SubmissionLicense;
                    var getbblDetails = Bblrepo.FindByID(Convert.ToInt32(taxRevenueData.EntityId)).FirstOrDefault();
                    if (getbblDetails != null)
                    {
                        string licenseNumber = (getbblDetails.B1_ALT_ID ?? "").Trim();
                      
                        var bblrenew = BblRenewalRepo.FindBybblRenewBasedonLicensenumber(licenseNumber, taxRevenueData.SubmissionLicense).ToList();
                        var bblrenewdata = bblrenew.FirstOrDefault();
                        RenewModel renewModel = new RenewModel();
                        renewModel.LicenseNumber = licenseNumber;
                        //var taxrevenue= _masterLicenseFeinRenewal.FindByLicense(renewModel).ToList();
                        // if (taxrevenue.Count() != 0)
                        // {
                        //     taxRevenueData.TaxNumber = (taxrevenue.FirstOrDefault().TextFEINNumber ?? "").Trim();
                        // }
                        string businessorganization = (getbblDetails.Business_Org ?? "Sole Proprietorship").Trim();
                        taxRevenueData.tradeName = (getbblDetails.Attr_TRADE_NAME ?? "NA").Trim();
                        if (businessorganization.ToUpper().Trim() == "SOLE PROPRIETORSHIP")
                        {
                            taxRevenueData.BusinessOwner = (getbblDetails.OwnrApplicant_FNAME == null ? ""
                               : getbblDetails.OwnrApplicant_FNAME.ToString().ToUpper().Trim() == "NA" ? ""
                                   : getbblDetails.OwnrApplicant_FNAME.ToString().ToUpper().Trim()).Trim()
                                   + " "
                                + (getbblDetails.OwnrApplicant_MNAME == null ? ""
                               : getbblDetails.OwnrApplicant_MNAME.ToString().ToUpper().Trim() == "NA" ? ""
                                   : " " + getbblDetails.OwnrApplicant_MNAME.ToString().ToUpper().Trim()).Trim()
                                + " "
                               + (getbblDetails.OwnrApplicant_LNAME == null ? ""
                               : getbblDetails.OwnrApplicant_LNAME.ToString().ToUpper().Trim() == "NA" ? ""
                                   : " " + getbblDetails.OwnrApplicant_LNAME.ToString().ToUpper().Trim()).Trim();
                            if ((getbblDetails.OwnrApplicant_FNAME ?? "NA").Trim() == "NA" && (getbblDetails.OwnrApplicant_MNAME ?? "NA").Trim() == "NA" && (getbblDetails.OwnrApplicant_LNAME ?? "NA").Trim() == "NA")
                            {
                                taxRevenueData.BusinessOwner = (getbblDetails.OwnrApplicant_BUSINESS_NAME ?? "").Trim();
                            }
                        }
                        else
                        {
                            taxRevenueData.BusinessOwner = (getbblDetails.OwnrApplicant_BUSINESS_NAME == null
                               ? ""
                               : getbblDetails.OwnrApplicant_BUSINESS_NAME.ToString().ToUpper().Trim() == "NA"
                                   ? ""
                                   : getbblDetails.OwnrApplicant_BUSINESS_NAME.ToString().ToUpper().Trim()).Trim();
                        }
                        taxRevenueData.TaxNumber = getbblDetails.FEIN_SSN == null
                              ? ""
                              : getbblDetails.FEIN_SSN.ToString().ToUpper().Trim() == "NA"
                                  ? ""
                                  : getbblDetails.FEIN_SSN.ToString().ToUpper().Trim();

                        taxRevenueData.TaxNumber = Regex.Replace(taxRevenueData.TaxNumber, @"[^\w\d]", "");
                        taxRevenueData.TaxNumber = Regex.Replace(taxRevenueData.TaxNumber, "[A-Za-z ]", "");
                        taxRevenueData.TaxNumber = taxRevenueData.TaxNumber.Replace(System.Environment.NewLine, "");
                        taxRevenueData.BusinessType = getbblDetails.B1_PER_TYPE == null
                              ? ""
                              : getbblDetails.B1_PER_TYPE.ToString().ToUpper().Trim() == "NA"
                                  ? ""
                                  : getbblDetails.B1_PER_TYPE.ToString().ToUpper().Trim();
                        taxRevenueData.FullAddress = ((getbblDetails.B1_HSE_NBR_START == null ? "" : getbblDetails.B1_HSE_NBR_START.ToString().ToUpper().Trim() == "NA" ? "" : getbblDetails.B1_HSE_NBR_START.ToString().ToUpper().Trim()) + " " +
                        (getbblDetails.B1_STR_NAME == null ? "" : getbblDetails.B1_STR_NAME.ToUpper().Trim() == "NA" ? "" : (getbblDetails.B1_STR_NAME ?? "").ToUpper().Trim()) + " " +
                          (getbblDetails.B1_STR_SUFFIX == null ? "" : getbblDetails.B1_STR_SUFFIX.ToUpper().Trim() == "NA" ? "" : (getbblDetails.B1_STR_SUFFIX ?? "").ToUpper().Trim()) + " " +
                              (getbblDetails.B1_STR_SUFFIX_DIR == null ? "" : getbblDetails.B1_STR_SUFFIX_DIR.ToUpper().Trim() == "NA" ? "" : (getbblDetails.B1_STR_SUFFIX_DIR ?? "").ToUpper().Trim())).Replace("  ", " ").Replace("  ", " ");
                    }
                }
            }
            return taxRevenueData;
        }
        /// <summary>
        /// This method is used to delete submission master based on master id
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return bool value</returns>
        public bool DeleteSubmissionMaster(string masterId)
        {
            SubmissionCategoryRepo.DeleteSubmissionCategory(masterId);
            SubmissionApplicationCheckListRepo.DeleteSubmissionCheckList(masterId);
            var submaster = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).FirstOrDefault();
            Delete(submaster);
            Save();
            return true;
        }
        /// <summary>
        /// This method is used to update business owner on submisson master based on master id and business owner
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="businessowner"></param>
        /// <returns>Return bool value</returns>
        public bool UpdateBusinessOwner(string masterId, string businessowner)
        {
            bool finalResult = false;
            try
            {
                var checksubmissionexist = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).ToList();
                if (checksubmissionexist.Count() != 0)
                {
                    var updatebusinessOwner = checksubmissionexist.Single();
                    updatebusinessOwner.BusinessOwnerName = businessowner;
                    Update(updatebusinessOwner, updatebusinessOwner.MasterId);
                    Save();
                    finalResult = true;
                }
            }
            catch (Exception)
            {
                finalResult = false;
            }
            return finalResult;
        }
        /// <summary>
        /// This method is used to update business name on submission master based on master id and business name
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="businessname"></param>
        /// <returns>Return bool value</returns>
        public bool UpdateBusinessName(string masterId, string businessname)
        {
            bool finalResult = false;
            try
            {
                var checksubmissionexist = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).ToList();
                if (checksubmissionexist.Count() != 0)
                {
                    var updatebusinessName = checksubmissionexist.Single();
                    updatebusinessName.BusinessName = businessname;
                    Update(updatebusinessName, updatebusinessName.MasterId);
                    Save();
                    finalResult = true;
                }
            }
            catch (Exception)
            {
                finalResult = false;
            }
            return finalResult;
        }
        /// <summary>
        /// This method is used to update premises address on submission master based on master id and premises addess
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="premisesAddress"></param>
        /// <returns></returns>
        public bool UpdatePremisesAddress(string masterId, string premisesAddress)
        {
            bool finalResult = false;
            try
            {
                var checksubmissionexist = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).ToList();
                if (checksubmissionexist.Count() != 0)
                {
                    var updatepremisesaddress = checksubmissionexist.Single();
                    updatepremisesaddress.PremisesAddress = premisesAddress;
                    Update(updatepremisesaddress, updatepremisesaddress.MasterId);
                    Save();
                    finalResult = true;
                }
            }
            catch (Exception)
            {
                finalResult = false;
            }
            return finalResult;
        }
        /// <summary>
        /// This method is used to update ehop file name and no doc order pdf status 
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="ehopFileName"></param>
        /// <param name="noDocumentOrderPdf"></param>
        /// <returns>Return bool value</returns>
        public bool UpdateSubmissionOrder(string masterId, string ehopFileName, string noDocumentOrderPdf)
        {
            bool finalResult = false;
            try
            {
                var checksubmissionexist = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).ToList();
                if (checksubmissionexist.Count() != 0)
                {
                    var updatebusinessName = checksubmissionexist.Single();

                    Update(updatebusinessName, updatebusinessName.MasterId);
                    Save();
                    finalResult = true;
                }
            }
            catch (Exception)
            {
                finalResult = false;
            }
            return finalResult;
        }
        /// <summary>
        /// This method is used to update submission master based on user inputs
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="licenseNumber"></param>
        /// <param name="status"></param>
        /// <param name="expiryDate"></param>
        /// <param name="entitiyid"></param>
        /// <returns>Return bool value</returns>
        public bool UpdateLicenseFromAccela(string masterId, string licenseNumber, string status, DateTime expiryDate, string entitiyid)
        {
            var updatesubmissionMaster = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).ToList();
            if (updatesubmissionMaster.Count() != 0)
            {
                var updateLicenseNumber = updatesubmissionMaster.AsEnumerable().FirstOrDefault();
                string oldLicense = updateLicenseNumber.SubmissionLicense.Trim();
                updateLicenseNumber.SubmissionLicense = licenseNumber.Trim();
                updateLicenseNumber.Status = status.Trim();
                updateLicenseNumber.ExpirationDate = expiryDate;
                Update(updateLicenseNumber, updateLicenseNumber.MasterId);
                Save();
                Userbblrepository.UpdateUserLicense(oldLicense, licenseNumber, entitiyid);
            }

            return true;
        }
        /// <summary>
        /// This method is used to update submission master based on master id 
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return bool values</returns>
        public bool UpdateUserSubmissionExpiryDate(string masterId)
        {
            bool finalResult = false;
            DateTime exiprydate = DateTime.Now;
            var submissionDetails = FindBy(x => x.MasterId == masterId).FirstOrDefault();

            if (submissionDetails != null)
            {
                if (submissionDetails.UserBblAssociateId != null)
                {
                    finalResult = Userbblrepository.UpdateUserAssociateExpiryDate(Convert.ToInt32(submissionDetails.UserBblAssociateId),
                     Convert.ToDateTime(DateTime.Now));
                }
            }

            return finalResult;
        }
        /// <summary>
        /// This method is used to update submission master data based on master id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return bool value</returns>
        public bool UpdateRenewNoDocStatus(string masterId)
        {
         //   bool finalResult = false;
            DateTime exiprydate = DateTime.Now;
            // int enitityid = Convert.ToInt32(bblassociate.EntityId);
            var userAssociate = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).First();
            var submissionDetails = FindBy(x => x.MasterId == masterId).FirstOrDefault();

            var userdetails = Userbblrepository.CheckUserBBL(submissionDetails.SubmissionLicense, submissionDetails.UserID).ToList();
            int enitityid = Convert.ToInt32(userdetails.FirstOrDefault().DCBC_ENTITY_ID);
            var bbldetails = Bblrepo.FindByID(enitityid).ToList();
            if (bbldetails.Any())
            {
                exiprydate = Convert.ToDateTime(bbldetails.FirstOrDefault().Expiration_Date);
            }
            int licenseDuration = Convert.ToInt32(userAssociate.LicenseDuration);
            //  userAssociate.Status = "Active";
            userAssociate.Status = GenericEnums.GetEnumDescription(GenericEnums.ApplicationValidateStatus.UnderreviewSpace);
            var currentDate = Convert.ToDateTime(exiprydate);
            var currentmonth = currentDate.Month;
            var currentYear = currentDate.Year;
            var startOfMonth = new DateTime(currentYear, currentmonth, 1);
            userAssociate.ExpirationDate = startOfMonth.AddYears(licenseDuration).AddMonths(1).AddDays(-1);
            userAssociate.Updatedate = DateTime.Now;
            Update(userAssociate, userAssociate.MasterId);
            Save();

            return true;
        }
        /// <summary>
        /// This method is used to get user bbl serivce based on master id
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return user bbl service</returns>
        public List<UserBBLService> GetUserBblDetails(string masterId)
        {
            var submissionDetails = FindBy(x => x.MasterId == masterId).FirstOrDefault();

            return Userbblrepository.CheckUserBBL(submissionDetails.SubmissionLicense, submissionDetails.UserID).ToList();
        }
        /// <summary>
        /// This method is used to get bbl data based on license number
        /// </summary>
        /// <param name="licenseNumber"></param>
        /// <returns>Return DCBC_ENTITY_BBL data</returns>
        public IQueryable<DCBC_ENTITY_BBL> GetBblDetails(string licenseNumber)
        {
            return Bblrepo.FindByLicense(licenseNumber).AsQueryable();
        }
        /// <summary>
        /// This method is used to update status on submission master based on master id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="status"></param>
        /// <returns>Return bool value</returns>
        public bool UpdateSubmissionsMasterExpirationDatewithStatus(string masterId, string status)
        {
            //bool finalResult = false;
            DateTime exiprydate = DateTime.Now;
            // int enitityid = Convert.ToInt32(bblassociate.EntityId);
            var userAssociate = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).First();
            int licenseDuration = Convert.ToInt32(userAssociate.LicenseDuration);
            userAssociate.Status = status;
            var currentDate = Convert.ToDateTime(exiprydate);
            var currentmonth = currentDate.Month;
            var currentYear = currentDate.Year;
            var startOfMonth = new DateTime(currentYear, currentmonth, 1);
            userAssociate.ExpirationDate = startOfMonth.AddYears(licenseDuration).AddDays(-1);
            userAssociate.Updatedate = DateTime.Now;
            Update(userAssociate, userAssociate.MasterId);
            Save();
            bool finalResult = false;

            var submissionDetails = FindBy(x => x.MasterId == masterId).FirstOrDefault();

            if (submissionDetails != null)
            {
                if (submissionDetails.UserBblAssociateId != null)
                {
                    finalResult = Userbblrepository.UpdateUserAssociateExpiryDate(Convert.ToInt32(submissionDetails.UserBblAssociateId),
                     startOfMonth.AddYears(licenseDuration).AddDays(-1));
                }
            }
            return finalResult;
        }
        /// <summary>
        /// This method is used to update status on submission master based on master id
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="bblStatus"></param>
        /// <returns>Return bool value</returns>
        public bool UpdateRenewalStatusAfterUnderReview(string masterId, string bblStatus)
        {
            var updatesubmissionMaster = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).ToList();
            if (updatesubmissionMaster.Count() != 0)
            {
                var updateLicenseNumber = updatesubmissionMaster.AsEnumerable().FirstOrDefault();
                if (updateLicenseNumber != null) updateLicenseNumber.Status = bblStatus.Trim();
                Save();
            }
            return true;
        }
        /// <summary>
        /// This method is used to check expiry data based on user data
        /// </summary>
        /// <param name="licenseNumber"></param>
        /// <param name="userId"></param>
        /// <param name="associateId"></param>
        /// <param name="masterId"></param>
        /// <param name="entityid"></param>
        /// <param name="expirationdate"></param>
        /// <returns>Return bool values</returns>
        public bool CheckExpirySoon(string licenseNumber, string userId, string associateId, string masterId, string entityid, string expirationdate)
        {
            try
            {
                BblAsscoiateService bblassiAsscoiateService = new BblAsscoiateService();
                var renewalData =
                    BblRenewalRepo.FindRenewLicense(licenseNumber)
                        .ToList();
                if (renewalData.Any())
                {
                    string lrenNumber = string.Empty;
                    var expiryrenewal = renewalData.Where(x => x.B1_APPL_STATUS_DATE == renewalData.FirstOrDefault().B1_APPL_STATUS_DATE).ToList();
                    if (expiryrenewal.Count > 1)
                    {
                        List<int> lrennumber = new List<int>();
                        foreach (var lren in expiryrenewal)
                        {
                            lren.b1_Alt_ID = lren.b1_Alt_ID ?? "".Trim();
                            if (lren.b1_Alt_ID != "")
                            {
                                var result =
                                    lren.b1_Alt_ID.Substring(lren.b1_Alt_ID.Length -
                                                             Math.Min(8,
                                                                 lren.b1_Alt_ID.Length));
                                lrennumber.Add(Convert.ToInt32(result));
                            }
                        }
                        var licenselren = lrennumber.OrderByDescending(v => v).ToList();
                        lrenNumber = licenselren.FirstOrDefault().ToString();
                    }
                    else
                    {
                        var dcbcEntityBblRenewals = expiryrenewal.FirstOrDefault();
                        if (dcbcEntityBblRenewals != null)
                            lrenNumber = dcbcEntityBblRenewals.b1_Alt_ID.ToString();
                    }

                    bblassiAsscoiateService.SubmissionLicense = lrenNumber.ToString();

                    bblassiAsscoiateService.B1_ALT_ID =
                       licenseNumber;
                    bblassiAsscoiateService.DCBC_ENTITY_ID =
                        entityid.ToString();
                    bblassiAsscoiateService.LicenseExpirationDate =
                        Convert.ToDateTime(expirationdate);
                    bblassiAsscoiateService.UserID = userId;
                    return Userbblrepository.BblSubmissionExpiryUpdate(
                        bblassiAsscoiateService,
                        Convert.ToInt32(associateId));
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in CheckExpiry Soon", ex);
            }
        }
    }
}