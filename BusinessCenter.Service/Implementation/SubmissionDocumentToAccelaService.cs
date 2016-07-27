using BusinessCenter.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Interface;

namespace BusinessCenter.Service.Implementation
{
    public class SubmissionDocumentToAccelaService : ISubmissionDocumentToAccelaService
    {
        protected ISubmissionDocumentToAccelaRepository _submissionDocumentToAccelaRepository;

        public SubmissionDocumentToAccelaService(ISubmissionDocumentToAccelaRepository submissionDocumentToAccelaRepository)
        {
            _submissionDocumentToAccelaRepository = submissionDocumentToAccelaRepository;
        }



        public bool SubmissionDocumentsToAccela(SubmissionDocumentToAccelaEntity submissiontoAccelaEntity)
        {
            return
                _submissionDocumentToAccelaRepository.AddSubmissionDocumentsToAccelaRepository(submissiontoAccelaEntity);
        }

        public bool InsertSubmissionDocumentsToAccela(AccelaDocument submissiontoAccelaDocuments)
        {
            return
                _submissionDocumentToAccelaRepository.UpdateFinalDocumentsToAccela(submissiontoAccelaDocuments);
        }

    }
}