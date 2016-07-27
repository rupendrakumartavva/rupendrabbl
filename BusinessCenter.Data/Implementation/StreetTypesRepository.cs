using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class StreetTypesRepository : GenericRepository<StreetTypes>, IStreetTypesRepository
    {
        public StreetTypesRepository(IUnitOfWork context) : base(context)
        {

        }
        /// <summary>
        /// This method is used to get All streetTypes with code in ascending order.
        /// </summary>
        /// <returns>List of Street Types</returns>
        public IEnumerable<StreetTypes> AllStreetTypes()
        {
           return GetAll().AsQueryable().OrderBy(x=>x.StreetType);
        }

      /// <summary>
      /// This method is used to retrive specific Street Type data based on Street Type Id.
      /// </summary>
      /// <param name="streetTypeId"></param>
      /// <returns>Speicific Street Type Data</returns>
        public IEnumerable<StreetTypes> FindByStreetTypeId(int streetTypeId)
        {
            return from streettypes in (FindBy(
                       x => x.StreetTypeId == streetTypeId))
                   select streettypes;
        }
        /// <summary>
        /// This method is used to retrive specific Street Type data based on Street Type.
        /// </summary>
        /// <param name="streetType"></param>
        /// <returns>Speicific Street Type Data</returns>
        public IEnumerable<StreetTypes> FindStreetIdbyType(string streetType)
        {
            return from streettypes in (FindBy(
                        x => x.StreetType.ToUpper() == streetType.ToUpper()))
                   select streettypes;
        }
        /// <summary>
        /// This method is used to retrive specific Street Type data based on Street Code.
        /// </summary>
        /// <param name="streetcode"></param>
        /// <returns>Speicific Street Type Data</returns>
        public IEnumerable<StreetTypes> FindStreetIdbyCode(string streetcode)
        {
            return from streettypes in (FindBy(
                        x => x.StreetCode == streetcode))
                   select streettypes;
        }
    }
}
