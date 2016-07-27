using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class DCBC_ENTITY_Cof_ORepository : GenericRepository<DCBC_ENTITY_Cof_O>, IDCBC_ENTITY_Cof_ORepository
    {
        protected ISubmissionCofoHopeHopRepository SubmissionlsReposiotry;
        protected IStreetTypesRepository streetrepo;
        protected ISubmissionMasterRepository _subMasterRepo;
        protected ISubmissionMasterApplicationChcekListRepository subcheckRepo;
        protected IEtlAddressAndParcelRepository _etlAddressRepository;
        protected ISubmissionCofoHopeHopAddressRepository SubmissionBaReposiotry;
           public DCBC_ENTITY_Cof_ORepository(IUnitOfWork context,   ISubmissionCofoHopeHopRepository submissionlsReposiotry,
               IStreetTypesRepository streetRepository, ISubmissionMasterRepository _subMasterRepository,
               ISubmissionMasterApplicationChcekListRepository subcheckRepository, ISubmissionCofoHopeHopAddressRepository submissionCofoHopeHopAddressRepository,
               IEtlAddressAndParcelRepository etlAddressRepository)
            : base(context)
        {
               SubmissionlsReposiotry = submissionlsReposiotry;
               streetrepo = streetRepository;
               _subMasterRepo = _subMasterRepository;
               subcheckRepo = subcheckRepository;
               SubmissionBaReposiotry = submissionCofoHopeHopAddressRepository;
               _etlAddressRepository = etlAddressRepository;
        }
        /// <summary>
           ///  This method is retrieve data from CofO/HOP model details, given Number, Date time,type and valid contion
        /// </summary>
        /// <param name="cofoHopDetailsModel"></param>
        /// <returns>CofO?HOP details </returns>
           public List<CofoHopDetailsModel> FindByNumberandDateofIssue(CofoHopDetailsModel cofoHopDetailsModel)
           {
               var cofdetailslist = new List<CofoHopDetailsModel>();
            try
            {
                DateTime convertDateinput = Convert.ToDateTime(cofoHopDetailsModel.DateofIssue);
                string type = String.Empty;
                if (cofoHopDetailsModel.Type.ToUpper() == "COFO")
                {
                    type = "CERTIFICATE OF OCCUPANCY PERMIT";
                }
                else if (cofoHopDetailsModel.Type.ToUpper() == "HOP")
                {
                    type = "HOME OCCUPATION PERMIT";
                }

                var cofhopdetail = (from cofhop in (
                        FindBy(x => x.b1_Alt_ID == cofoHopDetailsModel.Number.Trim() 
                            && x.B1_APP_TYPE_ALIAS.Replace(System.Environment.NewLine, "").ToUpper().Trim() == type.ToUpper() && x.DCBC_Status.ToUpper() == "VALID"))
                                    select cofhop).ToList();
                //&& EntityFunctions.TruncateTime(x.Status_DATE) == EntityFunctions.TruncateTime(convertDateinput)
                var getSubDetails = SubmissionlsReposiotry.FindByID(cofoHopDetailsModel.MasterId).FirstOrDefault();


                if (cofhopdetail.Count() != 0)
                {
                    var cofodetails = cofhopdetail.FirstOrDefault();
                    int streetid = 0;
                    cofoHopDetailsModel.MasterId = cofoHopDetailsModel.MasterId;
                    cofoHopDetailsModel.CofoHopId = 1;//item.CofoHopId == 0 ? 0 : item.CofoHopId;
                    string issuedate =Convert.ToDateTime(cofodetails.Status_DATE??Convert.ToDateTime("01/01/1900")).ToString("MM/dd/yyyy");
                    cofoHopDetailsModel.DateofIssue = issuedate;
                    cofoHopDetailsModel.Number = (cofodetails.b1_Alt_ID ?? "").Trim();
                    cofoHopDetailsModel.Quadrant = (cofodetails.B1_STR_SUFFIX_DIR ?? "").Trim();
                   
                    cofoHopDetailsModel.City = (cofodetails.B1_SITUS_CITY ?? "").Trim();
                    cofoHopDetailsModel.Zip =(cofodetails.B1_SITUS_ZIP ?? "").Trim();
                    cofoHopDetailsModel.State =(cofodetails.B1_SITUS_STATE ?? "").Trim();
                    cofoHopDetailsModel.AddressNumberSufix = (cofodetails.B1_HSE_FRAC_NBR_START ?? "").Trim();
                    cofoHopDetailsModel.Country = "United States";
                    //if (_etlAddressRepository.FindByStreetNumber(cofoHopDetailsModel.AddressNumberSufix).Any())
                    //{
                       
                    //}
                    //else { cofoHopDetailsModel.IsStreetNumber = true; }

                    cofoHopDetailsModel.AddressNumber = cofodetails.B1_HSE_NBR_START==null?"":(cofodetails.B1_HSE_NBR_START.ToString() ?? "").Trim();
                  
                    cofoHopDetailsModel.Street = "";
                    string streetname = (cofodetails.B1_STR_SUFFIX ?? "").Trim();
                                  
                    var streettype =
                      streetrepo.FindStreetIdbyCode(streetname).ToList();
                    if (streettype.Count() != 0)
                    {
                        streetid = streettype.First().StreetTypeId;
                        cofoHopDetailsModel.StreetType = streettype.First().StreetType ?? "";
                    }
                    else
                    {
                       
                        streetid = 0;
                        cofoHopDetailsModel.StreetType = "";
                    }
                    cofoHopDetailsModel.StreetName = (cofodetails.B1_STR_NAME ?? "").Trim();
                   
                    cofoHopDetailsModel.Telephone = string.Empty;
                    cofoHopDetailsModel.Unit = (cofodetails.B1_UNIT_START ?? "").Trim();
                    cofoHopDetailsModel.UnitType = string.Empty;
                    cofoHopDetailsModel.StreetTypeId = streetid;
                    cofoHopDetailsModel.DonothaveCof = Convert.ToBoolean(cofoHopDetailsModel.DonothaveCof);
                    if (getSubDetails != null)
                    {
                        cofoHopDetailsModel.OccupancyAddssValidate = getSubDetails.OccupancyAddressValidate;
                    }
                    else
                    {
                        cofoHopDetailsModel.OccupancyAddssValidate = string.Empty;
                    }
                    cofoHopDetailsModel.IsStreetNumber = _etlAddressRepository.FindByStreetNumber(cofoHopDetailsModel).Any();
                    cofoHopDetailsModel.IsStreetName = _etlAddressRepository.FindByStreetName(cofoHopDetailsModel).Any();
                    cofoHopDetailsModel.IsStreetType = _etlAddressRepository.FindByStreetType(cofoHopDetailsModel,streetname).Any();
                    cofoHopDetailsModel.IsQuadrant = _etlAddressRepository.FindByQuadrant(cofoHopDetailsModel, streetname).Any();
                    var cofocheckdetails = _etlAddressRepository.FindByDetails(cofoHopDetailsModel, streetname).ToList();
                    if (cofocheckdetails.Any())
                    {
                        var cofocheckdetailslist=cofocheckdetails.FirstOrDefault();
                        cofoHopDetailsModel.IsCofoHop = true;
                        cofoHopDetailsModel. UnitType = (cofocheckdetailslist.L1_UNIT_TYPE ?? "").Trim();
                        cofoHopDetailsModel.Xcoord = string.Empty;
                        cofoHopDetailsModel.Ycoord =string.Empty;
                        cofoHopDetailsModel.Anc = (cofocheckdetailslist.L1_UDF3 ?? "").Trim();
                        cofoHopDetailsModel.Ward = (cofocheckdetailslist.L1_UDF1 ?? "").Trim();
                        cofoHopDetailsModel.Cluster = string.Empty;
                        cofoHopDetailsModel. Latitude = string.Empty;
                        cofoHopDetailsModel.Longitude = string.Empty;
                        cofoHopDetailsModel.Vote_Prcnct = string.Empty;
                        cofoHopDetailsModel.UnitNumber = (cofocheckdetailslist.L1_UNIT_START??"").Trim();
                        cofoHopDetailsModel.Zone = (cofocheckdetailslist.L1_UDF2 ?? "").Trim();
                        cofoHopDetailsModel.SMD = (cofocheckdetailslist.L1_UDF4 ?? "").Trim();
                        cofoHopDetailsModel.SSL = (cofocheckdetailslist.L1_PARCEL_NBR ?? "").Trim();
                    }
                    else
                    {
                        cofoHopDetailsModel.IsCofoHop = false;
                    }
                }
                else
                {
                    cofoHopDetailsModel.Status = "NODATA";
                }
                cofdetailslist.Add(cofoHopDetailsModel);

            }
            catch (Exception)
            {
                
                throw;
            }
               
               return cofdetailslist;
           }
        /// <summary>
           /// This method is retrieve data from  DCBC_ENTITY_Cof_O table, given Number and Date time
        /// </summary>
        /// <param name="number"></param>
        /// <param name="dateofissue"></param>
           /// <returns>DCBC_ENTITY_Cof_O data</returns>
           public  List<DCBC_ENTITY_Cof_O> FindByNumber(string number, string dateofissue)
           {
            try
            {
                DateTime convertDateinput = Convert.ToDateTime(dateofissue);
                var cofhopdetail = (from cofhop in (
                       FindBy(x => x.b1_Alt_ID.Trim() == number.Trim()))
                                    select cofhop).ToList();
                return cofhopdetail;
                //&& EntityFunctions.TruncateTime(x.Status_DATE) == EntityFunctions.TruncateTime(convertDateinput)
            }
            catch (Exception)
            {
                
                throw;
            }
              
           }
        /// <summary>
        /// This method is get all the Street Details.
        /// </summary>
        /// <returns></returns>
           public List<StreetDetails> DropDownBind()
           {
               try
               {
                   var streets = new List<StreetDetails>();
                   var streetTypes = streetrepo.AllStreetTypes().Select(i => new { i.StreetType, i.StreetCode });
                   var dropdown = new List<string>();
                   foreach (var item in streetTypes)
                   {
                       StreetDetails st = new StreetDetails();
                       st.StreetType = item.StreetType.Replace(System.Environment.NewLine, "").ToString().Trim();
                       st.StreetCode = item.StreetCode.Replace(System.Environment.NewLine, "").ToString().Trim();
                       streets.Add(st);
                   }
                   return streets;
               }
               catch (Exception)
               {

                   throw;
               }
           }

           /// <summary>
           /// This method is used to display previously saved CofO/HOP/eHOP data, given Application unique id
           /// </summary>
           /// <param name="masterId"></param>
           /// <returns>List of CofO/HOP/eHOP details</returns>
           public List<CofoHopDetailsModel> GetSubmissionCofoOrHopDetails(string masterId)
           {
               try
               {
                   var cofdetailslist = new List<CofoHopDetailsModel>();
                   var getSubCofoDetails = SubmissionlsReposiotry.FindByID(masterId).ToList();
                   if (getSubCofoDetails.Count != 0)
                   {
                       var getSubDetails = getSubCofoDetails.FirstOrDefault();
                       CofoHopDetailsModel addDetails = new CofoHopDetailsModel();
                       var masterData = _subMasterRepo.FindByMasterID(masterId).ToList();
                       if (masterData.Count() != 0)
                       {
                           var subMasterData = masterData.FirstOrDefault();
                           addDetails.TradeName = (subMasterData.TradeName ?? "").Trim();
                           addDetails.BusinessStructure = (subMasterData.BusinessStructure ?? "").Trim();
                       }
                       else
                       {
                           addDetails.TradeName = null;
                           addDetails.BusinessStructure = null;
                       }
                       addDetails.Number = (getSubDetails.Number ?? "").Trim();
                       addDetails.MasterId = masterId;
                       addDetails.OccupancyAddssValidate = (getSubDetails.OccupancyAddressValidate ?? "").Trim();
                       addDetails.DonothaveCof = getSubDetails.DoNotHaveCofo ?? false;
                       var getFullAddress = SubmissionBaReposiotry.GetPrimisessAddress(getSubDetails.SubCofoHopEhopId,
                           getSubDetails.UserSelectType).FirstOrDefault();
                       if (getFullAddress != null)
                       {
                           addDetails.DateofIssue = Convert.ToDateTime(getSubDetails.DateOfIssuance).ToString("MM/dd/yyyy");
                           addDetails.Street = (getFullAddress.Street ?? "").Trim();
                           addDetails.StreetName = (getFullAddress.StreetName ?? "").Trim();
                           getFullAddress.StreetType = (getFullAddress.StreetType ?? "").Trim();
                           var streetdetails = streetrepo.FindStreetIdbyType(getFullAddress.StreetType).ToList();
                           if (streetdetails.Count != 0)
                           {
                               getFullAddress.StreetType = (streetdetails.FirstOrDefault().StreetCode).Trim();
                           }
                           addDetails.StreetType = (getFullAddress.StreetType ?? "").Trim();
                           addDetails.AddressNumber = (getFullAddress.AddressNumber ?? "").Trim();
                           addDetails.Quadrant = (getFullAddress.Quadrant ?? "").Trim();
                           addDetails.UnitType = (getFullAddress.UnitType ?? "").Trim();
                           addDetails.Unit = (getFullAddress.Unit ?? "").Trim();
                           addDetails.Country = (getFullAddress.Country ?? "").Trim();
                           addDetails.City = (getFullAddress.City ?? "").Trim();
                           addDetails.Zip = (getFullAddress.Zip ?? "").Trim();
                           addDetails.State = (getFullAddress.State ?? "").Trim();
                           addDetails.Telephone = (getFullAddress.Telephone ?? "").Trim();
                           addDetails.Type = (getFullAddress.CustomType ?? "").Trim();
                           addDetails.AddressNumberSufix = (getFullAddress.AddressNumberSufix ?? "").Trim();
                           addDetails.Xcoord = (getFullAddress.Xcoord ?? "").Trim();
                           addDetails.Ycoord = (getFullAddress.Ycoord ?? "").Trim();
                           addDetails.Anc = (getFullAddress.Anc ?? "").Trim();
                           addDetails.Ward = (getFullAddress.Ward ?? "").Trim();
                           addDetails.Cluster = (getFullAddress.Cluster ?? "").Trim();
                           addDetails.Latitude = (getFullAddress.Latitude ?? "").Trim();
                           addDetails.Longitude = (getFullAddress.Longitude ?? "").Trim();
                           addDetails.Vote_Prcnct = (getFullAddress.Vote_Prcnct ?? "").Trim();
                           addDetails.Zone = (getFullAddress.Zone ?? "").Trim();
                           addDetails.SMD = (getFullAddress.Smd ?? "").Trim();
                           addDetails.SSL = (getFullAddress.Ssl ?? "").Trim();
                           addDetails.IsValid = Convert.ToBoolean(getFullAddress.IsValid);


                           addDetails.OccupancyAddssValidate = (getSubDetails.OccupancyAddressValidate ?? "").Trim();
                       }
                       cofdetailslist.Add(addDetails);
                   }
                   return cofdetailslist;
               }
               catch (Exception)
               {

                   throw;
               }

           }
    }
}
