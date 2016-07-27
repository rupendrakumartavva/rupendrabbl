using BusinessCenter.Common;
using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessCenter.Data.Implementation
{
    public class SubmissionCofoHopeHopRepository : GenericRepository<SubmissionCofo_Hop_Ehop>, ISubmissionCofoHopeHopRepository
    {
        protected ISubmissionCofoHopeHopAddressRepository Subaddressrepo;
        protected ISubmissionMasterApplicationChcekListRepository SubcheckRepo;
        protected ISubmissionMasterRepository _subMasterRepo;
        protected IFixFeeRepository fixfeeRepo;
        protected IStreetTypesRepository StreetTypeRep;
        protected ISubmissionDocumentRepository docrep;

        public SubmissionCofoHopeHopRepository(IUnitOfWork context, ISubmissionCofoHopeHopAddressRepository subaddressRepository,
            ISubmissionMasterApplicationChcekListRepository subcheckRepository, ISubmissionMasterRepository _subMasterRepository, IStreetTypesRepository streetTypeRep,
            IFixFeeRepository fixfeeRepository, ISubmissionDocumentRepository docrepository)
            : base(context)
        {
            Subaddressrepo = subaddressRepository;
            SubcheckRepo = subcheckRepository;
            _subMasterRepo = _subMasterRepository;
            StreetTypeRep = streetTypeRep;
            fixfeeRepo = fixfeeRepository;
            docrep = docrepository;
        }

        /// <summary>
        /// This method is used to get the specific Cofo or Hop or Ehop details Based on Application Unique Id.
        /// </summary>
        /// <param name="generalBusiness"></param>
        /// <returns>Retrun Specific Cofo or Hop or Ehop details</returns>
        public IEnumerable<SubmissionCofo_Hop_Ehop> FindByID(GeneralBusiness generalBusiness)
        {
            var cofoHopDetails = FindBy(x => x.MasterId == generalBusiness.MasterId).ToList();
            return cofoHopDetails;
        }

        /// <summary>
        /// This method is used to get the specific Cofo or Hop or Ehop details Based on Application Unique Id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Retrun Specific Cofo or Hop or Ehop details</returns>
        public IEnumerable<SubmissionCofo_Hop_Ehop> FindByID(string masterId)
        {
            var cofoHopDetails = FindBy(x => x.MasterId == masterId).ToList();
            return cofoHopDetails;
        }

        /// <summary>
        /// This method is used to Insert Cofo or Hop or Ehop details Based on Application Unique Id.
        /// </summary>
        /// <param name="generalbusiness"></param>
        /// <returns>Return bool Result</returns>
        public bool InsertSubmissionLocation(CofoHopDetailsModel generalbusiness)
        {
            var status = false;
            generalbusiness.Type = generalbusiness.Type ?? "NO";
            try
            {
                var mailtype = _subMasterRepo.FindByMasterID(generalbusiness.MasterId).FirstOrDefault().UserSelectMailAddressType;
                string primisesaddress = mailtype ?? "";
                string number = generalbusiness.Number ?? "";
                var findMasterId = FindByID(generalbusiness.MasterId);
                if (!findMasterId.Any())
                {
                    if (generalbusiness.Number != "")
                    {
                        if (generalbusiness.Type.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.eHOP).ToUpper())
                        {
                            Random rnd = new Random();
                            generalbusiness.Number = "EHOP" + DateTime.Now.Year.ToString().Substring(2, 2) + "9" + rnd.Next(11111, 99999);
                            generalbusiness.DateofIssue = Convert.ToDateTime(System.DateTime.Now.ToShortDateString()).ToString();
                        }
                        var sublocandstruc = new SubmissionCofo_Hop_Ehop
                        {
                            MasterId = generalbusiness.MasterId ?? "",
                            UserSelectType = generalbusiness.Type ?? "",
                            Number = generalbusiness.Number ?? "",
                            DateOfIssuance = generalbusiness.DateofIssue == null ? Convert.ToDateTime("01/01/1900")
                                    : Convert.ToDateTime(generalbusiness.DateofIssue),
                            DoNotHaveCofo = generalbusiness.DonothaveCof,
                            IsUploadSupportDoc = generalbusiness.IsUploadSupportDoc,
                            IsValid = generalbusiness.IsValid,
                            IseHOPEligibility = generalbusiness.IseHOPEligibility,
                            EHopEligibilityType = generalbusiness.EHopEligibilityType ?? "",
                            ConfirmeHOPEligibilityType = generalbusiness.ConfirmeHOPEligibilityType,
                            OccupancyAddressValidate = generalbusiness.OccupancyAddssValidate ?? ""
                        };
                        Add(sublocandstruc);
                        Save();
                        if (primisesaddress.ToUpper() ==
                           GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.PrimsessAddress).ToUpper())
                        {
                            SubcheckRepo.UpdateMailTrueStatus(generalbusiness.MasterId);
                        }
                        int lastinsertedId = sublocandstruc.SubCofoHopEhopId;
                        Subaddressrepo.InsertSubmissionLocation(lastinsertedId, generalbusiness);
                        if (generalbusiness.Type.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.eHOP).ToUpper())
                        {
                            var master = _subMasterRepo.FindByMasterID(generalbusiness.MasterId).ToList();
                            if (master.Count() != 0)
                            {
                                var fixfee = fixfeeRepo.AllFixFees().FirstOrDefault();
                                decimal GrandTotal = Convert.ToDecimal(master.FirstOrDefault().GrandTotal);
                                decimal subtotal = GrandTotal + ((fixfee.eHOPFee.Value / 100) * 10) + fixfee.eHOPFee.Value;
                                _subMasterRepo.UpdateEhopTotal(subtotal, generalbusiness.MasterId);
                            }
                        }
                        SubcheckRepo.UpdateDetails(generalbusiness);
                    }
                    status = true;
                }
                else
                {
                    if (findMasterId.First().Number.Trim() != generalbusiness.Number)
                    {
                        string doctype = "";
                        if (generalbusiness.Type.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.HOP).ToUpper())
                        {
                            doctype = GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.HopDocument).ToUpper();
                            docrep.DeleteHopcofo(generalbusiness.MasterId, doctype);
                        }
                        else if (generalbusiness.Type.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.COFO).ToUpper())
                        {
                            doctype = GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.CofoDocument).ToUpper();
                            docrep.DeleteHopcofo(generalbusiness.MasterId, doctype);
                        }
                        else
                        {
                            doctype = GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.HopDocument).ToUpper();
                            docrep.DeleteHopcofo(generalbusiness.MasterId, doctype);
                            doctype = GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.CofoDocument).ToUpper();
                            docrep.DeleteHopcofo(generalbusiness.MasterId, doctype);
                        }
                    }

                    if (generalbusiness.Number != "")
                    {
                        if (generalbusiness.Type.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.eHOP).ToUpper())
                        {
                            generalbusiness.Number = findMasterId.FirstOrDefault().Number ?? "";
                        }

                        var sublocandstruc = new SubmissionCofo_Hop_Ehop
                        {
                            SubCofoHopEhopId = findMasterId.FirstOrDefault().SubCofoHopEhopId,
                            MasterId = generalbusiness.MasterId ?? "",
                            UserSelectType = generalbusiness.Type ?? "",
                            Number = generalbusiness.Number ?? "",
                            DateOfIssuance =
                                generalbusiness.DateofIssue == null ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(generalbusiness.DateofIssue),
                            DoNotHaveCofo = generalbusiness.DonothaveCof,
                            IsUploadSupportDoc = generalbusiness.IsUploadSupportDoc,
                            IsValid = generalbusiness.IsValid,
                            IseHOPEligibility = generalbusiness.IseHOPEligibility,
                            EHopEligibilityType = generalbusiness.EHopEligibilityType ?? "",
                            ConfirmeHOPEligibilityType = generalbusiness.ConfirmeHOPEligibilityType,
                            OccupancyAddressValidate = generalbusiness.OccupancyAddssValidate ?? ""
                        };

                        bool donthave = generalbusiness.DonothaveCof;
                        int findMasterId1 = findMasterId.FirstOrDefault().SubCofoHopEhopId;

                        Update(sublocandstruc, findMasterId1);
                        Save();
                        if (primisesaddress.ToUpper().Trim() ==
                           GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.PrimsessAddress).ToUpper().Trim())
                        {
                            SubcheckRepo.UpdateMailTrueStatus(generalbusiness.MasterId);
                        }
                        int lastinsertedId = sublocandstruc.SubCofoHopEhopId;
                        Subaddressrepo.InsertSubmissionLocation(lastinsertedId, generalbusiness);
                    }
                    else if (generalbusiness.Type.ToUpper() != GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.NoPo).ToUpper())
                    {
                        int findMasterId1 = findMasterId.FirstOrDefault().SubCofoHopEhopId;
                        Subaddressrepo.InsertSubmissionLocation(findMasterId1, generalbusiness);
                        Delete(findMasterId.FirstOrDefault());
                        Save();
                    }
                    if (generalbusiness.Number != "")
                    {
                        SubcheckRepo.UpdateDetails(generalbusiness);
                    }
                    else
                    {
                        var cofoModel = new CofoHopDetailsModel();
                        cofoModel.MasterId = generalbusiness.MasterId;
                        if (generalbusiness.Type.ToUpper() != GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.NoPo).ToUpper())
                        {
                            SubcheckRepo.UpdateCofodetails(cofoModel);
                        }
                        if (mailtype.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.PrimsessAddress).ToUpper()
                            && generalbusiness.Type.ToUpper() != GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.NoPo).ToUpper())
                        {
                            var general = new GeneralBusiness();
                            general.MasterId = generalbusiness.MasterId;
                            general.UserSelectTpe = "";
                            SubcheckRepo.UpdateMailStatus(generalbusiness.MasterId);
                        }
                        if (number == "")
                        {
                            string doctype = "";
                            if (generalbusiness.Type.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.HOP).ToUpper())
                            {
                                doctype = GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.HopDocument).ToUpper();
                                docrep.DeleteHopcofo(generalbusiness.MasterId, doctype);
                            }
                            else if (generalbusiness.Type.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.COFO).ToUpper())
                            {
                                doctype = GenericEnums.GetEnumDescription(GenericEnums.CategoryQuestions.HopDocument).ToUpper();
                                docrep.DeleteHopcofo(generalbusiness.MasterId, doctype);
                            }
                        }
                    }
                    status = true;
                }
            }
            catch (Exception)
            { status = false; }
            return status;
        }

        /// <summary>
        /// This method is used to Get Primisess Address using User Details.
        /// </summary>
        /// <param name="generalDetailsModel"></param>
        /// <returns>Retrun General Business</returns>
        public GeneralBusiness GetPrimisessAddress(GeneralBusiness generalDetailsModel)
        {
            try
            {
                int customid = 0;
                string fileNumber = string.Empty;
                var locationdetails = FindByID(generalDetailsModel);
                if (locationdetails.Count() != 0)
                {
                    var cofodetails = locationdetails.FirstOrDefault();
                    customid = cofodetails.SubCofoHopEhopId;
                    fileNumber = cofodetails.Number ?? "";
                }
                var premisisAddress = Subaddressrepo.GetPrimisessAddress(customid);
                if (premisisAddress.Count() != 0)
                {
                    var premAddress = premisisAddress.FirstOrDefault();
                    generalDetailsModel.UserType = premAddress.CustomType;
                    generalDetailsModel.FileNumber = fileNumber;
                    generalDetailsModel.BusinessName = "";
                    generalDetailsModel.FirstName = premAddress.Name ?? "";
                    generalDetailsModel.LastName = "";
                    generalDetailsModel.MiddleName = "";
                    generalDetailsModel.BusinessAddressLine1 = premAddress.StreetName ?? "";
                    premAddress.StreetType = premAddress.StreetType ?? "";
                    var streetdetails = StreetTypeRep.FindStreetIdbyType(premAddress.StreetType).ToList();
                    if (streetdetails.Count != 0)
                    {
                        premAddress.StreetType = (streetdetails.FirstOrDefault().StreetCode).Trim();
                    }
                    generalDetailsModel.BusinessAddressLine2 = premAddress.StreetType ?? "";
                    generalDetailsModel.BusinessAddressLine3 = premAddress.Street ?? "";
                    generalDetailsModel.BusinessCity = premAddress.City ?? "";
                    generalDetailsModel.BusinessState = premAddress.State ?? "";
                    generalDetailsModel.BusinessCountry = premAddress.Country ?? "";

                    generalDetailsModel.Quardrant = premAddress.Quadrant ?? "";
                    generalDetailsModel.UnitType = premAddress.UnitType ?? "";
                    generalDetailsModel.Unit = premAddress.Unit ?? "";
                    generalDetailsModel.ZipCode = premAddress.Zip ?? "";
                    generalDetailsModel.Telphone = premAddress.Telephone ?? "";
                    generalDetailsModel.Email = "";
                    generalDetailsModel.Dropdownlist = DropDownBind().ToList();
                    generalDetailsModel.OccupancyAddssValidate = locationdetails.FirstOrDefault().OccupancyAddressValidate ?? "";
                    generalDetailsModel.AddressID = premAddress.AddressID == null ? "" : premAddress.AddressID.ToString().Trim();
                    generalDetailsModel.AddressNumber = premAddress.AddressNumber ?? "";
                    generalDetailsModel.AddressNumberSufix = premAddress.AddressNumberSufix ?? "";
                    generalDetailsModel.Anc = premAddress.Anc ?? "";
                    generalDetailsModel.Cluster = premAddress.Cluster ?? "";
                    generalDetailsModel.Latitude = premAddress.Latitude ?? "";
                    generalDetailsModel.Longitude = premAddress.Longitude ?? "";
                    generalDetailsModel.Vote_Prcnct = premAddress.Vote_Prcnct ?? "";
                    generalDetailsModel.Ward = premAddress.Ward ?? "";
                    generalDetailsModel.Xcoord = premAddress.Xcoord ?? "";
                    generalDetailsModel.Ycoord = premAddress.Ycoord ?? "";
                    generalDetailsModel.Zone = premAddress.Zone ?? "";
                    generalDetailsModel.SSL = premAddress.Ssl ?? "";
                    generalDetailsModel.Smd = premAddress.Smd ?? "";
                }
                return generalDetailsModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method is used to get the Street Down in the Drop Down
        /// </summary>
        /// <returns>Retrun Street Name</returns>
        public List<StreetDetails> DropDownBind()
        {
            try
            {
                var streetTypes = StreetTypeRep.AllStreetTypes().Select(i => new { i.StreetType, i.StreetCode });
                var dropdown = new List<string>();
                return streetTypes.Select(item => new StreetDetails
                {
                    StreetType = item.StreetType.Replace(System.Environment.NewLine, "").ToString().Trim(),
                    StreetCode = item.StreetCode.Replace(System.Environment.NewLine, "").ToString().Trim()
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method is used to Delete Specific CofoHop Details using Application Unique Id.
        /// </summary>
        /// <param name="cofoModel"></param>
        /// <returns>Retrun Bool Result</returns>
        public bool DeleteCofo(CofoHopDetailsModel cofoModel)
        {
            bool status = false;
            try
            {
                var cofodetails = (FindBy(x => x.MasterId == cofoModel.MasterId)).ToList();

                if (cofodetails.Count != 0)
                {
                    var contact = cofodetails.Single();
                    cofoModel.CofoHopId = cofodetails.FirstOrDefault().SubCofoHopEhopId;
                    Subaddressrepo.DeleteBusinessAddress(cofoModel);
                    contact.MasterId = cofoModel.MasterId;
                    contact.UserSelectType = string.Empty;
                    contact.Number = string.Empty;
                    contact.DateOfIssuance = Convert.ToDateTime("01/01/1900");
                    contact.DoNotHaveCofo = cofoModel.DonothaveCof;
                    contact.IsUploadSupportDoc = false;
                    contact.IsValid = false;
                    contact.IseHOPEligibility = false;
                    contact.EHopEligibilityType = string.Empty;
                    contact.ConfirmeHOPEligibilityType = false;
                    contact.OccupancyAddressValidate = cofoModel.OccupancyAddssValidate ?? "";
                    Update(contact, contact.SubCofoHopEhopId);
                    Save();
                    SubcheckRepo.UpdateCofodetails(cofoModel);
                    var mailtype = _subMasterRepo.FindByMasterID(cofoModel.MasterId).FirstOrDefault().UserSelectMailAddressType;
                    if (mailtype.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.PrimsessAddress).ToUpper())
                    {
                        SubcheckRepo.UpdateMailStatus(cofoModel.MasterId);
                    }
                    if (cofoModel.Type.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.eHOP).ToUpper())
                    {
                        var master = _subMasterRepo.FindByMasterID(cofoModel.MasterId).ToList();
                        if (master.Count() != 0)
                        {
                            var fixfee = fixfeeRepo.AllFixFees().FirstOrDefault();
                            decimal GrandTotal = Convert.ToDecimal(master.FirstOrDefault().GrandTotal);
                            decimal subtotal = GrandTotal - (((fixfee.eHOPFee.Value / 100) * 10) + fixfee.eHOPFee.Value);
                            _subMasterRepo.UpdateEhopTotal(subtotal, cofoModel.MasterId);
                        }
                    }
                    status = true;
                }
                else
                {
                    if (cofoModel.DonothaveCof == false && cofoModel.Type.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.COFO).ToUpper())
                    {
                        var sublocandstruc = new SubmissionCofo_Hop_Ehop
                            {
                                MasterId = cofoModel.MasterId,
                                UserSelectType = string.Empty,
                                Number = cofoModel.Number,
                                DateOfIssuance = Convert.ToDateTime("01/01/1900"),
                                DoNotHaveCofo = cofoModel.DonothaveCof,
                                IsUploadSupportDoc = false,
                                IsValid = false,
                                IseHOPEligibility = false,
                                EHopEligibilityType = string.Empty,
                                ConfirmeHOPEligibilityType = false,
                                OccupancyAddressValidate = string.Empty
                            };
                        Add(sublocandstruc);
                        Save();
                    }
                    else
                        if (cofoModel.DonothaveCof == true && cofoModel.Type.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.DocumentTypes.COFO).ToUpper())
                        {
                            var sublocandstruc = new SubmissionCofo_Hop_Ehop
                            {
                                MasterId = cofoModel.MasterId,
                                UserSelectType = string.Empty,
                                Number = cofoModel.Number,
                                DateOfIssuance = Convert.ToDateTime("01/01/1900"),
                                DoNotHaveCofo = cofoModel.DonothaveCof,
                                IsUploadSupportDoc = false,
                                IsValid = false,
                                IseHOPEligibility = false,
                                EHopEligibilityType = string.Empty,
                                ConfirmeHOPEligibilityType = false,
                                OccupancyAddressValidate = string.Empty
                            };
                            Add(sublocandstruc);
                            Save();
                        }
                    SubcheckRepo.UpdateCofodetails(cofoModel);

                    status = true;
                }
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
        /// This method is used to Delete Specific CofoHop Details and update Checklist as well using Application Unique Id.
        /// </summary>
        /// <param name="MasterID"></param>
        /// <returns>Retrun bool Result</returns>
        public bool DeleteHOP(string MasterID)
        {
            try
            {
                var deletehop = FindBy(x => x.MasterId == MasterID).ToList();
                if (deletehop.Count() != 0)
                {
                    Subaddressrepo.DeleteHOP(deletehop.First().SubCofoHopEhopId);
                    Delete(deletehop.FirstOrDefault());
                    Save();
                    CofoHopDetailsModel cofoModel = new CofoHopDetailsModel();
                    cofoModel.MasterId = MasterID;
                    SubcheckRepo.UpdateCofodetails(cofoModel);
                    var mailtype = _subMasterRepo.FindByMasterID(cofoModel.MasterId).FirstOrDefault().UserSelectMailAddressType;
                    if (mailtype.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.PrimsessAddress).ToUpper())
                    {
                        SubcheckRepo.UpdateMailStatus(cofoModel.MasterId);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// This method is used to Delete Specific CofoHop Details and update Checklist as well using Application Unique Id.
        /// </summary>
        /// <param name="cofoModel"></param>
        /// <returns>Return bool value</returns>
        public bool DeleteHopPrimsesAddrssOnly(CofoHopDetailsModel cofoModel)
        {
            var deletehop = FindBy(x => x.MasterId == cofoModel.MasterId).ToList();
            if (deletehop.Count() != 0)
            {
                Subaddressrepo.DeleteHOP(deletehop.First().SubCofoHopEhopId);
                SubcheckRepo.UpdateCorpCheckStatus(cofoModel.MasterId, cofoModel.Type, string.Empty);
                var mailtype = _subMasterRepo.FindByMasterID(cofoModel.MasterId).FirstOrDefault().UserSelectMailAddressType;
                if (mailtype.ToUpper() == GenericEnums.GetEnumDescription(GenericEnums.GenaralTypes.PrimsessAddress).ToUpper())
                {
                    SubcheckRepo.UpdateMailStatus(cofoModel.MasterId);
                }
            }
            return true;
        }
        /// <summary>
        /// This method is used to submission cofo hop ehop details based on unique id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return SubmissionCofo_Hop_Ehop data</returns>
        public IEnumerable<SubmissionCofo_Hop_Ehop> EhopNumberWithMasterId(string masterId)
        {
            var cofoHopDetails = FindBy(x => x.MasterId == masterId);
            return cofoHopDetails;
        }
        /// <summary>
        /// This method is used to get SubmissionCofo_Hop_Ehop_Address based on unique id
        /// </summary>
        /// <param name="customTypeid"></param>
        /// <returns>Return SubmissionCofo_Hop_Ehop_Address data</returns>
        public IEnumerable<SubmissionCofo_Hop_Ehop_Address> GetPrimisessAddress(int customTypeid)
        {
            var businessAddress =
                Subaddressrepo.GetPrimisessAddress(customTypeid);
            return businessAddress;
        }
        /// <summary>
        /// This method is used to get tax and revneu intial display based on unique id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return TaxAndReneueInitailDisplay data</returns>
        public TaxAndReneueInitailDisplay DisplayTaxAndRevenuWithPrimisessDetails(string masterId)
        {
            var taxAndReneueInitailDisplay = new TaxAndReneueInitailDisplay();
            var getSubmissionCofoHopDetails = FindBy(x => x.MasterId == masterId).ToList();

            if (getSubmissionCofoHopDetails.Count > 0)
            {
                var submissionCofoHopEhop = getSubmissionCofoHopDetails.FirstOrDefault();
                if (submissionCofoHopEhop != null)
                {
                    var businessAddress =
                        Subaddressrepo.GetPrimisessAddress(submissionCofoHopEhop.SubCofoHopEhopId);
                    var getBusinessAddress = businessAddress.FirstOrDefault();
                    if (getBusinessAddress != null)
                    {
                        var sb = new StringBuilder();
                        sb.Append(getBusinessAddress.AddressNumber);
                        sb.Append(' ');
                        sb.Append(getBusinessAddress.AddressNumberSufix);
                        sb.Append(' ');
                        sb.Append(getBusinessAddress.StreetName);
                        sb.Append(' ');
                        sb.Append(GetFullStreetName(getBusinessAddress.StreetType));
                        sb.Append(' ');
                        sb.Append(getBusinessAddress.Quadrant);
                        taxAndReneueInitailDisplay.FullAddress = sb.ToString();
                    }
                }
            }

            taxAndReneueInitailDisplay.BussinessOwnerFullName = Subaddressrepo.GetBusinessOwnerFullName(masterId);
            taxAndReneueInitailDisplay.TradeName = Subaddressrepo.GetTradeNameWithSubmissionQuestions(masterId);
            return taxAndReneueInitailDisplay;
        }
        /// <summary>
        /// This method is used to street full name based on street code.
        /// </summary>
        /// <param name="streetCode"></param>
        /// <returns>Return string value</returns>
        private string GetFullStreetName(string streetCode)
        {
            var getStreetName = StreetTypeRep.FindStreetIdbyCode(streetCode).ToList();
            if (getStreetName.Count > 0)
            {
                return getStreetName.FirstOrDefault().StreetType.Trim();
            }
            else
            {
                return streetCode;
            }
        }
        /// <summary>
        /// This method is used to business owner name based on unique id.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns>Return string value</returns>
        public string BusinessOwnerName(string masterId)
        {
            return Subaddressrepo.GetBusinessOwnerFullName(masterId);
        }
    }
}