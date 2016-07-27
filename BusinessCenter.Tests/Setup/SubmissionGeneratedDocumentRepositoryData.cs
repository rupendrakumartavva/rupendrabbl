using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
    public class SubmissionGeneratedDocumentRepositoryData
    {
        private readonly List<SubmissionGeneratedDocument> _entities;
        public bool IsInitialized;

        public void AddSubmissionDocumentEntity(SubmissionGeneratedDocument obj)
        {
            _entities.Add(obj);
        }

        public List<SubmissionGeneratedDocument> SubmissionDocumentEntitiesList
        {
            get { return _entities; }
        }


        public SubmissionGeneratedDocumentRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<SubmissionGeneratedDocument>();
            byte[] arrayofbyte= new byte[1] { 0 };
            //Initial Model Details
            AddSubmissionDocumentEntity(new SubmissionGeneratedDocument()
            {
                SubmissionGeneratedDocumentId = "356ca00b-6472-4895-89a2-c02cfa4d62c6",
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                SubmissionLicenseNumber = "LREN13013234",
                UserId = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                FileByteCode = arrayofbyte,
                FileType = "LREN13013234_RenewalReceipt.pdf",
                CreatedDate = System.DateTime.Now,
                UpdatedDate = System.DateTime.Now,
                Gen_DocumentFrom = "SUBRENEW"
            });
            AddSubmissionDocumentEntity(new SubmissionGeneratedDocument()
            {
                SubmissionGeneratedDocumentId = "4ae105f2-a8c8-4260-a33f-266d791fbbba",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                SubmissionLicenseNumber = "DAPP15985360",
                UserId = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                FileByteCode = arrayofbyte,
                FileType = "400316957915_Receipt.pdf",
                CreatedDate = System.DateTime.Now,
                UpdatedDate = System.DateTime.Now,
                Gen_DocumentFrom = "SUBREC"
            });
            AddSubmissionDocumentEntity(new SubmissionGeneratedDocument()
            {
                SubmissionGeneratedDocumentId = "4ae105f2-a8c8-4260-a33f-266d791fbccc",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                SubmissionLicenseNumber = "DAPP15985360",
                UserId = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                FileByteCode = arrayofbyte,
                FileType = "400316957915_Receipt.pdf",
                CreatedDate = System.DateTime.Now,
                UpdatedDate = System.DateTime.Now,
                Gen_DocumentFrom = "EHOP"
            });



        }
    }
}
