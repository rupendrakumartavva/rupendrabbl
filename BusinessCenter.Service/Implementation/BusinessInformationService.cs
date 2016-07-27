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
    public class BusinessInformationService : IBusinessInformationService
    {
        private readonly IBusinessInformationRepository _businessInformationRepository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="businessInformationRepository"></param>
        public BusinessInformationService(IBusinessInformationRepository businessInformationRepository)
        {
            _businessInformationRepository = businessInformationRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<BusinessLicense> GetSubmissionData()
        {
            var commandata = _businessInformationRepository.GetSubmissionData();
            return commandata.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="businessLicense"></param>
        /// <returns></returns>
        public IEnumerable<BblLicenseView> FindByLicenseNumber(BusinessLicense businessLicense)
        {
            var commandata = _businessInformationRepository.FindByLicenseNumber(businessLicense);
            return commandata;
        }
    }
}