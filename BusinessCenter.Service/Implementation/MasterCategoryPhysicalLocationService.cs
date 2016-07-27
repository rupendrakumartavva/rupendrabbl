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
    public class MasterCategoryPhysicalLocationService : IMasterCategoryPhysicalLocationService
    {
        protected IMasterCategoryPhysicalLocationRepository _repository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="repo"></param>
        public MasterCategoryPhysicalLocationService(IMasterCategoryPhysicalLocationRepository repo)
        {
            _repository = repo;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MasterCategoryPhysicalLocation> GetAllCategoryPhysicallocations()
        {
            var commondata = _repository.AllCategoryPhysicallocations();
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionApplication"></param>
        /// <returns></returns>
        public IEnumerable<MasterCategoryPhysicalLocation> FindCategoryPhysicallocationsById(SubmissionApplication submissionApplication)
        {
            var commondata = _repository.FindByID(submissionApplication);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="submissionApplication"></param>
        /// <returns></returns>
        public IEnumerable<SubmissionApplication> GetAllScreeningQuestions(SubmissionApplication submissionApplication)
        {
            var commondata = _repository.AllScreeningQuestions(submissionApplication);
            return commondata;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryPhysicallocation"></param>
        /// <returns></returns>
        public string InsertUpdatePhysicalLocation(PrimaryPhysicallocation primaryPhysicallocation)
        {
            return _repository.InsertUpdatePhysicalLocation(primaryPhysicallocation);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="primaryCategoryId"></param>
        /// <returns></returns>
        public IEnumerable<MasterCategoryPhysicalLocation> FindCategoryPhysicallocationsById(string primaryCategoryId)
        {
            var commondata = _repository.FindCategoryID(primaryCategoryId);
            return commondata;
        }
    }
}