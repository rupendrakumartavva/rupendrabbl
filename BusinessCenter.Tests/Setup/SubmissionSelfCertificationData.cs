using System.Collections.Generic;
using BusinessCenter.Data;
namespace BusinessCenter.Tests.Setup
{
    public class SubmissionSelfCertificationData
    {
         private readonly List<SubmissionSelfCertification> _entities;
        public bool IsInitialized;

        public void AddSelfCertificationEntity(SubmissionSelfCertification obj)
        {
            _entities.Add(obj);
        }

        public List<SubmissionSelfCertification>SelfCertificationEntitiesList
        {
            get { return _entities; }
        }


        public SubmissionSelfCertificationData()
        {
            IsInitialized = true;
            _entities = new List<SubmissionSelfCertification>();

            AddSelfCertificationEntity(new SubmissionSelfCertification()
            {
                SelfCertificationId = "bfabb7c4-38e7-4225-b611-9eedede0f4c6",
                MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
                MasterCategoryId = "FF0C5A9C-86B4-4378-BA8F-C1CE165B814E",
                IsPropertyOccupied=false,
                FullName="",
                SelfCertificationOn=System.DateTime.Now,
                CreatedDate=System.DateTime.Now,
                UpdatedDate=System.DateTime.Now
            });
            //AddSelfCertificationEntity(new SubmissionSelfCertification()
            //{
            //    SelfCertificationId = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A",
            //    MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
            //    MasterCategoryId = "1",
            //    IsPropertyOccupied = false,
            //    FullName = "",
            //    SelfCertificationOn = System.DateTime.Now,
            //    CreatedDate = System.DateTime.Now,
            //    UpdatedDate = System.DateTime.Now
            //});
            //AddSelfCertificationEntity(new SubmissionSelfCertification()
            //{
            //    SelfCertificationId = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A",
            //    MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
            //    MasterCategoryId = "1",
            //    IsPropertyOccupied = false,
            //    FullName = "",
            //    SelfCertificationOn = System.DateTime.Now,
            //    CreatedDate = System.DateTime.Now,
            //    UpdatedDate = System.DateTime.Now
            //});
            //AddSelfCertificationEntity(new SubmissionSelfCertification()
            //{
            //    SelfCertificationId = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A",
            //    MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
            //    MasterCategoryId = "1",
            //    IsPropertyOccupied = false,
            //    FullName = "",
            //    SelfCertificationOn = System.DateTime.Now,
            //    CreatedDate = System.DateTime.Now,
            //    UpdatedDate = System.DateTime.Now
            //});

            //AddSelfCertificationEntity(new SubmissionSelfCertification()
            //{
            //    SelfCertificationId = "DAD0CC3C-95EB-42AC-93B8-C7DD78B9399A",
            //    MasterId = "cce0b056-d2a3-485e-a02a-e34c957c4e40",
            //    MasterCategoryId = "1",
            //    IsPropertyOccupied = false,
            //    FullName = "",
            //    SelfCertificationOn = System.DateTime.Now,
            //    CreatedDate = System.DateTime.Now,
            //    UpdatedDate = System.DateTime.Now
            //});

         
        }
         
    }
}