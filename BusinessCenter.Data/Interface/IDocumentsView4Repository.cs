using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Interface
{
    public interface IDocumentsView4Repository
    {
        IQueryable<BblLicenseView4> GetBblLicenseView4();
        IEnumerable<BblLicenseView4> FindByFileNumber(DocumentData documentData);
    }
}
