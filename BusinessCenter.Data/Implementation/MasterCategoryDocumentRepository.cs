using BusinessCenter.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Implementation
{
    public class MasterCategoryDocumentRepository : GenericRepository<MasterCategoryDocument>, IMasterCategoryDocumentRepository
    {
        public MasterCategoryDocumentRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method is used to retrieve specific Document based on Category Document id and status is true.
        /// </summary>
        /// <param name="documentid"></param>
        /// <returns>Specific Document Data </returns>
        public IEnumerable<MasterCategoryDocument> FindByDocID(int documentid)
        {
            try
            {
                var categoryDocument = FindBy(x => x.MasterCategoryDocId == documentid && x.Status == true);
                return categoryDocument;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// This method is used to retrieve relative Document(s) based on Category Name and status is true.
        /// </summary>
        /// <param name="categoryname"></param>
        /// <returns>Document Data</returns>
        public IEnumerable<MasterCategoryDocument> FindByDocName(string categoryname)
        {
            try
            {
                var categoryDocument = FindBy(x => x.CategoryName.Replace(System.Environment.NewLine, "").ToUpper().Trim() == categoryname.ToUpper().Trim() && x.Status == true);
                return categoryDocument;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// This method is used to retrieve relative Document(s) based on Category Name with InitialLience equal to "Y" and status is true.
        /// </summary>
        /// <param name="categoryname"></param>
        /// <returns>Document Data</returns>
        public IEnumerable<MasterCategoryDocument> FindByID(string categoryname,string categorytype)
        {
            try
            {
                //var categoryDocumentnoInput = new List <MasterCategoryDocument>();
                if (categorytype.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.Primary).ToString().ToUpper())
                {
                    var categoryDocument = FindBy(x => x.CategoryName.Replace(System.Environment.NewLine, "").ToUpper().Trim() == categoryname.ToUpper().Trim()
                                            && x.InitialLicense.Replace(System.Environment.NewLine, "").ToString().Trim().ToUpper() == "Y"
                                            && x.Status == true && x.IsPrimaryCategoryDoc==true);
                    return categoryDocument;
                }
                else if (categorytype.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.SecondaryCategory).ToString().ToUpper())
                {
                    var categoryDocument = FindBy(x => x.CategoryName.Replace(System.Environment.NewLine, "").ToUpper().Trim() == categoryname.ToUpper().Trim()
                                              && x.InitialLicense.Replace(System.Environment.NewLine, "").ToString().Trim().ToUpper() == "Y"
                                              && x.Status == true && x.IsSecondaryLicenseDoc == true);
                    return categoryDocument;
                }
                else
                { 
                var categoryDocumentnoInput = FindBy(x => x.CategoryName.Replace(System.Environment.NewLine, "").ToUpper().Trim() == categoryname.ToUpper().Trim()
                                              && x.InitialLicense.Replace(System.Environment.NewLine, "").ToString().Trim().ToUpper() == "Y"
                                              && x.Status == true );
                return categoryDocumentnoInput;
                }
                //return categoryDocument1;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// This method is used to retrieve relative Document(s) based on Category Name with Renewal equal to "Y" and status is true.
        /// </summary>
        /// <param name="categoryname"></param>
        /// <returns>Document Data</returns>
        public IEnumerable<MasterCategoryDocument> FindByRenewID(string categoryname, string categorytype)
        {
            if (categorytype.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.Primary).ToString().ToUpper())
            {
                var categoryDocument =
                    FindBy(x => x.CategoryName.Replace(System.Environment.NewLine, "").ToUpper().Trim() == categoryname.Replace(System.Environment.NewLine, "").ToUpper().Trim()
                                && x.Renewal.Replace(System.Environment.NewLine, "").ToString().Trim().ToUpper() == "Y" && x.IsPrimaryCategoryDoc==true
                                 && x.Status == true);
                return categoryDocument;
            }
            else if (categorytype.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.CategoryTypes.SecondaryCategory).ToString().ToUpper())
            {
                var categoryDocument =
                     FindBy(x => x.CategoryName.Replace(System.Environment.NewLine, "").ToUpper().Trim() == categoryname.Replace(System.Environment.NewLine, "").ToUpper().Trim()
                                 && x.Renewal.Replace(System.Environment.NewLine, "").ToString().Trim().ToUpper() == "Y" && x.IsSecondaryLicenseDoc == true
                                  && x.Status == true);
                return categoryDocument;
            }
            else
            {
                var categoryDocument =
                     FindBy(x => x.CategoryName.Replace(System.Environment.NewLine, "").ToUpper().Trim() == categoryname.Replace(System.Environment.NewLine, "").ToUpper().Trim()
                                 && x.Renewal.Replace(System.Environment.NewLine, "").ToString().Trim().ToUpper() == "Y"
                                  && x.Status == true);
                return categoryDocument;
            }
        }
        /// <summary>
        ///  This method is used to create or edit Category Document through Admin Portal.
        /// </summary>
        /// <param name="categoryDocumentModel"></param>
        /// <returns>Retrun result in Numbers</returns>
        public int InsertUpdateCategoryDocuments(MasterCategoryDocumentModel categoryDocumentModel)
        {
            int result = 0;
            try
            {

           
            var validate = FindBy(x => x.MasterCategoryDocId == categoryDocumentModel.MasterCategoryDocId);
            var categorydocuments = new MasterCategoryDocument
            {
                MasterCategoryDocId = categoryDocumentModel.MasterCategoryDocId,
                CategoryName=categoryDocumentModel.CategoryName,
                InitialLicense=categoryDocumentModel.InitialLicense,
                PostLicensure=categoryDocumentModel.PostLicensure,
                Renewal=categoryDocumentModel.Renewal,
                Agency=categoryDocumentModel.Agency,
                Agency_FullName=categoryDocumentModel.Agency_FullName,
                Div=categoryDocumentModel.Div,
                DivisionFullName=categoryDocumentModel.DivisionFullName,
                SupportingDocuments=categoryDocumentModel.SupportingDocuments,
                ShortDocName=categoryDocumentModel.ShortDocName,
                Description=categoryDocumentModel.Description,
                Status = true,
                IsPrimaryCategoryDoc = categoryDocumentModel.IsPrimaryCategoryDoc,
                IsSecondaryLicenseDoc = categoryDocumentModel.IsSecondaryLicenseDoc
            };
            if (!validate.Any())
            {
                Add(categorydocuments);
                Save();
                result = 1;
            }
            else
            {
                var updatecatdocuments = validate.SingleOrDefault();
                if (updatecatdocuments != null)
                {
                    updatecatdocuments.MasterCategoryDocId = categoryDocumentModel.MasterCategoryDocId;
                    updatecatdocuments.InitialLicense = categoryDocumentModel.InitialLicense;
                    updatecatdocuments.PostLicensure = categoryDocumentModel.PostLicensure;
                    updatecatdocuments.Renewal = categoryDocumentModel.Renewal;
                    updatecatdocuments.Agency = categoryDocumentModel.Agency;
                    updatecatdocuments.Agency_FullName = categoryDocumentModel.Agency_FullName;
                    updatecatdocuments.Div = categoryDocumentModel.Div;
                    updatecatdocuments.DivisionFullName = categoryDocumentModel.DivisionFullName;
                    updatecatdocuments.SupportingDocuments = categoryDocumentModel.SupportingDocuments;
                    updatecatdocuments.ShortDocName = categoryDocumentModel.ShortDocName;
                    updatecatdocuments.Description = categoryDocumentModel.Description;
                    updatecatdocuments.Status = categoryDocumentModel.Status;
                    updatecatdocuments.IsPrimaryCategoryDoc = categoryDocumentModel.IsPrimaryCategoryDoc;
                    updatecatdocuments.IsSecondaryLicenseDoc = categoryDocumentModel.IsSecondaryLicenseDoc;
                }
                Update(updatecatdocuments, updatecatdocuments.MasterCategoryDocId);
                Save();
                result = 2;
            }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        /// <summary>
        /// This method is used to Deactivate a specific Business Activity based on Category Document Id.
        /// </summary>
        /// <param name="categoryDocumentModel"></param>
        /// <returns>Return status in bool</returns>
        public bool DeleteCategoryDocument(MasterCategoryDocumentModel categoryDocumentModel)
        {
            bool status = false;
            try
            {
                var categorydocument = FindBy(x => x.MasterCategoryDocId == categoryDocumentModel.MasterCategoryDocId).SingleOrDefault();
                categorydocument.Status = false;
                Update(categorydocument, categorydocument.MasterCategoryDocId);
                Save();
                status = true;
            }
            catch (Exception )
            { status = false; }
            return status;
        }
        /// <summary>
        /// This method is used to update documents Category Name based on oldName and newName.
        /// </summary>
        /// <param name="oldname"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        public bool UpdateCategoryName(string oldname,string newName)
        {
            bool status = false;
            try
            {
            var categorydocument = FindBy(x => x.CategoryName.Trim().ToUpper() == oldname.ToUpper().Trim()).ToList();
                if (categorydocument.Count != 0)
                {
                    foreach (var document in categorydocument)
                    {
                        var documentCategory = FindBy(x => x.MasterCategoryDocId == document.MasterCategoryDocId).SingleOrDefault();
                        documentCategory.CategoryName = newName.Trim();
                        Update(documentCategory, documentCategory.MasterCategoryDocId);
                        Save();
                    }
                    status = true;
                }
            }
            catch (Exception )
            { status = false; }
            return status;
        }
        /// <summary>
        /// This method is used rertrive all document based on the category name for the admin portal
        /// </summary>
        /// <param name="categoryname"></param>
        /// <returns>list of documents</returns>
        public IEnumerable<MasterCategoryDocument> FindByDocNameBasedonCategoryName(string categoryname)
        {
            try
            {
                var categoryDocument = FindBy(x => x.CategoryName.Replace(System.Environment.NewLine, "").ToUpper().Trim() == categoryname.ToUpper().Trim());
                return categoryDocument;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// This method used to retrive single document for edit based on the Document Id for admin portal
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns>return Document data</returns>
        public IEnumerable<MasterCategoryDocument> FindByDocBasedonDocId(int documentId)
        {
            try
            {
                var categoryDocument = FindBy(x => x.MasterCategoryDocId == documentId);
                return categoryDocument;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
