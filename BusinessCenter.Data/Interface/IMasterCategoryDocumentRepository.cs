using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
   public interface IMasterCategoryDocumentRepository
    {
       IEnumerable<MasterCategoryDocument> FindByDocID(int documentid);
       IEnumerable<MasterCategoryDocument> FindByDocName(string categoryname);
       IEnumerable<MasterCategoryDocument> FindByID(string categoryname, string categorytype);
       IEnumerable<MasterCategoryDocument> FindByRenewID(string categoryname, string categorytype);
       int InsertUpdateCategoryDocuments(MasterCategoryDocumentModel categoryDocumentModel);
       bool DeleteCategoryDocument(MasterCategoryDocumentModel categoryDocumentModel);
       bool UpdateCategoryName(string oldname, string newName);
       IEnumerable<MasterCategoryDocument> FindByDocNameBasedonCategoryName(string categoryname);
       IEnumerable<MasterCategoryDocument> FindByDocBasedonDocId(int documentId);
    }
}
