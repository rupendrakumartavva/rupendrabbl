using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using BusinessCenter.Data.Models;

namespace BusinessCenter.Data.Common
{
    public  class MySaveSearchFilter
    {
         int _MyAbracount,  _MyCbecount, _MyCorpcount, _MyOplacount = 0;
         int _MyBblcount = 0;
         SearchDataViewModel _MyDataViewModel;
        public SearchResultData srd;
        /// <summary>
        /// user saved data as userid as an input
        /// </summary>
        /// <param name="searchinput"></param>
        /// <param name="multicolumn"></param>
        /// <returns></returns>
        public  IQueryable<UserService> GetUserServiceData(UserServiceModel searchinput, IQueryable<UserService> multicolumn)
        {
            srd=new SearchResultData();
            var usersaveddata = (multicolumn).Where(x => x.UserId == searchinput.UserId);
            srd.MultiUserData = null;
            srd.MultiUserData = (from fillterData in usersaveddata.OrderByDescending(x => x.CreatedDate)
                                 select fillterData).Distinct().AsQueryable();


            return srd.MultiUserData;
        }
        /// <summary>
        /// get the record count of the each regulatory entities of user saved data
        /// </summary>
        /// <returns></returns>
        public  IEnumerable<SearchDataViewModel> GetCountRecords(ICollection<UserService> userServices )
        {
            _MyAbracount = userServices.Count(x => x.DATA_SOURCE.ToUpper().Trim() == "ABRA");
            _MyBblcount = userServices.Count(x => x.DATA_SOURCE.ToUpper().Trim() == "BBL");
            _MyCbecount = userServices.Count(x => x.DATA_SOURCE.ToUpper().Trim() == "CBE");
            _MyCorpcount = userServices.Count(x => x.DATA_SOURCE.ToUpper().Trim() == "CORP");
            _MyOplacount = userServices.Count(x => x.DATA_SOURCE.ToUpper().Trim() == "OPLA");
            _MyDataViewModel = new SearchDataViewModel
            {
                ABRACount = _MyAbracount.ToString(),
                BBLCount = _MyBblcount.ToString(),
                CBECount = _MyCbecount.ToString(),
                CORPCount = _MyCorpcount.ToString(),
                OPLACount = _MyOplacount.ToString(),
                RecordCount = (_MyAbracount + _MyBblcount + _MyCbecount + _MyCorpcount + _MyOplacount).ToString(),
            };

            var final = new List<SearchDataViewModel> { _MyDataViewModel };
            _MyAbracount = 0; _MyBblcount = 0; _MyCbecount = 0; _MyCorpcount = 0; _MyOplacount = 0;
            return final;

        }

        /// <summary>
        /// get the filled records  of the each regulatory entities of user saved data
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="context"></param>
        /// <param name="bblData"></param>
        /// <param name="oplaData"></param>
        /// <param name="corpData"></param>
        /// <param name="cbeData"></param>
        /// <param name="abraData"></param>

        public  IQueryable<CommonData> GetAllData(UserServiceModel userService,
        IQueryable<UserService> userServices, ICollection<DCBC_ENTITY_BBL> bblData,
            ICollection<DCBC_ENTITY_OPLA> oplaData,
            ICollection<DCBC_ENTITY_CORP> corpData,
            ICollection<DCBC_ENTITY_CBE> cbeData,
            ICollection<DCBC_ENTITY_ABRA> abraData)

        {
            srd=new SearchResultData();
            SearchEntityDisplay sed=new SearchEntityDisplay();
            var Datall = new List<CommonData>();
          
            var lookuptable = from x in
                                  userServices.OrderByDescending(x => x.CreatedDate)
                select new {x.DCBC_ENTITY_ID, x.DATA_SOURCE, x.CompanyName, x.LicenseNumber, x.UserListId};
            int sno = 1;

            foreach (var data in lookuptable)
            {
                CommonData finaldata = new CommonData();
                switch (data.DATA_SOURCE.ToUpper())
                {
                    case "BBL":
                        var data1 = data;
                        var bbl = sed.GetBblRecords(sno, Convert.ToInt32(data.DCBC_ENTITY_ID), false,bblData).ToList();
                        if (bbl.Any())
                        {
                            Datall.AddRange(bbl);
                            _MyBblcount = _MyBblcount + 1;
                            sno = sno + 1;
                        }
                        break;
                    case "CORP":
                        var data2 = data;
                        var corp =
                            sed.GetCorpRecords(sno, Convert.ToInt32(data.DCBC_ENTITY_ID), false,corpData).ToList();
                        if (corp.Any())
                        {
                            Datall.AddRange(corp);
                            _MyCorpcount = _MyCorpcount + 1;
                            sno = sno + 1;
                        }
                        break;
                    case "OPLA":
                        var data3 = data;
                        var opla = sed.GetOplaRecords(sno, Convert.ToInt32(data.DCBC_ENTITY_ID), false,oplaData).ToList();
                            //((from opladatas in oplaData
                            //    where opladatas.DCBC_ENTITY_ID == data3.DCBC_ENTITY_ID
                            //    select opladatas).AsQueryable());
                        if (opla.Any())
                        {
                            Datall.AddRange(opla);
                            _MyOplacount = _MyOplacount + 1;
                            sno = sno + 1;
                        }
                        break;
                    case "CBE":
                        var data4 = data;
                        var cbe = sed.GetCbeRecords(sno, Convert.ToInt32(data.DCBC_ENTITY_ID), false,cbeData).ToList();
                            //((from cbedatas in cbeData
                            //    where cbedatas.DCBC_ENTITY_ID == data4.DCBC_ENTITY_ID
                            //    select cbedatas).AsQueryable());
                        if (cbe.Any())
                        {
                            Datall.AddRange(cbe);
                            _MyCbecount = _MyCbecount + 1;
                            sno = sno + 1;
                        }
                        break;
                    case "ABRA":
                        var data5 = data;
                        var abra = sed.GetAbraRecords(sno, Convert.ToInt32(data.DCBC_ENTITY_ID), false,abraData).ToList();
                            //((from abradatas in abraData
                            //    where abradatas.DCBC_ENTITY_ID == data5.DCBC_ENTITY_ID
                            //    select abradatas).AsQueryable());
                        if (abra.Any())
                        {
                            Datall.AddRange(abra);
                            _MyAbracount = _MyAbracount + 1;
                            sno = sno + 1;
                        }
                        break;
                }
               

            }
            srd.SavedFilledData = null;
         return   srd.SavedFilledData = Datall.AsQueryable().AsNoTracking();
        }
    }
}