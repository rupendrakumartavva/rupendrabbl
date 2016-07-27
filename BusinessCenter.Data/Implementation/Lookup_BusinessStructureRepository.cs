using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class Lookup_BusinessStructureRepository : GenericRepository<Lookup_BusinessStructure>,ILookup_BusinessStructureRepository
    {
         protected IdentityConnectionContext Context;
         public Lookup_BusinessStructureRepository(IUnitOfWork context)
            : base(context)
        {
           

        }
        /// <summary>
        /// This method is used to retrive enitre BusinessStructure records
        /// </summary>
        /// <returns>All ABRA Records</returns>
        public IEnumerable<Lookup_BusinessStructure> GetBusinessStructureAll()
        {
            var businessStructure= GetAll().AsQueryable();
            return businessStructure;
        }
        /// <summary>
        /// This method is used to retrive particular  Lookup_BusinessStructure based on Business Stucture
        /// </summary>
        /// <param name="businessStructure"></param>
        /// <returns>Retrun Lookup_BusinessStructure</returns>
          public IEnumerable<Lookup_BusinessStructure> FindByStructure(string businessStructure)
        {
            var business= FindBy(x=>x.BusinessStructure==businessStructure);
            return business;
        }
        /// <summary>
        /// This method is used to insert/update Lookup_businessStructure based on user inputs
        /// </summary>
        /// <param name="businessStructure"></param>
        /// <returns>Retrun bool Status</returns>
          public bool InsertUpdateBusienssStrucutureLookUp(BusinessStructureLookUp businessStructure)
          {
              bool status = false;
              try
              {

                  var businessStructureExist = FindBy(x => x.LookUpBusinessStructureId == businessStructure.LookUpBusinessStructureId).ToList();
                  if (businessStructureExist.Count() == 0)
                  {
                      Lookup_BusinessStructure business = new Lookup_BusinessStructure();
                      business.BusinessStructure = (businessStructure.BusinessStructure ?? "").Trim();
                      business.IsManualAddress = businessStructure.IsManualAddress;
                      Add(business);
                      Save();
                      status = true;
                  }
                  else
                  {
                      var updatebusinessStructure = businessStructureExist.SingleOrDefault();
                      updatebusinessStructure.BusinessStructure = (businessStructure.BusinessStructure ?? "").Trim();
                      updatebusinessStructure.IsManualAddress = businessStructure.IsManualAddress;
                      Save();
                      status = true;
                  }
              }
              catch (Exception )
              { status = false; }
              return status;
          }
    }
}
