using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Implementation
{
    public class MasterBblApplicationStatusRepository : GenericRepository<MasterBblApplicationStatus>, IMasterBblApplicationStatusRepository
    {
        public MasterBblApplicationStatusRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method is used to get all master bbl application status data 
        /// </summary>
        /// <returns>Return MasterBblApplicationStatus data</returns>
        public IEnumerable<MasterBblApplicationStatus> GetAllStatus()
        {
            return GetAll().AsEnumerable();
        }
        /// <summary>
        /// This method is used to get specific master bbl application status based on application cap.
        /// </summary>
        /// <param name="applicationcap"></param>
        /// <returns>Return MasterBblApplicationStatus Data</returns>
        public IEnumerable<MasterBblApplicationStatus> FindByStatus(string applicationcap)
        {
            try
            {
                var applicationstatus = FindBy(x => x.Application_Cap_Status.ToUpper().Trim() == applicationcap.ToUpper().Trim());

                return applicationstatus;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}