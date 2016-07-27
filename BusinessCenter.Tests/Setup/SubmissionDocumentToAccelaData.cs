using System.Collections.Generic;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class SubmissionDocumentToAccelaData
    {
        private readonly List<SubmissionDocumentToAccela> _entities;
        public bool IsInitialized;
        public void AddSubmissionDocumentToAccelaEntity(SubmissionDocumentToAccela obj)
        {
            _entities.Add(obj);
        }

        public List<SubmissionDocumentToAccela> SubmissionDocumentToAccelaEntitiesList
        {
            get { return _entities; }
        }
        public SubmissionDocumentToAccelaData()
        {
            IsInitialized = true;
            _entities = new List<SubmissionDocumentToAccela>();

            AddSubmissionDocumentToAccelaEntity(new SubmissionDocumentToAccela()
            {
                SubmisionDocToAccelaId = 1,
                LicenseNumber = "DAPP15985360",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                MasterCategoryDocId=1,
                FileName="",
                CreatedDate=System.DateTime.Now,
                Status=true
            });
           
        }
    }
}