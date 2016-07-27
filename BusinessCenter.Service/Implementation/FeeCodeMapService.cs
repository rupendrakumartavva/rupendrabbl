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
    public class FeeCodeMapService : IFeeCodeMapService
    {
        protected IFeeCodeMapRepository feecoderpo;

        /// <summary>
        ///
        /// </summary>
        /// <param name="feeCodeMapRepository"></param>
        public FeeCodeMapService(IFeeCodeMapRepository feeCodeMapRepository)
        {
            feecoderpo = feeCodeMapRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public bool Checkunits(string quantity)
        {
            var result = feecoderpo.Checkunits(quantity);
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryPhysicallocation"></param>
        /// <returns></returns>
        public bool InsertUpdateFee(PrimaryPhysicallocation primaryPhysicallocation)
        {
            var result = feecoderpo.InsertUpdateFee(primaryPhysicallocation);
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryPhysicallocation"></param>
        /// <returns></returns>
        public bool UpdateFeecode(PrimaryPhysicallocation primaryPhysicallocation)
        {
            var result = feecoderpo.UpdateFeecode(primaryPhysicallocation);
            return result;
        }
    }
}