using System;
using System.Collections.Generic;
using System.Linq;
using BusinessCenter.Common;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using Omu.ValueInjecter;
namespace BusinessCenter.Data.Implementation
{
    public class SubmissionIndividualRepository : GenericRepository<SubmissionIndividual>, ISubmissionIndividualRepository
    {
        private readonly ISubmissionMasterRepository _submissionMasterRepository;
         public SubmissionIndividualRepository(IUnitOfWork context,ISubmissionMasterRepository submissionMasterRepository)
            : base(context)
         {
             _submissionMasterRepository = submissionMasterRepository;
         }
        /// <summary>
        /// This method is used to Get Submission Individual based on Enitity Id.
        /// </summary>
        /// <param name="enitityid"></param>
        /// <returns></returns>
         public IEnumerable<SubmissionIndividual> FindByID(int enitityid)
        {
            var submissionIndividual = FindBy(x => x.SubmindividualId == enitityid);
            return submissionIndividual;
        }
        /// <summary>
        /// This method is used to Get Specific Individual Agent Details based on Application Unique Id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Retrun Submission Individual Details</returns>
         public IEnumerable<SubmissionIndividualEntity> GetSubmissionIndividualData(ChecklistModel checklistModel)
         {
            
            try
            {
                var individualData = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == checklistModel.MasterId);
                List<SubmissionIndividualEntity> subIndividuals = new List<SubmissionIndividualEntity>();
                foreach (var item in individualData)
                {
                    SubmissionIndividualEntity entity = new SubmissionIndividualEntity();
                    entity.SubmindividualId = item.SubmindividualId;
                    entity.City = (item.City ?? "").Trim();
                    entity.CompanyBusinessLicense = (item.CompanyBusinessLicense ?? "").Trim();
                    entity.Country = (item.Country ?? "").Trim();
                    string tt = String.Format("{0:MM/dd/yyyy}", item.DateofBirth);
                    entity.DateofBirth = tt == "01/01/1900" ? "" : tt.Replace("-", "/");
                    entity.EyeColor = (item.EyeColor ?? "").Trim();
                    entity.FirstName = (item.FirstName ?? "").Trim();
                    entity.HairColor = (item.HairColor ?? "").Trim();
                    string[] splitheight = item.Height.Split('-');
                    entity.Height = splitheight[0].ToString();
                    entity.HeightIn = splitheight[1].ToString();
                    entity.CompanyName = (item.CompanyName ?? "").Trim();
                    entity.MiddleName = (item.MiddleName ?? "").Trim();
                    entity.IdentificationCard = (item.IdentificationCard ?? "").Trim();
                    entity.LastName = (item.LastName ?? "").Trim();
                    entity.MasterId = (item.MasterId ?? "").Trim();
                    entity.State_Province = (item.State_Province ?? "").Trim();
                    entity.StateofIssuance = (item.StateofIssuance ?? "").Trim();
                    string ttt = String.Format("{0:MM/dd/yyyy}", item.ExpirationDate);
                    entity.ExpirationDate = ttt == "01/01/1900" ? "" : ttt.Replace("-", "/");
                    entity.CreatedDate = item.CreatedDate;
                    entity.UpdatedDate = item.UpdatedDate;
                    entity.Weight = item.Weight;
                    subIndividuals.Add(entity);
                }
                return subIndividuals;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
         }
        /// <summary>
        /// This method is used to validate the Individual Data is there or not based on Application Unique Id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
         public bool ValidateSubmission(string  masterId)
         {
            bool getCheckValidate = false;
            var individualData = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId);
            var submissionIndividuals = individualData as IList<SubmissionIndividual> ?? individualData.ToList();
            if (submissionIndividuals.Any())
            {
                var checkDrivingLicenseDate = submissionIndividuals.FirstOrDefault();
                if (checkDrivingLicenseDate != null)
                {
                    var drivingLicenseDate = Convert.ToDateTime(checkDrivingLicenseDate.ExpirationDate.ToString());
                    var currentDate = Convert.ToDateTime(System.DateTime.Now.ToShortDateString().ToString());
                    if (drivingLicenseDate >= currentDate)
                    {
                        getCheckValidate = true;
                    }
                }
            }
            return getCheckValidate;
         }
        /// <summary>
        /// This method is used to Insert Business Individual Data based on User Inputs.
        /// </summary>
        /// <param name="individualEntity"></param>
        /// <returns>Retrun Number Result</returns>
         public int InsertUpdateSubmissionIndividual(SubmissionIndividualEntity individualEntity)
         {
             bool validate = false;
            int individualid = 0;
             var result =FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == individualEntity.MasterId);
             var submissionIndividuals = result as IList<SubmissionIndividual> ?? result.ToList();
             validate = submissionIndividuals.Any();
             if (validate == false)
             {
                 var submissionIndividualDetails = new SubmissionIndividual();
                 submissionIndividualDetails.InjectFrom(individualEntity);
                 submissionIndividualDetails.Height = individualEntity.Height + "-" + individualEntity.HeightIn;
                     submissionIndividualDetails.DateofBirth = individualEntity.DateofBirth == null ?  Convert.ToDateTime("01-01-1900") :
                          individualEntity.DateofBirth == "" ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(individualEntity.DateofBirth.Replace("/","-"));
                     submissionIndividualDetails.ExpirationDate = individualEntity.ExpirationDate == null ? Convert.ToDateTime("01-01-1900") :
                         individualEntity.ExpirationDate == "" ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(individualEntity.ExpirationDate.Replace("/", "-"));
                Add(submissionIndividualDetails);
                 Save();
                 individualid= submissionIndividualDetails.SubmindividualId;
             }
             else
             {
                 var submissionIndividualDetails = new SubmissionIndividual();
                 submissionIndividualDetails.InjectFrom(individualEntity);
                 submissionIndividualDetails.Height = individualEntity.Height + "-" + individualEntity.HeightIn;
                 submissionIndividualDetails.SubmindividualId = individualEntity.SubmindividualId;
                     submissionIndividualDetails.DateofBirth = individualEntity.DateofBirth == null ? Convert.ToDateTime("01-01-1900") :
                       individualEntity.DateofBirth == "" ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(individualEntity.DateofBirth.Replace("/", "-"));
                submissionIndividualDetails.ExpirationDate = individualEntity.ExpirationDate == null ? Convert.ToDateTime("01-01-1900") :
                     individualEntity.ExpirationDate == "" ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(individualEntity.ExpirationDate.Replace("/", "-"));
               Update(submissionIndividualDetails, individualEntity.SubmindividualId);
                 Save();
                 individualid= submissionIndividualDetails.SubmindividualId;
             }
            //string ownername = individualEntity.FirstName + " " + individualEntity.LastName;
            //_submissionMasterRepository.UpdateBusinessOwner(individualEntity.MasterId, ownername);
            return individualid;
         }

         //public virtual void SaveChanges()
         //{
         //    Context.SaveChanges();
         //}

        /// <summary>
        /// This method is used to Delete Individual Data based on Application Unique Id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Retrun Bool Result</returns>
         public bool SubmissionIndividualDelete(string masterId)
         {
            var result = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == masterId).ToList();
            if (result.Count > 0)
            {
                Delete(result.FirstOrDefault());
                Save();
                return true;
            }
            return false;
         }
    }
}
