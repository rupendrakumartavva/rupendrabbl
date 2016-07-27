using BusinessCenter.Service.Interface;
using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
//using BusinessCenter.Api.App_Start;
using BusinessCenter.Api.Filters;
using BusinessCenter.Data.Model;

namespace BusinessCenter.Api.Controllers
{
    [RoutePrefix("api/BBLCategory")]
    [UpdateTokenLifeTime]
    public class BblCategoryController : ApiController
    {
        private readonly IMasterBusinessActivityService _bblActivities;
        private readonly IMasterPrimaryCategoryService _bblPrimaryCategories;
       // private readonly IMasterSecondaryLicenseCategoryService _bblSecondaryCategories;
         private readonly ISubmissionMasterService _submissionMaster;
         private readonly IMasterCategoryPhysicalLocationService _categoryphyloc;
         private readonly ISubmissionCategoryService _submissionCategories;
         private readonly IMasterSubCategoryService _superSubCategory;
         // IMasterSecondaryLicenseCategoryService bblSecondaryCategories,
         public BblCategoryController(IMasterBusinessActivityService bblActivities,
            IMasterPrimaryCategoryService bblPrimaryCategories,
           
            ISubmissionMasterService submissionMaster,
              IMasterCategoryPhysicalLocationService categoryphyloc,
             ISubmissionCategoryService submissionCategories,
             IMasterSubCategoryService superSubCategory
            )
        {

            _bblActivities = bblActivities;
            _bblPrimaryCategories = bblPrimaryCategories;
           // _bblSecondaryCategories = bblSecondaryCategories;
            _submissionMaster=submissionMaster;
            _categoryphyloc = categoryphyloc;
            _submissionCategories = submissionCategories;
            _superSubCategory=superSubCategory;
        }
        #region Business Activity
        /// <summary>
        /// To Display a List of Primary Business Activities
        /// https://dcbc.firebaseapp.com/#/preappquestions
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("BusinessActivities")]
        [DeflateCompression]
        public async Task<IHttpActionResult> GetBusinessActivities()
        {
            try
            {
                var allBusinessActivities = _bblActivities.GetBusinessActivity();
                if (allBusinessActivities == null)
                {
                    return NotFound();
                }
                return await Task.FromResult(Json(allBusinessActivities));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Get Business Activities", ex); 

            }
        }
        #endregion
        #region Primary categories
        /// <summary>
        /// To Display a List of Primary Categories based on Business Activity Id
        /// </summary>
        /// <param name="submissionApplication"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("PrimaryCategoryList")]
        [DeflateCompression]
        public async Task<IHttpActionResult> FindPrimaryCategoryById(SubmissionApplication submissionApplication)
        {
            try
            {
                var primarycategory = _bblPrimaryCategories.ActiveFindById(submissionApplication);
                if (primarycategory == null)
                {
                    return NotFound();
                }
                return await Task.FromResult(Json(primarycategory));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Primary Category By Id", ex); 
            }
        }
        #endregion
        #region Secondary Categories
        /// <summary>
        /// To Display a List of Secondary Category based on primaryId
        /// </summary>
        /// <param name="submissionApplication"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("SecondaryCategoryList")]
        [DeflateCompression]
        public async Task<IHttpActionResult> FindSecondaryCategoryById(SubmissionApplication submissionApplication)
        {
            try
            {
                var secondarycategory = _bblPrimaryCategories.FindSecondaryCategoryById(submissionApplication);
                if (secondarycategory == null)
                {
                    return NotFound();
                }
                return await Task.FromResult(Json(secondarycategory));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Secondary Category By Id", ex); 

            }
        }
        #endregion
        #region General Business sub Categories
        /// <summary>
        /// To Display a List of Secondary Category based on primaryId
        /// </summary>
        /// <param name="submissionApplication"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getsub")]
        [DeflateCompression]
        public async Task<IHttpActionResult> GetSuperSubcategory(SubmissionApplication submissionApplication)
        {
            try
            {
                var allSuperSubCategor = _superSubCategory.GetSuperSubCategory(submissionApplication);
                if (allSuperSubCategor == null)
                {
                    return NotFound();
                }
                return await Task.FromResult( Json(allSuperSubCategor));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Super Sub category", ex); 
            }
        }
        #endregion

        /// <summary>
        /// 
        /// Service:  SubmissionMasterService, Service Method: InsertAssociateBblService()
        /// Repository: SubmissionMasterRepository, Repository Method: InsertAssociateBblService()
        /// </summary>
        /// <param name="submissionApp"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("ApplicationSubmission")]
        [DeflateCompression]
        public async Task<IHttpActionResult> InsertAssociateBblService(SubmissionApplication submissionApp)
        {
            var result = _submissionMaster.InsertAssociateBblService(submissionApp);
            return await Task.FromResult(Json(new { Result = result }));
        }
         /// <summary>
        /// To get a list of Screening questions and Business Structure options
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("ScreeningQuestions")]
        [DeflateCompression]
        public async Task<IHttpActionResult> GetScreeningQuestions(SubmissionApplication submissionApplication)
        {
            try
            {
                var screeningquestions = _categoryphyloc.GetAllScreeningQuestions(submissionApplication);
                if (screeningquestions == null)
                {
                    return NotFound();
                }
                return await Task.FromResult(Json(screeningquestions));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Get Screening questions", ex); 
            }
        }
        /// <summary>
        /// Find Submission Categories By UserId
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
                if (submissionCategories == null)
                {
                    return NotFound();
                }
                return await Task.FromResult(Json(submissionCategories));
            }
            catch (Exception ex)
            {

                throw new Exception("Exception occurs in Submission category By Id", ex); 
            }
        }
        ///// <summary>
        ///// Get All Submission Categories
        ///// </summary>
        ///// <returns></returns>
        //[Authorize]
        //[HttpGet]
        //[Route("SubmissionCategories")]
        //[DeflateCompression]
        //public async Task<IHttpActionResult> GetSubmissionCategories()
        //{
        //    try
        //    {
        //        var allSubmissionCategories = _submissionCategories.GetAllSubmissionCategories();
        //        if (allSubmissionCategories == null)
        //        {
        //            return NotFound();
        //        }
        //        return Json(allSubmissionCategories);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        /// <summary>
        /// Get All Submission Masters
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("SubmissionMaster")]
        [DeflateCompression]
        public async Task<IHttpActionResult> GetSubmissionMaster()
        {
            try
            {
                var allSubmissionMaster = _submissionMaster.GetAllSubmissionMaster();
                if (allSubmissionMaster == null)
                {
                    return NotFound();
                }
                return await Task.FromResult(Json(allSubmissionMaster));
            }
            catch (Exception ex)
            {

                throw new Exception("Exception occurs in Submission Master", ex); 
            }
        }

        /// <summary>
        /// To Display Application Fee, Category License Fee, Endorsement Fee, % Tech Fee
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("TotalFees")]
        [DeflateCompression]
        public async Task<IHttpActionResult> GetTotalFees(SubmissionApplication submissionApp)
        {
            try
            {
                var totalfees = _submissionCategories.GetTotalFees(submissionApp);

                if (totalfees == null)
                {
                    return NotFound();
                }
                return await Task.FromResult(Json(totalfees));
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Get Total Fee", ex); 
            }
        }
       
    }
}
