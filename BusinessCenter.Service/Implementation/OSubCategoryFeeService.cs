using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCenter.Service.Implementation
{
    public class OSubCategoryFeeService : IOSubCategoryFeeService
    {
        private readonly IOSubCategoryFeesRepository _oSubCategoryFeesRepository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="oSubCategoryFeesRepository"></param>
        public OSubCategoryFeeService(IOSubCategoryFeesRepository oSubCategoryFeesRepository)
        {
            _oSubCategoryFeesRepository = oSubCategoryFeesRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryPhysicallocation"></param>
        /// <returns></returns>
        public bool UpdateSubFee(PrimaryPhysicallocation primaryPhysicallocation)
        {
            var result = _oSubCategoryFeesRepository.UpdateSubFee(primaryPhysicallocation);
            return result;
        }
    }
}