using System.Collections.Generic;

namespace BusinessCenter.Data.Interface
{
    public interface IUserPasswordTrackingRepository
    {
        IEnumerable<UserPasswordTracking> UserPasswordTrackingData();
        bool InsertUserPasswordTracking(string userId, string password);
        bool PasswordStatus(string userId, string password);
    }
}