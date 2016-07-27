using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class SubmissionEHOPEligibilityRepository : GenericRepository<SubmissionEHOPEligibility>, ISubmissionEHOPEligibilityRepository
    {
        protected IMastereHopEligibilityRepository masterEhoprepo;
        protected ISubmissionMasterApplicationChcekListRepository subcheckRepo;
        private readonly IUserRepository _userRepository;
        protected ISubmissionCofoHopeHopRepository sublocRepo;
        private readonly ISubmissionDocumentRepository _submissionDocumentRepository;
        private readonly IMasterEhopOptionType MasterEhopOptionType;
        //
        public SubmissionEHOPEligibilityRepository(IUnitOfWork context, IMastereHopEligibilityRepository masterEhoprepository,
            ISubmissionMasterApplicationChcekListRepository subcheckRepository,IUserRepository userRepository,
            ISubmissionCofoHopeHopRepository sublocRepository,ISubmissionDocumentRepository submissionDocumentRepository,IMasterEhopOptionType masterEhopOptionType)
            : base(context)
        {
            masterEhoprepo = masterEhoprepository;
            subcheckRepo = subcheckRepository;
            _userRepository = userRepository;
            sublocRepo = sublocRepository;
            _submissionDocumentRepository = submissionDocumentRepository;
            MasterEhopOptionType = masterEhopOptionType;
        }
        /// <summary> 
        /// This method is Insert Ehop Eligibility Data using User Inputs
        /// </summary>
        /// <param name="eligibilityModel"></param>
        /// <returns>Retrun bool Results</returns>
        public bool InsertEHopEligibility(EligibilityModel eligibilityModel)
        {
            bool status = false;
            try
            {
            
                
                var ehopeligible = (from eligible in FindBy(x => x.MasterId == eligibilityModel.MasterId)
                                    select eligible).ToList();
                if (!ehopeligible.Any())
                {
                    _submissionDocumentRepository.DeleteHopcofo(eligibilityModel.MasterId, "Home Occupancy Permit (HOP) Document");
                    SubmissionEHOPEligibility ehopEligble = new SubmissionEHOPEligibility();
                    var EhopIds = eligibilityModel.EhopIds.Remove(eligibilityModel.EhopIds.Length - 1).Split(',');
                    foreach (var id in EhopIds)
                    {
                        ehopEligble.MasterId = eligibilityModel.MasterId;
                        ehopEligble.MasterEhopId = Convert.ToInt32(id);
                        ehopEligble.TypeId = eligibilityModel.TypeId;
                        ehopEligble.CreatedBy = eligibilityModel.UserId;
                        ehopEligble.CreatedDate = System.DateTime.Now;
                        ehopEligble.UpdatedDate = System.DateTime.Now;
                        Add(ehopEligble);
                        Save();
                        status = true;
                    }
                }
                else
                {
                    SubmissionEHOPEligibility ehopEligble = new SubmissionEHOPEligibility();
                     //var EhopIds = eligibilityModel.EhopIds.Remove(eligibilityModel.EhopIds.Length - 1).Split(',');
                     foreach (var id in ehopeligible)
                    {
                        int updateId;
                        SubmissionEHOPEligibility subehop=new SubmissionEHOPEligibility();
                        updateId = Convert.ToInt32(id.SubEhopId);
                        subehop.SubEhopId = id.SubEhopId;
                        subehop.MasterId = id.MasterId;
                        subehop.MasterEhopId = Convert.ToInt32(id.MasterEhopId);
                        subehop.TypeId = eligibilityModel.TypeId;
                        subehop.CreatedDate = id.CreatedDate;
                        subehop.CreatedBy = eligibilityModel.UserId;
                       subehop.UpdatedDate = System.DateTime.Now;
                        Update(subehop, subehop.SubEhopId);
                        Save();
                    }
                    status = true;
                   // }
                }
                CofoHopDetailsModel ehopdetails=new CofoHopDetailsModel();
             
                ehopdetails.MasterId = eligibilityModel.MasterId;
                ehopdetails.Type = "EEHOP";
                ehopdetails.IsValid = true;
               // sublocRepo.DeleteHOP(ehopdetails.MasterId);
                subcheckRepo.UpdateDetails(ehopdetails);
            }
            catch (Exception )
            {

                status = false;
                //return status;
            }
            return status;
        }
        /// <summary>
        /// This method is used to Get Ehop exists or not based on Application Unique Id.
        /// </summary>
        /// <param name="ehopModel"></param>
        /// <returns>Retrun Bool Result</returns>
        public EhopModel MasterHopEligibility(EhopModel ehopModel)
        {
            var ehopeligible = FindBy(x => x.MasterId == ehopModel.MasterId);
            if (ehopeligible.Any())
            {
                ehopModel.IsChecked = true;
            }
            else
            {
                ehopModel.IsChecked = false;
            }
            ehopModel.CheckList = masterEhoprepo.GeMastereHopEligibility().ToList();
            return ehopModel;
        }
        /// <summary>
        /// This method is used to Get ehop Id based on Application Unique Id.
        /// </summary>
        /// <param name="ehopModel"></param>
        /// <returns>Retrun Number Result</returns>
        public int ValidateEhopEligibility(EhopModel ehopModel)
        {
            int result = 0;
            var ehopeligible = FindBy(x => x.MasterId == ehopModel.MasterId).FirstOrDefault();
            if (ehopeligible != null)
            {
                result = Convert.ToInt32(ehopeligible.TypeId);
            }
            return result;
        }
        /// <summary>
        /// This method is used to get ehop data based on unique id
        /// </summary>
        /// <param name="ehopData"></param>
        /// <returns>Return Ehop Data</returns>
        public EhopData EhopData(EhopData ehopData)
        {

            var ehopeligiblity =(from ehop in FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "") == ehopData.MasterID)
                                    join master in masterEhoprepo.GeMastereHopEligibility() on ehop.TypeId equals  master.Id
                                     select new{ master.Name,ehop.CreatedDate,ehop.CreatedBy,ehop.TypeId}).ToList();
            if (ehopeligiblity.Count != 0)
            {
                var getName = MasterEhopOptionType.FindById(Convert.ToInt32(ehopeligiblity.FirstOrDefault().TypeId)).ToList();
                if (getName.Count()!=0)
                    ehopData.OccupationType = getName.FirstOrDefault().EhopOptionName;
                   
                ehopData.CreatedDate =Convert.ToDateTime(ehopeligiblity.FirstOrDefault().CreatedDate).ToString("MM/dd/yyyy") ;
                ehopData.CreatedBy = (ehopeligiblity.FirstOrDefault().CreatedBy ?? "").Trim();
                GeneralBusiness generalBusiness = new GeneralBusiness();
                generalBusiness.MasterId = ehopData.MasterID;
                var ehopaddress = sublocRepo.GetPrimisessAddress(generalBusiness);
                ehopData.Name = (ehopaddress.FirstName ?? "").Trim() + " " + (ehopaddress.MiddleName ?? "").Trim() + " " +
                                (ehopaddress.LastName ?? "").Trim();
                ehopData.PermitNumber = (ehopaddress.FileNumber ?? "").Trim();
               
                ehopData.Address = ehopaddress.BusinessAddressLine3 ?? "".Trim();
                //ehopData.Address = (ehopaddress.BusinessAddressLine1 ?? "").Trim() + " " +
                //                   (ehopaddress.BusinessAddressLine2 ?? "").Trim() + " " +
                //                   (ehopaddress.BusinessAddressLine3 ?? "").Trim() + " " +
                //                   (ehopaddress.Quardrant ?? "").Trim();

                //+" " + (ehopaddress.Unit ?? "").Trim() + " " + (ehopaddress.UnitType ?? "").Trim();
                //                   (ehopaddress.BusinessCity ?? "").Trim() + " " + (ehopaddress.BusinessState ?? "").Trim() + " " +
                //                   (ehopaddress.BusinessCountry ?? "").Trim() + " " + (ehopaddress.ZipCode ?? "").Trim();

                ehopData.PhoneNumber = (ehopaddress.Telphone ?? "").Trim();
                var userdata = _userRepository.FindByID(ehopData.CreatedBy).ToList();
                if (userdata.Count != 0)
                {
                    ehopData.UserName = (userdata.FirstOrDefault().FirstName??"").Trim() + " " + (userdata.FirstOrDefault().LastName??"").Trim();
                }
                ehopData.Lot = "NA";
                ehopData.Square = "NA";

            }
            return ehopData;
        }

    }
}
