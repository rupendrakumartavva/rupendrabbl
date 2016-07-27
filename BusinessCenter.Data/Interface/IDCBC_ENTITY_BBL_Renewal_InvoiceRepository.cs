using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Interface
{
    public interface IDCBC_ENTITY_BBL_Renewal_InvoiceRepository
    {
        List<InvoiceModel> FindAmountByLicense(string lrennumber);
        decimal RenewalCalculation(RenewModel renewModel);
    }
}
