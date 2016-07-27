using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessCenter.Data.Implementation
{
    public class MasterPrimaryCategoryRepository : GenericRepository<MasterPrimaryCategory>, IMasterPrimaryCategoryRepository
    {
        protected IMasterSecondaryLicenseCategoryRepository Secondaryrepo;
        protected IMasterCategoryDocumentRepository MasterCategoryDocumentRepository;
        protected IMasterCategoryQuestionRepository MasterCategoryQuestionRepository;

        public MasterPrimaryCategoryRepository(IUnitOfWork context, IMasterSecondaryLicenseCategoryRepository secondaryrepo,
            IMasterCategoryDocumentRepository masterCategoryDocumentRepository, IMasterCategoryQuestionRepository masterCategoryQuestionRepository)
            : base(context)
        {
            Secondaryrepo = secondaryrepo;
            MasterCategoryDocumentRepository = masterCategoryDocumentRepository;
            MasterCategoryQuestionRepository = masterCategoryQuestionRepository;
        }

        /// <summary>
        /// This method is used to retrive All Primary Categories list
        /// </summary>
        /// <returns>List of Primary Categories</returns>
        public IEnumerable<MasterPrimaryCategory> AllPrimaryCategories()
        {
            return GetAll().AsQueryable().OrderBy(x => x.Description);
        }

        /// <summary>
        /// This method is used to retrive Primary Categories list based on Acivity Id and Status is True
        /// </summary>
        /// <param name="submissionApplication"></param>
        /// <returns>List of Primary Categories </returns>
        public IEnumerable<MasterPrimaryCategory> FindByID(SubmissionApplication submissionApplication)
        {
            var primaryCategory = FindBy(x => x.ActivityID == submissionApplication.ActivityID && x.Status == true);
            return primaryCategory;
        }

        /// <summary>
        /// This method is used to retrive Primary Categories list sort by alphabetics based on Acivity Id and Status is True
        /// </summary>
        /// <param name="submissionApplication"></param>
        /// <returns>List of Primary Categories</returns>
        public IEnumerable<MasterPrimaryCategory> ActiveFindById(SubmissionApplication submissionApplication)
        {
            var primaryCategory = FindBy(x => x.ActivityID == submissionApplication.ActivityID && x.Status == true).OrderBy(x => x.Description);
            return primaryCategory;
        }

        /// <summary>
        /// This method is used to retrive Primary Categories list  based on Acivity Id and Status is True
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns>List of Primary Categories</returns>
        public IEnumerable<MasterPrimaryCategory> FindByID(string activityId)
        {
            var primarycategory = FindBy(x => x.ActivityID == activityId.Trim() && x.Status == true);
            return primarycategory;
        }

        /// <summary>
        /// This method is used to retrive specific Primary Category  based on the Primary Id and status
        /// </summary>
        /// <param name="submissionApplication"></param>
        /// <returns>retrun specific Primary Category</returns>
        public IEnumerable<MasterPrimaryCategory> FindByprimaryID(SubmissionApplication submissionApplication)
        {
            var primaryCategory = FindBy(x => x.PrimaryID == submissionApplication.PrimaryID && x.Status == true);
            return primaryCategory;
        }

        /// <summary>
        /// This method is used to retrive specific Primary Category  based on the Primary Id and status
        /// </summary>
        /// <param name="primaryId"></param>
        /// <returns>retrun specific Primary Category</returns>
        public IEnumerable<MasterPrimaryCategory> FindByCategoryID(string primaryId)
        {
            var primaryCategory = FindBy(x => x.PrimaryID == primaryId && x.Status == true);
            return primaryCategory;
        }

        /// <summary>
        /// This method is used to retrive  Primary Category Id based on the Category Name and status
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns>List of Primary Categories</returns>
        public IEnumerable<MasterPrimaryCategory> SecondaryEndorsement(string categoryName)
        {
            var primaryCategory = FindBy(x => x.Description.Replace(System.Environment.NewLine, "").ToString().Trim().ToUpper() ==
                                   categoryName.Replace(System.Environment.NewLine, "").Trim().ToUpper() && x.Status == true);

            return primaryCategory;
        }

        /// <summary>
        /// This method is used to create and edit the Primary Category based on inputs
        /// </summary>
        /// <param name="primaryPhysicallocation"></param>
        /// <returns>return result in string</returns>
        public string InsertUpdatePrimaryCategory(PrimaryPhysicallocation primaryPhysicallocation)
        {
            try
            {
                string result = "0";
                var primaryCategorytbl = new MasterPrimaryCategory
                {
                    Description = primaryPhysicallocation.Description,
                    ActivityID = primaryPhysicallocation.ActivityID,
                    Endorsement = primaryPhysicallocation.Endorsement,
                    CategoryCode = primaryPhysicallocation.CategoryCode,
                    UnitOne = primaryPhysicallocation.UnitOne,
                    UnitTwo = primaryPhysicallocation.UnitTwo,
                    App_Type = primaryPhysicallocation.App_Type,
                    IsSecondaryLicenseCategory = primaryPhysicallocation.IsSecondaryLicenseCategory,
                    IsSubCategory = primaryPhysicallocation.IsSubCategory,
                    IsBackgroundInvestigation = primaryPhysicallocation.IsBackgroundInvestigation,
                    IsPDFShow = primaryPhysicallocation.IsPDFShow,
                    Status = true
                };

                if (primaryPhysicallocation.PrimaryID == "")
                {
                    string primaryCategoryName = Regex.Replace(primaryPhysicallocation.Description, @"\s+", " ");
                    var primaryCategoryCode = FindBy(x => x.CategoryCode.Replace(System.Environment.NewLine, "").Trim() == primaryPhysicallocation.CategoryCode);
                    if (!primaryCategoryCode.Any())
                    {
                        var primaryname = FindBy(x => x.Description.Replace(System.Environment.NewLine, "").ToUpper().Trim() ==
                                 primaryCategoryName.ToUpper().Trim()).ToList();
                        if (!primaryname.Any())
                        {
                            primaryCategorytbl.PrimaryID = Guid.NewGuid().ToString();
                            Add(primaryCategorytbl);
                            Save();

                            result = primaryCategorytbl.PrimaryID;
                            primaryPhysicallocation.OldCategoryName = (primaryPhysicallocation.Description ?? "NA").Replace(System.Environment.NewLine, "");
                            primaryPhysicallocation.OldUnitOne = (primaryPhysicallocation.UnitOne ?? "NA").Replace(System.Environment.NewLine, "");
                            primaryPhysicallocation.OldUnitTwo = (primaryPhysicallocation.UnitTwo ?? "NA").Replace(System.Environment.NewLine, "");
                            MasterCategoryQuestionRepository.InsertUpdateCategoryName(primaryPhysicallocation);
                        }
                        else
                        {
                            result = "3";
                        }
                    }
                    else
                    {
                        result = "4";
                    }
                }
                return result;
            }
            catch (Exception ex )
            {
                throw new Exception("Exception Occurs in Insert Update Primary Category",ex);
            }
        }

        /// <summary>
        /// This method is used to change the status of the Primary Cagtegory based on inputs
        /// </summary>
        /// <param name="primaryCatEntity"></param>
        /// <returns>return result in bool</returns>
        public bool DeletePrimaryCategory(PrimaryCategoryEntity primaryCatEntity)
        {
            bool status;
            try
            {
                var primaryCategory = FindBy(x => x.PrimaryID == primaryCatEntity.PrimaryID).Single();
                primaryCategory.Status = false;
                Update(primaryCategory, primaryCatEntity.PrimaryID);
                Save();
                status = true;
            }
            catch (Exception)
            { status = false; }
            return status;
        }

        /// <summary>
        /// This method is used to retrive list of Primary Categories based on Description
        /// </summary>
        /// <param name="term"></param>
        /// <returns>List of Primary Categories</returns>
        public IEnumerable<string> FindByPrimaryCategory(string term)
        {
            var primaryCategory = (FindBy(x => x.Description.StartsWith(term) && x.Status == true).Select(y => y.Description).ToList());
            return primaryCategory;
        }

        /// <summary>
        /// This method is used to retrive list of Category Names based on Category Name
        /// </summary>
        /// <param name="primaryName"></param>
        /// <returns>list of Category Name</returns>
        public IEnumerable<string> FindByPrimaryName(string primaryName)
        {
            return from primaryCategory in (
                FindBy(x => x.Description.Replace(System.Environment.NewLine, "").Trim() == primaryName.Trim() && x.Status == true))
                   select primaryCategory.Description;
        }

        /// <summary>
        /// This method is used to edit the specific Primary Category and it reflect Category Name in  Document(s) , Secondary Category Name,
        /// Fees, Fee Code and Category Question
        /// </summary>
        /// <param name="primaryPhysicallocation"></param>
        /// <returns>Return status in bool</returns>
        public string UpdatePrimaryCategory(PrimaryPhysicallocation primaryPhysicallocation)
        {
            string status = "0";
            try
            {
                var categorycodes = FindBy(x => x.CategoryCode == primaryPhysicallocation.CategoryCode);
                var primarycategory = FindBy(x => x.PrimaryID == primaryPhysicallocation.PrimaryID).ToList();
                if (primarycategory.Count != 0)
                {
                    var primary = primarycategory.SingleOrDefault();
                    if (categorycodes.Any() && primary.CategoryCode != primaryPhysicallocation.CategoryCode)
                    {
                        status = "1";
                    }
                    else
                    {
                        primaryPhysicallocation.OldCategoryName = (primary.Description ?? "NA").Replace(System.Environment.NewLine, "");
                        primaryPhysicallocation.OldUnitOne = (primary.UnitOne ?? "NA").Replace(System.Environment.NewLine, "");
                        primaryPhysicallocation.OldUnitTwo = (primary.UnitTwo ?? "NA").Replace(System.Environment.NewLine, "");
                        if (primaryPhysicallocation.OldCategoryName.ToUpper().Trim() != primaryPhysicallocation.Description.ToUpper().Trim())
                        {
                            MasterCategoryDocumentRepository.UpdateCategoryName(primaryPhysicallocation.OldCategoryName, primaryPhysicallocation.Description);
                            Secondaryrepo.UpdateCategoryName(primaryPhysicallocation.OldCategoryName, primaryPhysicallocation.Description);
                        }
                        MasterCategoryQuestionRepository.InsertUpdateCategoryName(primaryPhysicallocation);

                        primary.Description = primaryPhysicallocation.Description;
                        primary.Endorsement = primaryPhysicallocation.Endorsement;
                        primary.CategoryCode = primaryPhysicallocation.CategoryCode;
                        primary.UnitOne = primaryPhysicallocation.UnitOne ?? "NA";
                        primary.UnitTwo = primaryPhysicallocation.UnitTwo ?? "NA";
                        primary.App_Type = primaryPhysicallocation.App_Type;
                        primary.IsSecondaryLicenseCategory = primaryPhysicallocation.IsSecondaryLicenseCategory;
                        primary.IsSubCategory = primaryPhysicallocation.IsSubCategory;
                        primary.Status = primaryPhysicallocation.Status;
                        primary.IsBackgroundInvestigation = primaryPhysicallocation.IsBackgroundInvestigation;
                        primary.IsPDFShow = primaryPhysicallocation.IsPDFShow;
                        Update(primary, primary.PrimaryID);
                        Save();
                        status = "2";
                    }
                }
                else { status = "3"; }
            }
            catch (Exception)
            {
                throw;
            }
            return status;
        }

        /// <summary>
        /// This method is used to retrive all Primary Categories based on Activity Id for Admin portal
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns>List of Primary Categories</returns>
        public IEnumerable<MasterPrimaryCategory> FindByPrimaryIdbasedonActivity(string activityId)
        {
            var primaryCategory = FindBy(x => x.ActivityID == activityId.Trim());
            return primaryCategory;
        }

        /// <summary>
        /// This is method is used to get specific Primary Category for edit based on Primary Id for Admin Portal
        /// </summary>
        /// <param name="primaryId"></param>
        /// <returns>Specific Primary Category</returns>
        public IEnumerable<MasterPrimaryCategory> FindByCategoryIDBasedonPrimaryId(string primaryId)
        {
            var primaryCategory = FindBy(x => x.PrimaryID == primaryId);
            return primaryCategory;
        }
        /// <summary>
        /// This method is used to insert or udpate secondary category based in user input
        /// </summary>
        /// <param name="primaryCatEntity"></param>
        /// <returns>Return numeric value</returns>
        public int ActiveSecondary(SlCategoryEntity primaryCatEntity)
        {
            int result = 0;
            var primaryCategory = FindBy(x => x.Description.Replace(System.Environment.NewLine, "").ToUpper().Trim() ==
                        primaryCatEntity.SecondaryLicenseCategory.ToUpper().Trim()).ToList();
            if (primaryCategory.Count() != 0)
            {
                if (primaryCategory.FirstOrDefault().Status.Value)
                {
                    result = Secondaryrepo.InsertUpdateSlCategory(primaryCatEntity);
                }
                else
                {
                    result = 4;
                }
            }

            return result;
        }
        /// <summary>
        /// This method is used to get specific secondary category data based on primary id
        /// </summary>
        /// <param name="primaryId"></param>
        /// <returns>Return secodary category data</returns>
        public List<SecondaryCategory> SecondaryCategoriesList(string primaryId)
        {
            var secondaryCategory = from primary in GetAll()
                                    join secondary in Secondaryrepo.FindBySecondaryBasedonPrimaryId(primaryId)
                                        on primary.Description.Replace(System.Environment.NewLine, "").ToUpper().Trim() equals
                                        secondary.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").ToUpper().Trim()
                                    select new
                                    {
                                        primarystatus = primary.Status,
                                        secondary.SecondaryLicenseCategory,
                                        secondary.Endorsement,
                                        secondary.IsSubCategory,
                                        secondary.PrimaryID,
                                        secondary.SecondaryID,
                                        secondary.Status,
                                        secondary.UnitOne,
                                        secondary.UnitTwo
                                    };
            return secondaryCategory.Select(secondary => new SecondaryCategory
            {
                SecondaryLicenseCategory = secondary.SecondaryLicenseCategory,
                Endorsement = secondary.Endorsement,
                IsPrimaryStatus = Convert.ToBoolean(secondary.primarystatus),
                IsSubCategory = Convert.ToBoolean(secondary.IsSubCategory),
                PrimaryId = secondary.PrimaryID,
                SecondaryId = secondary.SecondaryID,
                Status = Convert.ToBoolean(secondary.Status),
                UnitOne = secondary.UnitOne,
                UnitTwo = secondary.UnitTwo
            }).ToList();
        }
        /// <summary>
        /// This method is used to get specific master secondary license category based on user inputs.
        /// </summary>
        /// <param name="submissionApplication"></param>
        /// <returns>Return master secondary license category data</returns>
        public IEnumerable<MasterSecondaryLicenseCategory> FindBySecondaryPrimaryID(SubmissionApplication submissionApplication)
        {
            var secondaryList = (from primary in GetAll()
                                 join secondary in Secondaryrepo.FindBySecondaryBasedonPrimaryId(submissionApplication.PrimaryID) on
                             primary.Description.Replace(System.Environment.NewLine, "").ToUpper().Trim() equals
                             secondary.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").ToUpper().Trim()
                                 where
                                     secondary.Status == true && primary.Status == true &&
                                     secondary.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").Trim().ToUpper() != "NA"
                                 select secondary).OrderBy(x => x.SecondaryLicenseCategory);

            return secondaryList;
        }
    }
}