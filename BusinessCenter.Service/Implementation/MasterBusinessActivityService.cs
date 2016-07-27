using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Interface;
using System.Collections.Generic;

namespace BusinessCenter.Service.Implementation
{
    public class MasterBusinessActivityService : IMasterBusinessActivityService
    {
        protected IMasterBusinessActivityRepository MasterBusinessRepo;

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterBusinessRepo"></param>
        public MasterBusinessActivityService(IMasterBusinessActivityRepository masterBusinessRepo)
        {
            MasterBusinessRepo = masterBusinessRepo;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MasterBusinessActivity> GetAllBusinessActivities()
        {
            var commandata = MasterBusinessRepo.AllBusinessActivities();
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="businessActivityModel"></param>
        /// <returns></returns>
        public IEnumerable<MasterBusinessActivity> FindBusinessActivitiesById(BusinessActivityModel businessActivityModel)
        {
            var commondata = MasterBusinessRepo.FindByID(businessActivityModel);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="businessActivityModel"></param>
        /// <returns></returns>
        public int InsertUpdateBusinessActivity(BusinessActivityEntity businessActivityModel)
        {
            return MasterBusinessRepo.InsertUpdateBusinessActivity(businessActivityModel);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="businessActivityModel"></param>
        /// <returns></returns>
        public bool DeleteBusinessActivity(BusinessActivityEntity businessActivityModel)
        {
            return MasterBusinessRepo.DeleteBusinessActivity(businessActivityModel);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MasterBusinessActivity> GetBusinessActivity()
        {
            return MasterBusinessRepo.GetBusinessActivity();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="businessActivityModel"></param>
        /// <returns></returns>
        public IEnumerable<MasterBusinessActivity> FindByIDBasedonActivityId(BusinessActivityModel businessActivityModel)
        {
            var commondata = MasterBusinessRepo.FindByIDBasedonActivityId(businessActivityModel);
            return commondata;
        }
    }
}