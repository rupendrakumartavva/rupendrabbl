using BusinessCenter.Data.Interface;
using BusinessCenter.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Implementation
{
    public class UserPasswordTrackingService : IUserPasswordTrackingService
    {
        protected IUserPasswordTrackingRepository UserPasswordTrackingRepository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="userPasswordTrackingRepository"></param>
        public UserPasswordTrackingService(IUserPasswordTrackingRepository userPasswordTrackingRepository)
        {
            UserPasswordTrackingRepository = userPasswordTrackingRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool PasswordStatus(string userId, string password)
        {
            var commondata = UserPasswordTrackingRepository.PasswordStatus(userId, password);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool InsertUserPasswordTracking(string userId, string password)
        {
            var commondata = UserPasswordTrackingRepository.InsertUserPasswordTracking(userId, password);
            return commondata;
        }
    }
}