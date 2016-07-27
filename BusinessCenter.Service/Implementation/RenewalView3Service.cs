using BusinessCenter.Data;
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
    public class RenewalView3Service : IRenewalView3Service
    {
        private readonly IRenewalView3Repository _renewalView3Repository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="renewalView3Repository"></param>
        public RenewalView3Service(IRenewalView3Repository renewalView3Repository)
        {
            _renewalView3Repository = renewalView3Repository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<RenewalLicense> GetRenewData()
        {
            var commandata = _renewalView3Repository.GetRenewData();
            return commandata.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="businessLicense"></param>
        /// <returns></returns>
        public IEnumerable<BblLicenseView3> FindByLicenseNumber(RenewalLicense businessLicense)
        {
            var commandata = _renewalView3Repository.FindByLicenseNumber(businessLicense);
            return commandata.ToList();
        }
    }
}