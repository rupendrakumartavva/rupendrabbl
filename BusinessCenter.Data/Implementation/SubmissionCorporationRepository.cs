using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class SubmissionCorporationRepository : GenericRepository<SubmissionCorporation_Agent>, ISubmissionCorporationRepository
    {
        protected ISubmissionCorporationAgentAddressRepository subCorpAgentRepo;
        protected ISubmissionMasterApplicationChcekListRepository SubmissionChceklistApp;
        protected IMasterRegisterAgentRepository _agentrepo;
        protected ICorpRespository corprepo;
        protected ISubmissionMasterRepository subMasterRepo;
        protected ISubmissionQuestionRepository subquesrepo;

        protected IMasterCountryRepository _MasterCountryRepository;
        protected IMasterStateRepository _masterStateRepository;

        public SubmissionCorporationRepository(IUnitOfWork context, ISubmissionCorporationAgentAddressRepository subCorpAgentRepository,
            ISubmissionMasterApplicationChcekListRepository submissionChceklistApp, IMasterRegisterAgentRepository agentrepo, ISubmissionQuestionRepository subquesrepository,
            ICorpRespository corpRepository, ISubmissionMasterRepository subMasterRepository,
            IMasterCountryRepository masterCountryRepository, IMasterStateRepository masterStateRepository)
            : base(context)
        {
            subCorpAgentRepo = subCorpAgentRepository;
            SubmissionChceklistApp = submissionChceklistApp;
            _agentrepo = agentrepo;
            corprepo = corpRepository;
            subMasterRepo = subMasterRepository;
            subquesrepo = subquesrepository;
            _MasterCountryRepository = masterCountryRepository;
            _masterStateRepository = masterStateRepository;
        }

        /// <summary>
        /// This method is used to Get All Corporation Data.
        /// </summary>
        /// <returns>Return Corporation Agent Result</returns>
        public IEnumerable<SubmissionCorporation_Agent> GetAllCorporations()
        {
            return GetAll().AsQueryable();
        }

        /// <summary>
        /// This method is used to Get Specific Corporation Data based on Application Unique Id.
        /// </summary>
        /// <param name="generalBusiness"></param>
        /// <returns>Return Specific Corporation Data</returns>
        public IEnumerable<SubmissionCorporation_Agent> FindByMasterId(GeneralBusiness generalBusiness)
        {
            var corporationAgent = FindBy(x => x.MasterId.Trim() == generalBusiness.MasterId.Trim());
            return corporationAgent;
        }

        //public static string GetEnumDescription(Enum value)
        //{
        //    var fi = value.GetType().GetField(value.ToString());
        //    var attributes =
        //        (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
        //    return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        //}
        /// <summary>
        /// This method is used to Get Corporation Data using user inputs.
        /// </summary>
        /// <param name="generalBusiness"></param>
        /// <returns>Retrun Corporation Data</returns>
        public IEnumerable<GeneralBusiness> GetCorpBusinessData(GeneralBusiness generalBusiness)
        {
            try
            {
                var corporationdataexist = FindByMasterId(generalBusiness).ToList();
                string corpmodel = string.Empty;
                if (corporationdataexist.Any())
                {
                    var corpdata = corporationdataexist.FirstOrDefault();
                    var getcorporationdata = corprepo.FindByFileNumber(corpdata.FileNumber).ToList();
                    generalBusiness.BusinessStructure = (corpdata.BusinessStructure ?? "").Trim();
                    generalBusiness.CBusinessName = (corpdata.BusinessName ?? "").Trim();
                    generalBusiness.FileNumber = (corpdata.FileNumber ?? "").Trim();
                    if (getcorporationdata.Any())
                    {
                        var corporationdata = getcorporationdata.FirstOrDefault();

                        generalBusiness.EntityStatus = (corporationdata.EntityStatus ?? "").Trim().ToUpper();
                        corpmodel = (corporationdata.ModelType ?? "").Trim();
                    }
                    var corporationAddressDetails = subCorpAgentRepo.FindByTypewithMasterId(corpdata.SubCorporationRegId, generalBusiness.UserType).ToList();
                    if (corporationAddressDetails.Any())
                    {
                        var corpAddressDetails = corporationAddressDetails.FirstOrDefault();
                        generalBusiness.FileNumber = (corpdata.FileNumber ?? "").Trim();
                        generalBusiness.MasterId = (generalBusiness.MasterId ?? "").Trim();
                        generalBusiness.CBusinessName = (corpdata.BusinessName ?? "").Trim();
                        generalBusiness.TradeName = (corpdata.TradeName ?? "").Trim();
                        generalBusiness.BusinessStructure = (corpdata.BusinessStructure ?? "").Trim();
                        generalBusiness.FirstName = (corpAddressDetails.FirstName ?? "").Trim();
                        generalBusiness.LastName = (corpAddressDetails.LastName ?? "").Trim();
                        generalBusiness.MiddleName = (corpAddressDetails.MiddelName ?? "").Trim();
                        generalBusiness.BusinessName = (corpAddressDetails.BusinessName ?? "").Trim();
                        generalBusiness.BusinessAddressLine1 = (corpAddressDetails.Address1 ?? "").Trim();
                        generalBusiness.BusinessAddressLine2 = (corpAddressDetails.Address2 ?? "").Trim();
                        generalBusiness.BusinessAddressLine3 = (corpAddressDetails.Address3 ?? "").Trim();
                        generalBusiness.BusinessCity = (corpAddressDetails.City ?? "").Trim();
                        generalBusiness.BusinessState = GetStateCode(corpAddressDetails.State ?? "", corpAddressDetails.Country ?? "").Trim();
                        generalBusiness.ZipCode = (corpAddressDetails.ZipCode ?? "").Trim();
                        generalBusiness.BusinessCountry = GetCountryFullName(corpAddressDetails.Country ?? "").Trim();
                        generalBusiness.Email = (corpAddressDetails.Email ?? "").Trim();
                        generalBusiness.Telphone = (corpAddressDetails.Telephone ?? "").Trim();
                        generalBusiness.CorpStatus = (corpdata.Status ?? "").Trim();
                        generalBusiness.HQStatus = (corpAddressDetails.Status ?? "").Trim();
                        SubmissionMasterModel submissionMasterModel = new SubmissionMasterModel();
                        submissionMasterModel.MasterId = generalBusiness.MasterId;
                        var masterdata = subMasterRepo.FindByID(submissionMasterModel).ToList();
                        if (masterdata.Count() != 0)
                        {
                            if (corpmodel.ToUpper().Trim() == (masterdata.FirstOrDefault().BusinessStructure ?? "").ToUpper().Trim())
                            {
                                generalBusiness.BusinessStructureStatus = true;
                            }
                            else
                            {
                                generalBusiness.BusinessStructureStatus = false;
                            }
                        }
                    }
                    else
                    { generalBusiness.CorpStatus = "NODATA"; }
                }

                //else
                //{
                //    //var questions = subquesrepo.FindByMasterID(generalBusiness.MasterId).ToList();
                //    //if (questions.Count != 0)
                //    //{
                //    //    var corpquestion = questions.Where(x =>
                //    //                x.Question.ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.IsDcraRegisteredInCorporation).ToUpper());
                //    //    if (corpquestion.Any())
                //    //    {
                //    //        if (corpquestion.FirstOrDefault().Answer.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES).ToUpper())
                //    //        {
                //    //        }
                //    //        else
                //    //        {
                //                SubmissionMasterModel submissionMasterModel = new SubmissionMasterModel();
                //                submissionMasterModel.MasterId = generalBusiness.MasterId;
                //                var masterdata = subMasterRepo.FindByID(submissionMasterModel).ToList();
                //                if (masterdata.Count() != 0)
                //                {
                //                    generalBusiness.BusinessStructure = (masterdata.First().BusinessStructure ?? "").Trim();
                //                    generalBusiness.TradeName = (masterdata.First().TradeName ?? "").Trim();
                //                    if ((corporationdata.ModelType ?? "").ToUpper().Trim() == (masterdata.FirstOrDefault().BusinessStructure ?? "").ToUpper().Trim())
                //                    {
                //                        generalBusiness.BusinessStructureStatus = true;
                //                    }
                //                    else
                //                    {
                //                        generalBusiness.BusinessStructureStatus = false;
                //                    }
                //                }
                //    //        }
                //    //    }
                //    //}
                //}
                var generalBusinessList = new List<GeneralBusiness> { generalBusiness };
                return generalBusinessList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method is used to Insert Corporation Data based on User Inputs.
        /// </summary>
        /// <param name="detailsModel"></param>
        /// <returns>Return bool Result</returns>
        public bool InsertCorporationDetails(GeneralBusiness detailsModel)
        {
            var status = false;
            detailsModel.UserSelectTpe = detailsModel.UserSelectTpe ?? "";
            detailsModel.UserType = detailsModel.UserType ?? "";
            if (detailsModel.UserSelectTpe.ToUpper().Trim() == "NEWMAIL")
            {
                var detailsfilenumber = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").ToString().Trim() == detailsModel.MasterId).ToList();
                if (detailsfilenumber.Count() != 0)
                {
                    detailsModel.FileNumber = detailsfilenumber.FirstOrDefault().FileNumber ?? "";
                }
            }
            try
            {
                var submissionmaster = subMasterRepo.FindByMasterID(detailsModel.MasterId).ToList();
                var mailtype = submissionmaster.FirstOrDefault().UserSelectMailAddressType ?? "";
                string headquatrMail = mailtype ?? "";
                string filenumber = detailsModel.FileNumber ?? "";
                var corpId = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").ToString().Trim() == detailsModel.MasterId);
                if (!corpId.Any())
                {
                    if (filenumber != "")
                    {
                        int RegId = 0;
                        var detailsexist = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").ToString().Trim() == detailsModel.MasterId);
                        var submissionCorporations = detailsexist as SubmissionCorporation_Agent[] ?? detailsexist.ToArray();
                        if (detailsexist != null && !submissionCorporations.Any())
                        {
                            var questions = subquesrepo.FindByMasterID(detailsModel.MasterId).ToList();
                            if (questions.Count != 0)
                            {
                                var corpquestion = questions.Where(x => x.Question.ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.IsDcraRegisteredInCorporation).ToUpper());
                                if (corpquestion.Any())
                                {
                                    if (corpquestion.FirstOrDefault().Answer.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.NO).ToUpper())
                                    {
                                        detailsModel.BusinessStructure = submissionmaster.FirstOrDefault().BusinessStructure;
                                    }
                                }
                            }
                            var cofoHopDetails = new SubmissionCorporation_Agent
                            {
                                MasterId = (detailsModel.MasterId ?? "").Trim(),
                                FileNumber = (detailsModel.FileNumber ?? "").Trim(),
                                BusinessName = (detailsModel.CBusinessName ?? "").Trim(),
                                TradeName = (detailsModel.TradeName ?? "").Trim(),
                                BusinessStructure = (detailsModel.BusinessStructure ?? "").Trim(),
                                Status = (detailsModel.CorpStatus ?? "").Trim(),
                                ChcekedFromCorp = true
                            };
                            Add(cofoHopDetails);
                            Save();
                            //if (headquatrMail.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.HqAdderss).ToUpper())
                            //{
                            //    SubmissionChceklistApp.UpdateMailTrueStatus(detailsModel.MasterId);
                            //}
                            RegId = cofoHopDetails.SubCorporationRegId;
                        }
                        else
                        {
                            var firstOrDefault = submissionCorporations.FirstOrDefault();
                            if (firstOrDefault != null)
                                RegId = firstOrDefault.SubCorporationRegId;
                        }
                        var result = subCorpAgentRepo.InsertCorporationDetails(RegId, detailsModel);
                        status = result;
                    }
                    else
                    {
                        status = true;
                    }
                }
                else
                {
                    var corporationId = (from corpid in FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").ToString().Trim() == detailsModel.MasterId)
                                         select corpid).FirstOrDefault();
                    if (filenumber != "")
                    {
                        if (!detailsModel.UserType.ToUpper().Contains("AGENT"))
                        {
                            //if (detailsModel.UserType.ToUpper() != "NEWMAIL")
                            //{
                            //    var questions = subquesrepo.FindByMasterID(detailsModel.MasterId).ToList();
                            //    if (questions.Count != 0)
                            //    {
                            //        var corpquestion = questions.Where(x => x.Question.ToUpper().Trim() == GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.IsDcraRegisteredInCorporation).ToUpper());
                            //        if (corpquestion.Any())
                            //        {
                            //            if (corpquestion.FirstOrDefault().Answer.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES).ToUpper())
                            //            {
                            //                var corpdata = corprepo.FindByFileNumber(detailsModel.FileNumber).ToList();
                            //                if (corpdata.Count() != 0)
                            //                {
                            //                    detailsModel.BusinessStructure = (corpdata.FirstOrDefault().ModelType ?? "").Trim();
                            //                    detailsModel.TradeName = "";
                            //                    detailsModel.CBusinessName = (corpdata.FirstOrDefault().BusinessName ?? "").Trim();
                            //                    subMasterRepo.UpdateBussinesStructure(detailsModel);
                            //                }
                            //            }
                            //        }
                            //    }
                            //}
                            corporationId.FileNumber = (detailsModel.FileNumber ?? "").Trim();
                            corporationId.BusinessName = (detailsModel.CBusinessName ?? "").Trim();
                            corporationId.TradeName = (detailsModel.TradeName ?? "").Trim();
                            //  corporationId.BusinessStructure = (detailsModel.BusinessStructure ?? "").Trim();
                            corporationId.ChcekedFromCorp = true;
                            if (!detailsModel.UserType.ToUpper().Contains(GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.NewMail).ToUpper()))
                            {
                                corporationId.Status = (detailsModel.CorpStatus ?? "").Trim();
                            }
                            Update(corporationId, corporationId.SubCorporationRegId);
                            Save();
                            //if (headquatrMail.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.HqAdderss).ToUpper())
                            //{
                            //    SubmissionChceklistApp.UpdateMailTrueStatus(detailsModel.MasterId);
                            //}
                        }
                        var result = subCorpAgentRepo.InsertCorporationDetails(corporationId.SubCorporationRegId, detailsModel);
                        status = result;
                    }
                    else
                    {
                        var result = subCorpAgentRepo.InsertCorporationDetails(corporationId.SubCorporationRegId, detailsModel);

                        //TODO : Need to Implement New Field Data In This one.
                        corporationId.BusinessName = string.Empty;
                        corporationId.BusinessStructure = string.Empty;
                        corporationId.Status = string.Empty;
                        corporationId.FileNumber = string.Empty;

                        corporationId.ChcekedFromCorp = false;
                        Update(corporationId, corporationId.SubCorporationRegId);
                        // Delete(corporationId);
                        Save();
                    }
                }
                string corpstatus = detailsModel.CorpStatus ?? GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.False).ToUpper();
                string HQStatus = detailsModel.HQStatus ?? GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.NewMail).ToUpper();
                if (corpstatus.ToString().ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.True).ToUpper() &&
                   HQStatus.ToString().ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.True).ToUpper())
                {
                    SubmissionChceklistApp.ChcekListUpdateStatus(detailsModel.MasterId, detailsModel.UserType, status);
                }
                else if (detailsModel.UserType.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.YCorpAgent).ToUpper())
                {
                    if (detailsModel.HQStatus.ToString().ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.True).ToUpper())
                    { status = true; }
                    else
                    { status = false; }
                    SubmissionChceklistApp.ChcekListUpdateStatus(detailsModel.MasterId, detailsModel.UserType, status);
                }
                else
                {
                    var corpmailtype = subMasterRepo.FindByMasterID(detailsModel.MasterId).FirstOrDefault().UserSelectMailAddressType;
                    SubmissionChceklistApp.UpdateCorpCheckStatus(detailsModel.MasterId, detailsModel.UserType, corpmailtype);
                    detailsModel.BusinessAddressLine1 = detailsModel.BusinessAddressLine1 ?? "";
                    detailsModel.Telphone = detailsModel.Telphone ?? "";
                    detailsModel.ZipCode = detailsModel.ZipCode ?? "";
                    if (detailsModel.BusinessAddressLine1 != "" && detailsModel.ZipCode != "")
                    {
                    }
                    else
                    { SubmissionChceklistApp.UpdateMailStatus(detailsModel.MasterId); }
                    //   if (corpmailtype.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.HqAdderss).ToUpper())
                    //{
                    //    string Notallow = "Allow";
                    //    if(detailsModel.UserType.ToUpper() ==GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.NCorpAgent).ToUpper())
                    //    {Notallow = "NotAllow";}else  if(detailsModel.UserType.ToUpper() ==GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.NCorpRegistration).ToUpper())
                    //    {Notallow = "NotAllow";}
                    //    if (Notallow.ToUpper() == "ALLOW")
                    //    {
                    //        var generalbusiness = new GeneralBusiness();
                    //        generalbusiness.MasterId = detailsModel.MasterId;
                    //        generalbusiness.UserSelectTpe = "";
                    //        subMasterRepo.UpdateUserSelect(generalbusiness);
                    //        SubmissionChceklistApp.UpdateMailStatus(detailsModel.MasterId);
                    //    }
                    //}
                }
                if (detailsModel.UserType.ToUpper() != "NEWMAIL")
                {
                    if (!detailsModel.UserType.ToUpper().Contains("AGENT"))
                    {
                        subMasterRepo.UpdateBusinessName(detailsModel.MasterId, detailsModel.CBusinessName);
                    }
                }
                return status;
            }
            catch (Exception)
            {
                status = false;
                return status;
            }
        }

        /// <summary>
        /// This method is used to Updae Checklist Updates using Appliation Unique Id, User Submission Type and Status.
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="userSubType"></param>
        /// <param name="status"></param>
        public void ChcekListUpdateStatus(string masterId, string userSubType, bool status)
        {
            SubmissionChceklistApp.ChcekListUpdateStatus(masterId, userSubType, status);
        }

        /// <summary>
        /// This method is used to Get Specific Head Quarter Address based on Application Unique Id and File Number.
        /// </summary>
        /// <param name="detailsModel"></param>
        /// <returns></returns>
        public GeneralBusiness GetHQAddess(GeneralBusiness detailsModel)
        {
            try
            {
                var submission = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").ToString().Trim() == detailsModel.MasterId);
                var submissionCorporations = submission as SubmissionCorporation_Agent[] ?? submission.ToArray();
                if (submissionCorporations.Any())
                {
                    if ((detailsModel.UserType == "N-CORPAGENT") && detailsModel.FileNumber == "NA")
                    {
                        detailsModel.FileNumber = "NA";
                    }
                    else
                    {
                        detailsModel.FileNumber = submissionCorporations.FirstOrDefault().FileNumber;
                        if (detailsModel.UserType == null)
                        {
                            if (detailsModel.FileNumber != "NA")
                            {
                                detailsModel.UserType = "Y-CORPREG";
                            }
                        }
                    }

                    var hQAddress = subCorpAgentRepo.GetHeadQuarterAddress(submissionCorporations.FirstOrDefault().SubCorporationRegId,
                        detailsModel.UserType, detailsModel.FileNumber);
                    if (hQAddress.Count() != 0)
                    {
                        var headQuarterAddress = hQAddress.FirstOrDefault();
                        detailsModel.AddressNumber = (headQuarterAddress.AddressNumber ?? "").Trim();
                        detailsModel.UserType = (headQuarterAddress.AddressType ?? "").Trim();
                        detailsModel.BusinessName = (headQuarterAddress.BusinessName ?? "").Trim();
                        detailsModel.FirstName = (headQuarterAddress.FirstName ?? "").Trim();
                        detailsModel.LastName = (headQuarterAddress.LastName ?? "").Trim();
                        detailsModel.MiddleName = (headQuarterAddress.MiddelName ?? "").Trim();
                        detailsModel.BusinessAddressLine1 = (headQuarterAddress.Address1 ?? "").Trim();
                        detailsModel.BusinessAddressLine2 = (headQuarterAddress.Address2 ?? "").Trim();
                        detailsModel.BusinessAddressLine3 = (headQuarterAddress.Address3 ?? "").Trim();
                        detailsModel.BusinessCity = (headQuarterAddress.City ?? "").Trim();
                        detailsModel.BusinessState = (headQuarterAddress.State ?? "").Trim();
                        detailsModel.BusinessCountry = (headQuarterAddress.Country ?? "").Trim();
                        detailsModel.ZipCode = (headQuarterAddress.ZipCode ?? "").Trim();
                        detailsModel.Email = (headQuarterAddress.Email ?? "").Trim();
                        detailsModel.Telphone = (headQuarterAddress.Telephone ?? "").Trim();
                    }
                }
                return detailsModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public virtual void SaveChanges()
        //{
        //    Context.SaveChanges();
        //}
        /// <summary>
        /// This method is used to Get Specific Corporation Address based on Application Unique Id.
        /// </summary>
        /// <param name="MasterId"></param>
        /// <returns>Retrun Specific Corporation Address</returns>
        public IEnumerable<SubmissionCorporation_Agent> FindById(string masterId)
        {
            var corportationAddress = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").Trim() == masterId);
            return corportationAddress;
        }

        /// <summary>
        ///  This method is used to Get Specific Corporation Address based on Application Unique Id and File Number.
        /// </summary>
        /// <param name="generalBusiness"></param>
        /// <returns>Retrun Specific Corporation Address</returns>
        public IEnumerable<SubmissionCorporation_Agent> FindByMasterwithFile(GeneralBusiness generalBusiness)
        {
            var corportationAddress = FindBy(x => x.MasterId == generalBusiness.MasterId);
            return corportationAddress;
        }

        /// <summary>
        /// This method is used to Delete specific Corporation Data based Application Unique Id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Retrun bool Result</returns>
        public bool DeleteSubmissionCorp(string masterId)
        {
            bool status = false;
            try
            {
                var contact = FindBy(x => x.MasterId == masterId).Single();
                Delete(contact);
                Save();
                status = true;
            }
            catch (Exception)
            { status = false; }
            return status;
        }

        /// <summary>
        /// This method is used to Get Hq Address based on User Inputs.
        /// </summary>
        /// <param name="generalBusiness"></param>
        /// <returns>Return Corporation Agent Address</returns>
        public List<GeneralBusiness> GetCorpAgent(GeneralBusiness generalBusiness)
        {
            try
            {
                var registeragent = new List<GeneralBusiness>();
                var corporationdata = FindByMasterwithFile(generalBusiness).ToList();

                if (corporationdata.Count() != 0)
                {
                    var corpdata = corporationdata.FirstOrDefault();
                    generalBusiness.FileNumber = corpdata.FileNumber ?? "";
                    var agentdata = subCorpAgentRepo.FindBySubID(corpdata.SubCorporationRegId).Where(x => x.AddressType.ToUpper().Contains("AGENT")
                        && x.FileNumber.Replace(System.Environment.NewLine, "").ToString().ToUpper() == generalBusiness.FileNumber.ToUpper()).ToList();
                    if (agentdata.Count() != 0)
                    {
                        foreach (var ragent in agentdata)
                        {
                            GeneralBusiness agent = new GeneralBusiness();
                            agent.FirstName = (ragent.FirstName ?? "").Trim();
                            agent.BusinessName = (ragent.BusinessName ?? "").Trim();
                            agent.BusinessAddressLine1 = (ragent.Address1 ?? "").Trim();
                            agent.BusinessAddressLine2 = (ragent.Address2 ?? "").Trim();
                            agent.BusinessAddressLine3 = (ragent.Address3 ?? "").Trim();
                            agent.Quardrant = (ragent.Quadrant ?? "").Trim();
                            agent.Unit = (ragent.UnitNumber ?? "").Trim();
                            agent.BusinessCity = (ragent.City ?? "").Trim();
                            agent.BusinessState = GetStateCode(ragent.State ?? "", ragent.Country ?? "").Trim();
                            agent.ZipCode = (ragent.ZipCode ?? "").Trim();
                            agent.Telphone = (ragent.Telephone ?? "").Trim();
                            agent.BusinessCountry = GetCountryFullName(ragent.Country ?? "").Trim();
                            agent.FileNumber = (ragent.FileNumber ?? "").Trim();
                            agent.UnitType = "";
                            agent.IsValid = true;
                            agent.CorpStatus = (corpdata.Status ?? "").Trim();
                            agent.AddressNumber = (ragent.AddressNumber ?? "").Trim();
                            agent.HQStatus = (ragent.Status ?? "").Trim();
                            agent.Email = (ragent.Email ?? "").Trim();
                            registeragent.Add(agent);
                        }
                    }
                    else
                    {
                        var agentdetails = _agentrepo.FindByID(generalBusiness);
                        foreach (var ragent in agentdetails)
                        {
                            GeneralBusiness agent = new GeneralBusiness();
                            agent.FirstName = string.Empty;// (ragent.RA_Name ?? "").Trim();
                            agent.BusinessName = (ragent.RA_Name ?? "").Trim();
                            agent.BusinessAddressLine1 = (ragent.RA_Address1 ?? "").Trim();
                            agent.BusinessCity = (ragent.RA_City ?? "").Trim();
                            agent.BusinessState = GetStateCode(ragent.RA_State ?? "", ragent.BusinessCountry ?? "").Trim();
                            agent.ZipCode = (ragent.RA_ZipCode ?? "").Trim();
                            agent.Telphone = string.Empty;// (ragent. ?? "").Trim();
                            agent.BusinessCountry = GetCountryFullName(ragent.BusinessCountry ?? "").Trim();
                            agent.FileNumber = (ragent.FileNumber ?? "").Trim();
                            agent.UnitType = string.Empty;//(ragent ?? "").Trim();
                            agent.IsValid = false;
                            agent.BusinessAddressLine2 = (ragent.RA_Address3 ?? "").Trim();
                            agent.BusinessAddressLine3 = (ragent.RA_Address4 ?? "").Trim();
                            agent.Quardrant = string.Empty;// (ragent.Quadrant ?? "").Trim();
                            agent.AddressNumber = (ragent.RA_Address2 ?? "").Trim();
                            agent.Unit = string.Empty;// (ragent.UnitNumber ?? "").Trim();
                            agent.Email = (ragent.RA_Email ?? "").Trim();
                            registeragent.Add(agent);
                        }
                    }
                }
                else
                { registeragent = null; }
                return registeragent;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method is used to Delete Coporation Data  when Getting Empty and Updated Check List based on Application Unique Id and Type.
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="type"></param>
        /// <returns>Retrun bool Result</returns>
        public bool DeleteSubmissionCorpEmpty(string masterId, string type)
        {
            bool status = false;
            try
            {
                var existcorporation = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").ToString().Trim() == masterId).ToList();
                if (existcorporation.Any())
                {
                    var corpaddress = existcorporation.Single();
                    if (type.ToUpper() == "N-CORPREG")
                    {
                        corpaddress.BusinessName = string.Empty;
                        Update(corpaddress, corpaddress.SubCorporationRegId);
                        Save();
                    }
                    subCorpAgentRepo.DeleteHqAddress(corpaddress.SubCorporationRegId, type);
                    SubmissionChceklistApp.UpdateCorpsHqstatusandagentStatus(masterId, type.ToUpper());
                }

                #region junk

                //var contact = FindBy(x => x.MasterId == masterId).Single();
                //if (contact != null)
                //{
                //    if ((type == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.YCorpAgent).ToUpper()) ||
                //        (type == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.NCorpAgentEmpty).ToUpper()) || (type == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.NCorpAgent).ToUpper()))
                //    {
                //        status = subCorpAgentRepo.DeleteHqAddress(contact.SubCorporationRegId, type);
                //        SubmissionChceklistApp.UpdateCorpCheckStatus(masterId, GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.NCorpAgentEmpty).ToUpper(), string.Empty);
                //    }
                //    else
                //    {
                //        Delete(contact);
                //        Save();
                //        status = subCorpAgentRepo.DeleteHqAddress(contact.SubCorporationRegId, type);
                //        SubmissionChceklistApp.UpdateCorpCheckStatus(masterId, type, string.Empty);
                //        var firstOrDefault = subMasterRepo.FindByMasterID(masterId).FirstOrDefault();
                //        if (firstOrDefault != null)
                //        {
                //            var mailtype = firstOrDefault.UserSelectMailAddressType;
                //            if (mailtype.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.HqAdderss).ToUpper())
                //            {
                //                SubmissionChceklistApp.UpdateMailStatus(masterId);
                //            }
                //        }
                //    }

                #endregion junk
            }
            catch (Exception)
            { status = false; }
            return status;
        }

        /// <summary>
        /// This method is used to Get Corporation Status based on Application Unique Id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return Status Result</returns>
        public CorporationStatus CorpServiceStatus(string masterId)
        {
            var status = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.Active).ToUpper();
            var usercorpdata = FindBy(x => x.MasterId.Trim() == masterId).ToList();
            var corpStatus = new CorporationStatus { OriginalCorpStatus = status.ToUpper() };
            if (!usercorpdata.Any()) return corpStatus;
            var filenumber = usercorpdata.First().FileNumber ?? "";
            var corporationdata = corprepo.FindByFileNumber(filenumber).ToList();
            if (!corporationdata.Any()) return corpStatus;
            var corpstatus = corporationdata.First().EntityStatus ?? "";
            corpStatus.OriginalCorpStatus = corpstatus;
            if (corpstatus.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.Active).ToUpper())
                return corpStatus;
            var firstOrDefault = subMasterRepo.FindByMasterID(masterId).FirstOrDefault();
            if (firstOrDefault != null)
            {
                var updateSubmittCorp = usercorpdata.FirstOrDefault();
                var submissionCorporationAgent = usercorpdata.FirstOrDefault();
                if (submissionCorporationAgent != null && (updateSubmittCorp != null && ((updateSubmittCorp.Status == "True") ||
                                                                                      (submissionCorporationAgent.Status == "False"))))
                {
                    updateSubmittCorp.Status = corpstatus;
                    updateSubmittCorp.ChcekedFromCorp = false;
                    Update(updateSubmittCorp, updateSubmittCorp.SubCorporationRegId);
                    Save();
                    status = "Dcbc_Corp_Status_Changed";
                }
                else
                {
                    var corporationAgent = usercorpdata.FirstOrDefault();
                    status = corporationAgent != null && corporationAgent.ChcekedFromCorp == false ? "Dcbc_Corp_Status_Changed" : corpstatus.ToUpper();
                }

                var mailtype = firstOrDefault.UserSelectMailAddressType;
                SubmissionChceklistApp.UpdateCorpCheckStatus(masterId, "", mailtype);
            }
            corpStatus.ChangeCorpStatus = status.ToUpper();
            corpStatus.OriginalCorpStatus = corpstatus.ToUpper();
            return corpStatus;
        }
        /// <summary>
        /// This method is used to update submission corporation data based on user inputs
        /// </summary>
        /// <param name="generalBusiness"></param>
        /// <returns>Return bool value</returns>
        public bool UpdateCorportationData(GeneralBusiness generalBusiness)
        {
            bool status = false;
            try
            {
                var submissionCorp = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").ToString().Trim() == generalBusiness.MasterId).ToList();
                if (submissionCorp.Count() != 0)
                {
                    var submissionCorporation = submissionCorp.Single();
                    if (generalBusiness.FileNumber.ToUpper().Trim() != submissionCorporation.FileNumber.ToUpper().Trim())
                    {
                        submissionCorporation.FileNumber = (generalBusiness.FileNumber ?? "").Trim();
                        submissionCorporation.BusinessName = (generalBusiness.CBusinessName ?? "").ToString().Trim();
                        submissionCorporation.TradeName = (generalBusiness.TradeName ?? "").ToString().Trim();
                        submissionCorporation.BusinessStructure = (generalBusiness.BusinessStructure ?? "").ToString().Trim();
                        submissionCorporation.Status = (generalBusiness.EntityStatus ?? "false").ToString().Trim();
                        Update(submissionCorporation, submissionCorporation.SubCorporationRegId);
                        Save();
                        // var corpmailtype = subMasterRepo.FindByMasterID(generalBusiness.MasterId).FirstOrDefault().UserSelectMailAddressType;
                        //if (corpmailtype.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.HqAdderss).ToUpper())
                        //{
                        //    SubmissionChceklistApp.UpdateMailStatus(generalBusiness.MasterId);
                        //}
                        status = true;
                    }
                }
                else
                {
                    var cofoHopDetails = new SubmissionCorporation_Agent
                    {
                        MasterId = (generalBusiness.MasterId ?? "").ToString().Trim(),
                        FileNumber = (generalBusiness.FileNumber ?? "").ToString().Trim(),
                        BusinessName = (generalBusiness.CBusinessName ?? "").ToString().Trim(),
                        TradeName = (generalBusiness.TradeName ?? "").ToString().Trim(),
                        BusinessStructure = (generalBusiness.BusinessStructure ?? "").ToString().Trim(),
                        Status = (generalBusiness.EntityStatus ?? "false").ToString().Trim()
                    };
                    Add(cofoHopDetails);
                    Save();
                }
            }
            catch (Exception)
            { status = false; }

            return status;
        }
        /// <summary>
        /// This method to get the status of corporation based on file number and last updated date
        /// </summary>
        /// <param name="corporationdetails"></param>
        /// <returns>Return string value</returns>
        public string CorpOnlineSearch(CorporationDetails corporationdetails)
        {
            return corprepo.CorpOnlineSearch(corporationdetails);
        }
        /// <summary>
        /// This method is used to update the status of check corporation from in submission corporation based on unique id.
        /// </summary>
        /// <param name="generalBusiness"></param>
        public void UpdateCorpDisplayStatus(GeneralBusiness generalBusiness)
        {
            var submissionCorp = FindBy(x => x.MasterId.Replace(System.Environment.NewLine, "").ToString().Trim() == generalBusiness.MasterId).ToList();
            if (submissionCorp.Count() != 0)
            {
                submissionCorp.FirstOrDefault().ChcekedFromCorp = true;
                Update(submissionCorp.FirstOrDefault(), submissionCorp.FirstOrDefault().SubCorporationRegId);
                Save();
            }
        }
        /// <summary>
        /// This method is used to get state full name based on state code and country code.
        /// </summary>
        /// <param name="stateCode"></param>
        /// <param name="countryCode"></param>
        /// <returns>Return string value</returns>
        public string GetStateFullName(string stateCode, string countryCode)
        {
            if (countryCode == "")
                return stateCode;
            var getCountry = _MasterCountryRepository.FindCountryBasedOnName(countryCode);
            if (getCountry.Any())
                countryCode = getCountry.FirstOrDefault().CountryCode;

            var statedata = _masterStateRepository.GetStateName(stateCode, countryCode).ToList();
            if (statedata.Any())
            {
                return statedata.FirstOrDefault().StateName;
            }
            else
            {
                return stateCode;
            }
        }
        /// <summary>
        /// This method is used to get state code based on state name and country code.
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="countryCode"></param>
        /// <returns>Return string value</returns>
        public string GetStateCode(string stateName, string countryCode)
        {
            if (countryCode == "")
                return stateName;
            var getCountry = _MasterCountryRepository.FindCountryBasedOnName(countryCode);
            if (getCountry.Any())
                countryCode = getCountry.FirstOrDefault().CountryCode;

            var statedata = _masterStateRepository.GetStateCode(stateName, countryCode).ToList();
            if (statedata.Any())
            {
                return statedata.FirstOrDefault().StateCode;
            }
            else
            {
                return stateName;
            }
        }
        /// <summary>
        /// This method is used to Get country full name based on country code.
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public string GetCountryFullName(string countryCode)
        {
            var countryData = _MasterCountryRepository.FindCountryBasedOnCode(countryCode).ToList();
            if (countryData.Any())
            {
                return countryData.FirstOrDefault().CountryName;
            }
            else
            {
                return countryCode;
            }
        }
    }
}