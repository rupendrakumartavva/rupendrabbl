using BusinessCenter.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Service.Interface;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Service.Implementation
{
    public class SubmissionIndividualService : ISubmissionIndividualService
    {
        protected ISubmissionIndividualRepository IndividualServiceRep;

        /// <summary>
        ///
        /// </summary>
        /// <param name="individualServiceRep"></param>
        public SubmissionIndividualService(ISubmissionIndividualRepository individualServiceRep)
        {
            IndividualServiceRep = individualServiceRep;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="individualEntity"></param>
        /// <returns></returns>
        public int InsertUpdateSubmissionIndividual(SubmissionIndividualEntity individualEntity)
        {
            return IndividualServiceRep.InsertUpdateSubmissionIndividual(individualEntity);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public bool ValidateSubmission(string masterId)
        {
            return IndividualServiceRep.ValidateSubmission(masterId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>

        public IEnumerable<SubmissionIndividualEntity> GetSubmissionIndividualData(ChecklistModel masterId)
        {
            var getdata = IndividualServiceRep.GetSubmissionIndividualData(masterId);
            // List<SubmissionIndividualEntity> getSubmissionIndividual= getdata.ToList().Select(items => new SubmissionIndividualEntity {MasterId = items.MasterId}).ToList();

            return getdata.AsEnumerable();
            //var getData = (from abradata in (
            //          FindBy(x => x.MasterId == masterId))
            //               select abradata);

            //return getData;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>

        public bool SubmissionIndividualDelete(string masterId)
        {
            return IndividualServiceRep.SubmissionIndividualDelete(masterId);
        }
    }
}