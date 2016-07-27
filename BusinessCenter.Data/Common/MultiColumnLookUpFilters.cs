using BusinessCenter.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Common
{
    public class MultiColumnLookUpFilters
    {
        /// <summary>
        /// This method is used to bind the Quick search for the abra data for the particular entity
        /// </summary>
        /// <param name="sno"></param>
        /// <param name="entityid"></param>
        /// <param name="wishlist"></param>
        /// <param name="abraQuery"></param>
        /// <returns></returns>
        public async Task<ICollection<CommonData>> GetAbraRecords(int sno, int entityid, bool wishlist, ICollection<DCBC_ENTITY_ABRA> abraQuery)
        {
          
            return await Task.FromResult(abraQuery.Select(abraData => new CommonData
            {
                id = sno,
                WishList = wishlist,
                EntityID = abraData.DCBC_ENTITY_ID,
                Source = abraData.DCBC_ENTITY_SOURCE.ToUpper().Trim(),
                SourceFullName = "Alcoholic Beverage License",
                LeftNameMiddle = "Trade Name : ",
                LeftNameResultMiddle = abraData.B1_SPECIAL_TEXT.Replace("  ", " "),
                MiddleNameTop = "Business Address : ",

                MiddleNameResultTop = ((abraData.B1_HSE_NBR_START == null ? "" : abraData.B1_HSE_NBR_START.ToString().ToUpper().Trim() == "NA" ? "" : abraData.B1_HSE_NBR_START.ToString().Trim()) +
                (abraData.B1_HSE_NBR_END == null ? "" : abraData.B1_HSE_NBR_END.ToString().ToUpper().Trim() == "NA" ? "" : "-" + abraData.B1_HSE_NBR_END.ToString().Trim()) +
                 (abraData.B1_HSE_FRAC_NBR_START == null ? "" : abraData.B1_HSE_FRAC_NBR_START.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.B1_HSE_NBR_START.ToString().Trim()) +
                  (abraData.B1_HSE_FRAC_NBR_END == null ? "" : abraData.B1_HSE_FRAC_NBR_END.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.B1_HSE_FRAC_NBR_END.ToString().Trim()) +
                     (abraData.B1_UNIT_START == null ? "" : abraData.B1_UNIT_START.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.B1_UNIT_START.ToString().Trim()) +
                     (abraData.B1_STR_NAME == null ? "" : abraData.B1_STR_NAME.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.B1_STR_NAME.ToString().Trim()) +
                     (abraData.B1_STR_SUFFIX == null ? "" : abraData.B1_STR_SUFFIX.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.B1_STR_SUFFIX.ToString().Trim()) +
                     (abraData.B1_STR_SUFFIX_DIR == null ? "" : abraData.B1_STR_SUFFIX_DIR.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.B1_STR_SUFFIX_DIR.ToString().Trim()) +
                      (abraData.B1_SITUS_CITY == null ? "" : abraData.B1_SITUS_CITY.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.B1_SITUS_CITY.ToString().Trim()) +
                      (abraData.B1_SITUS_STATE == null ? "" : abraData.B1_SITUS_STATE.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.B1_SITUS_STATE.ToString().Trim()) +
                      (abraData.B1_SITUS_ZIP == null ? "" : abraData.B1_SITUS_ZIP.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.B1_SITUS_ZIP.ToString().Trim())).Replace("  ", " ").Replace("  ", " "),
                RightNameTop = "License Number : ",

                RightNameResultTop = abraData.License_Number.ToString() == null ? "" : abraData.License_Number.ToString().ToUpper().Trim() == "NA" ? "" : abraData.License_Number.ToString().Trim(),
                RightNameMiddle1 = "License Class : ",
                RightNameResultMiddle1 = abraData.Class_Type == null ? "" : abraData.Class_Type.ToString().ToUpper().Trim() == "NA" ? "" : abraData.Class_Type.ToString().Trim(),
                RightNameMiddle2 = "License Status : ",

                RightNameResultMiddle2 = (abraData.License_Status == null ? "" : abraData.License_Status.ToString().ToUpper().Trim() == "NA" ? "" :
                abraData.License_Status.ToString().ToUpper().Trim() == "404.2 AWAITING LICENSE" ? "Approved - Pending Issuance" :
                abraData.License_Status.ToString().ToUpper().Trim() == "405.1 NEW CONST" ? "Approved - Pending Issuance" :
                abraData.License_Status.ToString().ToUpper().Trim() == "ACTIVE" ? "Issued" :
                abraData.License_Status.ToString().ToUpper().Trim() == "STIPULATED LICENSE ISSUED" ? "Issued" :
                  abraData.License_Status.ToString().ToUpper().Trim() == "LICENSE ISSUED" ? "Issued" :
                    abraData.License_Status.ToString().ToUpper().Trim() == "ABOUT TO EXPIRE" ? "Renewal Required" :
                abraData.License_Status.ToString().Trim()),
                RightNameBottom = "License Description : ",

                RightNameResultBottom = abraData.B1_APP_TYPE_ALIAS == null ? "" : abraData.B1_APP_TYPE_ALIAS.ToString().ToUpper().Trim() == "NA" ? "" : abraData.B1_APP_TYPE_ALIAS.ToString().Trim().Replace("  ", " ").Replace("  ", " "),

                Expantion1 = "APPLICANT NAME : ",
                ExpantionResult1 =
                    ((abraData.APP_STATUS_GROUP_CODE == null ? "" : abraData.APP_STATUS_GROUP_CODE.ToString().ToUpper().Trim() == "NA" ? "" :
                    abraData.APP_STATUS_GROUP_CODE.ToUpper().Trim() != "ABRA INDIVIDUAL" ?
                (abraData.Applicant_FNAME == null ? "" : abraData.Applicant_FNAME.ToString().ToUpper().Trim() == "NA" ? "" : abraData.Applicant_FNAME.ToString().Trim())
                    + (abraData.Applicant_MNAME == null ? "" : abraData.Applicant_MNAME.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.Applicant_MNAME.ToString().Trim()) +
                     (abraData.Applicant_LNAME == null ? "" : abraData.Applicant_LNAME.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.Applicant_LNAME.ToString().Trim())
                     : abraData.B1_APP_TYPE_ALIAS.ToUpper().Trim() == "LICENSES/ALCOHOL LICENSE/SOLICITOR/NA" ?
                     (abraData.Solicitor_FNAME == null ? "" : abraData.Solicitor_FNAME.ToString().ToUpper().Trim() == "NA" ? "" : abraData.Solicitor_FNAME.ToString().Trim()) +
                     (abraData.Solicitor_MNAME == null ? "" : abraData.Solicitor_MNAME.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.Solicitor_MNAME.ToString().Trim())
                    + (abraData.Solicitor_LNAME == null ? "" : abraData.Solicitor_LNAME.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.Solicitor_LNAME.ToString().Trim()) :
                    abraData.B1_APP_TYPE_ALIAS.ToUpper().Trim() == "LICENSES/ALCOHOL LICENSE/MANAGER/NA" ?
                     (abraData.Manager_FNAME == null ? "" : abraData.Manager_FNAME.ToString().ToUpper().Trim() == "NA" ? "" : abraData.Manager_FNAME.ToString().Trim()) +
                     (abraData.Manager_MNAME == null ? "" : abraData.Manager_MNAME.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.Manager_MNAME.ToString().Trim())
                    + (abraData.Manager_LNAME == null ? "" : abraData.Manager_LNAME.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.Manager_LNAME.ToString().Trim()) :
                      abraData.B1_APP_TYPE_ALIAS.ToUpper().Trim() == "LICENSES/ALCOHOL LICENSE/TEMPORARY/F" ?
                    (abraData.Applicant_FNAME == null ? "" : abraData.Applicant_FNAME.ToString().ToUpper().Trim() == "NA" ? "" : abraData.Applicant_FNAME.ToString().Trim()) +
                     (abraData.Applicant_MNAME == null ? "" : abraData.Applicant_MNAME.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.Applicant_MNAME.ToString().Trim())
                    + (abraData.Applicant_LNAME == null ? "" : abraData.Applicant_LNAME.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.Applicant_LNAME.ToString().Trim()) :
                      abraData.B1_APP_TYPE_ALIAS.ToUpper().Trim() == "LICENSES/ALCOHOL LICENSE/TEMPORARY/G" ?
                  (abraData.Applicant_FNAME == null ? "" : abraData.Applicant_FNAME.ToString().ToUpper().Trim() == "NA" ? "" : abraData.Applicant_FNAME.ToString().Trim()) +
                     (abraData.Applicant_MNAME == null ? "" : abraData.Applicant_MNAME.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.Applicant_MNAME.ToString().Trim())
                    + (abraData.Applicant_LNAME == null ? "" : abraData.Applicant_LNAME.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.Applicant_LNAME.ToString().Trim()) :
                      abraData.B1_APP_TYPE_ALIAS.ToUpper().Trim() == "LICENSES/ALCOHOL LICENSE/PROVIDER TRAINER/NA" ?
                    (abraData.Applicant_FNAME == null ? "" : abraData.Applicant_FNAME.ToString().ToUpper().Trim() == "NA" ? "" : abraData.Applicant_FNAME.ToString().Trim()) +
                     (abraData.Applicant_MNAME == null ? "" : abraData.Applicant_MNAME.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.Applicant_MNAME.ToString().Trim())
                    + (abraData.Applicant_LNAME == null ? "" : abraData.Applicant_LNAME.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.Applicant_LNAME.ToString().Trim()) :
                      abraData.B1_APP_TYPE_ALIAS.ToUpper().Trim() == "LICENSES/ALCOHOL LICENSE/CATERER/NA" ?
                    (abraData.Applicant_FNAME == null ? "" : abraData.Applicant_FNAME.ToString().ToUpper().Trim() == "NA" ? "" : abraData.Applicant_FNAME.ToString().Trim()) +
                     (abraData.Applicant_MNAME == null ? "" : abraData.Applicant_MNAME.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.Applicant_MNAME.ToString().Trim())
                    + (abraData.Applicant_LNAME == null ? "" : abraData.Applicant_LNAME.ToString().ToUpper().Trim() == "NA" ? "" : " " + abraData.Applicant_LNAME.ToString().Trim())
                    : "")).Replace("  ", " "),
                LastUpdateDateName = "Last Retrieved On : ",
                LastUpdateDate = abraData.LAST_UPDATE.ToString()
            }).ToList());
        }
        /// <summary>
        /// This method is used to bind the Quick search for bbl data for the particular entity
        /// </summary>
        /// <param name="sno"></param>
        /// <param name="entityid"></param>
        /// <param name="wishlist"></param>
        /// <param name="bblQueryData"></param>
        /// <returns></returns>
        public async Task<ICollection<CommonData>> GetBblRecords(int sno, int entityid, bool wishlist, ICollection<DCBC_ENTITY_BBL> bblQueryData)
        {
            var bblData1 = new List<CommonData>();
            try
            {
                Parallel.ForEach(bblQueryData, bblData =>
                {
                    //foreach (var bblData in bblQueryData)
                    //{
                    var getbl = new CommonData
                    {
                        id = sno,
                        WishList = wishlist,
                        EntityID = bblData.DCBC_ENTITY_ID,
                        Source = bblData.DCBC_ENTITY_SOURCE.ToUpper().Trim(),
                        SourceFullName = "Business License",
                        LeftNameTop = "Corporation Name :",
                        LeftNameResultTop =
                            bblData.OwnrApplicant_BUSINESS_NAME == null
                                ? ""
                                : bblData.OwnrApplicant_BUSINESS_NAME.ToUpper().Trim() == "NA"
                                    ? ""
                                    : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
                                        bblData.OwnrApplicant_BUSINESS_NAME.ToLower().Trim()),
                        LeftNameMiddle = "Trade Name : ",
                        LeftNameResultMiddle =
                            bblData.Attr_TRADE_NAME == null
                                ? ""
                                : bblData.Attr_TRADE_NAME.ToUpper().Trim() == "NA" ? "" : bblData.Attr_TRADE_NAME.Trim(),

                        LeftNameBottom = "License Category : ",
                        LeftNameResultBottom =
                            bblData.License_Category == null
                                ? ""
                                : bblData.License_Category.ToUpper().Trim() == "NA"
                                    ? ""
                                    : bblData.License_Category.Trim(),
                        LeftNameMiddleLabel1 = "Licensee Name : ",
                        LeftNameMiddle1Text =
                            bblData.OwnrApplicant_FNAME == null
                                ? ""
                                : bblData.OwnrApplicant_FNAME == "NA"
                                    ? ""
                                    : bblData.OwnrApplicant_FNAME == "N/A"
                                        ? ""
                                        : bblData.OwnrApplicant_FNAME.Trim()
                                          +
                                          (bblData.OwnrApplicant_MNAME == null
                                              ? ""
                                              : " " +
                                                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
                                                    bblData.OwnrApplicant_MNAME.ToLower().Trim())) +
                                          (bblData.OwnrApplicant_LNAME == null
                                              ? ""
                                              : " " +
                                                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
                                                    bblData.OwnrApplicant_LNAME.ToLower().Trim())),
                        MiddleNameTop = "Premise Address : ",
                        MiddleNameResultTop =
                            ((bblData.B1_HSE_NBR_START == null
                                ? ""
                                : bblData.B1_HSE_NBR_START.ToString().ToUpper().Trim() == "NA"
                                    ? ""
                                    : bblData.B1_HSE_NBR_START.ToString().ToUpper().Trim()) +
                             (bblData.B1_HSE_NBR_END == null
                                 ? ""
                                 : bblData.B1_HSE_NBR_END.ToString().ToUpper().Trim() == "NA"
                                     ? ""
                                     : "-" + bblData.B1_HSE_NBR_END.ToString().ToUpper().Trim()) +
                             (bblData.B1_HSE_FRAC_NBR_START == null
                                 ? ""
                                 : bblData.B1_HSE_FRAC_NBR_START.ToUpper().Trim() == "NA"
                                     ? ""
                                     : " " + bblData.B1_HSE_FRAC_NBR_START.ToString().ToUpper().Trim()) +
                             (bblData.B1_HSE_FRAC_NBR_END == null
                                 ? ""
                                 : bblData.B1_HSE_FRAC_NBR_END.ToUpper().Trim() == "NA"
                                     ? ""
                                     : " " + bblData.B1_HSE_FRAC_NBR_END.ToUpper().Trim()) +
                             (bblData.B1_UNIT_START == null
                                 ? ""
                                 : bblData.B1_UNIT_START.ToUpper().Trim() == "NA"
                                     ? ""
                                     : " Suite#" + bblData.B1_UNIT_START.ToUpper().Trim()) +
                             (bblData.B1_STR_NAME == null
                                 ? ""
                                 : bblData.B1_STR_NAME.ToUpper().Trim() == "NA"
                                     ? ""
                                     : " " + " " + bblData.B1_STR_NAME.ToUpper().ToUpper().Trim()) +
                             (bblData.B1_STR_SUFFIX == null
                                 ? ""
                                 : bblData.B1_STR_SUFFIX.ToUpper().Trim() == "NA"
                                     ? ""
                                     : " " + bblData.B1_STR_SUFFIX.ToUpper().Trim()) +
                             (bblData.B1_STR_SUFFIX_DIR == null
                                 ? ""
                                 : bblData.B1_STR_SUFFIX_DIR.ToUpper().Trim() == "NA"
                                     ? ""
                                     : " " + bblData.B1_STR_SUFFIX_DIR.ToUpper().Trim()) +
                             (bblData.B1_SITUS_CITY == null
                                 ? ""
                                 : bblData.B1_SITUS_CITY.ToUpper().Trim() == "NA"
                                     ? ""
                                     : " " + bblData.B1_SITUS_CITY.ToUpper().Trim()) +
                             (bblData.B1_SITUS_STATE == null
                                 ? ""
                                 : bblData.B1_SITUS_STATE.ToUpper().Trim() == "NA"
                                     ? ""
                                     : " " + bblData.B1_SITUS_STATE.ToUpper().Trim()) +
                             (bblData.B1_SITUS_ZIP == null
                                 ? ""
                                 : bblData.B1_SITUS_ZIP.ToUpper().Trim() == "NA"
                                     ? ""
                                     : " " + bblData.B1_SITUS_ZIP.ToUpper().Trim())).Replace("  ", " ")
                                .Replace("  ", " "),
                        RightNameTop = "License Number : ",
                        RightNameResultTop =
                            bblData.B1_ALT_ID == null
                                ? ""
                                : bblData.B1_ALT_ID.ToUpper().Trim() == "NA" ? "" : bblData.B1_ALT_ID.Trim(),
                        RightNameMiddle1 = "License Expiration Date : ",
                        RightNameResultMiddle1 = bblData.Expiration_Date == null ? "" : bblData.Expiration_Date.Trim(),
                        RightNameMiddle2 = "License Status : ",
                        RightNameResultMiddle2 =
                            (bblData.B1_APPL_STATUS == null
                                ? ""
                                : bblData.B1_APPL_STATUS.ToUpper().Trim() == "NA"
                                    ? ""
                                    : bblData.B1_APPL_STATUS.ToUpper().Trim() == "READY TO RENEW"
                                        ? "Active"
                                        : bblData.B1_APPL_STATUS.ToUpper().Trim() == "READY TO BATCH PRINT"
                                            ? "Active"
                                            : bblData.B1_APPL_STATUS.Trim()),
                        //  RightNameBottom = "",
                        RightNameResultBottom = string.Empty,

                        LastUpdateDateName = "Last Retrieved On : ",
                        LastUpdateDate = bblData.LAST_UPDATE == null ? "" : bblData.LAST_UPDATE.ToString(),
                        Expantion1 = "License Endorsement/Category",
                        ExpantionResult1 =
                            bblData.License_Category_Full == null
                                ? ""
                                : bblData.License_Category_Full.ToUpper().Trim() == "NA"
                                    ? ""
                                    : bblData.License_Category_Full.Trim()
                    };
                    bblData1.Add(getbl);
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occurs in Get Bbl Records",ex);
            }
            return await Task.FromResult( bblData1);
        }
        /// <summary>
        /// This method is used to bind the Quick search for cbe data for the particular entity
        /// </summary>
        /// <param name="sno"></param>
        /// <param name="entityid"></param>
        /// <param name="wishlist"></param>
        /// <param name="cbeQueryData"></param>
        /// <returns></returns>
        public async Task<ICollection<CommonData>> GetCbeRecords(int sno, int entityid, bool wishlist, ICollection<DCBC_ENTITY_CBE> cbeQueryData)
        {
            var cbeData1 = new List<CommonData>();
            try
            {
                Parallel.ForEach(cbeQueryData, cbeData =>
                {
                    //foreach (var cbeData in cbeQueryData)
                    //{
                    var getcbe = new CommonData
                    {
                        id = sno,
                        WishList = wishlist,
                        EntityID = cbeData.DCBC_ENTITY_ID,
                        Source = cbeData.DCBC_ENTITY_SOURCE.ToUpper().Trim(),
                        SourceFullName = "Certified Business Enterprise",
                        LeftNameTop = "Company Name : ",
                        LeftNameResultTop =
                            cbeData.BusinessName == null
                                ? ""
                                : cbeData.BusinessName.ToUpper().Trim() == "NA"
                                    ? ""
                                    : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
                                        cbeData.BusinessName.ToLower().Trim()),
                        LeftNameMiddle = "CBE Number : ",
                        LeftNameResultMiddle =
                            cbeData.LSDBE_Number == null
                                ? ""
                                : cbeData.LSDBE_Number.ToUpper().Trim() == "NA" ? "" : cbeData.LSDBE_Number.Trim(),
                        LeftNameBottom = "Preference Points : ",
                        LeftNameResultBottom =
                            (cbeData.RefPoints == null
                                ? ""
                                : cbeData.RefPoints.ToString().ToUpper().Trim() == "NA"
                                    ? ""
                                    : cbeData.RefPoints.ToString().Trim()),
                        MiddleNameTop = "Business Address : ",
                        MiddleNameResultTop =
                            (cbeData.BusinessAddress == null
                                ? ""
                                : cbeData.BusinessAddress.ToUpper().Trim() == "NA" ? "" : cbeData.BusinessAddress.Trim())
                                .Replace("  ", " ").Replace("  ", " "),
                        RightNameMiddle1 = "Expiration Date : ",
                        RightNameResultMiddle1 = cbeData.CertificationExpiry.ToString().Trim(),

                        RightNameBottom = "Organization Type : ",
                        RightNameResultBottom =
                            cbeData.BusinessStructure == null
                                ? ""
                                : cbeData.BusinessStructure.ToUpper().Trim() == "NA"
                                    ? ""
                                    : cbeData.BusinessStructure.Trim(),
                        Expantion1 = "COMPANY DESCRIPTION : ",
                        ExpantionResult1 =
                            cbeData.BusinessDescription == null
                                ? ""
                                : cbeData.BusinessDescription.ToUpper().Trim() == "NA"
                                    ? ""
                                    : cbeData.BusinessDescription.Trim(),

                        Expantion2 = "PREFERENCE POINTS DETAIL : ",
                        ExpantionResult2 =
                            (cbeData.RefPointsDesc == null
                                ? ""
                                : cbeData.RefPointsDesc.ToUpper().Trim() == "NA" ? "" : cbeData.RefPointsDesc.Trim())
                                .Replace("  ", " "),
                        Expantion3 = "BUSINESS CONTACT : ",
                        ExpantionResult3 =
                            cbeData.BusinessContact == null
                                ? ""
                                : cbeData.BusinessContact.ToUpper().Trim() == "NA" ? "" : cbeData.BusinessContact.Trim(),
                        Expantion4 = "BUSINESS PHONE : ",
                        ExpantionResult4 =
                            cbeData.BusinessPhone == null
                                ? ""
                                : cbeData.BusinessPhone.ToUpper().Trim() == "NA" ? "" : cbeData.BusinessPhone.Trim(),
                        Expantion5 = "BUSINESS EMAIL : ",
                        ExpantionResult5 =
                            cbeData.BusinessEmail == null
                                ? ""
                                : cbeData.BusinessEmail.ToUpper().Trim() == "NA" ? "" : cbeData.BusinessEmail.Trim(),
                        LastUpdateDateName = "Last Retrieved On : ",
                        LastUpdateDate = cbeData.LAST_UPDATE.ToString()
                    };
                    cbeData1.Add(getcbe);
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occurs in Get CBE Records",ex);
            }
            return await Task.FromResult(cbeData1);
        }
        /// <summary>
        /// This method is used to bind the Quick search for corp data for the particular entity
        /// </summary>
        /// <param name="sno"></param>
        /// <param name="entityid"></param>
        /// <param name="wishlist"></param>
        /// <param name="corp"></param>
        /// <returns></returns>
        public async Task<ICollection<CommonData>> GetCorpRecords(int sno, int entityid, bool wishlist, ICollection<DCBC_ENTITY_CORP> corp)
        {
            try
            {
                var corpfields = (from corpData in corp
                                  select new
                                  {
                                      corpData.DCBC_ENTITY_ID,
                                      corpData.REC_STATUS,
                                      corpData.LAST_UPDATE,
                                      corpData.DCBC_ENTITY_SOURCE,
                                      corpData.BusinessName,
                                      corpData.Suffix,
                                      corpData.Locale,
                                      corpData.ModelType,
                                      corpData.EntityStatus,
                                      corpData.EntityStatusDate,
                                      corpData.BusniessAddressLine1,
                                      corpData.BusniessAddressLine2,
                                      corpData.BusniessAddressLine3,
                                      corpData.BusniessAddressLine4,
                                      corpData.BusinessCity,
                                      corpData.BusinessState,
                                      corpData.ZipCode,
                                      corpData.EffectiveDate,
                                      corpData.NextReportYear,
                                      corpData.FileNumber
                                  });

                return await Task.FromResult(corpfields.Select(corpData => new CommonData
                {
                    id = sno,
                    WishList = wishlist,
                    EntityID = corpData.DCBC_ENTITY_ID,
                    Source = corpData.DCBC_ENTITY_SOURCE.ToUpper().Trim(),
                    SourceFullName = "Corporate Registration",
                    LeftNameTop = "Entity Name : ",

                    LeftNameResultTop = (corpData.BusinessName.ToString().ToUpper().Trim() == "NA" ? "" : corpData.BusinessName == null ? "" :
                        corpData.BusinessName.ToString().Trim()) +
                         (corpData.BusinessName.Length == 0 || corpData.Suffix == null ? "" : corpData.Suffix.Length == 0 ? "" : ", ") +
                    (corpData.Suffix == null ? "" : corpData.Suffix.ToString().ToUpper().Trim() == "LEGACY NO DATA" ? "" : corpData.Suffix.Length == 0 ? "" :
                    corpData.Suffix.ToString().Trim().Trim()).ToUpper(),

                    LeftNameMiddle = "File Number : ",
                    LeftNameResultMiddle = corpData.FileNumber.ToString().Trim() == null ? "" : corpData.FileNumber.ToString().ToUpper().Trim() == "NA" ? "" : corpData.FileNumber.ToUpper().ToString().Trim(),
                    LeftNameBottom = "Next Report Year : ",
                    LeftNameResultBottom = corpData.NextReportYear.ToUpper().Trim() ?? "",
                    MiddleNameTop = "Entity Address : ",
                    MiddleNameResultTop =
                    (
                    (corpData.BusniessAddressLine1 == null ? "" : corpData.BusniessAddressLine1.ToString().ToUpper().Trim() == "LEGACY NO DATA" ? "" : corpData.BusniessAddressLine1.ToString().Trim()) +
                    (corpData.BusniessAddressLine2 == null ? "" : corpData.BusniessAddressLine2.ToString().ToUpper().Trim() == "LEGACY NO DATA" ? "" : " " + corpData.BusniessAddressLine2.ToString().Trim()) +
                    (corpData.BusniessAddressLine3 == null ? "" : corpData.BusniessAddressLine3.ToString().ToUpper().Trim() == "LEGACY NO DATA" ? "" : " " + corpData.BusniessAddressLine3.ToString().Trim()) +
                    (corpData.BusniessAddressLine4 == null ? "" : corpData.BusniessAddressLine4.ToString().ToUpper().Trim() == "LEGACY NO DATA" ? "" : " " + corpData.BusniessAddressLine4.ToString().Trim()) +
                    (corpData.BusinessCity == null ? "" : corpData.BusinessCity.ToString().ToUpper().Trim() == "LEGACY NO DATA" ? "" : " " + corpData.BusinessCity.ToString().Trim())
                    + (corpData.BusinessState == null ? "" : corpData.BusinessState.ToString().ToUpper().Trim() == "LEGACY NO DATA" ? "" : " " + corpData.BusinessState.ToString().Trim())
                    + (corpData.ZipCode == null ? "" : corpData.ZipCode.ToString().ToUpper().Trim() == "LEGACY NO DATA" ? "" : corpData.ZipCode.ToString().Trim())
                    ).Replace("  ", " ").Replace("  ", " "),
                    RightNameTop = "Model Type : ",
                    RightNameResultTop = corpData.ModelType.ToString().Trim() == null ? "" : corpData.ModelType.ToString().ToUpper().Trim() == "LEGACY NO DATA" ? "" : corpData.ModelType.ToString().ToUpper().Trim(),
                    RightNameMiddle1 = "",
                    RightNameResultMiddle1 = string.Empty,
                    RightNameMiddle2 = "Entity Status : ",
                    RightNameResultMiddle2 = corpData.EntityStatus.ToUpper().Trim() ?? "",
                    LastUpdateDateName = "Last Retrieved On : ",
                    LastUpdateDate = corpData.LAST_UPDATE.ToString().Trim() == null ? "" : corpData.LAST_UPDATE.ToString().ToUpper().Trim() == "NA" ? "" : corpData.LAST_UPDATE.ToString(),
                    Expantion1 = "Effective Date : ",
                    ExpantionResult1 = corpData.EffectiveDate.ToString().Trim() == null ? "" : corpData.EffectiveDate.ToString().ToUpper().Trim() == "NA" ? "" : corpData.EffectiveDate.ToString().Trim(),
                    Expantion2 = "Locale : ",
                    ExpantionResult2 = corpData.Locale.ToString().Trim() == null ? "" : corpData.Locale.ToString().ToUpper().Trim() == "LEGACY NO DATA" ? "" : corpData.Locale.ToString().ToUpper().Trim()
                }).ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occurs in Get Corp Data",ex);
            }
        }
        /// <summary>
        /// This method is used to bind the Quick search for opla data for the particular entity
        /// </summary>
        /// <param name="sno"></param>
        /// <param name="entityid"></param>
        /// <param name="wishlist"></param>
        /// <param name="oplaQueryData"></param>
        /// <returns></returns>
        public async Task<ICollection<CommonData>> GetOplaRecords(int sno, int entityid, bool wishlist, ICollection<DCBC_ENTITY_OPLA> oplaQueryData)
        {
            var oplaData1 = new List<CommonData>();
            //var opla = (from data in oplaQueryData.AsNoTracking()
            //            where data.DCBC_ENTITY_ID == entityid
            //            select data);
            //foreach (var oplaData in oplaQueryData)
            //{
            Parallel.ForEach(oplaQueryData, oplaData =>
            {
                var getOpla = new CommonData
                {
                    id = sno,
                    WishList = wishlist,
                    EntityID = oplaData.DCBC_ENTITY_ID,
                    Source = oplaData.DCBC_ENTITY_SOURCE.ToUpper().Trim(),
                    SourceFullName = "Professional License",
                    LeftNameTop = "Licensee Name : ",
                    LeftNameResultTop =
                        (oplaData.Licensee_Name_1 == null
                            ? ""
                            : oplaData.Licensee_Name_1.ToUpper().Trim() == "NA"
                                ? ""
                                : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
                                    oplaData.Licensee_Name_1.ToLower().Trim())) +
                        (oplaData.Licensee_Name_2 == null
                            ? ""
                            : oplaData.Licensee_Name_2.ToUpper().Trim() == "NA"
                                ? ""
                                : " " +
                                  CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
                                      oplaData.Licensee_Name_2.ToLower().Trim())) +
                        (oplaData.Licensee_Name_3 == null
                            ? ""
                            : oplaData.Licensee_Name_3.ToUpper().Trim() == "NA"
                                ? ""
                                : " " +
                                  CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
                                      oplaData.Licensee_Name_3.ToLower().Trim())) +
                        (oplaData.Licensee_Name_4 == null
                            ? ""
                            : oplaData.Licensee_Name_4.ToUpper().Trim() == "NA"
                                ? ""
                                : oplaData.Licensee_Name_4.Length == 0
                                    ? ""
                                    : ", " +
                                      CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
                                          oplaData.Licensee_Name_4.ToLower().Trim()) + "."),
                    LeftNameMiddle = "License Number : ",
                    LeftNameResultMiddle =
                        (oplaData.LICENSE_PREFIX == null
                            ? ""
                            : oplaData.LICENSE_PREFIX.ToUpper().Trim() == "NA"
                                ? ""
                                : oplaData.LICENSE_PREFIX.Trim())
                        +
                        (oplaData.LICENSE_NUMBER == null
                            ? ""
                            : oplaData.LICENSE_NUMBER.ToUpper().Trim() == "NA"
                                ? ""
                                : oplaData.LICENSE_NUMBER.Trim()),
                    MiddleNameTop = "Address : ",
                    MiddleNameResultTop =
                        ((oplaData.BUSINESS_CITY == null
                            ? ""
                            : oplaData.BUSINESS_CITY.ToUpper().Trim() == "NA"
                                ? ""
                                : oplaData.BUSINESS_CITY.Trim()) +
                         (oplaData.BUSINESS_STATE == null
                             ? ""
                             : oplaData.BUSINESS_STATE.ToUpper().Trim() == "NA"
                                 ? ""
                                 : " " + oplaData.BUSINESS_STATE.Trim())
                         +
                         (oplaData.BUSINESS_ZIP == null
                             ? ""
                             : oplaData.BUSINESS_ZIP.ToUpper().Trim() == "NA"
                                 ? ""
                                 : " " + oplaData.BUSINESS_ZIP.Trim())).Replace("  ", " ").Replace("  ", " "),
                    RightNameTop = "Expiration Date : "
                };

                if (oplaData.LICENSE_EXPIRATION_DATE != null)
                    getOpla.RightNameResultTop = String.Format("{0:MM/dd/yyyy}", oplaData.LICENSE_EXPIRATION_DATE);

                getOpla.RightNameMiddle2 = "License Status : ";
                getOpla.RightNameResultMiddle2 = oplaData.LICENSE_STATUS_CODE == null
                    ? ""
                    : oplaData.LICENSE_STATUS_CODE.ToUpper().Trim() == "NA"
                        ? ""
                        : oplaData.LICENSE_STATUS_CODE.ToUpper().Trim() == "A" ? "Active" : "Unknown";
                getOpla.Expantion1 = "LICENSE BOARD : ";
                getOpla.ExpantionResult1 = oplaData.License_Board == null
                    ? ""
                    : oplaData.License_Board.ToUpper().Trim() == "NA" ? "" : oplaData.License_Board.Trim();

                getOpla.LeftNameMiddleLabel1 = "License Type : ";
                getOpla.LeftNameMiddle1Text = oplaData.License_Type == null
                    ? ""
                    : oplaData.License_Type.ToUpper().Trim() == "NA"
                        ? ""
                        : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(oplaData.License_Type.ToLower().Trim());

                getOpla.LastUpdateDateName = "Last Retrieved On : ";
                getOpla.LastUpdateDate = oplaData.LAST_UPDATE.ToString();
                oplaData1.Add(getOpla);
            });

            return await Task.FromResult(oplaData1);
        }
        /// <summary>
        /// Get record count
        /// </summary>
        /// <param name="quickSearchConditionFilterTable"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SearchDataViewModel>> GetCountRecords(QuickSearchConditionFilterTable quickSearchConditionFilterTable)
        {
            int abracount = quickSearchConditionFilterTable.AbraEntityTable.Count();
            int bblcount = quickSearchConditionFilterTable.BblEntityTable.Count();
            int cbecount = quickSearchConditionFilterTable.CbeEntityTable.Count();
            int corpcount = quickSearchConditionFilterTable.CorpEntityTable.Count();
            int oplacount = quickSearchConditionFilterTable.OplaEntityTable.Count();
            var dataViewModel = new SearchDataViewModel
            {
                ABRACount = abracount.ToString(),
                BBLCount = bblcount.ToString(),
                CBECount = cbecount.ToString(),
                CORPCount = corpcount.ToString(),
                OPLACount = oplacount.ToString(),

                RecordCount = (abracount + bblcount + cbecount + corpcount + oplacount).ToString()
            };

            var final = new List<SearchDataViewModel> { dataViewModel };

            return await Task.FromResult(final);
        }
    }
    /// <summary>
    /// Declare for quicksearchconditionfilter for quick search
    /// </summary>
    public class QuickSearchConditionFilterTable
    {
        public virtual ICollection<DCBC_ENTITY_BBL> BblEntityTable { get; set; }
        public virtual ICollection<DCBC_ENTITY_ABRA> AbraEntityTable { get; set; }
        public virtual ICollection<DCBC_ENTITY_CBE> CbeEntityTable { get; set; }
        public virtual ICollection<DCBC_ENTITY_CORP> CorpEntityTable { get; set; }
        public virtual ICollection<DCBC_ENTITY_OPLA> OplaEntityTable { get; set; }
    }
}