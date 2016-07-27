using System.Collections.Generic;

namespace BusinessCenter.Data.Interface
{
    public interface IMasterEhopOptionType
    {
        IEnumerable<MasterEhopOptionType> FindById(int typeId);
    }
}