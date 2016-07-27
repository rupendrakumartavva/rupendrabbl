using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class SubmissionBblAssociationToUsersRepository : GenericRepository<SubmissionBblAssociationToUsers>, ISubmissionBblAssociationToUsersRepository
    {
        public SubmissionBblAssociationToUsersRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method is used to get all transfers .
        /// </summary>
        /// <returns>Return SubmissionBblAssociationToUsers data</returns>
        public IEnumerable<SubmissionBblAssociationToUsers> GetAllTransfer()
        {
            return GetAll().AsQueryable();
        }


        /// <summary>
        /// This method is used to Transfer License from One User to Another License using User inputs
        /// </summary>
        /// <param name="tranferlic"></param>
        /// <returns>Retrun Bool Result</returns>
        public bool InsertTransferLicense(Submissiontransfer tranferlic)
        {
          //if(tranferlic.MasterId=="")
          //{
              tranferlic.MasterId = tranferlic.LicenseNumber;
          //}
            //  var checkuser = FindBy(x => x.SubmissionLicense.Replace(System.Environment.NewLine, "") == tranferlic.MasterId.Trim()
            //    && x.ToUserId.Replace(System.Environment.NewLine, "") == tranferlic.ToUserId.Trim()).ToList();
            //if (checkuser.Any()) return false;
            var transferData = new SubmissionBblAssociationToUsers
            {
                SubmissionBblAssociationOtherUid = Guid.NewGuid().ToString(),
                SubmissionLicense = tranferlic.MasterId ?? "",
                FromUserId = tranferlic.FromUserId ?? "",
                ToUserId = tranferlic.ToUserId ?? "",
                DateOfTransfer = DateTime.Now,
                CreatedBy = tranferlic.CreatedBy ?? "",
                ReasonForTransfer = tranferlic.ReasonForTransfer ?? ""
            };
            Add(transferData);
            Save();
         return true;
        }       
    }
}