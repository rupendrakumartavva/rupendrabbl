using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Interface
{
  public  interface IPortal_Content_ErrorsRepository
    {
      IEnumerable<Portal_Content_Errors> GetAllErrors();
      IEnumerable<Portal_Content_Errors> FindByMessageId(PortaContentErrorsModel portalContentErrors);
      IEnumerable<Portal_Content_Errors> FindByMessageName(PortaContentErrorsModel portalContentErrors);
      bool InsertUpdateContentErrors(PortaContentErrorsModel portalContentErrors);
      bool DeleteContentErrors(PortaContentErrorsModel portalContentErrors);

    }
}
