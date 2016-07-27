using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class SubmissionCategoryRepository : GenericRepository<SubmissionCategory>, ISubmissionCategoryRepository
    {
        protected IMasterCategoryPhysicalLocationRepository MastercategoryphycialRepo;
        protected IFeeCodeMapRepository Feecoderepo;
        protected IFixFeeRepository FixfeeRepo;
        protected IMasterPrimaryCategoryRepository Primaryrepo;
        protected IMasterSecondaryLicenseCategoryRepository Secondaryrepo;
        protected IOSubCategoryFeesRepository SubcatFeerepo;
        protected ISubmissionMasterApplicationChcekListRepository Masterchecklistrepo;
        protected ISubmissionMasterRenewalRepository SubmissionMasterRenewalRepo;
        protected ISubmissionQuestionRepository Submissionquesrepo;
        protected IMasterSubCategoryRepository Mastersubcategoryrepo;
        protected IDCBC_ENTITY_BBL_Renewal_InvoiceRepository Renewinvoicerepo;
        protected ILookup_ExistingCategoriesRepository _lookupExistingCategoriesRepository;

        public SubmissionCategoryRepository(IUnitOfWork context,
            IMasterPrimaryCategoryRepository primaryRepository, IMasterSecondaryLicenseCategoryRepository secondaryrepository,
            IOSubCategoryFeesRepository subcatFeerepository, IFixFeeRepository fixfeeRepository,
            ISubmissionQuestionRepository subquesrepository, IMasterSubCategoryRepository supersubcatrepository,
            IMasterCategoryPhysicalLocationRepository mastercategoryphycialRepository, IFeeCodeMapRepository feecoderepository,
            ISubmissionMasterApplicationChcekListRepository masterchecklistreposoitory,
            ISubmissionMasterRenewalRepository subMasterRenewalRepository, IDCBC_ENTITY_BBL_Renewal_InvoiceRepository renewinvoicerepository,
            ILookup_ExistingCategoriesRepository lookup_ExistingCategoriesRepository)
            : base(context)
        {
            Primaryrepo = primaryRepository;
            SubcatFeerepo = subcatFeerepository;
            FixfeeRepo = fixfeeRepository;
            Secondaryrepo = secondaryrepository;
            Submissionquesrepo = subquesrepository;
            Mastersubcategoryrepo = supersubcatrepository;
            MastercategoryphycialRepo = mastercategoryphycialRepository;
            Feecoderepo = feecoderepository;
            Masterchecklistrepo = masterchecklistreposoitory;
            SubmissionMasterRenewalRepo = subMasterRenewalRepository;
            Renewinvoicerepo = renewinvoicerepository;
            _lookupExistingCategoriesRepository = lookup_ExistingCategoriesRepository;
        }

        /// <summary>
        /// This method is used to retrive All  Submission Categories List.
        /// </summary>
        /// <returns>List of Submission Categories</returns>
        public IEnumerable<SubmissionCategory> AllSubmissionCategories()
        {
            return GetAll().AsQueryable();
        }

        /// <summary>
        /// This method is used to retrive specific Submission Category Data based on Category Id.
        /// </summary>
        /// <param name="submissionCategoryModel"></param>
        /// <returns>Specific Submission Category Data</returns>
        public IEnumerable<SubmissionCategory> FindByID(SubmissionCategoryModel submissionCategoryModel)
        {
            var submissionCategory = FindBy(x => x.SubmissionCategoryID == submissionCategoryModel.SubmissionCategoryId);
            return submissionCategory;
        }

        /// <summary>
        /// This method is used to Get Submission Categories Name based on Application Unique Id to Display the Name in BBl List.
        /// </summary>
        /// <param name="categorylist"></param>
        /// <param name="masterid"></param>
        /// <returns>List of Submission Cateogries List</returns>
        public SubmissionCategoryList SubmissionCategoryListWithStatus(SubmissionCategoryList categorylist, string masterid)
        {
            try
            {
                //var categories = "";
                var listcategoreis = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterid);
                categorylist.Status = true;
                var categorieslist = new List<CategoryDetails>();
                foreach (var category in listcategoreis)
                {
                    CategoryDetails categorydetail = new CategoryDetails();
                    GenericEnums.CategoryList type = (GenericEnums.CategoryList)Enum.Parse(typeof(GenericEnums.CategoryList), category.CategoryType.ToUpper());
                    switch (type)
                    {
                        case GenericEnums.CategoryList.PRIMARY:
                            var primarycateogry = Primaryrepo.FindByCategoryIDBasedonPrimaryId(category.CategoryTypeID).ToList();
                            if (primarycateogry.Count() != 0)
                            {
                                var masterPrimaryCategory = primarycateogry.FirstOrDefault();
                                if (masterPrimaryCategory != null)
                                {
                                    categorylist.CategoryName = masterPrimaryCategory.Description.Replace(System.Environment.NewLine, "");
                                    categorylist.PrimaryCategoryCode = masterPrimaryCategory.CategoryCode.Replace(System.Environment.NewLine, "");

                                    categorydetail.Endoresment = masterPrimaryCategory.Endorsement.Replace(System.Environment.NewLine, "");
                                    categorydetail.CategoryName = masterPrimaryCategory.Description.Replace(System.Environment.NewLine, "");

                                    categorieslist.Add(categorydetail);
                                    if (categorylist.Status)
                                    {
                                        categorylist.Status = Convert.ToBoolean(masterPrimaryCategory.Status);
                                    }
                                    categorylist.IsPDFShow =
                                       Convert.ToBoolean(
                                           masterPrimaryCategory.IsPDFShow);
                                }
                            }
                            break;

                        case GenericEnums.CategoryList.SECONDARYCATEGORY:
                            var secondaryCategory = Secondaryrepo.FindBySecondaryBasedonSecondaryId(category.CategoryTypeID).ToList();
                            if (secondaryCategory.Count != 0)
                            {
                                var masterSecondaryLicenseCategory = secondaryCategory.FirstOrDefault();
                                if (masterSecondaryLicenseCategory != null)
                                {
                                    var secondarydetails = Primaryrepo.SecondaryEndorsement((masterSecondaryLicenseCategory.SecondaryLicenseCategory ?? "").Replace(System.Environment.NewLine, "").Trim()).ToList();
                                    if (secondarydetails.Any())
                                    {
                                        categorydetail.Endoresment = secondarydetails.FirstOrDefault().Endorsement.Replace(System.Environment.NewLine, "");
                                        categorydetail.CategoryName = secondarydetails.FirstOrDefault().Description.Replace(System.Environment.NewLine, "");
                                        categorieslist.Add(categorydetail);
                                    }
                                    categorylist.CategoryName = categorylist.CategoryName + ", " + masterSecondaryLicenseCategory.SecondaryLicenseCategory
                                                                  .Replace(System.Environment.NewLine, "");
                                    if (categorylist.Status)
                                    {
                                        categorylist.Status = Convert.ToBoolean(masterSecondaryLicenseCategory.Status);
                                    }
                                }
                            }
                            break;

                        case GenericEnums.CategoryList.SUBCATEGORY:
                            var supersubcateogory = Mastersubcategoryrepo.FindBySubCategoryBasedonSubcatId(category.CategoryTypeID).ToList();
                            if (supersubcateogory.Count != 0)
                            {
                                var masterSubCategory = supersubcateogory.FirstOrDefault();
                                if (masterSubCategory != null)
                                {
                                    categorylist.SubCategory = masterSubCategory.SubCategoryName.Replace(System.Environment.NewLine, "");
                                    if (categorylist.Status)
                                    {
                                        categorylist.Status = Convert.ToBoolean(masterSubCategory.Status);
                                    }
                                }
                            }
                            break;
                    }
                }
                categorylist.CategoryDetailsList = categorieslist;
                return categorylist;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method is used to Insert Primary Categories based on User Inputs.
        /// </summary>
        /// <param name="submissionApp"></param>
        /// <returns>Return Bool Result</returns>
        public bool InsertPrimaryBbl(SubmissionApplication submissionApp)
        {
            bool result;
            try
            {
                var subCategory = new SubmissionCategory();
                subCategory.MasterId = submissionApp.MasterId;
                subCategory.CategoryTypeID = submissionApp.PrimaryID;
                var primarydata = submissionApp.DetailedCategoryList.FirstOrDefault(x => x.CategoryId == subCategory.CategoryTypeID);
                var itemsqty = "";
                var qty = submissionApp.SubQuestion.Where(x => x.CategoryId == subCategory.CategoryTypeID);
                foreach (var itemqty in qty)
                {
                    if (itemsqty == "")
                    {
                        itemsqty = itemqty.Answer;
                    }
                    else
                    {
                        itemsqty = itemsqty + "," + itemqty.Answer;
                    }
                }
                if (primarydata != null)
                {
                    subCategory.Endorsement = primarydata.Endorsement;
                    subCategory.LicenseCategoryFee = Convert.ToDecimal(primarydata.CategoryLicenseFee);
                    subCategory.EndorsementFee = Convert.ToDecimal(primarydata.EndorsementFee);
                }
                subCategory.CategoryType = GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.Primary).ToUpper();
                subCategory.ItemQty = itemsqty == null ? "NA" : itemsqty.ToUpper().Trim() == "" ? "NA" : itemsqty;
                Add(subCategory);
                Save();
                result = true;
            }
            catch (Exception )
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// This method is used to Insert Secondary Categories based on User inputs.
        /// </summary>
        /// <param name="submissionApp"></param>
        /// <returns>Return Bool Result</returns>
        public bool InsertSecondaryBbl(SubmissionApplication submissionApp)
        {
            var result = false;
            try
            {
                if (submissionApp.Secondary != "")
                {
                    var secondary = submissionApp.Secondary.Split(',');
                    foreach (var secondaryId in secondary)
                    {
                        if (secondaryId.Trim() != ",")
                        {
                            try
                            {
                                var subCategory = new SubmissionCategory();
                                subCategory.MasterId = submissionApp.MasterId;
                                subCategory.CategoryTypeID = secondaryId;
                                var secondarydata = submissionApp.DetailedCategoryList.FirstOrDefault(x => x.CategoryId == subCategory.CategoryTypeID);
                                if (secondarydata != null)
                                {
                                    subCategory.Endorsement = secondarydata.Endorsement;
                                    subCategory.LicenseCategoryFee = secondarydata.CategoryLicenseFee;
                                    subCategory.EndorsementFee = secondarydata.EndorsementFee;
                                }
                                subCategory.CategoryType = GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.SecondaryCategory).ToUpper();
                                var itemsqty = "";
                                var qty = submissionApp.SubQuestion.Where(x => x.CategoryId == secondaryId);
                                foreach (var itemqty in qty)
                                {
                                    if (itemsqty == "")
                                    {
                                        itemsqty = itemqty.Answer;
                                    }
                                    else
                                    {
                                        itemsqty = itemsqty + "," + itemqty.Answer;
                                    }
                                }
                                subCategory.ItemQty = itemsqty == null ? "NA" : itemsqty.ToUpper().Trim() == "" ? "NA" : itemsqty;
                                Add(subCategory);
                                Save();
                                result = true;
                            }
                            catch (Exception )
                            {
                                result = false;
                            }
                        }
                    }
                }
            }
            catch (Exception )
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// This method is used to Insert Sub Categories based on User Inputs.
        /// </summary>
        /// <param name="submissionApp"></param>
        /// <returns>Return Bool Result</returns>
        public bool InsertSubSubCagteogryBbl(SubmissionApplication submissionApp)
        {
            var result = false;
            try
            {
                submissionApp.SubSubCategory = submissionApp.SubSubCategory ?? "";
                if (submissionApp.SubSubCategory != "")
                {
                    var subsubCat = submissionApp.SubSubCategory.Split(',');
                    foreach (var subsubCategory in subsubCat)
                    {
                        if (subsubCategory.Trim() != ",")
                        {
                            try
                            {
                                var subCategory = new SubmissionCategory
                                {
                                    MasterId = submissionApp.MasterId,
                                    CategoryTypeID = subsubCategory
                                };
                                var secondaryCategory = Secondaryrepo.FindBySecondaryID(subCategory.CategoryTypeID).FirstOrDefault();

                                if (secondaryCategory != null)
                                {
                                    subCategory.Endorsement = secondaryCategory.Endorsement == null ? "" : secondaryCategory.Endorsement.Trim();
                                    {
                                        subCategory.LicenseCategoryFee = 0;
                                    }
                                    subCategory.LicenseCategoryFee = LicenseAmount(subCategory.CategoryTypeID, string.Empty);
                                }
                                else
                                {
                                    subCategory.Endorsement = "";
                                    subCategory.LicenseCategoryFee = 0;
                                }
                                if (
                                    !FindByMasterId(submissionApp.MasterId.Replace(System.Environment.NewLine, ""), subCategory.Endorsement.Replace(System.Environment.NewLine, "")).Any())
                                {
                                    subCategory.EndorsementFee = 0;// FixfeeRepo.AllFixFees().FirstOrDefault().EndorsementFee;
                                }
                                else
                                {
                                    subCategory.EndorsementFee = 0;
                                }
                                //this will be comes for both primary and secondary subcategory
                                subCategory.CategoryType = GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.SubCategory).ToUpper();
                                subCategory.ItemQty = string.Empty;
                                Add(subCategory);
                                Save();
                                result = true;
                            }
                            catch (Exception )
                            {
                                result = false;
                            }
                        }
                    }
                }
            }
            catch (Exception )
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// This method is used to Get License Duration for the Particular Application
        /// </summary>
        /// <param name="submissionApp"></param>
        /// <returns>Retrun String Result</returns>
        public string GetLicenseDuration(SubmissionApplication submissionApp)
        {
            var questions = submissionApp.SubQuestion.ToList();
            string duration = string.Empty;
            var licenseduration =
                questions.Where(xx =>
                    xx.Question.ToUpper().ToString().Trim() == GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.DurationOfLicense).ToString().ToUpper()).ToList();
            if (licenseduration.Count() != 0)
            {
                duration = licenseduration.FirstOrDefault().Answer.ToUpper();
            }
            return duration;
        }

        /// <summary>
        /// This method is used to Get the user answer based on the Category Question with Category Id.
        /// </summary>
        /// <param name="submissionApp"></param>
        /// <param name="submissionCategoryId"></param>
        /// <returns>Return String Result</returns>
        public string ItemQuanity(SubmissionApplication submissionApp, string submissionCategoryId)
        {
            string itemquanity = "";
            var quantity = submissionApp.SubQuestion.Where(x => x.CategoryId == submissionCategoryId);
            foreach (var itemqty in quantity)
            {
                if (itemquanity == "")
                {
                    itemquanity = itemqty.Answer;
                }
                else
                {
                    itemquanity = itemquanity + "," + itemqty.Answer;
                }
            }
            return itemquanity;
        }

        /// <summary>
        /// This method is ued to Get the Category and License fee based on user inputs.
        /// </summary>
        /// <param name="submissionApp"></param>
        /// <returns>Return Submission Application</returns>
        public SubmissionApplication GetTotalFees(SubmissionApplication submissionApp)
        {
            try
            {
                string primaryName = string.Empty;
                string answer = GetLicenseDuration(submissionApp);
                var calYear = 1;
                switch (answer)
                {
                    case "TWO (2) YEAR":
                        calYear = 1;
                        break;

                    case "FOUR (4) YEAR":
                        calYear = 2;
                        break;
                }
                string secondaryendorsement;
              
                var unitone = "";
                bool getSecondaryPrimaryIsSubCategory = false;
                var subcategoryendoresement = string.Empty;
                var detailedCategoryList = new List<DetailedCategoryList>();
                var fixfee = FixfeeRepo.AllFixFees().FirstOrDefault();
                var cphycial = MastercategoryphycialRepo.FindCategoryID(submissionApp.PrimaryID).FirstOrDefault();
                var primarycateogry = Primaryrepo.FindByCategoryID(submissionApp.PrimaryID).ToList();
                var categoryCode = string.Empty;
                var secondaryIdCategoryCode = string.Empty;
                var endorsementslist = new List<string>();
                decimal licensecount = 0;
                if (primarycateogry.Count != 0)
                {
                    var masterPrimaryCategory = primarycateogry.FirstOrDefault();
                    if (masterPrimaryCategory != null)
                    {
                        var itemsqty = ItemQuanity(submissionApp, masterPrimaryCategory.PrimaryID);

                        var primary = masterPrimaryCategory.Endorsement.ToUpper().Trim() == "NA" ? "" : masterPrimaryCategory.Endorsement.ToUpper().Trim() == "N" ? ""
                                : masterPrimaryCategory.Endorsement.ToUpper().Trim() == "N/A" ? "" : masterPrimaryCategory.Endorsement.Trim();
                        categoryCode = masterPrimaryCategory.CategoryCode == null ? "" : masterPrimaryCategory.CategoryCode.Replace(System.Environment.NewLine, "").Trim();
                        subcategoryendoresement = primary;
                        if (primary != "")
                        {
                            endorsementslist.Add(masterPrimaryCategory.Endorsement);
                        }

                        var primarylicenseamount = LicenseAmount(submissionApp.PrimaryID, itemsqty);
                        licensecount = cphycial.ExemptFromAllFees.Trim().ToUpper() != GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES).ToUpper() ? primarylicenseamount : 0;
                        unitone = masterPrimaryCategory.UnitOne == null ? ""
                            : masterPrimaryCategory.UnitOne.ToUpper().Trim() == "NA" ? ""
                                : masterPrimaryCategory.UnitOne.Replace(System.Environment.NewLine, string.Empty).Trim();
                        var detailcatList = new DetailedCategoryList();
                        detailcatList.IsSubCategory = Convert.ToBoolean(masterPrimaryCategory.IsSubCategory);
                        if (masterPrimaryCategory.IsSubCategory == true)
                            detailcatList.SubCategoryName = GetSuperSubCategoryName(submissionApp);

                        detailcatList.Endorsement = primary;
                        detailcatList.CategoryId = submissionApp.PrimaryID;
                        detailcatList.LicenseCategory = masterPrimaryCategory.Description.Replace(System.Environment.NewLine, string.Empty);
                        primaryName = detailcatList.LicenseCategory;
                        detailcatList.CategoryCode = categoryCode;
                        detailcatList.LicenseDuration = answer;
                        if (masterPrimaryCategory.Description.Replace(System.Environment.NewLine, "").ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.Category.APARTMENT).ToUpper() ||
                             masterPrimaryCategory.Description.Replace(System.Environment.NewLine, "").ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.Category.FamilyRental).ToUpper())
                        {
                            detailcatList.IsRaoFeeApplied = true;
                            submissionApp.RAOFee = Convert.ToDecimal(fixfee.RAOFee);
                        }
                        else
                        {
                            detailcatList.IsRaoFeeApplied = false;
                            submissionApp.RAOFee = 0;
                        }
                        var secondary = itemsqty.Split(',');
                        if (secondary.Count() > 1)
                        {
                            detailcatList.Units = secondary[0] + " Room(s)" + ", " + secondary[1] + " Kitchen(s)";
                        }
                        else
                        {
                            detailcatList.Units = itemsqty == null
                                ? "NA"
                                : itemsqty.ToUpper().Trim() == "" ? "NA" : itemsqty + " " + unitone;
                        }
                        //if (!string.IsNullOrEmpty(submissionApp.Secondary))
                        //{
                        //    detailcatList.ApplicationFee = Convert.ToDecimal(fixfee.ApplicationFee) * calYear;
                        //    detailcatList.EndorsementFee = Convert.ToDecimal(fixfee.EndorsementFee) * calYear;
                        //    if (cphycial.ExemptFromAllFees.Trim().ToUpper() !=
                        //        GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES).ToUpper())
                        //    {
                        //        detailcatList.CategoryLicenseFee = primarylicenseamount*calYear;
                        //    }
                        //    else
                        //    {
                        //        detailcatList.CategoryLicenseFee = 0;
                        //    }
                        //    detailcatList.SubTotal = detailcatList.ApplicationFee + detailcatList.EndorsementFee +
                        //                             detailcatList.CategoryLicenseFee;
                        //    detailcatList.TechFee = (detailcatList.SubTotal / 100) * 10;
                        //    detailcatList.TotalFee = detailcatList.SubTotal + detailcatList.TechFee;
                        //}
                        //else
                        //{

                            if (primary != "" &&
                                cphycial.ExemptFromAllFees.Trim().ToUpper() !=
                                GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES).ToUpper())
                            {
                                detailcatList.ApplicationFee = Convert.ToDecimal(fixfee.ApplicationFee)*calYear;
                                detailcatList.EndorsementFee = Convert.ToDecimal(fixfee.EndorsementFee)*calYear;
                                detailcatList.CategoryLicenseFee = primarylicenseamount*calYear;
                                detailcatList.SubTotal = detailcatList.ApplicationFee + detailcatList.EndorsementFee +
                                                         detailcatList.CategoryLicenseFee;
                                detailcatList.TechFee = (detailcatList.SubTotal/100)*10;
                                detailcatList.TotalFee = detailcatList.SubTotal + detailcatList.TechFee;
                            }
                            else
                            {
                                detailcatList.ApplicationFee = 0;
                                detailcatList.EndorsementFee = 0;
                                detailcatList.CategoryLicenseFee = 0;
                                detailcatList.SubTotal = 0;
                                detailcatList.TechFee = 0;
                                detailcatList.TotalFee = 0;
                            }
                       // }
                        detailedCategoryList.Add(detailcatList);
                    }
                }
                if (submissionApp.Secondary != null)
                {
                    if (submissionApp.Secondary != "")
                    {
                        var secondary = submissionApp.Secondary.Split(',');
                        foreach (var secondaryid in secondary)
                        {
                            if (secondaryid.Trim() != ",")
                            {
                                var secondaryCategory =
                                    Secondaryrepo.FindBySecondaryID(secondaryid).FirstOrDefault();
                                secondaryendorsement = secondaryCategory.Endorsement == null ? ""
                                        : secondaryCategory.Endorsement.ToUpper().Trim() == "NA" ? ""
                                            : secondaryCategory.Endorsement.ToUpper().Trim() == "N" ? ""
                                                : secondaryCategory.Endorsement.ToUpper().Trim() == "N/A" ? ""
                                                    : secondaryCategory.Endorsement.Trim();

                                if (secondaryendorsement != "")
                                {
                                    if (secondaryCategory.Endorsement != "NA" || secondaryCategory.Endorsement != "N" || secondaryCategory.Endorsement != "N/A")
                                    {
                                        endorsementslist.Add(secondaryendorsement);
                                    }
                                }
                                else
                                {
                                    var secondaryprimary =
                                        Primaryrepo.SecondaryEndorsement(secondaryCategory.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").Trim())
                                            .ToList();

                                    if (secondaryprimary.Count != 0)
                                    {
                                        var secondarycategory = secondaryprimary.FirstOrDefault();
                                        unitone = secondarycategory.UnitOne == null
                                            ? "" : secondarycategory.UnitOne.ToUpper().Trim() == "NA" ? ""
                                            : secondarycategory.UnitOne.Replace(System.Environment.NewLine, string.Empty).Trim();
                                        secondaryendorsement = secondarycategory.Endorsement.ToUpper().Trim() == "NA" ? ""
                                                    : secondarycategory.Endorsement.ToUpper().Trim() == "N" ? "" : secondarycategory.Endorsement.ToUpper().Trim() == "N/A"
                                                            ? "" : secondarycategory.Endorsement.Trim();
                                        secondaryIdCategoryCode = secondarycategory.CategoryCode;
                                        //rupu
                                        getSecondaryPrimaryIsSubCategory = Convert.ToBoolean(secondarycategory.IsSubCategory);
                                        if (secondaryendorsement != "")
                                        {
                                            endorsementslist.Add(secondaryendorsement);
                                        }
                                        if (secondaryCategory.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").ToUpper() ==
                                           GenericEnums.GetEnumDescription(GenericEnums.Category.GeneralBusiness).ToUpper())
                                        {
                                            subcategoryendoresement = secondarycategory.Endorsement;
                                            categoryCode = secondarycategory.CategoryCode;
                                        }
                                    }
                                }
                                var itemsqty = ItemQuanity(submissionApp, secondaryid);
                                var secondarylicenseamount = SecondLicenseAmount(secondaryid, itemsqty);
                                if (!string.IsNullOrEmpty(submissionApp.Secondary))
                                {
                                    licensecount = licensecount + secondarylicenseamount;
                                }
                                else
                                {
                                    licensecount = 0;
                                }
                                if (secondaryCategory != null)
                                {
                                    var detailcatList = new DetailedCategoryList();
                                    if (getSecondaryPrimaryIsSubCategory)
                                        detailcatList.SubCategoryName = GetSuperSubCategoryName(submissionApp);

                                    detailcatList.Endorsement = secondaryendorsement;
                                    detailcatList.CategoryId = secondaryid;
                                    detailcatList.LicenseCategory = secondaryCategory.SecondaryLicenseCategory.Replace(System.Environment.NewLine, string.Empty);
                                    detailcatList.CategoryCode = secondaryIdCategoryCode;
                                    detailcatList.LicenseDuration = answer;
                                    if (secondaryCategory.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.Category.APARTMENT).ToUpper() ||
                        secondaryCategory.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.Category.FamilyRental).ToUpper())
                                    {
                                        detailcatList.IsRaoFeeApplied = true;
                                        submissionApp.RAOFee = Convert.ToDecimal(fixfee.RAOFee);
                                    }
                                    else
                                    {
                                        detailcatList.IsRaoFeeApplied = false;
                                        submissionApp.RAOFee = 0;
                                    }
                                    var secondaryitems = itemsqty.Split(',');
                                    if (secondaryitems.Count() > 1)
                                    {
                                        detailcatList.Units = secondaryitems[0] + " Room(s)" + ", " + secondaryitems[1] + " Kitchen(s)";
                                    }
                                    else
                                    {
                                        detailcatList.Units = itemsqty == null ? "NA" : itemsqty.ToUpper().Trim() == "" ? "NA" : itemsqty + " " + unitone;
                                    }
                                    detailcatList.ApplicationFee = 0; //Convert.ToDecimal(fixfee.ApplicationFee);
                                    if (secondaryendorsement != "")
                                    {
                                        var count = (from detacat in detailedCategoryList where detacat.Endorsement == secondaryendorsement select detacat).Count();
                                        if (count == 0 && cphycial.ExemptFromAllFees.Trim().ToUpper() != GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES).ToUpper())
                                        {
                                            detailcatList.EndorsementFee = Convert.ToDecimal(fixfee.EndorsementFee) * calYear;
                                        }
                                        else
                                        {
                                            detailcatList.EndorsementFee = 0;
                                        }
                                    }
                                    else
                                    {
                                        detailcatList.EndorsementFee = 0;
                                    }
                                    //if (cphycial.ExemptFromAllFees.Trim().ToUpper() != GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES).ToUpper())
                                    //{
                                        detailcatList.CategoryLicenseFee = secondarylicenseamount * calYear;
                                    if (primaryName.ToUpper().Trim()== "CHARITABLE EXEMPT")
                                    {
                                        detailcatList.ApplicationFee = Convert.ToDecimal(fixfee.ApplicationFee)*calYear;
                                        detailcatList.EndorsementFee = Convert.ToDecimal(fixfee.EndorsementFee)*calYear;
                                    }
                                    //}
                                    //else
                                    //{
                                    //    detailcatList.CategoryLicenseFee = 0;
                                    //}
                                   // if (cphycial.ExemptFromAllFees.Trim().ToUpper() != GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES).ToUpper())
                                  //  {
                                        detailcatList.SubTotal = detailcatList.ApplicationFee + detailcatList.EndorsementFee + detailcatList.CategoryLicenseFee;
                                        detailcatList.TechFee = (detailcatList.SubTotal / 100) * 10;
                                        detailcatList.TotalFee = detailcatList.SubTotal + detailcatList.TechFee;
                                  //  }
                                    //else
                                    //{
                                    //    detailcatList.SubTotal = 0;
                                    //    detailcatList.TechFee = 0;
                                    //    detailcatList.TotalFee = 0;
                                    //}
                                    detailedCategoryList.Add(detailcatList);
                                }
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(submissionApp.Secondary))
                {
                    var endorsementcount = endorsementslist.Distinct().Count();
                    submissionApp.Applicationfee = Convert.ToDecimal(fixfee.ApplicationFee) * calYear;
                    submissionApp.Licensefee = licensecount * calYear;
                    submissionApp.Endorsmentfee = endorsementcount * Convert.ToDecimal(fixfee.EndorsementFee) * calYear;
                    var subtotal = (submissionApp.Applicationfee + submissionApp.Licensefee + submissionApp.Endorsmentfee);
                    submissionApp.Techfee = ((subtotal / 100) * 10);
                    submissionApp.TotalFee = (subtotal + submissionApp.Techfee);
                    submissionApp.GrandTotal = (subtotal + submissionApp.Techfee);
                }
                else
                {
                    if (cphycial.ExemptFromAllFees.Trim().ToUpper() != GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES).ToUpper())
                    {
                        var endorsementcount = endorsementslist.Distinct().Count();
                        submissionApp.Applicationfee = Convert.ToDecimal(fixfee.ApplicationFee) * calYear;
                        submissionApp.Licensefee = licensecount * calYear;
                        submissionApp.Endorsmentfee = endorsementcount * Convert.ToDecimal(fixfee.EndorsementFee) * calYear;
                        var subtotal = (submissionApp.Applicationfee + submissionApp.Licensefee + submissionApp.Endorsmentfee);
                        submissionApp.Techfee = ((subtotal / 100) * 10);
                        submissionApp.TotalFee = (subtotal + submissionApp.Techfee);
                        submissionApp.GrandTotal = (subtotal + submissionApp.Techfee);
                    }
                    else
                    {
                        submissionApp.Applicationfee = 0;
                        submissionApp.Licensefee = 0;
                        submissionApp.Endorsmentfee = 0;
                        submissionApp.TotalFee = 0;
                        submissionApp.Techfee = 0;
                    }
                    
                }
                

                submissionApp.LicenseCategory = primarycateogry.FirstOrDefault().Description;
                submissionApp.Endorsement = primarycateogry.FirstOrDefault().Endorsement;
                submissionApp.DetailedCategoryList = detailedCategoryList;
                return submissionApp;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method is used to get super sub category name based on category id.
        /// </summary>
        /// <param name="submissionApp"></param>
        /// <returns>Return string value</returns>
        public string GetSuperSubCategoryName(SubmissionApplication submissionApp)
        {
            string sendCategoryName = string.Empty;
            if (submissionApp.SubSubCategory != null)
            {
                if (submissionApp.SubSubCategory != "")
                {
                    var subCategory = submissionApp.SubSubCategory.Split(',');
                    foreach (var subcatid in subCategory)
                    {
                        if (subcatid.Trim() != ",")
                        {
                            var subCat = Mastersubcategoryrepo.FindByID(subcatid).FirstOrDefault();
                            if (subCat != null)
                            {
                                sendCategoryName = subCat.SubCategoryName.Replace(System.Environment.NewLine, string.Empty);
                            }
                        }
                    }
                }
            }
            return sendCategoryName;
        }

        /// <summary>
        /// This method is used to Get the list of Submission Categories based on Application Unique Id.
        /// </summary>
        /// <param name="masterid"></param>
        /// <returns>List of Submission Categories</returns>
        public IEnumerable<SubmissionCategory> FindbyMaster(string masterid)
        {
            var submissionCategory = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterid);
            return submissionCategory;
        }

        /// <summary>
        /// This methos is used to get Categories List Details with License Amount based on user Inputs
        /// </summary>
        /// <param name="servicechecklist"></param>
        /// <returns>Retruns ServiceChecklist</returns>
        public ServiceChecklist ServiceCheckList(ServiceChecklist servicechecklist)
        {
            try
            {
                var categoryid = FindbyMaster(servicechecklist.MasterId);
                var subendoresement = string.Empty;
                var questions = Submissionquesrepo.FindByMasterID(servicechecklist.MasterId).ToList();
                var fixfee = FixfeeRepo.AllFixFees().FirstOrDefault();
                var detailcatlist = new List<DetailedCategoryList>();
                var checklist = Masterchecklistrepo.FindByMasterId(servicechecklist.MasterId).ToList();
                decimal ehopfee = 0;
                string primaryName = string.Empty;
                bool isExemption = false;
                var categoryCode = string.Empty;
                string primarycategoryid = GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.Primary).ToUpper();
                var primaryid =
                    FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").Trim() == servicechecklist.MasterId &&
                                x.CategoryType.Replace(System.Environment.NewLine, "").Trim().ToUpper() == primarycategoryid)
                        .ToList();
                string answer = string.Empty;
                var catstatus = GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.NO).ToUpper();
                if (primaryid.Count() != 0)
                {
                    var primarycategoryId = primaryid.FirstOrDefault();

                    if (primarycategoryId != null)
                    {
                        var cphycial = MastercategoryphycialRepo.FindCategoryID(primarycategoryId.CategoryTypeID).ToList();
                        if (cphycial.Count() != 0)
                        {
                            catstatus = (cphycial.FirstOrDefault().BusinessMustBeInDC ?? "NO");
                        }
                    }

                    var dcquestion = questions.Where(x => x.Question.ToUpper().Trim() ==
                        GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.LocationMustBeInDc).ToString().ToUpper());
                    var homequestion =
                               questions.Where(x => x.Question.ToUpper().Trim() ==
                                   GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.IsHomeBased).ToString().ToUpper());
                    var homeocc = questions.Where(x => x.Question.ToUpper().Trim() ==
                               GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.IsEhopAllowed).ToString().ToUpper());
                    if (dcquestion.Any())
                    {
                        if (catstatus.ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES).ToUpper())
                        {
                            questions.Remove(dcquestion.FirstOrDefault());
                        }
                        else
                        {
                            if (dcquestion.FirstOrDefault().Answer.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.NO).ToUpper())
                            {
                                if (homequestion.Any() && homeocc.Any())
                                {
                                    questions.Remove(homequestion.FirstOrDefault());
                                    questions.Remove(homeocc.FirstOrDefault());
                                }
                            }
                            else if (homequestion.Any())
                            {
                                if (homequestion.FirstOrDefault().Answer.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.NO).ToUpper())
                                {
                                    questions.Remove(homeocc.FirstOrDefault());
                                }
                            }
                        }
                    }
                    else if (homequestion.Any())
                    {
                        if (homequestion.FirstOrDefault().Answer.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.NO).ToUpper())
                        {
                            questions.Remove(homeocc.FirstOrDefault());
                        }
                    }
                }

                var wouldquestion =
                    questions.Where(xx => xx.Question.ToUpper().ToString().Trim() == GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.DurationOfLicense).ToString().ToUpper());
                var calYear = 1;
                if (questions.Any())
                {
                    var licenseduration = wouldquestion.ToList();
                    if (licenseduration.Count() != 0)
                    {
                        answer = licenseduration.FirstOrDefault().Answer.ToUpper();
                    }
                    if (answer == "TWO (2) YEAR")
                    {
                        calYear = 1;
                    }
                    else if (answer == "FOUR (4) YEAR")
                    {
                        calYear = 2;
                    }
                }
                else
                {
                    calYear = 1;
                }
                //var corpquestion = questions.Where(x => x.Question.ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.IsDcraRegisteredInCorporation).ToString().ToUpper());
                //if (corpquestion.Any())
                //{
                //    if (corpquestion.FirstOrDefault().Answer.ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES).ToUpper())
                //    {
                //        var bsquestion = questions.Where(x => x.Question.ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.BusinessStructure).ToString().ToUpper());
                //        var tnquestion = questions.Where(x => x.Question.ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.TradeName).ToString().ToUpper());
                //        if (bsquestion.Any())
                //        {
                //            questions.Remove(bsquestion.FirstOrDefault());
                //        }
                //        if (tnquestion.Any())
                //        {
                //            questions.Remove(tnquestion.FirstOrDefault());
                //        }
                //    }
                //}
                var servicequestion = questions.Select(serviceques => new ScreeningQuestion
                {
                    Question = serviceques.Question,
                    Answer = serviceques.Answer,
                    Type = serviceques.OptionType
                }).ToList();
                var primaryDescription = string.Empty;
                foreach (var servicecategory in categoryid)
                {
                    if (servicecategory.CategoryType.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.Secondary).ToUpper())
                    {
                        servicecategory.CategoryType = GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.SecondaryCategory).ToUpper();
                    }
                    if (servicecategory.CategoryType.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.Primary).ToUpper())
                    {
                        var primarycateogry = Primaryrepo.FindByCategoryID(servicecategory.CategoryTypeID).FirstOrDefault();
                        var cphycial = MastercategoryphycialRepo.FindCategoryID(servicecategory.CategoryTypeID).FirstOrDefault();
                        var primary = primarycateogry.Endorsement.ToUpper().Trim() == "NA" ? "" : (primarycateogry.Endorsement ?? "").ToUpper().Trim().Replace("N", "").Replace("N/A", "");
                        categoryCode = (primarycateogry.CategoryCode ?? "").Replace(System.Environment.NewLine, "").Trim();
                        subendoresement = primary;
                        primaryDescription = primarycateogry.Description == null ? "" : primarycateogry.Description.Trim();
                        string subcategoryname = string.Empty;
                        if (Convert.ToBoolean(primarycateogry.IsSubCategory))
                        {
                            var subcategory = categoryid.Where(x => x.CategoryType.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.SubCategory)).ToList();
                            if (subcategory.Count() != 0)
                            {
                                subcategoryname = GetSubCategory(subcategory.FirstOrDefault().CategoryTypeID);
                            }
                        }

                        var unitone = primarycateogry.UnitOne == null
                                        ? "" : primarycateogry.UnitOne.ToUpper().Trim() == "NA" ? ""
                                        : primarycateogry.UnitOne.Replace(System.Environment.NewLine, string.Empty);
                        var unitTwo = primarycateogry.UnitTwo == null
                                       ? "" : primarycateogry.UnitTwo.ToUpper().Trim() == "NA" ? ""
                                       : primarycateogry.UnitTwo.Replace(System.Environment.NewLine, string.Empty);

                        var categorylist = new DetailedCategoryList
                        {
                            Endorsement = primarycateogry.Endorsement == null ? "" : primarycateogry.Endorsement.Trim(),
                            CategoryId = servicecategory.CategoryTypeID,
                            LicenseCategory = primarycateogry.Description == null ? "" : primarycateogry.Description.Trim(),
                            CategoryCode = categoryCode,
                            LicenseDuration = answer,
                            SubCategoryName = subcategoryname,
                            Units = "1 " + unitone,
                            IsBackgroundInvestigation = Convert.ToBoolean(primarycateogry.IsBackgroundInvestigation)
                        };
                        primaryName =
                            primarycateogry.Description.Replace(System.Environment.NewLine, "").ToUpper().Trim();
                        if (primarycateogry.Description.Replace(System.Environment.NewLine, "").ToUpper().Trim() ==
                          GenericEnums.GetEnumDescription(GenericEnums.Category.APARTMENT).ToUpper() ||
                           primarycateogry.Description.Replace(System.Environment.NewLine, "").ToUpper().Trim() ==
                           GenericEnums.GetEnumDescription(GenericEnums.Category.FamilyRental).ToUpper())
                        {
                            categorylist.IsRaoFeeApplied = true;
                        }
                        else
                        {
                            categorylist.IsRaoFeeApplied = false;
                        }
                        var secondary = servicecategory.ItemQty.Split(',');
                        if (secondary.Count() > 1)
                        {
                            switch (unitone.Trim())
                            {
                                case "Rooms":
                                    unitone = " Room(s) ";
                                    break;

                                case "Kitchens":
                                    unitone = " Kitchen(s) ";
                                    break;
                            }
                            switch (unitTwo.Trim())
                            {
                                case "Rooms":
                                    unitTwo = " Room(s) ";
                                    break;

                                case "Kitchens":
                                    unitTwo = " Kitchen(s) ";
                                    break;
                            }

                            categorylist.Units = secondary[0] + " " + unitone + ", " + secondary[1] + " " + unitTwo;
                        }
                        else
                        {
                            categorylist.Units = servicecategory.ItemQty == null ? "NA" : servicecategory.ItemQty.ToUpper().Trim() == "" ? "NA"
                                    : servicecategory.ItemQty + " " + unitone;
                        }
                        if (cphycial.ExemptFromAllFees.Trim().ToUpper() != GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES).ToUpper())
                        {
                            categorylist.ApplicationFee = Convert.ToDecimal(fixfee.ApplicationFee) * calYear;
                        }
                        else
                        {
                            isExemption = true;
                            //if (categoryid.ToList().Count() == 1)
                            //{
                                categorylist.ApplicationFee = 0;
                            //}
                            //else
                            //{
                            //    categorylist.ApplicationFee = Convert.ToDecimal(fixfee.ApplicationFee) * calYear;
                            //}
                            var checkEhop = Masterchecklistrepo.FindByMasterId(servicechecklist.MasterId).ToList();
                            if (checkEhop.Count() != 0)
                            {
                                bool ehopeligible = Convert.ToBoolean(checkEhop.FirstOrDefault().IsSubmissioneHop);
                                var fee = FixfeeRepo.AllFixFees().ToList();
                                if (fee.Count() != 0 && ehopeligible)
                                {
                                    ehopfee = fee.FirstOrDefault().eHOPFee.Value;
                                    ehopfee = ehopfee + (ehopfee / 100) * 10;
                                }
                            }
                        }
                        categorylist.CategoryLicenseFee = Convert.ToDecimal(servicecategory.LicenseCategoryFee);
                        categorylist.EndorsementFee = Convert.ToDecimal(servicecategory.EndorsementFee);
                        if (primarycateogry.Description.Replace(System.Environment.NewLine, "").ToUpper().Trim() ==
                           GenericEnums.GetEnumDescription(GenericEnums.Category.APARTMENT).ToUpper() ||
                            primarycateogry.Description.Replace(System.Environment.NewLine, "").ToUpper().Trim() ==
                            GenericEnums.GetEnumDescription(GenericEnums.Category.FamilyRental).ToUpper())
                        {
                            categorylist.IsRaoFeeApplied = true;
                        }
                        else
                        {
                            categorylist.IsRaoFeeApplied = false;
                        }
                        categorylist.SubTotal = categorylist.ApplicationFee + categorylist.EndorsementFee + categorylist.CategoryLicenseFee;
                        categorylist.TechFee = (categorylist.SubTotal / 100) * 10;
                        categorylist.TotalFee = categorylist.SubTotal + categorylist.TechFee;
                        detailcatlist.Add(categorylist);
                    }
                    else if (servicecategory.CategoryType.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.SecondaryCategory).ToUpper())
                    {
                        var secondarycateogry =
                            Secondaryrepo.FindBySecondaryID(servicecategory.CategoryTypeID).FirstOrDefault();

                        var categorylist = new DetailedCategoryList();
                        categorylist.Endorsement = servicecategory.Endorsement;
                        categorylist.CategoryId = servicecategory.CategoryTypeID;
                        categorylist.LicenseCategory = secondarycateogry.SecondaryLicenseCategory == null ? ""
                            : secondarycateogry.SecondaryLicenseCategory.Trim();
                        var secondaryprimaryCategory = Primaryrepo.SecondaryEndorsement(secondarycateogry.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").Trim()).ToList();
                        if (secondaryprimaryCategory.Count() != 0)
                        {
                            string subcategoryname = string.Empty;
                            if (Convert.ToBoolean(secondaryprimaryCategory.FirstOrDefault().IsSubCategory))
                            {
                                var subcategory =
                                    categoryid.Where(
                                        x =>
                                            x.CategoryType.ToUpper() ==
                                            GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.SubCategory)).ToList();
                                if (subcategory.Count() != 0)
                                {
                                    subcategoryname = GetSubCategory(subcategory.FirstOrDefault().CategoryTypeID);
                                }
                            }

                            var secondaryprimary = secondaryprimaryCategory.FirstOrDefault();
                            categorylist.IsBackgroundInvestigation =
                                Convert.ToBoolean(secondaryprimary.IsBackgroundInvestigation);
                            categorylist.Endorsement = secondaryprimary.Endorsement.Trim() == null ? "" : secondaryprimary.Endorsement.ToUpper().Trim() == "NA" ? "" : (secondaryprimary.Endorsement ?? "").Trim().Replace(System.Environment.NewLine, string.Empty);
                            var unitone = secondaryprimary.UnitOne == null ? "" : secondaryprimary.UnitOne.ToUpper().Trim() == "NA" ? "" : (secondaryprimary.UnitOne ?? "").Replace(System.Environment.NewLine, string.Empty);
                            var unitTwo = secondaryprimary.UnitTwo == null ? "" : secondaryprimary.UnitTwo.ToUpper().Trim() == "NA" ? "" : (secondaryprimary.UnitTwo ?? "").Replace(System.Environment.NewLine, string.Empty);
                            //    var unitone = secondaryprimary.UnitOne.ToUpper().Trim() == "NA" ? "" : (secondaryprimary.UnitOne ?? "").Trim().Replace(System.Environment.NewLine, string.Empty);
                            //   var unitTwo = secondaryprimary.UnitTwo.ToUpper().Trim() == "NA" ? "" : (secondaryprimary.UnitTwo ?? "").Trim().Replace(System.Environment.NewLine, string.Empty);

                            categorylist.Units = "1 " + unitone;
                            var secondary = servicecategory.ItemQty.Split(',');
                            if (secondary.Count() > 1)
                            {
                                categorylist.Units = secondary[0] + " " + unitone + ", " + secondary[1] + " " + unitTwo;
                            }
                            else
                            {
                                categorylist.Units = servicecategory.ItemQty == null ? "NA" : servicecategory.ItemQty.ToUpper().Trim() == "" ? "NA"
                                        : servicecategory.ItemQty + " " + unitone;
                            }

                            categorylist.SubCategoryName = subcategoryname;
                            categorylist.ApplicationFee = 0;
                            categorylist.LicenseDuration = answer;
                            categorylist.CategoryCode = secondaryprimary.CategoryCode;
                            if (secondarycateogry.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").ToUpper() ==
                               GenericEnums.GetEnumDescription(GenericEnums.Category.GeneralBusiness).ToUpper())
                            {
                                subendoresement = secondaryprimary.Endorsement;
                                categoryCode = secondaryprimary.CategoryCode;
                            }
                        }

                        categorylist.CategoryLicenseFee = Convert.ToDecimal(servicecategory.LicenseCategoryFee ?? 0);
                        categorylist.EndorsementFee =
                            Convert.ToDecimal(servicecategory.EndorsementFee ?? 0);
                        if (primaryName.ToUpper().Trim() == "CHARITABLE EXEMPT")
                        {
                            categorylist.ApplicationFee = Convert.ToDecimal(fixfee.ApplicationFee) * calYear;
                            categorylist.EndorsementFee = Convert.ToDecimal(fixfee.EndorsementFee) * calYear;
                        }
                        categorylist.SubTotal = categorylist.ApplicationFee + categorylist.EndorsementFee +
                                                categorylist.CategoryLicenseFee;
                        categorylist.TechFee = (categorylist.SubTotal / 100) * 10;
                        categorylist.TotalFee = categorylist.SubTotal + categorylist.TechFee;
                        if (secondarycateogry.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").ToUpper().Trim() ==
                          GenericEnums.GetEnumDescription(GenericEnums.Category.APARTMENT).ToUpper() ||
                           secondarycateogry.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").ToUpper().Trim() ==
                           GenericEnums.GetEnumDescription(GenericEnums.Category.FamilyRental).ToUpper())
                        {
                            categorylist.IsRaoFeeApplied = true;
                        }
                        else
                        {
                            categorylist.IsRaoFeeApplied = false;
                        }
                        detailcatlist.Add(categorylist);
                    }
                }
                foreach (var totalvalues in detailcatlist)
                {
                    servicechecklist.ApplicationFee = servicechecklist.ApplicationFee + totalvalues.ApplicationFee;
                    servicechecklist.CategoryLicenseFee = servicechecklist.CategoryLicenseFee +
                                                          totalvalues.CategoryLicenseFee;
                    servicechecklist.EndorsementFee = servicechecklist.EndorsementFee + totalvalues.EndorsementFee;
                    servicechecklist.SubTotal = servicechecklist.ApplicationFee + servicechecklist.EndorsementFee +
                                                servicechecklist.CategoryLicenseFee;
                    servicechecklist.TechFee = (servicechecklist.SubTotal / 100) * 10;
                    servicechecklist.TotalFee = servicechecklist.TotalFee + totalvalues.TotalFee;
                }
                var renewchecklist = SubmissionMasterRenewalRepo.FindByID(servicechecklist.MasterId).ToList();
                if (renewchecklist.Count != 0)
                {
                    servicechecklist.Extradays = renewchecklist.FirstOrDefault().Extradays;
                    servicechecklist.TotalFee = servicechecklist.TotalFee +
                                                Convert.ToDecimal((renewchecklist.FirstOrDefault().LapsedFee ?? 0) + (renewchecklist.FirstOrDefault().ExpiredFee ?? 0));
                }
                else
                {
                    servicechecklist.Extradays = "ACTIVE";
                    servicechecklist.TotalFee = servicechecklist.TotalFee;
                }
                if (checklist.Count() != 0)
                {
                    if (checklist.FirstOrDefault().IsSubmissioneHop.Value)
                    {
                        if (!isExemption)
                        {
                            var grandTotal = Convert.ToDecimal(servicechecklist.TotalFee);
                            servicechecklist.TotalFee = grandTotal + ((fixfee.eHOPFee.Value / 100) * 10) + fixfee.eHOPFee.Value;
                        }
                        else
                        {
                            servicechecklist.TotalFee = servicechecklist.TotalFee + ehopfee;
                        }
                        servicechecklist.Isehop = true;
                    }
                    else
                    {
                        servicechecklist.TotalFee = servicechecklist.TotalFee;
                        servicechecklist.Isehop = false;
                    }
                }
                else
                {
                    servicechecklist.TotalFee = servicechecklist.TotalFee;
                    servicechecklist.Isehop = false;
                }
                servicechecklist.DetailedCategoryList = detailcatlist;
                servicechecklist.SubQuestion = servicequestion;
                return servicechecklist;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method is used to get sub category based on category id
        /// </summary>
        /// <param name="categoryTypeId"></param>
        /// <returns></returns>
        public string GetSubCategory(string categoryTypeId)
        {
            var supersubcateogory = Mastersubcategoryrepo.FindByID(categoryTypeId).FirstOrDefault();
            if (supersubcateogory != null) return (supersubcateogory.SubCategoryName ?? "").Trim();
            return "";
        }

        /// <summary>
        /// This methos is used to Insert Categories from Renewal
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns>Retrun Amount in Decimal</returns>
        public decimal InsertRenewalData(RenewModel renewModel)
        {
            decimal result = 0;
            string categorylist = "1";
            try
            {
                var renwaldata =
                    FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").Trim() == renewModel.MasterId
                                && x.CategoryTypeID == renewModel.CategoryId).ToList();
                var category =
                 Renewinvoicerepo.FindAmountByLicense(renewModel.LrenNumber).ToList();
                if (renwaldata.Count() == 0)
                {
                    string Type = string.Empty;
                    string[] word = renewModel.CategoryName.Split('|');
                    int count = 1;
                    foreach (var categorydata in word)
                    {
                        if (categorydata != "")
                        {
                            var lookupCategoryName = _lookupExistingCategoriesRepository.FindBy(categorydata).ToList();
                            if (lookupCategoryName.Count() != 0)
                            {
                                renewModel.CategoryName = lookupCategoryName.FirstOrDefault().NewCategoryName;
                                if (count == 1)
                                {
                                    renewModel.CategoryId = string.Empty;
                                    var primarycategory = Primaryrepo.SecondaryEndorsement(renewModel.CategoryName).ToList();
                                    if (primarycategory.Count() != 0)
                                    {
                                        renewModel.CategoryId = primarycategory.FirstOrDefault().PrimaryID;
                                        Type = GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.Primary).ToUpper();
                                    }
                                }
                                else
                                {
                                    var secondarycategory = Secondaryrepo.FindBySecondaryName(renewModel.CategoryName).ToList();
                                    renewModel.CategoryId = string.Empty;
                                    if (secondarycategory.Count() != 0)
                                    {
                                        renewModel.CategoryId = secondarycategory.FirstOrDefault().SecondaryID;
                                        Type = GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.SecondaryCategory).ToUpper();
                                    }
                                }
                                count = count + 1;
                            }
                            if (renewModel.CategoryId != string.Empty)
                            {
                                try
                                {
                                    var subCategory = new SubmissionCategory
                                    {
                                        MasterId = renewModel.MasterId,
                                        CategoryTypeID = renewModel.CategoryId
                                    };
                                    var primarycateogry = Primaryrepo.FindByCategoryID(subCategory.CategoryTypeID).ToList();
                                    if (primarycateogry.Count != 0)
                                    {
                                        subCategory.Endorsement = primarycateogry.FirstOrDefault().Endorsement ?? "";
                                    }
                                    else
                                    {
                                        subCategory.Endorsement = renewModel.Endoresement;
                                    }
                                    if (renewModel.CategoryId == string.Empty)
                                    {
                                        subCategory.LicenseCategoryFee = 0;
                                    }
                                    else
                                    {
                                        subCategory.LicenseCategoryFee = 0;
                                        var license = category.Where(x => x.GF_DES.ToUpper().Trim().Contains(categorydata.Trim().ToUpper())).ToList();
                                        if (license.Count() != 0)
                                        {
                                            decimal licenseamount = 0;

                                          //  int categorycount = 0;
                                            if (license.Count() == 2)
                                            {
                                                var roomsdata =
                                                    license.Where(
                                                        x =>
                                                            x.GF_DES.ToUpper()
                                                                .Trim()
                                                                .Contains("ROOM")).ToList();
                                                if (roomsdata.Count != 0)
                                                {
                                                    licenseamount = Convert.ToDecimal(roomsdata.FirstOrDefault().GF_FEE);
                                                    categorylist =
                                                        Convert.ToInt32(roomsdata.FirstOrDefault().GF_UNIT).ToString();
                                                }
                                                var kitchendata =
                                                   license.Where(
                                                       x =>
                                                           x.GF_DES.ToUpper()
                                                               .Trim()
                                                               .Contains("KITCHEN")).ToList();
                                                if (kitchendata.Count != 0)
                                                {
                                                    licenseamount = licenseamount +
                                                                    Convert.ToDecimal(kitchendata.FirstOrDefault().GF_FEE);
                                                    categorylist = categorylist = categorylist + "," + Convert.ToInt32(kitchendata.FirstOrDefault().GF_UNIT).ToString();
                                                }
                                            }
                                            else
                                            {
                                                licenseamount = licenseamount + Convert.ToDecimal(license.FirstOrDefault().GF_FEE);
                                                categorylist = Convert.ToInt32(license.FirstOrDefault().GF_UNIT).ToString();
                                            }
                                            //foreach (var categoryamount in license)
                                            //{
                                            //    licenseamount = licenseamount + Convert.ToDecimal(categoryamount.GF_FEE);
                                            //    if (categorycount == 0)
                                            //    {
                                            //        categorylist =Convert.ToInt32(categoryamount.GF_UNIT).ToString();
                                            //    }
                                            //    else
                                            //    {
                                            //        categorylist = categorylist + "," + Convert.ToInt32(categoryamount.GF_UNIT).ToString();
                                            //    }
                                            //    categorycount = categorycount + 1;
                                            //}
                                            subCategory.LicenseCategoryFee =
                                                licenseamount;
                                        }
                                        else
                                        {
                                            var lookup = _lookupExistingCategoriesRepository.NewCategoryFindBy(renewModel.CategoryName.Trim()).ToList();
                                            foreach (var lookupcategory in lookup)
                                            {
                                                var oldcategoryname = category.Where(x => x.GF_DES.ToUpper().Trim().Contains(lookupcategory.ExistingCategory.Trim().ToUpper())).ToList();
                                                if (oldcategoryname.Any())
                                                {
                                                    subCategory.LicenseCategoryFee = Convert.ToDecimal(oldcategoryname.FirstOrDefault().GF_FEE); ;
                                                }
                                            }
                                        }
                                    }
                                    if (Type == GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.Primary).ToUpper())
                                    {
                                        var endoresement = category.Where(x => x.GF_DES.ToUpper().Contains("ENDORSEMENT")).ToList();
                                        subCategory.EndorsementFee = endoresement.Count() != 0 ? Convert.ToDecimal(endoresement.FirstOrDefault().GF_FEE) : 0;
                                    }
                                    else
                                    { subCategory.EndorsementFee = 0; }
                                    subCategory.CategoryType = Type;
                                    subCategory.ItemQty = categorylist;
                                    Add(subCategory);
                                    Save();
                                }
                                catch (Exception)
                                {
                                    result = 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }

        /// <summary>
        /// This method is used to Get specific List of Categories basedon Application Unique Id and Endorsment.
        /// </summary>
        /// <param name="masterid"></param>
        /// <param name="endorsment"></param>
        /// <returns>Retrun List of Submission Category</returns>
        public IEnumerable<SubmissionCategory> FindByMasterId(string masterid, string endorsment)
        {
            var submissionCategory = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterid && x.Endorsement.Replace(System.Environment.NewLine, "") == endorsment);
            return submissionCategory;
        }

        /// <summary>
        /// This method is used to get the Particular License fee based on Quantity, Category Name, Unit Type and Item Quantity.
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="categoryname"></param>
        /// <param name="unittype"></param>
        /// <param name="itemquantity"></param>
        /// <returns>Retrun Amount in Decimal</returns>
        public decimal CalculationAmount(int quantity, string categoryname, string unittype, string itemquantity)
        {
            decimal totallicensefee = 0;
            try
            {
                var secondary = itemquantity.Split(',');
                List<OSub_Category_Fees> unitonefee;
                var licensefee = new List<OSub_Category_Fees>();
                if (unittype != "")
                {
                    var licensetype = Feecoderepo.FindBycategoryID(unittype, categoryname.Replace(System.Environment.NewLine, ""), quantity).ToList();
                    licensefee = (from categorytype in licensetype
                                  join category in Primaryrepo.SecondaryEndorsement(categoryname.Replace(System.Environment.NewLine, ""))
                                      on categorytype.OSub_Description.Replace(System.Environment.NewLine, "").ToUpper().Trim() equals category.Description.Replace(System.Environment.NewLine, "").ToUpper().Trim()
                                  where categorytype.OSub_Description.Replace(System.Environment.NewLine, "").ToUpper().Trim() == categoryname.Replace(System.Environment.NewLine, "").ToUpper().Trim()
                                  select categorytype).ToList();
                    var tirestatus = licensefee.FirstOrDefault().Tier == null ? false : licensefee.FirstOrDefault().Tier == licensefee.FirstOrDefault().Tier ? true : true;
                    string feecode = "S";
                    string licensfeecode = licensefee.FirstOrDefault().Fee_Code == null ? "" : licensefee.FirstOrDefault().Fee_Code.ToString().Trim();
                    var code = Feecoderepo.FindByID(unittype.Replace(System.Environment.NewLine, string.Empty)).Where(x => x.FeeCode.Trim() == licensfeecode).ToList();

                    if (code.Count() != 0)
                    {
                        if (licensefee.FirstOrDefault().Fee_Code == "TA")
                        {
                            feecode = code.LastOrDefault().FeeCode;
                        }
                        else if (licensefee.FirstOrDefault().Fee_Code == "HT")
                        {
                            feecode = code.LastOrDefault().FeeCode;
                        }
                        else { feecode = code.FirstOrDefault().FeeCode; }
                    }
                    unitonefee = SubcatFeerepo.FindByFeeCode(categoryname.Replace(System.Environment.NewLine, ""), feecode, Convert.ToInt32(quantity)).ToList();
                }
                else
                {
                    licensefee = SubcatFeerepo.FindByCateogry(categoryname.Replace(System.Environment.NewLine, string.Empty), Convert.ToInt32(quantity)).ToList();
                    unitonefee = SubcatFeerepo.FindByCateogry(categoryname.Replace(System.Environment.NewLine, string.Empty), Convert.ToInt32(quantity)).ToList();
                }

                var tire = Convert.ToInt32(unitonefee.FirstOrDefault().Tier == null ? 0 : unitonefee.FirstOrDefault().Tier);
                if (tire != 0)
                {
                    var start = 0;
                    if (Convert.ToInt32(unitonefee.FirstOrDefault().Start) > 0)
                    {
                        start = Convert.ToInt32(unitonefee.FirstOrDefault().Start) - 1;
                    }
                    var remaining = quantity - Convert.ToInt32(start);
                    var modelo = Math.Ceiling(remaining / (double)tire);
                    if (tire == 1)
                    {
                        if (categoryname.Replace(System.Environment.NewLine, "").ToUpper().Trim() ==
                          GenericEnums.GetEnumDescription(GenericEnums.Category.APARTMENT).ToUpper() ||
                            categoryname.Replace(System.Environment.NewLine, "").ToUpper().Trim() ==
                             GenericEnums.GetEnumDescription(GenericEnums.Category.FamilyRental).ToUpper())
                        {
                            if (secondary[0] == "")
                            {
                                secondary[0] = "1";
                            }
                            var fixfee = FixfeeRepo.AllFixFees().FirstOrDefault();
                            var apartmentamount = Convert.ToDecimal(fixfee.RAOFee) * Convert.ToInt32(secondary[0]);
                            totallicensefee = (Convert.ToDecimal(unitonefee.FirstOrDefault().License_Fee) * quantity) + apartmentamount;
                        }
                        else
                        {
                            if (unittype.ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.Category.KITCHENS).ToUpper())
                            {
                                if (start == quantity)
                                { modelo = 0; }
                                else
                                {
                                    modelo = modelo - 1;
                                    quantity = quantity - 1;
                                }
                            }
                            if (unitonefee.FirstOrDefault().Fee_Code.ToUpper() == "TA")
                            {
                                var categoryfee = Feecoderepo.FindBycategoryName(unittype, categoryname.Replace(System.Environment.NewLine, "")).OrderBy(x => x.Start).ToList();
                                int index = categoryfee.FindIndex(c => c.Fee_Code == "TA");
                                if (index != 0)
                                {
                                    var myResult = categoryfee[index - 1];
                                    totallicensefee = Convert.ToDecimal(myResult.License_Fee);
                                }
                                totallicensefee = totallicensefee + (Convert.ToDecimal(licensefee.FirstOrDefault().License_Fee) * Convert.ToInt32(modelo));
                            }
                            else { totallicensefee = (Convert.ToDecimal(unitonefee.FirstOrDefault().License_Fee) * quantity); }
                        }
                    }
                    else
                    {
                        if (categoryname.Replace(System.Environment.NewLine, "").ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.Category.APARTMENT).ToUpper() ||
                           categoryname.Replace(System.Environment.NewLine, "").ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.Category.FamilyRental).ToUpper())
                        {
                            if (secondary[0] == "")
                            {
                                secondary[0] = "1";
                            }
                            var fixfee = FixfeeRepo.AllFixFees().FirstOrDefault();
                            var apartmentamount = Convert.ToDecimal(fixfee.RAOFee) * Convert.ToInt32(secondary[0]);
                            if (unitonefee.FirstOrDefault().Fee_Code.ToUpper() == "C")
                            {
                                totallicensefee = (Convert.ToDecimal(licensefee.FirstOrDefault().License_Fee) * Convert.ToInt32(modelo)) + apartmentamount;
                            }
                            else
                                if (unitonefee.FirstOrDefault().Fee_Code.ToUpper() == "TA")
                                {
                                    var categoryfee = Feecoderepo.FindBycategoryName(unittype, categoryname.Replace(System.Environment.NewLine, "")).OrderBy(x => x.Start).ToList();
                                    int index = categoryfee.FindIndex(c => c.Fee_Code == "TA");
                                    if (index != 0)
                                    {
                                        var myResult = categoryfee[index - 1];
                                        totallicensefee = Convert.ToDecimal(myResult.License_Fee);
                                    }
                                    totallicensefee = totallicensefee + (Convert.ToDecimal(licensefee.FirstOrDefault().License_Fee) * Convert.ToInt32(modelo)) + apartmentamount;
                                }
                                else
                                {
                                    totallicensefee = Convert.ToDecimal(unitonefee.FirstOrDefault().License_Fee) +
                                                      (Convert.ToDecimal(licensefee.FirstOrDefault().License_Fee) * Convert.ToInt32(modelo)) + apartmentamount;
                                }
                        }
                        else
                        {
                            if (unitonefee.FirstOrDefault().Fee_Code.ToUpper() == "C")
                            {
                                totallicensefee = (Convert.ToDecimal(licensefee.FirstOrDefault().License_Fee) * Convert.ToInt32(modelo));
                            }
                            else
                                if (unitonefee.FirstOrDefault().Fee_Code.ToUpper() == "TA")
                                {
                                    var categoryfee = Feecoderepo.FindBycategoryName(unittype, categoryname.Replace(System.Environment.NewLine, "")).OrderBy(x => x.Start).ToList();
                                    int index = categoryfee.FindIndex(c => c.Fee_Code == "TA");
                                    if (index != 0)
                                    {
                                        var myResult = categoryfee[index - 1];
                                        totallicensefee = Convert.ToDecimal(myResult.License_Fee);
                                    }
                                    totallicensefee = totallicensefee + (Convert.ToDecimal(licensefee.FirstOrDefault().License_Fee) * Convert.ToInt32(modelo));
                                }
                                else
                                {
                                    totallicensefee = Convert.ToDecimal(unitonefee.FirstOrDefault().License_Fee) + (Convert.ToDecimal(licensefee.FirstOrDefault().License_Fee) * Convert.ToInt32(modelo));
                                }
                        }
                    }
                }
                else
                {
                    if (categoryname.Replace(System.Environment.NewLine, "").ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.Category.APARTMENT).ToUpper() ||
                      categoryname.Replace(System.Environment.NewLine, "").ToUpper().Trim() ==
                        GenericEnums.GetEnumDescription(GenericEnums.Category.FamilyRental).ToUpper())
                    {
                        if (secondary[0] == "")
                        {
                            secondary[0] = "1";
                        }
                        var fixfee = FixfeeRepo.AllFixFees().FirstOrDefault();
                        var apartmentamount = Convert.ToDecimal(fixfee.RAOFee) * Convert.ToInt32(secondary[0]);
                        totallicensefee = Convert.ToDecimal(unitonefee.FirstOrDefault().License_Fee) + apartmentamount;
                    }
                    else
                    {
                        totallicensefee = Convert.ToDecimal(unitonefee.FirstOrDefault().License_Fee);
                    }
                }
            }
            catch (Exception)
            {
                totallicensefee = 0;
            }
            return totallicensefee;
        }

        /// <summary>
        /// This methos is used to get Primary Category License Amount based on Primary Id and Item Quantity.
        /// </summary>
        /// <param name="primarycategoryId"></param>
        /// <param name="itemquantity"></param>
        /// <returns>Retrun Amount in Decimal</returns>
        public decimal LicenseAmount(string primarycategoryId, string itemquantity)
        {
            decimal totallicensefee = 0;
            try
            {
                var secondary = itemquantity.Split(',');
                int quantity = 0;

                var primarycateogry = Primaryrepo.FindByCategoryID(primarycategoryId).FirstOrDefault();
                var Unitone = primarycateogry.UnitOne == null ? "" : primarycateogry.UnitOne.ToUpper().Trim() == "NA" ? "" : (primarycateogry.UnitOne ?? "").Replace(System.Environment.NewLine, string.Empty).ToUpper().Trim();
                var Unittwo = primarycateogry.UnitTwo == null ? "" : primarycateogry.UnitTwo.ToUpper().Trim() == "NA" ? "" : (primarycateogry.UnitTwo ?? "").Replace(System.Environment.NewLine, string.Empty).ToUpper().Trim();
                if (Unitone != "" && Unittwo != "")
                {
                    if (Unitone != "")
                    {
                        quantity = Convert.ToInt32(secondary[0]);
                        totallicensefee = CalculationAmount(quantity, primarycateogry.Description, Unitone, itemquantity);
                    }
                    if (Unittwo != "")
                    {
                        quantity = Convert.ToInt32(secondary[1]);
                        totallicensefee = totallicensefee + CalculationAmount(quantity, primarycateogry.Description, Unittwo, itemquantity);
                    }
                }
                else
                    if (Unitone != "")
                    {
                        quantity = Convert.ToInt32(secondary[0]);
                        totallicensefee = CalculationAmount(quantity, primarycateogry.Description, Unitone, itemquantity);
                    }
                    else

                        if (Unittwo != "")
                        {
                            quantity = Convert.ToInt32(secondary[1]);
                            totallicensefee = totallicensefee + CalculationAmount(quantity, primarycateogry.Description, Unittwo, itemquantity);
                        }
                        else
                        {
                            if (secondary[0] == "")
                            {
                                secondary[0] = "1";
                            }
                            quantity = Convert.ToInt32(secondary[0]);
                            totallicensefee = CalculationAmount(quantity, primarycateogry.Description, "", itemquantity);
                        }
            }
            catch (Exception)
            {
                totallicensefee = 0;
            }
            return totallicensefee;
        }

        /// <summary>
        /// This method is used to Get Secondary Category License Amount based on Secondary Id and item Qunatity.
        /// </summary>
        /// <param name="secondaryId"></param>
        /// <param name="itemquantity"></param>
        /// <returns>Retrun Amount in Decimal</returns>
        public decimal SecondLicenseAmount(string secondaryId, string itemquantity)
        {
            decimal totallicensefee = 0;
            string Unitone = string.Empty;
            string Unittwo = string.Empty;
            try
            {
                var secondary = itemquantity.Split(',');
                var quantity = 0;
                var secondarycateogry = Secondaryrepo.FindBySecondaryID(secondaryId).FirstOrDefault();
                string secondaryName = secondarycateogry.SecondaryLicenseCategory.ToUpper().Trim() == "NA" ? "" :
                    (secondarycateogry.SecondaryLicenseCategory ?? "").Replace(System.Environment.NewLine, string.Empty).ToUpper().Trim();
                var primary = Primaryrepo.SecondaryEndorsement(secondaryName).ToList();
                if (primary.Count != 0)
                {
                    var primarycategory = primary.FirstOrDefault();

                    Unitone = primarycategory.UnitOne == null
                                     ? "" : primarycategory.UnitOne.ToUpper().Trim() == "NA" ? ""
                                     : primarycategory.UnitOne.Replace(System.Environment.NewLine, string.Empty).ToUpper().Trim();

                    Unittwo = primarycategory.UnitTwo == null
                                  ? "" : primarycategory.UnitTwo.ToUpper().Trim() == "NA" ? ""
                                  : primarycategory.UnitTwo.Replace(System.Environment.NewLine, string.Empty).ToUpper().Trim();
                }
                if (Unitone != "" && Unittwo != "")
                {
                    if (Unitone != "")
                    {
                        quantity = Convert.ToInt32(secondary[0]);
                        totallicensefee = CalculationAmount(quantity, secondarycateogry.SecondaryLicenseCategory, Unitone, itemquantity);
                    }
                    if (Unittwo != "")
                    {
                        quantity = Convert.ToInt32(secondary[1]);
                        totallicensefee = totallicensefee + CalculationAmount(quantity, secondarycateogry.SecondaryLicenseCategory, Unitone, itemquantity);
                    }
                }
                else
                    if (Unitone != "")
                    {
                        quantity = Convert.ToInt32(secondary[0]);
                        totallicensefee = CalculationAmount(quantity, secondarycateogry.SecondaryLicenseCategory, Unitone, itemquantity);
                    }
                    else if (Unittwo != "")
                    {
                        quantity = Convert.ToInt32(secondary[1]);
                        totallicensefee = totallicensefee + CalculationAmount(quantity, secondarycateogry.SecondaryLicenseCategory, Unittwo, itemquantity);
                    }
                    else
                    {
                        if (secondary[0] == "")
                        {
                            secondary[0] = "1";
                        }
                        quantity = Convert.ToInt32(secondary[0]);
                        totallicensefee = CalculationAmount(quantity, secondarycateogry.SecondaryLicenseCategory, "", itemquantity);
                    }
            }
            catch (Exception)
            {
                totallicensefee = 0;
            }
            return totallicensefee;
        }

        /// <summary>
        /// This method is used to Units name Exists of not.
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns>Retrun Bool Result</returns>
        public bool Checkunits(string quantity)
        {
            var result = Feecoderepo.Checkunits(quantity);
            return result;
        }
        /// <summary>
        /// This method is used to delete submission category based on unique id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return bool value</returns>
        public bool DeleteSubmissionCategory(string masterId)
        {
            bool status = false;
            List<DeleteRecord> drecord = new List<DeleteRecord>();
            try
            {
                var contact = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).ToList();
                if (contact.Count != 0)
                {
                    foreach (var concatid in contact)
                    {
                        DeleteRecord record = new DeleteRecord();
                        record.DeleteId = concatid.SubmissionCategoryID;
                        drecord.Add(record);
                    }
                    foreach (var delete in drecord)
                    {
                        var deleterecord = FindBy(x => x.SubmissionCategoryID == delete.DeleteId).ToList();
                        if (deleterecord.Count != 0)
                        {
                            Delete(deleterecord.Single());
                            Save();
                        }
                    }
                }
                status = true;
            }
            catch (Exception )
            { status = false; }

            return status;
        }
        /// <summary>
        /// This method is used to get count of the units based on unique id
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return numeric value</returns>
        public int Unitcount(string masterId)
        {
            int unit = 0;
            var categorylist = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").Trim() == masterId.Trim() && x.CategoryType.ToUpper().Trim() ==
                "PRIMARY").ToList();
            if (categorylist.Count != 0)
            {
                foreach (var categoryUnits in categorylist)
                {
                    int count = 0;
                    var unitCategory = categoryUnits.ItemQty.Split(',');
                    foreach (var itemunit in unitCategory)
                    {
                        string units = itemunit.Trim() == "" ? "NA" : itemunit;

                        if (units.Trim() != "," && units.Trim() != "NA")
                        {
                            if (count == 0)
                            {
                                unit = Convert.ToInt32(units);
                                //  unit = unit + Convert.ToInt32(units);
                            }
                            count = count + 1;
                        }
                    }
                }
            }
            return unit;
        }
        /// <summary>
        /// This method is used to get category code based on category name.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns>Return string value</returns>
        public string CategoryCode(string categoryName)
        {
            var existingcategory = _lookupExistingCategoriesRepository.FindBy(categoryName).ToList();
            if (existingcategory.Count != 0)
            {
                var primarydata = Primaryrepo.SecondaryEndorsement(existingcategory.FirstOrDefault().NewCategoryName.ToString().Trim());
                return primarydata.FirstOrDefault().CategoryCode.ToString().Trim();
            }
            return string.Empty;
        }
        /// <summary>
        /// This method is used to check pdf option exist or not based on category name
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns>Return bool value</returns>
        public bool CheckPrimaryCategoryShowPdf(string categoryName)
        {
            var existingcategory = _lookupExistingCategoriesRepository.FindBy(categoryName).ToList();
            if (existingcategory.Count != 0)
            {
                var primarydata =
                    Primaryrepo.SecondaryEndorsement(existingcategory.FirstOrDefault().NewCategoryName.ToString().Trim());
                if (primarydata.Any())
                {
                    return Convert.ToBoolean(primarydata.FirstOrDefault().IsPDFShow);
                }
            }
            return false;
        }
    }
}