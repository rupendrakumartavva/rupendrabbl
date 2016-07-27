using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface ISubmissionBblAssociationToUsersRepository
    {
        IEnumerable<SubmissionBblAssociationToUsers> GetAllTransfer();
        bool InsertTransferLicense(Submissiontransfer bbldoc);
    }
}
