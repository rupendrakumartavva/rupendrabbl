using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data;

namespace BusinessCenter.Tests.Setup
{
    public class SubmissionBblAssociationToUsersRepositoryData
    {
         private readonly List<SubmissionBblAssociationToUsers> _entities;
        public bool IsInitialized;

        public void AddSubmissionBblAssociationToUsersEntity(SubmissionBblAssociationToUsers obj)
        {
            _entities.Add(obj);
        }

        public List<SubmissionBblAssociationToUsers> SubmissionBblAssociationToUsersEntitiesList
        {
            get { return _entities; }
        }

        public SubmissionBblAssociationToUsersRepositoryData()
        {
            IsInitialized = true;
            _entities = new List<SubmissionBblAssociationToUsers>();

            AddSubmissionBblAssociationToUsersEntity(new SubmissionBblAssociationToUsers()
            {
                SubmissionBblAssociationOtherUid = "0ef2261c-4548-4216-8d27-8fc9c938ab6c",
                SubmissionLicense = "LAPP16900002",
                FromUserId = "FF6E6EC1-2976-4C34-B92D-22E4EB00A897",
                ToUserId = "8EB70E26-725E-4E52-9109-CF9C37F3B980",
                DateOfTransfer = Convert.ToDateTime("2014-06-05"),
                CreatedBy = "2AC53E53-87D0-468F-9628-4AEBA1120613",
                ReasonForTransfer = "",
                CreatedDate = Convert.ToDateTime("2014-06-01"),
                UpdatedDate = Convert.ToDateTime("2014-06-01")
            });
            AddSubmissionBblAssociationToUsersEntity(new SubmissionBblAssociationToUsers()
            {
                SubmissionBblAssociationOtherUid = "23644abb-a2bc-4530-bd12-fb66d3d58ff3",
                SubmissionLicense = "2a5676eb-5b79-45f4-9b13-758d3df378a3",
                FromUserId = "8EB70E26-725E-4E52-9109-CF9C37F3B980",
                ToUserId = "FF6E6EC1-2976-4C34-B92D-22E4EB00A897",
                DateOfTransfer = Convert.ToDateTime("2014-06-08"),
                CreatedBy = "2AC53E53-87D0-468F-9628-4AEBA1120613",
                ReasonForTransfer = "",
                CreatedDate = Convert.ToDateTime("2014-06-01"),
                UpdatedDate = Convert.ToDateTime("2014-06-01")
            });
        }
    }
}
