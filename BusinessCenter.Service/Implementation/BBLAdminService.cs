using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Implementation
{
    public class BBLAdminService : IBBLAdminService
    {
       // protected IMasterCategoryDocumentRepository _docrepository;
        protected IOSubCategoryFeesRepository _feesrepository;

        public BBLAdminService(IOSubCategoryFeesRepository feerepo, IMasterCategoryDocumentRepository docrepo)
        {
           // _docrepository = docrepo;
            _feesrepository = feerepo;
        }
        //public IEnumerable<MasterCategoryDocument> FindByDocID(int documentid)
        //{
        //    var commondata = _docrepository.FindByDocID(documentid);
        //    return commondata;
        //}
        //public IEnumerable<MasterCategoryDocument> FindByDocName(string categoryname)
        //{
        //    var commondata = _docrepository.FindByDocName(categoryname);
        //    return commondata;
        //}
        //public IEnumerable<MasterCategoryDocument> FindByID(string categoryname)
        //{
        //    var commondata = _docrepository.FindByID(categoryname);
        //    return commondata;
        //}
        //public IEnumerable<MasterCategoryDocument> FindByRenewID(string categoryname)
        //{
        //    var commondata = _docrepository.FindByRenewID(categoryname);
        //    return commondata;
        //}
        //public int InsertUpdateCategoryDocuments(MasterCategoryDocumentModel categoryDocumentModel)
        //{
        //    return _docrepository.InsertUpdateCategoryDocuments(categoryDocumentModel);
        //}

        //public bool DeleteCategoryDocument(MasterCategoryDocumentModel categoryDocumentModel)
        //{
        //    return _docrepository.DeleteCategoryDocument(categoryDocumentModel);
        //}

        //public int InsertUpdateCategoryFees(OSub_Category_FeesEntity oSub_Category_FeesEntity)
        //{
        //    return _feesrepository.InsertUpdateCategoryFees(oSub_Category_FeesEntity);
        //}

        ////public IEnumerable<OSub_Category_Fees> FindFeesByDescription(string Description)
        ////{
        ////    return _feesrepository.FindByDescription(Description);
        ////}
        //public IEnumerable<OSub_Category_Fees> FindFeesByPrimaryCategory(string PrimaryId)
        //{
        //    return _feesrepository.FindFeesByPrimaryCategory(PrimaryId);
        //}

        //public IEnumerable<OSub_Category_Fees> FindFeesBySecondaryCategory(string secondaryId)
        //{
        //    return _feesrepository.FindFeesBySecondaryCategory(secondaryId);
        //}

        //public IEnumerable<OSub_Category_Fees> FindByCategoryFeeId(string categoryFeeId)
        //{
        //    return _feesrepository.FindByCategoryFeeId(categoryFeeId);
        //}

        //public IEnumerable<OSub_Category_Fees> AllSubCategoryFees()
        //{
        //    return _feesrepository.AllSubCategoryFees();
        //}
    }
}
