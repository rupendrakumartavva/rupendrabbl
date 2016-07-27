using System;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System.Collections.Generic;
using System.Linq;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Data.Implementation
{
    public class CorpRespository : GenericRepository<DCBC_ENTITY_CORP>, ICorpRespository
    {
        public CorpRespository(IUnitOfWork context)
            : base(context)
        {
          
        }
        /// <summary>
        /// This method is used to retrive enitre Corporate Registration(CORP) records.
        /// </summary>
        /// <returns>All CORP records</returns>
        public IEnumerable<DCBC_ENTITY_CORP> GeCorpLookupAll()
        {
            return GetAll().AsQueryable();
        }
       

        /// <summary>
        ///This method is used to retrieve a particular CORP record based on any dynamic column. 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>particular Corporate Registration</returns>
        public new IQueryable<DCBC_ENTITY_CORP> FindBy(System.Linq.Expressions.Expression<Func<DCBC_ENTITY_CORP, bool>> predicate)
        {
            try
            {
                var corpdata = DbSet.Where(predicate).AsQueryable();
                return corpdata;
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// This method is used to retrieve a particular CORP record, given the File Number.
        /// </summary>
        /// <param name="fileNumber"></param>
        /// <returns></returns>
        public IEnumerable<DCBC_ENTITY_CORP> FindByFileNumber(string fileNumber)
        {
            try
            {
                fileNumber = (fileNumber ?? "").Trim();
                var corpdata = FindBy(x => x.FileNumber == fileNumber).OrderBy(x=> x.LAST_UPDATE);
              
                return corpdata;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// This method is used to get the status of particular Corporation data based on File number and last updated.
        /// </summary>
        /// <param name="corporationdetails"></param>
        /// <returns>Return Status String</returns>
        public string CorpOnlineSearch(CorporationDetails corporationdetails)
        {
            try
            {
                string status;
                var corpdata = FindByFileNumber(corporationdetails.FileNumber).OrderBy(x => x.LAST_UPDATE).ToList();
                if (corpdata.Count != 0)
                {
                    status = (corpdata.FirstOrDefault().EntityStatus??"").Trim();
                }
                else
                {
                    status = "NoData";
                }
                return status;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

  