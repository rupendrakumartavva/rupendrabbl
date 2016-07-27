using BusinessCenter.Admin.Common;
using BusinessCenter.Admin.Filters;
using BusinessCenter.Admin.Interface;
using BusinessCenter.Admin.Models;
using BusinessCenter.Common;
using BusinessCenter.Data;
using BusinessCenter.Data.Model;
using BusinessCenter.Email;
using BusinessCenter.Identity.Interfaces;
using BusinessCenter.Service.Interface;
using iTextSharp.text.pdf;
using Omu.ValueInjecter;
//using RazorEngine.Templating;
using System;
//using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web;
using System.Web.Mvc;
//using System.Web.Services.Description;

namespace BusinessCenter.Admin.Controllers
{
    public class BBLController : Controller
    {
        private readonly IMasterBusinessActivityService _bblBusinessActivity;
        private readonly IMasterPrimaryCategoryService _bblprimarycategories;
        private readonly IMasterSecondaryLicenseCategoryService _bblsecondarycategories;
        private readonly IMasterCategoryPhysicalLocationService _categoryphysical;

        // private readonly IBBLAdminService _bbladminservice;
        private readonly ISubmissionMasterService _bblsubmissionmaster;

        private readonly IBBLAssociateService _bblAssociateService;
        private readonly ISubmissionCategoryService _submissionCategoryService;
        private readonly IMasterSubCategoryService _subCategoryService;
        private readonly ISubmissionIndividualService _subIndividualService;
        private readonly IUserManager _userManager;
        private readonly IGridBindings _gridBindings;
        private readonly IEmailTemplate _emailTemplate;
        private readonly IFeeCodeMapService _feeCodeMapService;
        private readonly IOSubCategoryFeeService _oSubCategoryFeeService;
        private readonly IBusinessInformationService _businessInformationService;
        private readonly ISubmissionToAccelaService _submissiontoAccelaService;
        private readonly IDocumentsView4Service _documentsView4Service;
        private readonly IRenewalView3Service _renewalView3Service;
        private readonly ISubmissionTaxRevenueService _submissiontaxrevenueservice;
        private readonly ISubmissionSelfCertificationService _submissionselfcertification;
        private readonly ISubmissionIndividualService _submissionindividualservice;
        private readonly IPortal_Content_ErrorsService _portalContentErrorsService;
        private readonly IBusinessCenterCommon _businesscentercommon;
        private readonly IMailTemplateService _mailTemplateService;

        public BBLController(
            IMasterBusinessActivityService bblBusinessActivity,
            IMasterPrimaryCategoryService bblprimarycategories,
            IMasterSecondaryLicenseCategoryService bblsecondarycategories,
            IMasterCategoryPhysicalLocationService categoryphysical,
            //   IBBLAdminService bbladminservice
            ISubmissionMasterService bblsubmissionmaster,
            IBBLAssociateService bblAssociateService,
              ISubmissionCategoryService submissionCategoryService,
            IMasterSubCategoryService subCategoryService,
            ISubmissionIndividualService subIndividualService,
            IUserManager userManager,
             IGridBindings gridBindings,
            IEmailTemplate emailTemplate,
            IFeeCodeMapService feeCodeMapService,
            IOSubCategoryFeeService oSubCategoryFeeService, IBusinessInformationService businessInformationService,
            ISubmissionToAccelaService submissiontoAccelaService, IDocumentsView4Service documentsView4Service, IRenewalView3Service renewalView3Service,
 ISubmissionTaxRevenueService submissiontaxrevenueservice, ISubmissionSelfCertificationService submissionselfcertification, ISubmissionIndividualService submissionindividualservice,
            IPortal_Content_ErrorsService portalContentErrorsService, IBusinessCenterCommon businesscentercommon,
            IMailTemplateService mailTemplateService)
        {
            _bblBusinessActivity = bblBusinessActivity;
            _bblprimarycategories = bblprimarycategories;
            _bblsecondarycategories = bblsecondarycategories;
            _categoryphysical = categoryphysical;
            //    _bbladminservice = bbladminservice;
            _bblsubmissionmaster = bblsubmissionmaster;
            _bblAssociateService = bblAssociateService;
            _submissionCategoryService = submissionCategoryService;
            _subCategoryService = subCategoryService;
            _subIndividualService = subIndividualService;
            _userManager = userManager;
            _gridBindings = gridBindings;
            _emailTemplate = emailTemplate;
            _feeCodeMapService = feeCodeMapService;
            _oSubCategoryFeeService = oSubCategoryFeeService;
            _businessInformationService = businessInformationService;
            _submissiontoAccelaService = submissiontoAccelaService;
            _documentsView4Service = documentsView4Service;
            _renewalView3Service = renewalView3Service;
            _submissiontaxrevenueservice = submissiontaxrevenueservice;
            _submissionselfcertification = submissionselfcertification;
            _submissionindividualservice = submissionindividualservice;
            _portalContentErrorsService = portalContentErrorsService;
            _businesscentercommon = businesscentercommon;
            _mailTemplateService = mailTemplateService;
        }

        #region Assign License
        /// <summary>
        /// This Method is used to get Licenses Numbers based on masterId
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="transferToUserName"></param>
        /// <param name="reasonForTransfer"></param>
        /// <returns></returns>
        [ConcurrentLogin]
        public async Task<ActionResult> TransferLicense(string masterId, string transferToUserName, string reasonForTransfer)
        {
            try
            {
                var submissionMasterModel = new SubmissionMasterModel {MasterId = masterId};
                var getmasterDetails = _bblsubmissionmaster.FindBySubmissionMasterId(submissionMasterModel).ToList();

                var userName = TempData["UserName"].ToString();
                UserAccountById user1 = new UserAccountById();
                var userid = await _userManager.FindByNameAsync(userName);
                user1.UserId = userid.Id;
                var userIDs = await _userManager.FindUserDetailsByIdAsync(user1.UserId);
                RegisterUserModel superAdminUser = new RegisterUserModel
                {
                    FirstName = userIDs.FirstName,
                    LastName = userIDs.LastName,
                    UserName = userIDs.UserName,
                    Email = userIDs.Email,
                    Address = userIDs.Address,
                    City = userIDs.City,
                    MobileNumber = userIDs.MobileNumber,
                    PostalCode = userIDs.PostalCode,
                    State = userIDs.State,
                    SecurityQuestion1 = userIDs.SecurityQuestion1,
                    SecurityQuestion2 = userIDs.SecurityQuestion2,
                    SecurityQuestion3 = userIDs.SecurityQuestion3,
                    SecurityAnswer1 = userIDs.SecurityAnswer1,
                    SecurityAnswer2 = userIDs.SecurityAnswer2,
                    SecurityAnswer3 = userIDs.SecurityAnswer3,
                    IsDelete = userIDs.IsDelete
                };

                ViewBag.Status = "";
                ViewBag.UserName = userName;
                @ViewBag.UserId = userid.Id;

                //UserTypeModel model = new UserTypeModel();
                //model.UserStatus = "Active";
                //model.searchText = "";
                //var users = await _gridBindings.UserTypeBasedList(model);
                //var filteredUsers = users.Where(x => x.UserName.Replace(System.Environment.NewLine, "").Trim() != userName.Trim()).OrderBy(x => x.UserName);
                //ViewBag.Users = filteredUsers;
                SubmissionTransferViewModel viewModel = new SubmissionTransferViewModel {User = superAdminUser};
                //viewModel.UsersList = users;
                SubmissiontransferModel submissiontransfer = new SubmissiontransferModel {FromUserId = userid.Id};

                viewModel.submissiontransfer = submissiontransfer;
                TempData["UserName"] = userName;
                string license = "";
                if (getmasterDetails.Any())
                {
                    var masterDetails = getmasterDetails.FirstOrDefault();
                    if (masterDetails != null) license = masterDetails.SubmissionLicense;
                    submissiontransfer.MasterId = masterId;
                }
                else
                {
                    BblAsscoiateService bassociateservice = new BblAsscoiateService
                    {
                        UserID = userid.Id,
                        DCBC_ENTITY_ID = masterId
                    };
                    var userdata = _bblAssociateService.GetTransferdata(bassociateservice).ToList();
                    if (userdata.Any())
                    {
                        var userdetailsdata = userdata.FirstOrDefault();
                        if (userdetailsdata != null) license = userdetailsdata.SubmissionLicense ?? "";
                    }
                    submissiontransfer.MasterId = masterId;
                }
                TempData["SubmissionLicense"] = license;
                viewModel.UserId = submissiontransfer.FromUserId.Trim();
                viewModel.SubmissionLicense = license;
                viewModel.LoggedUserName = Session["UserName"].ToString();

                if (!string.IsNullOrEmpty(transferToUserName) && !string.IsNullOrEmpty(reasonForTransfer))
                {
                    var touserid = await _userManager.FindByNameAsync(userName);
                    viewModel.submissiontransfer.ToUserId = touserid.Id;
                    viewModel.submissiontransfer.ReasonForTransfer = reasonForTransfer;
                    viewModel.submissiontransfer.TransferToUserName = transferToUserName;
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occurs in Transfer License",ex);
            }
        }

        /// <summary>
        /// This Method is used to Transfer the License From one user to another user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ConcurrentLogin]
        public async Task<ActionResult> TransferLicense(SubmissionTransferViewModel viewModel)
        {
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                    string userName = TempData["UserName"].ToString();
                    ViewBag.UserName = userName;
                    if (string.IsNullOrEmpty(viewModel.submissiontransfer.TransferToUserName) ||
                        string.IsNullOrEmpty(viewModel.submissiontransfer.ReasonForTransfer))
                    {
                        ViewBag.requiredmsg = "Please complete all required fields";
                        TempData["UserName"] = userName;
                        return View(viewModel);
                    }
                    if (viewModel.submissiontransfer.TransferToUserName.ToUpper().Trim() == userName.ToUpper().Trim())
                    {
                        ViewBag.invaliduser = "‘Transfer From’ and ‘Transfer To’ Usernames cannot be same.";
                        TempData["UserName"] = userName;
                        return View(viewModel);
                    }
                    var validate = await _userManager.FindByNameAsync(viewModel.submissiontransfer.TransferToUserName.Trim());
                    if (validate != null)
                    {
                        viewModel.ToUserId = validate.Id;
                    }
                    else
                    {
                        ViewBag.invaliduser = "‘Transfer To’ Username doesn’t exist.";
                        TempData["UserName"] = userName;
                        return View(viewModel);
                    }
                    viewModel.FromUserId = viewModel.submissiontransfer.FromUserId;
                    viewModel.ReasonForTransfer = viewModel.submissiontransfer.ReasonForTransfer;
                    viewModel.MasterId = viewModel.submissiontransfer.MasterId;
                    return RedirectToAction("ConfirmTransfer", viewModel);
                }
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Transfer License", ex); 
            }
        }
       /// <summary>
       /// This method is used to display Confirmation window with License Details
       /// </summary>
       /// <param name="viewModel"></param>
       /// <returns></returns>
        [ConcurrentLogin]
        public async Task<ActionResult> ConfirmTransfer(SubmissionTransferViewModel viewModel)
        {
            try
            {
                ViewBag.UserId = viewModel.FromUserId;
                viewModel.LoggedUserName = Session["UserName"].ToString();

                var fromUser = await _userManager.FindUserDetailsByIdAsync(viewModel.FromUserId);
                if (fromUser != null)
                {
                    viewModel.TransferredFromUserName = fromUser.UserName;
                }

                var toUser = await _userManager.FindUserDetailsByIdAsync(viewModel.ToUserId);
                if (toUser != null)
                {
                    viewModel.TransferToUserName = toUser.UserName;
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in ConfirmTransfer", ex); 
            }
        }

        //[HttpPost]
        //public async Task<ActionResult> TransferLicense(SubmissionTransferViewModel submissionTransferViewModel)
        //{
        //    if (!string.IsNullOrEmpty(submissionTransferViewModel.submissiontransfer.ToUserId) ||
        //       !string.IsNullOrEmpty(submissionTransferViewModel.submissiontransfer.ReasonForTransfer))
        //    {
        //        ViewBag.requiredmsg = "Please complete all required fields";
        //        return View(submissionTransferViewModel);

        //    }
        //    return  (submissionTransferViewModel);
        //}

        //public async Task<JsonResult> GetUsersforTransfer(string term)
        //{
        //    string userName = string.Empty;
        //    userName = TempData["UserName"].ToString();
        //    UserTypeModel model = new UserTypeModel();

        //    model.UserStatus = "Active";

        //    var users = await _gridBindings.UserTypeBasedList(model);
        //    var filteredUsers = users.Where(x => x.UserName.Replace(System.Environment.NewLine, "").Trim() != userName.Trim());

        //    TempData["UserName"] = userName;
        //    List<string> usersList = filteredUsers.Where(x => x.UserName.StartsWith(term)).Select(y => y.UserName).ToList();
        //    return Json(usersList, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        [ActionName("ConfirmTransfer")]
        [ConcurrentLogin]
        public async Task<ActionResult> ConfirmTransferView(SubmissionTransferViewModel submissionTransferViewModel)
        {
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                    if (ModelState.IsValid)
                    {
                        string fromUserId = submissionTransferViewModel.FromUserId;
                        UserTypeModel model = new UserTypeModel();
                        model.UserStatus = "Active";
                        model.searchText = "";
                        var users = await _gridBindings.UserTypeBasedList(model);
                        var filteredUsers = users.Where(x => x.UserName.Replace(System.Environment.NewLine, "").Trim() != submissionTransferViewModel.FromUserId.Trim());
                        ViewBag.Users = filteredUsers;
                        var fromUser = await _userManager.FindUserDetailsByIdAsync(submissionTransferViewModel.FromUserId);
                        ViewBag.UserName = fromUser.UserName;
                        //if (!string.IsNullOrEmpty(submissionTransferViewModel.submissiontransfer.ToUserId) ||
                        //   !string.IsNullOrEmpty(submissionTransferViewModel.submissiontransfer.ReasonForTransfer))
                        //{
                        //    ViewBag.requiredmsg = "Please complete all required fields";
                        //    return View(submissionTransferViewModel);

                        //}

                        Submissiontransfer transfer = new Submissiontransfer
                        {
                            MasterId = submissionTransferViewModel.MasterId ?? "",
                            FromUserId = submissionTransferViewModel.FromUserId,
                            ToUserId = submissionTransferViewModel.ToUserId,
                            ReasonForTransfer = submissionTransferViewModel.ReasonForTransfer,
                            LicenseNumber = submissionTransferViewModel.SubmissionLicense ?? ""
                        };

                        string userName = Session["UserName"].ToString();
                        var userid = await _userManager.FindByNameAsync(userName);
                        transfer.CreatedBy = userid.Id;

                        //var fromUser = await _userManager.FindUserDetailsByIdAsync(submissionTransferViewModel.submissiontransfer.FromUserId);
                        var toUser = await _userManager.FindUserDetailsByIdAsync(submissionTransferViewModel.ToUserId);

                        //ViewBag.UserName = fromUser.UserName;
                        _bblsubmissionmaster.TransferSubmissions(transfer);
                        ViewBag.Statusmsg = "License transferred Successfully";
                        ViewBag.ToUserName = toUser.UserName;
                        string license = TempData["SubmissionLicense"].ToString();

                        var emailBody = new StringBuilder();
                        emailBody.Append("Dear " + fromUser.FirstName + " " + fromUser.LastName);
                        emailBody.Append("<br/>");
                        emailBody.Append("<br/>");
                        emailBody.Append("License/Application # ");
                        emailBody.Append("<strong>" + license + "</strong>");
                        emailBody.Append(" is transfered from ");
                        emailBody.Append('"' + fromUser.FirstName + " " + fromUser.LastName + '"');
                        emailBody.Append(" to ");
                        emailBody.Append('"' + toUser.FirstName + " " + toUser.LastName + '"');

                        var toUserEmailBody = new StringBuilder();
                        toUserEmailBody.Append("Dear " + toUser.FirstName + " " + toUser.LastName);
                        toUserEmailBody.Append("<br/>");
                        toUserEmailBody.Append("<br/>");
                        toUserEmailBody.Append("License/Application # ");
                        toUserEmailBody.Append("<strong>" + license + "</strong>");
                        toUserEmailBody.Append(" is transfered from ");
                        toUserEmailBody.Append('"' + fromUser.FirstName + " " + fromUser.LastName + '"');
                        toUserEmailBody.Append(" to ");
                        toUserEmailBody.Append('"' + toUser.FirstName + " " + toUser.LastName + '"');
                        var mailTemplateModel = new MailTemplateModel
                        {
                            UserId = fromUserId,
                            CustomApplicationId = fromUserId,
                            Type = "LICENSE TRANSFER",
                            Subject = "License Transfer",
                            MailSentFailCount = 0,
                            MailContent = emailBody.ToString().Trim(),
                            IsMailSent = true
                        };
                        _mailTemplateService.InsertUpdateMailTemplate(mailTemplateModel);
                        _emailTemplate.MailSending("License Transfer", emailBody.ToString(), fromUser.Email);
                        var mailTemplateToModel = new MailTemplateModel
                        {
                            UserId = transfer.ToUserId,
                            CustomApplicationId = transfer.ToUserId,
                            Type = "LICENSE TRANSFER",
                            Subject = "License Transfer",
                            MailSentFailCount = 0,
                            MailContent = toUserEmailBody.ToString().Trim(),
                            IsMailSent = true
                        };
                        _mailTemplateService.InsertUpdateMailTemplate(mailTemplateToModel);
                        _emailTemplate.MailSending("License Transfer", toUserEmailBody.ToString(), toUser.Email);
                        ViewBag.LicenseNumber = license;
                        ViewBag.IsLicenseTransfer = true;
                        ModelState.Clear();

                        //var transfermodel = new SubmissionTransferViewModel();
                        //transfermodel.UserId = fromUserId.Trim();
                        submissionTransferViewModel.UserId = fromUserId.Trim();
                        return View(submissionTransferViewModel);
                        //return RedirectToAction("UserSubmissions", new { userId = submissionTransferViewModel.submissiontransfer.ToUserId });
                    }
                    return View();
                }
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Confirm Transfer View", ex); 
            }
        }
        /// <summary>
        /// This method returns the transferAudit Page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult TransferAudit()
        {
            return View();
        }
        /// <summary>
        /// This method is used to show the transfering License History Details
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [ConcurrentLogin]
        public PartialViewResult TransferHistoryPartial()
        {
            var repodata = _bblAssociateService.GetTransferHistory().OrderByDescending(x => x.DateOfTransfer).ToList();
            var transferDetails = repodata.Select(item => new TransferModel
            {
                SubmissionLicense = _bblAssociateService.GetRenewalLicense(item.SubmissionLicense),
                FromUserId = _bblAssociateService.FindByAdminId(item.FromUserId).First().UserName,
                ToUserId = _bblAssociateService.FindByAdminId(item.ToUserId).First().UserName,
                DateOfTransfer = !string.IsNullOrEmpty(item.DateOfTransfer.ToString()) ? Convert.ToDateTime(item.DateOfTransfer).ToString("MM/dd/yyyy") : "01/01/1900",
                CreatedBy = _bblAssociateService.FindByAdminId(item.CreatedBy).First().UserName,
                ReasonForTransfer = item.ReasonForTransfer
            }).ToList();
            return PartialView("BBL/_TransferHistoryPartial", transferDetails);
            //Session["UserName"].ToString(),
        }

        #endregion Assign License

        #region Business Activities

        ///// <summary>
        ///// To Return Business Activity Page.
        ///// </summary>
        ///// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult BusinessActivities(string type)
        {
            BusinessActivityViewModel activityModel = new BusinessActivityViewModel {CategoryType = type};
            TempData["CategoryType"] = type;
            return View(activityModel);
        }

        /// <summary>
        /// This method is usedto create BusinessActivities
        /// </summary>
        /// <param name="activityModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        //  [ConcurrentLogin]
        public ActionResult BusinessActivities(ActivityModel activityModel)
        {
            var responsestatus = "";
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                    if (ModelState.IsValid)
                    {
                        BusinessActivityEntity businessActivityEntity = new BusinessActivityEntity();
                        if (activityModel.APP_Type == null)
                        {
                            activityModel.APP_Type = "1";
                        }
                        //  activityModel.ActivityID = TempData["ActivityId"] != null ? TempData["ActivityId"].ToString() : string.Empty;
                        businessActivityEntity.InjectFrom(activityModel);
                        businessActivityEntity.APP_Type = activityModel.APP_Type;
                        int result = _bblBusinessActivity.InsertUpdateBusinessActivity(businessActivityEntity);
                        switch (result)
                        {
                            case 1:
                                // ReSharper disable once RedundantAssignment
                                responsestatus = "inserted";
                                return JavaScript(" $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Business Activity is added successfully');");
                              //  break;

                            case 3:
                                // ReSharper disable once RedundantAssignment
                                responsestatus = "exists";
                                return JavaScript(" $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('This Business Activity already exist, please choose another');");
                               // break;
                        }
                    }
                    return Json(new { status = "success", response = responsestatus });
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                { return Json(new { status = "SessionExipred" }); }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Business Activities", ex); 
            }
        }
        /// <summary>
        /// This method is used to display Business Activities
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [ConcurrentLogin]
        public PartialViewResult BusinessActivitiesGridPartial()
        {
            var repodata = _bblBusinessActivity.GetAllBusinessActivities().ToList().OrderBy(x => x.ActivityName);
            var businessActivities = repodata.Select(item => new ActivityModel
            {
                ActivityID = item.ActivityID,
                ActivityName = item.ActivityName,
                APP_Type = item.APP_Type
            }).ToList();
            //businessActivities = businessActivities.OrderBy(x => x.CreateDate).ToList();
            return PartialView("BBL/_BusinessActivitiesGridPartial", businessActivities);
        }
        /// <summary>
        /// This method loads  the BusinessActivity Page for creation of new Activities
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public PartialViewResult BusinessActivityAddPartial()
        {
            //    return Json(new { status = "success" });
            return PartialView("BBL/_BusinessActivityAddPartial");
        }
        /// <summary>
        /// This method returns the BusinessActivity Page for updation of activity
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public PartialViewResult UpdateBusinessActivityPartial(string activityId)
        {
            var businessActivityModel = new BusinessActivityModel { ActivityId = activityId };
            var businessActivities = new ActivityModel();
            var repodata = _bblBusinessActivity.FindByIDBasedonActivityId(businessActivityModel).FirstOrDefault();
            businessActivities.InjectFrom(repodata);
            return PartialView("BBL/_UpdateBusinessActivityPartial", businessActivities);
        }
        /// <summary>
        /// This method is used to Update the Business Activity
        /// </summary>
        /// <param name="activityModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        //    [ConcurrentLogin]
        public ActionResult UpdateBusinessActivity(ActivityModel activityModel)
        {
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                   // string statusdata;
                    var businessActivityEntity = new BusinessActivityEntity();
                    businessActivityEntity.InjectFrom(activityModel);
                    businessActivityEntity.APP_Type = activityModel.APP_Type;
                    var response = _bblBusinessActivity.InsertUpdateBusinessActivity(businessActivityEntity);
                    if (response == 4)
                    {
                        { return JavaScript("var redirect='Update';$('#modelerror').css('color','red'); $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('You have not made any changes to the Business Activity. If needed, please do changes and then click on [Update Business Activity]');"); }
                    }
                    string statusdata = response == 2 ? "success" : "exists";
                    if (statusdata.ToUpper() == "SUCCESS")
                    { return JavaScript("var redirect='';$('#modelerror').css('color','green'); $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Business Activity is updated successfully');"); }
                    // ReSharper disable once RedundantIfElseBlock
                    else if (statusdata.ToUpper() == "EXISTS")
                    { return JavaScript("var redirect='Update';$('#modelerror').css('color','red'); $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('This Business Activity already exist, please choose another');"); }

                    return Json(new { status = statusdata });
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                { return Json(new { status = "SessionExipred" }); }
            }
            catch (Exception ex)
            { throw new Exception("Exception occurs in Update Business Activity", ex); }
        }
        /// <summary>
        /// This method is used to Delete the Business Activity based on activityId
        /// </summary>
        /// <param name="businessActivityEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult DeleteBusinessActivity(BusinessActivityEntity businessActivityEntity)
        {
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                    var resp = _bblBusinessActivity.DeleteBusinessActivity(businessActivityEntity);
                    if (resp)
                    {
                        if (businessActivityEntity.APP_Type == "0")
                        {
                            return JavaScript(" $('#modelerror').css('color','green');$('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Business Activity is Deactivated Successfully');");
                        }
                        // ReSharper disable once RedundantIfElseBlock
                        else if (businessActivityEntity.APP_Type == "1")
                        {
                            return JavaScript("$('#modelerror').css('color','green'); $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Business Activity is Activated Successfully');");
                        }
                    }
                    return Json(new { status = "success" });
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Delete Business Activity", ex); 
            }
        }

        #endregion Business Activities

        #region Primary Categories
        /// <summary>
        /// This method returns the PrimayCategories based on activityId
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [Authorize]
        [ConcurrentLogin]
        public ActionResult PrimaryCategories(string activityId, string type)
        {
            if (type != null)
            {
                var masterPrimaryCategory = _bblprimarycategories.FindByCategoryID(activityId).FirstOrDefault();
                if (masterPrimaryCategory != null)
                {
                    var primarycategory = masterPrimaryCategory.ActivityID;
                    ViewBag.ActivityId = primarycategory;
                }
            }
            else
            {
                ViewBag.ActivityId = activityId;
            }

            return View();
        }
        /// <summary>
        /// This method displays Primary Category based on activityId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public PartialViewResult PrimaryCategoriesPartial(string id)
        {
            //string activityId = TempData["ActivityId"].ToString();
            var repodata = _bblprimarycategories.FindByPrimaryIdbasedonActivity(id);
            var primarycategories = repodata.Select(item => new PrimaryCategoryModel
            {
                PId = item.PrimaryID.ToString(),
                IsSecondaryLicenseCategory = Convert.ToBoolean(item.IsSecondaryLicenseCategory),
                IsSubCategory = Convert.ToBoolean(item.IsSubCategory),
                PrimaryID = item.PrimaryID,
                ActivityID = item.ActivityID,
                Description = item.Description,
                Endorsement = item.Endorsement,
                CategoryCode = item.CategoryCode,
                Status = item.Status
            }).ToList().OrderBy(x => x.Description);

            ViewBag.CategoryType = TempData["CategoryType"];
            TempData["CategoryType"] = ViewBag.CategoryType;
            return PartialView("BBL/_PrimaryCategoriesPartial", primarycategories);
        }
        /// <summary>
        /// This method is used to create PrimaryCategory 
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult CreatePrimaryCategory(string activityId)
        {
            ViewBag.Status = string.Empty;
            ViewBag.ActivityId = activityId.Trim();
            //  TempData["ActivityId"] = activityId.Trim();
            PrimaryCategoryModel primaryCategoryModel = new PrimaryCategoryModel();
            primaryCategoryModel.ActivityID = activityId.Trim();
            //     ViewBag.IsPrmaryCreate = otherParam;
            return View(primaryCategoryModel);
        }

        private bool Checkuntits(string units)
        {
            bool result = _submissionCategoryService.Checkunits(units);
            return result;
        }
        /// <summary>
        /// This Method is used to Create Primary Category based on activityId
        /// </summary>
        /// <param name="primaryCategoryModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult CreatePrimaryCategory(PrimaryCategoryModel primaryCategoryModel)
        {
            ViewBag.Status = string.Empty;
            string response = "0";
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                    if (ModelState.IsValid)
                    {
                        if (string.IsNullOrEmpty(primaryCategoryModel.BusinessMustbeinDC) ||
                            string.IsNullOrEmpty(primaryCategoryModel.SubCategory) ||
                            string.IsNullOrEmpty(primaryCategoryModel.SecondaryLicenseCategory) ||
                            string.IsNullOrEmpty(primaryCategoryModel.HOP_EHOPAllowed) ||
                            string.IsNullOrEmpty(primaryCategoryModel.CofORequired) ||
                             string.IsNullOrEmpty(primaryCategoryModel.ExemptfromAllFees) ||
                            string.IsNullOrEmpty(primaryCategoryModel.IsBackgroundInvestigation))
                        {
                            if (primaryCategoryModel.Fee_Code == "C" || primaryCategoryModel.Fee_Code == "T")
                            {
                                if (primaryCategoryModel.Fee_Code == "T" && primaryCategoryModel.End > 99999)
                                {
                                    ViewBag.endmsg = "End Range must not be greater than 99999";
                                }
                                if (primaryCategoryModel.UnitOne == null)
                                {
                                    ViewBag.Unitmsg = "Primary Units A is requried";
                                }
                                if (primaryCategoryModel.UserQuestion1 == null)
                                {
                                    ViewBag.UnitQuestionmsg = "Primary Units A Question is requried";
                                }
                            }
                            ViewBag.radiobuttonsmsg = "Please answer all Questions";
                            return View(primaryCategoryModel);
                        }
                        // ReSharper disable once RedundantIfElseBlock
                        else
                        {
                            ViewBag.ActivityID = primaryCategoryModel.ActivityID;
                            if (primaryCategoryModel.Fee_Code == "C" || primaryCategoryModel.Fee_Code == "T")
                            {
                                if (primaryCategoryModel.Fee_Code == "T" && primaryCategoryModel.End > 99999)
                                {
                                    ViewBag.endmsg = "End Range must not be greater than 99999";
                                    return View(primaryCategoryModel);
                                }
                                if (primaryCategoryModel.UnitOne == null)
                                {
                                    ViewBag.Unitmsg = "Primary Units A is requried";

                                    return View(primaryCategoryModel);
                                }
                                if (primaryCategoryModel.UserQuestion1 == null)
                                {
                                    ViewBag.UnitQuestionmsg = "Primary Units A Question is requried";
                                    return View(primaryCategoryModel);
                                }
                            }
                            var primaryPhysicallocation = new PrimaryPhysicallocation();
                            primaryPhysicallocation.InjectFrom(primaryCategoryModel);
                            primaryPhysicallocation.ActivityID = primaryCategoryModel.ActivityID;
                            primaryPhysicallocation.App_Type = primaryPhysicallocation.App_Type == "Business" ? "B" : primaryPhysicallocation.App_Type == "Individual" ? "I" : primaryPhysicallocation.App_Type;

                            primaryPhysicallocation.IsSecondaryLicenseCategory = primaryCategoryModel.SecondaryLicenseCategory != "NO";
                            primaryPhysicallocation.IsSubCategory = primaryCategoryModel.SubCategory != "NO";
                            primaryPhysicallocation.IsBackgroundInvestigation = primaryCategoryModel.IsBackgroundInvestigation != "NO";
                            primaryPhysicallocation.IsPDFShow = primaryCategoryModel.IsPDFShow != "NO";
                            primaryPhysicallocation.LicenseType = primaryPhysicallocation.App_Type;
                            primaryPhysicallocation.Status = true;
                            primaryPhysicallocation.PrimaryID = string.Empty;
                            primaryPhysicallocation.Fee_Code = primaryCategoryModel.Fee_Code;
                            if (primaryCategoryModel.Fee_Code == "S" || primaryCategoryModel.Fee_Code == "C")
                            {
                                primaryPhysicallocation.Start = 0;
                                primaryPhysicallocation.End = 99999;
                            }
                            else
                            {
                                primaryPhysicallocation.Start = Convert.ToInt32(primaryCategoryModel.Start);
                                primaryPhysicallocation.End = Convert.ToInt32(primaryCategoryModel.End);
                            }
                            primaryPhysicallocation.UnitTwo = "NA";

                            primaryPhysicallocation.License_Fee = primaryCategoryModel.License_Fee;
                            primaryPhysicallocation.Tier = primaryCategoryModel.Tier;
                            //     string primaryid = _bblprimarycategories.InsertUpdatePrimaryCategory(primaryPhysicallocation);
                            string primaryid = _bblAssociateService.InsertUpdatePrimaryCategory(primaryPhysicallocation);
                            string unitone = primaryPhysicallocation.UnitOne == null ? "" : primaryPhysicallocation.UnitOne.Replace(System.Environment.NewLine, "") ?? "";
                            // ReSharper disable once UnusedVariable
                            string unittwo = primaryPhysicallocation.UnitTwo == null ? "" : primaryPhysicallocation.UnitTwo.Replace(System.Environment.NewLine, "") ?? "";

                            if (primaryid == "3")// && questionsAdd == 1)
                            {
                                // ReSharper disable once RedundantAssignment
                                response = primaryid;
                                @ViewBag.Status = "This Primary License Category name already exist";
                                return View(primaryCategoryModel);
                            }
                            // ReSharper disable once RedundantIfElseBlock
                            else if (primaryid == "4")// && questionsAdd == 1)
                            {
                                // ReSharper disable once RedundantAssignment
                                response = primaryid;
                                @ViewBag.Status = "This Category Id already exists.Please provide another one";
                                return View(primaryCategoryModel);
                            }
                            // ReSharper disable once RedundantIfElseBlock
                            else
                            {
                                if (unitone != "")
                                {
                                    _feeCodeMapService.InsertUpdateFee(primaryPhysicallocation);
                                }

                                primaryPhysicallocation.PrimaryID = primaryid;
                                // ReSharper disable once RedundantAssignment
                                response = _categoryphysical.InsertUpdatePhysicalLocation(primaryPhysicallocation);
                                ModelState.Clear();
                                var primaryCategoryModel1 = new PrimaryCategoryModel
                                {
                                    ActivityID = primaryCategoryModel.ActivityID.Trim()
                                };
                                ViewBag.IsPrmaryCreate = true;
                                return View(primaryCategoryModel1);
                            }
                        }
                    }
                    return View();
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Create Primary Category", ex); 
            }
        }
        /// <summary>
        /// This method is used to Update the Primary Category
        /// </summary>
        /// <param name="primaryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult UpdatePrimaryCategory(string primaryId)
        {
            if (primaryId != null)
            {
                ViewBag.PrimaryId = primaryId;
                var repodata = _bblprimarycategories.FindByCategoryIDBasedonPrimaryId(primaryId).First();
                var primaryCategoryModel = new PrimaryLicenseCategoryModel();
                primaryCategoryModel.InjectFrom(repodata);
                var result =
                    _categoryphysical.FindCategoryPhysicallocationsById(primaryId).FirstOrDefault();
                if (result != null)
                {
                    var feecode = _bblAssociateService.FindFeesByDescription(primaryCategoryModel.Description.Trim()).ToList();
                    if (feecode.Count() != 0)
                    {
                        primaryCategoryModel.Fee_Code = feecode.First().Fee_Code.ToString() ?? "";
                    }
                    //   TempData["ActivityId"] = repodata.ActivityID.ToString();

                    ViewBag.ActivityId = repodata.ActivityID.ToString();
                    primaryCategoryModel.IsSecondaryLicenseCategory = Convert.ToBoolean(repodata.IsSecondaryLicenseCategory);
                    primaryCategoryModel.IsSubCategory = Convert.ToBoolean(repodata.IsSubCategory);
                    primaryCategoryModel.BusinessMustbeinDC = result.BusinessMustBeInDC.Trim();
                    primaryCategoryModel.ExemptfromAllFees = result.ExemptFromAllFees.Trim();
                    primaryCategoryModel.CofORequired = result.COFORequired.Trim();
                    primaryCategoryModel.HOP_EHOPAllowed = result.HOP_EHOPAllowed.Trim();
                    primaryCategoryModel.IsBackgroundInvestigation = Convert.ToBoolean(repodata.IsBackgroundInvestigation);
                    primaryCategoryModel.IsPDFShow = Convert.ToBoolean(repodata.IsPDFShow);
                    primaryCategoryModel.App_Type = repodata.App_Type.Trim() == "B" ? "Business" : "Individual";

                    var getQuestion = _bblAssociateService.FindBySecondaryName(primaryCategoryModel.Description.Trim()).ToList();
                    if (getQuestion.Any())
                    {
                        var questionsData = getQuestion.FirstOrDefault();
                        if (questionsData != null) primaryCategoryModel.UserQuestion1 = questionsData.UserQuestion;
                    }
                    else
                    {
                        primaryCategoryModel.UserQuestion1 = null;
                    }
                    //primaryCategoryModel.UserQuestion1 =
                    //    _bblAssociateService.FindBySecondaryName(primaryCategoryModel.Description.Trim())
                    //        .First()
                    //        .UserQuestion;
                }
                return View(primaryCategoryModel);
            }

            return View();
        }
        /// <summary>
        /// This method is used to update Primary Category based on PrimaryId
        /// </summary>
        /// <param name="primaryCategoryModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult UpdatePrimaryCategory(PrimaryLicenseCategoryModel primaryCategoryModel)
        {
         //   string response = "0";
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                    if (ModelState.IsValid)
                    {
                        var primaryPhysicallocation = new PrimaryPhysicallocation();
                        if (primaryCategoryModel.Fee_Code == "C" || primaryCategoryModel.Fee_Code == "T")
                        {
                            if (primaryCategoryModel.UnitOne == null)
                            {
                                ViewBag.Unitmsg = "Primary Units A is requried";
                                return View(primaryCategoryModel);
                            }
                            if (primaryCategoryModel.UserQuestion1 == null)
                            {
                                ViewBag.UnitQuestionmsg = "Primary Units A Question is requried";
                                return View(primaryCategoryModel);
                            }
                        }

                        primaryPhysicallocation.InjectFrom(primaryCategoryModel);

                        primaryPhysicallocation.ActivityID = primaryCategoryModel.ActivityID;
                        primaryPhysicallocation.App_Type = primaryPhysicallocation.App_Type == "Business" ? "B" : primaryPhysicallocation.App_Type == "Individual" ? "I" : primaryPhysicallocation.App_Type;
                        primaryPhysicallocation.LicenseType = primaryPhysicallocation.App_Type;
                        primaryPhysicallocation.UnitOne = primaryPhysicallocation.UnitOne == null ? "NA" : primaryPhysicallocation.UnitOne.Replace(System.Environment.NewLine, "") ?? "NA";
                        primaryPhysicallocation.OldUnitOne = primaryPhysicallocation.OldUnitOne == null ? "NA" : primaryPhysicallocation.OldUnitOne.Replace(System.Environment.NewLine, "") ?? "NA";

                        primaryPhysicallocation.PrimaryID = primaryCategoryModel.PrimaryID;

                        primaryPhysicallocation.Fee_Code = primaryCategoryModel.Fee_Code;
                        string response = _bblprimarycategories.UpdatePrimaryCategory(primaryPhysicallocation);
                        if (response == "2")
                        {
                            _categoryphysical.InsertUpdatePhysicalLocation(primaryPhysicallocation);
                            if (primaryPhysicallocation.OldCategoryName.ToUpper().Trim() !=
                                primaryPhysicallocation.Description.ToUpper().Trim())
                            {
                                _oSubCategoryFeeService.UpdateSubFee(primaryPhysicallocation);
                            }

                            if (primaryPhysicallocation.OldUnitOne.ToUpper().Trim() !=
                                primaryPhysicallocation.UnitOne.ToUpper().Trim().Replace("NA", ""))
                            {
                                _feeCodeMapService.UpdateFeecode(primaryPhysicallocation);
                            }
                        }
                        if (response == "2")
                        {
                            ViewBag.IsPrmaryUpdate = true;
                        }
                        else if (response == "1")
                        {
                            @ViewBag.ErrMsg = "This Category Id already exists.Please provide another one";
                            return View(primaryCategoryModel);
                        }
                    }

                    return View(primaryCategoryModel);
                }
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Update Primary Category", ex); 
            }
        }
        /// <summary>
        /// This method is used to Delete Primary Category based on Primary Id
        /// </summary>
        /// <param name="primaryCatEntity"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult DeletePrimaryCategory(PrimaryCategoryEntity primaryCatEntity)
        {
            var response = _bblprimarycategories.DeletePrimaryCategory(primaryCatEntity);
            if (response)
            {
                return JavaScript(" $('#modelerror').css('color','green');$('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Primary License Category is Deactivated Successfully');");
            }
            //  return RedirectToAction("PrimaryCategories");
            return Json(new { status = "success" });
        }

        #endregion Primary Categories

        #region Secondary Categories
        /// <summary>
        /// This method returns SecondaryCategories
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="primaryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult SecondaryCategories(string activityId, string primaryId)
        {
            if (_businesscentercommon.ValidateSessionResult())
            {
                //  TempData["PrimaryId"] = primaryId;
                var primary = _bblprimarycategories.FindByCategoryID(primaryId).First();
                ViewBag.PrimaryName = primary.Description;
                ViewBag.PrimaryId = primaryId;
                ViewBag.activityId = activityId;
                TempData["PrimaryName"] = primary.Description;
                return View();
            }
            else
            { return RedirectToAction("SessionExpiry", "Account"); }
        }
        /// <summary>
        /// This method is used to display all PrimaryCategories based on text entered in autosuggest Textbox
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        [Authorize]
        //  [ConcurrentLogin]
        public JsonResult GetPrimaries(string term)
        {
           // string primaryName = string.Empty;
            if (_businesscentercommon.ValidateSessionResult())
            {
                string primaryName = TempData["PrimaryName"].ToString();
                List<string> primaries = _bblprimarycategories.FindByPrimaryCategory(term).ToList();
                primaries.Remove(primaryName);
                TempData["PrimaryName"] = primaryName;
                return Json(primaries, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<string> sessionexpiry = new List<string>();
                sessionexpiry.Add("Session Expired");

                return Json(sessionexpiry, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// This method is used to display all UserNames based on text entered in autosuggest Textbox
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        [Authorize]
        //[ConcurrentLogin]
        public async Task<ActionResult> GetTransferUsers(string term)
        {
            if (_businesscentercommon.ValidateSessionResult())
            {
               // string userName = string.Empty;
                string userName = TempData["UserName"].ToString();

                UserTypeModel model = new UserTypeModel();
                model.UserStatus = "Active";
                model.searchText = "";
                var users = await _gridBindings.UserTypeBasedList(model);
                users = users.Where(x => x.UserName.Replace(System.Environment.NewLine, "").Trim() != userName.Trim()).OrderBy(x => x.UserName).ToList();
                //var primaryCategory = (FindBy(x => x.Description.StartsWith(term) && x.Status == true).Select(y => y.Description).ToList());
                //var filteredUsers = users.Where(x => x.UserName.Replace(System.Environment.NewLine, "").Trim() != userName.Trim()).OrderBy(x => x.UserName);
                var filteredUsers = (users.Where(x => x.UserName.ToUpper().StartsWith(term.ToUpper())).Select(y => y.UserName).ToList());
                TempData["UserName"] = userName;
                return Json(filteredUsers, JsonRequestBehavior.AllowGet);
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            {
                List<string> sessionexpiry = new List<string>();
                sessionexpiry.Add("Session Expired");

                return Json(sessionexpiry, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// This method is used to create Secondary Category based on PrimaryId
        /// </summary>
        /// <param name="SecondaryModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        //[ConcurrentLogin]
        public ActionResult SecondaryCategories(SecondaryModel SecondaryModel)
        {
            string result = "";
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                    if (ModelState.IsValid)
                    {
                        SlCategoryEntity slCategoryEntity = new SlCategoryEntity();
                        slCategoryEntity.InjectFrom(SecondaryModel);

                        var primaries = _bblprimarycategories.FindByPrimaryName(SecondaryModel.SecondaryLicenseCategory).ToList();

                        int isSubCategoryCount = 0;

                        var getprimaries = _bblprimarycategories.FindByCategoryID(SecondaryModel.PrimaryId).ToList();

                        if (getprimaries.Any())
                        {
                            var primarycategories = getprimaries.FirstOrDefault();
                            if (primarycategories != null)
                            {
                                if (Convert.ToBoolean(primarycategories.IsSubCategory))
                                {
                                    isSubCategoryCount = isSubCategoryCount + 1;
                                }
                            }
                        }

                        var getSecondaries = _bblsecondarycategories.FindSecondaryCategoryById(SecondaryModel.PrimaryId).ToList();
                        if (getSecondaries.Any())
                        {
                            foreach (var item in getSecondaries)
                            {
                                var getSecondary = _bblprimarycategories.SecondaryEndorsement(item.SecondaryLicenseCategory).ToList();
                                if (getSecondary.Any())
                                {
                                    var secondarydata = getSecondary.FirstOrDefault();
                                    if (secondarydata != null)
                                    {
                                        if (Convert.ToBoolean(secondarydata.IsSubCategory))
                                        {
                                            isSubCategoryCount = isSubCategoryCount + 1;
                                        }
                                    }
                                }
                            }
                        }

                        if (!primaries.Any())
                        {
                            // ReSharper disable once RedundantAssignment
                            result = "notexists";
                            return JavaScript(" $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Please add the Secondary Category from the auto suggest only');");
                        }
                        // ReSharper disable once RedundantIfElseBlock
                        else
                        {
                            var primaycategory = _bblprimarycategories.FindByCategoryID(SecondaryModel.PrimaryId).First();
                            if (primaycategory.Description.ToUpper().Trim() == SecondaryModel.SecondaryLicenseCategory.ToUpper().Trim())
                            {
                                TempData["PrimaryId"] = SecondaryModel.PrimaryId;
                                // ReSharper disable once RedundantAssignment
                                result = "invalid";
                                return JavaScript(" $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('invalid Categoryname');");
                            }
                            // ReSharper disable once RedundantIfElseBlock
                            else
                            {
                                bool status = true;
                                var getSecondary = _bblprimarycategories.SecondaryEndorsement(SecondaryModel.SecondaryLicenseCategory).ToList();
                                if (isSubCategoryCount != 0)
                                {
                                      var secondarydata = getSecondary.FirstOrDefault();
                                    if (secondarydata != null)
                                    {
                                        if (Convert.ToBoolean(secondarydata.IsSubCategory))
                                        {
                                            status = false;
                                        }
                                    }
                                }
                                if (status)
                                {
                                    TempData["PrimaryId"] = SecondaryModel.PrimaryId;
                                    var response = _bblsecondarycategories.InsertUpdateSlCategory(slCategoryEntity);
                                    switch (response)
                                    {
                                        case 1:
                                            // ReSharper disable once RedundantAssignment
                                            result = "inserted";
                                            return JavaScript("var redirect='insert'; $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Secondary License Category added successfully');");
                                          //  break;

                                        case 3:
                                            // ReSharper disable once RedundantAssignment
                                            result = "exists";
                                            return JavaScript(" $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Secondary License Category already exist, please choose another');");
                                           // break;
                                    }
                                }
                                else
                                {
                                    // ReSharper disable once RedundantAssignment
                                    result = "exists";
                                    return JavaScript(" $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('You cannot add the selected License category as a Secondary. The Primary/Secondary License might already have Sub categories.');");
                                }
                                return Json(new { status = "success", response = result });
                            }
                        }
                    }
                    return Json(new { status = "success", response = result });
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Secondary Categories", ex); 
            }
        }
        /// <summary>
        /// This method returns Secondary Categories
        /// </summary>
        /// <param name="primaryName"></param>
        /// <param name="primaryId"></param>
        /// <returns></returns>
        [Authorize]
        //[ConcurrentLogin]
        public PartialViewResult SecondaryCategoriesGridPartial(string primaryName, string primaryId)
        {
            if (_businesscentercommon.ValidateSessionResult())
            {
                // ReSharper disable once RedundantAssignment
                var secondarycategories = new List<SecondaryModel>();

                if (primaryName != null)
                {
                    var primary = _bblprimarycategories.SecondaryEndorsement(primaryName).ToList();
                    // ReSharper disable once PossibleNullReferenceException
                    primaryId = primary.FirstOrDefault().PrimaryID;
                }

                var repodata = _bblprimarycategories.SecondaryCategoriesList(primaryId).ToList().OrderBy(x => x.SecondaryLicenseCategory);
                secondarycategories = repodata.Select(i => new SecondaryModel().InjectFrom(i)).Cast<SecondaryModel>().ToList();
                return PartialView("BBL/_SecondaryCategoriesGridPartial", secondarycategories);
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            // ReSharper disable once Mvc.PartialViewNotResolved
            { return PartialView("Account/SessionExpiry"); }
        }
        /// <summary>
        /// This method is used to create Secondary Category
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        //  [ConcurrentLogin]
        public PartialViewResult SecondaryCategoryAddPartial()
        {
            if (_businesscentercommon.ValidateSessionResult())
            {
                return PartialView("BBL/_SecondaryCategoryAddPartial");
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            // ReSharper disable once Mvc.PartialViewNotResolved
            { return PartialView("Account/SessionExpiry"); }
        }
        /// <summary>
        /// This method is used to update Secondary Category
        /// </summary>
        /// <param name="secondaryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        // [ConcurrentLogin]
        public PartialViewResult UpdateSecondaryCategoryPartial(string secondaryId)
        {
            if (_businesscentercommon.ValidateSessionResult())
            {
                var repodata = _bblsecondarycategories.FindBySecondaryBasedonSecondaryId(secondaryId).FirstOrDefault();
                var secondarymodel = new SecondaryModel();
                secondarymodel.InjectFrom(repodata);
                if (repodata != null)
                {
                    secondarymodel.PrimaryId = repodata.PrimaryID;
                    secondarymodel.SecondaryId = repodata.SecondaryID;
                }

                return PartialView("BBL/_UpdateSecondaryCategoryPartial", secondarymodel);
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            // ReSharper disable once Mvc.PartialViewNotResolved
            { return PartialView("Account/SessionExpiry"); }
        }
        /// <summary>
        /// This method is used to update the Secondary Category based on SecondaryId
        /// </summary>
        /// <param name="secondaryModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        //  [ConcurrentLogin]
        public ActionResult UpdateSecondaryCategories(SecondaryModel secondaryModel)
        {
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                    string result = string.Empty;

                    var repodata = _bblsecondarycategories.FindBySecondaryBasedonSecondaryId(secondaryModel.SecondaryId).FirstOrDefault();
                    if (repodata != null) secondaryModel.SecondaryLicenseCategory = repodata.SecondaryLicenseCategory;
                    SlCategoryEntity slCategoryEntity = new SlCategoryEntity();
                    slCategoryEntity.InjectFrom(secondaryModel);

                    var primaycategory = _bblprimarycategories.FindByCategoryIDBasedonPrimaryId(secondaryModel.PrimaryId).First();
                    if (primaycategory.Description.ToUpper().Trim() == secondaryModel.SecondaryLicenseCategory.ToUpper().Trim())
                    {
                        result = "invalid";
                    }
                    else
                    {
                        var response = _bblprimarycategories.ActiveSecondary(slCategoryEntity);

                        if (response == 2)
                        {
                            return JavaScript("var redirect='Update'; $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Secondary Category is updated successfully');");
                            // result = "success";
                        }
                        // ReSharper disable once RedundantIfElseBlock
                        else if (response == 3)
                        {
                            result = "exists";
                        }
                        else if (response == 4)
                        {
                            result = "Inactive";
                        }
                    }
                    return Json(new { status = result });
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            { throw new Exception("Exception occurs in Update Secondary Categories", ex); }
        }
        /// <summary>
        /// This method is used to Delete Secondary Category based on SecondaryId
        /// </summary>
        /// <param name="slCategoryEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        // [ConcurrentLogin]
        public ActionResult DeleteSecondaryCategory(SlCategoryEntity slCategoryEntity)
        {
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                    var resp = _bblsecondarycategories.DeleteSecondaryCategory(slCategoryEntity);
                    if (resp)
                    {
                        if (slCategoryEntity.Status == false)
                        {
                            return JavaScript(" $('#modelerror').css('color','green');$('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Secondary License Category is Deactivated Successfully');");
                        }
                        // ReSharper disable once RedundantIfElseBlock
                        else if (slCategoryEntity.Status == true)
                        {
                            return JavaScript(" $('#modelerror').css('color','green');$('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Secondary License Category is activated Successfully');");
                        }
                    }
                    return Json(new { status = "success" });
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                { return Json(new { status = "SessionExipred" }); }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Delete Secondary Category", ex); 
            }
        }

        #endregion Secondary Categories

        #region Sub Categories
        /// <summary>
        /// This method returns subcategories
        /// </summary>
        /// <param name="primaryId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult SubCategories(string primaryId)
        {
            var primary = _bblprimarycategories.FindByCategoryID(primaryId).First();
            ViewBag.PrimaryName = primary.Description;
            ViewBag.PrimaryId = primaryId;
            SubCategoryModel subCategoryModel = new SubCategoryModel {CustomCategoryName = primary.Description};
            return View(subCategoryModel);
        }
        /// <summary>
        /// This method is used to create Subcategories based on PrimaryId
        /// </summary>
        /// <param name="subCategoryModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        // [ConcurrentLogin]
        public ActionResult SubCategories(SubCategoryModel subCategoryModel)
        {
            var responsecheck = "";
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                    if (ModelState.IsValid)
                    {
                        //ViewBag.PrimaryId = primaryId;
                        SubCategoryEntity subCategoryEntity = new SubCategoryEntity();

                        //  activityModel.ActivityID = TempData["ActivityId"] != null ? TempData["ActivityId"].ToString() : string.Empty;
                        subCategoryEntity.InjectFrom(subCategoryModel);

                        int result = _subCategoryService.InsertUpdateSubCategory(subCategoryEntity);
                        switch (result)
                        {
                            case 1:
                                // ReSharper disable once RedundantAssignment
                                responsecheck = "inserted";
                                //  return JavaScript("var redirect='insert'; $('.homemodaldiv').modal('show');$('.modal-body .error_message').empty().append('Sub-Category has been added successfully');");
                                return JavaScript("var redirect='insert'; $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Sub-Category has been added successfully');");
                               // break;

                            case 3:
                                responsecheck = "exists";
                                break;
                        }
                    }
                    return Json(new { status = "success", response = responsecheck });
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                { return Json(new { status = "SessionExipred" }); }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Sub Categories ", ex); 
            }
        }
        /// <summary>
        /// This method is used to display subCategories based on primaryId
        /// </summary>
        /// <param name="primaryId"></param>
        /// <returns></returns>
        [Authorize]
        [ConcurrentLogin]
        public PartialViewResult SubCategoriesGridPartial(string primaryId)
        {
            var subcategories = new List<SubCategoryModel>();
            //if (primaryName != null)
            //{
            //    var primary = _bblprimarycategories.SecondaryEndorsement(primaryName).ToList();
            //    var repodata = _bblsecondarycategories.FindSecondaryCategoryById(primary.FirstOrDefault().PrimaryID).ToList();
            //    subcategories = repodata.Select(i => new SubCategoryModel().InjectFrom(i)).Cast<SubCategoryModel>().ToList();
            //}
            if (primaryId != null)
            {
                var primary = _bblprimarycategories.FindByCategoryID(primaryId).ToList();
                var primaryCateogry = primary.FirstOrDefault();
                if (primaryCateogry != null)
                {
                    var repodata = _subCategoryService.FindBySubCategoriesBasedonPrimaryName(primaryCateogry.Description).ToList().OrderBy(x => x.SubCategoryName);
                    //  var repodata = _bblsecondarycategories.FindSecondaryCategoryById(primaryId).ToList();
                    subcategories = repodata.Select(i => new SubCategoryModel().InjectFrom(i)).Cast<SubCategoryModel>().ToList();
                }
            }
            return PartialView("BBL/_SubCategoriesGridPartial", subcategories);
        }
        /// <summary>
        /// This method is used to load  SubCategoryPage for creation of SubCategory
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public PartialViewResult SubCategoryAddPartial()
        {
            return PartialView("BBL/_SubCategoryAddPartial");
        }
        /// <summary>
        /// This method is used to Load SubCategory 
        /// </summary>
        /// <param name="subCatId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public PartialViewResult UpdateSubCategoryPartial(string subCatId)
        {
            //var subCategoryModel = new SubCategoryModel { SuperSubCatID = superSubCatID };
            var subCategory = new SubCategoryModel();
            var repodata = _subCategoryService.FindBySubCategoryBasedonSubcatId(subCatId).FirstOrDefault();
            subCategory.InjectFrom(repodata);
            return PartialView("BBL/_UpdateSubCategoryPartial", subCategory);
        }
        /// <summary>
        /// This method is used to Update SubCategory based on SubcategoryId
        /// </summary>
        /// <param name="subCategoryModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        //  [ConcurrentLogin]
        public ActionResult UpdateSubCategory(SubCategoryModel subCategoryModel)
        {
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                    string statuscheck;
                    var subcategory = new SubCategoryEntity();
                    if (subCategoryModel.SubCategoryName == null)
                    {
                        var repodata =
                            _subCategoryService.FindBySubCategoryBasedonSubcatId(subCategoryModel.SubCatID).FirstOrDefault();
                        if (repodata != null)
                        {
                            subCategoryModel.SubCategoryName = repodata.SubCategoryName;
                            subCategoryModel.CustomCategoryName = repodata.CustomCategoryName;
                        }
                    }
                    subcategory.InjectFrom(subCategoryModel);
                    subcategory.Status = subCategoryModel.Status;
                    var response = _subCategoryService.InsertUpdateSubCategory(subcategory);
                    //status = response == 2 ? "success" : "exists";
                    if (response == 2)
                    {
                        // return JavaScript("var redirect='update'; $('.homemodaldiv').modal('show');$('.modal-body .error_message').empty().append('Subcategory is updated successfully');");
                        return JavaScript("var redirect='Update'; $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Subcategory is updated successfully');");
                    }
                    if (response == 4)
                    {
                        { return JavaScript("var redirect='Update';$('#modelerror').css('color','red'); $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('You have not made any changes to the SubCategory. If needed, please do changes and then click on [Update Sub-Category]');"); }
                    }
                    // ReSharper disable once RedundantIfElseBlock
                    else
                    {
                        statuscheck = "exists";
                    }
                    return Json(new { status = statuscheck });
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                { return Json(new { status = "SessionExipred" }); }
            }
            catch (Exception ex)
            { throw new Exception("Exception occurs in Update Sub Category", ex); }
        }
        /// <summary>
        /// This method is used to Delete Subcategory based on SubcategoryId
        /// </summary>
        /// <param name="subCategoryEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        // [ConcurrentLogin]
        public ActionResult DeleteSubCategory(SubCategoryEntity subCategoryEntity)
        {
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                    var resp = _subCategoryService.DeleteSubCategory(subCategoryEntity);
                    if (resp)
                    {
                        if (subCategoryEntity.Status == false)
                        {
                            return JavaScript(" $('#modelerror').css('color','green');$('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Subcategory is deactivated Successfully');");
                        }
                        // ReSharper disable once RedundantIfElseBlock
                        else if (subCategoryEntity.Status == true)
                        {
                            return JavaScript(" $('#modelerror').css('color','green');$('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Subcategory is activated Successfully');");
                        }
                    }
                    return Json(new { status = "success" });
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                { return Json(new { status = "SessionExipred" }); }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Delete Sub Category", ex); 
            }
        }

        #endregion Sub Categories

        #region Documents

        /// <summary>
        /// To Create documents and Display Existing documents for that Category
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult Documents(string categoryId, string type, string activityId)
        {
            DocumentsModel documentsModel = new DocumentsModel();
            if (activityId != null)
            {
                Session["Activityid"] = activityId;
            }

            if (type == "primary")
            {
                documentsModel.PrimaryID = categoryId;
                var primaycategory = _bblprimarycategories.FindByCategoryID(documentsModel.PrimaryID).First();
                documentsModel.CategoryName = primaycategory.Description;
                documentsModel.ActivityID = activityId;
                Session["Categoryname"] = primaycategory.Description;
            }
            else if (type == "secondary")
            {
                documentsModel.SecondaryID = categoryId;
                var secondarycategory = _bblsecondarycategories.FindSecondaryId(documentsModel.SecondaryID).First();
                documentsModel.CategoryName = secondarycategory.SecondaryLicenseCategory;
                documentsModel.ActivityID = activityId;
                Session["Categoryname"] = secondarycategory.SecondaryLicenseCategory;
            }
            ViewBag.PatientId = activityId;
            ViewBag.categoryId = categoryId;

            documentsModel.ActivityID = Convert.ToString(Session["Activityid"]);
            return View(documentsModel);
        }
        /// <summary>
        /// This method is used to create Documents
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="type"></param>
        /// <param name="activityId"></param>
        /// <param name="otherParam"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult CreateDocuments(string categoryId, string type, string activityId, string otherParam)
        {
            DocumentsModel documentsModel = new DocumentsModel
            {
                CategoryName = Convert.ToString(Session["Categoryname"]),
                PrimaryID = categoryId,
                ActivityID = activityId
            };
            ViewBag.categoryId = categoryId;
            ViewBag.activityId = activityId;
            ViewBag.IsDocumentCreate = otherParam;
            return View(documentsModel);
        }

        /// <summary>
        /// This method is used to Create documents based on PrimaryCategory
        /// </summary>
        /// <param name="documentsModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("CreateDocuments")]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult InsertDocuments(DocumentsModel documentsModel)
        {
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                    MasterCategoryDocumentModel masterCategoryDocumentModel = new MasterCategoryDocumentModel();
                    masterCategoryDocumentModel.InjectFrom(documentsModel);
                    var response = _bblAssociateService.InsertUpdateCategoryDocuments(masterCategoryDocumentModel);
                    if (response == 1)
                    {
                        //ViewBag.Status = "Document Created Successfully";
                        return RedirectToAction("CreateDocuments", "BBL", new
                        {
                            categoryId = documentsModel.PrimaryID,
                            type = "primary",
                            activityId = documentsModel.ActivityID,
                            otherParam = "true"
                        });
                    }
                    ModelState.Clear();
                    DocumentsModel documents = new DocumentsModel
                    {
                        CategoryName = masterCategoryDocumentModel.CategoryName
                    };
                    return View(documents);
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Insert Document", ex); 
            }
        }
        /// <summary>
        /// This method is used to load Documents Page
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [ConcurrentLogin]
        public PartialViewResult DocumentAddPartial()
        {
            return PartialView("BBL/_DocumentAddPartial");
        }
        /// <summary>
        /// This method is used to display Documents based on PrimaryCategory
        /// </summary>
        /// <param name="documentsModel"></param>
        /// <returns></returns>
        [Authorize]
        [ConcurrentLogin]
        public PartialViewResult DocumentsGridPartial(DocumentsModel documentsModel)
        {
            string categoryname;
            string customId;
            string activityId = documentsModel.ActivityID;
            // ReSharper disable once RedundantAssignment
            var categoryDocuments = new List<DocumentsModel>();
            if (documentsModel.PrimaryID != null)
            {
                var primaycategory = _bblprimarycategories.FindByCategoryID(documentsModel.PrimaryID).First();
                categoryname = primaycategory.Description;
                customId = primaycategory.PrimaryID;
            }
            else
            {
                //var secondarycategory = _bblsecondarycategories.FindSecondaryId(documentsModel.SecondaryID).First();
                //categoryname = secondarycategory.SecondaryLicenseCategory1;
                categoryname = Convert.ToString(Session["Categoryname"]);
                customId = documentsModel.SecondaryID;
            }

            var repodata = _bblAssociateService.FindByDocNameBasedonCategoryName(categoryname.Trim()).ToList().OrderBy(x => x.ShortDocName);
            categoryDocuments = repodata.Select(i => new DocumentsModel().InjectFrom(i)).Cast<DocumentsModel>().ToList();
            foreach (var item in categoryDocuments)
            {
                item.PrimaryID = customId;
                item.ActivityID = activityId;
                item.InitialLicense = item.InitialLicense == "Y" ? "Yes" : "No";
                item.PostLicensure = item.PostLicensure == "Y" ? "Yes" : "No";
                item.Renewal = item.Renewal == "Y" ? "Yes" : "No";
            }
            return PartialView("BBL/_DocumentsGridPartial", categoryDocuments);
        }
        /// <summary>
        /// This method is used to update document
        /// </summary>
        /// <param name="documentid"></param>
        /// <param name="otherParam"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult UpdateDocuments(int? documentid, string otherParam)
        {
            DocumentsModel documentsModel = new DocumentsModel();
            var repodata = _bblAssociateService.FindByDocBasedonDocId(Convert.ToInt32(documentid)).First();
            documentsModel.InjectFrom(repodata);
            ViewBag.IsDocumentUpdate = otherParam;
            return View(documentsModel);
        }
        /// <summary>
        /// This method is used to update Documents
        /// </summary>
        /// <param name="documentsModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult UpdateDocuments(DocumentsModel documentsModel)
        {
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                    MasterCategoryDocumentModel masterCategoryDocumentModel = new MasterCategoryDocumentModel();
                    masterCategoryDocumentModel.InjectFrom(documentsModel);
                    var response = _bblAssociateService.InsertUpdateCategoryDocuments(masterCategoryDocumentModel);
                    if (response == 2)
                    {
                        return RedirectToAction("UpdateDocuments", "BBL", new
                        {
                            categoryId = documentsModel.PrimaryID,
                            type = "primary",
                            activityId = documentsModel.ActivityID,
                            documentid = documentsModel.MasterCategoryDocId,
                            otherParam = "true"
                        });
                    }
                    return View(documentsModel);
                }
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Update Documents", ex); 
            }
        }
        /// <summary>
        /// This method is used to Delete the Documents
        /// </summary>
        /// <param name="documentmodel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        //   [ConcurrentLogin]
        public ActionResult DeletePrimaryDocuments(MasterCategoryDocumentModel documentmodel)
        {
            if (_businesscentercommon.ValidateSessionResult())
            {
                try
                {
                    var response = _bblAssociateService.DeleteCategoryDocument(documentmodel);
                    if (response)
                    {
                        return JavaScript(" $('#modelerror').css('color','green');$('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Documents  Deactivated Successfully');");
                    }
                    return RedirectToAction("Documents");
                }
                catch (Exception ex)
                {
                    throw new Exception("Exception occurs in Delete Primary Documents", ex); 
                }
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            { return Json(new { status = "SessionExipred" }); }
        }

        #endregion Documents

        #region Categories Fees

       /// <summary>
       /// This method is used to get CategoryFees basedon PrimaryId
       /// </summary>
       /// <param name="aId"></param>
       /// <param name="pId"></param>
       /// <param name="type"></param>
       /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult CategoryFees(string aId, string pId, string type)
        {
            var repodata = _bblprimarycategories.FindByCategoryIDBasedonPrimaryId(pId).First();
            //   Session["Categoryid"] = id;
            //  Session["Categorytype"] = type;
            // var records = _bbladminservice.AllSubCategoryFees().ToList();
            ViewBag.CategoryId = pId;
            ViewBag.CategoryType = type;
            ViewBag.activityid = aId;
            ViewBag.PrimaryName = repodata.Description;
            ViewBag.Unitone = repodata.UnitOne;

            return View();
        }
        /// <summary>
        /// This method is used to Add Categoryfees to the PrimaryCategory
        /// </summary>
        /// <param name="categoryFeeModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        // [ConcurrentLogin]
        public ActionResult CategoryFees(CategoryFeeModel categoryFeeModel)
        {
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                    if (ModelState.IsValid)
                    {
                        OSub_Category_FeesEntity entity = new OSub_Category_FeesEntity();
                        categoryFeeModel.App_Type = categoryFeeModel.App_Type == "Business" ? "B" : categoryFeeModel.App_Type == "Individual" ? "I" : categoryFeeModel.App_Type;
                        entity.InjectFrom(categoryFeeModel);
                        var response = _bblAssociateService.InsertUpdateCategoryFees(entity);
                        var primaryPhysicallocation = new PrimaryPhysicallocation();
                        primaryPhysicallocation.InjectFrom(categoryFeeModel);
                        _feeCodeMapService.InsertUpdateFee(primaryPhysicallocation);
                        if (response == 1)
                        {
                            // return JavaScript("var redirect='insert'; $('.homemodaldiv').modal('show');$('.modal-body .error_message').empty().append('Category Fee added Successfully');");
                            return JavaScript("$('#modelerror').css('color','green');$('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Category Fee added Successfully');");
                        }
                    }
                    return Json(new { status = "success", response = "inserted" });
                }
                else
                { return Json(new { status = "SessionExipred" }); }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Category Fee", ex); 
            }
        }
        /// <summary>
        /// This method is used to display Categoryfees based on CategoryId
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [ConcurrentLogin]
        public PartialViewResult CategoryFeesGridPartial(string type, string id)
        {
            var categoryfees = new List<CategoryFeeModel>();
            // string type = Session["Categorytype"].ToString();
            // TempData["primarysecondarytype"] = type;
            if (type == "primary")
            {
                var response = _bblAssociateService.FindFeesByPrimaryCategory(id);
                categoryfees = response.Select(i => new CategoryFeeModel().InjectFrom(i)).Cast<CategoryFeeModel>().ToList();
            }
            else if (type == "secondary")
            {
                var response = _bblAssociateService.FindFeesBySecondaryCategory(id);
                categoryfees = response.Select(i => new CategoryFeeModel().InjectFrom(i)).Cast<CategoryFeeModel>().ToList();
            }
            return PartialView("BBL/_CategoryFeesGridPartial", categoryfees);
        }
        /// <summary>
        /// This method is used to add Categoryfees
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [ConcurrentLogin]
        public PartialViewResult CategoryFeeAddPartial(string type, string id)
        {
            try
            {
                List<CategoryFeeModel> categoryfees;
                //   string type = Session["Categorytype"].ToString();
                CategoryFeeModel categoryFeeModel = new CategoryFeeModel();
                //CategoryFeeViewModel categoryFeeViewModel = new CategoryFeeViewModel();
                if (type == "primary")
                {
                    var response = _bblAssociateService.FindFeesByPrimaryCategory(id).ToList();
                    if (response.Any())
                    {
                        categoryfees = response.Select(i => new CategoryFeeModel().InjectFrom(i)).Cast<CategoryFeeModel>().ToList();
                        int end = Convert.ToInt32(categoryfees.Last().End) + 1;
                        if (end != 0)
                        {
                            categoryFeeModel.Start = end;
                        }
                        var categoryfeesdata = categoryfees.FirstOrDefault();
                        if (categoryfeesdata != null) categoryFeeModel.Fee_Code = categoryfeesdata.Fee_Code;

                        if ((end - 1) == 99999)
                        {
                           // categoryFeeModel = null;
                            return PartialView("BBL/_CategoryFeeAddPartial", null);
                        }
                    }

                    var findbyprimary = _bblprimarycategories.FindByCategoryID(id).ToList();

                    if (findbyprimary.Any())
                    {
                        
                        // ReSharper disable once RedundantToStringCall
                        string data = findbyprimary.First().Description.ToString();
                        categoryFeeModel.OSub_Description = data;
                        // ReSharper disable once RedundantToStringCall
                        categoryFeeModel.UnitOne = findbyprimary.First().UnitOne.ToString();
                        //     categoryFeeModel.OSub_Category = findbyprimary.First().os .ToString();
                    }
                }
                else if (type == "secondary")
                {
                    var response = _bblAssociateService.FindFeesBySecondaryCategory(id).ToList();

                    if (response.Any())
                    {
                        categoryfees = response.Select(i => new CategoryFeeModel().InjectFrom(i)).Cast<CategoryFeeModel>().ToList();
                        int end = Convert.ToInt32(response.Last().End + 1);
                        if (end != 0)
                        {
                            categoryFeeModel.Start = end;
                        }
                        var feemodel = categoryfees.FirstOrDefault();
                        if (feemodel != null) categoryFeeModel.Fee_Code = feemodel.Fee_Code;
                    }
                    var findbyprimary = _bblsecondarycategories.FindSecondaryId(id).ToList();
                    if (findbyprimary.Any())
                    {
                        // ReSharper disable once RedundantToStringCall
                        string data = findbyprimary.First().SecondaryLicenseCategory.ToString();
                        categoryFeeModel.OSub_Description = data;
                    }
                }
                return PartialView("BBL/_CategoryFeeAddPartial", categoryFeeModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Category Fee Add Partials", ex); 
            }
        }
        /// <summary>
        /// This method is used to Update categoryfees
        /// </summary>
        /// <param name="categoryfeeid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [ConcurrentLogin]
        public PartialViewResult UpdateCategoryFeePartial(string categoryfeeid, string id)
        {
            // TempData["categoryId"] = categoryfeeid;
            ViewBag.CategoryId = categoryfeeid;
            var repodata = _bblAssociateService.FindByCategoryFeeId(categoryfeeid).First();
            var findbyprimary = _bblprimarycategories.FindByCategoryID(id).ToList();

            //  var   = _bblprimarycategories.FindByCategoryIDBasedonPrimaryId(pId).First();
            var categoryFeeModel = new CategoryFeeModel {UnitOne = findbyprimary.First().UnitOne ?? ""};
            categoryFeeModel.InjectFrom(repodata);

            categoryFeeModel.App_Type = categoryFeeModel.App_Type == "B" ? "Business" : categoryFeeModel.App_Type == "I" ? "Individual" : categoryFeeModel.App_Type;
            return PartialView("BBL/_UpdateCategoryFeePartial", categoryFeeModel);
        }
        /// <summary>
        /// This method is used to update Categoryfee based on given inputs
        /// </summary>
        /// <param name="categoryFeeModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        // [ConcurrentLogin]
        public ActionResult UpdateCategoryFee(CategoryFeeModel categoryFeeModel)
        {
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                    if (ModelState.IsValid)
                    {
                        OSub_Category_FeesEntity entity = new OSub_Category_FeesEntity();
                        entity.InjectFrom(categoryFeeModel);
                        entity.OSub_Category = categoryFeeModel.OSub_Category;
                        entity.App_Type = categoryFeeModel.App_Type == "Business" ? "B" : categoryFeeModel.App_Type == "Individual" ? "I" : categoryFeeModel.App_Type;
                        var response = _bblAssociateService.InsertUpdateCategoryFees(entity);
                        var primaryPhysicallocation = new PrimaryPhysicallocation();
                        primaryPhysicallocation.InjectFrom(categoryFeeModel);
                        _feeCodeMapService.InsertUpdateFee(primaryPhysicallocation);
                        if (response == 2)
                        {
                            //  return JavaScript("$('.homemodaldiv').modal('show');$('.modal-body .error_message').empty().append('Category Fee Updated Successfully');");
                            return JavaScript("$('#modelerror').css('color','green'); $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Category Fee Updated Successfully');");
                        }
                    }
                    return Json(new { status = "success" });
                }
                else
                { return Json(new { status = "SessionExipred" }); }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Update Category Fee", ex); 
            }
        }
        /// <summary>
        /// This Method is used to Delete Categoryfee  based on given inputs
        /// </summary>
        /// <param name="categoryFeeModel"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult DeleteCategoryFee(CategoryFeeModel categoryFeeModel)
        {
            try
            {
                //  var resp = _bblsecondarycategories.DeleteSecondaryCategory(slCategoryEntity);
                return RedirectToAction("SecondaryCategories", "BBL");
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Delete Category Fee", ex); 
            }
        }

        #endregion Categories Fees

        #region Submission Master
        /// <summary>
        /// This method is used to display  the counts of Draft and UnderReview Licenses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult ApplicationReview()
        {
            var submissionModel = new SubmissionMasterDetailsViewModel();
            var repodata = _bblsubmissionmaster.GetApplicationReviewCounts();
            submissionModel.DraftlistCount = repodata.DraftlistCount;
            submissionModel.UnderReviewlistCount = repodata.UnderReviewlistCount;
            return View(submissionModel);
        }
        /// <summary>
        /// This method is used to display the User submissions based on given inputs
        /// </summary>
        /// <param name="status"></param>
        /// <param name="userId"></param>
        /// <param name="licenseType"></param>
        /// <returns></returns>
        [Authorize]
        [ConcurrentLogin]
        public PartialViewResult SubmissionMasterGridPartial(string status, string userId, string licenseType)
        {
            try
            {
                string username = string.Empty;
                string key = status.ToUpper();
                BblAsscoiateService bblService = new BblAsscoiateService {UserID = userId};
                var userDetails = _bblAssociateService.FindByAdminId(userId).ToList();
                if(userDetails.Any())
                {
                    var userdetailsdata = userDetails.FirstOrDefault();
                    if (userdetailsdata != null) username = userdetailsdata.UserName;
                }
                var submissiondata = _bblsubmissionmaster.GetBblService(bblService).ToList();
                var userlicenseData = new List<SubmissionMasterDetails>();
                var recs = new List<SubmissionMasterDetails>();
                var submissiondetailsdata = submissiondata.FirstOrDefault();
                if (submissiondetailsdata != null && submissiondetailsdata.BblServiceList.Any())
                {
                    var repodata = submissiondetailsdata.BblServiceList.ToList();
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    if (status != null)
                    {
                        if (status.ToUpper().Trim() == "|S|")
                        {
                            repodata = repodata.Where(x => x.UserAssociateType == "S").ToList();
                            key = "S";
                        }
                        else if (status.ToUpper().Trim() == "|A|")
                        {
                            repodata = repodata.Where(x => x.UserAssociateType == "A").ToList();
                            key = "A";
                        }
                        else if (status.ToUpper().Trim() == "DRAFT")
                        {
                            repodata = repodata.Where(x => x.Status.ToUpper().Trim() == "DRAFT APPLICATION IN PROGRESS").ToList();
                            key = "DRAFT";
                        }
                        else if (status.ToUpper().Trim() == "UNDER REVIEW")
                        {
                            repodata = repodata.Where(x => x.Status.ToUpper().Trim() == "UNDER REVIEW").ToList();
                            key = "UNDER REVIEW";
                        }
                    }
                    if (string.IsNullOrEmpty(userId))
                    {
                        TempData["Redirection"] = "ApplicationReview";
                    }
                    if (status == string.Empty)
                    {
                        foreach (var item in repodata)
                        {
                            SubmissionMasterDetails submissionmasterdetails = new SubmissionMasterDetails
                            {
                                MasterId = (item.MasterId ?? "").Trim(),
                                UserName = username
                            };
                            // submissionmasterdetails.UserName = (item.UserName ?? "").Trim();
                            if (item.LicTypes.ToUpper().Trim() == "MULTIPLE")
                                // ReSharper disable once RedundantToStringCall
                            { submissionmasterdetails.ActivityName = item.MultipleLicense == null ? "" : item.MultipleLicense.ToString().Trim(); }
                            else
                            {
                                // ReSharper disable once RedundantToStringCall
                                submissionmasterdetails.ActivityName = item.LicTypes == null ? "" : item.LicTypes.ToString().Trim();
                            }
                            submissionmasterdetails.App_Type = (item.APP_Type == null ? "" : item.APP_Type.ToUpper().Trim() == "B" ? "Business" : item.APP_Type.ToUpper().Trim() == "I" ? "Individual" : "").Trim();
                            submissionmasterdetails.SubmissionLicense = (item.LicNumber ?? "").Trim();
                            submissionmasterdetails.GrandTotal = item.GrandTotal;
                            submissionmasterdetails.Status = (item.Status ?? "").Trim();
                            submissionmasterdetails.ApplicationSubmitType = item.UserAssociateType ?? "".Trim();
                            recs.Add(submissionmasterdetails);
                        }
                        userlicenseData = recs.ToList();
                        return PartialView("BBL/_SubmissionMasterGridPartial", userlicenseData);
                    }
                    // ReSharper disable once RedundantIfElseBlock
                    else
                    {
                        foreach (var item in repodata)
                        {
                            StringBuilder builder = new StringBuilder();
                            // ReSharper disable once RedundantToStringCall
                            builder.Append(item.LicNumber == null ? "" : item.LicNumber.ToString());
                            // ReSharper disable once RedundantToStringCall
                            builder.Append(item.UserName == null ? "" : item.UserName.ToString());
                            builder.Append(item.APP_Type == null ? "" : item.APP_Type == "B" ? "Business" : item.APP_Type == "I" ? "Individual" : "");
                            if (item.LicTypes.ToUpper().Trim() == "MULTIPLE")
                                // ReSharper disable once RedundantToStringCall
                            { builder.Append(item.MultipleLicense == null ? "" : item.MultipleLicense.ToString()); }
                            else
                            {
                                // ReSharper disable once RedundantToStringCall
                                builder.Append(item.LicTypes == null ? "" : item.LicTypes.ToString());
                            }
                            // ReSharper disable once RedundantToStringCall
                            builder.Append(item.Status == null ? "" : item.Status.ToString());
                            // builder.Append(item.UserAssociateType == null ? "" : item.UserAssociateType.ToString());
                            if (status.ToUpper().Trim() == "|S|" || status.ToUpper().Trim() == "|A|")
                            {
                                // ReSharper disable once UseObjectOrCollectionInitializer
                                SubmissionMasterDetails submissionmasterdetails = new SubmissionMasterDetails();
                                submissionmasterdetails.MasterId = (item.MasterId ?? "").Trim();
                                submissionmasterdetails.UserName = (item.UserName ?? "").Trim();
                                if (item.LicTypes.ToUpper().Trim() == "MULTIPLE")
                                    // ReSharper disable once RedundantToStringCall
                                { submissionmasterdetails.ActivityName = item.MultipleLicense == null ? "" : item.MultipleLicense.ToString().Trim(); }
                                else
                                {
                                    // ReSharper disable once RedundantToStringCall
                                    submissionmasterdetails.ActivityName = item.LicTypes == null ? "" : item.LicTypes.ToString().Trim();
                                }
                                submissionmasterdetails.App_Type = (item.APP_Type == null ? "" : item.APP_Type == "B" ? "Business" : item.APP_Type == "I" ? "Individual" : "").Trim();
                                submissionmasterdetails.SubmissionLicense = (item.LicNumber ?? "").Trim();
                                submissionmasterdetails.GrandTotal = item.GrandTotal;
                                submissionmasterdetails.Status = (item.Status ?? "").Trim();
                                submissionmasterdetails.ApplicationSubmitType = item.UserAssociateType ?? "".Trim();
                                recs.Add(submissionmasterdetails);
                            }
                            else
                                if (builder.ToString().ToLower().Contains(key.ToLower()))
                                {
                                    // ReSharper disable once UseObjectOrCollectionInitializer
                                    SubmissionMasterDetails submissionmasterdetails = new SubmissionMasterDetails();
                                    submissionmasterdetails.MasterId = (item.MasterId ?? "").Trim();
                                    submissionmasterdetails.UserName = (item.UserName ?? "").Trim();
                                    if (item.LicTypes.ToUpper().Trim() == "MULTIPLE")
                                        // ReSharper disable once RedundantToStringCall
                                    { submissionmasterdetails.ActivityName = item.MultipleLicense == null ? "" : item.MultipleLicense.ToString().Trim(); }
                                    else
                                    {
                                        // ReSharper disable once RedundantToStringCall
                                        submissionmasterdetails.ActivityName = item.LicTypes == null ? "" : item.LicTypes.ToString().Trim();
                                    }
                                    submissionmasterdetails.App_Type = (item.APP_Type == null ? "" : item.APP_Type == "B" ? "Business" : item.APP_Type == "I" ? "Individual" : "").Trim();
                                    submissionmasterdetails.SubmissionLicense = (item.LicNumber ?? "").Trim();
                                    submissionmasterdetails.GrandTotal = item.GrandTotal;
                                    submissionmasterdetails.Status = (item.Status ?? "").Trim();
                                    submissionmasterdetails.ApplicationSubmitType = item.UserAssociateType ?? "".Trim();
                                    recs.Add(submissionmasterdetails);
                                }
                            userlicenseData = recs.ToList();
                        }
                    }
                }
                return PartialView("BBL/_SubmissionMasterGridPartial", userlicenseData);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Submission Master Grid", ex); 
            }
        }

        #endregion Submission Master

        #region Information Verfication

        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult SubmissionInformationDetails(string masterid)
        {
            string userId = Convert.ToString(TempData["UserId"]);
            ViewBag.UserId = userId;

            //var repodata = _bblsubmissionmaster.FindByEntityID(submissionLicense);
            //string masterid = repodata.First().MasterId;
            SubmissionVerfication submissionVerfication = new SubmissionVerfication {MasterID = masterid};
            DocumentsModel documentsModel = new DocumentsModel
            {
                BblDocumentsList = _bblAssociateService.SubmissionDetails(submissionVerfication)
            };

            ServiceChecklist serviceChecklist = new ServiceChecklist {MasterId = masterid};
            documentsModel.Servicechecklist = _submissionCategoryService.ServiceCheckList(serviceChecklist);
            documentsModel.MasterId = masterid;

            TempData["UserId"] = userId;
            return View(documentsModel);
        }

        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult InformationVerification(string masterid)
        {
            string userId = string.Empty;
            var repodata = _bblsubmissionmaster.FindByMasterID(masterid.Trim()).ToList();
            if (repodata.Any())
            {
                var respositoryData = repodata.FirstOrDefault();
                if (respositoryData != null) userId = respositoryData.UserID;
            }

            string licno = string.Empty;
            if (ViewData["LicNumber"] != null)
                licno = ViewData["LicNumber"].ToString();

            SubmissionVerfication submissionVerfication = new SubmissionVerfication {MasterID = masterid};
            DocumentsModel documentsModel = new DocumentsModel
            {
                BblDocumentsList = _bblAssociateService.SubmissionDetails(submissionVerfication)
            };
            //  documentsModel.LicenseNumber = licno;
            ServiceChecklist serviceChecklist = new ServiceChecklist {MasterId = masterid};
            documentsModel.Servicechecklist = _submissionCategoryService.ServiceCheckList(serviceChecklist);

            ViewData["LicNumber"] = licno;

            documentsModel.UserId = userId;

            return View(documentsModel);
        }

        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult ViewCheckList(string masterId)
        {
            string userId = Convert.ToString(TempData["UserId"]);
            ViewBag.UserId = userId;
            BblDocuments bblDocuments = new BblDocuments {MasterId = masterId};
            var commandata = _bblAssociateService.DocumentList(bblDocuments).ToList();
            var validateResult = _subIndividualService.ValidateSubmission(masterId);
            foreach (var item in commandata)
            {
                item.IsIndividual = validateResult;
            }

            TempData["UserId"] = userId;
            return View(commandata);
        }

        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult ViewReceipt(string masterId)
        {
            string userId = string.Empty;
            var repodata = _bblsubmissionmaster.FindByMasterID(masterId.Trim()).ToList();
            if (repodata.Any())
            {
                var repositoryData = repodata.FirstOrDefault();
                if (repositoryData != null) userId = repositoryData.UserID;
            }
            ViewBag.PatientId = masterId.Trim();
          //  string paymentId = string.Empty;
            // ReSharper disable once UseObjectOrCollectionInitializer
            PaymentDetails paymentDetails = new PaymentDetails();
            paymentDetails.MasterId = masterId;
            paymentDetails = _bblAssociateService.FindByPaymentID(paymentDetails).FirstOrDefault();
            if (paymentDetails != null)
            {
                // ReSharper disable once UseObjectOrCollectionInitializer
                ReceiptModel receiptModel = new ReceiptModel();
                receiptModel.MasterID = masterId;
                receiptModel.PaymentID = paymentDetails.PaymentId;
                receiptModel.EmailAddress = paymentDetails.PaymentMailAddress;
                receiptModel = _bblAssociateService.GetReceiptData(receiptModel);
                receiptModel.UserId = userId;
                receiptModel.SubmitTypeFrom = paymentDetails.PaymentFrom ?? "";
                receiptModel.GrandTotals = (receiptModel.TotalFee).ToString("#,##0.00");
                if (repodata.Any())
                {
                    var repositoryData = repodata.FirstOrDefault();
                    if (repositoryData != null) receiptModel.ApplicationStatus = repositoryData.Status;
                }
               
                //receiptModel.ExtraAmount=paymentDetails.
                return View(receiptModel);
            }
            return View();
        }

        #endregion Information Verfication
        /// <summary>
        /// This method is used to display the Particular user Submissions
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult CustomerSubmissions(string userId)
        {
            SubmissionMasterDetailsViewModel viewmodel = new SubmissionMasterDetailsViewModel();
            // ReSharper disable once UseObjectOrCollectionInitializer
            BblAsscoiateService bblService = new BblAsscoiateService();
            bblService.UserID = userId;
            var submissiondata = _bblsubmissionmaster.GetBblService(bblService).ToList();
            var userlicenseData = new List<SubmissionMasterDetails>();
            var submissiondetailsdata = submissiondata.FirstOrDefault();
            if (submissiondetailsdata != null && submissiondetailsdata.BblServiceList.Any())
            {
                var repodata = submissiondetailsdata.BblServiceList.ToList();

                var recs = new List<SubmissionMasterDetails>();

                foreach (var item in repodata)
                {
                    // ReSharper disable once UseObjectOrCollectionInitializer
                    SubmissionMasterDetails submissionmasterdetails = new SubmissionMasterDetails();
                    submissionmasterdetails.MasterId = (item.MasterId ?? "").Trim();
                    submissionmasterdetails.UserName = (item.UserName ?? "").Trim();
                    viewmodel.UserName = submissionmasterdetails.UserName;
                    if (item.LicTypes.ToUpper().Trim() == "MULTIPLE")
                        // ReSharper disable once RedundantToStringCall
                    { submissionmasterdetails.ActivityName = item.MultipleLicense == null ? "" : item.MultipleLicense.ToString().Trim(); }
                    else
                    {
                        // ReSharper disable once RedundantToStringCall
                        submissionmasterdetails.ActivityName = item.LicTypes == null ? "" : item.LicTypes.ToString().Trim();
                    }
                    submissionmasterdetails.App_Type = (item.APP_Type == null ? "" : item.APP_Type == "B" ? "Business" : item.APP_Type == "I" ? "Individual" : "").Trim();
                    submissionmasterdetails.SubmissionLicense = (item.LicNumber ?? "").Trim();
                    submissionmasterdetails.GrandTotal = item.GrandTotal;
                    submissionmasterdetails.Status = (item.Status ?? "").Trim();
                    submissionmasterdetails.ApplicationSubmitType = item.UserAssociateType ?? "".Trim();
                    recs.Add(submissionmasterdetails);

                    userlicenseData = recs.ToList();
                }
            }
            var username = _bblAssociateService.FindByAdminId(userId);
            var enumerable = username as User[] ?? username.ToArray();
            if (username != null && enumerable.Any())
            {
                var firstOrDefault = enumerable.FirstOrDefault();
                if (firstOrDefault != null) viewmodel.UserName = firstOrDefault.UserName;
                
            }

            viewmodel.Draftlist = userlicenseData.Where(x => x.Status.ToUpper().Trim() == "DRAFT APPLICATION IN PROGRESS").ToList();
            viewmodel.UnderReviewlist = userlicenseData.Where(x => x.Status.ToUpper().Trim() == "UNDER REVIEW").ToList();
            viewmodel.Licenselist = userlicenseData.Where(x => x.ApplicationSubmitType.ToUpper().Trim() == "S").ToList();
            viewmodel.Renewlist = userlicenseData.Where(x => x.ApplicationSubmitType.ToUpper().Trim() == "A").ToList();
            //  viewmodel.Licenselist = userlicenseData.ToList();
            viewmodel.DraftlistCount = viewmodel.Draftlist.Count();
            viewmodel.UnderReviewlistCount = viewmodel.UnderReviewlist.Count();
            viewmodel.LicenseCount = viewmodel.Licenselist.Count();
            viewmodel.RenewCount = viewmodel.Renewlist.Count();
            viewmodel.LicenseType = "";
            TempData["UserId"] = userId;
           

            return View(viewmodel);
        }
        /// <summary>
        /// This method is used to Download the Documents
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Download(string fileId)
        {
            if (_businesscentercommon.ValidateSessionResult())
            {
                // ReSharper disable once RedundantToStringCall
                string drive = ConfigurationManager.AppSettings["UploadDrive"].ToString();
                // ReSharper disable once RedundantToStringCall
                string uploadFolder = ConfigurationManager.AppSettings["UploadFolder"].ToString();
                string currentFileName = drive + uploadFolder + fileId;

                byte[] bytes = System.IO.File.ReadAllBytes(currentFileName);

                bytes = ManipulatePdf(bytes, fileId);
                var contentType = "application/pdf";

                return File(bytes, contentType);
            }
            else
            { return RedirectToAction("SessionExpiry", "Account"); }
        }

        public byte[] ManipulatePdf(byte[] src, string fileName)
        {
            PdfReader reader = new PdfReader(src);
            PdfReader.unethicalreading = true;
            using (MemoryStream ms = new MemoryStream())
            {
                using (PdfStamper stamper = new PdfStamper(reader, ms))
                {
                    Dictionary<String, String> info = reader.Info;
                    info["Title"] = fileName;

                    stamper.MoreInfo = info;
                }
                return ms.ToArray();
            }
        }

        #region Submission To Accel
        /// <summary>
        /// This method returns Queue page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult Queue()
        {
            return View();
        }
        /// <summary>
        /// This method is used to  display SubmissionAccelaDetails
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [Authorize]
        [ConcurrentLogin]
        public PartialViewResult SubmissionAccelaApplicationsPartial(string searchText)
        {
            List<SubmissiontoAccelaDetails> submissionsearchdata = new List<SubmissiontoAccelaDetails>();
            var accelaSubmissions = (from s in _submissiontoAccelaService.SubmissionsAccelaDetails()
                                     join sl in _bblsubmissionmaster.GetAllSubmissionMaster() on s.LicenseNumber equals sl.SubmissionLicense
                                     join userDetails in _bblsubmissionmaster.GetUserDetails() on sl.UserID equals userDetails.Id
                                     join userBblAssociation in _bblsubmissionmaster.UserBblAssociationService() on sl.SubmissionLicense equals userBblAssociation.SubmissionLicense
                                     where sl.SubmissionLicense == userBblAssociation.SubmissionLicense && sl.UserID == userBblAssociation.UserID
                                     select new
                                     {
                                         sl.SubmissionLicense,
                                         sl.Updatedate,
                                         s.AccelaGeneratedID,
                                         s.AllDocumentsUpdated,
                                         s.ApplicationCompleted,
                                         s.ApplicationCreated,
                                         s.ApplicationFeeMatched,
                                         s.EHOPCreated,
                                         s.LicenseNumber,
                                         s.SubmissiontoAccelaId,
                                         s.RenewalFeeMatched,
                                         s.RenewalPaymentUpdated,
                                         s.ProcessCompleted,
                                         userDetails.FirstName,
                                         userDetails.LastName,
                                         userBblAssociation.Type,
                                         sl.MasterId
                                     }).OrderByDescending(x => x.SubmissiontoAccelaId).ToList();
            //if (!string.IsNullOrEmpty(searchText))
            //{
            //    accelaSubmissions = accelaSubmissions.Where(x => x.LicenseNumber.Contains(searchText.Trim())).ToList();
            //}

            foreach (var acceladetails in accelaSubmissions)
            {
                StringBuilder builder = new StringBuilder();
                // ReSharper disable once RedundantToStringCall
                builder.Append(acceladetails.AccelaGeneratedID == null ? "" : acceladetails.AccelaGeneratedID.ToString().ToUpper());
                builder.Append("δ");
                builder.Append(acceladetails.Updatedate == null ? "" : Convert.ToDateTime(acceladetails.Updatedate).ToString("MM/dd/yyyy").ToUpper());
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append((acceladetails.FirstName == null ? "" : acceladetails.FirstName.ToString().ToUpper()) + " " + (acceladetails.LastName == null ? "" : acceladetails.LastName.ToString().ToUpper()));
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(acceladetails.LicenseNumber == null ? "" : acceladetails.LicenseNumber.ToString().ToUpper());
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(acceladetails.Type == null ? "" : acceladetails.Type.ToString().ToUpper());
                builder.Append("δ");
                if (builder.ToString().ToUpper().Contains(searchText.Trim().ToUpper()))
                {
                    SubmissiontoAccelaDetails submissiondetails = new SubmissiontoAccelaDetails();
                    submissiondetails.SubmissiontoAccelaId = acceladetails.SubmissiontoAccelaId;
                    submissiondetails.ApplicationCompleted = acceladetails.ApplicationCompleted;
                    submissiondetails.ApplicationCreated = acceladetails.ApplicationCreated;
                    submissiondetails.ApplicationFeeMatched = acceladetails.ApplicationFeeMatched;
                    submissiondetails.RenewalPaymentUpdated = acceladetails.RenewalPaymentUpdated;
                    submissiondetails.RenewalFeeMatched = acceladetails.RenewalFeeMatched;
                    submissiondetails.AllDocumentsUpdated = acceladetails.AllDocumentsUpdated;
                    submissiondetails.EhopCreated = acceladetails.EHOPCreated;
                    submissiondetails.AccelaGeneratedID = acceladetails.AccelaGeneratedID;
                    submissiondetails.ProcessCompleted = acceladetails.ProcessCompleted;
                    submissiondetails.CreatedDate = Convert.ToDateTime(acceladetails.Updatedate).ToString("MM/dd/yyyy");
                    submissiondetails.FullName = acceladetails.FirstName + " " + acceladetails.LastName;
                    submissiondetails.LicenseNumber = acceladetails.LicenseNumber;
                    submissiondetails.Type = acceladetails.Type.Trim();
                    submissiondetails.MasterId = acceladetails.MasterId;
                    submissionsearchdata.Add(submissiondetails);
                }
            }
            submissionsearchdata = submissionsearchdata.ToList();
            return PartialView("BBL/_SubmissionAccelaApplicationsPartial", submissionsearchdata);
        }

        [Authorize]
        [ConcurrentLogin]
        public string GetLrenNumberFromRenewal(string entityId)
        {
            return _bblAssociateService.GetRenewalLicenseNumber(entityId);
        }
        /// <summary>
        /// This method is used to display the BusinessLicense Details
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [Authorize]
        [ConcurrentLogin]
        public PartialViewResult BusinessLicensePartial(string searchText)
        {
            var submissionData = new List<BusinessLicense>();
            var businesslicense = _businessInformationService.GetSubmissionData().ToList();

            foreach (var licensedetails in businesslicense)
            {
                StringBuilder builder = new StringBuilder();
                // ReSharper disable once RedundantToStringCall
                builder.Append(licensedetails.CreatedDate == null ? "" : licensedetails.CreatedDate.ToString().ToUpper()).Replace("-", "/");
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(licensedetails.FullName == null ? "" : licensedetails.FullName.ToString().ToUpper());
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(licensedetails.GrandTotal == null ? "" : licensedetails.GrandTotal.ToString().ToUpper());
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(licensedetails.LicenseNumber == null ? "" : licensedetails.LicenseNumber.ToString().ToUpper());
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(licensedetails.LicesnseType == null ? "" : licensedetails.LicesnseType.ToString().ToUpper());
                builder.Append("δ");
                builder.Append(licensedetails.PaymentDate == null ? "" : Convert.ToDateTime(licensedetails.PaymentDate).ToString("MM/dd/yyyy").ToUpper());
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(licensedetails.PaymentTransaction == null ? "" : licensedetails.PaymentTransaction.ToString().ToUpper());
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(licensedetails.Status == null ? "" : licensedetails.Status.ToString().ToUpper());
                builder.Append("δ");

                if (builder.ToString().ToUpper().Contains(searchText.ToUpper().Trim()))
                {
                    submissionData.Add(licensedetails);
                }
            }
            //if(!string.IsNullOrEmpty(searchText))
            //{
            //    submissionData = submissionData.Where(x => x.LicenseNumber.Contains(searchText.Trim())).ToList();
            //}
            return PartialView("BBL/_BusinessLicensePartial", submissionData.ToList());
        }
        /// <summary>
        /// This method is used to Particular BusinessLicense Details
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult BusinessLicenseDetails(string appId)
        {
            BusinessLicense businessLicense = new BusinessLicense {MasterId = appId};
            var submissionData =
                _businessInformationService.FindByLicenseNumber(businessLicense).ToList().FirstOrDefault();
            // ReSharper disable once PossibleNullReferenceException
            submissionData.Online_App_License_Status = submissionData.Online_App_License_Status == "UnderReview" ? "Under Review" : submissionData.Online_App_License_Status;
            return View(submissionData);
        }
        /// <summary>
        /// This method is used to show the RenewalLicenses
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [Authorize]
        [ConcurrentLogin]
        public PartialViewResult RenewalsLicensePartial(string searchText)
        {
            var submissionData = new List<RenewalLicense>();
            var renewallicense = _renewalView3Service.GetRenewData().ToList();
            foreach (var licensedetails in renewallicense)
            {
                StringBuilder builder = new StringBuilder();
                // ReSharper disable once RedundantToStringCall
                builder.Append(licensedetails.CategoryName == null ? "" : licensedetails.CategoryName.ToString().ToUpper());
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(licensedetails.CreatedDate == null ? "" : licensedetails.CreatedDate.ToString().ToUpper()).Replace("-", "/");
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(licensedetails.FullName == null ? "" : licensedetails.FullName.ToString().ToUpper());
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(licensedetails.LicenseNumber == null ? "" : licensedetails.LicenseNumber.ToString().ToUpper());
                builder.Append("δ");
                builder.Append(licensedetails.PaymentDate == null ? "" : Convert.ToDateTime(licensedetails.PaymentDate).ToString("MM/dd/yyyy").ToUpper());
                builder.Append("δ");

                if (builder.ToString().ToUpper().Contains(searchText.ToUpper().Trim()))
                {
                    submissionData.Add(licensedetails);
                }
            }
            //if (!string.IsNullOrEmpty(searchText))
            //{
            //    submissionData = submissionData.Where(x => x.LicenseNumber.Contains(searchText.Trim())).ToList();
            //}
            return PartialView("BBL/_RenewalsLicensePartial", submissionData.ToList());
        }

        // public PartialViewResult BusinessLicenseDetailPartial(string masterid)
        //{
        //    BusinessLicense businessLicense =new BusinessLicense();
        //    businessLicense.MasterId = masterid;
        //    var submissionData = _businessInformationService.FindByLicenseNumber(businessLicense).ToList();
        //    return PartialView("BBL/_RenewalsPartial", submissionData);
        //}
        /// <summary>
        /// This method is used to show the SubmissionDocumets
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [Authorize]
        [ConcurrentLogin]
        public PartialViewResult SubmittedDocumentPartial(string searchText)
        {
            List<BblLicenseView4> documentlist = new List<BblLicenseView4>();
            var submissionData = _documentsView4Service.GetBblLicenseView4().ToList();
            foreach (var documentdetails in submissionData)
            {
                StringBuilder builder = new StringBuilder();
                // ReSharper disable once RedundantToStringCall
                builder.Append(documentdetails.Agency == null ? "" : documentdetails.Agency.ToString().ToUpper());
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(documentdetails.Agency_FullName == null ? "" : documentdetails.Agency_FullName.ToString().ToUpper());
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(documentdetails.Application_License_No_ == null ? "" : documentdetails.Application_License_No_.ToString().ToUpper());
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(documentdetails.CategoryName == null ? "" : documentdetails.CategoryName.ToString().ToUpper());
                builder.Append("δ");
                builder.Append(documentdetails.Created_Date == null ? "" : documentdetails.Created_Date.ToString().ToUpper()).Replace("-", "/");
                builder.Append("δ");

                // ReSharper disable once RedundantToStringCall
                builder.Append(documentdetails.Description == null ? "" : documentdetails.Description.ToString().ToUpper());
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(documentdetails.Div == null ? "" : documentdetails.Div.ToString().ToUpper());
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(documentdetails.DivisionFullName == null ? "" : documentdetails.DivisionFullName.ToString().ToUpper());
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(documentdetails.DocumentSubmissionType == null ? "" : documentdetails.DocumentSubmissionType.ToString().ToUpper());
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(documentdetails.FileName == null ? "" : documentdetails.FileName.ToString().ToUpper());
                builder.Append("δ");

                builder.Append(documentdetails.MasterCategoryDocId == null ? "" : documentdetails.MasterCategoryDocId.ToString().ToUpper());
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(documentdetails.ShortDocName == null ? "" : documentdetails.ShortDocName.ToString().ToUpper());
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(documentdetails.SubmissionType == null ? "" : documentdetails.SubmissionType.ToString().ToUpper());
                builder.Append("δ");
                // ReSharper disable once RedundantToStringCall
                builder.Append(documentdetails.SupportingDocuments == null ? "" : documentdetails.SupportingDocuments.ToString().ToUpper());
                builder.Append("δ");
                builder.Append(documentdetails.UpDated_Date == null ? "" : Convert.ToDateTime(documentdetails.UpDated_Date).ToString("MM/dd/yyyy").ToUpper());

                if (builder.ToString().ToUpper().Contains(searchText.ToUpper().Trim()))
                {
                    documentlist.Add(documentdetails);
                }
            }
            //if(!string.IsNullOrEmpty(searchText))
            //{
            //    submissionData = submissionData.Where(x => x.Application_License_No_.Contains(searchText.Trim())).ToList();
            //}
            return PartialView("BBL/_SubmittedDocumentPartial", documentlist.ToList());
        }
        /// <summary>
        /// This method is used to Particular RenewalLicense Details
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult RenewalLicenseDetails(string appId)
        {
            RenewalLicense businessLicense = new RenewalLicense {MasterId = appId};
            var submissionData = _renewalView3Service.FindByLicenseNumber(businessLicense).FirstOrDefault();
            return View(submissionData);
        }
        /// <summary>
        /// This method is used to show  the Particular SubmissionDocument Details
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult DocumentDetails(string appId)
        {
            DocumentData documentData = new DocumentData {ApplicationNo = appId};
            var submissionData =
                _documentsView4Service.FindByFileNumber(documentData).ToList().FirstOrDefault();
            return View(submissionData);
        }
        /// <summary>
        /// This method is used to display the BusinessCompareData Details
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult BusinessComapreData(string appId)
        {
            BusinessLicenseCompare businessComapare = new BusinessLicenseCompare();
            BusinessLicenseFromTables businesslicensetable = new BusinessLicenseFromTables();
            BusinessLicense businessLicense = new BusinessLicense {MasterId = appId};
            var viewData = _businessInformationService.FindByLicenseNumber(businessLicense).ToList().FirstOrDefault();
            if (viewData != null)
            {
                viewData.Online_App_License_Status = viewData.Online_App_License_Status == "UnderReview"
                    ? "Under Review"
                    : viewData.Online_App_License_Status;
            }
            businessComapare.BusinessView = viewData;
            businesslicensetable.ApplicationUniqueID = appId;
            //submissionmaster details
            var submissionMaster = _bblsubmissionmaster.FindByMasterID(appId).ToList();
            if (submissionMaster.Any())
            {
                var masterDetails = submissionMaster.FirstOrDefault();

                // user details
                // ReSharper disable once PossibleNullReferenceException
                var userdetails = _bblAssociateService.FindByAdminId((masterDetails.UserID ?? string.Empty).Trim()).ToList();
                if (userdetails.Any())
                {
                    var userdetailsData = userdetails.FirstOrDefault();
                    if (userdetailsData != null)
                        businesslicensetable.FullName = (userdetailsData.FirstName ?? string.Empty).Trim() + " " + (userdetailsData.LastName ?? string.Empty).Trim();
                }
                businesslicensetable.ApplicantName = masterDetails.BusinessName;
                businesslicensetable.OnlineAppLicenseStatus = masterDetails.Status;
                businesslicensetable.OnlineAppLicenseStatus = businesslicensetable.OnlineAppLicenseStatus == "UnderReview" ? "Under Review" : businesslicensetable.OnlineAppLicenseStatus;
                businesslicensetable.ApplicationLicenseNo = masterDetails.SubmissionLicense;
                //submission verfication details
                SubmissionVerfication submissionVerficataion = new SubmissionVerfication {MasterID = appId};
                var submissionverficationdetails = _bblAssociateService.SubmissionDetails(submissionVerficataion);
                //submission category details
                var categoryDetails = submissionverficationdetails.ServiceCheckList;
                if (categoryDetails.Any())
                {
                    // ReSharper disable once PossibleNullReferenceException
                    businesslicensetable.ApplicationType = categoryDetails.FirstOrDefault().LicenseCategory;
                    businesslicensetable.ApplicationFee = Convert.ToDecimal(categoryDetails.Sum(x => x.ApplicationFee));
                    businesslicensetable.EndorsementFee = Convert.ToDecimal(categoryDetails.Sum(x => x.EndorsementFee));
                    businesslicensetable.LicenseFee = Convert.ToDecimal(categoryDetails.Sum(x => x.CategoryLicenseFee));
                    businesslicensetable.ESFFee = Convert.ToDecimal(categoryDetails.Sum(x => x.TechFee));
                }
                businesslicensetable.App_Type = (masterDetails.App_Type ?? string.Empty).Trim();
                businesslicensetable.LicensePeriod = Convert.ToInt32(masterDetails.LicenseDuration);
                businesslicensetable.RAOFee = Convert.ToDecimal(masterDetails.RAOFee);
                businesslicensetable.EHOPFees = Convert.ToDecimal(submissionverficationdetails.Isehop ? "72.60" : "0.00");
                businesslicensetable.TotalAmount = Convert.ToDecimal(masterDetails.GrandTotal);
                //payment details
                //ReceiptModel receiptModel = new ReceiptModel();
                //receiptModel.MasterID = appId;
                //var paymentdetails = _bblAssociateService.GetReceiptData(receiptModel);

                // submission tax and revenue details
                var taxrevenuedetails = _submissiontaxrevenueservice.FindByID(appId).ToList();
                if (taxrevenuedetails.Any())
                {
                    var taxdetails = taxrevenuedetails.FirstOrDefault();
                    if (taxdetails != null)
                    {
                        businesslicensetable.CHSelfCertificateSignature = (taxdetails.FullName ?? string.Empty).Trim();
                        businesslicensetable.CHSelfCertificateDate = Convert.ToDateTime(taxdetails.CreatdedDate).ToString("MM/dd/yyyy");
                        businesslicensetable.CHSelfCertificateType = (taxdetails.BusinessOwnerRoles ?? string.Empty).Trim();
                        businesslicensetable.TaxIDNumber = (taxdetails.TaxRevenueNumber ?? string.Empty).Trim();
                    }
                }
                //self certification details
                // ReSharper disable once UseObjectOrCollectionInitializer
                SubmissionSelfCertification submissionSelfCertification = new SubmissionSelfCertification();
                submissionSelfCertification.MasterId = appId;
                var selfcertificationdetails = _submissionselfcertification.GetSelfCertificationOnMasterId(submissionSelfCertification).ToList();
                // ReSharper disable once PossibleNullReferenceException
                businesslicensetable.SelfCertificateOneFamily = selfcertificationdetails.Any() ? (selfcertificationdetails.FirstOrDefault().FullName ?? string.Empty).Trim() : string.Empty;
                //cofo details
                var servicechecklist = _bblAssociateService.FindByMasterId(appId).ToList();
                if (servicechecklist.Any())
                {
                    var checklist = servicechecklist.FirstOrDefault();
                    if (checklist != null && Convert.ToBoolean( checklist.IsSubmissionCofo))
                    {
                        businesslicensetable.CertificateofOccupancyNumber = (submissionverficationdetails.OccupanyNumber ?? string.Empty).Trim();
                        // businesslicensetable.COIssueDate = submissionverficationdetails.DateofIssue;
                    }
                    else
                    {
                        businesslicensetable.CertificateofOccupancyNumber = string.Empty;
                        //  businesslicensetable.COIssueDate = string.Empty;
                    }
                    //hop details
                    if (checklist != null && Convert.ToBoolean(checklist.IsSubmissionHop))
                    {
                        businesslicensetable.HomeOccupationNumber = (submissionverficationdetails.OccupanyNumber ?? string.Empty).Trim();
                        //  businesslicensetable.HOIssueDate = submissionverficationdetails.DateofIssue;
                    }
                    else
                    {
                        businesslicensetable.HomeOccupationNumber = string.Empty;
                        //  businesslicensetable.HOIssueDate = string.Empty;
                    }
                    //ehopdata details
                    if (checklist != null && Convert.ToBoolean(checklist.IsSubmissioneHop))
                    {
                        businesslicensetable.EHOPNumber = (submissionverficationdetails.OccupanyNumber ?? string.Empty).Trim();
                        businesslicensetable.EHOPIssueDate = submissionverficationdetails.DateofIssue;
                        businesslicensetable.SEHOPAttestedBy = (masterDetails.BusinessName ?? string.Empty).Trim();
                        //businesslicensetable.FullName;
                        EhopData ehopdata = new EhopData {MasterID = appId};
                        var ehopeligibility = _bblAssociateService.EhopData(ehopdata);
                        businesslicensetable.EHOPOccupationType = (ehopeligibility.OccupationType ?? string.Empty).Trim();
                        businesslicensetable.EHOPIssuanceStatus = "Issued";
                    }
                    else
                    {
                        businesslicensetable.EHOPNumber = string.Empty;
                        businesslicensetable.EHOPIssueDate = string.Empty;
                        businesslicensetable.SEHOPAttestedBy = string.Empty;
                        businesslicensetable.EHOPOccupationType = string.Empty;
                        businesslicensetable.EHOPIssuanceStatus = string.Empty;
                    }
                }
                businesslicensetable.FirstName = (submissionverficationdetails.HeadQFirstName ?? "").Trim();
                //if (businesslicensetable.FirstName == "")
                //{
                //    businesslicensetable.FirstName = (masterDetails.BusinessName ?? string.Empty).Trim();
                //}
                businesslicensetable.LastName = (submissionverficationdetails.HeadQLastName ?? string.Empty).Trim();
                businesslicensetable.MiddleName = (submissionverficationdetails.HeadQMiddleName ?? string.Empty).Trim();
                businesslicensetable.OrganizationName = (submissionverficationdetails.HeadQBName ?? string.Empty).Trim();
                businesslicensetable.FullAddressLine1 = (submissionverficationdetails.HeadQAddressNumber ?? string.Empty).Trim();
                businesslicensetable.AddressLine2 = (submissionverficationdetails.HeadQStreetName ?? string.Empty).Trim();
                businesslicensetable.City = (submissionverficationdetails.HeadQCity ?? string.Empty).Trim();
                businesslicensetable.State = (submissionverficationdetails.HeadQState ?? string.Empty).Trim();
                businesslicensetable.ZipCode = (submissionverficationdetails.HeadQZip ?? string.Empty).Trim();
                businesslicensetable.CountryRegion = (submissionverficationdetails.HeadQCountry ?? string.Empty).Trim();
                businesslicensetable.Email = (submissionverficationdetails.HeadQEmail ?? string.Empty).Trim();
                businesslicensetable.BusinessOrganization = (masterDetails.BusinessStructure ?? string.Empty).Trim();
                businesslicensetable.TradeNameIfapplicable = (masterDetails.TradeName ?? string.Empty).Trim();

                //user bbl details
                var userbbldetails = _bblAssociateService.CheckUserBBL(businesslicensetable.ApplicationLicenseNo, masterDetails.UserID).ToList();
                if (userbbldetails.Any())
                {
                    // ReSharper disable once PossibleNullReferenceException
                    businesslicensetable.FEINSSN = (userbbldetails.FirstOrDefault().CleanHandsType_SSN_FEIN ?? string.Empty).Trim();
                }
                businesslicensetable.PrimStreetNumber = (submissionverficationdetails.PremiseAddressNumber ?? string.Empty).Trim();
                businesslicensetable.PrimStreetName = (submissionverficationdetails.PremiseStreetName ?? string.Empty).Trim();
                businesslicensetable.Street_Type = (submissionverficationdetails.PremiseStreetType ?? string.Empty).Trim();
                businesslicensetable.Quadrant = (submissionverficationdetails.PremiseQuadrant ?? string.Empty).Trim();
                businesslicensetable.UnitType = (submissionverficationdetails.PremiseUnitType ?? string.Empty).Trim();
                businesslicensetable.SuiteUnitNumber = (submissionverficationdetails.PremiseUnit ?? string.Empty).Trim();
                businesslicensetable.PCity = (submissionverficationdetails.PremiseCity ?? string.Empty).Trim();
                businesslicensetable.PState = (submissionverficationdetails.PremiseState ?? string.Empty).Trim();
                businesslicensetable.Country = (submissionverficationdetails.PremiseCountry ?? string.Empty).Trim();
                businesslicensetable.PZip = (submissionverficationdetails.PremiseZip ?? string.Empty).Trim();
                businesslicensetable.Phone = (submissionverficationdetails.PremiseTelePhone ?? string.Empty).Trim();
                businesslicensetable.PremiseAddressVerified = masterDetails.IsBusinessMustbeinDC.ToString();
                businesslicensetable.ParcelPremiseSuffix = (submissionverficationdetails.PremiseAddressNumberSufix ?? string.Empty).Trim();
                businesslicensetable.ParcelPremiseWard = (submissionverficationdetails.PremiseWard ?? string.Empty).Trim();
                businesslicensetable.ParcelPremiseANC = (submissionverficationdetails.PremiseANC ?? string.Empty).Trim();
                businesslicensetable.ParcelPremiseZone = (submissionverficationdetails.PremiseZone ?? string.Empty).Trim();
                businesslicensetable.ParcelPremiseSsl = (submissionverficationdetails.PremiseSsl ?? string.Empty).Trim();
                //payment address details
                // billing details
                var billingdetails = _bblAssociateService.FindAddressByPaymentId(appId);
                businesslicensetable.PaymentTransactionID = (billingdetails.PaymentTransactionID ?? string.Empty).Trim();
                businesslicensetable.PaymentTransactionDate = billingdetails.PaymentTransactionDate;

                var submissionMasterApplicationCheckList = servicechecklist.FirstOrDefault();
                if (submissionMasterApplicationCheckList != null && Convert.ToBoolean( submissionMasterApplicationCheckList.IsSubmissioneHop))
                { businesslicensetable.EHOPIssueDate = businesslicensetable.PaymentTransactionDate; }
                var getGillingAddress = _bblAssociateService.SubmissionCorporation_Agent_ByMasterId(appId.Trim()).ToList();
                if (getGillingAddress != null && getGillingAddress.Any())
                {
                    var getMailingAddress = getGillingAddress.FirstOrDefault();

                    if (getMailingAddress != null)
                    {
                        var billingaddress =
                            _bblAssociateService.MailAddresses(getMailingAddress.SubCorporationRegId, "NEWMAIL").ToList();
                        if (billingaddress.Any())
                        {
                            var maillingAddressDetails = billingaddress.FirstOrDefault();

                            businesslicensetable.BillingOrgBussName = maillingAddressDetails.BusinessName.Trim();
                            businesslicensetable.BillingContactFirstName = (maillingAddressDetails.FirstName.Trim() ?? string.Empty).Trim();

                            businesslicensetable.BillingContactLastName = (maillingAddressDetails.LastName.Trim() ?? string.Empty).Trim();
                            businesslicensetable.BillingContactMiddleName = (maillingAddressDetails.MiddelName.Trim() ?? string.Empty).Trim();
                            businesslicensetable.BillingStreetNumber = (maillingAddressDetails.Address2.Trim() ?? string.Empty).Trim();
                            businesslicensetable.BillingStreetName = (maillingAddressDetails.Address1.Trim() ?? string.Empty).Trim();

                            businesslicensetable.BillingStreetType = (maillingAddressDetails.Address3.Trim() ?? string.Empty).Trim();
                            businesslicensetable.BillingQuadrant = (maillingAddressDetails.Quadrant.Trim() ?? string.Empty).Trim();
                            businesslicensetable.BillingSuiteUnitNumber = (maillingAddressDetails.UnitNumber.Trim() ?? string.Empty).Trim();
                            businesslicensetable.BillingCity = (maillingAddressDetails.City.Trim() ?? string.Empty).Trim();

                            businesslicensetable.BillingState = _bblAssociateService.StateCode((maillingAddressDetails.State.Trim() ?? string.Empty), (maillingAddressDetails.Country ?? string.Empty).Trim());
                            businesslicensetable.BillingCountry = _bblAssociateService.GetCountryFullName((maillingAddressDetails.Country.Trim() ?? string.Empty).Trim());
                            businesslicensetable.BillingZip = (maillingAddressDetails.ZipCode.Trim() ?? string.Empty).Trim();
                            businesslicensetable.BillingPhone = (maillingAddressDetails.Telephone.Trim() ?? string.Empty).Trim();
                            businesslicensetable.BillingEmail = (maillingAddressDetails.Email.Trim() ?? string.Empty).Trim();
                        }
                    }
                }

                //if (billingdetails.PaymentAddressDetails.Any())
                //{
                //    var billingpaymentdetails = billingdetails.PaymentAddressDetails.FirstOrDefault();
                //    businesslicensetable.BillingOrgBussName = (billingpaymentdetails.BusinessName ?? string.Empty).Trim();
                //    //  businesslicensetable.BillingContactFirstName = (billingpaymentdetails.ContactFirstName ?? string.Empty).Trim();
                //    if (billingpaymentdetails.ContactFirstName == "")
                //    {
                //        businesslicensetable.BillingContactFirstName =
                //            (masterDetails.BusinessName ?? string.Empty).Trim();
                //    }
                //    else
                //    {
                //        businesslicensetable.BillingContactFirstName = (billingpaymentdetails.ContactFirstName ?? string.Empty).Trim();
                //    }
                //    businesslicensetable.BillingContactLastName = (billingpaymentdetails.ContactLastName ?? string.Empty).Trim();
                //    businesslicensetable.BillingContactMiddleName = (billingpaymentdetails.ContactMiddleName ?? string.Empty).Trim();
                //    businesslicensetable.BillingStreetNumber = (billingpaymentdetails.StreetNumber ?? string.Empty).Trim();
                //    businesslicensetable.BillingStreetName = (billingpaymentdetails.StreetName ?? string.Empty).Trim();
                //    businesslicensetable.BillingStreetType = (billingpaymentdetails.StreetType ?? string.Empty).Trim();
                //    businesslicensetable.BillingQuadrant = (billingpaymentdetails.Quadrant ?? string.Empty).Trim();
                //    businesslicensetable.BillingSuiteUnitNumber = (billingpaymentdetails.UnitNumber ?? string.Empty).Trim();
                //    businesslicensetable.BillingCity = (billingpaymentdetails.City ?? string.Empty).Trim();
                //    businesslicensetable.BillingState = _bblAssociateService.StateCode((billingpaymentdetails.State ?? string.Empty), (billingpaymentdetails.Country ?? string.Empty).Trim());
                //    businesslicensetable.BillingCountry = _bblAssociateService.GetCountryFullName((billingpaymentdetails.Country ?? string.Empty).Trim());
                //    businesslicensetable.BillingZip = (billingpaymentdetails.Zip ?? string.Empty).Trim();
                //    businesslicensetable.BillingPhone = (billingpaymentdetails.ContactNumber1 ?? string.Empty).Trim();
                //    businesslicensetable.BillingEmail = (billingpaymentdetails.EmailAddress ?? string.Empty).Trim();
                //}

                //var getGillingAddress = _bblAssociateService.SubmissionCorporation_Agent_ByMasterId(appId.Trim());
                //if (getGillingAddress != null && getGillingAddress.Any())
                //{
                //    var getMailingAddress = getGillingAddress.FirstOrDefault();

                //    var billingaddress =
                //        _bblAssociateService.MailAddresses(getMailingAddress.SubCorporationRegId, "NEWMAIL");
                //    if (billingaddress.Any())
                //    {
                //        var maillingAddressDetails = billingaddress.FirstOrDefault();
                //        basicBusinessLicense.BillingCompanyName = maillingAddressDetails.BusinessName.Trim();
                //        basicBusinessLicense.BillingName = (maillingAddressDetails.FirstName == null ? "" : maillingAddressDetails.FirstName == "NA" ? "" :
                //         maillingAddressDetails.FirstName.Trim()) + " " +
                //        (maillingAddressDetails.MiddelName == null ? "" : maillingAddressDetails.MiddelName == "NA" ? "" :
                //        maillingAddressDetails.MiddelName.Trim()) + " " +
                //       (maillingAddressDetails.LastName == null ? "" : maillingAddressDetails.LastName == "NA" ? "" :
                //        maillingAddressDetails.LastName.Trim());

                //        basicBusinessLicense.BillingAddress1 = (maillingAddressDetails.Address1 ?? "").Trim();
                //        basicBusinessLicense.BillingAddress2 = (maillingAddressDetails.Address2 ?? "").Trim();
                //        basicBusinessLicense.BillingAddress3 = (maillingAddressDetails.Address1 ?? "").Trim();
                //        basicBusinessLicense.BillingAddress = ((maillingAddressDetails.City ?? "").Trim() + " " +
                //                                             GetStateCode((maillingAddressDetails.State ?? "").Trim(), (maillingAddressDetails.Country ?? "").Trim()) + " " +
                //                                             GetCountryFullName((maillingAddressDetails.Country ?? "").Trim()).Replace("United States", "") + " " +
                //                                              (maillingAddressDetails.ZipCode ?? "").Trim()).Replace("  ", " ");
                //    }
                //}

                //agent details
                businesslicensetable.AgentOrgBussName = (submissionverficationdetails.AgentBName ?? string.Empty).Trim();
                businesslicensetable.ContactFirstName = (submissionverficationdetails.AgentFirstName ?? string.Empty).Trim();
                businesslicensetable.ContactLastName = (submissionverficationdetails.AgentLastName ?? string.Empty).Trim();
                businesslicensetable.ContactMiddleName = (submissionverficationdetails.AgentMiddleName ?? string.Empty).Trim();
                businesslicensetable.StreetFullAddress = (submissionverficationdetails.AgentAddress ?? string.Empty).Trim();

                businesslicensetable.StreetName = submissionverficationdetails.AgentAddressNumber != "" ? (submissionverficationdetails.AgentAddressNumber ?? string.Empty).Trim() : (submissionverficationdetails.AgentStreetName ?? string.Empty).Trim();

                businesslicensetable.StreetType = (submissionverficationdetails.AgentStreetType ?? string.Empty).Trim();
                businesslicensetable.AgentQuadrant = (submissionverficationdetails.AgentQuadrant ?? string.Empty).Trim();
                businesslicensetable.AgentUnitNumber = (submissionverficationdetails.AgentUnit ?? string.Empty).Trim();
                businesslicensetable.ContactAgent_City = (submissionverficationdetails.AgentCity ?? string.Empty).Trim();
                businesslicensetable.ContactAgent_State = (submissionverficationdetails.AgentState ?? string.Empty).Trim();
                businesslicensetable.ContactAgent_Country = (submissionverficationdetails.AgentCountry ?? string.Empty).Trim();
                businesslicensetable.ContactAgent_ZipCode = (submissionverficationdetails.AgentZip ?? string.Empty).Trim();
                businesslicensetable.CorpTelephone = (submissionverficationdetails.AgentTelePhone ?? string.Empty).Trim();
                businesslicensetable.ContactAgent_Email = (submissionverficationdetails.AgentEmail ?? string.Empty).Trim();
                businesslicensetable.FileNumber = (submissionverficationdetails.CorpFileNo ?? string.Empty).Trim();

                //inidvidual details

                // ReSharper disable once UseObjectOrCollectionInitializer
                ChecklistModel checklistModel = new ChecklistModel();
                checklistModel.MasterId = appId;
                var individualdetails = _submissionindividualservice.GetSubmissionIndividualData(checklistModel).ToList();
                if (individualdetails.Any())
                {
                    var submissionindividual = individualdetails.FirstOrDefault();
                    businesslicensetable.CompanyName = (submissionindividual.CompanyName ?? string.Empty).Trim();
                    businesslicensetable.EmployeeName = (submissionindividual.FirstName ?? string.Empty).Trim();
                    businesslicensetable.CompanyBussLicNumber = (submissionindividual.CompanyBusinessLicense ?? string.Empty).Trim();
                    businesslicensetable.DateofBirth = submissionindividual.DateofBirth;
                    businesslicensetable.PlaceofBirth = (submissionindividual.City ?? string.Empty) + ", " + (_bblAssociateService.StateCode((submissionindividual.State_Province ?? string.Empty), (submissionindividual.Country ?? string.Empty)))
                        + ", " + (_bblAssociateService.GetCountryFullName(submissionindividual.Country ?? string.Empty));
                    businesslicensetable.Height = (submissionindividual.Height ?? string.Empty).Trim() + "-" + (submissionindividual.HeightIn ?? string.Empty).Trim();
                    businesslicensetable.Weight = (submissionindividual.Weight ?? string.Empty).Trim();
                    businesslicensetable.HairColor = (submissionindividual.HairColor ?? string.Empty).Trim();
                    businesslicensetable.EyesColor = (submissionindividual.EyeColor ?? string.Empty).Trim();
                    businesslicensetable.DriversLicense = (submissionindividual.IdentificationCard ?? string.Empty).Trim();
                    businesslicensetable.StateofLicense = (submissionindividual.StateofIssuance ?? string.Empty).Trim();
                    businesslicensetable.IndividualExpirationDate = submissionindividual.ExpirationDate;
                }

                businessComapare.BusinessApplication = businesslicensetable;
            }
            return View(businessComapare);
        }

        #endregion Submission To Accel

        #region Error Messages
        /// <summary>
        /// This method returns the error page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult Errors()
        {
            return View();
        }
        /// <summary>
        /// This method  is used to displays the content  error messages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public PartialViewResult ErrorMessagesPartial()
        {
            var repodata = _portalContentErrorsService.GetAllContentErrors();
            var contentErrorMessages = repodata.Select(item => new PortaContentErrorsModel
            {
                MessageId = item.MessageId,
                MessageType = item.MessageType,
                ShortName = item.ShortName,
                ErrrorMessage = item.ErrrorMessage,
                IsActive = item.IsActive
            }).ToList();

            return PartialView("BBL/_ErrorMessagesPartial", contentErrorMessages);
        }
        /// <summary>
        /// This method returns the Error page for Adding  ErrorMessages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult CreateError()
        {
            return View();
        }
        /// <summary>
        /// This method is used to Create Error Messages
        /// </summary>
        /// <param name="portalContentModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult CreateError(PortaContentModel portalContentModel)
        {
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                    if (ModelState.IsValid)
                    {
                        var portaContentErrorsModel = new PortaContentErrorsModel();
                        portaContentErrorsModel.InjectFrom(portalContentModel);
                        var response = _portalContentErrorsService.InsertUpdateContentErrors(portaContentErrorsModel);
                        if (response)
                        {
                            ModelState.Clear();
                            ViewBag.IsCreateError = true;
                            return View();
                        }
                    }

                    return View();
                }
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Create Error", ex); 
            }
        }
        /// <summary>
        /// This method is used to update Error
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult UpdateError(string messageId)
        {
            PortaContentModel portalContentModel = new PortaContentModel();
            if (messageId != null)
            {
                var portaContentErrorsModel = new PortaContentErrorsModel {MessageId = messageId};
                var repodata = _portalContentErrorsService.FindByMessageId(portaContentErrorsModel).First();
                portalContentModel.InjectFrom(repodata);
            }
            return View(portalContentModel);
        }
        /// <summary>
        /// This method is used to update the Error based on given inputs
        /// </summary>
        /// <param name="portalContentModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ConcurrentLogin]
        public ActionResult UpdateError(PortaContentModel portalContentModel)
        {
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                    if (ModelState.IsValid)
                    {
                        var portaContentErrorsModel = new PortaContentErrorsModel();
                        portaContentErrorsModel.InjectFrom(portalContentModel);
                        var response = _portalContentErrorsService.InsertUpdateContentErrors(portaContentErrorsModel);
                        if (response)
                        {
                            ViewBag.IsUpadateError = true;
                            return View(portalContentModel);
                        }
                    }
                    return View();
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                { return RedirectToAction("SessionExpiry", "Account"); }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Update Error", ex); 
            }
        }

        #endregion Error Messages
        /// <summary>
        /// This method is used to Delete the Error message
        /// </summary>
        /// <param name="contentErrorsModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        // [ConcurrentLogin]
        public ActionResult DeleteContentErrorMessages(PortaContentErrorsModel contentErrorsModel)
        {
            try
            {
                if (_businesscentercommon.ValidateSessionResult())
                {
                    var resp = _portalContentErrorsService.DeleteContentErrors(contentErrorsModel);
                    if (resp)
                    {
                        if (contentErrorsModel.IsActive == false)
                        {
                            return JavaScript(" $('#modelerror').css('color','green');$('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Error Message is Deactivated Successfully');");
                        }
                        // ReSharper disable once RedundantIfElseBlock
                        else if (contentErrorsModel.IsActive == true)
                        {
                            return JavaScript("$('#modelerror').css('color','green'); $('.registermodaldiv').modal('show');$('.modal-body .error_message').empty().append('Error Message is Activated Successfully');");
                        }
                    }

                    return Json(new { status = "success" });
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                { return Json(new { status = "SessionExipred" }); }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurs in Delete Content Error Message", ex); 
            }
        }
    }
}