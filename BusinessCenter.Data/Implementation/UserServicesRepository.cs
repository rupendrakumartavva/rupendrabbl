using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BusinessCenter.Data.Models;

namespace BusinessCenter.Data.Implementation
{
    public class UserServicesRepository : GenericRepository<UserService>, IUserServicesRepository
    {
        public IAbraRepository Abrarep;
        public IBblRepository Bblrep;
        public ICbeRepository Cberep;
        public IOplaRepository Oplarep;
        public ICorpRespository Corprep;
        public MySaveSearchFilter mssf;
        public UserServicesRepository(IUnitOfWork context, IAbraRepository iabra, IBblRepository ibbl, ICbeRepository icbe, IOplaRepository iopla,ICorpRespository icorp)
            : base(context)
        {
            Abrarep = iabra;
            Bblrep = ibbl;
            Cberep = icbe;
            Oplarep = iopla;
            Corprep = icorp;
        }
        /// <summary>
        /// This method is used to get all User Service Data
        /// </summary>
        /// <returns>Get User Service Data</returns>
        public IEnumerable<UserService> GetUserServiceAll()
        {
            return GetAll().AsQueryable().AsNoTracking();
        }
        /// <summary>
        /// This method is get the  particular Users Service Count  based on the user id, data source, entityid, license number,company name
        /// </summary>
        /// <param name="userService"></param>
        /// <returns>Interger value</returns>
        public int UserServiceAdd(UserServiceModel userService)
        {
            var result = 0;
             var count = FindBy(x => x.UserId==userService.UserId && x.DATA_SOURCE == userService.DataSource &&
                                     x.DCBC_ENTITY_ID == userService.EntityId &&
                                     x.LicenseNumber == userService.LicenseNumber &&
                                     x.CompanyName == userService.CompanyName).Count();
            if (count!=0) return result;
            var userServiceAdd = new UserService
            {
                DCBC_ENTITY_ID = userService.EntityId,
                DATA_SOURCE = userService.DataSource,
                Status = "Active",
                CreatedBy = userService.CreatedBy,
                CreatedDate = System.DateTime.Now,
                UserId = userService.UserId,
                LicenseNumber = userService.LicenseNumber,
                CompanyName = userService.CompanyName
            };
            Add(userServiceAdd);
            Save();
            result = 1;
            return result;
        }

        #region not using this Code
       // private static Expression<Func<UserService, bool>> UserValidate(
       //  string dataSourceInput)
       // {
       //     return lookUpIndex => lookUpIndex.UserId == dataSourceInput;
       // }
       // private static Expression<Func<UserService, bool>> UserDatasource(
       // string dataSourceInput)
       // {
       //     return lookUpIndex => lookUpIndex.DATA_SOURCE == dataSourceInput;
       // }
       // private static Expression<Func<UserService, bool>> UserDcbcentityid(
       // int? dataSourceInput)
       // {
       //     return lookUpIndex => lookUpIndex.DCBC_ENTITY_ID == dataSourceInput;
       // }

       // private static Expression<Func<UserService, bool>> LicenseNumber(
       //string  dataSourceInput)
       // {
       //     return lookUpIndex => lookUpIndex.LicenseNumber == dataSourceInput;
       // }
       // private static Expression<Func<UserService, bool>> CompanyName(
       //string  dataSourceInput)
       // {
       //     return lookUpIndex => lookUpIndex.CompanyName == dataSourceInput;
       // }

        #endregion

        /// <summary>
        ///This method is used to get Data of particular data 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Get User Service Data</returns>
        public new IQueryable<UserService> FindBy(System.Linq.Expressions.Expression<Func<UserService, bool>> predicate)
        {
            var query = DbSet.Where(predicate).AsQueryable();
            return query;
        }

        /// <summary>
        /// This method is used to get user service count based on Used Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User service integer count</returns>
        public int UserSaveListCount(string userId)
        {
            var f = FindBy(x => x.UserId == userId).Count();
            return f;
        }
        /// <summary>
        /// This method is used to get User service User data based on Criteria.
        /// </summary>
        /// <param name="searchinput"></param>
        /// <returns>Get User Service Data</returns>
        public IQueryable<CommonData> GetAllData(UserServiceModel searchinput)
        {
            try
            {
                InputCheckedValues checkedValue = new InputCheckedValues();
                string[] parts = searchinput.DisplayType.Split('-');
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
                var getdata = GetSearchData(searchinput);
                var fill = from data in getdata
                           where data.Source == checkedValue.Bbl || data.Source == checkedValue.Abra
                                 || data.Source == checkedValue.Opla || data.Source == checkedValue.Cbe
                         || data.Source == checkedValue.Corp
                           select data;
                return fill;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// This method is used to Get Count based on Userservice Model
        /// </summary>
        /// <param name="userService"></param>
        /// <returns>Retrun Look count data</returns>
        public IEnumerable<SearchDataViewModel> GetCount(UserServiceModel userService)
        {
             mssf=new MySaveSearchFilter();
             var dataa = mssf.GetUserServiceData(userService, GetUserServiceAll().AsQueryable().AsNoTracking());
             var lookuptable = mssf.GetCountRecords(dataa.ToList());
            return lookuptable.AsQueryable();

        }
        /// <summary>
        /// This method is used to Get Common data based on user service Model
        /// </summary>
        /// <param name="userService"></param>
        /// <returns>Return Common Data</returns>
        public IQueryable<CommonData> GetSearchData(UserServiceModel userService)
        {
            IQueryable<CommonData> dagta;
            SearchResultData srd = new SearchResultData();
            mssf = new MySaveSearchFilter();
          var userdata=  mssf.GetUserServiceData(userService, GetUserServiceAll().AsQueryable().AsNoTracking());
          var distinctcount = (from searchcount in userdata select new { searchcount.DCBC_ENTITY_ID,searchcount.DATA_SOURCE }).AsNoTracking().Distinct();
            InputCheckedValues checkedValue = new InputCheckedValues();
             string[] parts = userService.DisplayType.Split('-');
             foreach (var part in distinctcount)
             {
                 switch (part.DATA_SOURCE.ToUpper().Trim().ToUpper())
                 {
                     case "BBL":
                         srd.BblData = null;
                         var getBblEntityList =
                            (from x in
                                 distinctcount.Where(x => x.DATA_SOURCE.ToUpper().Trim() == "BBL")
                             select x.DCBC_ENTITY_ID).ToList();
                         srd.BblData = Bblrep.FindBy(x => getBblEntityList.Contains(x.DCBC_ENTITY_ID)).ToList();
                         break;
                     case "CORP":
                         srd.CorpData = null;
                         var getCorpEntityList =
                           (from x in
                                distinctcount.Where(x => x.DATA_SOURCE.ToUpper().Trim() == "CORP")
                            select x.DCBC_ENTITY_ID).ToList();
                         srd.CorpData = Corprep.FindBy(x => getCorpEntityList.Contains(x.DCBC_ENTITY_ID)).ToList();
                         break;
                     case "OPLA":
                         srd.OplaData = null;
                         var getOplaEntityList =
                           (from x in
                                distinctcount.Where(x => x.DATA_SOURCE.ToUpper().Trim() == "OPLA")
                            select x.DCBC_ENTITY_ID).ToList();
                         srd.OplaData = Oplarep.FindBy(x => getOplaEntityList.Contains(x.DCBC_ENTITY_ID)).ToList();
                         break;
                     case "CBE":
                         srd.CbeData = null;
                         var getcbeEntityList =
                          (from x in
                               distinctcount.Where(x => x.DATA_SOURCE.ToUpper().Trim() == "CBE")
                           select x.DCBC_ENTITY_ID).ToList();
                         srd.CbeData = Cberep.FindBy(x => getcbeEntityList.Contains(x.DCBC_ENTITY_ID)).ToList();
                         break;
                     case "ABRA":
                         srd.AbraData = null;
                         var getabraEntityList =
                        (from x in
                             distinctcount.Where(x => x.DATA_SOURCE.ToUpper().Trim() == "ABRA")
                         select x.DCBC_ENTITY_ID).ToList();
                         srd.AbraData = Abrarep.FindBy(x => getabraEntityList.Contains(x.DCBC_ENTITY_ID)).ToList();
                         break;
                     //case "ALL":
                     //    srd.BblData = null;
                     //    srd.BblData = Bblrep.GeBblLookupAll().ToList();
                     //    srd.OplaData = null;
                     //    srd.OplaData = Oplarep.GetOplaLookupAll().ToList();
                     //    srd.CorpData = null;
                     //    srd.CorpData = Corprep.GeCorpLookupAll().ToList();
                     //    srd.CbeData = null;
                     //    srd.CbeData = Cberep.GetCbeLookupAll().ToList();
                     //    srd.AbraData = null;
                     //    srd.AbraData = Abrarep.GeArbraLookupAll().ToList();
                     //    break;
                 }
             }
             dagta = mssf.GetAllData(userService, userdata, srd.BblData, srd.OplaData, srd.CorpData, srd.CbeData, srd.AbraData).AsQueryable();
            return dagta;
        }
       
        /// <summary>
        /// This method is used to Delete Particular user data based on  Enitity data and datasource
        /// </summary>
        /// <param name="userService"></param>
        public void  DeleteUserService(UserServiceModel userService)
        {
            var contact = GetAll().AsQueryable().Single(c => c.DCBC_ENTITY_ID == userService.EntityId
                                      && c.DATA_SOURCE == userService.DataSource && c.UserId == userService.UserId);
            Delete(contact);
            Save();
        }
        /// <summary>
        /// This method is used to Delete multiple user data based on  Enitity data and datasource
        /// </summary>
        /// <param name="userService"></param>
        public  void DeleteUserService(List<UserServiceModel> userService)
        {
            foreach (var item in userService)
            {
                var contact = GetAll().AsQueryable().Single(c => c.DCBC_ENTITY_ID == item.EntityId && c.DATA_SOURCE == item.DataSource && c.UserId == item.UserId);
                Delete(contact);
                Save();
            }

        }
        /// <summary>
        /// This method is used to Delete user service whole data based in user id
        /// </summary>
        /// <param name="userId"></param>
        public void DeleteSingleUerService(string userId)
        {
            var users = FindBy(u => u.UserId == userId);
            foreach (var u in users)
            {
                Delete(u);
                Save();
            }
        }
    }
}