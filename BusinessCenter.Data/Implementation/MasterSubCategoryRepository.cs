using System.Collections.Generic;
using System.Linq;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System;
using BusinessCenter.Data.Model;
using System.Text.RegularExpressions;

namespace BusinessCenter.Data.Implementation
{
    public class MasterSubCategoryRepository : GenericRepository<MasterSubCategory>, IMasterSubCategoryRepository
    {
        protected IMasterPrimaryCategoryRepository _primaryRepository;
         protected IMasterSecondaryLicenseCategoryRepository _secondaryRepository;
         public MasterSubCategoryRepository(IUnitOfWork context, IMasterPrimaryCategoryRepository primaryRepository,
             IMasterSecondaryLicenseCategoryRepository secondaryRepository)
            : base(context)
        {
           _primaryRepository = primaryRepository;
           _secondaryRepository = secondaryRepository;
        }
        
        /// <summary>
        /// This method is used to Get Sub Categries list if any of the primary and secondary category contains subcategory is true based on user inputs and Status
        /// </summary>
        /// <param name="submissionApplication"></param>
        /// <returns>List of Sub Cateogries</returns>
         public IEnumerable<MasterSubCategory> GetSuperSubCategory(SubmissionApplication submissionApplication)
         {
            var supersubcategory = new List<MasterSubCategory>();
            var primarycategory = _primaryRepository.FindByCategoryID(submissionApplication.PrimaryID).ToList();
            if (primarycategory.Count() != 0)
            {
                bool isprimarysupersub;
                isprimarysupersub = primarycategory.First().IsSubCategory ?? false;
                if (isprimarysupersub)
                {
                    string categoryname = primarycategory.First().Description ?? "";
                    supersubcategory = FindBy(x => x.CustomCategoryName.Trim() == categoryname && x.Status == true).OrderBy(x => x.SubCategoryName).ToList();
                    //if (categoryname.Replace(System.Environment.NewLine, "").ToUpper().Trim() ==
                    //    GenericEnums.GetEnumDescription(GenericEnums.Category.GeneralBusiness).ToUpper())
                    //{
                    if (supersubcategory.Any())
                    {
                        var otherbusiness = supersubcategory.Where(x => x.SubCategoryName.ToUpper().Contains("OTHER BUSINESS ACTIVITY")).ToList();
                        if (otherbusiness.Any())
                        {
                            supersubcategory.Remove(otherbusiness.FirstOrDefault());
                            supersubcategory.Add(otherbusiness.FirstOrDefault());
                        }
                    }
                    //}
                }
                if (supersubcategory.Count == 0)
                {
                    if (submissionApplication.Secondary != null)
                    {
                        var secondary = submissionApplication.Secondary.Split(',');
                        foreach (var secondaryid in secondary)
                        {
                            if (supersubcategory.Count == 0)
                            {
                                if (secondaryid.Trim() != ",")
                                {
                                    var secondarycategory = _secondaryRepository.FindBySecondaryID(secondaryid);
                                    var secondarycategoryid=_primaryRepository.SecondaryEndorsement((secondarycategory.FirstOrDefault().SecondaryLicenseCategory??"").Trim());
                                    bool issecondarysupersub;
                                    issecondarysupersub = secondarycategoryid.First().IsSubCategory ?? false;
                                    if (issecondarysupersub)
                                    {
                                        if (supersubcategory != null)
                                        {
                                            string secondarycategoryname = secondarycategory.First().SecondaryLicenseCategory ?? "";
                                            supersubcategory = FindBy(x => x.CustomCategoryName.Replace(System.Environment.NewLine, "").Trim() ==
                                                                           secondarycategoryname.Replace(System.Environment.NewLine, "") && x.Status == true).OrderBy(x => x.SubCategoryName).ToList();
                                            //if (secondarycategory.FirstOrDefault().SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").ToUpper().Trim() ==
                                            //    GenericEnums.GetEnumDescription(GenericEnums.Category.GeneralBusiness).ToUpper())
                                            //{
                                            if (supersubcategory.Any())
                                            {
                                                var otherbusiness = supersubcategory.Where(
                                                    x => x.SubCategoryName.ToUpper().Contains("OTHER BUSINESS ACTIVITY")).ToList();
                                                if (otherbusiness.Any())
                                                {
                                                    supersubcategory.Remove(otherbusiness.FirstOrDefault());
                                                    supersubcategory.Add(otherbusiness.FirstOrDefault());
                                                }
                                            }
                                          //  }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return supersubcategory.AsQueryable();
         }
        /// <summary>
        /// This method is used to Retrive specific Subcategory based on Sub Category Id and Status.
        /// </summary>
        /// <param name="enitityid"></param>
         /// <returns>Specific Sub Category </returns>
         public IEnumerable<MasterSubCategory> FindByID(string subCategoryId)
         {
            var subCategory = FindBy(x => x.SubCatID == subCategoryId && x.Status == true);
            return subCategory;
         }
        /// <summary>
        /// This method is used to retrive Sub Categories list based on Catergory Name based on Categoryname and Status.
        /// </summary>
        /// <param name="customCategoryName"></param>
        /// <returns></returns>
         public IEnumerable<MasterSubCategory> SubCategories(string customCategoryName)
         {
            var subCategories = FindBy(x => x.CustomCategoryName.Replace(System.Environment.NewLine, "").ToString().Trim() ==
                                            customCategoryName.Replace(System.Environment.NewLine, "").Trim() && x.Status == true);
            return subCategories;
         }
        /// <summary>
        /// This method is used to create or edit the Sub Category based on user inputs.
        /// </summary>
        /// <param name="subCategoryEntity"></param>
        /// <returns>Retrun result Number</returns>
         public int InsertUpdateSubCategory(SubCategoryEntity subCategoryEntity)
         {
             int result = 0;
             try
             {
                 string categoryName = Regex.Replace(subCategoryEntity.SubCategoryName, @"\s+", " ");
                 string customCategoryName = Regex.Replace(subCategoryEntity.CustomCategoryName, @"\s+", " ");
                 var subcategories = GetAll().AsQueryable();
                 var validate = subcategories.Where(x => x.SubCatID == subCategoryEntity.SubCatID);
                 subcategories = subcategories.Where(x => x.CustomCategoryName.Trim().Replace(System.Environment.NewLine, "").ToUpper()
                     == customCategoryName.Trim().Replace(System.Environment.NewLine, "").ToUpper());
                 var subcategoryname = subcategories.Where(x => x.SubCategoryName.Trim().Replace(System.Environment.NewLine, "").ToUpper()
                       == categoryName.Trim().Replace(System.Environment.NewLine, "").ToUpper()).ToList();
             
                 if (!validate.Any())
                 {
                     if (!subcategoryname.Any())
                     {
                         var subCategory = new MasterSubCategory
                         {
                             SubCatID = Guid.NewGuid().ToString().Trim(),
                             SubCategoryName = subCategoryEntity.SubCategoryName,
                             CustomCategoryName = subCategoryEntity.CustomCategoryName,
                             Status = subCategoryEntity.Status
                         };
                         Add(subCategory);
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
                     if (!subcategoryname.Any())
                     {
                         var subcategory = validate.FirstOrDefault();
                         subcategory.SubCategoryName = subCategoryEntity.SubCategoryName;
                         subcategory.Status = subCategoryEntity.Status;

                         Update(subcategory, subcategory.SubCatID);
                         Save();

                         result = 2;
                     }
                     else
                     {
                         if (validate.First().SubCategoryName.Trim().ToUpper() == subCategoryEntity.SubCategoryName.Trim().ToUpper())
                         {
                             var subcategory = validate.FirstOrDefault();
                             if (subcategory.Status == subCategoryEntity.Status)
                             {
                                 result = 4;
                             }
                             else
                             {
                                 subcategory.SubCategoryName = subCategoryEntity.SubCategoryName;
                                 subcategory.Status = subCategoryEntity.Status;
                                 Update(subcategory, subcategory.SubCatID);
                                 Save();

                                 result = 2;
                             }
                         }
                         else
                         {
                             result = 3;
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
        /// This method is used to Deactive the specific Sub Category based on user inputs.
        /// </summary>
        /// <param name="subCategoryEntity"></param>
        /// <returns>Retrun result bool</returns>
         public bool DeleteSubCategory(SubCategoryEntity subCategoryEntity)
         {
             bool status = false;
             try
             {
                 var validate = FindBy(x => x.SubCatID.Replace(System.Environment.NewLine, "").ToString().Trim() == subCategoryEntity.SubCatID.ToString().Trim()).Single();
                 validate.Status = subCategoryEntity.Status;// false;
                 Update(validate, subCategoryEntity.SubCatID);
                 Save();
                 status = true;
             }
             catch (Exception )
             { status = false; }
             return status;
         }
        /// <summary>
         /// This method is used to Retrive  Subcategories based on categoryName. 
        /// </summary>
         /// <param name="categoryName"></param>
        /// <returns>List of sub Cagtegories</returns>
         public IEnumerable<MasterSubCategory> FindBySubCategoriesBasedonPrimaryName(string categoryName)
         {
            var subcategories = FindBy(x => x.CustomCategoryName.Replace(System.Environment.NewLine, "").ToString().Trim() ==
                                            categoryName.Replace(System.Environment.NewLine, "").Trim());
            return subcategories;
         }
         //public virtual void SaveChanges()
         //{
         //    Context.SaveChanges();
         //}
        /// <summary>
         /// This method is used to Retrive specific  Subcategory based on Sub Category Id. 
        /// </summary>
        /// <param name="subCategoryId"></param>
        /// <returns>Specific Sub Category</returns>
         public IEnumerable<MasterSubCategory> FindBySubCategoryBasedonSubcatId(string subCategoryId)
         {
            var subcategories = FindBy(x => x.SubCatID == subCategoryId);
            return subcategories;
         }
    }
}