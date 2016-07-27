using BusinessCenter.Data.Common;
using BusinessCenter.Data.Interface;
using BusinessCenter.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessCenter.Data.Implementation
{
    public class CofoHopDetailsRepository : GenericRepository<MasterCofoHopDetails>, ICofoHopDetailsRepository
    {
        protected IStreetTypesRepository Streettyperepo;
        protected ISubmissionCofoHopeHopRepository SubmissionlsReposiotry;
        protected ISubmissionCofoHopeHopAddressRepository SubmissionBaReposiotry;
        protected ISubmissionMasterRepository SubmissionMasterRepository;
        public CofoHopDetailsRepository(IUnitOfWork context, IStreetTypesRepository streetTypesRepository,
            ISubmissionCofoHopeHopRepository submissionlsReposiotry,
            ISubmissionCofoHopeHopAddressRepository submissionCofoHopeHopAddressRepository,
            ISubmissionMasterRepository submissionMasterRepository
            )
            : base(context)
        {
            Streettyperepo = streetTypesRepository;
            SubmissionlsReposiotry = submissionlsReposiotry;
            SubmissionBaReposiotry = submissionCofoHopeHopAddressRepository;
            SubmissionMasterRepository = submissionMasterRepository;
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
                    var masterData = SubmissionMasterRepository.FindByMasterID(masterId).ToList();
                    if (masterData.Count() != 0)
                    {
                        var subMasterData = masterData.FirstOrDefault();
                        addDetails.TradeName = (subMasterData.TradeName ?? "").Trim();
                        addDetails.BusinessStructure = (subMasterData.BusinessStructure ??"").Trim();
                    }
                    else
                    {
                        addDetails.TradeName = null;
                        addDetails.BusinessStructure = null;
                    }
                    addDetails.Number = (getSubDetails.Number ?? "").Trim();
                    addDetails.MasterId = masterId;
                    addDetails.OccupancyAddssValidate = (getSubDetails.OccupancyAddressValidate??"").Trim();
                    addDetails.DonothaveCof = getSubDetails.DoNotHaveCofo ?? false;
                    var getFullAddress = SubmissionBaReposiotry.GetPrimisessAddress(getSubDetails.SubCofoHopEhopId,
                        getSubDetails.UserSelectType).FirstOrDefault();
                    if (getFullAddress != null)
                    {
                        addDetails.DateofIssue = Convert.ToDateTime(getSubDetails.DateOfIssuance).ToString("MM/dd/yyyy");
                        addDetails.Street = (getFullAddress.Street ?? "").Trim();
                        addDetails.StreetName = (getFullAddress.StreetName ?? "").Trim();
                        getFullAddress.StreetType = (getFullAddress.StreetType ?? "").Trim();
                        var streetdetails = Streettyperepo.FindStreetIdbyType(getFullAddress.StreetType).ToList();
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
        /// <summary>
        /// This method is used to retrive enitre Street type values from StretType Table.
        /// </summary>
        /// <returns>List of Street Types</returns>
        public List<StreetDetails> DropDownBind()
        {
            try
            {
                var streets = new List<StreetDetails>();
                var streetTypes = Streettyperepo.AllStreetTypes().Select(i => new { i.StreetType,i.StreetCode });
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
    }
}
