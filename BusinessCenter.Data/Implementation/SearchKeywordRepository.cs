using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using BusinessCenter.Data.Models;

namespace BusinessCenter.Data.Implementation
{
    public class SearchKeywordRepository : GenericRepository<KeywordMaster>, ISearchKeywordRepository
    {
        protected IKeywordDetailsRepository keyDetails;
        protected IUserRepository userRepo;
        protected IUserServicesRepository userServiceRepo;
        public SearchKeywordRepository(IUnitOfWork context,IKeywordDetailsRepository KeyDetailsRepository,IUserRepository userRepository,
            IUserServicesRepository userServiceRepsitory)
            : base(context)
        {
            keyDetails = KeyDetailsRepository;
            userRepo = userRepository;
            userServiceRepo = userServiceRepsitory;
        }
       
        /// <summary>
        /// This method is used to Get all Key Master Data .
        /// </summary>
        /// <returns>List of KeyMaster</returns>
        public IEnumerable<KeywordMaster> GetKeyMaster()
        {
            return GetAll().AsQueryable().AsNoTracking();
        }
        /// <summary>
        /// This method is used to retrive Key Master Data with Dyanmic inputs
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of Key Master</returns>
        public new IQueryable<KeywordMaster> FindBy(System.Linq.Expressions.Expression<Func<KeywordMaster, bool>> predicate)
        {

            var query = DbSet.Where(predicate).AsQueryable();
            return query;
        }
        /// <summary>
        /// This method is used to Insert data into the Key Master.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public  void AddKeyWord(KeywordMaster entity)
        {
            Add(entity);
            Save();
        }
      
        /// <summary>
        /// This Method is used to get Key Master Details based on From Date, To Date and Display Type
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="displayType"></param>
        /// <returns>Retrun List Search Keyword </returns>
        public async Task<IQueryable<SearchKeywordModel>> GetKeywordSearchCount(DateTime fromDate, DateTime toDate, string displayType)
        {
            var finalldata = new List<SearchKeywordModel>();
            if (displayType.Trim().ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.ALL).ToUpper().Trim())
            {
                var keycount = ((from mkey in GetKeyMaster().AsQueryable()
                                 join dkey in keyDetails.KeyDetailsGetAll() on mkey.KeyId equals dkey.KeyId                              
                                 orderby dkey.CreatedDate descending, dkey.KeyCount descending
                                 select new
                                 {
                                     mkey.KeyId,
                                     mkey.Keywords,
                                     mkey.TypeID,
                                     dkey.KeywordDid,
                                     dkey.KeyCount,
                                     dkey.CreatedDate
                                 }).AsQueryable().AsNoTracking());
                foreach (var keyData in keycount)
                {
                    var skm = new SearchKeywordModel
                    {
                        KeyId = keyData.KeyId.ToString(),
                        KeywordDid = keyData.KeywordDid.ToString(),
                        Keywords = keyData.Keywords.Trim(),
                        TypeID = TypeData(keyData.TypeID),
                        KeyCount =Convert.ToInt16( keyData.KeyCount.ToString()),
                        CreatedDate = Convert.ToDateTime(keyData.CreatedDate).ToString("MM/dd/yyyy").Replace("-", "/")
                    };
                    finalldata.Add(skm);
                }
            }
            else
            {
                var bkey = string.Empty;
                if (displayType.Trim().ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.BUSINESSNAME).ToUpper().Trim())
                 bkey = Convert.ToInt32(GenericEnums.SearchType.BusinessName).ToString().Trim();
                if (displayType.Trim().ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.LICENSENUMBER).ToUpper().Trim())
                    bkey = Convert.ToInt32(GenericEnums.SearchType.LicenseNumber).ToString().Trim();
                        if (displayType.Trim().ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.FIRSTNAME).ToUpper().Trim())
                    bkey = Convert.ToInt32(GenericEnums.SearchType.FirstName).ToString().Trim();
                  if (displayType.Trim().ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.LASTNAME).ToUpper().Trim())
                    bkey = Convert.ToInt32(GenericEnums.SearchType.LastName).ToString().Trim();
                var keycount = ((from Mkey in GetKeyMaster().AsQueryable()
                                 join Dkey in keyDetails.KeyDetailsGetAll() on Mkey.KeyId equals Dkey.KeyId
                                 where Mkey.TypeID == bkey
                                 orderby Dkey.CreatedDate descending, Dkey.KeyCount descending
                                 select new
                                 {
                                     Mkey.KeyId,
                                     Mkey.Keywords,
                                     Mkey.TypeID,
                                     Dkey.KeywordDid,
                                     Dkey.KeyCount,
                                     Dkey.CreatedDate
                                 }).AsQueryable().AsNoTracking());
                foreach (var keyData in keycount)
                {
                    var skm = new SearchKeywordModel
                    {
                        KeyId = keyData.KeyId.ToString(),
                        KeywordDid = keyData.KeywordDid.ToString(),
                        Keywords = keyData.Keywords.Trim(),
                        TypeID = TypeData(keyData.TypeID),
                        KeyCount =Convert.ToInt16( keyData.KeyCount.ToString()),
                        CreatedDate = Convert.ToDateTime(keyData.CreatedDate).ToString("MM/dd/yyyy")
                    };
                    finalldata.Add(skm);
                }
            }
            var result1 =(from x in finalldata select new {x.Keywords, x.CreatedDate, x.KeyCount,x.TypeID});
            var keygroupcount = from r in result1
                     where (true) orderby r.CreatedDate
                     group r by new { r.Keywords, r.CreatedDate, r.TypeID} into grp
                     select new
                     {
                         Keywords = grp.Key.Keywords,
                         CreatedDate =grp.Key.CreatedDate,
                         cnt = grp.Count(),
                         typeid=grp.Key.TypeID
                     };
            var orderbydata = keygroupcount.Select(keycount => new SearchKeywordModel()
            {
                Keywords = keycount.Keywords,
                KeyCount =(from kc in result1
                                where kc.Keywords.ToUpper() ==keycount.Keywords.ToUpper() && kc.CreatedDate==keycount.CreatedDate && kc.TypeID==keycount.typeid 
                                select kc.KeyCount).FirstOrDefault(),
                CreatedDate = keycount.CreatedDate.ToString(),
                TypeID=keycount.typeid
            }).AsQueryable().AsNoTracking();
            var keydata = from data in orderbydata orderby data.CreatedDate descending select data;
            return await Task.FromResult(keydata.AsQueryable());           
        }
        /// <summary>
        /// This method is get the Count of each Key based on From Date, To Date and Display Type.
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="displayType"></param>
        /// <returns>Retrun List Search Keyword</returns>
        public async Task<List<SearchKeywordModel>> AdminKeywordSearchCount(DateTime fromDate, DateTime toDate, string displayType)
        {
            var finalldata = new List<SearchKeywordModel>();
            if (displayType.Trim().ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.ALL).ToUpper().Trim())
            {
                var keycount = ((from mkey in GetKeyMaster().AsQueryable()
                                 join dkey in keyDetails.KeyDetailsGetAll() on mkey.KeyId equals dkey.KeyId
                                 orderby dkey.CreatedDate descending, dkey.KeyCount descending
                                 select new
                                 {
                                     mkey.KeyId,
                                     mkey.Keywords,
                                     mkey.TypeID,
                                     dkey.KeywordDid,
                                     dkey.KeyCount
                                     
                                 }).AsQueryable().AsNoTracking());
                foreach (var keyData in keycount)
                {
                    var skm = new SearchKeywordModel
                    {
                        KeyId = keyData.KeyId.ToString(),
                        KeywordDid = keyData.KeywordDid.ToString(),
                        Keywords = keyData.Keywords.Trim(),
                        TypeID = TypeData(keyData.TypeID),
                        KeyCount =Convert.ToInt16( keyData.KeyCount.ToString())
                    };
                    finalldata.Add(skm);
                }
            }
            else
            {
                var bkey = string.Empty;
                if (displayType.Trim().ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.BUSINESSNAME).ToUpper().Trim())
                    bkey = Convert.ToInt32(GenericEnums.SearchType.BusinessName).ToString().Trim();
                if (displayType.Trim().ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.LICENSENUMBER).ToUpper().Trim())
                    bkey = Convert.ToInt32(GenericEnums.SearchType.LicenseNumber).ToString().Trim();
                if (displayType.Trim().ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.FIRSTNAME).ToUpper().Trim())
                    bkey = Convert.ToInt32(GenericEnums.SearchType.FirstName).ToString().Trim();
                if (displayType.Trim().ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.LASTNAME).ToUpper().Trim())
                    bkey = Convert.ToInt32(GenericEnums.SearchType.LastName).ToString().Trim();
                var keycount = ((from Mkey in GetKeyMaster().AsQueryable()
                                 join Dkey in keyDetails.KeyDetailsGetAll() on Mkey.KeyId equals Dkey.KeyId
                                 where Mkey.TypeID == bkey
                                 orderby Dkey.KeyCount descending
                                 select new
                                 {
                                     Mkey.KeyId,
                                     Mkey.Keywords,
                                     Mkey.TypeID,
                                     Dkey.KeywordDid,
                                     Dkey.KeyCount
                                 }).AsQueryable().AsNoTracking());
                foreach (var keyData in keycount)
                {
                    var skm = new SearchKeywordModel
                    {
                        KeyId = keyData.KeyId.ToString(),
                        KeywordDid = keyData.KeywordDid.ToString(),
                        Keywords = keyData.Keywords.Trim(),
                        TypeID = TypeData(keyData.TypeID),
                        KeyCount =Convert.ToInt16( keyData.KeyCount.ToString())
                    };
                    finalldata.Add(skm);
                }
            }
            var result1 =(from x in finalldata select new { x.Keywords,  x.KeyCount, x.TypeID });
            var keygroupcount = (from r in result1 orderby r.KeyCount descending where (true)
                                group r by new { r.Keywords, r.TypeID } into grp
                                select new
                                {
                                    Keywords = grp.Key.Keywords,
                                   cnt = grp.Count(),
                                    typeid = grp.Key.TypeID
                                }).Take(1000);
            var orderbydata = keygroupcount.Select(keycount => new SearchKeywordModel()
            {
                Keywords = keycount.Keywords,
                fullKeyCount = (from kc in result1
                                where kc.Keywords.ToUpper() ==keycount.Keywords.ToUpper() && kc.TypeID==keycount.typeid
                                select kc.KeyCount).Sum(),
                TypeID = keycount.typeid
            }).AsQueryable().ToList().OrderByDescending(x => x.KeyCount);
            var keydata = from data in orderbydata orderby data.fullKeyCount descending select data;
            return await Task.FromResult(keydata.ToList());
        }
        /// <summary>
        /// This method is used to Get the Search Type based on Type Id
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns>Retrun Result String</returns>
        public string TypeData(string typeid)
        {
            string searchtype;
            switch (Convert.ToUInt16(typeid))
            {
                case 1:
                    searchtype= "Business Name";
                    break;
                case 2:
                    searchtype = "License Number";
                    break;
                case 3:
                    searchtype = "First Name";
                    break;
                case 4:
                    searchtype = "Last Name";
                    break;
                default:
                    searchtype = "";
                    break;
            }
            return searchtype;
        }
        /// <summary>
        /// This method is used to Count of the Search Key Word Type
        /// </summary>
        /// <returns>Retrun KewordsCount</returns>
        public async Task<IQueryable<KewordsCount>> GetDashBoardInvdvidualcountsCount()
        {
               string bcount="0";
               //string bkey = Convert.ToInt32(GenericEnums.SearchType.BusinessName).ToString().Trim();
               var keymaster = await GetKeywordSearchCount(DateTime.Now, DateTime.Now, "ALL");
               bcount = keymaster.Where(master => master.TypeID.Trim().ToUpper() == "BUSINESS NAME").Count().ToString();
               string lcount="0";
              // string lkey = Convert.ToInt32(GenericEnums.SearchType.LicenseNumber).ToString().Trim();
               lcount = keymaster.Where(master => master.TypeID.Trim().ToUpper() == "LICENSE NUMBER").Count().ToString();
               string fcount = "0";
             //  string fkey = Convert.ToInt32(GenericEnums.SearchType.FirstName).ToString().Trim();
               fcount = keymaster.Where(master => master.TypeID.Trim().ToUpper() == "FIRST NAME").Count().ToString(); //(
               string lncount = "0";
            //   string lnkey = Convert.ToInt32(GenericEnums.SearchType.LastName).ToString().Trim();
               lncount = keymaster.Where(master => master.TypeID.Trim().ToUpper() == "LAST NAME").Count().ToString(); 
               var keywordcountdata = new List<KewordsCount>();
                var kwcd = new KewordsCount
                {
                    Businesscount = bcount,
                    Licensecount = lcount,
                    Firstnamecount = fcount,
                    Lastnamecount = lncount
                };
                keywordcountdata.Add(kwcd);

         
           return keywordcountdata.AsQueryable();          
      }
        /// <summary>
        /// This method is used to Count of the Search Key Word Type
        /// </summary>
        /// <returns></returns>
        public async Task<IQueryable<RegulatorCount>> GetDashBoardRegulatorCount()
        {
            var pl = from r in userRepo.GetUserLookupAll()
                     join us in userServiceRepo.GetUserServiceAll() on r.Id equals us.UserId
                     where r.IsDelete != true
                     group us by us.DATA_SOURCE into grp
                     select new { key = grp.Key, cnt = grp.Count() };
            var regularcount = new List<RegulatorCount>();
            foreach (var keyData in pl)
            {
                var rc = new RegulatorCount {
                    Regulator = keyData.key, 
                    RegularCount = keyData.cnt
                };
                regularcount.Add(rc);
            }
            if (!regularcount.Any(x => x.Regulator == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.ABRA).ToUpper().Trim()))
            { 
                RegulatorCount rc =new RegulatorCount();
                rc.Regulator = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.ABRA).ToUpper().Trim();
                rc.RegularCount = 0;
                regularcount.Add(rc); }
            if (!regularcount.Any(x => x.Regulator == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.BBL).ToUpper().Trim()))
            {
                RegulatorCount rc = new RegulatorCount();
                rc.Regulator = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.BBL).ToUpper().Trim();
                rc.RegularCount = 0;
                regularcount.Add(rc);
            }
            if (!regularcount.Any(x => x.Regulator == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.CBE).ToUpper().Trim()))
            {
                RegulatorCount rc = new RegulatorCount();
                rc.Regulator = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.ABRA).ToUpper().Trim();
                rc.RegularCount = 0;
                regularcount.Add(rc);
            }
            if (!regularcount.Any(x => x.Regulator == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.CORP).ToUpper().Trim()))
            {
                RegulatorCount rc = new RegulatorCount();
                rc.Regulator = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.CORP).ToUpper().Trim();
                rc.RegularCount = 0;
                regularcount.Add(rc);
            }
            if (!regularcount.Any(x => x.Regulator == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.OPLA).ToUpper().Trim()))
            {
                RegulatorCount rc = new RegulatorCount();
                rc.Regulator = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.OPLA).ToUpper().Trim();
                rc.RegularCount = 0;
                regularcount.Add(rc);
            }
            return await Task.FromResult(regularcount.AsQueryable());
        }

    }
}
