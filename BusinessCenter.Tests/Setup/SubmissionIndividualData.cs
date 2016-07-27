using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
    public class SubmissionIndividualData
    {
        private readonly List<SubmissionIndividual> _entities;
        public bool IsInitialized;

        public void AddSubmissionIndividualDataEntity(SubmissionIndividual obj)
        {
            _entities.Add(obj);
        }

        public List<SubmissionIndividual> SubmissionIndividualDataEntitiesList
        {
            get { return _entities; }
        }


        public SubmissionIndividualData()
        {
            IsInitialized = true;
            _entities = new List<SubmissionIndividual>();

            AddSubmissionIndividualDataEntity(new SubmissionIndividual()
            {
                SubmindividualId = 1,
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                CompanyName = "IMA PIZZA STORE 13 LLC.",
                CompanyBusinessLicense ="931315000135",
                FirstName ="DCBCFIRST",
                MiddleName="DCBCMIDDLE",
                LastName ="DCBCLAST",
                DateofBirth=Convert.ToDateTime("1994-11-30 00:00:00.000"), 
                City ="WINSINTON",
                State_Province ="DC",
                Country ="USA",
                Height ="10-15",
                Weight ="100",
                HairColor ="Red",
                EyeColor ="BLACK",
                IdentificationCard ="10000ABC",
                StateofIssuance ="DC",
                ExpirationDate=Convert.ToDateTime("2016-12-01 00:00:00.000"),
                CreatedDate =null,
                UpdatedDate =null
            });
            AddSubmissionIndividualDataEntity(new SubmissionIndividual()
            {
                SubmindividualId = 2,
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                Height = "10-15",
                ExpirationDate = Convert.ToDateTime("2016-12-01 00:00:00.000"),
              
            });
            //AddSubmissionIndividualDataEntity(new SubmissionIndividual()
            //{
            //    SubmindividualId = 1,
            //    MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
            //    CompanyName = "IMA PIZZA STORE 13 LLC.",
            //    CompanyBusinessLicense ="",
            //    FirstName ="",
            //    MiddleName="",
            //    LastName ="",
            //    DateofBirth=null, 
            //    City ="",
            //    State_Province ="",
            //    Country ="",
            //    Height ="",
            //    Weight ="",
            //   HairColor ="",
            //   EyeColor ="",
            //   IdentificationCard ="",
            //   StateofIssuance ="",
            //  ExpirationDate="",
            //  CreatedDate =null,
            //  UpdatedDate =null;
            //});
            //AddSubmissionIndividualDataEntity(new SubmissionIndividual()
            //{
            //    ActivityID = "668F4B68-8437-431F-9052-60809E556028",
            //    ActivityName = "Used Goods Dealing & Sales",
            //    APP_Type = "1"
            //});
            //AddSubmissionIndividualDataEntity(new SubmissionIndividual()
            //{
            //    ActivityID = "C49EACE2-2180-430E-BA2F-8B891D659421",
            //    ActivityName = "Home Improvement and Security",
            //    APP_Type = "1"
            //});

            //AddSubmissionIndividualDataEntity(new SubmissionIndividual()
            //{
            //    ActivityID = "1ED33685-B54E-4D7A-BBCE-1C93BD762CB1",
            //    ActivityName = "Retail Sales, Consulting, and Other Services",
            //    APP_Type = "1"
            //});
            //new SubmissionIndividualEntity() {
            //    SubmindividualId = 2, 
            //    MasterId = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A",
            //    CompanyName = "LincolnInc",
            //    CompanyBusinessLicense = "LincolnInc",
            //    FirstName = "Lincoln",
            //    MiddleName = "Abraham",
            //    LastName = "AB",
            //    State_Province = "DC",
            //    Country = "US",
            //    Height = "5.8",
            //    Weight = "65",
            //    HairColor = "Brown",
            //    EyeColor = "Green",
            //    IdentificationCard = "AWERER568",
            //    StateofIssuance = "DC",
            // },
        }
    }
}
