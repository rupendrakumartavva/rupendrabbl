using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface IOSubCategoryFeesRepository
    {
         IEnumerable<OSub_Category_Fees> AllSubCategoryFees();
         IEnumerable<OSub_Category_Fees> FindByCateogry(string description,int items);
         IEnumerable<OSub_Category_Fees> FindByDescription(string Description);
         IEnumerable<OSub_Category_Fees> FindByFeeCode(string Description, string feecode,int item);
         int InsertUpdateCategoryFees(OSub_Category_FeesEntity oSub_Category_FeesEntity);
        IEnumerable<OSub_Category_Fees> FindFeesByPrimaryCategory(string PrimaryId);
         IEnumerable<OSub_Category_Fees> FindFeesBySecondaryCategory(string secondaryId);
         IEnumerable<OSub_Category_Fees> FindByCategoryFeeId(string categoryFeeId);
         //bool InsertUpdateFee(PrimaryPhysicallocation primaryPhysicallocation);
         string InsertUpdatePrimaryCategory(PrimaryPhysicallocation primaryPhysicallocation);
        bool UpdateSubFee(PrimaryPhysicallocation primaryPhysicallocation);
    }
}
