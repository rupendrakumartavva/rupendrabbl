using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessCenter.Data.Implementation
{
    public class SubmissionCofoHopeHopAddressRepository : GenericRepository<SubmissionCofo_Hop_Ehop_Address>, ISubmissionCofoHopeHopAddressRepository
    {
        protected ICorpRespository corprepo;
        protected ISubmissionMasterRepository submissionMasterrepo;
        protected IStreetTypesRepository streettyperepo;
        protected IMasterRegisterAgentRepository _agentrepo;
        protected ISubmissionCorporationRepository SubmissionCorpRepository;
        protected ISubmissionQuestionRepository subquesrepo;
        protected ISubmissionMasterApplicationChcekListRepository _submissionMasterApplicationChcekList;

        protected IMasterCountryRepository _MasterCountryRepository;
        protected IMasterStateRepository _masterStateRepository;

        public SubmissionCofoHopeHopAddressRepository(IUnitOfWork context, ICorpRespository corpRepository, ISubmissionMasterRepository submissionMasterRepository
            , IStreetTypesRepository streetTypesRepository, IMasterRegisterAgentRepository agentrepo, ISubmissionCorporationRepository submissionCorpRepository,
            ISubmissionQuestionRepository subquesrepository, ISubmissionMasterApplicationChcekListRepository submissionMasterApplicationChcekList,
            IMasterStateRepository masterStateRepository, IMasterCountryRepository masterCountryRepository)
            : base(context)
        {
            submissionMasterrepo = submissionMasterRepository;
            corprepo = corpRepository;
            _agentrepo = agentrepo;
            streettyperepo = streetTypesRepository;
            SubmissionCorpRepository = submissionCorpRepository;
            subquesrepo = subquesrepository;
            _submissionMasterApplicationChcekList = submissionMasterApplicationChcekList;
            _masterStateRepository = masterStateRepository;
            _MasterCountryRepository = masterCountryRepository;
        }

        /// <summary>
        /// This method is used to get the Business Address Based on the Custom Type Id.
        /// </summary>
        /// <param name="customTypeid"></param>
        /// <returns>Retrun Business Address </returns>
        public IEnumerable<SubmissionCofo_Hop_Ehop_Address> GetPrimisessAddress(int customTypeid)
        {
            var businessAddress = FindBy(x => x.CustomTypeId == customTypeid).ToList();
            return businessAddress;
        }

        /// <summary>
        /// This method is used to Get specific Business Address based on Custom Type id and Custom Type.
        /// </summary>
        /// <param name="customTypeId"></param>
        /// <param name="customType"></param>
        /// <returns>Retrun Business Address </returns>
        public IEnumerable<SubmissionCofo_Hop_Ehop_Address> GetPrimisessAddress(int customTypeId, string customType)
        {
            var businessAddress = FindBy(x => x.CustomTypeId == customTypeId && x.CustomType == customType).ToList();
            return businessAddress;
        }

        /// <summary>
        /// This method is used to Get Businsess Address based in File Number and Application Unique Id.
        /// </summary>
        /// <param name="generalBusiness"></param>
        /// <returns>Retrun GeneralBusiness</returns>
        public IEnumerable<GeneralBusiness> GetCorpBusinessData(GeneralBusiness generalBusiness)
        {
            try
            {
                var corporationData = SubmissionCorpRepository.FindByMasterId(generalBusiness).ToList();
                if (corporationData.Count != 0)
                {
                    string filenumber = (corporationData.FirstOrDefault().FileNumber ?? "").ToUpper().Trim();
                    generalBusiness.FileNumber = generalBusiness.FileNumber ?? "";
                    if (filenumber != generalBusiness.FileNumber.ToUpper().Trim())
                    {
                        _submissionMasterApplicationChcekList.UpdateCorpSearchStatus(generalBusiness.MasterId);
                    }
                }
                var corpdata = corprepo.FindByFileNumber(generalBusiness.FileNumber).FirstOrDefault();
                var submissionMaster = submissionMasterrepo.FindByMasterID(generalBusiness.MasterId).FirstOrDefault();
                if (submissionMaster != null)
                {
                    generalBusiness.BusinessStructure = submissionMaster.BusinessStructure;
                    if (corpdata != null)
                    {
                        generalBusiness.FileNumber = (corpdata.FileNumber ?? "").Trim();
                        generalBusiness.MasterId = (generalBusiness.MasterId ?? "").Trim();
                        generalBusiness.OccupancyAddssValidate = (generalBusiness.OccupancyAddssValidate ?? "").Trim();

                        //var questions = subquesrepo.FindByMasterID(generalBusiness.MasterId).ToList();
                        // if (questions.Count != 0)
                        // {
                        //     var corpquestion = questions.Where(x => x.Question.ToUpper().Trim() ==
                        //        GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.IsDcraRegisteredInCorporation).ToUpper());
                        //     if (corpquestion.Any())
                        //     {
                        //         if (corpquestion.FirstOrDefault().Answer.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.CategoryAnswers.YES).ToUpper())
                        //         {
                        //             generalBusiness.TradeName = "";
                        //             generalBusiness.BusinessStructure = (corpdata.ModelType ?? "" ).Trim();
                        //         }
                        //         else
                        //         {
                        //        }
                        //    }
                        //}

                        if ((corpdata.ModelType ?? "").ToUpper().Trim() == (submissionMaster.BusinessStructure ?? "").ToUpper().Trim())
                        {
                            generalBusiness.BusinessStructureStatus = true;
                        }
                        else
                        {
                            generalBusiness.BusinessStructureStatus = false;
                        }
                        generalBusiness.TradeName = (submissionMaster.TradeName ?? "").Trim();
                        generalBusiness.BusinessStructure = (corpdata.ModelType ?? "").Trim();

                        generalBusiness.CBusinessName = (corpdata.BusinessName ?? "").Trim();
                        generalBusiness.FirstName = "";
                        generalBusiness.LastName = "";
                        generalBusiness.MiddleName = "";
                        generalBusiness.BusinessName = "";
                        generalBusiness.BusinessAddressLine1 = (corpdata.BusniessAddressLine1 ?? "").Trim();
                        generalBusiness.BusinessAddressLine2 = (corpdata.BusniessAddressLine2 ?? "").Trim();
                        generalBusiness.BusinessAddressLine3 = (corpdata.BusniessAddressLine3 ?? "").Trim();
                        generalBusiness.BusinessAddressLine4 = (corpdata.BusniessAddressLine4 ?? "").Trim();
                        generalBusiness.BusinessCity = (corpdata.BusinessCity ?? "");

                        corpdata.BusinessState = (corpdata.BusinessState ?? "").Trim();
                        corpdata.BusinessCountry = (corpdata.BusinessCountry ?? "").Trim();

                        if (corpdata.BusinessCountry == "")
                        {
                            if (corpdata.BusinessState.ToUpper().Trim() != "NOTSET")
                            {
                                corpdata.BusinessCountry = "US";
                            }
                            else
                            { corpdata.BusinessCountry = ""; }
                        }

                        generalBusiness.BusinessCountry = GetCountryFullName((corpdata.BusinessCountry ?? "").Trim());
                        generalBusiness.BusinessState = GetStateCode(corpdata.BusinessState ?? "", (corpdata.BusinessCountry ?? "").Trim()).Trim();
                        generalBusiness.ZipCode = (corpdata.ZipCode ?? "").Trim();

                        generalBusiness.Email = "";
                        corpdata.EntityStatus = (corpdata.EntityStatus ?? "").Trim();
                        generalBusiness.EntityStatus = corpdata.EntityStatus.ToUpper().Trim();

                        if (generalBusiness.EntityStatus != "ACTIVE")
                        {
                            SubmissionCorpRepository.UpdateCorportationData(generalBusiness);
                        }
                        generalBusiness.Telphone = "";
                    }
                    else
                    { generalBusiness.EntityStatus = GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.NoData).ToUpper(); }
                }
                var generalBusinessList = new List<GeneralBusiness> { generalBusiness };
                return generalBusinessList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This method is used to get business owner name based on unique id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return string value</returns>
        public string GetBusinessOwnerFullName(string masterId)
        {
            var questions = subquesrepo.FindByMasterID(masterId).ToList();
            if (questions.Count != 0)
            {
                var corpquestion = questions.Where(x => x.Question.ToUpper().Trim() ==
                           GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.BusinessOwner).ToUpper()).ToList();
                if (corpquestion.Any())
                {
                    var businessOwner = corpquestion.FirstOrDefault();
                    if (businessOwner != null) return businessOwner.Answer;
                }
            }
            return "";
        }
        /// <summary>
        /// This method is used to get trade name based on unique id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return string value</returns>
        public string GetTradeNameWithSubmissionQuestions(string masterId)
        {
            var questions = subquesrepo.FindByMasterID(masterId).ToList();
            if (questions.Count != 0)
            {
                var corpquestion = questions.Where(x => x.Question.ToUpper().Trim() ==
                           GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.TradeName).ToUpper()).ToList();
                if (corpquestion.Any())
                {
                    var tradeName = corpquestion.FirstOrDefault();
                    if (tradeName != null) return tradeName.Answer;
                }
            }
            return "NA";
        }

        //DeleteSubmissionCorp(string masterId);
        /// <summary>
        /// This method is used to Insert Bussiness Address based on Custom Type and user Inputs.
        /// </summary>
        /// <param name="customtype"></param>
        /// <param name="generalbusiness"></param>
        /// <returns>Return bool Result</returns>
        public bool InsertSubmissionLocation(int customtype, CofoHopDetailsModel generalbusiness)
        {
            bool status = false;
            string street = string.Empty;
            int streettype = Convert.ToInt32(generalbusiness.StreetTypeId);
            var Streets = streettyperepo.FindByStreetTypeId(streettype).ToList();
            var Street = Streets.FirstOrDefault();

            if (generalbusiness.StreetTypeId == 0)
            { street = ""; }
            else
            {
                street = generalbusiness.StreetType ?? "";
            }
            try
            {
                //generalbusiness.State = generalbusiness.State ?? "";
                //generalbusiness.Country = generalbusiness.Country ?? "";
                //if (generalbusiness.State != "" && generalbusiness.Country == "")
                //{
                //    if (generalbusiness.State.ToUpper().Trim() != "NOTSET")
                //    {
                //        generalbusiness.Country = "US";
                //    }
                //    else
                //    { generalbusiness.Country = ""; }
                //}
                generalbusiness.StreetTypeId = generalbusiness.StreetTypeId ?? 0;
                if (generalbusiness.StreetTypeId != 0)
                {
                    street = Street.StreetType ?? "";
                }
                var findMasterId = GetPrimisessAddress(customtype).ToList();
                if (findMasterId.Count == 0)
                {
                    if (generalbusiness.Number != "")
                    {
                        var submBusinessMaster = new SubmissionCofo_Hop_Ehop_Address
                        {
                            CustomTypeId = customtype,
                            CustomType = (generalbusiness.Type ?? "").Trim(),
                            Name = (generalbusiness.Name ?? "").Trim(),
                            Street = (generalbusiness.Street ?? "").Trim(),
                            StreetName = (generalbusiness.StreetName ?? "").Trim(),
                            StreetType = street,
                            Quadrant = (generalbusiness.Quadrant ?? "").Trim(),
                            UnitType = (generalbusiness.UnitType ?? "").Trim(),
                            Unit = (generalbusiness.Unit ?? "").Trim(),
                            City = (generalbusiness.City ?? "").Trim(),
                            State = (generalbusiness.State ?? "").Trim(),
                            Telephone = (generalbusiness.Telephone ?? "").Trim(),
                            Zip = (generalbusiness.Zip ?? "").Trim(),
                            // ReSharper disable once SimplifyConditionalTernaryExpression
                            IsValid = generalbusiness.IsValid == null ? false : Convert.ToBoolean(generalbusiness.IsValid),
                            Country = (generalbusiness.Country ?? "").Trim(),
                            AddressID = (generalbusiness.AddressId ?? "").Trim(),
                            AddressNumber = (generalbusiness.AddressNumber ?? "").Trim(),
                            AddressNumberSufix = (generalbusiness.AddressNumberSufix ?? "").Trim(),
                            Xcoord = (generalbusiness.Xcoord ?? "").Trim(),
                            Ycoord = (generalbusiness.Ycoord ?? "").Trim(),
                            Anc = (generalbusiness.Anc ?? "").Trim(),
                            Ward = (generalbusiness.Ward ?? "").Trim(),
                            Cluster = (generalbusiness.Cluster ?? "").Trim(),
                            Latitude = (generalbusiness.Latitude ?? "").Trim(),
                            Longitude = (generalbusiness.Longitude ?? "").Trim(),
                            Vote_Prcnct = (generalbusiness.Vote_Prcnct ?? "").Trim(),
                            Zone = (generalbusiness.Zone ?? "").Trim(),
                            Smd = (generalbusiness.SMD ?? "").Trim(),
                            Ssl = (generalbusiness.SSL ?? "").Trim()
                        };
                        Add(submBusinessMaster);
                        Save();
                    }
                    status = true;
                }
                else
                {
                    string number = (generalbusiness.Number ?? "").Trim();
                    if (generalbusiness.Number != "")
                    {
                        var submBusinessMaster = new SubmissionCofo_Hop_Ehop_Address
                        {
                            CustomTypeId = customtype,
                            SubmissionCofo_Hop_Ehop_AddressId = findMasterId.FirstOrDefault().SubmissionCofo_Hop_Ehop_AddressId,
                            CustomType = (generalbusiness.Type ?? "").Trim(),
                            Name = (generalbusiness.Name ?? "").Trim(),
                            Street = (generalbusiness.Street ?? "").Trim(),
                            StreetName = (generalbusiness.StreetName ?? "").Trim(),
                            StreetType = street,
                            Quadrant = (generalbusiness.Quadrant ?? "").Trim(),
                            UnitType = (generalbusiness.UnitType ?? "").Trim(),
                            Unit = (generalbusiness.Unit ?? "").Trim(),
                            City = (generalbusiness.City ?? "").Trim(),
                            State = (generalbusiness.State ?? "").Trim(),
                            Telephone = (generalbusiness.Telephone ?? "").Trim(),
                            Zip = (generalbusiness.Zip ?? "").Trim(),
                            IsValid = generalbusiness.IsValid == null ? false : Convert.ToBoolean(generalbusiness.IsValid),
                            Country = (generalbusiness.Country ?? "").Trim(),
                            AddressID = (generalbusiness.AddressId ?? "").Trim(),
                            AddressNumber = (generalbusiness.AddressNumber ?? "").Trim(),
                            AddressNumberSufix = (generalbusiness.AddressNumberSufix ?? "").Trim(),
                            Xcoord = (generalbusiness.Xcoord ?? "").Trim(),
                            Ycoord = (generalbusiness.Ycoord ?? "").Trim(),
                            Anc = (generalbusiness.Anc ?? "").Trim(),
                            Ward = (generalbusiness.Ward ?? "").Trim(),
                            Cluster = (generalbusiness.Cluster ?? "").Trim(),
                            Latitude = (generalbusiness.Latitude ?? "").Trim(),
                            Longitude = (generalbusiness.Longitude ?? "").Trim(),
                            Vote_Prcnct = (generalbusiness.Vote_Prcnct ?? "").Trim(),
                            Zone = (generalbusiness.Zone ?? "").Trim(),
                            Smd = (generalbusiness.SMD ?? "").Trim(),
                            Ssl = (generalbusiness.SSL ?? "").Trim()
                        };
                        int customId = findMasterId.FirstOrDefault().SubmissionCofo_Hop_Ehop_AddressId;
                        Update(submBusinessMaster, customId);
                        Save();
                        status = true;
                    }
                    else
                    {
                        var removebusiness = findMasterId.FirstOrDefault();
                        Delete(removebusiness);
                        Save();
                        status = true;
                    }
                }
                var presmisisaddress = new StringBuilder();
                presmisisaddress.Append((generalbusiness.Name ?? "").Trim());
                presmisisaddress.Append(" ");
                presmisisaddress.Append((generalbusiness.AddressNumber ?? "").Trim());
                presmisisaddress.Append(" ");
                presmisisaddress.Append((generalbusiness.AddressNumberSufix ?? "").Trim());
                presmisisaddress.Append(" ");
                presmisisaddress.Append((generalbusiness.StreetName ?? "").Trim());
                presmisisaddress.Append(" ");
                presmisisaddress.Append(GetStreetFullName(street));
                presmisisaddress.Append(" ");
                presmisisaddress.Append((generalbusiness.Quadrant ?? "").Trim());
                presmisisaddress.Append(" ");
                presmisisaddress.Append((generalbusiness.UnitType ?? "").Trim());
                presmisisaddress.Append(" ");
                presmisisaddress.Append((generalbusiness.Unit ?? "").Trim());
                presmisisaddress.Append(" ");
                presmisisaddress.Append((generalbusiness.City ?? "").Trim());
                presmisisaddress.Append(" ");
                presmisisaddress.Append(GetStateCode(generalbusiness.State ?? "", generalbusiness.Country ?? "").Trim());
                presmisisaddress.Append(" ");
                presmisisaddress.Append((GetCountryFullName(generalbusiness.Country ?? "")).Trim());
                presmisisaddress.Append(" ");
                presmisisaddress.Append((generalbusiness.Zip ?? "").Trim());
                submissionMasterrepo.UpdatePremisesAddress(generalbusiness.MasterId, presmisisaddress.ToString());
            }
            catch (Exception )
            { status = false; }

            return status;
        }

        /// <summary>
        /// This method is used to Delete Particular Business Address from the Database based on Custom Type Id.
        /// </summary>
        /// <param name="cofoModel"></param>
        /// <returns>Retrun Bool Result</returns>
        public bool DeleteBusinessAddress(CofoHopDetailsModel cofoModel)
        {
            bool status = false;

            try
            {
                var contact = FindBy(x => x.CustomTypeId == cofoModel.CofoHopId).Single();
                Delete(contact);
                Save();
                status = true;
            }
            catch (Exception )
            { status = false; }
            return status;
        }

        //public virtual void SaveChanges()
        //{
        //    Context.SaveChanges();
        //}
        /// <summary>
        /// This method is used to Delete Particular Business Address from the Database based on Custom Type Id.
        /// </summary>
        /// <param name="customTypeId"></param>
        /// <returns>Retrun Bool Result</returns>
        public bool DeleteHOP(int customTypeId)
        {
            var deletehop = FindBy(x => x.CustomTypeId == customTypeId).ToList();
            if (deletehop.Count() != 0)
            {
                Delete(deletehop.FirstOrDefault());
                Save();
            }
            return true;
        }
        /// <summary>
        /// This method is used to get state full name based on state code and country code
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
        /// This method is used to get state code based on state name and country code
        /// </summary>
        /// <param name="stateName"></param>
        /// <param name="countryCode"></param>
        /// <returns></returns>
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
        /// This method is used to get country full name based on country code
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns>Return string value</returns>
        public string GetCountryFullName(string countryCode)
        {
            var countrydata = _MasterCountryRepository.FindCountryBasedOnCode(countryCode).ToList();
            if (countrydata.Any())
            {
                return countrydata.FirstOrDefault().CountryName;
            }
            else
            {
                return countryCode;
            }
        }
        /// <summary>
        /// This method is used to get street full name based on street code.
        /// </summary>
        /// <param name="streetCode"></param>
        /// <returns>Return string value</returns>
        public string GetStreetFullName(string streetCode)
        {
            var streetdata = streettyperepo.FindStreetIdbyCode(streetCode).ToList();
            if (streetdata.Any())
            {
                return streetdata.FirstOrDefault().StreetType;
            }
            else
            {
                return streetCode;
            }
        }
    }
}