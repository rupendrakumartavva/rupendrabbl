

(function () {

    'use strict';
    var serviceId = "requestService";
    angular.module('DCRA').service(serviceId, ['$http', 'appConstants', requestService]);

    function requestService($http, appConstants) {

        

        var baseUrl = appConstants.apiServiceBaseUri;

        //this.getErrors = function () {
        //    $http.get('js/errormessages.json').then(function (resp) {
        //        console.log(resp);
        //    }, function (resp) {
        //        console.log(resp);
        //    });
        //}

        //this.getErrors();

        this.GetQuestions = function () {
            return $http.get(baseUrl + 'api/UserAccounts/Questions');
        }

        this.UserLogin = function (userCredentials) {
            return $http.post(baseUrl + 'api/Account/Login', userCredentials);
        }

        this.createUser = function (registrationDetails) {
            return $http.post(baseUrl + 'api/UserAccounts/create', registrationDetails);
        }

        this.UserLogout = function (data) {
            return $http.post(baseUrl + 'api/Account/Logout', data);
        }

        this.validateRecaptcha = function (responseValue) {
            return $http.post(baseUrl + 'api/UserAccounts/UserReCaptcha', responseValue);
        }

        this.EmailConfirmation = function (verifyEmail) {
            return $http.post(baseUrl + 'api/UserAccounts/ConfirmEmail', verifyEmail);
        }

        this.ForgotPassword = function (data) {
            return $http.post(baseUrl + 'api/Account/ForgotValidation', data);
            //  return $http.post(baseUrl + 'api/Account/ForgotPassword', data);
        }

        this.NewForgotPassword = function (data) {
            return $http.post(baseUrl + 'api/Account/NewForgotPassword', data);
        }

        this.NewPassword = function (data) {
            return $http.post(baseUrl + 'api/Account/ConfirmForgotPassword', data);
        }

        this.checkUserNameAvailabilty = function (name) {
            return $http.post(baseUrl + 'api/UserAccounts/CheckUser', name);
        }

        this.UserDetails = function (id) {
            return $http.post(baseUrl + 'api/UserAccounts/UserDetails', id);
        }

        this.UpdateUserProfile = function (profileinfo) {
            return $http.post(baseUrl + 'api/UserAccounts/ProfileUpdate', profileinfo);
        }

        this.deleteUser = function (deleteuser) {
            return $http.post(baseUrl + 'api/UserAccounts/delete', deleteuser);
        }

        this.resendEmail = function (emaildata) {
            return $http.post(baseUrl + 'api/UserAccounts/ResendMail', emaildata);
        }

        this.UserEmailvalidation = function (emaildata) {
            return $http.post(baseUrl + 'api/Account/ForgotUserName', emaildata);
        }

        this.getUsername = function (questions) {
            return $http.post(baseUrl + 'api/Account/ForgotUserNameValidation', questions);
        }

        this.checkEmailAvailability = function (email) {
            return $http.post(baseUrl + 'api/UserAccounts/checkemail', email);
        }

        this.checkEmailAvailabilityInProfile = function (email) {
            return $http.post(baseUrl + 'api/UserAccounts/CheckUserEmailProfile', email);
        }

        this.GetSearchData = function (Total) {
            return $http.post(baseUrl + 'api/Search/SelectAll', Total);
        }
        this.GetData = function (searchtype) {
            return $http.post(baseUrl + 'api/Search/All', searchtype);
        }
        this.GetSaveCountData = function (searchInput) {
            return $http.post(baseUrl + 'api/MySavedResults/SelectAll', searchInput);
        }
        this.GetSaveData = function (searchtype) {
            return $http.post(baseUrl + 'api/MySavedResults/All', searchtype);
        }

        this.SearchData = function (Opla) {
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");
            return $http.post(baseUrl + 'api/Search/All', Opla);
        }
        this.SearchAll = function (All) {
            return $http.post(baseUrl + 'api/Search/All', All);
        }

        this.GetFilterData = function (FilterData) {
            return $http.post(baseUrl + 'api/Search/FilterData', FilterData);
        }
        this.UserPreviousEmailConfirmation = function (profileUserPreviousEmail) {
            return $http.post(baseUrl + 'api/UserAccounts/UserPreviousEmail', profileUserPreviousEmail);
        }
        this.AddToMyList = function (data) {
            return $http.post(baseUrl + 'api/MySavedResults/AddToMyList', data);
        }

        this.ValidatePassword = function (password) {
            return $http.post(baseUrl + 'api/UserAccounts/ValidatePassword', password);
        }

        this.checkPreviousPasswords = function (password) {
            return $http.post(baseUrl + 'api/Account/ValidatePassword', password);
        }

        this.GetUserServiceCount = function (userService) {
            return $http.post(baseUrl + 'api/MySavedResults/mycount', userService);
        }
        this.removeSelected = function (items) {
            return $http.post(baseUrl + 'api/MySavedResults/MultipleDelete', items);
        }
        this.removeAll = function (userid) {
            return $http.post(baseUrl + 'api/MySavedResults/DeleteAll', userid);
        }
        this.Deletesingle = function (userobj) {
            return $http.post(baseUrl + 'api/MySavedResults/Deletesingle', userobj);
        }

        this.Autofill = function (searchKey, name) {
            return $http.post(baseUrl + 'api/Search/' + name, searchKey);
        }
        this.PasswordCheck = function (data) {
            return $http.post(baseUrl + 'api/Account/ForgetPasswordCheck', data);
        }
        this.traceTimeOut = function () {
            return $http.post(baseUrl + 'api/Account/TraceTimeout');
        }
        //this.getCurrentStepQeustions = function (step, data) {
        //    return $http.get(baseUrl + 'api/Account' + step, data);
        //}
        //BBL Section Starts here

        //Service Name Binding BBL data

        this.BblServiceList = function (data) {
            return $http.post(baseUrl + 'api/BBLAssociation/BblServiceList', data);
        }

        // Service is Used for Remove the BBLs

        this.BblRemoveServiceList = function (data) {
            return $http.post(baseUrl + 'api/BBLAssociation/BblRemoveServiceList', data);
        }
        // Service  is used for validate PInNumbers
        this.validateLicense = function (data) {
            return $http.post(baseUrl + 'api/BBLAssociation/CheckAssociate', data);
        }
        //Service is Used for associate the BBLs
        this.AssociateBblService = function (data) {
            return $http.post(baseUrl + 'api/BBLAssociation/AssociateBblService', data);
        }
        // Service is Used for getting the BUsiness Activities

        this.getCurrentStepQeustions = function (step, data) {
            if (step == "BusinessActivities")
                return $http.get(baseUrl + 'api/BBLCategory/' + step);
            else
                return $http.post(baseUrl + 'api/BBLCategory/' + step, data);
        }

        this.GetSubIndividuals = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/GetSubIndividuals', data);
        }
        this.getApplicationData = function (data) {
            return $http.post(baseUrl + 'api/BBLCategory/ApplicationSubmission', data);
        }

        this.BblRequiredDocuments = function (data) {
            return $http.post(baseUrl + 'api/BBLAssociation/BblRequiredDocuments', data);
        }

        this.SubmissionIndividualBusiness = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/SubIndividual', data);
        }
        this.ServiceCheckList = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/ServiceCheckList', data);
        }
        this.updateSubmissionStatus = function (data) {
            return $http.post(baseUrl + 'api/BBLAssociation/SubmissionUpdate', data);
        }
        this.ValidateIndiduval = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/ValidateIndiduval', data);
        }


        this.validateTaxRevenue = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/TaxValidation', data);
        }
        this.SubmittTaxandRevenue = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/SubmissionTaxRevenu', data);
        }
        this.CorporationSearchFind = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/BusinessDataList', data);
        }
        this.getTaxRevenuData = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/TaxRevenueNumber', data);
        }
        this.StreetTypeAhead = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/LocationVerifier', data);
        }
        this.searchZone = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/CofoHopDetails', data);
        }

        this.GetSubmissionCofoHop = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/SubCofoHopdetl', { masterId: data });
        }

        this.GetMasterEhop = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/GetMasterEhop', data);
        }

        this.MasterEhopEligibility = function (data) {
            return $http.Post(baseUrl + 'api/BBLApplication/GetEHopWithMasterId', data);
        }


        this.FetchedDataToDb = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/SubmitCofoHop', data);
        }


        this.CofoHopformData = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/CofoHop', data);
        }
        this.SubmitCorpAgent = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/SubmitCorpAgent', data);
        }
        this.GetHQA = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/HeadQuarterAddress', data);
        }
        this.GetPriAdd = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/PrimisessAddress', data);
        }

        this.Ehopeligible = function (data) {
            return $http.post(baseUrl + 'api/BBLAssociation/EhopEligibility', data);
        }
        this.SubmitHAorPA = function (data) {
            return $http.post(baseUrl + 'api/BBLAssociation/SubmitHAorPA', data);
        }

        this.GetCorporationDetails = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/GetCorpDetails', data);
        }

        this.InfoVerification = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/VerficationDetails', data);
        }

        this.ValidateEhop = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/ValidateEhop', data);
        }
        this.removeUpladedFile = function (data) {
            return $http.post(baseUrl + 'api/BBLAssociation/DeleteDocument', data);
        }

        this.submitPayment = function (data) {
            return $http.post(baseUrl + 'api/BBLAssociation/SubmitPayment', data);
        }
        this.GetAgentDetails = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/GetCorpAgent', data);
        }
        this.getPaymentDetails = function (data) {
            return $http.post(baseUrl + 'api/BBLAssociation/GetReceiptDetails', data);
        }

        this.GetMailType = function (data) {
            return $http.post(baseUrl + 'api/BBLAssociation/GetMailType', data);
        }
        this.SaveWhenNoCofo = function (data) {
            return $http.post(baseUrl + 'api/BBLAssociation/RemoveCofo', data);
        }
        this.SubmitHop = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/SubmitHop', data);
        }
        this.ValidateLicenceNum = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/ValidateLicenceNum', data);
        }
        this.UpdateCorpStatus = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/UpdateCorpStatus', data);
        }


        //Renewal Services are Strating here 

        this.GetrenuwalLicense = function (data) {
            return $http.post(baseUrl + 'api/Renew/GetRenewalData', data);
        }
        this.confirmRenuwal = function (data) {
            return $http.post(baseUrl + 'api/Renew/RenewalStatus', data);
        }
        this.CheckDocumentStatus = function (data) {
            return $http.post(baseUrl + 'api/Renew/CheckDocumentStatus', data);
        }
        this.UpdateRenwalDocumentType = function (data) {
            return $http.post(baseUrl + 'api/Renew/UpdateRenwalDocumentType', data);
        }
        //this.RenuwalValidation = function (data) {
        //    return $http.post(baseUrl + 'api/Renew/', data);
        //}

        this.updatenoneEhop = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/UpdateEhopNon', data);
        }

        this.UserBblExpCount = function (data) {
            return $http.post(baseUrl + 'api/BBLAssociation/UserBblExpCount', data);
        }

        this.streetsDropDown = function () {
            return $http.get(baseUrl + 'api/BBLApplication/binddropdown');
        }

        this.SubmissionIndividualDelete = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/subinddelete', data);
        }

        this.SubmissionStatus = function (data) {
            return $http.post(baseUrl + 'api/BBLAssociation/SubmissionStatus', data);
        }

        this.deleteRenewalData = function (data) {
            return $http.post(baseUrl + 'api/Renew/RemoveRenewalData', data);
        }
        this.PdfFileDownload = function (data) {
            return $http.post(baseUrl + 'api/DownloadFile/PdfFileDownload', data);
        }

        //New Service added for deletion of ehopAddress

        this.DeleteEhopAddress = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/DeleteEhopAddress', data);
        }
        this.CorpNotRegEmptyHqAddress = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/EmptyHeadQuarterAddress', data);
        }
        this.SubmissionTaxRevenuDel = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/SubmissionTaxRevenuDel', data);
        }

        this.GetPaymentDetailsFromVerification = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/VerficationPayDetails', data);
        }

        this.getSubmissionMasterWithbbl = function (data) {
            return $http.post(baseUrl + 'api/BBLAssociation/GetMasterBasedonUserAssociateId', data);
        }

        this.renewalPayment = function (data) {
            return $http.post(baseUrl + '', data);
        }

        this.UserServiceDetailsOnId = function (data) {
            return $http.post(baseUrl + 'api/BBLAssociation/UserServiceDetailsOnId', data);
        }

        this.getSelfCertificationDetailsByMasterId = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/GetSelftCertification', data);
        }

        this.submitSelfCertificationDetails = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/SelftCertificationInsert', data);
        }
        this.deleteSelfCertification = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/SelftCertificationDel', data);
        }
        this.BblFulladdress = function (data) {
            return $http.post(baseUrl + 'api/Renew/fulladdress', data);
        }

        this.deleteRefreshToken = function (data) {
            return $http.post(baseUrl + 'api/RefreshToken/deleterefresh', data);
        }
        this.getRenewalCleanhandsData = function (data) {
            return $http.post(baseUrl + 'api/Renew/GetTaxRevenue', data);
        }
        this.renewTaxValidation = function (data) {
            return $http.post(baseUrl + 'api/Renew/RenewTaxValidation', data);
        }
        this.checkAmount = function (data) {
            return $http.post(baseUrl + 'api/Renew/CheckAmount', data);
        }
        this.getDocuments = function (data) {
            return $http.post(baseUrl + 'api/Renew/CheckDocuments', data);
        }

        this.getStateList = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/StateListBasedOnCode', data);
        }
        this.Pdfdownloadnow = function (val) {
            return $http.get(baseUrl + 'api/Download/abc', { headers: { "testval": val } });
        }

        this.RenewalCorporationSearchFind = function (data) {
            return $http.post(baseUrl + 'api/BBLApplication/CorpOnlineSearch', data);
        }
        this.ApplicationChcekListPdf = function (data) {
            return $http.post(baseUrl + 'api/Download/applicationChecklist_GeneratedDocument_Angular', data, { responseType: 'arraybuffer' });
        }

        //StateListBasedOnCode
    }

})();