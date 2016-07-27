using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessCenter.Common;

namespace BusinessCenter.Data.Implementation
{
    public class SubmissionDocumentRepository : GenericRepository<SubmissionDocument>, ISubmissionDocumentRepository
    {
        protected ISubmissionCategoryRepository SubmissionCategoryrepo;
        protected IMasterCategoryDocumentRepository MasterCategoryDocumentrepo;
        protected IMasterPrimaryCategoryRepository MasterPrimaryCategoryprimaryrepo;
        protected IMasterSecondaryLicenseCategoryRepository MasterSecondaryrepo;
       protected ISubmissionMasterRepository Submissionmasterrepo;
        protected IMasterCategoryPhysicalLocationRepository MasterCategoryPhysicalrepo;
        protected ISubmissionMasterApplicationChcekListRepository Submissionchecklistrepo;
        protected ISubmissionIndividualRepository Submissionindividualrepo;
        protected ISubmissionDocumentToAccelaRepository _submissionDocumentToAccelaRepository;

        public SubmissionDocumentRepository(IUnitOfWork context, ISubmissionCategoryRepository subcatrepository,
            IMasterCategoryDocumentRepository masterreposiotry, IMasterPrimaryCategoryRepository primaryrepository,
        IMasterSecondaryLicenseCategoryRepository secondaryrepository, ISubmissionMasterRepository submasterrepository,
            IMasterCategoryPhysicalLocationRepository physicalrepository, ISubmissionMasterApplicationChcekListRepository subcheckrepository,
            ISubmissionIndividualRepository subindivrepository,ISubmissionDocumentToAccelaRepository submissionDocumentToAccelaRepository)
            : base(context)
        {
            SubmissionCategoryrepo = subcatrepository;
            MasterCategoryDocumentrepo = masterreposiotry;
            MasterPrimaryCategoryprimaryrepo = primaryrepository;
            MasterSecondaryrepo = secondaryrepository;
            Submissionmasterrepo = submasterrepository;
            MasterCategoryPhysicalrepo = physicalrepository;
            Submissionchecklistrepo = subcheckrepository;
            Submissionindividualrepo = subindivrepository;
            _submissionDocumentToAccelaRepository = submissionDocumentToAccelaRepository;
        }
       
        /// <summary>
        /// This method is used to Get Submission Document based on Application Unique Id.
        /// </summary>
        /// <param name="enitityid"></param>
        /// <returns>Retrun Submission Documents</returns>
        public IEnumerable<SubmissionDocument> FindDocumentList(string masterId)
        {
            var getSubmissionDocuments = FindBy(x => x.MasterId == masterId);
            return getSubmissionDocuments;
        }
        /// <summary>
        /// This method is used to Get Submission Document Status based on User Inputs.
        /// </summary>
        /// <param name="bbldoc"></param>
        /// <returns>Retrun Submission Document </returns>
        public IEnumerable<SubmissionDocument> IsUploadStatus(BblServiceDocuments bbldoc)
        {
            var submissionDocuments = FindBy(x => x.SubmissionCategoryID == bbldoc.SubmissionCategoryID && x.MasterCategoryDocId == bbldoc.CategoryID
                                                //&& x.DocRequired.Replace(System.Environment.NewLine, "").Trim() == bbldoc.DocRequired
                                                  && x.MasterId.Replace(System.Environment.NewLine, "").Trim() == bbldoc.MasterId.Trim());
                                                  //&& x.Description.Replace(System.Environment.NewLine, "").Trim() == bbldoc.Description.Trim());
            return submissionDocuments;
        }
        /// <summary>
        /// This method used to Get the Document List based on Application Unique Id.
        /// </summary>
        /// <param name="bbldoc"></param>
        /// <returns>Retrun Bbl Documents</returns>
        public IEnumerable<BblDocuments> DocumentList(BblDocuments bbldoc)
        {
            try
            {
            var documentservicelist = new List<BblDocuments>();
            var documentlist = new List<BblServiceDocuments>();
            var submaster = Submissionmasterrepo.FindByMasterID(bbldoc.MasterId).FirstOrDefault();
            var listdoc = SubmissionCategoryrepo.FindbyMaster(bbldoc.MasterId);
            string primaryendorement = string.Empty;
            string licenseName = string.Empty;
            string categoryCode= string.Empty;
            foreach (var categoryid in listdoc)
            {
                if (categoryid.CategoryType == GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.Primary).ToString().ToUpper())
                {
                  var category = MasterPrimaryCategoryprimaryrepo.FindByCategoryID(categoryid.CategoryTypeID).FirstOrDefault();
                    if (category != null)
                    {
                        categoryCode = category.CategoryCode ?? "";
                        primaryendorement = category.Endorsement;
                        licenseName = category.Description == null ? "" : category.Description.ToString().Trim();
                        if (submaster != null)
                        {
                            if (category.Description != null)
                            {
                                var questionsList = new QuestionsList
                                {
                                    MasterId = (bbldoc.MasterId??"").Trim(),
                                    CategoryName = (category.Description??"").Replace(System.Environment.NewLine, "").Trim(),
                                    CategoryTypeID = (categoryid.CategoryTypeID??"").Trim(),
                                    SubmissionCategoryID = categoryid.SubmissionCategoryID,
                                    Endorsement = (category.Endorsement ?? "").Trim(),
                                    License = (category.Description ??"").Trim(),
                                    SubmissionLicense = (submaster.SubmissionLicense??"").Trim(),
                                    CategoryCode = categoryCode.Trim(),
                                     LicenseName = (submaster.SubmissionLicense??"").Trim(),
                                     Type = "Y"
                                };
                                if (category.App_Type.Replace(System.Environment.NewLine, "").ToString().Trim().ToUpper() == "I")
                                { bbldoc.IsIndividual = true; }
                                else
                                { bbldoc.IsIndividual = false; }
                               // bbldoc.IsFEIN = Convert.ToBoolean(submaster.IsFEIN);
                                bbldoc.DocSubType = (submaster.DocSubmType??"").Trim();
                                bbldoc.AppType = (submaster.App_Type??"").Trim();
                                bbldoc.BusinessStructure = (submaster.BusinessStructure??"").Trim();
                                bbldoc.TradeName = (submaster.TradeName??"").Trim();
                              
                                bbldoc.IsCorporationDivision = Convert.ToBoolean(submaster.isCorporationDivision);
                                bbldoc.IsHop = Convert.ToBoolean(submaster.IseHOP);
                                bbldoc.IsCof = Convert.ToBoolean(submaster.IsCofo);
                                documentlist.AddRange(DocumentsList(questionsList, GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.Primary).ToString().ToUpper()));
                            }
                        }
                    }
                }
                else if (categoryid.CategoryType.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.SecondaryCategory).ToString().ToUpper())
                {
                    var secondarycategory = MasterSecondaryrepo.FindBySecondaryID(categoryid.CategoryTypeID).FirstOrDefault();
                    var secondaryprimary = MasterPrimaryCategoryprimaryrepo.SecondaryEndorsement(secondarycategory.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").ToString().Trim()).FirstOrDefault();
                    //var secondaryprimary = MasterPrimaryCategoryprimaryrepo.SecondaryEndorsement(secondarycategory.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").ToString().Trim())
                    //   .ToList();

                    QuestionsList questionsList = new QuestionsList
                    {
                        MasterId = bbldoc.MasterId,
                        CategoryName = (secondarycategory.SecondaryLicenseCategory??"").Replace(System.Environment.NewLine, "").Trim(),
                        CategoryTypeID = (categoryid.CategoryTypeID??"").Trim(),
                        SubmissionCategoryID = categoryid.SubmissionCategoryID,
                        Endorsement = (secondaryprimary.Endorsement ??"").Trim(),
                        License = (secondarycategory.SecondaryLicenseCategory??"") .Replace(System.Environment.NewLine, "").Trim(),
                        SubmissionLicense = (submaster.SubmissionLicense??"").Trim(),
                       CategoryCode = (secondaryprimary.CategoryCode??"").Trim(),
                        LicenseName = (submaster.SubmissionLicense??"").Trim()
                    };
                    documentlist.AddRange(DocumentsList(questionsList, GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.SecondaryCategory).ToString().ToUpper()));
                    var duplicatedocs = (from row in documentlist.ToList()
                                         where row.DocRequired.ToUpper().Trim() ==  GenericEnums.GetEnumDescription(GenericEnums.SupportingDocumentList.DocumentRequired).ToUpper().Trim().ToString()  &&
                                             row.Division.ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.SupportingDocumentList.DivisionName).ToUpper().Trim().ToString()
                                         select row).ToList();
                    if (duplicatedocs.Count() > 1)
                    {
                        var data = duplicatedocs.Where(x => x.Endorsement == (secondaryprimary.Endorsement ?? "").Trim()).LastOrDefault();
                        documentlist.Remove(data);
                    }
                }
            }
            var checklist = Submissionchecklistrepo.FindByMasterId(bbldoc.MasterId).FirstOrDefault();
            if (checklist!=null && checklist.IsDocForCleanHands == true)
            {
                var questionsList = new QuestionsList
                {
                    MasterId = (bbldoc.MasterId??"").Trim(),
                    CategoryName =  GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.CleanHandsDoc).ToString(),
                    CategoryTypeID = "TaxRevenue-B8B2D2AB-F1A8-42E5-BEAC-9B07E4910625",
                    SubmissionCategoryID = 999999,
                    Endorsement = (primaryendorement??"").Trim(),
                    License = (licenseName??"").Trim(),
                    SubmissionLicense = (submaster.SubmissionLicense??"").Trim(),
                    CategoryCode = (categoryCode??"").Trim(),
                    LicenseName = (submaster.SubmissionLicense??"").Trim(),
                    Type = "Y"
                };
                documentlist.AddRange(DocumentsList(questionsList, GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.Primary).ToString().ToUpper()));
            }
             
                    if (checklist != null && checklist.IsDocForeHop == true)
                    {
                         var questionsList = new QuestionsList
                         {
                             MasterId = (bbldoc.MasterId??"").Trim(),
                             CategoryName = GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.eHOP).ToString(),
                             CategoryTypeID = "Ehop-B8B2D2AB-F1A8-42E5-BEAC-9B07E4910625",
                             SubmissionCategoryID = 999999,
                             Endorsement = (primaryendorement??"").Trim(),
                             License = (licenseName??"").Trim(),
                             SubmissionLicense = (submaster.SubmissionLicense??"").Trim(),
                             CategoryCode = (categoryCode??"").Trim(),
                             LicenseName = (submaster.SubmissionLicense??"").Trim(),
                             Type = "Y"
                         };
                         documentlist.AddRange(DocumentsList(questionsList, GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.Primary).ToString().ToUpper()));
                    }
                        if (checklist != null && checklist.IsDocForHop == true)
                        {
                            var questionsList = new QuestionsList
                            {
                                MasterId = (bbldoc.MasterId??"").Trim(),
                                CategoryName = GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.HOP).ToString(),
                                CategoryTypeID = "Hop-B8B2D2AB-F1A8-42E5-BEAC-9B07E4910625",
                                SubmissionCategoryID = 999999,
                                Endorsement = (primaryendorement??"").Trim(),
                                License = (licenseName??"").Trim(),
                                SubmissionLicense = (submaster.SubmissionLicense??"").Trim(),
                                CategoryCode = (categoryCode??"").Trim(),
                                LicenseName = (submaster.SubmissionLicense??"").Trim(),
                                Type = "Y"
                            };
                            documentlist.AddRange(DocumentsList(questionsList, GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.Primary).ToString().ToUpper()));
                        }
                         if (checklist != null && checklist.IsDocForCofo == true)
                        {
                               var questionsList = new QuestionsList
                                {
                                    MasterId = (bbldoc.MasterId??"").Trim(),
                                    CategoryName = GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.COFO).ToString(),
                                    CategoryTypeID = "Cofo-B8B2D2AB-F1A8-42E5-BEAC-9B07E4910625",
                                    SubmissionCategoryID = 999999,
                                    Endorsement = (primaryendorement??"").Trim(),
                                    License = (licenseName??"").Trim(),
                                    SubmissionLicense = (submaster.SubmissionLicense??"").Trim(),
                                    CategoryCode = (categoryCode??"").Trim(),
                                    LicenseName = (submaster.SubmissionLicense??"").Trim(),
                                    Type = "Y"
                                };

                               documentlist.AddRange(DocumentsList(questionsList, GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.Primary).ToString().ToUpper()));
                        }
                       
                var doclist = from row in documentlist.ToList() orderby row.Agency, row.Division, row.DocRequired select row;
                bool checkstatus = ChecklistStatus(documentlist, bbldoc.MasterId, submaster.DocSubmType == null ? "" : submaster.DocSubmType.Trim());
                bool validate = Submissionindividualrepo.ValidateSubmission(bbldoc.MasterId);
                        var subapp = new BblDocuments
                        {
                            MasterId = (bbldoc.MasterId??"").Trim(),
                            BblServiceDoc = doclist.ToList(),
                            DocSubType = (bbldoc.DocSubType??"").Trim(),
                            IsIndividual = bbldoc.IsIndividual,
                            IsFEIN = bbldoc.IsFEIN,
                            IsHop = bbldoc.IsHop,
                            IsCof = bbldoc.IsCof,
                            AppType = (bbldoc.AppType ?? "").Trim(),
                            BusinessStructure = (bbldoc.BusinessStructure ?? "").Trim(),

                            TradeName = (submaster.TradeName ?? "").Trim(),
                            PremisesAddress = (submaster.PremisesAddress  ?? "").Trim(),
                            BusinessName = (submaster.BusinessName ?? "").Trim(), 
                            CategoryName = (licenseName ?? "").Trim(),
                            IsCorporationDivision = bbldoc.IsCorporationDivision,
                            ISFEINSSN = Convert.ToBoolean(checklist.FEIN_SSN),
                            IsCleanHandsVerify = Convert.ToBoolean(checklist.IsCleanHandsVerify),
                            IsCorporateRegistration = Convert.ToBoolean(checklist.IsCorporateRegistration),
                            IsBHAddress = Convert.ToBoolean(checklist.IsBHAddress),
                            IsBPAddress = Convert.ToBoolean(checklist.IsBPAddress),
                            IsMailAddress = Convert.ToBoolean(checklist.IsMailAddress),
                            IsResidentAgent = Convert.ToBoolean(checklist.IsResidentAgent),
                            IsDocforCleanHands = Convert.ToBoolean(checklist.IsDocForCleanHands),
                            IsDocforCofo = Convert.ToBoolean(checklist.IsDocForCofo),
                            IsDocforHop = Convert.ToBoolean(checklist.IsDocForHop),
                            IsDocforEhop = Convert.ToBoolean(checklist.IsDocForeHop),
                            IsSubmissionCofo = Convert.ToBoolean(checklist.IsSubmissionCofo),
                            IsSubmissionHop = Convert.ToBoolean(checklist.IsSubmissionHop),
                            IsSubmissioneHop = Convert.ToBoolean(checklist.IsSubmissioneHop),
                            CheckedStatus=checkstatus,
                            IsHomeBased =Convert.ToBoolean( submaster.IsHomeBased),
                            IsBusinessMustbeinDC=Convert.ToBoolean(submaster.IsBusinessMustbeinDC),
                            IsMcofo = Convert.ToBoolean(submaster.IsCofo),
                            CategoryCode = (categoryCode ?? "").Trim(),
                            IsIndividualValid = validate,
                            IsCategorySelfCertification = Convert.ToBoolean(submaster.IsCategorySelfCertification),
                            IsSelfCertification = Convert.ToBoolean(checklist.IsSelfCertification)

                        };
                documentservicelist.Add(subapp);
                return documentservicelist;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// This method is used to Check list Status based on Document Count, Application Unique Id and Document Type.
        /// </summary>
        /// <param name="docCount"></param>
        /// <param name="masterId"></param>
        /// <param name="docType"></param>
        /// <returns>Retrun Bool Result</returns>
        public bool ChecklistStatus(List<BblServiceDocuments> documentlist,string masterId,string docType)
        {
            try
            {
                bool docStatus = false;
                bool status = false;
                string apptype = string.Empty;
                var submaster = Submissionmasterrepo.FindByMasterID(masterId).ToList();
                if (submaster.Count() != 0)
                {
                    apptype = submaster.First().App_Type.Trim();
                }
                var checklist = Submissionchecklistrepo.FindByMasterId(masterId).FirstOrDefault();
                if (docType.ToUpper() != GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.InPerson).ToString().ToUpper())
                {
                    int unsubmitteddoc = 0;
                    foreach (var documents in documentlist)
                    {
                        var SubDoc = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").Trim() == masterId && x.FileName.Trim() != "" && x.MasterCategoryDocId == documents.CategoryID).ToList();
                        if(!SubDoc.Any())
                        {
                            unsubmitteddoc = unsubmitteddoc + 1;
                        }
                    }
                    if (unsubmitteddoc == 0)
                    { docStatus = true; }
                }
                //else if (docType.ToUpper() ==
                //         GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.InPerson).ToString().ToUpper())
                //{
                //    docStatus = true;
                //}
                else { docStatus = false; }
                if (docStatus == true && checklist.FEIN_SSN == true && (checklist.IsCleanHandsVerify == true || checklist.IsDocForCleanHands == true) && checklist.IsCorporateRegistration == true
                    && checklist.IsBHAddress == true && checklist.IsBPAddress == true && checklist.IsMailAddress == true && checklist.IsResidentAgent == true)
                {
                    status = true;
                    if (apptype.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.I).ToString().ToUpper())
                    {
                        status = Submissionindividualrepo.ValidateSubmission(masterId);
                    }
                }
                return status;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// This method is used to Get Document list based on Category Name
        /// </summary>
        /// <param name="questionsList"></param>
        /// <returns>Retrun Document List</returns>
        public List<BblServiceDocuments> DocumentsList(QuestionsList questionsList,string categoryType)
        {
            try
            {
                var masterdoc = MasterCategoryDocumentrepo.FindByID(questionsList.CategoryName.Replace(System.Environment.NewLine, "").Trim(), categoryType).ToList();
                var Documentlist = new List<BblServiceDocuments>();
                foreach (var documentlist in masterdoc)
                {

                    BblServiceDocuments servicelist = new BblServiceDocuments();
                    servicelist.MasterId = questionsList.MasterId;
                    servicelist.SubmissionId = questionsList.SubmissionLicense;
                    servicelist.SubmissionCategoryID = questionsList.SubmissionCategoryID;
                    servicelist.ApprovedBy = "";
                    servicelist.FileName = "";
                    servicelist.FileLocation = "";
                    servicelist.DocStatus = "Open";
                    servicelist.CategoryID = documentlist.MasterCategoryDocId.ToString();
                    servicelist.DocRequired = documentlist.SupportingDocuments == null ? "" : documentlist.SupportingDocuments.ToString().Trim();
                    servicelist.Agency = documentlist.Agency == null ? "" : documentlist.Agency.ToString().Trim();
                    servicelist.ShortName = documentlist.ShortDocName == null ? "" : documentlist.ShortDocName.ToString().Trim();
                    servicelist.Div = documentlist.Div == null ? "" : documentlist.Div.ToString().Trim();
                    servicelist.Division = documentlist.DivisionFullName == null ? "" : documentlist.DivisionFullName.ToString().Trim();
                    servicelist.Description = documentlist.Description == null ? "" : documentlist.Description.ToString().Trim();
                    servicelist.Endorsement = questionsList.Endorsement == null ? "" : questionsList.Endorsement.ToString().Trim();
                    servicelist.License = questionsList.License == null ? "" : questionsList.License.ToString().Trim();
                    servicelist.CheckListType = "Document";
                    servicelist.CategoryCode = questionsList.CategoryCode == null ? "" : questionsList.CategoryCode.ToString().Trim();
                    servicelist.LicenseName = questionsList.LicenseName;
                    var uploadlist = IsUploadStatus(servicelist).ToList();
                    if (uploadlist.Count() != 0)
                    {
                        servicelist.UploadFileName = uploadlist.FirstOrDefault().FileName;
                    }
                    else { servicelist.UploadFileName = string.Empty; }
                    servicelist.IsUpload = uploadlist.Any();

                    string Nodocs = servicelist.Agency.ToString().Trim().ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.NoDocs).ToString().ToUpper() ?
                        GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.NoDocument).ToString().ToUpper() :
                    servicelist.Agency.ToString().Trim().ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.NoDocs).ToString().ToUpper() ?
                     GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.NoDocument).ToString().ToUpper() :
                    servicelist.Agency.ToString().Trim().ToUpper();
                    if (Nodocs.ToUpper() != GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.NoDocument).ToString().ToUpper())
                    {
                        if (documentlist.InitialLicense.ToString().Trim().ToUpper() != GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.N).ToString().ToUpper())
                        {
                            Documentlist.Add(servicelist);
                        }
                    }
                }
                return Documentlist;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// This method is used to Delete Documents based on User Inputs
        /// </summary>
        /// <param name="bbldoc"></param>
        /// <returns>Retrun bool Result</returns>
        public bool UpdateSubmissionMaster(BblDocuments bbldoc)
        {
            try
            {
                bool status = false;
                List<DeleteRecord> drecord = new List<DeleteRecord>();
                status = Submissionmasterrepo.UpdateDocumentType(bbldoc);
                if (status == true && bbldoc.DocSubType.ToUpper().ToString().Trim() == "IN")
                {
                    var contact = GetAll().AsQueryable().Where(x => x.MasterId == bbldoc.MasterId);
                    if (contact.Count() != 0)
                    {
                        foreach (var concatid in contact)
                        {
                            DeleteRecord record = new DeleteRecord();
                            record.DeleteId = concatid.SubmDocId;
                            drecord.Add(record);
                        }
                    }
                    foreach (var delete in drecord)
                    {
                        var deleterecord = GetAll().AsQueryable().Single(x => x.SubmDocId == delete.DeleteId);
                        Delete(deleterecord);
                        Save();
                    }
                }
                return status;
            }
            catch (Exception ex)
            {

                throw new Exception("Exception Occurs in Update Submission Master", ex);
            }
        }
        /// <summary>
        /// This method is used to Insert Document Data based on user Inputs.
        /// </summary>
        /// <param name="bblServiceDocuments"></param>
        /// <returns>Return bool Result</returns>
        public UploadStatus InsertServiceDocuments(BblServiceDocuments bblServiceDocuments)
        {
//bool status = false;
            UploadStatus us = new UploadStatus();
            try
            {
                if (!IsUploadStatus(bblServiceDocuments).Any())
                {
                    SubmissionDocument subdocument = new SubmissionDocument
                    {
                        MasterId = bblServiceDocuments.MasterId,
                        SubmissionCategoryID = bblServiceDocuments.SubmissionCategoryID,
                        MasterCategoryDocId = bblServiceDocuments.CategoryID,
                        SubmittedDocumentType = bblServiceDocuments.CheckListType,
                        ApprovedBy = bblServiceDocuments.ApprovedBy,
                        DocRequired = bblServiceDocuments.DocRequired,
                        FileName = bblServiceDocuments.FileName,
                        FileLocation = bblServiceDocuments.FileLocation,
                        DocStatus = bblServiceDocuments.DocStatus,
                        Description = bblServiceDocuments.Description
                    };
                    Add(subdocument);
                    Save();
                    us.Status = true;
                    us.FileName = bblServiceDocuments.FileName;
                }
                else
                {
                    var subdocument = (from submaster in (IsUploadStatus(bblServiceDocuments)) select submaster).First();
                    subdocument.FileName = bblServiceDocuments.FileName;
                    subdocument.FileLocation = bblServiceDocuments.FileLocation;
                    Update(subdocument, subdocument.SubmDocId);
                    Save();
                    us.Status = true;
                    us.FileName = bblServiceDocuments.FileName;
                }
            }
            catch (Exception )
            {
                us.Status = false;
                us.FileName = string.Empty;
            }

            return us;

        }
        //public virtual void SaveChanges()
        //{
        //    Context.SaveChanges();
        //}
        /// <summary>
        /// This method is used to Delete Submission Document based on user Input.
        /// </summary>
        /// <param name="bbldoc"></param>
        /// <returns>Retrun bool Result</returns>
        public bool DeleteDocuments(BblServiceDocuments bbldoc)
        {
            bool status = false;
          
            try{
                var contact = FindBy(x => x.SubmissionCategoryID == bbldoc.SubmissionCategoryID
                    && x.Description == bbldoc.Description && x.DocRequired==bbldoc.DocRequired
                    && x.MasterId==bbldoc.MasterId).ToList();
                if (contact.Count() != 0)
                {
                    Delete(contact.FirstOrDefault());
                    Save();
                }
                status = true;
                }
                catch (Exception )
                { status = false; }
             return status;
            }
        /// <summary>
        /// This method is to get Renewal Document based on User Inputs.
        ///  </summary>
        /// <param name="questionsList"></param>
        /// <returns>Retrun BblServiceDocuments</returns>
        public List<BblServiceDocuments> RenewalDocument(RenewQuestionsList questionsList)
        {
            try
            {
                var doclist = new List<MasterCategoryDocument>();
                if (questionsList.IsCleanHands)
                {
                    var masterdoc1 = MasterCategoryDocumentrepo.FindByRenewID(GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.CleanHandsDoc).ToString().ToUpper()
                        , GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.Primary).ToString().ToUpper()).ToList();
                    if (masterdoc1.Count() != 0)
                    {
                        doclist.AddRange(masterdoc1.ToList());
                    }
                }
                if (questionsList.IsCorpRegistration)
                {
                    var masterdoc2 = MasterCategoryDocumentrepo.FindByRenewID(GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.Corpration).ToString().ToUpper(),
                        GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.Primary).ToString().ToUpper()).ToList();
                    if (masterdoc2.Count() != 0)
                    {
                        doclist.AddRange(masterdoc2.ToList());
                    }
                }
                var masterdoc = MasterCategoryDocumentrepo.FindByRenewID(questionsList.CategoryName.Replace(System.Environment.NewLine, "").Trim(),
                    GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.Primary).ToString().ToUpper()).ToList();
                doclist.AddRange(masterdoc.ToList());
                var Documentlist = new List<BblServiceDocuments>();
                foreach (var documentlist in doclist)
                {

                    BblServiceDocuments servicelist = new BblServiceDocuments();
                    servicelist.MasterId = (questionsList.MasterId ?? "").Trim();
                    servicelist.SubmissionId = (questionsList.SubmissionLicense ?? "").Trim();
                    servicelist.SubmissionCategoryID = questionsList.SubmissionCategoryID;
                    servicelist.ApprovedBy = "";
                    servicelist.FileName = "";
                    servicelist.FileLocation = "";
                    servicelist.DocStatus = "Open";
                    servicelist.CategoryID = documentlist.MasterCategoryDocId.ToString();
                    servicelist.DocRequired = (documentlist.SupportingDocuments ?? "").Trim();
                    servicelist.Agency = (documentlist.Agency ?? "").Trim();
                    servicelist.ShortName = (documentlist.ShortDocName ?? "").Trim();
                    servicelist.Div = (documentlist.Div ?? "").Trim();
                    servicelist.Division = (documentlist.DivisionFullName ?? "").Trim();
                    servicelist.Description = (documentlist.Description ?? "").Trim();
                    servicelist.Endorsement = (questionsList.Endorsement ?? "").Trim();
                    servicelist.License = (questionsList.License ?? "").Trim();
                    servicelist.CheckListType = "Document";
                    servicelist.CategoryCode = (questionsList.CategoryCode ?? "").Trim();
                    servicelist.LicenseName = (questionsList.LicenseName ?? "").Trim();
                    var uploadlist = IsUploadStatus(servicelist).ToList();
                    if (uploadlist.Count() != 0)
                    {
                        servicelist.UploadFileName = uploadlist.FirstOrDefault().FileName;
                    }
                    else { servicelist.UploadFileName = string.Empty; }
                    servicelist.IsUpload = uploadlist.Any();
                    string Nodocs = servicelist.Agency.ToString().Trim().ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.NoDocs).ToString().ToUpper() ?
                      GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.NoDocument).ToString().ToUpper() :
                  servicelist.Agency.ToString().Trim().ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.NoDocs).ToString().ToUpper() ?
                   GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.NoDocument).ToString().ToUpper() :
                  servicelist.Agency.ToString().Trim().ToUpper();
                    if (Nodocs.ToUpper() != GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.NoDocument).ToString().ToUpper())
                    {
                        if (documentlist.Renewal.ToString().Trim().ToUpper() != GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.N).ToString().ToUpper())
                        {
                            Documentlist.Add(servicelist);
                        }
                    }
                }

                return Documentlist;
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
        /// <summary>
        /// This method is used to Document Exists or not using Application Unique Id.
        /// </summary>
        /// <param name="documentCheck"></param>
        /// <returns>Return bool Result</returns>
        public bool CheckDocument(DocumentCheck documentCheck)
        {
            try
            {
            bool status = false;
            var documentlist = documentCheck.DocumentList.ToList();
            int Doclist = documentlist.Count();
            if (Doclist != 0)
            {
                string masterid = documentlist.FirstOrDefault().MasterId;
                if (documentCheck.DocType.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.OnLine).ToString().ToUpper())
                { 
                var submissionDoc =FindBy(x =>x.MasterId.Replace(System.Environment.NewLine, "").ToString().Trim() ==masterid).ToList();
                if(submissionDoc.Count()==Doclist)
                { status = true; }
                else
                {
                    status = false;
                }
                 }
                else
                { status = true; }
            }
            else
            {
                status = true;
            }
            return status;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region categorydocument
        /// <summary>
        /// This method is used to Get Specific Category Document based on Document Id.
        /// </summary>
        /// <param name="documentid"></param>
        /// <returns>Retrun Specific Category Document</returns>
        public IEnumerable<MasterCategoryDocument> FindByDocID(int documentid)
        {
            var documentList = MasterCategoryDocumentrepo.FindByDocID(documentid);
            return documentList;
        }
        /// <summary>
        ///  This method is used to Get Specific Category Document based on Document Id.
        /// </summary>
        /// <param name="documentid"></param>
        /// <returns>Retrun Specific Category Document</returns>
        public IEnumerable<MasterCategoryDocument> FindByDocBasedonDocId(int documentid)
        {
            var documentList = MasterCategoryDocumentrepo.FindByDocBasedonDocId(documentid);
            return documentList;
        }
        /// <summary>
        /// This method is used to Get List of Category Documents based on Category Name.
        /// </summary>
        /// <param name="categoryname"></param>
        /// <returns>Retrun List of  Category Documents</returns>
        public IEnumerable<MasterCategoryDocument> FindByDocName(string categoryname)
        {
            var documentList = MasterCategoryDocumentrepo.FindByDocName(categoryname);
            return documentList;
        }
        /// <summary>
        /// This method is used to Get List of Category Documents based on Category Name.
        /// </summary>
        /// <param name="categoryname"></param>
        /// <returns>Retrun List of  Category Documents</returns>
        public IEnumerable<MasterCategoryDocument> FindByID(string categoryname)
        {
            var documentList = MasterCategoryDocumentrepo.FindByID(categoryname, "");
            return documentList;
        }
        /// <summary>
        /// This method is used to Get List of Category Documents based on Category Name.
        /// </summary>
        /// <param name="categoryname"></param>
        /// <returns>Retrun List of  Category Documents</returns>
        public IEnumerable<MasterCategoryDocument> FindByRenewID(string categoryname)
        {
            var documentList = MasterCategoryDocumentrepo.FindByRenewID(categoryname,"");
            return documentList;
        }
        /// <summary>
        /// This method is used to Insert the Document based on User Inputs.
        /// </summary>
        /// <param name="categoryDocumentModel"></param>
        /// <returns>Return Number Result</returns>
        public int InsertUpdateCategoryDocuments(MasterCategoryDocumentModel categoryDocumentModel)
        {
            var documentList = MasterCategoryDocumentrepo.InsertUpdateCategoryDocuments(categoryDocumentModel);
            return documentList;
        }
        /// <summary>
        /// This method is used to Delete the Document based on User Inputs.
        /// </summary>
        /// <param name="categoryDocumentModel"></param>
        /// <returns>Return Bool Result</returns>
        public bool DeleteCategoryDocument(MasterCategoryDocumentModel categoryDocumentModel)
        {
            var documentList = MasterCategoryDocumentrepo.DeleteCategoryDocument(categoryDocumentModel);
            return documentList;
        }
        #endregion
        /// <summary>
        /// This method is used to Delete Specific Document based on Application Unique Id and Description.
        /// </summary>
        /// <param name="masterid"></param>
        /// <param name="description"></param>
        /// <returns>Return Bool Result</returns>
        public bool DeleteHopcofo(string masterid ,string description)
        {
            var deletehopcofo = FindBy(x => x.MasterId == masterid && x.DocRequired.ToUpper().Contains(description.ToUpper())).ToList();
            if (deletehopcofo.Count() != 0)
            {
                Delete(deletehopcofo.First());
                Save();
            }
            return true;
        }
        /// <summary>
        /// This method is used to Delete  Document based on User Input.
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns>Return Bool Result</returns>
        public bool DeleteRenewalDocument(RenewModel renewModel)
        {
            bool status = false;
            List<DeleteRecord> drecord = new List<DeleteRecord>();
            try
            { 
            var contact = GetAll().AsQueryable().Where(x => x.MasterId == renewModel.MasterId);
                if (contact.Count() != 0)
                {
                    foreach (var concatid in contact)
                    {
                        DeleteRecord record = new DeleteRecord();
                        record.DeleteId = concatid.SubmDocId;
                        drecord.Add(record);

                    }
                foreach (var delete in drecord)
                {
                    var deleterecord = GetAll().AsQueryable().Where(x => x.SubmDocId == delete.DeleteId).Single();
                    Delete(deleterecord);
                    Save();
                }
                
                }
                status = true;
            }
            catch (Exception )
            { status = false; }

            return status;
        }
        /// <summary>
        /// This method is used to Get List of Category Documents based on Category Name.
        /// </summary>
        /// <param name="categoryname"></param>
        /// <returns>Retrun List of  Category Documents</returns>
        public IEnumerable<MasterCategoryDocument> FindByDocNameBasedonCategoryName(string categoryname)
        {
            var documentList = MasterCategoryDocumentrepo.FindByDocNameBasedonCategoryName(categoryname);
            return documentList;
        }
        /// <summary>
        /// This method is used to Insert Document when Document Type like In Person based on Application Unique id.
        /// </summary>
        /// <param name="masterid"></param>
        /// <returns>Retrun bool Result</returns>
        public bool DocumentInsertion(string masterid, string licenseNumber)
        {
            try
            {
                var submaster = Submissionmasterrepo.FindByMasterID(masterid).ToList();
                if (submaster.Count() != 0)
                {

                    BblDocuments bbldoc = new BblDocuments();
                    bbldoc.MasterId = (submaster.First().MasterId ?? "").Trim();
                    bbldoc.DocSubType = (submaster.First().DocSubmType ?? "").Trim();
                    var documentlist = DocumentList(bbldoc);
                    var doc = documentlist.First().BblServiceDoc.ToList();
                    if (doc.Count() != 0)
                    {
                        //if (bbldoc.DocSubType.ToUpper() ==
                        //    GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.InPerson).ToString().ToUpper())
                        //{
                        //    foreach (var document in doc)
                        //    {
                        //        InsertServiceDocuments(document);
                        //    }
                        //}
                        //else
                        //{
                           // BblDocuments bbldoc=new BblDocuments();

                         var getdocuments=   DocumentList(bbldoc);
                         var submissiondocument = getdocuments.FirstOrDefault().BblServiceDoc;
                            var documents =from document in FindDocumentList(bbldoc.MasterId).ToList() join
                                             subdoc in submissiondocument on document.MasterCategoryDocId equals subdoc.CategoryID.ToString()
                                               select document;
                            if (documents.Count() != 0)
                            {
                                foreach (var document in documents)
                                {
                                    AccelaDocument accelaDocument = new AccelaDocument();
                                    accelaDocument.MasterId = (document.MasterId ?? "").Trim();
                                    accelaDocument.LicenseNumber = (licenseNumber ?? "").Trim();
                                    accelaDocument.MasterCategoryDocId = (document.MasterCategoryDocId ?? "").Trim();
                                    accelaDocument.FileName = (document.FileName ?? "").Trim();
                                    _submissionDocumentToAccelaRepository.UpdateFinalDocumentsToAccela(accelaDocument);
                                }
                            }  
                     
                            //we are updating expiration date and status of submission master

                            Submissionmasterrepo.UpdateSubmissionsMasterExpirationDatewithStatus(bbldoc.MasterId, "UnderReview");
                      
                    }
                    else
                    {
                      
                        Submissionmasterrepo.UpdateNoDocStatus(bbldoc.MasterId);
                    }

                }
                return true;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// This method is used to Insert Document when Document Type like In Person based on User Inputs.
        /// </summary>
        /// <param name="documentCheck"></param>
        /// <returns>Retrun bool Result</returns>
        public bool RenewaldocumentDelete(DocumentCheck documentCheck)
        {
            RenewModel renewModel = new RenewModel();
            renewModel.MasterId = documentCheck.MasterId;
            DeleteRenewalDocument(renewModel);
            if (documentCheck.DocType.ToUpper() != GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.OnLine).ToString().ToUpper())
            {
                foreach (var document in documentCheck.DocumentList)
                { InsertServiceDocuments(document); }
            }
            return true;
        }
        /// <summary>
        /// This method is used to Get List of Documents based on Application Unique Id.
        /// </summary>
        /// <param name="masterid"></param>
        /// <returns>Return List of Documents </returns>
        public List<BblServiceDocuments> DocumentByID(string masterid)
        {
            try
            {
                var Documentlist = new List<BblServiceDocuments>();
                var doclist = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").Trim() == masterid).ToList();
                var submaster = Submissionmasterrepo.FindByMasterID(masterid).FirstOrDefault();
                string licenseName = string.Empty;
                string categoryCode = string.Empty;
                string primaryendorement = string.Empty;
                var subcaegory = SubmissionCategoryrepo.FindbyMaster(masterid).ToList();
                if (subcaegory.Count() != 0)
                {
                    var category = MasterPrimaryCategoryprimaryrepo.FindByCategoryID(subcaegory.FirstOrDefault().CategoryTypeID).ToList();
                    if (category.Count != 0)
                    {
                        licenseName = (category.FirstOrDefault().Description ?? "").Trim();
                        categoryCode = (category.FirstOrDefault().CategoryCode ?? "").Trim();
                        primaryendorement = (category.FirstOrDefault().Endorsement ?? "").Trim();
                    }
                }
                if (doclist.Count() != 0)
                {
                    foreach (var documentlist in doclist)
                    {
                        var documentData = MasterCategoryDocumentrepo.FindByDocID(Convert.ToInt32(documentlist.MasterCategoryDocId.Trim())).ToList();
                        if (documentData.Count() != 0)
                        {
                            BblServiceDocuments servicelist = new BblServiceDocuments();
                            servicelist.MasterId = masterid;
                            if (submaster != null)
                            {
                                servicelist.SubmissionId = (submaster.SubmissionLicense ?? "").Trim();
                                servicelist.SubmissionCategoryID = documentlist.SubmissionCategoryID;
                                servicelist.ApprovedBy = "";
                                servicelist.FileName = (documentlist.FileName ?? "").Trim();
                                servicelist.FileLocation = (documentlist.FileLocation ?? "").Trim();
                                servicelist.DocStatus = (documentlist.DocStatus ?? "").Trim();
                                servicelist.CategoryID = (documentlist.MasterCategoryDocId?? "").Trim();
                                var masterCategoryDocument = documentData.FirstOrDefault();
                                servicelist.DocRequired = (masterCategoryDocument.SupportingDocuments ?? "").Trim();
                                servicelist.Agency = (documentData.FirstOrDefault().Agency ?? "").Trim();
                                servicelist.ShortName = (documentData.FirstOrDefault().ShortDocName ?? "").Trim();
                                servicelist.Div = (documentData.FirstOrDefault().Div ?? "").Trim();
                                servicelist.Division = (documentData.FirstOrDefault().DivisionFullName ?? "").Trim();
                                servicelist.Description = (documentData.FirstOrDefault().Description ?? "").Trim();
                                var category = MasterPrimaryCategoryprimaryrepo.SecondaryEndorsement(documentData.FirstOrDefault().CategoryName).ToList();
                                if (category.Count() == 0)
                                {
                                    servicelist.Endorsement = (primaryendorement ?? "").Trim();
                                    servicelist.License = (licenseName ?? "").Trim();
                                    servicelist.CheckListType = "Document";
                                    servicelist.CategoryCode = (categoryCode ?? "").Trim();
                                    servicelist.LicenseName = (submaster.SubmissionLicense ?? "").Trim();
                                }
                                else
                                {
                                    servicelist.Endorsement = (category.FirstOrDefault().Endorsement ?? "").Trim();
                                    servicelist.License = (category.FirstOrDefault().Description ?? "").Trim();
                                    servicelist.CheckListType = "Document";
                                    servicelist.CategoryCode = (category.FirstOrDefault().CategoryCode ?? "").Trim();
                                    servicelist.LicenseName = (submaster.SubmissionLicense ?? "").Trim();
                                }
                            }
                            var uploadlist = IsUploadStatus(servicelist).ToList();
                            if (uploadlist.Count() != 0)
                            {
                                servicelist.UploadFileName = (uploadlist.FirstOrDefault().FileName ?? "").Trim();
                            }
                            else { servicelist.UploadFileName = string.Empty; }
                            servicelist.IsUpload = uploadlist.Any();
                            Documentlist.Add(servicelist);
                        }
                    }
                }
                return Documentlist;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// This method is used to Update Status of Submission for renewal based on Application Unique Id.
        /// </summary>
        /// <param name="masterid"></param>
        /// <returns>Return bool Result</returns>
        public bool RenewalStatuUpdation(string masterid, string submissionLicense)
        {
            var submaster = FindBy(x => x.MasterId.Trim() == masterid).ToList();
            Submissionmasterrepo.UpdateRenewNoDocStatus(masterid);
            if (submaster.Count() != 0)
            {
                DocumentInsertion(masterid, submissionLicense);
            }
            //else
            //{
            //    DocumentInsertion(masterid, submissionLicense);
            //}
            return true;
        }

      
    }
}

