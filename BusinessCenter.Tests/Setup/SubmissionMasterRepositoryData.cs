using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
   public class SubmissionMasterRepositoryData
    {
        private readonly List<SubmissionMaster> _entities;
        public bool IsInitialized;

        public void AddSubmissionMasterEntity(SubmissionMaster obj)
        {
            _entities.Add(obj);
        }

        public List<SubmissionMaster> SubmissionMasterEntitiesList
        {
            get { return _entities; }
        }


        public SubmissionMasterRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<SubmissionMaster>();
            //Charity Row
            AddSubmissionMasterEntity(new SubmissionMaster()
            {
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                SubmissionLicense = "LREN11002680",
                UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                ActivityID = "BE29F663-A3FA-4B64-8697-C9C4FF91B69F",
                ApplicationFee = Convert.ToDecimal(0.0000),
                RAOFee = Convert.ToDecimal(0.0000),
                IseHOP = true,
                eHOP = Convert.ToDecimal(0.0000),
                Status = "Draft",
                ExpirationDate = Convert.ToDateTime("2018-02-28 08:25:05.687"),
              
                ApprovedBy = null,
                Description = null,
                App_Type = "I",
                DocSubmType = "ON",
               
                IsBusinessMustbeinDC = true,
                IsHomeBased = true,
                IsCofo = false,
                IsPhysicalLocationVerify = false,
                GrandTotal = Convert.ToDecimal(0.2000),
                isCorporationDivision = true,
                BusinessStructure = "",
                TradeName = "",
                CreatedDate = Convert.ToDateTime("2015-12-04 10:19:39.467"),
                Updatedate = Convert.ToDateTime("2015-12-04 10:19:39.467"),
                UserSelectMailAddressType = "NEWMAIL",
                LicenseDuration=2,
                UserBblAssociateId="2"


            });
           
            AddSubmissionMasterEntity(new SubmissionMaster()
            {
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                SubmissionLicense = "DAPP15985360",
                UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                ActivityID = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A",
                ApplicationFee = Convert.ToDecimal(70.0000),
                RAOFee = Convert.ToDecimal(0.0000),
                IseHOP = false,
                eHOP = Convert.ToDecimal(0.0000),
                Status = "Draft",
                ExpirationDate = Convert.ToDateTime("2017-12-05 07:39:42.147"),
                ApprovedBy = null,
                Description = null,
                App_Type = "B",
                DocSubmType = "ON",
               
                IsBusinessMustbeinDC = true,
                IsHomeBased = false,
                IsCofo = true,
                IsPhysicalLocationVerify = false,
                GrandTotal = Convert.ToDecimal(1730.3000),
                isCorporationDivision = true,
                BusinessStructure = "Corporation (Non-Profit)",
                TradeName = "",
                CreatedDate = Convert.ToDateTime("2015-12-05 07:39:42.147"),
                Updatedate =  Convert.ToDateTime("2015-12-04 10:19:39.467"),
                UserSelectMailAddressType = "NEWMAIL",
                LicenseDuration = 2,
                UserBblAssociateId = "1"


            });

            AddSubmissionMasterEntity(new SubmissionMaster()
            {
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                SubmissionLicense = "LAPP15985360",
                UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                ActivityID = "668F4B68-8437-431F-9052-60809E556028",
                ApplicationFee = Convert.ToDecimal(70.0000),
                RAOFee = Convert.ToDecimal(0.0000),
                IseHOP = false,
                eHOP = Convert.ToDecimal(0.0000),
                Status = "UnderReview",
                ExpirationDate = Convert.ToDateTime("2017-12-05 09:06:47.207"),
                ApprovedBy = null,
                Description = null,
                App_Type = "I",
                DocSubmType = "IN",
              
                IsBusinessMustbeinDC = true,
                IsHomeBased = false,
                IsCofo = false,
                IsPhysicalLocationVerify = false,
                GrandTotal = Convert.ToDecimal(740.3000),
                isCorporationDivision = true,
                BusinessStructure = "",
                TradeName = "",
                CreatedDate = Convert.ToDateTime("2015-12-05 07:39:42.147"),
                Updatedate =  Convert.ToDateTime("2015-12-04 10:19:39.467"),
                UserSelectMailAddressType = "NEWMAIL",
                LicenseDuration = 2,
                UserBblAssociateId = "3"


            });
            AddSubmissionMasterEntity(new SubmissionMaster()
            {
                MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
                SubmissionLicense = "DAPP15997187",
                UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                ActivityID = "1ED33685-B54E-4D7A-BBCE-1C93BD762CB1",
                ApplicationFee = Convert.ToDecimal(70.0000),
                RAOFee = Convert.ToDecimal(0.0000),
                IseHOP = false,
                eHOP = Convert.ToDecimal(0.0000),
                Status = "Draft",
                ExpirationDate = Convert.ToDateTime("2017-12-06 16:13:27.487"),
                ApprovedBy = null,
                Description = null,
                App_Type = "B",
                DocSubmType = "",

                IsBusinessMustbeinDC = true,
                IsHomeBased = false,
                IsCofo = false,
                IsPhysicalLocationVerify = false,
                GrandTotal = Convert.ToDecimal(556.6000),
                isCorporationDivision = true,
                BusinessStructure = "",
                TradeName = "",
                CreatedDate = Convert.ToDateTime("2015-12-06 16:13:27.487"),
                Updatedate = null,
                UserSelectMailAddressType = "NEWMAIL",
                LicenseDuration = 2,
                UserBblAssociateId = "4"
            });

            AddSubmissionMasterEntity(new SubmissionMaster()
            {
                MasterId = "1c6deeee-3b72-485d-8af5-30939af94d99",
                SubmissionLicense = "LREN13018589",
                UserID = "2AC53E53-87D0-468F-9628-4AEBA1120613",
                ActivityID = "BE29F663-A3FA-4B64-8697-C9C4FF91B69F",
                ApplicationFee = Convert.ToDecimal(0.0000),
                RAOFee = Convert.ToDecimal(0.0000),
                IseHOP = true,
                eHOP = Convert.ToDecimal(0.0000),
                Status = "Draft",
                ExpirationDate = Convert.ToDateTime("2017-12-05 08:25:05.687"),
                ApprovedBy = null,
                Description = null,
                App_Type = "I",
                DocSubmType = "ON",

                IsBusinessMustbeinDC = true,
                IsHomeBased = true,
                IsCofo = false,
                IsPhysicalLocationVerify = false,
                GrandTotal = Convert.ToDecimal(0.2000),
                isCorporationDivision = true,
                BusinessStructure = "",
                TradeName = "",
                CreatedDate = Convert.ToDateTime("2015-12-04 10:19:39.467"),
                Updatedate = Convert.ToDateTime("2015-12-04 10:19:39.467"),
                UserSelectMailAddressType = "NEWMAIL",
                LicenseDuration = 2,
                UserBblAssociateId = "5"


            });
        }
    }
}
