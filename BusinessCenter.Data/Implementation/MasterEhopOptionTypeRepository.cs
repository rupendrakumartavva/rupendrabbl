using System;
using System.Collections.Generic;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;

namespace BusinessCenter.Data.Implementation
{
    public class MasterEhopOptionTypeRepository : GenericRepository<MasterEhopOptionType>, IMasterEhopOptionType
    {
        public MasterEhopOptionTypeRepository(IUnitOfWork context)
            : base(context)
        {
        }
        /// <summary>
        /// This method is used get specific master ehop option type based on type id
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns>Return master ehop option type data</returns>
        public IEnumerable<MasterEhopOptionType> FindById(int typeId)
        {
            try
            {
                var businesslicense = FindBy(x => x.EhopOptionId == typeId);
                return businesslicense;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}