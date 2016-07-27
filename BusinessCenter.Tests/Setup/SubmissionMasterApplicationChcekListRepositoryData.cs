using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Tests.Setup
{
   public class SubmissionMasterApplicationChcekListRepositoryData
    {
       private readonly List<SubmissionMaster_ApplicationCheckList> _entities;
        public bool IsInitialized;

        public void AddSubmissionMaster_ApplicationCheckListyEntity(SubmissionMaster_ApplicationCheckList obj)
        {
            _entities.Add(obj);
        }

        public List<SubmissionMaster_ApplicationCheckList> SubmissionMaster_ApplicationCheckListEntitiesList
        {
            get { return _entities; }
        }


        public SubmissionMasterApplicationChcekListRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<SubmissionMaster_ApplicationCheckList>();

            AddSubmissionMaster_ApplicationCheckListyEntity(new SubmissionMaster_ApplicationCheckList()
            {
                SubMaster_ApplicationCheckListId = 1,
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                FEIN_SSN  =false,
                IsSubmissionCofo=false,
                IsSubmissionHop=false,
                IsSubmissioneHop=false,
                IsCleanHandsVerify=false,
                IsCorporateRegistration=false,
                IsBHAddress=false,
                IsBPAddress=false,
                IsMailAddress=false,
                IsResidentAgent=false,
                IsDocForCleanHands=false,
                IsDocForCofo=false,
                IsDocForHop=false,
                IsDocForeHop=false
            });
            AddSubmissionMaster_ApplicationCheckListyEntity(new SubmissionMaster_ApplicationCheckList()
            {
                SubMaster_ApplicationCheckListId = 2,
                MasterId = "8c6deecc-3b72-485d-8af5-30939af94e97",
                FEIN_SSN = true,
                IsSubmissionCofo = true,
                IsSubmissionHop = true,
                IsSubmissioneHop = true,
                IsCleanHandsVerify = true,
                IsCorporateRegistration = true,
                IsBHAddress = true,
                IsBPAddress = true,
                IsMailAddress = true,
                IsResidentAgent = true,
                IsDocForCleanHands = true,
                IsDocForCofo = true,
                IsDocForHop = true,
                IsDocForeHop = true
            });

            AddSubmissionMaster_ApplicationCheckListyEntity(new SubmissionMaster_ApplicationCheckList()
            {
                SubMaster_ApplicationCheckListId = 3,
                MasterId = "33b635b9-fec2-4b4f-9d07-686b8d6cc60c",
                FEIN_SSN = false,
                IsSubmissionCofo = false,
                IsSubmissionHop = false,
                IsSubmissioneHop = false,
                IsCleanHandsVerify = false,
                IsCorporateRegistration = false,
                IsBHAddress = false,
                IsBPAddress = false,
                IsMailAddress = false,
                IsResidentAgent = false,
                IsDocForCleanHands = false,
                IsDocForCofo = false,
                IsDocForHop = false,
                IsDocForeHop = false
            });
            AddSubmissionMaster_ApplicationCheckListyEntity(new SubmissionMaster_ApplicationCheckList()
            {
                SubMaster_ApplicationCheckListId = 4,
                MasterId = "4cb3a7f0-8e98-4b8d-86ae-a2d9dbe14959",
                FEIN_SSN = false,
                IsSubmissionCofo = false,
                IsSubmissionHop = false,
                IsSubmissioneHop = false,
                IsCleanHandsVerify = false,
                IsCorporateRegistration = false,
                IsBHAddress = false,
                IsBPAddress = false,
                IsMailAddress = false,
                IsResidentAgent = false,
                IsDocForCleanHands = false,
                IsDocForCofo = false,
                IsDocForHop = false,
                IsDocForeHop = false
            });
        }
    }
}
