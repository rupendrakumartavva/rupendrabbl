using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Implementation
{
    public class FeeCodeMapRepository : GenericRepository<FeeCodeMap>, IFeeCodeMapRepository
    {
       protected IdentityConnectionContext Context;
       protected IOSubCategoryFeesRepository OSubCategoryFeesRepository;
       public FeeCodeMapRepository(IUnitOfWork context,IOSubCategoryFeesRepository osubrepository)
            : base(context)
        {
           OSubCategoryFeesRepository = osubrepository;
        }
        /// <summary>
        /// This method is used to retrieve particular feecodemap data, given Unit and Tier
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="tier"></param>
        /// <returns>feecodemap particular data</returns>
       public IEnumerable<FeeCodeMap> FindByID(string quantity)
        {
            try
            {
                var feecode = FindBy(x => x.Quantity.Replace(System.Environment.NewLine, "").ToString().Trim() == quantity).ToList();
                return feecode;
            }
            catch (Exception)
            {
                
                throw;
            }
           
       }
        /// <summary>
        /// This method is used to retrieve fee to be applied based on Unit, Description and quantity
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="description"></param>
        /// <param name="item"></param>
       /// <returns>OSub_Category_Fees particular data</returns>
       public IEnumerable<OSub_Category_Fees> FindBycategoryID(string quantity,string description,int item)
       {
            try
            {
                var data = (from feecode in (FindBy(x => x.Quantity.Replace(System.Environment.NewLine, "").ToString().ToUpper().Trim() == quantity.ToUpper().Trim()))
                           join osub in OSubCategoryFeesRepository.FindByCateogry(description, 0) on feecode.FeeCode equals osub.Fee_Code
                           where item >= osub.Start && item <= osub.End
                           select osub).ToList();

                return data;
            }
            catch (Exception)
            {
                
                throw;
            }
           
       }

       /// <summary>
       /// This method is used to retrieve fee to be applied based on Unit, Description and quantity
       /// </summary>
       /// <param name="quantity"></param>
       /// <param name="description"></param>
       /// <param name="item"></param>
       /// <returns>OSub_Category_Fees particular data</returns>
       public IEnumerable<OSub_Category_Fees> FindBycategoryName(string quantity, string description)
       {
           try
           {
               var data =( from feecode in (FindBy(x => x.Quantity.Replace(System.Environment.NewLine, "").ToString().Trim() == quantity))
                          join osub in OSubCategoryFeesRepository.FindByCateogry(description, 0) on feecode.FeeCode equals osub.Fee_Code
                          select osub).ToList();

               return data;
           }
           catch (Exception)
           {

               throw;
           }

       }
        /// <summary>
        /// This method is used to check the Unit Exists or not using Qunatity.
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns>Retrun Result bool</returns>
        public bool Checkunits(string quantity)
        {
            bool result = true;
            var feecodemap = FindBy(x => x.Quantity.Replace(System.Environment.NewLine, "").ToUpper().Trim() == quantity).ToList();
            if (feecodemap.Count()==0)
            {result = false;}

            return result;
        }
        /// <summary>
        /// This is method is used to create Fee Code for Unit one and Unit Two for Category name based on user inputs
        /// </summary>
        /// <param name="primaryPhysicallocation"></param>
        /// <returns>return status in bool</returns>
        public bool InsertUpdateFee(PrimaryPhysicallocation primaryPhysicallocation)
        {
            bool status = false;
            try
            {
                primaryPhysicallocation.UnitOne = primaryPhysicallocation.UnitOne ?? "";
                if (primaryPhysicallocation.UnitOne != "")
                {
                    var feecodecheck =FindBy(x=>x.Quantity.Replace(System.Environment.NewLine, "").ToString().Trim() == primaryPhysicallocation.UnitOne
                                            && x.FeeCode.Replace(System.Environment.NewLine, "").ToString().ToUpper().Trim() == primaryPhysicallocation.Fee_Code).ToList();
                    if (feecodecheck.Count() == 0)
                    {
                        FeeCodeMap feecode = new FeeCodeMap();
                        feecode.FeeCode = primaryPhysicallocation.Fee_Code ?? "";
                        feecode.Quantity = primaryPhysicallocation.UnitOne;
                        if (feecode.FeeCode.Trim() == "TA")
                        {
                            feecode.IsTier = true;
                        }
                        else
                        {
                            feecode.IsTier = false;
                        }
                        feecode.Status = null;
                        Add(feecode);
                        Save();
                    }
                }
                primaryPhysicallocation.UnitTwo = primaryPhysicallocation.UnitTwo ?? "";
                if (primaryPhysicallocation.UnitTwo != "")
                {
                    var feecodecheck =FindBy(x=>x.Quantity.Replace(System.Environment.NewLine, "").ToString().Trim() == primaryPhysicallocation.UnitTwo
                                            && x.FeeCode.Replace(System.Environment.NewLine, "").ToString().ToUpper().Trim() == primaryPhysicallocation.Fee_Code).ToList();
                    if (feecodecheck.Count() == 0)
                    {
                        FeeCodeMap feecode = new FeeCodeMap();
                        feecode.FeeCode = primaryPhysicallocation.Fee_Code ?? "";
                        feecode.Quantity = primaryPhysicallocation.UnitTwo;
                        feecode.IsTier = true;
                        feecode.Status = null;
                       Add(feecode);
                        Save();
                    }
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        /// <summary>
        /// This method is used to Update FeeCode 
        /// </summary>
        /// <param name="primaryPhysicallocation"></param>
        /// <returns>Return Bool values</returns>
        public bool UpdateFeecode(PrimaryPhysicallocation primaryPhysicallocation)
        {
            bool status = false;
            try
            {
                primaryPhysicallocation.OldUnitOne = (primaryPhysicallocation.OldUnitOne ?? "NA").Replace(System.Environment.NewLine, "") ;
                primaryPhysicallocation.OldUnitTwo = (primaryPhysicallocation.OldUnitTwo ?? "NA").Replace(System.Environment.NewLine, "");
                var feeCode = FindBy(x => x.Quantity.Replace(System.Environment.NewLine, "").ToUpper().Trim() == primaryPhysicallocation.OldUnitOne.Trim().ToUpper()).ToList();
                if (feeCode.Count() != 0)
                {
                    foreach (var code in feeCode)
                    {
                        var codefee = FindBy(x => x.FeeCodeId == code.FeeCodeId).Single();
                        codefee.Quantity = primaryPhysicallocation.UnitOne.Trim();
                        Update(codefee, codefee.FeeCodeId);
                        Save();
                    }
                    status = true;
                }
            }
            catch (Exception)
            {

                status = false;
            }
            return status;
        }

        
    }
}
