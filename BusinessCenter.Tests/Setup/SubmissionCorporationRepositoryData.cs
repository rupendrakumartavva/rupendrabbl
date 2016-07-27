using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
  public  class SubmissionCorporationRepositoryData
    {
       private readonly List<SubmissionCorporation_Agent> _entities;
        public bool IsInitialized;

        public void AddSubmissionCorporationEntity(SubmissionCorporation_Agent obj)
        {
            _entities.Add(obj);
        }

        public List<SubmissionCorporation_Agent> SubmissionCorporationEntitiesList
        {
            get { return _entities; }
        }


        public SubmissionCorporationRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<SubmissionCorporation_Agent>();

            AddSubmissionCorporationEntity(new SubmissionCorporation_Agent()
            {
                SubCorporationRegId = 1,
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                FileNumber = "C943266",
                BusinessName = "APROSERVE CORPORATION",
                TradeName = "",
                BusinessStructure = "Corporation (Non-Profit)",
                Status = "1",
                CreatedDate=System.DateTime.Now,
                UpdatedDate=System.DateTime.Now

            });
            AddSubmissionCorporationEntity(new SubmissionCorporation_Agent()
            {
                SubCorporationRegId = 2,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                FileNumber = "C880040",
                BusinessName = "LincolnInc",
                TradeName = "Lincoln",
                BusinessStructure = "sole Proprietor",
                Status = "1",
                CreatedDate = System.DateTime.Now,
                UpdatedDate = System.DateTime.Now
            });
            //AddSubmissionCorporationEntity(new SubmissionCorporation_Agent()
            //{
            //    SubCorporationRegId = 3,
            //    MasterId = "SDE0CC3C-95EB-42AC-93B8-C7DD78B9399AA",
            //    FileNumber = "C212821",
            //    BusinessName = "LincolnInc",
            //    TradeName = "Lincoln",
            //    BusinessStructure = "sole Proprietor",
            //    Status = "0",
            //    CreatedDate = System.DateTime.Now,
            //    UpdatedDate = System.DateTime.Now
            //});
            AddSubmissionCorporationEntity(new SubmissionCorporation_Agent()
            {
                SubCorporationRegId = 4,
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                FileNumber = "C212821",
                BusinessName = "LincolnInc",
                TradeName = "Lincoln",
                BusinessStructure = "sole Proprietor",
                Status = "True"
            });
            //AddSubmissionCorporationEntity(new SubmissionCorporation_Agent()
            //{
            //    SubCorporationRegId = 1,
            //    MasterId = "SDE0CC3C-95EB-42AC-93B8-C7DD78B9399A",
            //    FileNumber = "CE55585",
            //    BusinessName = "LincolnInc",
            //    TradeName = "Lincoln",
            //    BusinessStructure = "sole Proprietor",
            //    Status = "1"
            //});

            //AddSubmissionCorporationEntity(new SubmissionCorporation_Agent()
            //{
            //    SubCorporationRegId = 1,
            //    MasterId = "SDE0CC3C-95EB-42AC-93B8-C7DD78B9399A",
            //    FileNumber = "CE55585",
            //    BusinessName = "LincolnInc",
            //    TradeName = "Lincoln",
            //    BusinessStructure = "sole Proprietor",
            //    Status = "1"
            //});

        }
    }
}
