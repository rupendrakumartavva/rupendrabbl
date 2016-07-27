using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
    public class SubmissionCofoHopeHopData
    {
        private readonly List<SubmissionCofo_Hop_Ehop> _entities;
        public bool IsInitialized;

        public void AddSubmissionCofoHopEhopEntity(SubmissionCofo_Hop_Ehop obj)
        {
            _entities.Add(obj);
        }

        public List<SubmissionCofo_Hop_Ehop> SubmissionCofoHopEhopList
        {
            get { return _entities; }
        }


        public SubmissionCofoHopeHopData()
        {
            IsInitialized = true;
            _entities = new List<SubmissionCofo_Hop_Ehop>();

            AddSubmissionCofoHopEhopEntity(new SubmissionCofo_Hop_Ehop()
            {
                SubCofoHopEhopId = 1,
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                UserSelectType = "hop",
                Number = "HO0800065",
                DateOfIssuance = Convert.ToDateTime("2015-09-25 00:00:00.000"),
                DoNotHaveCofo = false,
                IsUploadSupportDoc = true,
                IsValid = true,
                OccupancyAddressValidate = "InCorrect",
                IseHOPEligibility = false,
                EHopEligibilityType = "",
                ConfirmeHOPEligibilityType = false,
                CreatedDate = null,
                UpdatedDate = null
            });
            AddSubmissionCofoHopEhopEntity(new SubmissionCofo_Hop_Ehop()
            {
                SubCofoHopEhopId = 2,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                UserSelectType = "cofo",
                Number = "CO1501622",
                DateOfIssuance = Convert.ToDateTime("2015-09-25 00:00:00.000"),
                DoNotHaveCofo = false,
                IsUploadSupportDoc = true,
                IsValid = true,
                OccupancyAddressValidate = "InCorrect",
                IseHOPEligibility = false,
                EHopEligibilityType = "",
                ConfirmeHOPEligibilityType = false,
                CreatedDate = null,
                UpdatedDate = null
            });
            AddSubmissionCofoHopEhopEntity(new SubmissionCofo_Hop_Ehop()
            {
                SubCofoHopEhopId = 3,
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                UserSelectType = "ehop",
                Number = "E1599417",
                DateOfIssuance = Convert.ToDateTime("2015-12-06 00:00:00.000"),
                DoNotHaveCofo = false,
                IsUploadSupportDoc = false,
                IsValid = true,
                OccupancyAddressValidate = "",
                IseHOPEligibility = false,
                EHopEligibilityType = "",
                ConfirmeHOPEligibilityType = false,
                CreatedDate = null,
                UpdatedDate = null
            });
            //AddSubmissionCofoHopEhopEntity(new SubmissionCofo_Hop_Ehop()
            //{
            //    SubCofoHopEhopId = 4,
            //    MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
            //    UserSelectType = "hop",
            //    Number = "HO0800065",
            //    DateOfIssuance = Convert.ToDateTime("2015-09-25 00:00:00.000"),
            //    DoNotHaveCofo = false,
            //    IsUploadSupportDoc = true,
            //    IsValid = true,
            //    OccupancyAddressValidate = "InCorrect",
            //    IseHOPEligibility = false,
            //    EHopEligibilityType = "",
            //    ConfirmeHOPEligibilityType = false,
            //    CreatedDate = null,
            //    UpdatedDate = null
            //});
            //AddSubmissionCofoHopEhopEntity(new SubmissionCofo_Hop_Ehop()
            //{
            //    SubCofoHopEhopId = 4,
            //    MasterId = "4c373306-e6dd-4a51-890a-aaecdd7ea416",
            //    UserSelectType = "NOPO",
            //    Number = "NOPO",
            //    DateOfIssuance = Convert.ToDateTime("1900-01-01 00:00:00.000"),
            //    DoNotHaveCofo = false,
            //    IsUploadSupportDoc = false,
            //    IsValid = false,
            //    OccupancyAddressValidate = "",
            //    IseHOPEligibility = false,
            //    EHopEligibilityType = "",
            //    ConfirmeHOPEligibilityType = false,
            //    CreatedDate = null,
            //    UpdatedDate = null
            //});
            
        }
    }
}
