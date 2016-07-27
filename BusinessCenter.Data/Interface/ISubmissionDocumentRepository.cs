using BusinessCenter.Common;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface ISubmissionDocumentRepository
    {
      //  IEnumerable<SubmissionDocument> AllSubmissionDocuments();
      //  IEnumerable<SubmissionDocument> FindByID(SubmissionDocumentModel submissionDocumentModel);
        IEnumerable<BblDocuments> DocumentList(BblDocuments bbldoc);
        UploadStatus InsertServiceDocuments(BblServiceDocuments bblServiceDocuments);
        bool UpdateSubmissionMaster(BblDocuments bbldoc);
       // IEnumerable<SubmissionDocument> FindByMasterId(SubmissionTaxRevenue submissionTaxRevenu);
     //   SubmissionDocument InsertSubmissionDocument(SubmissionTaxRevenu submissionTaxRevenu);
        bool DeleteDocuments(BblServiceDocuments bbldoc);
        List<BblServiceDocuments> DocumentsList(QuestionsList questionsList,string categoryType);
        List<BblServiceDocuments> RenewalDocument(RenewQuestionsList questionsList);
        bool CheckDocument(DocumentCheck documentCheck);

        IEnumerable<MasterCategoryDocument> FindByDocID(int documentid);
        IEnumerable<MasterCategoryDocument> FindByDocName(string categoryname);
        IEnumerable<MasterCategoryDocument> FindByID(string categoryname);
        IEnumerable<MasterCategoryDocument> FindByRenewID(string categoryname);
        int InsertUpdateCategoryDocuments(MasterCategoryDocumentModel categoryDocumentModel);
        bool DeleteCategoryDocument(MasterCategoryDocumentModel categoryDocumentModel);
        bool DeleteHopcofo(string masterid, string description);
        bool DeleteRenewalDocument(RenewModel renewModel);
    //    bool FindByMasterId(string MasterId, string ChcekListType);
        bool DocumentInsertion(string Masterid,string licenseNumber);
        bool RenewaldocumentDelete(DocumentCheck documentCheck);
        List<BblServiceDocuments> DocumentByID(string Masterid);
        IEnumerable<SubmissionDocument> FindDocumentList(string enitityid);
        IEnumerable<MasterCategoryDocument> FindByDocNameBasedonCategoryName(string categoryname);
        IEnumerable<MasterCategoryDocument> FindByDocBasedonDocId(int documentid);
        bool RenewalStatuUpdation(string masterid, string submissionLicense);
    }
}
