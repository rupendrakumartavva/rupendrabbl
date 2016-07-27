using BusinessCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Interface
{
    public interface ISecurityQuestionsService
    {
        IEnumerable<SecurityQuestion> GetAll();
    }
}
