using BusinessCenter.Common;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Service.Interface
{
    public interface ISubmissionGeneratedDocumentService
    {
        bool AddBusinessEntityGenarationPdf(SubmissionGeneratedDocumentEntity businessEntityGenarationPdfEntity);

        SubmissionGeneratedDocumentData BusinessEntityGenarationPdf(string licenseNumber, string userId, string documentFrom);
        bool FindBblOrderDocuments(string masterId, string documentType);
    }
}