using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessCenter.Data.Implementation
{
    public class SubmissionCorporationAgentAddressRepository : GenericRepository<SubmissionCorporation_Agent_Address>, ISubmissionCorporationAgentAddressRepository
    {
        protected ISubmissionMasterApplicationChcekListRepository Checkrepository;
        protected ISubmissionCorporationRepository Corprepo;
        protected ISubmissionMasterRepository Masterrepo;
        public SubmissionCorporationAgentAddressRepository(IUnitOfWork context, ISubmissionMasterApplicationChcekListRepository checkrepository,
            ISubmissionMasterRepository masterRepository)
            : base(context)
        {
            Checkrepository = checkrepository;
            Masterrepo = masterRepository;
        }
        /// <summary>
        /// This method is used to Get Specific Corporation Address using Submission Corporation Id.
        /// </summary>
        /// <param name="subCorpAgentModel"></param>
        /// <returns>Retrun Speicific Corporation Address</returns>
        public IEnumerable<SubmissionCorporation_Agent_Address> FindById(SubmissionCorpAgentModel subCorpAgentModel)
        {
            var corporationAddress = FindBy(x => x.SubCorporationRegId == subCorpAgentModel.SCAId);
            return corporationAddress;
        }
        /// <summary>
        /// This method is used to Get Specific Corporation Address using Submission Corporation Id and Address Type.
        /// </summary>
        /// <param name="subCorporationRegId"></param>
        /// <param name="type"></param>
        /// <returns>Retrun  Speicific Corporation Address</returns>
        public IEnumerable<SubmissionCorporation_Agent_Address> FindByTypewithMasterId(int subCorporationRegId, string type)
        {
            var corporationAddress = FindBy(x => x.SubCorporationRegId == subCorporationRegId && x.AddressType == type);
            return corporationAddress;
        }
       
        /// <summary>
        /// This method is used to insert Corporation Address Details and Update Check Repository using User Inputs.
        /// </summary>
        /// <param name="regId"></param>
        /// <param name="detailsModel"></param>
        /// <returns>Retrun bool Result</returns>
        public bool InsertCorporationDetails(int regId,GeneralBusiness detailsModel)
        {
            var status = false;
            string corpid = string.Empty;
            try
            {
                string filenumber = detailsModel.FileNumber ?? "";
                var corptid =  FindBy(x => x.SubCorporationRegId == regId && x.AddressType.Replace(System.Environment.NewLine,"").ToString().Trim()==
                    detailsModel.UserType.Trim());
               
                 detailsModel.BusinessState=detailsModel.BusinessState ?? "";
                        detailsModel.BusinessCountry=detailsModel.BusinessCountry ?? "";
                        if (detailsModel.BusinessCountry=="")
                        {
                            if (detailsModel.BusinessState.ToUpper().Trim() != "NOTSET")
                            {
                                detailsModel.BusinessCountry="US";
                            }else
                            { detailsModel.BusinessCountry = ""; }
                        }
                if (!corptid.Any())
                {
                    if (filenumber != "")
                    {
                       
                       
                        var details = new SubmissionCorporation_Agent_Address
                        {
                            SubCorporationRegId = regId,
                            AddressType = (detailsModel.UserType ?? "").Trim(),
                            BusinessName = (detailsModel.BusinessName ?? "").Trim(),
                            FirstName = (detailsModel.FirstName ?? "").Trim(),
                            MiddelName = (detailsModel.MiddleName ?? "").Trim(),
                            LastName = (detailsModel.LastName ?? "").Trim(),
                            Address1 =(detailsModel.BusinessAddressLine1 ?? "").Trim(),
                            Address2 = (detailsModel.BusinessAddressLine2 ?? "").Trim(),                 
                            FileNumber = (detailsModel.FileNumber  ?? "").Trim(),
                            Address3 = (detailsModel.BusinessAddressLine3 ?? "").Trim(),
                            City = (detailsModel.BusinessCity ?? "").Trim(),
                            State = (detailsModel.BusinessState ?? "").Trim(),
                            Country = (detailsModel.BusinessCountry ?? "").Trim(),
                            ZipCode = (detailsModel.ZipCode ?? "").Trim(),
                            Email = (detailsModel.Email ?? "").Trim(),
                            Telephone = (detailsModel.Telphone ?? "").Trim(),
                            Status = (detailsModel.HQStatus ?? "").Trim(),
                            Quadrant=(detailsModel.Quardrant ?? "").Trim(),
                            UnitNumber=(detailsModel.Unit ?? "").Trim(),
                            AddressNumber = (detailsModel.AddressNumber ?? "").Trim()
                        };
                        Add(details);
                        Save();
                    }
                }
                else
                {
                    if (filenumber != "")
                    {
                        var corpagentid =FindBy(x =>x.SubCorporationRegId == regId && x.AddressType.Replace(System.Environment.NewLine, "").ToString().Trim() ==
                                        detailsModel.UserType.Trim()).First();
                        corpagentid.SubCorporationRegId = regId;
                        corpagentid.AddressType = (detailsModel.UserType ?? "").Trim();
                        corpagentid.BusinessName = (detailsModel.BusinessName ?? "").Trim();
                        corpagentid.FileNumber = (detailsModel.FileNumber ?? "").Trim();
                        corpagentid.FirstName = (detailsModel.FirstName ?? "").Trim();
                        corpagentid.MiddelName = (detailsModel.MiddleName ?? "").Trim();
                        corpagentid.LastName = (detailsModel.LastName ?? "").Trim();
                        corpagentid.Address1 = (detailsModel.BusinessAddressLine1 ?? "").Trim();
                        corpagentid.Address2 = (detailsModel.BusinessAddressLine2 ?? "").Trim();
                        corpagentid.Address3 = (detailsModel.BusinessAddressLine3 ?? "").Trim();
                        corpagentid.City = (detailsModel.BusinessCity ?? "").Trim();
                        corpagentid.State = (detailsModel.BusinessState ?? "").Trim();
                        corpagentid.Country = (detailsModel.BusinessCountry ?? "").Trim();
                        corpagentid.ZipCode = (detailsModel.ZipCode ?? "").Trim();
                        corpagentid.Email = (detailsModel.Email ?? "").Trim();
                        corpagentid.Telephone = (detailsModel.Telphone ?? "").Trim();
                        corpagentid.AddressNumber = (detailsModel.AddressNumber ?? "").Trim();
                        corpagentid.Status = (detailsModel.HQStatus ?? "").Trim();
                        corpagentid.Quadrant=(detailsModel.Quardrant ?? "").Trim();
                        corpagentid.UnitNumber = (detailsModel.Unit ?? "").Trim();
                        corpagentid.AddressNumber = (detailsModel.AddressNumber ?? "").Trim();
                        
                        Update(corpagentid, corpagentid.SCAId);
                        Save();
                        string hqstatus = (detailsModel.HQStatus ?? "").Trim();
                        string corpstatus = (detailsModel.CorpStatus ?? "").Trim();
                        if (detailsModel.UserType.ToUpper().Contains(GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.CorpRegistration).ToUpper())
                            && (hqstatus.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.False).ToUpper() ||
                            corpstatus.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.False).ToUpper()))
                        {
                            var agent =FindBy(x =>x.SubCorporationRegId == regId && x.AddressType.Replace(System.Environment.NewLine, "").ToString().ToUpper().
                                           Contains("CORPAGENT")).ToList();
                            //GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.CorpAgent).ToUpper()
                            if(agent.Count()!=0)
                            {
                                var updateagent = agent.First();
                                updateagent.Status =null;
                                Update(updateagent, updateagent.SCAId);
                                    Save();
                            }
                        }
                    }
                    else
                    {
                        var corporationid = FindBy(x => x.SubCorporationRegId == regId && x.AddressType.ToUpper() != "NEWMAIL").ToList();
                        foreach (var detailid in corporationid)
                        {
                            corpid = detailid.SCAId + "," + corpid;
                        }
                        var subsubCat = corpid.Split(',');
                        foreach (var detailid in subsubCat)
                        {
                            if (detailid.Trim() != "," )
                            {
                                if (detailid.Trim() != "")
                                {
                                    int subid = Convert.ToInt32(detailid);
                                    var corpagentid = FindBy(x => x.SubCorporationRegId == regId && x.SCAId == subid && x.AddressType.ToUpper() != "NEWMAIL").FirstOrDefault();
                                    if (corpagentid != null)
                                    {
                                        //corpagentid.FileNumber = string.Empty;
                                        //corpagentid.Status = string.Empty;
                                      //  Update(corpagentid, subid);
                                          Delete(corpagentid);
                                        Save();
                                    }
                                }
                            }
                        }
                    }
                }
                if (detailsModel.UserType.ToUpper().ToString().Trim() ==GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.YCorpRegistration).ToUpper()  
                    || detailsModel.UserType.ToUpper().ToString().Trim() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.NCorpRegistration).ToUpper())
                {
                    Checkrepository.UpdateIsCorporation(detailsModel);
                }
                else
                    if (detailsModel.UserType.ToUpper().ToString().Trim() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.YCorpAgent).ToUpper() || 
                        detailsModel.UserType.ToUpper().ToString().Trim() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.NCorpAgent).ToUpper())
                {
                    Checkrepository.UpdateIsCorporation(detailsModel);
                }  else
                if(detailsModel.UserType.ToUpper().ToString().Trim() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.NewMail).ToUpper()){
                    detailsModel.BusinessAddressLine1 = detailsModel.BusinessAddressLine1 ?? "";
                    detailsModel.Telphone = detailsModel.Telphone ?? "";
                    detailsModel.ZipCode = detailsModel.ZipCode ?? "";
                    if (detailsModel.BusinessAddressLine1 != ""  && detailsModel.ZipCode != "")
                    {
                        Checkrepository.UpdateIsMailAddress(detailsModel);
                        Masterrepo.UpdateUserSelect(detailsModel);
                    }
                    else
                    { Checkrepository.UpdateMailStatus(detailsModel.MasterId); }
                   
                }
                status = true;
                return true;
            }
            catch (Exception )
            {
                return status;
            }
        }
        /// <summary>
        /// This method is used to Get Head Quater Address using Submission Id, Address Type and File Number
        /// </summary>
        /// <param name="submissionid"></param>
        /// <param name="addresstype"></param>
        /// <param name="fileNumber"></param>
        /// <returns></returns>
        public IEnumerable<SubmissionCorporation_Agent_Address> GetHeadQuarterAddress(int submissionid, string addresstype, string fileNumber)
        {
            if (addresstype.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.NewMail).ToUpper())
            {
                var HqAddress = FindBy(x => x.SubCorporationRegId == submissionid && x.AddressType.ToUpper().Contains(addresstype.ToUpper()));
                return HqAddress;
            }
            else
            {
                var HqAddress = FindBy(x => x.SubCorporationRegId == submissionid && x.AddressType.ToUpper().Contains(addresstype.ToUpper())
                                            && x.FileNumber.ToUpper().Trim() == fileNumber.ToUpper().Trim());
                return HqAddress;
            }
        }

        /// <summary>
        /// This method is used to Get Specific Corporation Address based on Submission Corporation Id.
        /// </summary>
        /// <param name="subId"></param>
        /// <returns>Retrun Specifi Corporation Address</returns>
        public IEnumerable<SubmissionCorporation_Agent_Address> FindBySubID(int subId)
        {
            var corporationAddress =FindBy(x => x.SubCorporationRegId == subId);
            return corporationAddress;
        }
        /// <summary>
        /// This method is used to Delete Corporation Address based on Submission Coproation Id and Address Type.
        /// </summary>
        /// <param name="subID"></param>
        /// <param name="typeName"></param>
        /// <returns>Retrun bool Result</returns>
        public bool DeleteHqAddress(int subID,string typeName)
        {
            bool result = false;
            var corpagent = FindBy(x => x.SubCorporationRegId == subID && x.AddressType.Trim() == typeName.Trim()).ToList();
            if (corpagent.Any())
            {
                var deletefrom = corpagent.FirstOrDefault();
                Delete(deletefrom);
                Save();
                result = true;
            }
            return result;
        }
    }
}
