using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessCenter.Data.Implementation
{
    public class SearchRepository : GenericRepository<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX>, ISearchRepository
    {
        //  protected IdentityConnectionContext Context;

        public IAbraRepository Abrarep;
        public IBblRepository Bblrep;
        public ICbeRepository Cberep;
        public IOplaRepository Oplarep;
        public ICorpRespository Corprep;
        public ISearchKeywordRepository KeywordRepository;
        public IKeywordDetailsRepository KeywordDetRep;
        public IUserServicesRepository UserServicesRepository;
        public MultiColumnFilters multi_columnfilter;

        public SearchRepository(IUnitOfWork context, IAbraRepository iabra,
            IBblRepository ibbl,
            ICbeRepository icbe,
            IOplaRepository iopla,
            ICorpRespository icorp,
            ISearchKeywordRepository keywordRepository,
            IKeywordDetailsRepository keywordDetRep,
             IUserServicesRepository userServicesRepository
            )
            : base(context)
        {
            Abrarep = iabra;
            Bblrep = ibbl;
            Cberep = icbe;
            Oplarep = iopla;
            Corprep = icorp;
            KeywordRepository = keywordRepository;
            KeywordDetRep = keywordDetRep;
            UserServicesRepository = userServicesRepository;
        }

        /// <summary>
        /// Get records count in search page
        /// </summary>
        /// <param name="searchinput"></param>
        /// <returns></returns>
        public async Task<ICollection<SearchData>> GetSearchData(SearchServiceInputs searchinput)
        {
            await SerachKeyWordInsertDetails(searchinput);
            var getSearchFilterByDataSource = await SerachMultiColumnIndexByDataSource(searchinput);
            multi_columnfilter = new MultiColumnFilters();
            var finaldata = await FillSearchData(searchinput, getSearchFilterByDataSource);
            var final = await multi_columnfilter.GetCountRecords(finaldata.ToList());
            var totalSearchData = new List<SearchData>();
            var searchdata = new SearchData
            {
                SearchCount = final.ToList(),
                SearchFinalData = finaldata.ToList()
            };
            totalSearchData.Add(searchdata);
            return totalSearchData;
        }

        /// <summary>
        /// get DCBC_ENTITY_MultiColumn_LOOKUP_INDEX using repository
        /// </summary>
        /// <returns></returns>
        //public IEnumerable<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> GetMultiColumnLookupAll()
        //{
        //    return GetAll().AsQueryable().AsNoTracking();
        //}

        private async Task<int> SerachKeyWordInsertDetails(SearchServiceInputs searchInput)
        {
            if (searchInput.IsChanged != true)
            {
                if (searchInput.KeyType.ToUpper() == "SEARCH")
                {
                    var searchtype = searchInput.SearchType.Split('-');

                    foreach (var part in searchtype)
                    {
                        if (searchInput.CompanyName != string.Empty && string.IsNullOrEmpty(part.Trim().ToUpper()) == false && string.IsNullOrEmpty(searchInput.CompanyName) == false)
                        {
                            InsertUpdateKeyword(searchInput.CompanyName, GenericEnums.SearchType.BusinessName, part.Trim().ToUpper());
                        }
                        if (searchInput.LicenseName != string.Empty && string.IsNullOrEmpty(searchInput.LicenseName) == false && string.IsNullOrEmpty(part.Trim().ToUpper()) == false)
                        {
                            InsertUpdateKeyword(searchInput.LicenseName, GenericEnums.SearchType.LicenseNumber, part.Trim().ToUpper());
                        }
                        if (searchInput.FirstName != string.Empty && string.IsNullOrEmpty(searchInput.FirstName) == false && string.IsNullOrEmpty(part.Trim().ToUpper()) == false)
                        {
                            InsertUpdateKeyword(searchInput.FirstName, GenericEnums.SearchType.FirstName, part.Trim().ToUpper());
                        }
                        if (searchInput.LastName != string.Empty && string.IsNullOrEmpty(searchInput.LastName) == false && string.IsNullOrEmpty(part.Trim().ToUpper()) == false)
                        {
                            InsertUpdateKeyword(searchInput.LastName, GenericEnums.SearchType.LastName, part.Trim().ToUpper());
                        }
                    }
                }
            }
            return await Task.FromResult(1);
        }
        /// <summary>
        /// This method is used to get look up index data based on user inputs
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns>Return DCBC_ENTITY_MultiColumn_LOOKUP_INDEX data</returns>
        private async Task<IEnumerable<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX>> SerachMultiColumnIndexByDataSource(
            SearchServiceInputs searchInput)
        {
            try
            {
                var checkedValue = new InputCheckedValues();
                var searchCheckedList = searchInput.SearchType.Split('-');
                foreach (var checkedItem in searchCheckedList)
                {
                    switch (checkedItem.Trim().ToUpper())
                    {
                        case "BBL":
                            checkedValue.Bbl =
                                GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.BBL).ToUpper().Trim();
                            break;

                        case "CORP":
                            checkedValue.Corp =
                                GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.CORP).ToUpper().Trim();
                            break;

                        case "OPLA":
                            checkedValue.Opla =
                                GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.OPLA).ToUpper().Trim();
                            break;

                        case "CBE":
                            checkedValue.Cbe =
                                GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.CBE).ToUpper().Trim();

                            break;

                        case "ABRA":
                            checkedValue.Abra =
                                GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.ABRA).ToUpper().Trim();

                            break;

                        case "ALL":
                            checkedValue.Bbl =
                                GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.BBL).ToUpper().Trim();
                            checkedValue.Opla =
                                GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.OPLA).ToUpper().Trim();
                            checkedValue.Corp =
                                GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.CORP).ToUpper().Trim();
                            checkedValue.Cbe =
                                GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.CBE).ToUpper().Trim();
                            checkedValue.Abra =
                                GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.ABRA).ToUpper().Trim();

                            break;
                    }
                }
                var getFilterMulticolumnData =
                    FindBy(x => x.DATA_SOURCE.ToUpper().Trim() == checkedValue.Bbl.ToUpper().Trim()
                                || x.DATA_SOURCE.ToUpper().Trim() == checkedValue.Opla.ToUpper().Trim() ||
                                x.DATA_SOURCE.ToUpper().Trim() == checkedValue.Corp.ToUpper().Trim()
                                || x.DATA_SOURCE.ToUpper().Trim() == checkedValue.Abra.ToUpper().Trim()
                                || x.DATA_SOURCE.ToUpper().Trim() == checkedValue.Cbe.ToUpper().Trim());

                return await Task.FromResult(getFilterMulticolumnData);
            }
            catch (Exception ex)
            { throw ex; }
        }

        /// <summary>
        /// get data after filter all the conditions with search criteria and fill regulatory entities data according to conditions
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        protected async Task<ICollection<CommonData>> FillSearchData(SearchServiceInputs searchInput, IEnumerable<DCBC_ENTITY_MultiColumn_LOOKUP_INDEX> getFilterMulticolumnData)
        {
            ICollection<CommonData> lookupdata = null;
            try
            {
                var getUserData = UserServicesRepository.FindBy(x => x.UserId == searchInput.Userid).AsQueryable().AsNoTracking().ToList();
                multi_columnfilter = new MultiColumnFilters();
                lookupdata = await multi_columnfilter.GetFilterData(searchInput, getFilterMulticolumnData, getUserData,
              Abrarep, Bblrep, Cberep, Corprep, Oplarep);
            }
            catch (Exception ex)
            { throw ex; }

            return await Task.FromResult(lookupdata.ToList());
        }

        /// <summary>
        /// get data to bind the final data after change made in angular like save and delete the data will be rebind
        /// </summary>
        /// <param name="searchinput"></param>
        /// <returns></returns>
        public async Task<IQueryable<CommonData>> GetAllData(SearchServiceInputs searchinput)
        {
            SearchResultData srd = new SearchResultData();
            var checkedValue = new InputCheckedValues();
            var parts = searchinput.DisplayType.Split('-');
            foreach (var part in parts)
            {
                switch (part.Trim().ToUpper())
                {
                    case "BBL":
                        checkedValue.Bbl = "BBL";
                        break;

                    case "CORP":
                        checkedValue.Corp = "CORP";
                        break;

                    case "OPLA":
                        checkedValue.Opla = "OPLA";
                        break;

                    case "CBE":
                        checkedValue.Cbe = "CBE";
                        break;

                    case "ABRA":
                        checkedValue.Abra = "ABRA";
                        break;

                    case "ALL":
                        checkedValue.Bbl = "BBL";
                        checkedValue.Opla = "OPLA";
                        checkedValue.Corp = "CORP";
                        checkedValue.Cbe = "CBE";
                        checkedValue.Abra = "ABRA";
                        break;
                }
            }
            if (searchinput.IsChanged)
            {
                //   FillSearchData(searchinput);
            }

            var fill = srd.FilledData;
            return await Task.FromResult(fill.AsQueryable());
        }

        /// <summary>
        /// use for auto search of the company name using table DCBC_ENTITY_MultiColumn_LOOKUP_INDEX
        /// </summary>
        /// <param name="inputSearchKeyWord"></param>
        /// <returns></returns>
        public async Task<IQueryable<string>> GetCompanyName(AutoFillKeyWord inputSearchKeyWord)
        {
            var getCompanyName = from x in (FindBySingle(x => x.CompanyNameLookup.ToUpper().Contains(inputSearchKeyWord.SearchKeyWord.ToUpper()))
                       .OrderBy(x => x.CompanyNameLookup))
                                 select x.CompanyNameOrig.ToLower();
            return await Task.FromResult(getCompanyName.AsNoTracking().Distinct().AsQueryable());
        }

        /// <summary>
        /// use for auto search of the first name using table DCBC_ENTITY_MultiColumn_LOOKUP_INDEX
        /// </summary>
        /// <param name="inputSearchKeyWord"></param>
        /// <returns></returns>
        public async Task<IQueryable<string>> GetFirstName(AutoFillKeyWord inputSearchKeyWord)
        {
            var getFirstName = from x in (FindBySingle(x => x.FirstNameLookup.ToUpper().Contains(inputSearchKeyWord.SearchKeyWord.ToUpper()))
                    .OrderBy(x => x.FirstNameOrig))
                               select x.FirstNameOrig.ToLower();

            return await Task.FromResult(getFirstName.AsNoTracking().Distinct().AsQueryable());
        }

        /// <summary>
        /// use for auto search of the last name using table DCBC_ENTITY_MultiColumn_LOOKUP_INDEX
        /// </summary>
        /// <param name="inputSearchKeyWord"></param>
        /// <returns></returns>

        public async Task<IQueryable<string>> GetLastName(AutoFillKeyWord inputSearchKeyWord)
        {
            var getLastName = from x in (FindBySingle(x => x.LastNameLookup.ToUpper().Contains(inputSearchKeyWord.SearchKeyWord.ToUpper()))
                    .OrderBy(x => x.LastNameOrig))
                              select x.LastNameOrig.ToLower();
            return await Task.FromResult(getLastName.AsNoTracking().Distinct().AsQueryable());
        }

        /// <summary>
        /// This method is used to get License Number based on User input.
        /// </summary>
        /// <param name="inputSearchKeyWord"></param>
        /// <returns>Retrun result String</returns>
        public List<string> GetLicenceNumber(AutoFillKeyWord inputSearchKeyWord)
        {
            var getLicenseNumber = from x in (FindBy(x => x.LicenseNumberOrig.ToUpper().Contains(inputSearchKeyWord.SearchKeyWord.ToUpper()))
                     .OrderBy(x => x.LicenseNumberLookup))
                                   select x.LicenseNumberOrig.ToLower();
            return getLicenseNumber.AsEnumerable().Distinct().ToList();
        }

        /// <summary>
        /// insert the user search data with the regulatory enties
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="keyType"></param>
        /// <param name="searchtype"></param>
        public void InsertUpdateKeyword(string keyWord, GenericEnums.SearchType keyType, string searchtype)
        {
            string keyt = Convert.ToInt32(keyType).ToString().Trim();
            var dateAndTime = DateTime.Now;
            try
            {
                var keywordid = (from x in KeywordRepository.FindBy(key => key.Keywords == keyWord && key.TypeID == keyt && key.SearchCriteria == searchtype)
                                 select new { x.KeyId }).ToList();
                if (!keywordid.Any())
                {
                    var keymaster = new KeywordMaster
                    {
                        Keywords = keyWord,
                        CreatedDate = dateAndTime.Date,
                        TypeID = keyt,
                        SearchCriteria = searchtype
                    };
                    KeywordRepository.AddKeyWord(keymaster);
                    var keyid = Convert.ToInt32(keymaster.KeyId);
                    var keyDetails = new KeywordDetails { KeyId = keyid, KeyCount = 1, CreatedDate = dateAndTime.Date };
                    KeywordDetRep.AddKeyWord(keyDetails);
                }
                else
                {
                    long id = 0;
                    foreach (var i in keywordid)
                    { id = i.KeyId; }
                    var keyCount = KeywordDetRep.FindBy(key => key.KeyId == id && EntityFunctions.TruncateTime(key.CreatedDate) ==
                        EntityFunctions.TruncateTime(dateAndTime.Date)).Select(key => new { key.KeyCount });
                    if (!keyCount.Any())
                    {
                        var keyDetails = new KeywordDetails { KeyId = id, KeyCount = 1, CreatedDate = dateAndTime.Date };
                        KeywordDetRep.AddKeyWord(keyDetails);
                    }
                    else
                    {
                        var keyworddet = KeywordDetRep.FindBy(key => key.KeyId == id &&
                            EntityFunctions.TruncateTime(key.CreatedDate) == EntityFunctions.TruncateTime(dateAndTime.Date)).First();
                        keyworddet.KeyCount = Convert.ToInt32(keyworddet.KeyCount) + 1;
                        KeywordDetRep.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //public async Task<IEnumerable<SearchData>> GetSearchPdfData(SearchServiceInputs searchinput)
        //{
        //    MultiColumnFilters mcf = new MultiColumnFilters();

        //    var finaldata = await FillPdfData(searchinput);
        //    var final = await mcf.GetCountRecords(SearchResultData.FilledData.ToList());
        //    var totalSearchData = new List<SearchData>();
        //    var searchdata = new SearchData
        //    {
        //        SearchCount = final.ToList(),
        //        SearchFinalData = SearchResultData.FilledData.ToList()
        //    };
        //    totalSearchData.Add(searchdata);
        //    return totalSearchData;
        //}

        //protected async Task<IEnumerable<SearchResultData>> FillPdfData(SearchServiceInputs searchInput)
        //{
        //    //if (searchInput.IsChanged != true)
        //    //{
        //    //    if (searchInput.KeyType.ToUpper() == "SEARCH")
        //    //    {
        //    //        var searchtype = searchInput.SearchType.Split('-');

        //    //        foreach (var part in searchtype)
        //    //        {
        //    //            if (searchInput.CompanyName != string.Empty && string.IsNullOrEmpty(part.Trim().ToUpper()) == false && string.IsNullOrEmpty(searchInput.CompanyName) == false)
        //    //            {
        //    //                InsertUpdateKeyword(searchInput.CompanyName, GenericEnums.SearchType.BusinessName, part.Trim().ToUpper());
        //    //            }
        //    //            if (searchInput.LicenseName != string.Empty && string.IsNullOrEmpty(searchInput.LicenseName) == false && string.IsNullOrEmpty(part.Trim().ToUpper()) == false)
        //    //            {
        //    //                InsertUpdateKeyword(searchInput.LicenseName, GenericEnums.SearchType.LicenseNumber, part.Trim().ToUpper());
        //    //            }
        //    //            if (searchInput.FirstName != string.Empty && string.IsNullOrEmpty(searchInput.FirstName) == false && string.IsNullOrEmpty(part.Trim().ToUpper()) == false)
        //    //            {
        //    //                InsertUpdateKeyword(searchInput.FirstName, GenericEnums.SearchType.FirstName, part.Trim().ToUpper());
        //    //            }
        //    //            if (searchInput.LastName != string.Empty && string.IsNullOrEmpty(searchInput.LastName) == false && string.IsNullOrEmpty(part.Trim().ToUpper()) == false)
        //    //            {
        //    //                InsertUpdateKeyword(searchInput.LastName, GenericEnums.SearchType.LastName, part.Trim().ToUpper());
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    var getSearchData = new List<SearchResultData>();
        //    var resultData = new SearchResultData();
        //    SearchResultData.AbraData = null;
        //    SearchResultData.BblData = null;
        //    SearchResultData.CbeData = null;
        //    SearchResultData.OplaData = null;
        //    SearchResultData.CorpData = null;
        //    try
        //    {
        //        var checkedValue = new InputCheckedValues();
        //        var searchCheckedList = searchInput.SearchType.Split('-');
        //        foreach (var checkedItem in searchCheckedList)
        //        {
        //            switch (checkedItem.Trim().ToUpper())
        //            {
        //                case "BBL":
        //                    checkedValue.Bbl = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.BBL).ToUpper().Trim();
        //                    SearchResultData.BblData = Bblrep.FindBy(x => (x.B1_APPL_STATUS.ToUpper().Trim() == "ACTIVE"
        //                    || x.B1_APPL_STATUS.ToUpper().Trim() == "READY TO RENEW" || x.B1_APPL_STATUS.ToUpper().Trim()
        //                    == "READY TO BATCH PRINT")).AsQueryable().AsNoTracking();
        //                    break;

        //                case "CORP":
        //                    checkedValue.Corp = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.CORP).ToUpper().Trim();
        //                    SearchResultData.CorpData = Corprep.FindBy(x => x.BusinessName != null &&
        //                     x.EntityStatus.ToUpper().Trim() != "DEPRECATE"
        //                     && x.EntityStatus.ToUpper().Trim() != "MIGRATEDNODATA" &&
        //                   x.EntityStatus.ToUpper().Trim() != "OLDACT" && x.EntityStatus.ToUpper().Trim() != "PENDING"
        //                   && x.EntityStatus.ToUpper().Trim() != "REDEEMED").AsQueryable().AsNoTracking();
        //                    break;

        //                case "OPLA":
        //                    checkedValue.Opla = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.OPLA).ToUpper().Trim();
        //                    SearchResultData.OplaData = (Oplarep.GetOplaLookupAll()).AsQueryable().AsNoTracking();
        //                    break;

        //                case "CBE":
        //                    checkedValue.Cbe = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.CBE).ToUpper().Trim();
        //                    SearchResultData.CbeData = (Cberep.GetCbeLookupAll()).AsQueryable().AsNoTracking();
        //                    break;

        //                case "ABRA":
        //                    checkedValue.Abra = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.ABRA).ToUpper().Trim();
        //                    SearchResultData.AbraData = (Abrarep.GeArbraLookupAll()).AsQueryable().AsNoTracking();
        //                    break;

        //                case "ALL":
        //                    checkedValue.Bbl = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.BBL).ToUpper().Trim();
        //                    checkedValue.Opla = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.OPLA).ToUpper().Trim();
        //                    checkedValue.Corp = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.CORP).ToUpper().Trim();
        //                    checkedValue.Cbe = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.CBE).ToUpper().Trim();
        //                    checkedValue.Abra = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.ABRA).ToUpper().Trim();
        //                    Parallel.Invoke(
        //                        () => SearchResultData.AbraData = (Abrarep.GeArbraLookupAll()).AsQueryable().AsNoTracking(),
        //                        () => SearchResultData.BblData = Bblrep.FindBy(x => (x.B1_APPL_STATUS.ToUpper().Trim() == "ACTIVE"
        //                            || x.B1_APPL_STATUS.ToUpper().Trim() == "READY TO RENEW" || x.B1_APPL_STATUS.ToUpper().Trim()
        //                            == "READY TO BATCH PRINT")).AsQueryable().AsNoTracking(),
        //                            () => SearchResultData.CbeData = (Cberep.GetCbeLookupAll()).AsQueryable().AsNoTracking(),
        //                            () => SearchResultData.OplaData = (Oplarep.GetOplaLookupAll()).AsQueryable().AsNoTracking(),
        //                            () => SearchResultData.CorpData = Corprep.FindBy(x => x.BusinessName != null &&
        //                                x.EntityStatus.ToUpper().Trim() != "DEPRECATE"
        //                                && x.EntityStatus.ToUpper().Trim() != "MIGRATEDNODATA" &&
        //                                x.EntityStatus.ToUpper().Trim() != "OLDACT" && x.EntityStatus.ToUpper().Trim() != "PENDING"
        //                                && x.EntityStatus.ToUpper().Trim() != "REDEEMED").AsQueryable().AsNoTracking());
        //                    break;
        //            }
        //        }
        //        var getFilterMulticolumnData = FindBy(x => x.DATA_SOURCE.ToUpper().Trim() == checkedValue.Bbl.ToUpper().Trim()
        //                                                   || x.DATA_SOURCE.ToUpper().Trim() == checkedValue.Opla.ToUpper().Trim() ||
        //                                                    x.DATA_SOURCE.ToUpper().Trim() == checkedValue.Corp.ToUpper().Trim()
        //                                                    || x.DATA_SOURCE.ToUpper().Trim() == checkedValue.Abra.ToUpper().Trim()
        //                                                    || x.DATA_SOURCE.ToUpper().Trim() == checkedValue.Cbe.ToUpper().Trim());
        //        var getUserData = UserServicesRepository.FindBy(x => x.UserId == searchInput.Userid).AsQueryable().AsNoTracking().ToList();
        //        MultiColumnFilters.PdfDownloadData(searchInput, getFilterMulticolumnData.AsQueryable().AsNoTracking(), getUserData,
        //            Abrarep, Bblrep, Cberep, Corprep, Oplarep);
        //    }
        //    catch (Exception ex)
        //    { throw ex; }
        //    getSearchData.Add(resultData);
        //    return await Task.FromResult(getSearchData.AsEnumerable());
        //}
    }
}