using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class DCBC_ENTITY_BBL_RenewalsRepository : GenericRepository<DCBC_ENTITY_BBL_Renewals>, IDCBC_ENTITY_BBL_RenewalsRepository
    {
        //  protected IMasterLicenseFEINRenewal mLicenseRepo;
        public DCBC_ENTITY_BBL_RenewalsRepository(IUnitOfWork context)
            : base(context)
        {
            //,IMasterLicenseFEINRenewal mLicenseRepository
            //   mLicenseRepo = mLicenseRepository;
        }
        /// <summary>
        /// This method is used to get all Dcbc_entity_bbl_renewals data
        /// </summary>
        /// <returns>Return Dcbc_entity_bbl_renewals data</returns>
        public IEnumerable<DCBC_ENTITY_BBL_Renewals> GetBBL_Renewals()
        {
            return GetAll().AsQueryable();
        }

        /// <summary>
        /// This method is retrieve data from bbl renewal table, given License Number
        /// </summary>
        /// <param name="licenseNumber"></param>
        /// <returns>Particular bbl renewal data</returns>
        public IEnumerable<DCBC_ENTITY_BBL_Renewals> FindByLicense(string licenseNumber)
        {
            try
            {
                var renewalData = FindBy(x => x.License_Being_Renewed.Replace(System.Environment.NewLine, "").Trim() == licenseNumber.Trim() && x.B1_APPL_STATUS.ToUpper().ToString() == "BILLED")
                    .OrderByDescending(x => x.B1_APPL_STATUS_DATE).ToList();
                return renewalData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method is retrieve data from bbl renewal table, given License Number and Pin Number
        /// </summary>
        /// <param name="bblassociate"></param>
        /// <returns>Particular bbl renewal data</returns>
        public IEnumerable<DCBC_ENTITY_BBL_Renewals> FindByPin(BblAsscoiatePin bblassociate)
        {
            try
            {
                var renewaldata = FindByLicense(bblassociate.LicenseNumber).ToList();
                var expiryrenewal = renewaldata.Where(x => x.B1_APPL_STATUS_DATE == renewaldata.FirstOrDefault().B1_APPL_STATUS_DATE).ToList();
                if (expiryrenewal.Count > 1)
                {
                    List<int> lrennumber = new List<int>();
                    foreach (var lren in expiryrenewal)
                    {
                        lren.b1_Alt_ID = lren.b1_Alt_ID ?? "".Trim();
                        if (lren.b1_Alt_ID != "")
                        {
                            var result = lren.b1_Alt_ID.Substring(lren.b1_Alt_ID.Length - Math.Min(8, lren.b1_Alt_ID.Length));
                            lrennumber.Add(Convert.ToInt32(result));
                        }
                    }
                    var licenselren = lrennumber.OrderByDescending(v => v).ToList();
                    if (licenselren.Count != 0)
                    {
                        var datachck = renewaldata.Where(x => x.b1_Alt_ID.Contains(licenselren.FirstOrDefault().ToString())).ToList();
                        if (datachck.Any())
                        {
                            string renewalpin = (datachck.FirstOrDefault().OSR_Pin ?? "").Trim();
                            if (renewalpin != bblassociate.PinNumber)
                            {
                                bblassociate.PinNumber = "dcra";
                            }
                        }
                    }
                }
                else
                {
                    if (expiryrenewal.Any())
                    {
                        if ((expiryrenewal.FirstOrDefault().OSR_Pin ?? "").Trim() != bblassociate.PinNumber)
                        {
                            bblassociate.PinNumber = "dcra";
                        }
                    }
                }
                var bblpin = FindBy(x => x.License_Being_Renewed.Trim() == bblassociate.LicenseNumber.Trim() && x.OSR_Pin.Trim() == bblassociate.PinNumber.Trim()
                    && x.B1_APPL_STATUS.ToUpper().ToString() == "BILLED"
                    ).OrderByDescending(x => x.B1_APPL_STATUS_DATE);
                return bblpin;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method is used to check if the given Tax number is associated with the respective License number.
        /// </summary>
        /// <param name="bblassociate"></param>
        /// <returns></returns>
        public bool CheckAssociate(BblAsscoiatePin bblassociate)
        {
            bool status = false;
            //try
            //{
            status = true;
            //if (mLicenseRepo.FindByLicenseTax(bblassociate.LicenseNumber.Trim(), bblassociate.TaxNumber.Trim()))
            //{
            //    status = true;
            //}
            //}
            //catch (Exception)
            //{
            //    status = false;
            //}

            return status;
        }

        /// <summary>
        /// This method is used to Get the bbl renewdata based on the licesenumber and lren number
        /// </summary>
        /// <param name="licenseNumber"></param>
        /// <param name="lrenNumber"></param>
        /// <returns></returns>
        public IEnumerable<DCBC_ENTITY_BBL_Renewals> FindBybblRenewBasedonLicensenumber(string licenseNumber, string lrenNumber)
        {
            try
            {
                var renewalData = FindBy(x => x.License_Being_Renewed.Replace(System.Environment.NewLine, "").Trim() == licenseNumber.Trim()
                    && x.b1_Alt_ID.Replace(System.Environment.NewLine, "").Trim() == lrenNumber.Trim()
                    ).ToList();
                return renewalData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///This method is used to retrive particualr DCBC_ENTITY_BBL_Renewals based on b1_alt_id.
        /// </summary>
        /// <param name="licenseNumber"></param>
        /// <param name="lrenNumber"></param>
        /// <returns></returns>
        public IEnumerable<DCBC_ENTITY_BBL_Renewals> FindByLicensenumber(string lrenNumber)
        {
            try
            {
                var renewalData = FindBy(x => x.b1_Alt_ID.Replace(System.Environment.NewLine, "").Trim() == lrenNumber.Trim()
                    ).ToList();
                return renewalData;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method is used to retrive particualr DCBC_ENTITY_BBL_Renewals based on License_Being_Renewed , B1_APPL_STATUS and B1_APPL_STATUS_DATE.
        /// </summary>
        /// <param name="licensenumber"></param>
        /// <returns></returns>
        public IEnumerable<DCBC_ENTITY_BBL_Renewals> FindRenewLicense(string licensenumber)
        {
            try
            {
                var renewalData = FindBy(x => x.License_Being_Renewed.Replace(System.Environment.NewLine, "").Trim() == licensenumber.Trim() && x.B1_APPL_STATUS.ToUpper().ToString() == "BILLED")
                    .OrderByDescending(x => x.B1_APPL_STATUS_DATE).ToList();
                return renewalData;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}