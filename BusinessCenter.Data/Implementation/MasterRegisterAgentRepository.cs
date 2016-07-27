using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;

namespace BusinessCenter.Data.Implementation
{
    public class MasterRegisterAgentRepository : GenericRepository<DCBC_ENTITY_CORP>, IMasterRegisterAgentRepository
    {
        public MasterRegisterAgentRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method is used to retrive Register Agent details based on File Number
        /// </summary>
        /// <param name="generalModel"></param>
        /// <returns>retrun DCBC_ENTITY_CORP data</returns>
        public IEnumerable<DCBC_ENTITY_CORP> FindByID(GeneralBusiness generalModel)
        {
            var agentDetails =
                FindBy(x => x.FileNumber == generalModel.FileNumber);
            return agentDetails;
        }

        
    }
}
