using BusinessCenter.Data;
using BusinessCenter.Data.Implementation;
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
    public class CofoHopDetailsService : ICofoHopDetailsService
    {
        // protected ICofoHopDetailsRepository _repository;
        protected IDCBC_ENTITY_Cof_ORepository _coforepository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="_coforepo"></param>
        public CofoHopDetailsService(IDCBC_ENTITY_Cof_ORepository _coforepo)
        {
            //  _repository = repo;
            _coforepository = _coforepo;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cofoHopDetailsModel"></param>
        /// <returns></returns>
        public IEnumerable<CofoHopDetailsModel> FindByNumberandDateofIssue(CofoHopDetailsModel cofoHopDetailsModel)
        {
            var commondata = _coforepository.FindByNumberandDateofIssue(cofoHopDetailsModel);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public List<StreetDetails> DropDownsBind()
        {
            var commondata = _coforepository.DropDownBind();
            return commondata;
        }

        //public bool InsertCofoHopDetails(CofoHopDetailsModel cofoHopDetailsModel)
        //{
        //    var commondata = _repository.InsertCofoHopDetails(cofoHopDetailsModel);
        //    return commondata;
        //}

        //public bool UpdateCofoHopDetails(CofoHopDetailsModel cofoHopDetailsModel)
        //{
        //    var commondata = _repository.UpdateCofoHopDetails(cofoHopDetailsModel);
        //    return commondata;
        //}
        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public List<CofoHopDetailsModel> GetSubmissionCofoOrHopDetails(string masterId)
        {
            var commondata = _coforepository.GetSubmissionCofoOrHopDetails(masterId);
            return commondata;
        }

        //List<CofoHopDetailsModel> GetSubmissionCofoOrHopDetails(string masterId)
    }
}