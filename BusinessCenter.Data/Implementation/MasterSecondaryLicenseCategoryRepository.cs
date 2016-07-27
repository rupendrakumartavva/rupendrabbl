using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessCenter.Data.Implementation
{
    public class MasterSecondaryLicenseCategoryRepository : GenericRepository<MasterSecondaryLicenseCategory>,
        IMasterSecondaryLicenseCategoryRepository
    {
        public MasterSecondaryLicenseCategoryRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method is used to specific Secondary Catogeries list based on Primary Id and status is true.
        /// </summary>
        /// <param name="submissionApplication"></param>
        /// <returns>List of Secondary Categories</returns>
        public IEnumerable<MasterSecondaryLicenseCategory> FindByID(SubmissionApplication submissionApplication)
        {
            var secondary = FindBy(x => x.PrimaryID == submissionApplication.PrimaryID && x.Status == true 
                                        && x.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").Trim().ToUpper() != "NA");
            return secondary;
        }

        /// <summary>
        ///  This method is used to specific Secondary Catogeries list based on Primary Id and status is true.
        /// </summary>
        /// <param name="primaryId"></param>
        /// <returns>List of Secondary Categories</returns>
        public IEnumerable<MasterSecondaryLicenseCategory> FindByID(string primaryId)
        {
            var secondary =FindBy(x => x.PrimaryID == primaryId.Trim() && x.SecondaryLicenseCategory != null && x.Status == true);
            return secondary;
        }

        /// <summary>
        /// This method is used to specific Secondary Catogeries list based on Primary Id , status is true and not equal to NA.
        /// </summary>
        /// <param name="primaryId"></param>
        /// <returns>List of Secondary Categories</returns>
        public IEnumerable<MasterSecondaryLicenseCategory> FindByPrimaryId(string primaryId)
        {
            var secondary = FindByID(primaryId);
            var secondarydata =secondary.Where(x =>x.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").Trim().ToUpper() != "NA" && x.Status == true);
            return secondarydata;
        }

        /// <summary>
        /// This method is used to retrive specific Secondary Category based on Secondary Id
        /// </summary>
        /// <param name="secondaryID"></param>
        /// <returns>Specific Secondary Category</returns>
        public IEnumerable<MasterSecondaryLicenseCategory> FindBySecondaryID(string secondaryId)
        {
            try
            {
                var secondary = FindBy(x => x.SecondaryID == secondaryId && x.Status == true);
                return secondary;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// This method is used to retrive specific Secondary Category based on Secondary Name
        /// </summary>
        /// <param name="secondaryName"></param>
        /// <returns>Specific Secondary Catorgies</returns>
        public IEnumerable<MasterSecondaryLicenseCategory> FindBySecondaryName(string secondaryName)
        {
            var secondary =FindBy(x =>x.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").Trim() ==
                                      secondaryName.Trim() && x.Status == true);
            return secondary;
        }

        /// <summary>
        ///This method is used to create or edit Secondary Category based on user inputs
        /// </summary>
        /// <param name="primaryCatEntity"></param>
        /// <returns>Return numbers as Result</returns>
        public int InsertUpdateSlCategory(SlCategoryEntity primaryCatEntity)
        {
            try
            {
                int result = 0;
                var secondaryCategoryName = Regex.Replace(primaryCatEntity.SecondaryLicenseCategory, @"\s+", " ");
              var  secondaries = FindBy(x => x.PrimaryID == primaryCatEntity.PrimaryId);
             var secondaryname = secondaries.Where(x=>x.SecondaryLicenseCategory.Trim().Replace(System.Environment.NewLine,"").ToUpper()==
                      secondaryCategoryName.ToUpper().Trim()).ToList();
                var validate = secondaries.Where(x => x.SecondaryID == primaryCatEntity.SecondaryId);
                var primaryCategorytbl = new MasterSecondaryLicenseCategory
                {
                    SecondaryLicenseCategory = primaryCatEntity.SecondaryLicenseCategory,
                    PrimaryID = primaryCatEntity.PrimaryId,
                    SecondaryID = primaryCatEntity.SecondaryId,
                    Status = primaryCatEntity.Status
                };
                if (!validate.Any())
                {
                    if (!secondaryname.Any())
                    {
                        primaryCategorytbl.SecondaryID = Guid.NewGuid().ToString();
                        Add(primaryCategorytbl);
                        Save();
                        result = 1;
                    }
                    else
                    {
                        result = 3;
                    }
                }
                else
                {
                    if (!secondaryname.Any())
                    {
                        var firstOrDefault = validate.FirstOrDefault();
                        firstOrDefault.SecondaryLicenseCategory = primaryCatEntity.SecondaryLicenseCategory;
                        firstOrDefault.Status = primaryCatEntity.Status;
                        Update(firstOrDefault, firstOrDefault.SecondaryID);
                        Save();
                        result = 2;
                    }
                    else
                    {
                        if (validate.First().SecondaryLicenseCategory.ToUpper().Trim() ==
                           primaryCatEntity.SecondaryLicenseCategory.ToUpper().Trim())
                        {
                            var firstOrDefault = validate.FirstOrDefault();
                            firstOrDefault.SecondaryLicenseCategory = primaryCatEntity.SecondaryLicenseCategory;
                            firstOrDefault.Status = primaryCatEntity.Status;
                            Update(firstOrDefault, firstOrDefault.SecondaryID);
                            Save();
                            result = 2;
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method is used to deactive the specific Secondary Catetory based on Secondary Id as user input for admin portal
        /// </summary>
        /// <param name="slCategoryEntity"></param>
        /// <returns>Retrun Result in Bool</returns>
        public bool DeleteSecondaryCategory(SlCategoryEntity slCategoryEntity)
        {
            bool status = false;
            try
            {
                var validate = FindBy(x => x.SecondaryID == slCategoryEntity.SecondaryId).Single();
                validate.Status = slCategoryEntity.Status;//false;//
                Update(validate, slCategoryEntity.SecondaryId);
                Save();
                status = true;
            }
            catch (Exception )
            {
                status = false;
            }
            return status;
        }
      
        /// <summary>
        /// This method is used to deactivate the all the Secondary Category whose related to Primary Category
        /// </summary>
        /// <param name="secondaryName"></param>
        /// <returns>Retrun result bool</returns>
        public bool DeleteSecondaryByPrimary(string secondaryName)
        {
            bool status = false;
            try
            {
                var secondaryCategory = FindBy(x => x.SecondaryLicenseCategory.Replace(System.Environment.NewLine,"").Trim().ToUpper() == secondaryName.Trim().ToUpper()).ToList();
                foreach (var item in secondaryCategory)
                {
                    var validate = FindBy(x => x.SecondaryID == item.SecondaryID).Single();
                    validate.Status = false;
                    Update(validate, item.SecondaryID);
                    Save();
                }
                status = true;
            }
            catch (Exception )
            {
                status = false;
            }
            return status;
        }

        /// <summary>
        /// This method is used to update the Category Name based on Primary Category name 
        /// </summary>
        /// <param name="oldname"></param>
        /// <param name="newName"></param>
        /// <returns>Return result bool</returns>
        public bool UpdateCategoryName(string oldname, string newName)
        {
            var secondarycategory =
                FindBy(x => x.SecondaryLicenseCategory.Trim().ToUpper() == oldname.ToUpper().Trim()).ToList();
            if (secondarycategory.Count != 0)
            {
                foreach (var document in secondarycategory)
                {
                    var secondary =
                        FindBy(x => x.SecondaryID == document.SecondaryID).SingleOrDefault();
                    secondary.SecondaryLicenseCategory = newName.Trim();
                    Update(secondary, document.SecondaryID);
                    Save();
                }
            }
            return true;
        }
        /// <summary>
        ///  This method is used to retrive specific Secondary Categories list based on Primary Id
        /// </summary>
        /// <param name="primaryId"></param>
        /// <returns>List of Secondary Categories</returns>
        public IEnumerable<MasterSecondaryLicenseCategory> FindBySecondaryBasedonPrimaryId(string primaryId)
        {
            var secondary = FindBy(x => x.PrimaryID.Replace(System.Environment.NewLine, "") == primaryId);
            var secondarydata =
                secondary.Where(
                    x => x.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").Trim().ToUpper() != "NA");
            return secondarydata;
        }

        /// <summary>
        /// This method is used to retrive specific Secondary Category based on Secondary Id
        /// </summary>
        /// <param name="secondaryID"></param>
        /// <returns>Specific Secondary Category</returns>
        public IEnumerable<MasterSecondaryLicenseCategory> FindBySecondaryBasedonSecondaryId(string secondaryId)
        {
            var secondary = FindBy(x => x.SecondaryID == secondaryId);
            return secondary;
        }
        /// <summary>
        /// This method is used to check the Renew data with secondary data link with primary Id
        /// </summary>
        /// <param name="categoryName,primaryId"></param>
        /// <returns></returns>
        public IEnumerable<MasterSecondaryLicenseCategory> FindByRenewSecondaryBasedonPrimary(string categoryName, string primaryId)
        {
            var secondary = FindBy(x => x.SecondaryLicenseCategory.ToUpper().Trim() == categoryName.ToUpper().Trim()
                && x.PrimaryID.Trim().ToUpper() == primaryId.Trim().ToUpper());
            return secondary;
        }
    }
}
