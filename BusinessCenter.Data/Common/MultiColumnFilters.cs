using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Common
{
    public class MultiColumnFilters
    {
        private SearchDataViewModel _dataViewModel;

        //  public  int listcount = 0;
        public int abracount = 0;

        public int bblcount = 0;
        public int cbecount = 0;
        public int corpcount = 0;
        public int oplacount = 0;

        public int abrarecords = 0;
        public int bblrecords = 0;
        public int cberecords = 0;
        public int corprecords = 0;
        public int oplarecords = 0;

        public string _ExcededBbl = string.Empty;
        public string _ExcededOpla = string.Empty;
        public string _ExcededCorp = string.Empty;
        public string _ExcededAbra = string.Empty;
        public string _ExcededCbp = string.Empty;
        public int _ExcededCount = 0;
        public ICollection<CommonData> dataaa;

        #region serachresults

        /// <summary>
        /// get data from dcbc_entity_multicolumn_lookup_index and filter using by seach inputs , get data from appropriate table
        /// data , get count of regulatory enities and get list of data
        /// take top 100 most regulatory of the each regulatory .
        /// </summary>
        /// <param name="searchinput"></param>
        /// <param name="context"></param>
        /// <param name="multicolumn"></param>
        /// <param name="userServiceData"></param>
        public async Task<ICollection<CommonData>> GetFilterData(SearchServiceInputs searchinput,

             IEnumerable<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> multicolumn, List<UserService> userServiceData, IAbraRepository abrarep
             , IBblRepository bblrep, ICbeRepository cberep, ICorpRespository corprep, IOplaRepository oplarep)
        {
            try
            {
                if (searchinput.CompanyName.ToLower() != "" ||
                    searchinput.LicenseName.ToLower() != "" ||
                    searchinput.FirstName.ToLower() != "" ||
                    searchinput.LastName.ToLower() != "")
                {
                    var searchFilterResultData = (from x in
                                                      multicolumn.AsQueryable().AsNoTracking().WithCompanyName(searchinput.CompanyName.ToLower()).
                                                          WithLicenseName(searchinput.LicenseName.ToLower())
                                                          .WithFirstName(searchinput.FirstName.ToLower())
                                                          .WithLastName(searchinput.LastName.ToLower())
                                                  select x).OrderBy(x => x.CompanyNameOrig)
                        .ThenBy(x => x.LastNameOrig)
                        .ThenBy(x => x.FirstNameOrig)
                         .ThenBy(x => x.LicenseNumberOrig)
                        .AsNoTracking().ToList();

                    var searchFilterResultDataContainsNull =
                        searchFilterResultData.Where(a => a.FirstNameOrig == string.Empty && a.LastNameLookup == string.Empty)
                        .OrderBy(x => x.CompanyNameOrig)
                        .ThenBy(x => x.LastNameOrig)
                            .ThenBy(x => x.FirstNameOrig)
                            .ThenBy(x => x.LicenseNumberOrig)
                           .AsQueryable().AsNoTracking();

                    var searchFilterResultDataContainsNotNull =
                        searchFilterResultData.Where(a => a.FirstNameOrig != string.Empty && a.LastNameLookup != string.Empty)
                         .OrderBy(x => x.CompanyNameOrig)
                        .ThenBy(x => x.LastNameOrig)
                            .ThenBy(x => x.FirstNameOrig)
                            .ThenBy(x => x.LicenseNumberOrig)
                          .AsQueryable().AsNoTracking();

                    var totalfilterData = searchFilterResultDataContainsNotNull.Concat(searchFilterResultDataContainsNull);

                    var addDataToSearhResult = new List<CommonData>();

                    var distinctcount = (from searchcount in totalfilterData select new { searchcount.DCBC_ENTITY_ID, searchcount.DATA_SOURCE }).AsNoTracking().Distinct();

                    var searchConditionFilterData = await QuickSearchConditionFilterTable(searchinput, totalfilterData, bblrep, abrarep, corprep, oplarep, cberep);
                   // int count = 0;

                    var searchTablesFilterData = new MultiColumnLookUpFilters();

                    abrarecords = 100;
                    bblrecords = 100;
                    cberecords = 100;
                    corprecords = 100;
                    oplarecords = 100;

                    abracount = 0;
                    bblcount = 0;
                    cbecount = 0;
                    corpcount = 0;
                    oplacount = 0;
                    _ExcededCount = 0;
                    _ExcededBbl = string.Empty;
                    _ExcededOpla = string.Empty;
                    _ExcededCorp = string.Empty;
                    _ExcededAbra = string.Empty;
                    _ExcededCbp = string.Empty;
                    int sno = 1;
                    try
                    {
                        foreach (var data in distinctcount)
                        {
                            if (abracount <= 101 || bblcount <= 101 || cbecount <= 101 || corpcount <= 101 || oplacount <= 101)
                            {
                                var wishlist = false;
                                if (userServiceData.Count != 0)
                                {
                                    wishlist = MySavedList(data.DCBC_ENTITY_ID, userServiceData);
                                }
                                switch (data.DATA_SOURCE.ToUpper())
                                {
                                    case "ABRA":
                                        if (abracount < abrarecords)
                                        {
                                            var abra = await
                                                searchTablesFilterData.GetAbraRecords(sno, data.DCBC_ENTITY_ID, wishlist, searchConditionFilterData.AbraEntityTable.Where(x => x.DCBC_ENTITY_ID == data.DCBC_ENTITY_ID).ToList());

                                            if (abra.Any())
                                            {
                                                addDataToSearhResult.AddRange(abra);
                                                abracount = abracount + 1;
                                                sno = sno + 1;
                                            }
                                        }
                                        else
                                        {
                                            _ExcededAbra = ",Alcoholic Beverage License";
                                            _ExcededCount = 1;
                                        }

                                        break;

                                    case "BBL":

                                        if (bblcount < bblrecords)
                                        {
                                            var bbl = await
                                                searchTablesFilterData.GetBblRecords(sno, data.DCBC_ENTITY_ID, wishlist, searchConditionFilterData.BblEntityTable.Where(x => x.DCBC_ENTITY_ID == data.DCBC_ENTITY_ID).ToList());

                                            if (bbl.Any())
                                            {
                                                addDataToSearhResult.AddRange(bbl);
                                                bblcount = bblcount + 1;
                                                sno = sno + 1;
                                            }
                                        }
                                        else
                                        {
                                            _ExcededBbl = ",Business License";
                                            _ExcededCount = 1;
                                        }
                                        break;

                                    case "CBE":
                                        if (cbecount < cberecords)
                                        {
                                            var cbe = await searchTablesFilterData.GetCbeRecords(sno, data.DCBC_ENTITY_ID, wishlist, searchConditionFilterData.CbeEntityTable.Where(x => x.DCBC_ENTITY_ID == data.DCBC_ENTITY_ID).ToList());
                                            if (cbe.Any())
                                            {
                                                addDataToSearhResult.AddRange(cbe);
                                                cbecount = cbecount + 1;
                                                sno = sno + 1;
                                            }
                                        }
                                        else
                                        {
                                            _ExcededCbp = ",Certified Business Enterprise";
                                            _ExcededCount = 1;
                                        }
                                        break;

                                    case "CORP":
                                        if (corpcount < corprecords)
                                        {
                                            var corp = await searchTablesFilterData.GetCorpRecords(sno, data.DCBC_ENTITY_ID,
                                                wishlist, searchConditionFilterData.CorpEntityTable.Where(x => x.DCBC_ENTITY_ID == data.DCBC_ENTITY_ID).ToList());

                                            if (corp.Any())
                                            {
                                                addDataToSearhResult.AddRange(
                                                  corp);
                                                corpcount = corpcount + 1;
                                                sno = sno + 1;
                                            }
                                        }
                                        else
                                        {
                                            _ExcededCorp = ",Corporate Registration";
                                            _ExcededCount = 1;
                                        }
                                        break;

                                    case "OPLA":
                                        if (oplacount < oplarecords)
                                        {
                                            var opla = await searchTablesFilterData.GetOplaRecords(sno, data.DCBC_ENTITY_ID, wishlist, searchConditionFilterData.OplaEntityTable.Where(x => x.DCBC_ENTITY_ID == data.DCBC_ENTITY_ID).ToList());

                                            if (opla.Count > 0)
                                            {
                                                addDataToSearhResult.AddRange(opla);
                                                oplacount = oplacount + 1;
                                                sno = sno + 1;
                                            }
                                        }
                                        else
                                        {
                                            _ExcededOpla = ",Professional License";
                                            _ExcededCount = 1;
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    { throw ex; }

                    dataaa = addDataToSearhResult.ToList();
                }
                else
                {
                    abracount = 0;
                    bblcount = 0;
                    cbecount = 0;
                    corpcount = 0;
                    oplacount = 0;
                    _ExcededCount = 0;
                    _ExcededBbl = string.Empty;
                    _ExcededOpla = string.Empty;
                    _ExcededCorp = string.Empty;
                    _ExcededAbra = string.Empty;
                    _ExcededCbp = string.Empty;
                    //var filter = from multicol in multicolumn.AsQueryable().AsNoTracking()
                    //             where multicol.CompanyNameLookup == "" && multicol.LicenseNumberLookup == ""
                    //                 && multicol.FirstNameLookup == "" && multicol.LastNameLookup == ""
                    //             select multicol;
                    dataaa = null;
                }
                return await Task.FromResult( dataaa.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occus in Get Filter Data",ex);
            }
        }

        public async Task<QuickSearchConditionFilterTable> QuickSearchConditionFilterTable(
           SearchServiceInputs searchInput, IEnumerable<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> filterMulticolumnIndexTable,
            IBblRepository bblrep, IAbraRepository abrarep, ICorpRespository corpRepositoryData, IOplaRepository oplaRepositoryData, ICbeRepository cbeRepositoryData)
        {
            var filterMulticolumnIndexData =
     (from searchcount in filterMulticolumnIndexTable
      select new { searchcount.DCBC_ENTITY_ID, searchcount.DATA_SOURCE }).Distinct();

            var bindQuickSearchTables = new QuickSearchConditionFilterTable();

            try
            {
                var searchCheckedList = searchInput.SearchType.Split('-');

                foreach (var checkedItem in searchCheckedList)
                {
                    switch (checkedItem.Trim().ToUpper())
                    {
                        case "BBL":
                            bindQuickSearchTables.BblEntityTable = null;
                            var getBblEntityList =
                                (from x in
                                     filterMulticolumnIndexData.Where(x => x.DATA_SOURCE.ToUpper().Trim() == "BBL")
                                 select x.DCBC_ENTITY_ID).ToList();
                            bindQuickSearchTables.BblEntityTable =
                                bblrep.FindBy(x => (x.B1_APPL_STATUS.ToUpper().Trim() == "ACTIVE"
                                                    || x.B1_APPL_STATUS.ToUpper().Trim() == "READY TO RENEW" ||
                                                    x.B1_APPL_STATUS.ToUpper().Trim()
                                                    == "READY TO BATCH PRINT") &&
                                                   (getBblEntityList.Contains(x.DCBC_ENTITY_ID)))
                                                .Take(101).ToList();

                            break;

                        case "CORP":
                            bindQuickSearchTables.CorpEntityTable = null;
                            var getCorpEntityList =
                                (from x in
                                     filterMulticolumnIndexData.Where(x => x.DATA_SOURCE.ToUpper().Trim() == "CORP")
                                 select x.DCBC_ENTITY_ID).ToList();
                            //  FindBy
                            bindQuickSearchTables.CorpEntityTable =
                                corpRepositoryData.FindBy(x => x.BusinessName != null &&
                                                               x.EntityStatus.ToUpper()
                                                                   .Trim() !=
                                                               "DEPRECATE"
                                                               &&
                                                               x.EntityStatus.ToUpper()
                                                                   .Trim() !=
                                                               "MIGRATEDNODATA" &&
                                                               x.EntityStatus.ToUpper()
                                                                   .Trim() != "OLDACT" &&
                                                               x.EntityStatus.ToUpper()
                                                                   .Trim() != "PENDING"
                                                               &&
                                                               x.EntityStatus.ToUpper()
                                                                   .Trim() != "REDEEMED" &&
                                                               (getCorpEntityList
                                                                   .Contains(
                                                                       x.DCBC_ENTITY_ID)))
                                    .Take(101)
                                    .ToList();

                            break;

                        case "OPLA":
                            bindQuickSearchTables.OplaEntityTable = null;
                            var getOplaEntityList =
                                (from x in
                                     filterMulticolumnIndexData.Where(x => x.DATA_SOURCE.ToUpper().Trim() == "OPLA")
                                 select x.DCBC_ENTITY_ID).ToList();
                            bindQuickSearchTables.OplaEntityTable =
                                oplaRepositoryData.FindBy(x => getOplaEntityList.Contains(x.DCBC_ENTITY_ID))
                                    .Take(101)
                                    .ToList();

                            break;

                        case "CBE":
                            bindQuickSearchTables.CbeEntityTable = null;
                            var getCbeEntityList =
                                (from x in
                                     filterMulticolumnIndexData.Where(x => x.DATA_SOURCE.ToUpper().Trim() == "CBE")
                                 select x.DCBC_ENTITY_ID).ToList();
                            bindQuickSearchTables.CbeEntityTable =
                                (cbeRepositoryData.FindBy(x => getCbeEntityList.Contains(x.DCBC_ENTITY_ID))).Take(101)
                                    .ToList();
                            break;

                        case "ABRA":
                            bindQuickSearchTables.AbraEntityTable = null;
                            var getAbraEntityList =
                                (from x in
                                     filterMulticolumnIndexData.Where(x => x.DATA_SOURCE.ToUpper().Trim() == "ABRA")
                                 select x.DCBC_ENTITY_ID).ToList();
                            bindQuickSearchTables.AbraEntityTable =
                                (abrarep.FindBy(x => getAbraEntityList.Contains(x.DCBC_ENTITY_ID))).Take(101).ToList();

                            break;

                        case "ALL":
                            bindQuickSearchTables.CbeEntityTable = null;
                            bindQuickSearchTables.BblEntityTable = null;
                            bindQuickSearchTables.CorpEntityTable = null;
                            bindQuickSearchTables.OplaEntityTable = null;
                            bindQuickSearchTables.AbraEntityTable = null;

                            var getBblEntityListAll =
                                (from x in
                                     filterMulticolumnIndexData.Where(x => x.DATA_SOURCE.ToUpper().Trim() == "BBL")
                                 select x.DCBC_ENTITY_ID).ToList();
                            var getOplaEntityListAll =
                                (from x in
                                     filterMulticolumnIndexData.Where(x => x.DATA_SOURCE.ToUpper().Trim() == "OPLA")
                                 select x.DCBC_ENTITY_ID).ToList();
                            var getAbraEntityListAll =
                                (from x in
                                     filterMulticolumnIndexData.Where(x => x.DATA_SOURCE.ToUpper().Trim() == "ABRA")
                                 select x.DCBC_ENTITY_ID).ToList();
                            var getCbeEntityListAll =
                                (from x in
                                     filterMulticolumnIndexData.Where(x => x.DATA_SOURCE.ToUpper().Trim() == "CBE")
                                 select x.DCBC_ENTITY_ID).ToList();
                            var getCorpEntityListALL =
                                (from x in
                                     filterMulticolumnIndexData.Where(x => x.DATA_SOURCE.ToUpper().Trim() == "CORP")
                                 select x.DCBC_ENTITY_ID).ToList();

                            bindQuickSearchTables.BblEntityTable =
                                bblrep.FindBy(x => (x.B1_APPL_STATUS.ToUpper().Trim() == "ACTIVE"
                                                    || x.B1_APPL_STATUS.ToUpper().Trim() == "READY TO RENEW" ||
                                                    x.B1_APPL_STATUS.ToUpper().Trim()
                                                    == "READY TO BATCH PRINT") &&
                                                   (getBblEntityListAll.Contains(x.DCBC_ENTITY_ID)))
                                    .Take(101)
                                    .ToList();
                            bindQuickSearchTables.CorpEntityTable =
                                corpRepositoryData.FindBy(x => x.BusinessName != null &&
                                                               x.EntityStatus.ToUpper().Trim() != "DEPRECATE"
                                                               &&
                                                               x.EntityStatus.ToUpper().Trim() !=
                                                               "MIGRATEDNODATA" &&
                                                               x.EntityStatus.ToUpper().Trim() != "OLDACT" &&
                                                               x.EntityStatus.ToUpper().Trim() != "PENDING"
                                                               && x.EntityStatus.ToUpper().Trim() != "REDEEMED" &&
                                                               (getCorpEntityListALL.Contains(x.DCBC_ENTITY_ID)))
                                    .Take(101)
                                    .ToList();
                            bindQuickSearchTables.OplaEntityTable =
                                oplaRepositoryData.FindBy(x => getOplaEntityListAll.Contains(x.DCBC_ENTITY_ID))
                                    .Take(101)
                                    .ToList();
                            bindQuickSearchTables.CbeEntityTable =
                                (cbeRepositoryData.FindBy(x => getCbeEntityListAll.Contains(x.DCBC_ENTITY_ID)))
                                    .Take(101).ToList();
                            bindQuickSearchTables.AbraEntityTable =
                                (abrarep.FindBy(x => getAbraEntityListAll.Contains(x.DCBC_ENTITY_ID))).Take(101)
                                    .ToList();

                            break;
                    }
                }
                return await Task.FromResult(bindQuickSearchTables);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        private static Expression<Func<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX, bool>> WithDataSource(
            string dataSourceInput)
        {
            return lookUpIndex => lookUpIndex.DATA_SOURCE == dataSourceInput;
        }

        //saved the paricular regulatory field and save it into the user list
        public static bool MySavedList(int entityid, List<UserService> userServicesData)
        {
            var result = false;
            if (userServicesData != null)
            {
                result = (userServicesData).Any(a => a.DCBC_ENTITY_ID == entityid);
            }
            return result;
        }

        #endregion serachresults

        #region Common

        /// <summary>
        /// get list of each regulatory count and exceed regulatory data and total number of counts
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SearchDataViewModel>> GetCountRecords(ICollection<CommonData> finaldata)
        {
            _dataViewModel = new SearchDataViewModel
            {
                ABRACount = abracount.ToString(),
                BBLCount = bblcount.ToString(),
                CBECount = cbecount.ToString(),
                CORPCount = corpcount.ToString(),
                OPLACount = oplacount.ToString(),
                ExcededBbl = _ExcededBbl.ToString(),
                ExcededAbra = _ExcededAbra.ToString(),
                ExcededCbp = _ExcededCbp.ToString(),
                ExcededCorp = _ExcededCorp.ToString(),
                ExcededOpla = _ExcededOpla.ToString(),
                ExcededCount = _ExcededCount,
                ExcededRegulatoryEntities = (_ExcededBbl.ToString() + _ExcededAbra.ToString() +
                                 _ExcededCbp.ToString() + _ExcededCorp.ToString() + _ExcededOpla.ToString()),
                RecordCount = (abracount + bblcount + cbecount + corpcount + oplacount).ToString()
            };

            var final = new List<SearchDataViewModel> { _dataViewModel };

            return await Task.FromResult(final);
        }

        #endregion Common
    }

    public static class FilterMultiColumnData1
    {
        /// <summary>
        /// DCBC_ENTITY_MultiColumn_LOOKUP_INDEX table filter with company name with start's with , ends with and contains condition according to input given by user
        /// </summary>
        /// <param name="filterData"></param>
        /// <param name="companyName"></param>
        /// <returns></returns>
        public static IQueryable<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> WithCompanyName(
            this IQueryable<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> filterData, string companyName)
        {
            if (!string.IsNullOrEmpty(companyName))
            {
                if (companyName.Split('*').Length - 1 != 0)
                {
                    if ((companyName.StartsWith("*")) && (!companyName.EndsWith("*")))
                    {
                        string companyNameStartWith = companyName.Remove(0, 1);
                        filterData = filterData.AsNoTracking().Where(a => a.CompanyNameLookup.ToUpper().EndsWith(companyNameStartWith.ToUpper()));
                    }
                    else if ((companyName.EndsWith("*")) && (!companyName.StartsWith("*")))
                    {
                        filterData = filterData.Where(a => a.CompanyNameLookup.ToUpper().Trim().StartsWith(companyName.Remove(companyName.Length - 1).ToUpper().Trim()));
                    }
                    else if (companyName.Split('*').Length - 1 == 2 && companyName.StartsWith("*") && companyName.EndsWith("*"))
                    {
                        filterData = filterData.Where(a => a.CompanyNameLookup.ToUpper().Contains(companyName.Replace("*", "").ToUpper()));
                    }
                    else
                    { filterData = filterData.AsNoTracking().Where(a => a.CompanyNameLookup.ToUpper() == companyName.Trim().ToUpper()); }
                }
                else if (companyName.Split('*').Length - 1 == 0)
                {
                    filterData = filterData.AsNoTracking().Where(a => a.CompanyNameLookup.ToUpper().Trim() == (companyName.ToUpper().Trim()));
                    // filterData = filterData.AsNoTracking().Where(a => a.CompanyNameLookup.ToUpper().Trim().Contains(companyName.ToUpper().Trim()));
                }
            }
            return filterData;
        }

        /// <summary>
        /// DCBC_ENTITY_MultiColumn_LOOKUP_INDEX table filter with license name with contains condition according to input given by user
        /// </summary>
        /// <param name="filterData"></param>
        /// <param name="licenseName"></param>
        /// <returns></returns>
        public static IQueryable<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> WithLicenseName(
            this IQueryable<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> filterData, string licenseName)
        {
            if (!string.IsNullOrEmpty(licenseName))
            {
                filterData =
                    filterData.Where(a => a.LicenseNumberLookup.ToUpper() == licenseName.ToUpper().Replace("*", "").ToUpper());
            }
            return filterData;
        }

        /// <summary>
        /// DCBC_ENTITY_MultiColumn_LOOKUP_INDEX table filter with first name with start's with , ends with and contains condition according to input given by user
        /// </summary>
        /// <param name="filterData"></param>
        /// <param name="firstName"></param>
        /// <returns></returns>
        public static IQueryable<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> WithFirstName(
            this IQueryable<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> filterData, string firstName)
        {
            if (!string.IsNullOrEmpty(firstName))
            {
                if (firstName.Split('*').Length - 1 != 0)
                {
                    if ((firstName.StartsWith("*")) && (!firstName.EndsWith("*")))
                    {
                        string firstNameStartWith = firstName.Remove(0, 1);
                        filterData = filterData.AsNoTracking().AsNoTracking().Where(a => a.FirstNameLookup.ToUpper().EndsWith(firstNameStartWith.ToUpper()));
                    }
                    else if ((firstName.EndsWith("*")) && (!firstName.StartsWith("*")))
                    {
                        filterData = filterData.AsNoTracking().AsNoTracking().Where(a => a.FirstNameLookup.ToUpper().StartsWith(firstName.Remove(firstName.Length - 1).ToUpper()));
                    }
                    else if (firstName.Split('*').Length - 1 == 2 && firstName.StartsWith("*") && firstName.EndsWith("*"))
                    {
                        filterData = filterData.Where(a => a.FirstNameLookup.ToUpper().Trim().Contains(firstName.Replace("*", "").ToUpper().Trim()));
                    }
                    else
                    { filterData = filterData.AsNoTracking().Where(a => a.FirstNameLookup.ToUpper() == firstName.Trim().ToUpper()); }
                }
                else if (firstName.Split('*').Length - 1 == 0)
                {
                    filterData = filterData.AsNoTracking().Where(a => a.FirstNameLookup.ToUpper().Trim() == (firstName.Trim().ToUpper().Trim()));
                    //filterData = filterData.AsNoTracking().Where(a => a.FirstNameLookup.ToUpper().Trim().Contains(firstName.Trim().ToUpper().Trim()));
                }
            }
            return filterData;
        }

        /// <summary>
        /// DCBC_ENTITY_MultiColumn_LOOKUP_INDEX table filter with last name with start's with , ends with and contains condition according to input given by user
        /// </summary>
        /// <param name="filterData"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public static IQueryable<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> WithLastName(
            this IQueryable<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> filterData, string lastName)
        {
            if (!string.IsNullOrEmpty(lastName))
            {
                if (lastName.Split('*').Length - 1 != 0)
                {
                    if ((lastName.StartsWith("*")) && (!lastName.EndsWith("*")))
                    {
                        string lastNameStartWith = lastName.Remove(0, 1);
                        filterData = filterData.AsNoTracking().Where(a => a.LastNameLookup.ToUpper().EndsWith(lastNameStartWith.ToUpper()));
                    }
                    else if ((lastName.EndsWith("*")) && (!lastName.StartsWith("*")))
                    {
                        filterData = filterData.AsNoTracking().Where(a => a.LastNameLookup.ToUpper().StartsWith(lastName.Remove(lastName.Length - 1).ToUpper()));
                    }
                    else if (lastName.Split('*').Length - 1 == 2 && lastName.StartsWith("*") && lastName.EndsWith("*"))
                    {
                        filterData = filterData.Where(a => a.LastNameLookup.ToUpper().Trim().Contains(lastName.Replace("*", "").ToUpper().Trim()));
                    }
                    else
                    { filterData = filterData.AsNoTracking().Where(a => a.LastNameLookup.ToUpper() == lastName.Trim().ToUpper()); }
                }
                else if (lastName.Split('*').Length - 1 == 0)
                {
                    filterData = filterData.AsNoTracking().Where(a => a.LastNameLookup.ToUpper().Trim() == (lastName.Trim().ToUpper().Trim()));
                    // filterData = filterData.AsNoTracking().Where(a => a.LastNameLookup.ToUpper().Trim().Contains(lastName.Trim().ToUpper().Trim()));
                }
            }
            return filterData;
        }
    }

    public static class PredicateExtensions
    {
        /// <summary>
        /// Begin an expression chain
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Default return value if the chanin is ended early</param>
        /// <returns>A lambda expression stub</returns>
        public static Expression<Func<T, bool>> Begin<T>(bool value = false)
        {
            if (value)
                return parameter => true; //value cannot be used in place of true/false

            return parameter => false;
        }
        /// <summary>
        /// And an expression chain
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right">Default return value if the chanin is ended early</param>
        /// <returns>A lambda expression stub</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            return CombineLambdas(left, right, ExpressionType.AndAlso);
        }
        /// <summary>
        /// OR an expression chain
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right">Default return value if the chanin is ended early</param>
        /// <returns>A lambda expression stub</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            return CombineLambdas(left, right, ExpressionType.OrElse);
        }

        #region private
        /// <summary>
        /// CombineLambdas expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="expressionType"></param>
        /// <returns></returns>
        private static Expression<Func<T, bool>> CombineLambdas<T>(this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right, ExpressionType expressionType)
        {
            //Remove expressions created with Begin<T>()
            if (IsExpressionBodyConstant(left))
                return (right);

            var p = left.Parameters[0];

            var visitor = new SubstituteParameterVisitor();
            visitor.Sub[right.Parameters[0]] = p;

            Expression body = Expression.MakeBinary(expressionType, left.Body, visitor.Visit(right.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        private static bool IsExpressionBodyConstant<T>(Expression<Func<T, bool>> left)
        {
            return left.Body.NodeType == ExpressionType.Constant;
        }

        internal class SubstituteParameterVisitor : ExpressionVisitor
        {
            public Dictionary<Expression, Expression> Sub = new Dictionary<Expression, Expression>();

            protected override Expression VisitParameter(ParameterExpression node)
            {
                Expression newValue;
                if (Sub.TryGetValue(node, out newValue))
                {
                    return newValue;
                }
                return node;
            }
        }

        #endregion private
    }
}