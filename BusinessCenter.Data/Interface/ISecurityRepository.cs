using System.Collections.Generic;

namespace BusinessCenter.Data.Interface
{
    public interface ISecurityRepository
    {
        IEnumerable<SecurityQuestion> GetQuestions();
    }
}