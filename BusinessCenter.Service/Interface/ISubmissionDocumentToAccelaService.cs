using BusinessCenter.Common;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Service.Interface
{
    public interface ISubmissionDocumentToAccelaService
    {
        bool SubmissionDocumentsToAccela(SubmissionDocumentToAccelaEntity submissiontoAccelaEntity);
        bool InsertSubmissionDocumentsToAccela(AccelaDocument submissiontoAccelaDocuments);
    }
}