using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface IFixFeeRepository
    {
        IEnumerable<FixFee> AllFixFees();
    }
}
