using BusinessCenter.Data;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Service.Implementation
{
    public class EtlAddressAndParcelService : IEtlAddressAndParcelService
    {
        protected IEtlAddressAndParcelRepository EtlAddressAndParcelRepository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="etlAddressAndParcelRepository"></param>
        public EtlAddressAndParcelService(IEtlAddressAndParcelRepository etlAddressAndParcelRepository)
        {
            EtlAddressAndParcelRepository = etlAddressAndParcelRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="enterAddress"></param>
        /// <returns></returns>
        public IList<AddressDetails> ListEtlAddressDetails(string enterAddress)
        {
            return EtlAddressAndParcelRepository.ListEtlAddressDetails(enterAddress);
        }
    }
}