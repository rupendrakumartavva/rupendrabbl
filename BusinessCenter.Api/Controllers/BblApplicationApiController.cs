using BusinessCenter.Api.Filters;

//using BusinessCenter.Api.App_Start;
//using BusinessCenter.Api.Common;
using BusinessCenter.Api.Models;
using BusinessCenter.Api.Utility;
using BusinessCenter.Common;
using BusinessCenter.Data;
using BusinessCenter.Data.Model;
using BusinessCenter.Service.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace BusinessCenter.Api.Controllers
{
    [RoutePrefix("api/BBLApplication")]
    [UpdateTokenLifeTime]
    public class BblAssociateApiController : ApiController
    {
        private readonly IBBLAssociateService _bblAssociate;

        private readonly ISubmissionCategoryService _submissionCategories;
        private readonly ISubmissionMasterService _submissionMaster;
        private readonly ISubmissionQuestionService _submissionQuestions;

        //private readonly IMasterCategoryPhysicalLocationService _categoryphyloc;
        private readonly ISubmissionIndividualService _subIndividualService;

        private readonly ISubmissionTaxRevenueService _submissionTaxRevenuService;
        private readonly ISubmissionCofoHopeHopAddressService _businessService;

        //  private readonly IwebServiceData _webserviceLocation;
        private readonly ICofoHopDetailsService _cofoDetailsService;

        private readonly IMasterCountryService _masterCountryService;
        private readonly IMastereHopEligibilityService _mastereHopService;
        //  private readonly IMasterTaxRevenueService _masterTaxRevenueService;

        private readonly ISubmissionSelfCertificationService _submissionSelfCertificationService;

        private readonly IMasterStateService _masterStateService;
        private readonly IEtlAddressAndParcelService _etlAddressAndParcelService;
        private readonly IPortal_Content_ErrorsService _portalContentErrorsService;

        // //IMasterCategoryPhysicalLocationService categoryphyloc,
        // IwebServiceData webserviceLocation,
        public BblAssociateApiController(IBBLAssociateService bblAssociateService,

            ISubmissionCategoryService submissionCategories,
            ISubmissionMasterService submissionMaster,
            ISubmissionQuestionService submissionQuestions,

            ISubmissionIndividualService subIndividualService,
            ISubmissionTaxRevenueService submissionTaxRevenuService,
            ISubmissionCofoHopeHopAddressService businessService,

          ICofoHopDetailsService cofoDetailsService,
            IMastereHopEligibilityService mastereHopService,
             IMasterCountryService masterCountryService,
            ISubmissionSelfCertificationService submissionSelfCertificationService, IMasterStateService masterStateService,
            IEtlAddressAndParcelService etlAddressAndParcelService,
            IPortal_Content_ErrorsService portalContentErrorsService
            )
        {
            _bblAssociate = bblAssociateService;

            _submissionCategories = submissionCategories;
            _submissionMaster = submissionMaster;
            _submissionQuestions = submissionQuestions;
            //_categoryphyloc = categoryphyloc;
            _subIndividualService = subIndividualService;
            // _masterTaxRevenueService = taxRevenueService;
            _businessService = businessService;
            //_webserviceLocation = webserviceLocation;
            _submissionTaxRevenuService = submissionTaxRevenuService;
            _cofoDetailsService = cofoDetailsService;
            _mastereHopService = mastereHopService;
            _masterCountryService = masterCountryService;
            _submissionSelfCertificationService = submissionSelfCertificationService;
            _masterStateService = masterStateService;
            _etlAddressAndParcelService = etlAddressAndParcelService;
            _portalContentErrorsService = portalContentErrorsService;
        }

        /// <summary>
        /// To Get All the Submissions that are in Submission Table
        /// Service: SubmissionService, Service Method: GetAllSubmissions()
        /// Repository: SubmissionRepository, Repository Method: AllSubmissions()
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        //[HttpGet]
        //[Route("Submissions")]
        //[DeflateCompression]
        //public async Task<IHttpActionResult> GetSubmission()
        //{
        //    try
        //    {
        //        var allSubmissions = _submissions.GetAllSubmissions();
        //        if (allSubmissions == null)
        //        {
        //            return NotFound();
        //        }
        //        return Json(allSubmissions);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// To Find Submission based on ActivityId
        /// Service: SubmissionService, Service Method: FindBySubmissionId()
        /// Repository: SubmissionRepository, Repository Method: FindByID()
        /// </summary>
        //  /// <param name="userid"></param>
        /// <returns></returns>
        //[Authorize]
        //[HttpPost]
        //[Route("FindSubmission")]
        //[DeflateCompression]
        //public async Task<IHttpActionResult> FindSubmissionsById(SubmissionModel submissionModel)
        //{
        //    try
        //    {
        //        var submissions = _submissions.FindBySubmissionId(submissionModel);
        //        if (submissions == null)
        //        {
        //            return NotFound();
        //        }
        //        return Json(submissions);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Find Submission Categories By SubmissionCategoryId
        /// Service: SubmissionCategoryService, Service Method: FindBySubmissionCategoryId()
        /// Repository: SubmissionCategoryRepository, Repository Method: FindByID()
        /// </summary>
        /// <param name="submissionCategoryModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("FindSubmissionCategory")]
        [DeflateCompression]
        public async Task<IHttpActionResult> FindSubmissionCategoryById(SubmissionCategoryModel submissionCategoryModel)
        {
            try
            {
                var submissionCategories = _submissionCategories.FindBySubmissionCategoryId(submissionCategoryModel);
                //if (submissionCategories == null)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(submissionCategories));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Submission Category Id", ex);
            }
        }

        /// <summary>
        /// To Display Application Fee, Endorsement fees and all individual endorsements details
        /// Service: SubmissionCategoryService, Service Method: ServiceCheckList()
        /// Repository: SubmissionCategoryRepository, Repository Method: ServiceCheckList()
        /// </summary>
        /// <param name="servicechecklist"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("ServiceCheckList")]
        [DeflateCompression]
        public async Task<IHttpActionResult> ServiceCheckList(ServiceChecklist servicechecklist)
        {
            try
            {
                var totalfees = _submissionCategories.ServiceCheckList(servicechecklist);

                //if (totalfees == null)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(totalfees));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Service Check List", ex);
            }
        }

        /// <summary>
        /// Get All Submission Masters
        /// Service: SubmissionMasterService, Service Method: GetAllSubmissionMaster()
        /// Repository: SubmissionMasterRepository, Repository Method: AllSubmissionMaster()
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        //[HttpGet]
        //[Route("SubmissionMaster")]
        //[DeflateCompression]
        //public async Task<IHttpActionResult> GetSubmissionMaster()
        //{
        //    try
        //    {
        //        var allSubmissionMaster = _submissionMaster.GetAllSubmissionMaster();
        //        if (allSubmissionMaster == null)
        //        {
        //            return NotFound();
        //        }
        //        return Json(allSubmissionMaster);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Find SubmissionMaster by MasterId
        /// Service: SubmissionMasterService, Service Method: FindBySubmissionMasterId()
        /// Repository: SubmissionMasterRepository, Repository Method: FindByID()
        /// </summary>
        // /// <param name="userid"></param>
        /// <returns></returns>
        //[Authorize]
        //[HttpPost]
        //[Route("FindSubmissionMaster")]
        //[DeflateCompression]
        //public async Task<IHttpActionResult> FindSubmissionMasterById(SubmissionMasterModel submissionMasterModel)
        //{
        //    try
        //    {
        //        var submissionMaster = _submissionMaster.FindBySubmissionMasterId(submissionMasterModel);
        //        if (submissionMaster == null)
        //        {
        //            return NotFound();
        //        }
        //        return Json(submissionMaster);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Get all Submission Questions
        /// Service: SubmissionQuestionService, Service Method: GetAllSubmissionQuestions()
        /// Repository: SubmissionQuestionRepository, Repository Method: AllSubmissionQuestions()
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        //[HttpGet]
        //[Route("SubmissionQuestions")]
        //[DeflateCompression]
        //public async Task<IHttpActionResult> GetSubmissionQuestions()
        //{
        //    try
        //    {
        //        var allSubmissionQuestions = _submissionQuestions.GetAllSubmissionQuestions();
        //        if (allSubmissionQuestions == null)
        //        {
        //            return NotFound();
        //        }
        //        return Json(allSubmissionQuestions);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// To Find Submission Questions By Userid
        /// service: SubmissionQuestionService, Service Method: FindSubmissionQuestionId()
        /// Repository: SubmissionQuestionRepository, Repository Method: FindByID()
        /// </summary>
        /// <param name="submissionQuestionModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("FindSubmissionQuestion")]
        [DeflateCompression]
        public async Task<IHttpActionResult> FindSubmissionQuestionById(SubmissionQuestionModel submissionQuestionModel)
        {
            try
            {
                var submissionQuestions = _submissionQuestions.FindSubmissionQuestionId(submissionQuestionModel);
                //if (submissionQuestions == null)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(submissionQuestions));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Find Submission Question ", ex);
            }
        }

        /// <summary>
        /// To Find Submission Individual based on MasterId
        /// Service: SubmissionIndividualService, Service Method: GetSubmissionIndiuvalData()
        /// Repository: SubmissionIndividualRepository, Repository Method: GetSubmissionIndiuvalData()
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("GetSubIndividuals")]
        [DeflateCompression]
        public async Task<IHttpActionResult> SubmissionIndiuvalData(ChecklistModel masterId)
        {
            try
            {
                var subindividuals = _subIndividualService.GetSubmissionIndividualData(masterId);
                //if (subindividuals == null)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(subindividuals));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Submission Individual Data", ex);
            }
        }

        /// <summary>
        /// To Insert of Update a Submission Individual based on MasterId
        /// Service: SubmissionIndividualService, Service Method: InsertUpdateSubmissionIndividual
        /// Repository: SubmissionIndividualRepository, Repository Method: InsertUpdateSubmissionIndividual()
        /// </summary>
        /// <param name="individual"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("SubIndividual")]
        [DeflateCompression]
        public async Task<IHttpActionResult> SubmissionIndividualInsert(SubmissionIndividualEntity individual)
        {
            try
            {
                var subIndividualService = _subIndividualService.InsertUpdateSubmissionIndividual(individual);
                //if (_subIndividualService == null)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(subIndividualService));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Submission Individual Insert", ex);
            }
        }

        /// <summary>
        /// Returns true or false If that Submissiond Individual is there in Database or not based on MasterId
        /// Service: SubmissionIndividualService, Service Method: ValidateSubmission
        /// Repository: SubmissionIndividualRepository, Repository Method: ValidateSubmission
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ValidateIndiduval")]
        [DeflateCompression]
        [Authorize]
        public async Task<IHttpActionResult> ValidateInduvalLic(ChecklistModel masterId)
        {
            // bool result;
            try
            {
                var validateResult = _subIndividualService.ValidateSubmission(masterId.MasterId);
                return await Task.FromResult(Json(new { status = validateResult }));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Validate Indivual License", ex);
            }
        }

        /// <summary>
        /// To Validate FEIN Number, Check for record exist with given MasterId or not, If it not exist just insert record into SubmissionTaxRevenue table.
        /// If IsDoc is false, then Update ChecklistApp
        /// Service: SubmissionTaxRevenuService, Service Method: SubmissionTaxRevenuInsertUpdate()
        /// Repository: SubmissionTaxRevenuRepository, Repository Method: SubmissionTaxRevenuInsertUpdate()
        /// </summary>
        /// <param name="taxrevenue"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("TaxValidation")]
        [DeflateCompression]
        public async Task<IHttpActionResult> TaxValidation(SubmissionTaxRevenuEntity taxrevenue)
        {
            try
            {
                // var taxes = _masterTaxRevenueService.ValidateFEINNumber(taxrevenue);

                var taxes1 = _submissionTaxRevenuService.SubmissionTaxRevenuInsertUpdate(taxrevenue);
                ////}
                return await Task.FromResult(Json(new { status = taxes1 }));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Tax Validation", ex);
            }
        }

        /// <summary>
        /// To Validate FEIN Number, Check for record exist with given MasterId or not, If it not exist just insert record into SubmissionTaxRevenue table.
        /// If IsDoc is false, then Update ChecklistApp
        /// Service: SubmissionTaxRevenuService, Service Method: SubmissionTaxRevenuInsertUpdate
        /// Repository: SubmissionTaxRevenuRepository, Repository Method: SubmissionTaxRevenuInsertUpdate()
        /// </summary>
        /// <param name="submissionTaxRevenu"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("SubmissionTaxRevenu")]
        [DeflateCompression]
        public async Task<IHttpActionResult> SubmissionTaxRevenuInsert(SubmissionTaxRevenuEntity submissionTaxRevenu)
        {
            try
            {
                var taxes = _submissionTaxRevenuService.SubmissionTaxRevenuInsertUpdate(submissionTaxRevenu);
                return await Task.FromResult(Json(new { status = taxes }));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Submission Tax Revenu Insert", ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("SubmissionTaxRevenuDel")]
        [DeflateCompression]
        public async Task<IHttpActionResult> SubmissionTaxRevenuDeletion(SubmissionTaxRevenuEntity submissionTaxRevenu)
        {
            try
            {
                var taxes = _submissionTaxRevenuService.DeleteSubmissionTaxandRevenue(submissionTaxRevenu);
                return await Task.FromResult(Json(new { status = taxes }));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Submission Tax Revenue Deletion", ex);
            }
        }

        /// <summary>
        /// Find a record in SubmissionBusinessAddress table based on MasterId
        /// Service: SubmissionBusinessAddressService, Service Method: GetCorpBusinessData()
        /// Repository: SubmissionBusinessAddressRepository, Repository Method: GetCorpBusinessData()
        /// </summary>
        /// <param name="generalBusiness"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("BusinessDataList")]
        [DeflateCompression]
        public async Task<IHttpActionResult> GetBusinessData(GeneralBusiness generalBusiness)
        {
            try
            {
                var businessData = _businessService.GetCorpBusinessData(generalBusiness);
                //if (businessData == null)
                //{
                //    return NotFound();
                //}
                //else
                //{ }
                return await Task.FromResult(Json(businessData));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occurs in Get Business Data", ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("GetCorpDetails")]
        [DeflateCompression]
        public async Task<IHttpActionResult> GetBusinessData1(GeneralBusiness generalBusiness)
        {
            try
            {
                var corpDetails = _bblAssociate.GetCorpBusinessDetails(generalBusiness);
                //if (corpDetails == null)
                //{
                //    return NotFound();
                //}
                //else
                //{
                //}
                return await Task.FromResult(Json(corpDetails));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Find a record in SubmissionBusinessAddress table based on MasterId
        /// Service: SubmissionBusinessAddressService, Service Method: FindByID()
        /// Repository: SubmissionBusinessAddressRepository, Repository Method: FindByID()
        /// </summary>
        //   /// <param name="generalBusiness"></param>
        /// <returns></returns>
        //[Authorize]
        //[HttpPost]
        //[Route("BusinessData")]
        //[DeflateCompression]
        //public async Task<IHttpActionResult> BusinessDataById(GeneralBusiness generalBusiness)
        //{
        //    try
        //    {
        //        var businessdata = _businessService.FindByID(generalBusiness);
        //        if (businessdata == null)
        //        {
        //            return NotFound();
        //        }
        //        return Json(businessdata);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Based on TaxRevenueNumber find a SubmissionTaxRevenue table
        /// Service: SubmissionTaxRevenuService, Service Method: FindByTaxRevenueNumber()
        /// Repository: SubmissionTaxRevenuRepository, Repository Method: FindByTaxRevenueNumber()
        /// </summary>
        /// <param name="submissionTaxRevenu"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("TaxRevenueNumber")]
        [DeflateCompression]
        public async Task<IHttpActionResult> FindByTaxRevenueNumber(SubmissionTaxRevenuEntity submissionTaxRevenu)
        {
            try
            {
                var getTaxDetails = _bblAssociate.DisplayTaxAndRevenuWithPrimisessDetails(submissionTaxRevenu.MasterId);
                //

                var taxrevenuedata = _submissionTaxRevenuService.FindByTaxRevenueNumber(submissionTaxRevenu);
                //if (taxrevenue == null)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(new
                {
                    taxrevenue = taxrevenuedata,
                    primisessAddress = getTaxDetails.FullAddress,
                    tradeName = getTaxDetails.TradeName,
                    bOwnerName = getTaxDetails.BussinessOwnerFullName
                }));
                // return Json(taxrevenue);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Tax Revenue Number", ex);
            }
        }

        /// <summary>
        ///
        /// Service: CofoHopDetailsService, Service Method: DropDownsBind()
        /// Repository: CofoHopDetailsRepository, Repository Method: DropDownBind()
        /// </summary>
        /// <param name="serviceDropDownData"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("LocationVerifier")]
        [DeflateCompression]
        public async Task<IHttpActionResult> LocationVerification(ServiceDropDownData serviceDropDownData)
        {
            try
            {
                var cofoDetails = _cofoDetailsService.DropDownsBind();
                var locverify = _etlAddressAndParcelService.ListEtlAddressDetails(serviceDropDownData.STNAME);
                var data = new ServiceDropDownData();
                if (cofoDetails != null)
                {
                    data.Dropdownlist = _cofoDetailsService.DropDownsBind().ToList();
                }
                if (locverify != null)
                {
                    data.WebserviceList = locverify.ToList();
                }
                //if (data == null)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(data));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Location Verification", ex);
            }
        }

        /// <summary>
        ///
        /// Service:  CofoHopDetailsService, Service Method: FindByNumberandDateofIssue
        /// Repository: CofoHopDetailsRepository, Repository Method: FindByNumberandDateofIssue()
        /// </summary>
        /// <param name="cofoHopDetailsModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("CofoHopDetails")]
        [DeflateCompression]
        public async Task<IHttpActionResult> CofoHopDetails(CofoHopDetailsModel cofoHopDetailsModel)
        {
            try
            {
                var cofoDetails = _cofoDetailsService.FindByNumberandDateofIssue(cofoHopDetailsModel);
                //if (cofoDetails == null)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(cofoDetails));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in CofoHop Details", ex);
            }
        }

        /// <summary>
        ///
        /// Service: CofoHopDetailsService, Service Method: GetSubmissionCofoOrHopDetails()
        /// Repository: CofoHopDetailsRepository, Repository Method: GetSubmissionCofoOrHopDetails()
        /// </summary>
        /// <param name="master"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("SubCofoHopdetl")]
        [DeflateCompression]
        public async Task<IHttpActionResult> GetSubmissionCofoOrHopDetails(Master master)
        {
            try
            {
                var cofoDetails = _cofoDetailsService.GetSubmissionCofoOrHopDetails(master.MasterID);
                //  var locverify = _cofoDetailsService.GetSubmissionCofoOrHopDetails(masterId);
                //  var locverify = _cofoDetailsService.DropDownsBind();
                //    var locverify = _webserviceLocation.Data(serviceDropDownData.STNAME);
                var data = new ServiceData();
                //if (cofoDetails != null)
                //{
                data.Dropdownlist = _cofoDetailsService.DropDownsBind().ToList();
                //}
                if (cofoDetails != null)
                {
                    data.WebserviceList = cofoDetails;
                }
                //if (data == null)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(data));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Submission CofoOrHop Details", ex);
            }
            //try
            //{
            //    var cofoDetails = _cofoDetailsService.GetSubmissionCofoOrHopDetails(masterId);
            //    if (cofoDetails == null)
            //    {
            //        return NotFound();
            //    }
            //    return Json(cofoDetails);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        /// <summary>
        ///
        /// Service: BBLAssociateService, Service Method: InsertPhysicallocation()
        /// Repository: SubmissionLocationandStrucRepository, Repository Method: InsertSubmissionLocation()
        /// </summary>
        /// <param name="cofoHopDetailsModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("SubmitCofoHop")]
        [DeflateCompression]
        public async Task<IHttpActionResult> InsertCofoHop(CofoHopDetailsModel cofoHopDetailsModel)
        {
            try
            {
                var cofoDetails = _bblAssociate.InsertPhysicallocation(cofoHopDetailsModel);
                //if (cofoDetails == false)
                //{
                //    return NotFound();
                //}
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                cofoHopDetailsModel.IsDataChange = cofoHopDetailsModel.IsDataChange == null ? false : cofoHopDetailsModel.IsDataChange;
                if (cofoHopDetailsModel.IsDataChange)
                {
                    _submissionTaxRevenuService.TaxAndRevenueUpdate(cofoHopDetailsModel.MasterId);
                }
                //if (cofoHopDetailsModel.IsSelfCertificationChange)
                //{
                //    _submissionSelfCertificationService.SelfCertificationUpdate(cofoHopDetailsModel.MasterId);
                //}

                return await Task.FromResult(Json(cofoDetails));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Insert Cofo Hop", ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("DeleteEhopAddress")]
        [DeflateCompression]
        public async Task<IHttpActionResult> DeleteHopPrimsesAddrss(CofoHopDetailsModel cofoHopDetailsModel)
        {
            try
            {
                var cofoDetails = _bblAssociate.DeleteHopPrimsesAddrss(cofoHopDetailsModel);
                //if (cofoDetails == false)
                //{
                //    return NotFound();
                //}
                if (cofoHopDetailsModel.IsDataChange)
                {
                    _submissionTaxRevenuService.TaxAndRevenueUpdate(cofoHopDetailsModel.MasterId);
                }
                return await Task.FromResult(Json(cofoDetails));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Delete Ho Primesess Address", ex);
            }
        }

        //DeleteHopPrimsesAddrss
        /// <summary>
        ///
        /// Service: CofoHopDetailsService, Service Method: UpdateCofoHopDetails()
        /// Repository: CofoHopDetailsRepository, Repository Method: UpdateCofoHopDetails()
        /// </summary>
        //  /// <param name="cofoHopDetailsModel"></param>
        /// <returns></returns>
        //[Authorize]
        //[HttpPost]
        //[Route("DetailCofoHop")]
        //[DeflateCompression]
        //public async Task<IHttpActionResult> UpdateCofoHop(CofoHopDetailsModel cofoHopDetailsModel)
        //{
        //    try
        //    {
        //        var updatecofohop = _cofoDetailsService.UpdateCofoHopDetails(cofoHopDetailsModel);
        //        if (updatecofohop == false)
        //        {
        //            return NotFound();
        //        }
        //        return Json(updatecofohop);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        ///
        /// Service: MastereHopEligibilityService, Service Method: GeMastereHop()
        /// Repository: MastereHopEligibilityRepository, Repository Method: GeMastereHopEligibility()
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("GetMasterEhop")]
        //    [ValidateAntiForgeryToken]
        [DeflateCompression]
        public async Task<IHttpActionResult> MastereHop(EhopModel ehopModel)
        {
            var mastereEhopData = _mastereHopService.GeMastereHop(ehopModel);
            //return   Json(securityQuestions);
            //if (mastereEhopData == null)
            //{
            //    return NotFound();
            //}
            return await Task.FromResult(Ok(mastereEhopData));
        }

        [HttpPost]
        [Authorize]
        [Route("GetEHopWithMasterId")]
        //  [ValidateAntiForgeryToken]
        [DeflateCompression]
        public async Task<IHttpActionResult> GetEHopWithSubmission(EhopModel ehopModel)
        {
            var mastereEhopData = _mastereHopService.GeMastereHop(ehopModel);
            //return   Json(securityQuestions);
            //if (mastereEhopData == null)
            //{
            //    return NotFound();
            //}
            return await Task.FromResult(Ok(mastereEhopData));
        }

        /// <summary>
        ///
        /// Service: , Service Method: ()
        /// Repository: , Repository Method: ()
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("EhopChecklist")]
        //   [ValidateAntiForgeryToken]
        [DeflateCompression]
        public async Task<IHttpActionResult> EhopChecklist(EhopModel ehopModel)
        {
            var data = _bblAssociate.MasterHopEligibility(ehopModel);
            //return   Json(securityQuestions);
            //if (data == null)
            //{
            //    return NotFound();
            //}
            return await Task.FromResult(Ok(data));
        }

        /// <summary>
        ///
        /// Service: BBLAssociateService, Service Method: InsertCorporationDetails()
        /// Repository: SubmissionCorporationRepository, Repository Method: InsertCorporationDetails()
        /// </summary>
        /// <param name="detailsModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("SubmitCorpAgent")]
        [DeflateCompression]
        public async Task<IHttpActionResult> SubmitCorpAgent(GeneralBusiness detailsModel)
        {
            var result = _bblAssociate.InsertCorporationDetails(detailsModel);

            if (result == null)
            {
                return Json("No Data");
            }
            detailsModel.IsDataChange = detailsModel.IsDataChange == null ? false : detailsModel.IsDataChange;
            if (detailsModel.IsDataChange)
            {
                _submissionTaxRevenuService.TaxAndRevenueUpdate(detailsModel.MasterId);
                _submissionSelfCertificationService.SelfCertificationUpdate(detailsModel.MasterId);
            }
            return await Task.FromResult(Json(result));
        }

        /// <summary>
        /// Find Submission Categories By UserId
        /// Service: BBLAssociateService, Service Method: GetHeadQuarterAddress()
        /// Repository: SubmissionCorporationRepository, Repository Method: GetHQAddess()
        /// </summary>
        /// <param name="subCorpAgentModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("HeadQuarterAddress")]
        [DeflateCompression]
        public async Task<IHttpActionResult> HeadQuarterAddress(GeneralBusiness subCorpAgentModel)
        {
            try
            {
                var submissionCategories = _bblAssociate.GetHeadQuarterAddress(subCorpAgentModel);
                //if (submissionCategories == null)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(submissionCategories));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Head Quarter Address", ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("EmptyHeadQuarterAddress")]
        [DeflateCompression]
        public async Task<IHttpActionResult> HeadQuarterAddressEmpty(GeneralBusiness subCorpAgentModel)
        {
            try
            {
                var submissionCategories = _bblAssociate.DeleteSubmissionCorpEmpty(subCorpAgentModel.MasterId, subCorpAgentModel.UserType);
                //if (submissionCategories == null)
                //{
                //    return NotFound();
                //}
                subCorpAgentModel.IsDataChange = subCorpAgentModel.IsDataChange == null ? false : subCorpAgentModel.IsDataChange;
                if (subCorpAgentModel.IsDataChange)
                {
                    _submissionTaxRevenuService.TaxAndRevenueUpdate(subCorpAgentModel.MasterId);
                    _submissionSelfCertificationService.SelfCertificationUpdate(subCorpAgentModel.MasterId);
                }
                return await Task.FromResult(Json(submissionCategories));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Head Quater Address Empty", ex);
            }
        }

        //DeleteSubmissionCorpEmpty(string masterId, string type)

        /// <summary>
        /// Find Submission Categories By UserId
        /// Service: BBLAssociateService, Service Method: GetPrimisessAddress()
        /// Repository: SubmissionLocationandStrucRepository, Repository Method: GetPrimisessAddress()
        /// </summary>
        /// <param name="subCorpAgentModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("PrimisessAddress")]
        [DeflateCompression]
        public async Task<IHttpActionResult> PrimisessAddress(GeneralBusiness subCorpAgentModel)
        {
            try
            {
                var submissionCategories = _bblAssociate.GetPrimisessAddress(subCorpAgentModel);
                //if (submissionCategories == null)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(submissionCategories));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Premisess Address", ex);
            }
        }

        /// <summary>
        ///
        /// Service: BBLAssociateService, Service Method: SubmissionDetails()
        /// Repository: SubmissionVerficationRepository, Repository Method: SubmissionDetails()
        /// </summary>
        /// <param name="subVerfication"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("VerficationDetails")]
        [DeflateCompression]
        public async Task<IHttpActionResult> VerficationDetails(SubmissionVerfication subVerfication)
        {
            try
            {
                var verficationdetails = _bblAssociate.SubmissionDetails(subVerfication);
                //if (verficationdetails == null)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(verficationdetails));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Service Check List", ex);
            }
        }

        /// <summary>
        ///
        /// Service: BBLAssociateService, Service Method: SubmissionDetails()
        /// Repository: SubmissionVerficationRepository, Repository Method: SubmissionDetails()
        /// </summary>
        /// <param name="subVerfication"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("VerficationPayDetails")]
        [DeflateCompression]
        public async Task<IHttpActionResult> VerficationPayDetails(SubmissionVerfication subVerfication)
        {
            try
            {
                var verficationdetails = _bblAssociate.SubmissionPayDetails(subVerfication);
                //if (verficationdetails == null)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(verficationdetails));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Verfication Pay Details", ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("ValidateEhop")]
        [DeflateCompression]
        public async Task<IHttpActionResult> VerficationDetails(EhopModel ehopModel)
        {
            try
            {
                var verficationdetails = _mastereHopService.ValidateEhopEligibility(ehopModel);
                //if (verficationdetails == null)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(verficationdetails));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Verfication Details", ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("GetCorpAgent")]
        [DeflateCompression]
        public async Task<IHttpActionResult> GetCorpAgent(GeneralBusiness generalBusiness)
        {
            try
            {
                var verficationdetails = _bblAssociate.GetCorpAgent(generalBusiness);
                //if (verficationdetails == null)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(verficationdetails));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Corporation Agent", ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("SubmitHop")]
        [DeflateCompression]
        public async Task<IHttpActionResult> SubmitHop(CofoHopDetailsModel cofoModel)
        {
            try
            {
                var verficationdetails = _submissionMaster.UpdateEhop(cofoModel);
                //if (verficationdetails == null)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(verficationdetails));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Submit Hop", ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("ValidateLicenceNum")]
        [DeflateCompression]
        public async Task<IHttpActionResult> ValidateLicenceNum(SubmissionIndividualEntity individual)
        {
            try
            {
                var verficationdetails = _submissionMaster.ValidateBblLicence(individual.CompanyBusinessLicense);
                //if (verficationdetails == null)
                //{
                //    return NotFound();
                //}

                return await Task.FromResult(Json(new { Status = verficationdetails }));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Validate License Number", ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("UpdateEhopNon")]
        [DeflateCompression]
        public async Task<IHttpActionResult> UpdateEhopNonSelect(EhopModel ehopModel)
        {
            try
            {
                var verficationdetails = _submissionMaster.UpdateEhopNonSelect(ehopModel.MasterId);
                if (!verficationdetails)
                   
                {
                    // ReSharper disable once HeuristicUnreachableCode
                    return await Task.FromResult(NotFound());
                }
                return await Task.FromResult(Json(true));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Update Ehop Non Select", ex);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("binddropdown")]
        [DeflateCompression]
        public async Task<IHttpActionResult> BindDropDown()
        {
            try
            {
                AddressType addressType = new AddressType
                {
                    StreetList = _cofoDetailsService.DropDownsBind().ToList(),
                    CountryList = _masterCountryService.GetCountryList().ToList()
                };
                //if (addressType == null)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(addressType));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Bind Drop Down", ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("subinddelete")]
        [DeflateCompression]
        public async Task<IHttpActionResult> BindDropDown(ChecklistModel masterId)
        {
            try
            {
                var individualDelete = _subIndividualService.SubmissionIndividualDelete(masterId.MasterId);
                return await Task.FromResult(Json(individualDelete));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Bind Drop Down", ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("GetSelftCertification")]
        [DeflateCompression]
        public async Task<IHttpActionResult> SubmissionSelftCertification(SubmissionSelfCertification selftCertification)
        {
            try
            {
                var submissionSelfCertification = _submissionSelfCertificationService.GetSelfCertificationOnMasterId(selftCertification);
                //if (submissionSelfCertification == null)
                //{
                //    return NotFound();
                //}

                return await Task.FromResult(Json(submissionSelfCertification));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Submission self Certification", ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("SelftCertificationInsert")]
        [DeflateCompression]
        public async Task<IHttpActionResult> SubmissionSelftCertificationInsertUpdate(SubmissionSelfCertification selftCertification)
        {
            try
            {
                var result = _submissionSelfCertificationService.InsertUpdateSelfCertification(selftCertification);
                return await Task.FromResult(Json(new { Status = result }));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Submission self certification Insert Update", ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("SelftCertificationDel")]
        [DeflateCompression]
        public async Task<IHttpActionResult> SubmissionSelftCertificationDelete(SubmissionSelfCertification selftCertification)
        {
            try
            {
                var result = _submissionSelfCertificationService.DeleteSelfCertification(selftCertification);
                return await Task.FromResult(Json(new { Status = result }));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Submission Self Certification", ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("StateListBasedOnCode")]
        [DeflateCompression]
        public async Task<IHttpActionResult> GetStateDetails(ModelFactory.MasterStateModel masterStateModel)
        {
            try
            {
                var result = _masterStateService.StatesFindByCountry(masterStateModel.CountryCode);
                return await Task.FromResult(Json(new { Status = result }));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in State Details", ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("GetEhopDetails")]
        [DeflateCompression]
        public async Task<IHttpActionResult> EhopData(EhopData ehopData)
        {
            try
            {
                var ehopDetails = _bblAssociate.EhopData(ehopData);
                var submissionmaster = _submissionMaster.FindByMasterID(ehopDetails.MasterID).ToList();
                if (submissionmaster.Any())
                {
                    var masterData = submissionmaster.FirstOrDefault();
                    if (masterData != null) ehopDetails.Name = (masterData.BusinessOwnerName ?? "").Trim();
                }

                //if (ehopDetails == null)
                //{
                //    return NotFound();
                //}

                return await Task.FromResult(Json(ehopDetails));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Ehop Data", ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("UpdateCorpStatus")]
        [DeflateCompression]
        public async Task<IHttpActionResult> UpdateSubmissionCorpStatus(GeneralBusiness genraBusiness)
        {
            try
            {
                 _bblAssociate.UpdateCorpDisplayStatus(genraBusiness);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Update Submission Corporation Status", ex);
            }
            return await Task.FromResult( Json("True"));
        }

        [Authorize]
        [HttpPost]
        [Route("CorpOnlineSearch")]
        [DeflateCompression]
        public async Task<IHttpActionResult> CorpOnlineSearch(CorporationDetails corporationdetails)
        {
            try
            {
                var corpDetails = _bblAssociate.CorpOnlineSearch(corporationdetails);
                //if (corpDetails == null)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(new { Status = corpDetails }));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Corporation Details", ex);
            }
        }

        //_masterStateService
        [Authorize]
        [HttpPost]
        [Route("FindByMessageName")]
        [DeflateCompression]
        public async Task<IHttpActionResult> FindByMessageName(PortaContentErrorsModel portalContentErrors)
        {
            try
            {
                var portalcontentErrors = _portalContentErrorsService.FindByMessageName(portalContentErrors).ToList();
                //if (portalcontentErrors.Count() == 0)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(portalcontentErrors));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Message Name", ex);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllMessages")]
        [DeflateCompression]
        public async Task<IHttpActionResult> GetAllMessages()
        {
            try
            {
                var portalcontentErrors = _portalContentErrorsService.GetAllContentErrors().Select(msg => new { msg.ShortName, msg.ErrrorMessage }).ToList();
                //if (portalcontentErrors.Count() == 0)
                //{
                //    return NotFound();
                //}
                return await Task.FromResult(Json(portalcontentErrors));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occurs in All Message", ex);
            }
        }
    }
}