using BusinessCenter.Common;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Interface
{
    public interface ISubmissionGeneratedDocumentRepository
    {
        bool AddBusinessEntityGenarationPdf(SubmissionGeneratedDocumentEntity businessEntityGenarationPdfEntity);
        SubmissionGeneratedDocumentData AddBusinessEntityGenarationPdf(string licenseNumber, string userId, string documentFrom);
        bool FindDocument(string masterId, string documentType);
    }
}