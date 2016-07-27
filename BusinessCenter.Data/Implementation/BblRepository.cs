using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
//using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace BusinessCenter.Data.Implementation
{
    public class BblRepository : GenericRepository<DCBC_ENTITY_BBL>, IBblRepository
    {
        public BblRepository(IUnitOfWork context)
            : base(context)
        {
        }

        /// <summary>
        /// This method is used to retrive enitre Business License(BBL) records.
        /// </summary>
        /// <returns>All ABRA Records</returns>
        public IEnumerable<DCBC_ENTITY_BBL> GeBblLookupAll()
        {
            return GetAll().AsQueryable();
        }

        /// <summary>
        /// This method is used to retrieve a particular BBL record based on the Entity ID.
        /// </summary>
        /// <param name="enitityid"></param>
        /// <returns>particular bussiness license</returns>
        public IEnumerable<DCBC_ENTITY_BBL> FindByID(int enitityid)
        {
            try
            {
                var businesslicense = FindBy(x => x.DCBC_ENTITY_ID == enitityid);

                return businesslicense;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Find By Id", ex);
            }
        }
        /// <summary>
        /// This method is used to retrieve a particular BBL record based on any dynamic column for single value.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public new IEnumerable<DCBC_ENTITY_BBL> FindBySingle(Expression<Func<DCBC_ENTITY_BBL, bool>> predicate)
        {
            var query = DbSet.Where(predicate).AsNoTracking();
            return query;
        }
        /// <summary>
        /// This method is used to retrieve a particular BBL record based on any dynamic column fo first.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public DCBC_ENTITY_BBL GetFirstOrDefault(Expression<Func<DCBC_ENTITY_BBL, bool>> predicate)
        {
            var query = base.GetFirstOrDefault(predicate);
            return query;
        }

        /// <summary>
        /// This method is used to retrieve a particular BBL record based on any dynamic column.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>particular bussiness license</returns>
        // ReSharper disable once RedundantNameQualifier
        public new IQueryable<DCBC_ENTITY_BBL> FindBy(System.Linq.Expressions.Expression<Func<DCBC_ENTITY_BBL, bool>> predicate)
        {
            try
            {
                var query = DbSet.Where(predicate).AsQueryable().AsNoTracking();
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Find By Data",ex);
            }
        }

        /// <summary>
        /// This method is used to retrieve a particular BBL record, given the License Number.
        /// </summary>
        /// <param name="licenseNumber"></param>
        /// <returns>particular bussiness license</returns>
        public IEnumerable<DCBC_ENTITY_BBL> FindByLicense(string licenseNumber)
        {
            try
            {
                var bbldata = FindBy(x => x.B1_ALT_ID == licenseNumber);
                return bbldata;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Find By License", ex);
            }
        }

        /// <summary>
        /// This method is used to retrieve a particular BBL record, given the License Number and Entity Id.
        /// </summary>
        /// <param name="renewModel"></param>
        /// <returns>particular bussiness license</returns>
        public IEnumerable<DCBC_ENTITY_BBL> GetRenewData(RenewModel renewModel)
        {
            try
            {
                int entityid = Convert.ToInt32(renewModel.EntityId);
                var bblData = FindBy(x => x.DCBC_ENTITY_ID == entityid);

                return bblData;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Get Renew Data", ex);
            }
        }

        /// <summary>
        /// This method is used to retrieve a particular BBL record's status, given the License Number.
        /// </summary>
        /// <param name="licenceNumber"></param>
        /// <returns> Status of BBL record's </returns>
        public string ValidateBblLicence(string licenceNumber)
        {
            string finalResult;
            try
            {
                // ReSharper disable once RedundantNameQualifier
                var getresult = FindBy(x => x.B1_ALT_ID.Replace(System.Environment.NewLine, "").Trim().ToUpper() == licenceNumber.Trim().ToUpper()).FirstOrDefault();
                if (getresult == null)
                {
                    finalResult = "NoData";
                }
                else
                {
                    if ((getresult.B1_APPL_STATUS.ToUpper() == "ACTIVE") ||
                        (getresult.B1_APPL_STATUS.ToUpper() == "LAPSED") ||
                        (getresult.B1_APPL_STATUS.ToUpper() == "EXPIRED"))
                    {
                        finalResult = getresult.OwnrApplicant_BUSINESS_NAME ?? "";
                    }
                    else
                    {
                        finalResult = "InActive";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Validate Bbl License", ex);
            }

            return finalResult;
        }
        /// <summary>
        /// This method is used to validate tax number
        /// </summary>
        /// <param name="bblassociatepin"></param>
        /// <returns>Return bool value</returns>
        public bool FindByLicenseTax(BblAsscoiatePin bblassociatepin)
        {
            bool status = false;

            // ReSharper disable once RedundantNameQualifier
            var data = FindBy(x => x.B1_ALT_ID.Replace(System.Environment.NewLine, "").Trim() == bblassociatepin.LicenseNumber.Trim()
                                && x.TaxNumber.Replace(System.Environment.NewLine, "").Trim() == bblassociatepin.CleanHandsType.Trim()).ToList();
            if (data.Any())
            {
                var taxNumber = data.FirstOrDefault();
                // ReSharper disable once RedundantToStringCall
                if (taxNumber != null)
                {
                    // ReSharper disable once RedundantNameQualifier
                    string fienNumber = taxNumber.FEIN_SSN.ToString().Replace(System.Environment.NewLine, "");
                    fienNumber = Regex.Replace(fienNumber, @"[^\w\d]", "");
                    fienNumber = Regex.Replace(fienNumber, "[A-Za-z ]", "");
                    if (fienNumber.Trim().Replace(" ", "") == bblassociatepin.TaxNumber.Replace("-", "").Replace(" ", ""))
                    { status = true; }
                }
            }
            return status;
        }
        /// <summary>
        /// This method is used to retrieve data of bbl near to expiry license 
        /// </summary>
        /// <returns>Retrun BBl Data</returns>
        public IEnumerable<DCBC_ENTITY_BBL> DailyMailAlarmToBBlLicenseUsers()
        {
            int expiredDays = Convert.ToInt32(GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.MailalerttoExpiryDate).Trim());
            TimeSpan time = new TimeSpan(0, 0, 0, 0);
            DateTime date = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy")) + time;
            var bbldata = FindBy(x => x.Expiration_Date != null).ToList();
            bbldata = bbldata.Where(x => Convert.ToDateTime(x.Expiration_Date) >= date
                && Convert.ToDateTime(x.Expiration_Date) <= date.AddDays(expiredDays).AddHours(23).AddMinutes(59).AddSeconds(59)).ToList();
            return bbldata;
        }
        /// <summary>
        /// This method is used to retrieve a particular BBL record's , given the applicationCap.
        /// </summary>
        /// <param name="applicationCap"></param>
        /// <returns>Return Bbl Data</returns>
        public IEnumerable<DCBC_ENTITY_BBL> FindByApplicationCap(string applicationCap)
        {
            try
            {
                var bbldata = FindBy(x => x.APPLICATION_CAP == applicationCap);
                return bbldata;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Application Cap", ex);
            }
        }

        //public bool ServiceExpiryStatus(DateTime dateofExpiry)
        //{
        //    try
        //    {
        //        DateTime expiryDate = dateofExpiry;
        //        string time = DateTime.Now.ToString("HH:mm:ss");
        //        string hour = time.Substring(0, 2);
        //        int hourInt = int.Parse(hour);
        //        DateTime checkTime = DateTime.Now;
        //        if (hourInt == 0 && checkTime.Minute < 31)
        //        {
        //            checkTime = checkTime.AddDays(-1);
        //        }
        //        double dayperiod = Math.Ceiling((expiryDate - checkTime).TotalDays);

        //        if (dayperiod >= -90 && dayperiod < -1)
        //        {
        //            return true;
        //        }

        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}
    }
}