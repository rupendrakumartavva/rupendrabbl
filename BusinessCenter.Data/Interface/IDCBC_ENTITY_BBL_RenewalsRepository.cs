using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface IDCBC_ENTITY_BBL_RenewalsRepository
    {
        IEnumerable<DCBC_ENTITY_BBL_Renewals> FindByLicense(string LicenseNumber);

        IEnumerable<DCBC_ENTITY_BBL_Renewals> FindByPin(BblAsscoiatePin bblassociate);

        bool CheckAssociate(BblAsscoiatePin bblassociate);

        IEnumerable<DCBC_ENTITY_BBL_Renewals> GetBBL_Renewals();

        IEnumerable<DCBC_ENTITY_BBL_Renewals> FindBybblRenewBasedonLicensenumber(string licenseNumber, string lrenNumber);

        IEnumerable<DCBC_ENTITY_BBL_Renewals> FindByLicensenumber(string lrenNumber);

        IEnumerable<DCBC_ENTITY_BBL_Renewals> FindRenewLicense(string licensenumber);
    }
}