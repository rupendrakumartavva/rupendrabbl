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
    public class SubmissionTaxRevenueService : ISubmissionTaxRevenueService
    {
        protected ISubmissionTaxRevenueRepository _repository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        public SubmissionTaxRevenueService(ISubmissionTaxRevenueRepository repo)
        {
            _repository = repo;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionTaxRevenu"></param>
        /// <returns></returns>
        public IEnumerable<SubmissionTaxRevenue> FindByTaxRevenueNumber(SubmissionTaxRevenuEntity submissionTaxRevenu)
        {
            var commondata = _repository.FindByTaxRevenueNumber(submissionTaxRevenu);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionTaxRevenu"></param>
        /// <returns></returns>
        public bool SubmissionTaxRevenuInsertUpdate(SubmissionTaxRevenuEntity submissionTaxRevenu)
        {
            var commandata = _repository.SubmissionTaxRevenuInsertUpdate(submissionTaxRevenu);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionTaxRevenu"></param>
        /// <returns></returns>
        public bool DeleteSubmissionTaxandRevenue(SubmissionTaxRevenuEntity submissionTaxRevenu)
        {
            var commandata = _repository.DeleteSubmissionTaxandRevenue(submissionTaxRevenu.MasterId);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public bool DeleteTaxandRevenue(string masterId)
        {
            var commandata = _repository.DeleteTaxandRevenue(masterId);
            return commandata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public IEnumerable<SubmissionTaxRevenue> FindByID(string masterId)
        {
            var commondata = _repository.FindByID(masterId);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public bool TaxAndRevenueUpdate(string masterId)
        {
            var commandata = _repository.UpdateTaxAndRevenue(masterId);
            return commandata;
        }
    }
}