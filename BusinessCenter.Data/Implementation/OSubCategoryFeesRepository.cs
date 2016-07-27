using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class OSubCategoryFeesRepository : GenericRepository<OSub_Category_Fees>,IOSubCategoryFeesRepository
    {
        protected IMasterPrimaryCategoryRepository Primarycategory;
        protected IMasterSecondaryLicenseCategoryRepository Secondarycategory;
        public OSubCategoryFeesRepository(IUnitOfWork context, IMasterPrimaryCategoryRepository primarycategory,
            IMasterSecondaryLicenseCategoryRepository secondarycategory
            )
            : base(context)
        {
            Primarycategory = primarycategory;
            Secondarycategory = secondarycategory;
        }
        /// <summary>
        /// This method is used to retrive all Categories Fees 
        /// </summary>
        /// <returns>List of Categories Fee</returns>
        public IEnumerable<OSub_Category_Fees> AllSubCategoryFees()
        {
            return GetAll().AsQueryable();
        }
        /// <summary>
        /// This method is used to get the Categories Fee(s) based on Sub Description and Quantities.
        /// </summary>
        /// <param name="description"></param>
        /// <param name="items"></param>
        /// <returns>List of Categories Fee(s)</returns>
        public IEnumerable<OSub_Category_Fees> FindByCateogry(string description,int items)
        {
           
                if (items == 0)
                {
                    var categoryfees = FindBy(x => x.OSub_Description.Replace(System.Environment.NewLine,"").ToString().ToUpper().Trim() == 
                        description.ToString().ToUpper().Trim()).ToList();
                    return categoryfees;
                }
                else
                {
                    var categoryfees = FindBy(x => x.OSub_Description.Replace(System.Environment.NewLine, "").ToString().ToUpper().Trim() ==
                             description.ToString().ToUpper().Trim() && (items >= x.Start && items <= x.End));
                    var get = GetAll();
                    return categoryfees;
                }
           
            
        }
        /// <summary>
        /// This method is used to list of Categories Fee(s) for specific Category Name 
        /// </summary>
        /// <param name="Description"></param>
        /// <returns>List of Categories Fee(s)</returns>
        public IEnumerable<OSub_Category_Fees> FindByDescription(string Description)
        {
            var categoryfees = (FindBy(x => x.OSub_Description.Replace(System.Environment.NewLine, "").Trim() == Description)).ToList();
            return categoryfees;
        }
        /// <summary>
        /// This methid is used to get the cateogry fee(s) based on Primary Id.
        /// </summary>
        /// <param name="primaryId"></param>
        /// <returns>List of category fee(s)</returns>
        public IEnumerable<OSub_Category_Fees> FindFeesByPrimaryCategory(string primaryId)
        {
           
                var primaries = Primarycategory.FindByCategoryID(primaryId);

                var records = ((from categoryfees in GetAll().OrderBy(m => m.End)
                                join primary in primaries on categoryfees.OSub_Description.Replace(System.Environment.NewLine, "").Trim()
                                    equals primary.Description.Replace(System.Environment.NewLine, "").Trim()
                                select categoryfees
                            ).ToList());
                return records;
           
        }
        /// <summary>
        /// This methid is used to get the cateogry fee(s) based on Secondary Id.
        /// </summary>
        /// <param name="secondaryId"></param>
        /// <returns>List of category fee(s)</returns>
        public IEnumerable<OSub_Category_Fees> FindFeesBySecondaryCategory(string secondaryId)
        {
            var secondaries = Secondarycategory.FindBySecondaryID(secondaryId);

            var records = ((from categoryfees in GetAll()
                join secondary in secondaries on categoryfees.OSub_Description.Replace(System.Environment.NewLine, "").Trim()
                    equals secondary.SecondaryLicenseCategory.Replace(System.Environment.NewLine, "").Trim()
                select categoryfees
                ).ToList());
            return records;
        }
        
        /// <summary>
        /// This method is used tor retrive specific Category Fee based on description, fee code and quantities.
        /// </summary>
        /// <param name="Description"></param>
        /// <param name="feecode"></param>
        /// <param name="item"></param>
        /// <returns>list of Category Fee</returns>
        public IEnumerable<OSub_Category_Fees> FindByFeeCode(string Description,string feecode,int item)
        {
            var categoryfees =
                FindBy(x => x.OSub_Description.Replace(System.Environment.NewLine, "").ToString().Trim() == Description
                            && x.Fee_Code.Replace(System.Environment.NewLine, "").ToString().Trim() == feecode
                            && (item >= x.Start && item <= x.End)).ToList();
            return categoryfees;
        }
        /// <summary>
        /// This method is used to retrive speific Category Fee record based on Fee Category Id for Admin portal
        /// </summary>
        /// <param name="categoryFeeId"></param>
        /// <returns>Specific Category Fee</returns>
        public IEnumerable<OSub_Category_Fees> FindByCategoryFeeId(string categoryFeeId)
        {
            var categoryFees = FindBy(x => x.OSub_Category == categoryFeeId);
            return categoryFees;
        }
    
    /// <summary>
    /// This method is used to create or edit Category Fee record based on user inputs.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>Return result in Number</returns>
        public int InsertUpdateCategoryFees(OSub_Category_FeesEntity entity)
        {
            int result = 0;
            try
            {
                string description = entity.OSub_Description.Replace("\n", "");
                var validate = FindBy(x => x.OSub_Description.Replace(System.Environment.NewLine, "").Trim() == description.Trim());
                var checkforvalue = validate.Where(x => x.OSub_Category == entity.OSub_Category);
                var primary = Primarycategory.SecondaryEndorsement(entity.OSub_Description.Replace("\n", "")).ToList();
                var feedocuments = new OSub_Category_Fees
                {

                    OSub_Description = entity.OSub_Description.Trim(),
                    Start = entity.Start,
                    End = entity.End,
                    Fee_Code = entity.Fee_Code,
                    License_Fee = entity.License_Fee,
                    Tier = entity.Tier,
                    App_Type = primary.FirstOrDefault().App_Type,
                    Status = true
                };
                if (!checkforvalue.Any())

                {
                    feedocuments.End = entity.End;
                    feedocuments.Fee_Code = entity.Fee_Code;
                    feedocuments.OSub_Category = Guid.NewGuid().ToString();
                    Add(feedocuments);
                    Save();
                    result = 1;
                }
                else
                {
                    var updatefee = validate.Where(x => x.OSub_Category == entity.OSub_Category).FirstOrDefault();
                    if (entity.Fee_Code.ToUpper().Trim() == "T" || entity.Fee_Code.ToUpper().Trim() == "TA")
                    {
                        var endindex = 0;
                        if (updatefee != null)
                        {
                            var endvalue = Convert.ToInt32(updatefee.End);
                            var start = FindBy(x => x.OSub_Description.Replace(System.Environment.NewLine, "").Trim() ==
                                                    entity.OSub_Description.Trim()).OrderBy(x => x.Start).ToList();
                            if (start.Count() != 1)
                            {
                                int index = start.IndexOf(start.Single(i => i.OSub_Category == entity.OSub_Category));
                                int startindex = start.IndexOf(start.Single(x => x.Start == entity.Start));
                                var data = start.Where(x => x.End == entity.End).ToList();
                                if (data.Any())
                                {
                                    endindex = start.IndexOf(start.Single(x => x.End == entity.End));
                                }
                                int diff = endindex - startindex;
                                string deletelist = string.Empty;
                                int count = 0;
                                if (diff > 1)
                                {
                                    foreach (var variable in start)
                                    {
                                        if (startindex < count)
                                        {
                                            deletelist = deletelist + variable.OSub_Category + ",";
                                        }
                                        count = count + 1;
                                    }
                                }
                                else if (diff == 1)
                                {
                                    var updatefees = start[index + 1];
                                    Delete(updatefees);
                                    Save();
                                }
                                else if (endvalue != entity.End && deletelist == string.Empty)
                                {
                                    if (start.Count() != 0)
                                    {
                                        if (start.Count() > index + 1)
                                        {
                                            var updatefees = start[index + 1];
                                            if (updatefees != null)
                                            {
                                                updatefees.Start = entity.End + 1;
                                            }
                                            Update(updatefees, updatefees.OSub_Category);
                                            Save();
                                        }
                                    }

                                }
                                if (deletelist != string.Empty)
                                {
                                    var list = deletelist.Split(',');
                                    foreach (var deleteid in list)
                                    {
                                        if (deleteid != "")
                                        {
                                            string id = deleteid;
                                            var delete = FindBy(x => x.OSub_Category == id).FirstOrDefault();
                                            Delete(delete);
                                            Save();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (updatefee != null)
                    {
                        updatefee.OSub_Description = entity.OSub_Description;
                        updatefee.Start = entity.Start;
                        updatefee.End = entity.End;
                        if (entity.Tier == null && updatefee.Fee_Code.ToUpper().Trim() == "TA")
                        {
                            updatefee.Fee_Code = "T";
                        }
                        else
                        {
                            updatefee.Fee_Code = entity.Fee_Code;
                        }
                        updatefee.License_Fee = entity.License_Fee;
                        updatefee.Tier = entity.Tier;
                        updatefee.Status = updatefee.Status;
                    }
                    Update(updatefee, updatefee.OSub_Category);
                    Save();
                    result = 2;
                }
        }
        catch (Exception)
        {
            return  100;
        }
           
           
            return result;
        }
        /// <summary>
        /// This method is used to create or edit Category Fee record based on user inputs.
        /// </summary>
        /// <param name="primaryPhysicallocation"></param>
        /// <returns>Retrun Result in String</returns>
        public string InsertUpdatePrimaryCategory(PrimaryPhysicallocation primaryPhysicallocation)
        {
           string result=string.Empty;
            try{
                result= Primarycategory.InsertUpdatePrimaryCategory(primaryPhysicallocation);
                
                    switch (result)
                    {
                        case "3":
                            return result;
//break;
                        case "4":
                            return result;
  //                          break;
                        default:
                        {
                            string description = primaryPhysicallocation.Description.Replace("\n", "");
                            var validate =FindBy(x =>x.OSub_Category.Replace(System.Environment.NewLine, "").Trim() == description.Trim());
                            var feedocuments = new OSub_Category_Fees
                            {
                                OSub_Description = primaryPhysicallocation.Description.Trim(),
                                Start = primaryPhysicallocation.Start,
                                End = primaryPhysicallocation.End,
                                Fee_Code = primaryPhysicallocation.Fee_Code,
                                License_Fee = primaryPhysicallocation.License_Fee,
                                Tier = primaryPhysicallocation.Tier,
                                App_Type = primaryPhysicallocation.App_Type,
                                Status = true
                            };
                            if (validate.ToList().Count == 0)
                            {
                                feedocuments.Start = 0;
                                if (feedocuments.Fee_Code.ToUpper().Trim() == "C")
                                {
                                   // feedocuments.Tier = 1;
                                    feedocuments.Tier = primaryPhysicallocation.Tier??1;
                                }
                                if (feedocuments.Fee_Code.ToUpper().Trim() == "T" && feedocuments.Tier != null)
                                {
                                    feedocuments.Fee_Code = "TA";
                                    feedocuments.End = 99999;
                                }
                                feedocuments.OSub_Category = Guid.NewGuid().ToString();
                                Add(feedocuments);
                                Save();
                                return result;
                            }
                            break;
                        }
                    }
               
            }
            catch (Exception)
            { result = ""; }
            return result;
        }
        /// <summary>
        /// This method is used to update sub category fee based on user inputs
        /// </summary>
        /// <param name="primaryPhysicallocation"></param>
        /// <returns>Return bool value</returns>
        public bool UpdateSubFee(PrimaryPhysicallocation primaryPhysicallocation)
        {
            var categoryFee =FindBy(x =>x.OSub_Description.Replace(System.Environment.NewLine, "").Trim().ToUpper() ==
                               primaryPhysicallocation.OldCategoryName.ToUpper()).ToList();
            if (categoryFee.Count != 0)
            {
                foreach (var feecategory in categoryFee)
                {
                    var fee =FindBy(x =>x.OSub_Category.Trim() == feecategory.OSub_Category.Trim()).SingleOrDefault();
                    fee.OSub_Description = primaryPhysicallocation.Description.Trim();
                    Update(fee, fee.OSub_Category);
                    Save();
                }
            }
            return true;
        }
    }
}
