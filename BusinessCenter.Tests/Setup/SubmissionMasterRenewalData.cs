using BusinessCenter.Data;
using System.Collections.Generic;
namespace BusinessCenter.Tests.Setup
{
    public class SubmissionMasterRenewalData
    {
        private readonly List<SubmissionMasterRenewal> _entities;
        public bool IsInitialized;

        public void AddMasterRenewalEntity(SubmissionMasterRenewal obj)
        {
            _entities.Add(obj);
        }

        public List<SubmissionMasterRenewal> MasterRenewalEntitiesList
        {
            get { return _entities; }
        }


        public SubmissionMasterRenewalData()
        {
            IsInitialized = true;
            _entities = new List<SubmissionMasterRenewal>();

            AddMasterRenewalEntity(new SubmissionMasterRenewal()
            {
                RenewalSubmId = 4555,
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                SubmissionLicense = "LREN11002680",
                CorpNumber = "900175",
                IsDcraCorpDivision = false,
                ExpiredFee = 0,
                LapsedFee=58855,
                Extradays = "Expired",IsCorpDocRegistration=true
            });
            //AddMasterRenewalEntity(new SubmissionMasterRenewal()
            //{
            //    RenewalSubmId = 4556,
            //    MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
            //    RenewalLicenseCode = "",
            //    CorpNumber = "466262626",
            //    IsDcraCorpDivision = true,
            //    ExtraAmount = 58855,
            //    Extradays = ""
            //});
          

          

        }
    }
}