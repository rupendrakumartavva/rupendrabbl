using BusinessCenter.Common;
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
    public class SubmissionCofoHopeHopAddressService : ISubmissionCofoHopeHopAddressService
    {
        protected ISubmissionCofoHopeHopAddressRepository _repository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        public SubmissionCofoHopeHopAddressService(ISubmissionCofoHopeHopAddressRepository repo)
        {
            _repository = repo;
        }

        //public IEnumerable<SubmissionCofo_Hop_Ehop_Address> FindByID(GeneralBusiness generalBusiness)
        //{
        //    var commondata = _repository.FindByID(generalBusiness);
        //    return commondata;
        //}
        /// <summary>
        ///
        /// </summary>
        /// <param name="generalBusiness"></param>
        /// <returns></returns>
        public IEnumerable<GeneralBusiness> GetCorpBusinessData(GeneralBusiness generalBusiness)
        {
            var commondata = _repository.GetCorpBusinessData(generalBusiness);
            return commondata;
        }

        //public TaxAndReneueInitailDisplay DisplayTaxAndRevenuWithPrimisessDetails(string masterId)
        //{
        //    var commondata = _repository.(masterId);
        //    return commondata;
        //}
    }
}