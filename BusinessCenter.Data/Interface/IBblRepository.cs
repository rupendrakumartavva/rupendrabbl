using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Interface
{
    public interface IBblRepository
    {
        IEnumerable<DCBC_ENTITY_BBL> GeBblLookupAll();

        IEnumerable<DCBC_ENTITY_BBL> FindByID(int enitityid);

        IQueryable<DCBC_ENTITY_BBL> FindBy(Expression<Func<DCBC_ENTITY_BBL, bool>> predicate);

        IEnumerable<DCBC_ENTITY_BBL> FindBySingle(Expression<Func<DCBC_ENTITY_BBL, bool>> predicate);

        // IEnumerable<DCBC_ENTITY_BBL> SearchbyCriteria();
        IEnumerable<DCBC_ENTITY_BBL> FindByLicense(string licenseNumber);

        IEnumerable<DCBC_ENTITY_BBL> GetRenewData(RenewModel renewModel);

        string ValidateBblLicence(string licenceNumber);

        //bool UpdateExpiryDate(int entityId);
        bool FindByLicenseTax(BblAsscoiatePin bblassociatepin);

        IEnumerable<DCBC_ENTITY_BBL> DailyMailAlarmToBBlLicenseUsers();

        IEnumerable<DCBC_ENTITY_BBL> FindByApplicationCap(string applicationCap);

        DCBC_ENTITY_BBL GetFirstOrDefault(Expression<Func<DCBC_ENTITY_BBL, bool>> predicate);
    }
}