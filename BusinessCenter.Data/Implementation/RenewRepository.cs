using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Implementation
{
    public class RenewRepository : GenericRepository<UserBBLService>, IRenewRepository
    {
        protected IBblRepository BblRepository;
        protected ICorpRespository CorpRespository;
      //  protected IMasterLicenseFEINRenewal MasterLicenseFeinRenewal;
     //   protected IMasterTaxRevenueRepository MasterTaxRevenueRepository;
        protected ISubmissionMasterRepository SubmissionMasterRepository;
        protected IMasterBusinessActivityRepository MasterBusinessActivityRepository;
        protected IMasterPrimaryCategoryRepository MasterPrimaryCategoryRepository;
        protected ISubmissionCategoryRepository SubmissionCategoryRepository;
        protected ISubmissionDocumentRepository SubmissionDocumentRepository;
        protected ISubmissionMasterRenewalRepository SubmissionMasterRenewalRepository;
        protected IMasterSecondaryLicenseCategoryRepository MasterSecondaryLicenseCategoryRepository;
        protected IDCBC_ENTITY_BBL_RenewalsRepository BblRenewalsrepo;
        protected ILookup_ExistingCategoriesRepository LookupExistingCategoriesRepository;
        private readonly IDCBC_ENTITY_BBL_Renewal_InvoiceRepository _renewinvoicerepository;

        public RenewRepository(IUnitOfWork context, IBblRepository bblRepository, ICorpRespository corpRepository,
            ISubmissionMasterRepository submissionMasterRepository, IMasterBusinessActivityRepository masterBusinessActivityRepository,
           IMasterPrimaryCategoryRepository primaryCategoryRepository, ISubmissionCategoryRepository subcatrepository,
           ISubmissionDocumentRepository subdocrepository, ISubmissionMasterRenewalRepository submissionMasterRenewalReposiotry,
           IMasterSecondaryLicenseCategoryRepository secondarycatrepository, IDCBC_ENTITY_BBL_RenewalsRepository bblRenewalsrepository, ILookup_ExistingCategoriesRepository
           lookupExistingCategoriesRepository, IDCBC_ENTITY_BBL_Renewal_InvoiceRepository renewinvoicerepository)
            : base(context)
        {
            BblRepository = bblRepository;
            CorpRespository = corpRepository;
            //IMasterLicenseFEINRenewal renewRepository, MasterLicenseFeinRenewal = renewRepository;
          //  MasterTaxRevenueRepository = masterTaxRepository;
            SubmissionMasterRepository = submissionMasterRepository;
            MasterBusinessActivityRepository = masterBusinessActivityRepository;
            MasterPrimaryCategoryRepository = primaryCategoryRepository;
            SubmissionCategoryRepository = subcatrepository;
            SubmissionDocumentRepository = subdocrepository;
            SubmissionMasterRenewalRepository = submissionMasterRenewalReposiotry;
            MasterSecondaryLicenseCategoryRepository = secondarycatrepository;
            BblRenewalsrepo = bblRenewalsrepository;
            LookupExistingCategoriesRepository = lookupExistingCategoriesRepository;
            _renewinvoicerepository = renewinvoicerepository;
        }
        /// <summary>
        /// This method is used to get specific Renewal Data based on the user inputs.
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns>Retrun Renewal Data</returns>
        public RenewModel CheckRenewal(RenewModel renewModel)
        {
            try
            {
               if (renewModel.MasterId == "")
               {
                   CheckCategoryStatus(renewModel.EntityId, renewModel.LicenseNumber, renewModel.SubmissionLicense);
                   renewModel.RenewStatus = "Data";
                   var premisisData = BblRenewalsrepo.FindBybblRenewBasedonLicensenumber(renewModel.LicenseNumber,renewModel.SubmissionLicense).OrderBy(x => x.B1_APPL_STATUS_DATE).ToList();
                    if (premisisData.Count != 0)
                    {
                        var bblData = premisisData.FirstOrDefault();
                        renewModel.LrenNumber = bblData.b1_Alt_ID.ToUpper().Trim() == "NA" ? "" : (bblData.b1_Alt_ID ?? "").Trim();
                        if (bblData.License_Period != null ||bblData.License_Period!="")
                        {
                            if (bblData.License_Period != null && bblData.License_Period.Contains("4 YR"))
                            {
                                renewModel.LicenseDuration = 4;
                            }
                        }
                        renewModel.App_Type = bblData.B1_PER_TYPE == null ? "B" : bblData.B1_PER_TYPE.ToUpper() == "INDIVIDUAL LICENSE RENEWAL" ? "I" :
                            bblData.B1_PER_TYPE.ToUpper() == "Business License Renewal" ? "B" : "B";
                    }
                    else
                    {
                        renewModel.BblAddress = "";
                        renewModel.BblCity = "";
                        renewModel.BblState = "";
                        renewModel.BblZip = "";
                    }
                    renewModel.IsCorpRegistration = renewModel.IsCorpRegistration;
                 
                    renewModel.MasterId = SubmissionMasterRepository.InsertRenewData(renewModel);
                }
                else
                {
                    renewModel.RenewStatus = "Data";
                }
                renewModel.CorpNumber = renewModel.CorpNumber ?? "";
                if (renewModel.CorpNumber == "" && renewModel.IsCorp == false)
                {
                    SubmissionMasterRenewalRepository.updateIscorpStatus(renewModel.IsCorpRegistration, renewModel.MasterId);
                }
                else
                {
                   
                   string status= SubmissionMasterRenewalRepository.InsertRenewalDetails(renewModel);
                   if (status.ToUpper().Trim() == "CHANGE")
                   { SubmissionDocumentRepository.DeleteHopcofo(renewModel.MasterId, "Clean Hands Certificate"); }
                }
               
                var submissionrenewal = SubmissionMasterRenewalRepository.FindByID(renewModel.MasterId.Trim()).ToList();
                if (submissionrenewal.Count != 0)
                {
                    var submissionrenewnumber = submissionrenewal.FirstOrDefault();
                    renewModel.CorpNumber = renewModel.CorpNumber ?? "";
                    if (submissionrenewnumber != null)
                    {
                        renewModel.CorpNumber = submissionrenewnumber.CorpNumber;
                        renewModel.IsCorp = Convert.ToBoolean(submissionrenewnumber.IsDcraCorpDivision);
                        renewModel.LicenseNumber = submissionrenewnumber.SubmissionLicense;
                        renewModel.IsCorpRegistration =Convert.ToBoolean(submissionrenewnumber.IsCorpDocRegistration);
                    }
                }
                if (renewModel.CorpNumber == "" && !renewModel.IsCorp)
                {
                    renewModel.CorpNumber = GetCorpNumber(renewModel.MasterId);
                }
                var corprationdata = CorpRespository.FindByFileNumber(renewModel.CorpNumber.ToUpper()).ToList();
                if (!renewModel.IsCorp)
                {
                    if (corprationdata.Count() != 0)
                    {
                        string corpstatus =corprationdata.FirstOrDefault().EntityStatus.Replace(System.Environment.NewLine, "").Trim();
                        if (corpstatus.ToUpper() == "ACTIVE")
                        {
                            renewModel.CorpStatus = (corprationdata.FirstOrDefault().EntityStatus ?? "NODATA").Trim().ToUpper();
                        }
                        else
                        {
                            renewModel.CorpStatus = "FALSE";
                        }
                    }
                    else
                    {
                        renewModel.CorpStatus = "NODATA";
                    }
                }
                else
                {
                    renewModel.CorpStatus = "IsCorp";
                }
            }
            catch (Exception)
            {

                throw;
            }
            return renewModel;
        }
        /// <summary>
        /// This method is used to get renew data based on unique id
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns>Return renewmodel data</returns>
        public RenewModel CheckDocument(RenewModel renewModel)
        {
            try
            {
                renewModel.MasterId = (renewModel.MasterId ?? "").Trim();
                if (renewModel.MasterId != "")
                {

                    renewModel.RenewStatus = "Data";
                    var submissionrenewal = SubmissionMasterRenewalRepository.FindByID(renewModel.MasterId.Trim()).ToList();
                    if (submissionrenewal.Count() != 0)
                    {
                        var submissionrenewnumber = submissionrenewal.FirstOrDefault();
                        renewModel.CorpNumber = renewModel.CorpNumber ?? "";
                        renewModel.CorpNumber = submissionrenewnumber.CorpNumber;
                        renewModel.IsCorp = Convert.ToBoolean(submissionrenewnumber.IsDcraCorpDivision);
                        renewModel.LicenseNumber = submissionrenewnumber.SubmissionLicense ?? "";
                        renewModel.LrenNumber = submissionrenewnumber.SubmissionLicense ?? "";
                        renewModel.Extradays = submissionrenewnumber.Extradays ?? "";
                       
                        renewModel.IsCorpRegistration = submissionrenewnumber.IsCorpDocRegistration.Value;
                    }
                    if (renewModel.CorpNumber == null && !renewModel.IsCorp)
                    {
                        renewModel.CorpNumber = GetCorpNumber(renewModel.MasterId);
                    }
                    DocumentList(renewModel);
                    if (renewModel.DocumentList.Count() != 0)
                    {
                        DocumentCheck documentCheck=new DocumentCheck();
                        documentCheck.MasterId = renewModel.MasterId;
                        documentCheck.DocType = "ON";
                        SubmissionMasterRepository.UpdateRenwalDocumentType(documentCheck);
                    }
                }
            }
            catch
                (Exception)
            {

                throw;
            }

            return renewModel;
        }
        /// <summary>
        /// This method is used to get renew data based on user inputs
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns>Return renew model data</returns>
        public RenewModel CheckAmount(RenewModel renewModel)
        {
            try
            {
                var submissionRenewData = SubmissionMasterRepository.GetRenewalSubmissionData(renewModel);
                renewModel.MasterId = (renewModel.MasterId ?? "").Trim();
                if (renewModel.MasterId != "")
                {
                    var submissionrenewal = SubmissionMasterRenewalRepository.FindByID(renewModel.MasterId.Trim()).ToList();
                    if (submissionrenewal.Count() != 0)
                    {
                        var submissionrenewnumber = submissionrenewal.FirstOrDefault();
                        renewModel.CorpNumber = renewModel.CorpNumber ?? "";
                        renewModel.CorpNumber = submissionrenewnumber.CorpNumber;
                        renewModel.IsCorp = Convert.ToBoolean(submissionrenewnumber.IsDcraCorpDivision);
                        renewModel.LicenseNumber = submissionrenewnumber.SubmissionLicense ?? "";
                        renewModel.LrenNumber = submissionrenewnumber.SubmissionLicense ?? "";
                        renewModel.Extradays = submissionrenewnumber.Extradays ?? "";
                        renewModel.CurrentDate = DateTime.Now.ToString("MM/dd/yyyy");
                    }
                    _renewinvoicerepository.RenewalCalculation(renewModel);
                    SubmissionMasterRepository.UpdateEhopTotal(renewModel.GrandTotalAmount, renewModel.MasterId);
                }
            }
            catch
                (Exception)
            {

                throw;
            }

            return renewModel;
        }
        /// <summary>
        /// This method is used to get the specific corp number based on unique id
        /// </summary>
        /// <param name="masterid"></param>
        /// <returns>Return string value</returns>
        public string GetCorpNumber(string masterid)
        {

            var submissionRenewalData = SubmissionMasterRenewalRepository.FindByID(masterid).FirstOrDefault();
            if (submissionRenewalData != null)
            {
                return submissionRenewalData.CorpNumber;
            }
            return string.Empty;


        }

        /// <summary>
        /// This method is used to Get Documents based on Maseter Id
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns>List of Documents</returns>
        public RenewModel DocumentList(RenewModel renewModel)
        {
            try
            {
                var listdoc = SubmissionCategoryRepository.FindbyMaster(renewModel.MasterId).ToList();
                var submaster = SubmissionMasterRepository.FindByMasterID(renewModel.MasterId).ToList();
                var documentlist = new List<BblServiceDocuments>();
                foreach (var categoryid in listdoc)
                {
                    string categorycode = string.Empty;
                    if (categoryid.CategoryTypeID != "" && categoryid.CategoryType.ToUpper() == "PRIMARY")
                    {
                        var category = MasterPrimaryCategoryRepository.FindByCategoryID(categoryid.CategoryTypeID).ToList();//.FirstOrDefault();

                       var categoryData = category.FirstOrDefault();
                       categorycode = categoryData.CategoryCode ?? "";
                        
                        var questionsList = new RenewQuestionsList
                        {

                            MasterId = renewModel.MasterId,
                            CategoryName = categoryData.Description.Replace(System.Environment.NewLine, "").Trim(),
                            CategoryTypeID = categoryid.CategoryTypeID,
                            SubmissionCategoryID = categoryid.SubmissionCategoryID,
                            Endorsement = (categoryData.Endorsement ?? "").Trim(),
                            License = (categoryData.Description ?? "").Trim(),
                            SubmissionLicense = submaster.FirstOrDefault().SubmissionLicense,
                            CategoryCode = categoryData.CategoryCode ?? "",
                            LicenseName = renewModel.LicenseNumber,
                            IsCorpRegistration = renewModel.IsCorpRegistration,
                            IsCleanHands = renewModel.IsCleanHands,
                            Type = GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.Primary).ToString().ToUpper()
                        };
                        documentlist.AddRange(SubmissionDocumentRepository.RenewalDocument(questionsList));
                    }
                    if (categoryid.CategoryTypeID != string.Empty && categoryid.CategoryType.ToUpper() == "SECONDARYCATEGORY")
                    {
                        try
                        {
                            var category = MasterSecondaryLicenseCategoryRepository.FindBySecondaryID(categoryid.CategoryTypeID).ToList();
                            //.FirstOrDefault();
                            var categoryData = category.FirstOrDefault();
                            var questionsList = new RenewQuestionsList
                            {
                                MasterId = renewModel.MasterId,
                                CategoryName =
                                    categoryData.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").Trim(),
                                CategoryTypeID = categoryid.CategoryTypeID,
                                SubmissionCategoryID = categoryid.SubmissionCategoryID,
                                Endorsement = (categoryData.Endorsement ?? "").Trim(),
                                License = (categoryData.SecondaryLicenseCategory ?? "").Trim(),
                                SubmissionLicense = submaster.FirstOrDefault().SubmissionLicense,
                                CategoryCode = categorycode,
                                LicenseName = renewModel.LicenseNumber,
                                IsCorpRegistration = false,
                                IsCleanHands = false,
                                Type = GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.SecondaryCategory).ToString().ToUpper()
                            };
                            documentlist.AddRange(SubmissionDocumentRepository.RenewalDocument(questionsList));
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }

                renewModel.DocumentList = documentlist;
            }
            catch (Exception)
            {

                throw;
            }

            return renewModel;
        }
        /// <summary>
        /// this method is used to get the all Required document Uploaded or not based on DocumentCheck
        /// </summary>
        /// <param name="documentCheck"></param>
        /// <returns>Retrun Result bool</returns>
        public bool CheckDocument(DocumentCheck documentCheck)
        {
            try
            {
                var documentStatus = SubmissionDocumentRepository.CheckDocument(documentCheck);
                return documentStatus;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// This method is used to Delete the Existing Documents for renewal and Updated Document Type based on user input
        /// </summary>
        /// <param name="documentCheck"></param>
        /// <returns></returns>
        public bool UpdateRenwalDocumentType(DocumentCheck documentCheck)
        {
            try
            {
                SubmissionDocumentRepository.RenewaldocumentDelete(documentCheck);
                var updatedStatus = SubmissionMasterRepository.UpdateRenwalDocumentType(documentCheck);
                return updatedStatus;
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// This method is used Delete the document while user is not completed the renewal based on renewmodel
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns></returns>
        public bool DeleteRenewal(RenewModel renewModel)
        {
            try
            {
                var documentStatus = SubmissionDocumentRepository.DeleteRenewalDocument(renewModel);
                SubmissionMasterRenewalRepository.DeleteMasterRenewal(renewModel.MasterId);
                SubmissionMasterRepository.DeleteSubmissionMaster(renewModel.MasterId);
                return documentStatus;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// This method is used to check category exist or not based on entity id, license number and lren number
        /// </summary>
        /// <param name="entityID"></param>
        /// <param name="renewallicenseNumber"></param>
        /// <param name="renewallrenNumber"></param>
        /// <returns>Return string value</returns>
        public string CheckCategoryStatus(string entityID,string renewallicenseNumber,string renewallrenNumber)
        {
          try
          {
              string primarycategoryId = string.Empty;
              string categoryName = string.Empty;
              string licensenumber = string.Empty;
              string lrenNumber = string.Empty;
              var bbldata = BblRepository.FindByID(Convert.ToInt32(entityID)).ToList();
              if (bbldata.Count() != 0)
              {
                  categoryName = (bbldata.FirstOrDefault().License_Category ?? "").Trim();
                  licensenumber = renewallicenseNumber;//(bbldata.FirstOrDefault().B1_ALT_ID ?? "").Trim();
                  //var bblrenewdata = BblRenewalsrepo.FindByLicense(licensenumber);
                  //if (bblrenewdata.Count() != 0)
                  //{
                     lrenNumber =renewallrenNumber;// (bblrenewdata.FirstOrDefault().b1_Alt_ID ?? "").Trim();
                  //}
             //else
             //{
             //    return "NoCategory";
             //}

                  var bblrenewinvoice = _renewinvoicerepository.FindAmountByLicense(lrenNumber);

              string[] word = categoryName.Split('|');
                int count = 1;
                foreach (var category in word)
                {
                    if (category != "")
                    {

                        //foreach (var renewinvoice in bblrenewinvoice)
                        //{
                        //    bool negative = renewinvoice.GF_FEE > 0;
                        //    if (negative)
                        //    {
                        //        var lookupName = renewinvoice.Remove(
                        //            renewinvoice.Where(x => x.GF_FEE.Trim().ToUpper().Contains(renewinvoice.GF_FEE)));
                        //    }
                           
                        //}

                        if (bblrenewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains(GenericEnums.RenewalCheck.ENDORSEMENT.ToString().ToUpper())).Any())
                        {
                            bblrenewinvoice.Remove(
                                bblrenewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains(GenericEnums.RenewalCheck.ENDORSEMENT.ToString().ToUpper())).Single());
                        }
                        if (bblrenewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains(GenericEnums.RenewalCheck.ENHANCED.ToString().ToUpper())).Any())
                        {
                            bblrenewinvoice.Remove(
                                bblrenewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains(GenericEnums.RenewalCheck.ENHANCED.ToString().ToUpper())).Single());
                        }
                        if (bblrenewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains(GenericEnums.RenewalCheck.APPLICATION.ToString().ToUpper())).Any())
                        {
                            bblrenewinvoice.Remove(
                                bblrenewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains(GenericEnums.RenewalCheck.APPLICATION.ToString().ToUpper())).Single());
                        }
                        if (bblrenewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains(GenericEnums.RenewalCheck.RAO.ToString().ToUpper())).Any())
                        {
                            bblrenewinvoice.Remove(
                                bblrenewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains(GenericEnums.RenewalCheck.RAO.ToString())).Single());
                        }
                      var bblpenaltyexipred=bblrenewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (EXPIRED)")).ToList();
                      if (bblpenaltyexipred.Any())
                        {
                            if (bblpenaltyexipred.Count() == 1)
                            {
                                bblrenewinvoice.Remove(
                                    bblrenewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (EXPIRED)")).Single());
                            }
                            else
                            {
                                foreach (var penality in bblpenaltyexipred)
                                {
                                    bblrenewinvoice.Remove(
                                     bblrenewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (EXPIRED)") && x.GF_FEE == penality.GF_FEE).Single());
                               }
                            }

                        }
                      var bblpenaltylapsed= bblrenewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (LAPSE)")).ToList();
                      if (bblpenaltylapsed.Any())
                        {
                            if (bblpenaltylapsed.Count() == 1)
                            {
                                bblrenewinvoice.Remove(
                                    bblrenewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (LAPSE)")).Single());
                            }
                          
                            else
                            {
                                foreach (var penality in bblpenaltylapsed)
                                {
                                    bblrenewinvoice.Remove(
                                     bblrenewinvoice.Where(x => x.GF_DES.Trim().ToUpper().Contains("PENALTY FEE (LAPSE)") && x.GF_FEE == penality.GF_FEE).Single());
                                }
                            }
                        }
                        if (!bblrenewinvoice.Where(xxx => xxx.GF_DES.Trim() == category.Trim()).Any())
                        {
                            foreach (var renewinvoice in bblrenewinvoice)
                            {
                                var lookupName = LookupExistingCategoriesRepository.FindBy(renewinvoice.GF_DES.Trim()).ToList();
                                if (lookupName.Count() == 0)
                                {
                                    return "NoCategory";
                                }
                            }
                        }
                        var lookupCategoryName = LookupExistingCategoriesRepository.FindBy(category).ToList();
                        if (lookupCategoryName.Count() != 0)
                        {
                            string categoryname;
                            categoryname = lookupCategoryName.FirstOrDefault().NewCategoryName;
                            if (count == 1)
                            {
                                var categoryID = MasterPrimaryCategoryRepository.SecondaryEndorsement(categoryname).ToList();
                                if (categoryID.Count == 0)
                                {
                                    return  "NoCategory";
                                }
                                else
                                {
                                    primarycategoryId = categoryID.FirstOrDefault().PrimaryID;
                                }
                            }
                            else
                            {
                                var secondarcategoryID = MasterSecondaryLicenseCategoryRepository.FindByRenewSecondaryBasedonPrimary(categoryname, primarycategoryId).ToList();
                                if (secondarcategoryID.Count == 0)
                                {
                                    return "NoCategory";
                                }
                            }
                            count = count + 1;
                        }
                        else
                        {
                            return "NoCategory";
                        }
                    }
                }
              }
              else
              {
                  return "NoCategory";
              }
            }
            catch (Exception)
            {
                
                throw;
            }

            return "Category";
        }
    }
}
