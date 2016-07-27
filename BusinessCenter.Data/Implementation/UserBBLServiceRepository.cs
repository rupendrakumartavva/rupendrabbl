using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class UserBBLServiceRepository : GenericRepository<UserBBLService>, IUserBBLServiceRepository
    {
        public UserBBLServiceRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method is used to insert/update the user bbl service based on bbl service
        /// </summary>
        /// <param name="bblService"></param>
        /// <returns>Return user id as string</returns>
        public string InsertAssociateBbl(BblAsscoiateService bblService)
        {
            string associateUserId;
            var userexist = FindByUserIdEntityId(bblService).ToList();
            if (userexist.Count == 0)
            {
                UserBBLService userservice = new UserBBLService();
                userservice.SubmissionLicense = bblService.SubmissionLicense;
                userservice.UserID = bblService.UserID;
                userservice.Type = "A";
                userservice.LicenseExpirationDate = bblService.LicenseExpirationDate;
                userservice.CreateDate = DateTime.Now;
                userservice.Status = Convert.ToBoolean(bblService.Status);
                userservice.CleanHandsType_SSN_FEIN = bblService.CleanHandsType;
                userservice.DCBC_ENTITY_ID = bblService.DCBC_ENTITY_ID;
                userservice.B1_ALT_ID = bblService.B1_ALT_ID;
                Add(userservice);
                Save();
                associateUserId = userservice.ServiceID.ToString();
            }
            else
            {
                var updateuser = userexist.First();
                updateuser.Status = Convert.ToBoolean(bblService.Status);
                Update(updateuser, updateuser.ServiceID);
                Save();
                associateUserId = updateuser.ServiceID.ToString();
            }
            return associateUserId;
        }
        /// <summary>
        /// This method is used to Insert user bbl service based on submission application
        /// </summary>
        /// <param name="subApp"></param>
        /// <returns>Retrun service id as integer</returns>
        public int InsertSubmissionBbl(SubmissionApplication subApp)
        {
            int status;
            try
            {
                UserBBLService userservice = new UserBBLService();
                userservice.SubmissionLicense = subApp.SubmissionLicense;
                userservice.UserID = subApp.UserID;
                userservice.Type = "S";
                userservice.CreateDate = DateTime.Now;
                userservice.CleanHandsType_SSN_FEIN = subApp.IsFEIN == true ? "FEIN" : "SSN";
                userservice.Status = true;
                userservice.DCBC_ENTITY_ID = subApp.SubmissionLicense;
                userservice.B1_ALT_ID = subApp.SubmissionLicense;
                Add(userservice);
                Save();
                status = userservice.ServiceID;
            }
            catch (Exception)
            {
                status = 0;
            }
            return status;
        }
        /// <summary>
        /// This method is used to update the user bbl service based in entity id , payment details and user id 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="pDetails"></param>
        /// <param name="userid"></param>
        /// <returns>Retrun bool value</returns>
        public bool UpdateUserBBL(string entityId, PaymentDetailsModel pDetails, string userid)
        {
            bool status;
            try
            {
                var updateUserServiceBbL = FindBy(x => x.SubmissionLicense.Replace(System.Environment.NewLine, "").Trim().ToUpper() == entityId.Trim()
                           && x.UserID.Trim() == userid.Trim()).ToList();
                if (updateUserServiceBbL.Any())
                {
                    var updateUserBbL = updateUserServiceBbL.First();
                    updateUserBbL.SubmissionLicense = pDetails.SubmissionLicense;
                    updateUserBbL.B1_ALT_ID = pDetails.SubmissionLicense;
                    updateUserBbL.UpdateDate = DateTime.Now;
                    Update(updateUserBbL, updateUserBbL.ServiceID);
                    Save();
                }

                status = true;
            }
            catch (Exception)
            { status = false; }
            return status;
        }
        /// <summary>
        /// This method is used to update the license expiration data on the user bbl service based on service id 
        /// </summary>
        /// <param name="userBblServiceId"></param>
        /// <param name="updateExpireDate"></param>
        /// <returns></returns>
        public bool UpdateUserAssociateExpiryDate(int userBblServiceId, DateTime updateExpireDate)
        {
            bool status;
            try
            {
                var updateUserBbL = FindBy(x => x.ServiceID == userBblServiceId).First();
                updateUserBbL.LicenseExpirationDate = updateExpireDate;
                Update(updateUserBbL, updateUserBbL.ServiceID);
                Save();
                status = true;
            }
            catch (Exception)
            { status = false; }
            return status;
        }
        /// <summary>
        /// This method is used to get particular user bbl service data based on user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Return user bbl service</returns>
        public IEnumerable<UserBBLService> FindByUserID(string userId)
        {
            var userservice = FindBy(x => x.UserID.Replace(System.Environment.NewLine, "") == userId && x.Status == true).ToList();
            return userservice;
        }
        /// <summary>
        /// This method is used to get particular user bbl service data based on service id
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns>Return user bbl service</returns>
        public IEnumerable<UserBBLService> FindByID(int serviceId)
        {
            var userservice = FindBy(x => x.ServiceID == serviceId);
            return userservice;
        }
        /// <summary>
        /// This method is used to get particular user bbl service data based on user id,submission license,b1_alt_id and entity id
        /// </summary>
        /// <param name="bblService"></param>
        /// <returns>Return user bbl service</returns>
        public IEnumerable<UserBBLService> FindByUserIdEntityId(BblAsscoiateService bblService)
        {
            var userservice = FindBy(x => x.UserID.Replace(System.Environment.NewLine, "").ToString().Trim() ==
                        bblService.UserID.ToString().Trim() && x.SubmissionLicense == bblService.SubmissionLicense.ToString().Trim()
                        && x.B1_ALT_ID.Replace(System.Environment.NewLine, "").ToString().Trim() == bblService.B1_ALT_ID.ToString().Trim()
                        && x.DCBC_ENTITY_ID.Replace(System.Environment.NewLine, "").ToString().Trim() == bblService.DCBC_ENTITY_ID.ToString().Trim());

            return userservice;
        }
        /// <summary>
        /// This method is used to get particular user bbl service data based on user id and entity id
        /// </summary>
        /// <param name="bblService"></param>
        /// <returns>Return user bbl service</returns>
        public IEnumerable<UserBBLService> FindByEntityId(BblAsscoiateService bblService)
        {
            var userservice = FindBy(x => x.UserID.Replace(System.Environment.NewLine, "").ToString().Trim() ==
                        bblService.UserID.ToString().Trim()
                        && x.DCBC_ENTITY_ID.Replace(System.Environment.NewLine, "").ToString().Trim() == bblService.DCBC_ENTITY_ID.ToString().Trim());
            //&& x.SubmissionLicense == bblService.SubmissionLicense.ToString().Trim()
            //           && x.B1_ALT_ID.Replace(System.Environment.NewLine, "").ToString().Trim() == bblService.B1_ALT_ID.ToString().Trim()
            return userservice;
        }
        /// <summary>
        /// This method is used to Update the Status of associate service based on entity id
        /// </summary>
        /// <param name="bblService"></param>
        /// <returns>Return bool status</returns>
        public bool DeleteUserService(BblAsscoiateService bblService)
        {
            bool status = false;
            try
            {
                var contact = FindByEntityId(bblService).FirstOrDefault();
                    contact.Status = false;
                    Update(contact, contact.ServiceID);
                Save();
                status = true;
            }
            catch (Exception )
            { status = false; }
            return status;
        }
        /// <summary>
        /// This method is used to get particular user bbl service data based on submission license and status is true
        /// </summary>
        /// <param name="submissionlicense"></param>
        /// <param name="userid"></param>
        /// <returns>Return user bbl service</returns>
        public IEnumerable<UserBBLService> CheckUserBBL(string submissionlicense, string userid)
        {
            var paymentdetails = FindBy(x => x.SubmissionLicense.Replace(System.Environment.NewLine, "").Trim().ToUpper() == submissionlicense.Trim()
                            && x.Status == true);
            return paymentdetails;
        }
        /// <summary>
        /// This method is used to get particular user bbl service data based on user id and status is true
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Return user bbl service</returns>
        public IEnumerable<UserBBLService> FindByUserStatusID(string userId)
        {
            var userservice = FindBy(x => x.UserID.Replace(System.Environment.NewLine, "") == userId && x.Status == true
                && x.Type.ToUpper().Trim() != "O");
            return userservice;
        }
        /// <summary>
        /// This method is used to update user id to transfer from one user to another user based on subimssion license
        /// </summary>
        /// <param name="bbldoc"></param>
        /// <param name="submissionlicense"></param>
        /// <returns>Retrun bool status</returns>
        public bool TransferSubmissions(Submissiontransfer bbldoc, string submissionlicense)
        {
            var subtransfer = FindBy(x => x.SubmissionLicense.Replace(System.Environment.NewLine, "") == submissionlicense.Trim()).FirstOrDefault();
            subtransfer.UserID = bbldoc.ToUserId;
            Update(subtransfer, subtransfer.ServiceID);
            Save();
            return true;
        }
        /// <summary>
        /// This method is used to get all user bbl service data .
        /// </summary>
        /// <returns>return user bbl service</returns>
        public IEnumerable<UserBBLService> UserBblServicesList()
        {
            var commandata = GetAll().AsQueryable();
            return commandata;
        }
        /// <summary>
        /// This method is used to update license data and entity id based on exist license , new license and entity id
        /// </summary>
        /// <param name="oldLicense"></param>
        /// <param name="newLicense"></param>
        /// <param name="entitiyid"></param>
        /// <returns>Return bool value</returns>
        public bool UpdateUserLicense(string oldLicense, string newLicense, string entityId)
        {
            bool status = false;
            try
            {
                var updateUserBbL = FindBy(x => x.SubmissionLicense.Replace(System.Environment.NewLine, "").Trim().ToUpper() == oldLicense.Trim());
                if (updateUserBbL != null && updateUserBbL.Any())
                {
                    var userBblData = updateUserBbL.FirstOrDefault();
                    userBblData.SubmissionLicense = newLicense.Trim();
                    userBblData.DCBC_ENTITY_ID = entityId;
                    Update(userBblData, userBblData.ServiceID);
                    Save();
                    status = true;
                }
            }
            catch (Exception)
            { status = false; }
            return status;
        }
        /// <summary>
        /// This method is used to get particular user bbl service based on Type equals to "A"
        /// </summary>
        /// <returns>Retrun user bbl service</returns>
        public IEnumerable<UserBBLService> GetAssociateUsers()
        {
            var commandata = FindBy(x => x.Type.ToUpper().Trim() == "A").ToList();
            return commandata;
        }
        /// <summary>
        /// This method is used to update user bbl service based on service id, submission license, b1_alt_id, entity id and user id
        /// </summary>
        /// <param name="bblService"></param>
        /// <param name="serviceId"></param>
        /// <returns>Retrun bool value</returns>
        public bool BblSubmissionExpiryUpdate(BblAsscoiateService bblService, int serviceId)
        {
            try
            {
                var checkuserbbl =
                    FindBy(x => x.SubmissionLicense.Trim().ToUpper() == bblService.SubmissionLicense.ToUpper().Trim()
                                && x.B1_ALT_ID.ToUpper().Trim() == bblService.B1_ALT_ID.ToUpper().Trim()
                                && x.DCBC_ENTITY_ID.Trim() == bblService.DCBC_ENTITY_ID.Trim()
                        ).Any();
                if (!checkuserbbl)
                {
                    var updateUser = FindBy(x => x.ServiceID == serviceId && x.UserID == bblService.UserID).ToList();
                    if (updateUser.Any())
                    {
                        var updateUserBbL = updateUser.First();

                        updateUserBbL.Type = "O";
                        updateUserBbL.UpdateDate = DateTime.Now;
                        Update(updateUserBbL, updateUserBbL.ServiceID);
                        Save();
                        UserBBLService userservice = new UserBBLService();
                        userservice.SubmissionLicense = bblService.SubmissionLicense;
                        userservice.UserID = bblService.UserID;
                        userservice.Type = "A";
                        userservice.LicenseExpirationDate = bblService.LicenseExpirationDate;
                        userservice.CreateDate = DateTime.Now;
                        userservice.Status = true;
                        userservice.CleanHandsType_SSN_FEIN = updateUser.FirstOrDefault().CleanHandsType_SSN_FEIN ?? "";
                        userservice.DCBC_ENTITY_ID = bblService.DCBC_ENTITY_ID;
                        userservice.B1_ALT_ID = bblService.B1_ALT_ID;
                        Add(userservice);
                        Save();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method is used to get particular user bbl service based on entity id and  Type equals to "O" 
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public IEnumerable<UserBBLService> FindByRenewEntityId(string entityId)
        {
            var userservice = FindBy(x =>
                        x.DCBC_ENTITY_ID.Replace(System.Environment.NewLine, "").ToString().Trim() == entityId.ToString().Trim() && x.Type.ToUpper().Trim() != "O");
            return userservice;
        }
    }
}