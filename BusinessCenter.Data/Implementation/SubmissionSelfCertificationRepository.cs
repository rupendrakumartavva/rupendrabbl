using System;
using System.Collections.Generic;
using System.Linq;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;

namespace BusinessCenter.Data.Implementation
{
    public class SubmissionSelfCertificationRepository : GenericRepository<SubmissionSelfCertification>, ISubmissionSelfCertificationRepository
    {
        protected ISubmissionMasterApplicationChcekListRepository SubmissionApplicationCheckListRepo;
        public SubmissionSelfCertificationRepository(IUnitOfWork context, ISubmissionMasterApplicationChcekListRepository submissionMasterApplicationChcekListRepository)
            : base(context)
        {
            SubmissionApplicationCheckListRepo = submissionMasterApplicationChcekListRepository;
        }
        /// <summary>
        /// This method is used to get all submission self certification data
        /// </summary>
        /// <returns>Return submission self certification</returns>
        public IEnumerable<SubmissionSelfCertification> GetSelfCertificationDetails()
        {
            return GetAll().AsQueryable();
        }
        /// <summary>
        /// This method is used to get all submission self certification data based on master id
        /// </summary>
        /// <param name="submissionSelfCertification"></param>
        /// <returns>Return submission self certification</returns>
        public IEnumerable<SubmissionSelfCertification> GetSelfCertificationOnMasterId(SubmissionSelfCertification submissionSelfCertification)
        {
            return FindBy(x => x.MasterId == submissionSelfCertification.MasterId);
        }
        /// <summary>
        /// This method is used to insert/update submission self certification based on submissionselfcertification.
        /// </summary>
        /// <param name="submissionSelfCertification"></param>
        /// <returns>Return bool value</returns>
        public bool InsertUpdateSelfCertification(SubmissionSelfCertification submissionSelfCertification)
        {
            try
            {
                var getSubmissionSelfCertification = (FindBy(x => x.MasterId == submissionSelfCertification.MasterId));
                if (getSubmissionSelfCertification.ToList().Count == 0)
                {
                    submissionSelfCertification.SelfCertificationId = Guid.NewGuid().ToString();
                    submissionSelfCertification.CreatedDate = System.DateTime.Now;
                    submissionSelfCertification.SelfCertificationOn =
                        Convert.ToDateTime(submissionSelfCertification.SelfCertificationOn);
                    submissionSelfCertification.IsAgree = submissionSelfCertification.IsAgree;
                    Add(submissionSelfCertification);
                    Save();
                    SubmissionApplicationCheckListRepo.UpdateChcekList("SELFCERTIFICATION", true, submissionSelfCertification.MasterId);

                    return true;
                }
                else
                {
                    var getSelfCertificationId = getSubmissionSelfCertification.FirstOrDefault().SelfCertificationId;
                    submissionSelfCertification.UpdatedDate = System.DateTime.Now;
                    submissionSelfCertification.IsAgree = submissionSelfCertification.IsAgree;
                    Update(submissionSelfCertification, getSelfCertificationId);
                    Save();
                    SubmissionApplicationCheckListRepo.UpdateChcekList("SELFCERTIFICATION", true, submissionSelfCertification.MasterId);
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// This method is used delete submission self certification based on master id
        /// </summary>
        /// <param name="submissionSelfCertification"></param>
        /// <returns>Return bool value</returns>
        public bool DeleteSelfCertification(SubmissionSelfCertification submissionSelfCertification)
        {
            try
            {
                var getSubmissionSelfCertification = (FindBy(x => x.MasterId == submissionSelfCertification.MasterId)).ToList();
                if (getSubmissionSelfCertification.ToList().Count != 0)
                {
                    submissionSelfCertification.SelfCertificationId =
                        getSubmissionSelfCertification.FirstOrDefault().SelfCertificationId;
                    Delete(getSubmissionSelfCertification.FirstOrDefault());
                    Save();
                    SubmissionApplicationCheckListRepo.UpdateChcekList("SELFCERTIFICATION", false, submissionSelfCertification.MasterId);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
        /// <summary>
        /// This method is used to Update is agree for submission self certification based on master  id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public bool UpdateSelfCertification(string masterId)
        {
            try
            {
                var getSubmissionSelfCertification = (FindBy(x => x.MasterId.Trim() == masterId.Trim())).ToList();
                if (getSubmissionSelfCertification.ToList().Count != 0)
                {
                    var updateselfcertification=getSubmissionSelfCertification.Single();
                    updateselfcertification.IsAgree = false;
                    Update(updateselfcertification, updateselfcertification.SelfCertificationId);
                    Save();
                    SubmissionApplicationCheckListRepo.UpdateChcekList("SELFCERTIFICATION", false, masterId);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}