using BusinessCenter.Common;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Interface
{
    public interface ISubmissionDocumentToAccelaRepository
    {
        bool AddSubmissionDocumentsToAccelaRepository(SubmissionDocumentToAccelaEntity submissiontoAccelaEntity);
        bool UpdateFinalDocumentsToAccela(AccelaDocument accelaDocument);
    }
}