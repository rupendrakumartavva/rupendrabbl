using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class UserBBLServiceRepositoryData
    {
        private readonly List<UserBBLService> _entities;
        public void UserBblServiceEntity(UserBBLService obj)
        {
            _entities.Add(obj);
        }

        public List<UserBBLService> UserBblServiceList
        {
            get { return _entities; }
        }
        public UserBBLServiceRepositoryData()
        {

            _entities = new List<UserBBLService>();

            UserBblServiceEntity(new UserBBLService()
            {
               ServiceID=1,
               SubmissionLicense = "DAPP15985360",
               UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
               Type="S",
               CreateDate=Convert.ToDateTime("2015-12-04 10:36:03.063"),
               UpdateDate=null,
               LicenseExpirationDate=Convert.ToDateTime("2019-12-04 10:36:03.027"),
               Status=true,
               CleanHandsType_SSN_FEIN="SSN",
               DCBC_ENTITY_ID="DAPP15985360",
               B1_ALT_ID="DAPP15985360"
            });
            UserBblServiceEntity(new UserBBLService()
            {
                ServiceID = 2,
                SubmissionLicense = "LREN11002680",
                UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                Type = "A",
                CreateDate = Convert.ToDateTime("2015-12-04 10:36:03.063"),
                UpdateDate = null,
                LicenseExpirationDate = Convert.ToDateTime("2016-02-04 10:36:03.027"),
                Status = true,
                CleanHandsType_SSN_FEIN = "FEIN",
                DCBC_ENTITY_ID = "1",
                B1_ALT_ID = "931315000136"
            });
            UserBblServiceEntity(new UserBBLService()
            {
                ServiceID = 3,
                SubmissionLicense = "LAPP15985360",
                UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                Type = "S",
                CreateDate = Convert.ToDateTime("2015-12-04 10:36:03.063"),
                UpdateDate = null,
                LicenseExpirationDate = Convert.ToDateTime("2019-12-04 10:36:03.027"),
                Status = true,
                CleanHandsType_SSN_FEIN = "SSN",
                DCBC_ENTITY_ID = "LAPP15985360",
                B1_ALT_ID = "LAPP15985360"
            });
            UserBblServiceEntity(new UserBBLService()
            {
                ServiceID = 4,
                SubmissionLicense = "DAPP15997187",
                UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                Type = "S",
                CreateDate = Convert.ToDateTime("2015-12-04 10:36:03.063"),
                UpdateDate = null,
                LicenseExpirationDate = Convert.ToDateTime("2019-12-04 10:36:03.027"),
                Status = true,
                CleanHandsType_SSN_FEIN = "SSN",
                DCBC_ENTITY_ID = "DAPP15997187",
                B1_ALT_ID = "DAPP15997187"
            });
            UserBblServiceEntity(new UserBBLService()
            {
                ServiceID = 5,
                SubmissionLicense = "LREN13018589",
                UserID = "2AC53E53-87D0-468F-9628-4AEBA1120613",
                Type = "A",
                CreateDate = Convert.ToDateTime("2015-12-04 10:36:03.063"),
                UpdateDate = null,
                LicenseExpirationDate = Convert.ToDateTime("2016-02-04 10:36:03.027"),
                Status = true,
                CleanHandsType_SSN_FEIN = "FEIN",
                DCBC_ENTITY_ID = "100",
                B1_ALT_ID = "400312000307"
            });

            UserBblServiceEntity(new UserBBLService()
            {
                ServiceID = 6,
                SubmissionLicense = "LREN13018589",
                UserID = "FC09F5E0-762B-49B6-9E63-8BD3F0DCC57F",
                Type = "A",
                CreateDate = Convert.ToDateTime("2015-12-04 10:36:03.063"),
                UpdateDate = null,
                LicenseExpirationDate = Convert.ToDateTime("2015-02-04 10:36:03.027"),
                Status = true,
                CleanHandsType_SSN_FEIN = "FEIN",
                DCBC_ENTITY_ID = "2",
                B1_ALT_ID = "400312000720"
            });
        }
    }
}
