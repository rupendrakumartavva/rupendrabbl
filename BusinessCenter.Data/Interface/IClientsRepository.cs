using System.Collections.Generic;

namespace BusinessCenter.Data.Interface
{
    public interface IClientsRepository
    {
        IEnumerable<Clients> FindClient(string clientId);
    }
}