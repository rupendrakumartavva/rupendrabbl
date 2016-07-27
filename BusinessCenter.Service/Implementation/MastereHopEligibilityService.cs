using BusinessCenter.Common;
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
    public class MastereHopEligibilityService : IMastereHopEligibilityService
    {
        protected IMastereHopEligibilityRepository Mehopepository;
        protected ISubmissionEHOPEligibilityRepository SubEHopRepository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="mehopepository"></param>
        /// <param name="subEHopRepository"></param>
        public MastereHopEligibilityService(IMastereHopEligibilityRepository mehopepository, ISubmissionEHOPEligibilityRepository subEHopRepository)
        {
            Mehopepository = mehopepository;
            SubEHopRepository = subEHopRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ehopModel"></param>
        /// <returns></returns>
        public IEnumerable<MastereHopEligibilityEntity> GeMastereHop(EhopModel ehopModel)
        {
            var getData = Mehopepository.GeMastereHopEligibility();
            int typevalue = ValidateEhopEligibility(ehopModel);
            bool checkedReturn = false;
            if (typevalue == 0)
            {
                checkedReturn = false;
            }
            else
            {
                checkedReturn = true;
            }
            var getMasterEhop = getData.Select(item => new MastereHopEligibilityEntity
            {
                Id = item.Id,
                Name = item.Name,
                GetChcekedItem = checkedReturn,
                TypeId = typevalue
            }).ToList();
            return getMasterEhop.AsEnumerable();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ehopModel"></param>
        /// <returns></returns>
        public int ValidateEhopEligibility(EhopModel ehopModel)
        {
            return SubEHopRepository.ValidateEhopEligibility(ehopModel);
        }
    }
}