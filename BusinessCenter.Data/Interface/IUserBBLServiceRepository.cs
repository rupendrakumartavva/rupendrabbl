using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface IUserBBLServiceRepository
    {
        string InsertAssociateBbl(BblAsscoiateService bblService);

        //  IEnumerable<BBlService> BBlServiceList(BblAsscoiateService BblService);
        int InsertSubmissionBbl(SubmissionApplication subApp);

        IEnumerable<UserBBLService> FindByUserID(string UserID);

        bool DeleteUserService(BblAsscoiateService bblService);

        bool UpdateUserBBL(string entityID, PaymentDetailsModel pDetails, string userid);

        IEnumerable<UserBBLService> CheckUserBBL(string entityID, string userid);

        bool UpdateUserAssociateExpiryDate(int userBblServiceId, DateTime updateExpireDate);

        IEnumerable<UserBBLService> FindByID(int ServiceID);

        IEnumerable<UserBBLService> FindByUserStatusID(string UserID);

        bool TransferSubmissions(Submissiontransfer bbldoc, string dcbcentityid);

        IEnumerable<UserBBLService> UserBblServicesList();

        bool UpdateUserLicense(string oldLicense, string newLicense, string entitiyid);

        IEnumerable<UserBBLService> GetAssociateUsers();

        IEnumerable<UserBBLService> FindByEntityId(BblAsscoiateService bblService);

        bool BblSubmissionExpiryUpdate(BblAsscoiateService bblService, int serviceId);
        IEnumerable<UserBBLService> FindByRenewEntityId(string entityId);
    }
}