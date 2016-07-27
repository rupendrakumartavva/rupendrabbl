using System.Collections.Generic;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;

namespace BusinessCenter.Data.Implementation
{
    public class ClientsRepository : GenericRepository<Clients>, IClientsRepository
    {
        public ClientsRepository(IUnitOfWork context)
            : base(context)
        {
           
        }
        /// <summary>
        /// This method is used to retrive Client Data based on clientId
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns>Retrun Clients data</returns>
        public IEnumerable<Clients> FindClient(string clientId)
        {
            return FindBy(x => x.Id == clientId);
        }
    }
}