describe('renewalPayment Controller Spec', function () {
    var $scope, controller, httpBackend, mockservice, renewalPaymentcontroller, localStore, appconstants, basePath, renewalfac, routeparams,compile,errorfactory;
    var paymentDetails = { "MasterId": "a54d4fdb-5332-4983-b176-652a715964de", "EntityId": "10049456", "LicenseNumber": "LREN15018910", "UserId": "C4C26C13-5881-4060-8A87-CBC6B1DB9CA9", "GrandTotalAmount": 690.30, "CorpNumber": "C212821", "IsCorp": false, "CorpStatus": "FALSE", "TaxStatus": null, "TaxNumber": null, "SubCategoryName": null, "CategoryName": "One Family Rental", "ActivityName": null, "Endoresement": null, "CategoryId": "3E5B807D-2ADB-4417-B4EF-5368DA34F817", "ActivityId": null, "SubCategoryID": null, "LicenseAmount": 35.00, "EndorsementFee": 25.00, "ApplicationFee": 113.00, "TechFee": 17.30, "RAOFee": 43.00, "IsCorpRegistration": true, "IsCleanHands": false, "DocumentList": [{ "MasterId": "a54d4fdb-5332-4983-b176-652a715964de", "SubmissionId": "10049456", "SubmissionCategoryID": 1, "ApprovedBy": "", "DocRequired": "Letter of Good Standing", "Agency": "DCRA", "Division": "DCRA", "Div": "DCRA", "FileName": "", "FileLocation": "", "DocStatus": "Open", "Description": "DCRA", "Endorsement": "Housing: Residential", "License": "One Family Rental", "CategoryID": "1309", "ShortName": "Corporation", "IsUpload": true, "UploadFileName": "5005_LREN15018910_DCRA_DCRA_Corporation.pdf", "CheckListType": "Document", "CategoryCode": "5005", "LicenseName": "LREN15018910" }], "ServiceCheckList": [], "BblAddress": "132540413THSTNW", "BblCity": "WASHINGTON", "BblState": "DC", "BblZip": "20005", "ExtraAmount": 500.0, "Extradays": "Expired", "RenewalLicenseCode": null, "LrenNumber": "LREN15018910", "UserBblAssociateId": "1", "LicenseDuration": 0, "RenewStatus": "Data", "InitalDocumet": null }
    var doneWithPaymentDetails = { "paymentId": "22abcf0f-4626-4ede-b36f-5fd02fa2ce3f", "trasactionresult": { "Success": true, "Result": 0, "Message": "Test Approval", "Id": "Test-Transaction", "Time": "2015-12-25T15:37:52.8420345+05:30" }, "finalsuccess": "YES", "masterTextRevenue": false, "validateCorpFileStatus": "Active" }
    var dropdownData = ["Alley", "Boulevard", "Bridge", "Circle", "Court", "Crescent", "Drive", "Driveway", "Expressway", "Freeway",
                    "Gardens", "Green", "Lane", "Mews", "Parkway", "Place", "Plaza", "Promenade", "Road", "Row", "Square", "Street", "Terrace", "Walk", "Way"];
    var statesdata = { "Status": [{ "StateCode": "AL", "StateName": "Alaska", "CountryCode": "US" }] };
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants, $routeParams, RenewalUtilityFactory, $compile, errorFactory) {
        rootscope = $rootScope.$new();
        $scope = $rootScope.$new();
        $location = _$location_;
        basePath = appConstants.apiServiceBaseUri;
        spyOn($location, 'path').and.returnValue('test/testpath');
        localStore = (function () {
            var store = {};
            return {
                getItem: function (key) {
                    return store[key];
                },
                setItem: function (key, value) {
                    store[key] = value.toString();
                },
                removeItem: function (key) {
                    delete store[key];
                },
                clear: function () {
                    store = {};
                },
                loggedin: '2'
            };
        })();

        Object.defineProperty(window, 'localStorage', { value: localStore });
        mockservice = requestService;
        httpBackend = $httpBackend;
        controller = $controller;
        renewalfac = RenewalUtilityFactory;
        routeparams = $routeParams;
        routeparams.guid = 'guid';
        compile = $compile;
        appconstants = appConstants;
        errorfactory = errorFactory;
    }));


    describe('if no guid available', function () {

        beforeEach(function () {
            spyOn(renewalfac, 'ifGuidAvailable').and.returnValue(false);
            renewalPaymentcontroller = controller('RenewalPaymentController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, $routeParams: routeparams,
                mockservice: mockservice, appConstants: appconstants,
                renewalFactory: renewalfac, errorFactory: errorfactory
            });
        });

        it('should test init when no guid available', function () {
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });
    })
    

    describe('if guid isavailable', function () {
        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            paymentDetails.CurrentDate = new Date().getMonth() + 1 + "/" + new Date().getDate() + "/" + new Date().getFullYear();
            httpBackend.when('POST', basePath + 'api/Renew/CheckAmount').respond(paymentDetails);
            httpBackend.when('GET', basePath + 'api/BBLApplication/binddropdown').respond(dropdownData);
            httpBackend.when('POST', basePath + 'api/BBLApplication/StateListBasedOnCode').respond(statesdata);
            spyOn(renewalfac, 'ifGuidAvailable').and.returnValue(true);
            renewalPaymentcontroller = controller('RenewalPaymentController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, $routeParams: routeparams,
                mockservice: mockservice, appConstants: appconstants,
                renewalFactory: renewalfac, errorFactory: errorfactory
            });
            $scope.vm = renewalPaymentcontroller;
            var element = angular.element(
                      '<form name="vm.paymentform">' +
                      '<input ng-model="vm.cardexpdate" name="cardexpdate" />' +
                      '<input ng-model="vm.confirmemail" name="confirmemail" />' +
                      '<input ng-model="vm.zip" name="zip" />' +
                      '<input ng-model="vm.telephone" name="telephone" />' +
                      '</form>'
                      );
            compile(element)($scope);
            form = $scope.vm.paymentform;
        });

        it('should test init', function () {
            httpBackend.flush();
            expect(renewalPaymentcontroller.examptAllfee).toBeFalsy();
        });

        it('should test ValidateDate when year is undefined', function () {
            renewalPaymentcontroller.currentMonth = new Date().getMonth() + 1;
            renewalPaymentcontroller.cardexpdate = 22;
            renewalPaymentcontroller.ValidateDate();
            expect(form.cardexpdate.$valid).toBeFalsy();
        });

        it('should test ValidateDate when year is undefined and card expiration month is valid', function () {
            renewalPaymentcontroller.currentMonth = new Date().getMonth() + 1;
            renewalPaymentcontroller.cardexpdate = 11;
            renewalPaymentcontroller.ValidateDate();
            expect(form.cardexpdate.$valid).toBeTruthy();
        });

        it('should test ValidateDate when year is defined and month is valid', function () {
            renewalPaymentcontroller.currentMonth = new Date().getMonth() + 1;
            renewalPaymentcontroller.currentyear = new Date().getFullYear();
            renewalPaymentcontroller.cardexpdate = 11;
            renewalPaymentcontroller.year = 2015;
            renewalPaymentcontroller.ValidateDate();
            expect(form.cardexpdate.$valid).toBeFalsy();
        });

        it('should test ValidateDate when year is valid and month is valid', function () {
            renewalPaymentcontroller.currentMonth = new Date().getMonth() + 1;
            renewalPaymentcontroller.currentyear = new Date().getFullYear();
            renewalPaymentcontroller.cardexpdate = 11;
            renewalPaymentcontroller.year = 2017;
            renewalPaymentcontroller.ValidateDate();
            expect(form.cardexpdate.$valid).toBeTruthy();
        });

        it('should test ValidateDate when year is equal to current year and month is valid', function () {
            renewalPaymentcontroller.currentMonth = new Date().getMonth() + 1;
            renewalPaymentcontroller.currentyear = new Date().getFullYear();
            renewalPaymentcontroller.cardexpdate = 11;
            renewalPaymentcontroller.year = 2016;
            renewalPaymentcontroller.ValidateDate();
            expect(form.cardexpdate.$valid).toBeTruthy();
        });

        it('should test ValidateYear when creditcardclear is defined', function () {
            renewalPaymentcontroller.creditcardclear();
            expect(renewalPaymentcontroller.cardnumber).toBe('');
        });


        it('should test setErrorMsg', function () {
            renewalPaymentcontroller.setErrorMsg();
        });

        it('should test ValidateYear when creditcardclear is defined', function () {
            renewalPaymentcontroller.creditcardclear();
            expect(renewalPaymentcontroller.cardnumber).toBe('');
        });

        it('should test navToMyBBLs', function () {
            renewalPaymentcontroller.navToMyBBLs();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test navigateAnyWay', function () {
            renewalPaymentcontroller.navigateAnyWay();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test navToBblsHome', function () {
            renewalPaymentcontroller.navToBblsHome();
            expect($location.path).toHaveBeenCalledWith('/mybbl');
        });

        it('should test toggleRadio', function () {
            renewalPaymentcontroller.toggleRadio();
            expect(renewalPaymentcontroller.country).toBe('US');
        });

        //it('should test navToReceipt', function () {
        //    renewalPaymentcontroller.navToReceipt();
        //});

        it('should test cancelNavigation', function () {
            renewalPaymentcontroller.cancelNavigation();
        });

        it('should test selectcountryoption method when country is US', function () {
            renewalPaymentcontroller.country = "US";
            renewalPaymentcontroller.selectcountryoption();
            expect(form.zip.$valid).toBeTruthy();
            expect(form.telephone.$valid).toBeTruthy();
            expect(renewalPaymentcontroller.selectStateDropdown).toBeFalsy();
        });

        it('should test selectcountryoption method when country is not in US', function () {
            renewalPaymentcontroller.country = "otherthanus";
            renewalPaymentcontroller.selectcountryoption();
            expect(form.zip.$valid).toBeTruthy();
            expect(form.telephone.$valid).toBeTruthy();
            expect(renewalPaymentcontroller.selectStateDropdown).toBeTruthy();
        });

        it('should test checkTelephoneMaxLength when country is US', function () {
            renewalPaymentcontroller.validations_wrt_contry = errorfactory.isCountryUS(true);
            renewalPaymentcontroller.diffRenewalPayment = {};
            renewalPaymentcontroller.diffRenewalPayment.Telephone = "11";
            renewalPaymentcontroller.checkTelephoneMaxLength();
            expect(form.telephone.$valid).toBeFalsy();

            //telephone length is correct

            renewalPaymentcontroller.diffRenewalPayment.Telephone = "1111111111";
            renewalPaymentcontroller.checkTelephoneMaxLength();
            expect(form.telephone.$valid).toBeTruthy();
        });

        it('should test checkTelephoneMaxLength when country is not US', function () {
            renewalPaymentcontroller.validations_wrt_contry = errorfactory.isCountryUS(false);
            renewalPaymentcontroller.diffRenewalPayment = {};
            renewalPaymentcontroller.diffRenewalPayment.Telephone = "11";
            renewalPaymentcontroller.checkTelephoneMaxLength();
            expect(form.telephone.$valid).toBeTruthy();

            //telephone length is correct

            renewalPaymentcontroller.diffRenewalPayment.Telephone = "1111111111";
            renewalPaymentcontroller.checkTelephoneMaxLength();
            expect(form.telephone.$valid).toBeTruthy();
        });

        it('should test checkZipMaxLength when country is US', function () {
            renewalPaymentcontroller.validations_wrt_contry = errorfactory.isCountryUS(true);
            renewalPaymentcontroller.diffRenewalPayment = {};
            renewalPaymentcontroller.diffRenewalPayment.zip = "11";
            renewalPaymentcontroller.checkZipMaxLength();
            expect(form.zip.$valid).toBeFalsy();

            //zip length is correct
            renewalPaymentcontroller.diffRenewalPayment.zip = "11111";
            renewalPaymentcontroller.checkZipMaxLength();
            expect(form.zip.$valid).toBeTruthy();
        });

        it('should test emailconfirmcase is not undefined', function () {
            renewalPaymentcontroller.emailconfirm = "a@b.co";
            renewalPaymentcontroller.email = "a@b.co";
            renewalPaymentcontroller.emailconfirmcase();
            expect(form.confirmemail.$valid).toBeTruthy();
        });

        it('should test emailconfirmcase is not undefined and both are not equal', function () {
            renewalPaymentcontroller.emailconfirm = "a@ber.co";
            renewalPaymentcontroller.email = "a@b.co";
            renewalPaymentcontroller.emailconfirmcase();
            expect(form.confirmemail.$valid).toBeFalsy();
        });

        it('should test emailconfirmcase is not defined', function () {
            renewalPaymentcontroller.emailconfirmcase();
            expect(form.confirmemail.$valid).toBeTruthy();
        });

        it('should test navToReceipt when form is invalid', function () {
            form.$invalid = true;
            renewalPaymentcontroller.currentpage_errors = {};
            renewalPaymentcontroller.navToReceipt();
        });
        it('should test navToReceipt when form is valid', function () {
            form.$invalid = false;
            renewalPaymentcontroller.diffRenewalPayment = {};
            renewalPaymentcontroller.navToReceipt();
        });

        var submitpaymentdata = {
            "paymentId": "testid",
            "trasactionresult": {
                "Success": true, "Result": "success",
                "Message": "successful", "Id": "testpaymentid", "Time": "testtime"
            },
            "finalsuccess": "YES", "validateCorpFileStatus": "active"
        }

        it('should test navToReceipt when form is valid and transaction status is true', function () {
            form.$invalid = false;
            renewalPaymentcontroller.billingaddress = true;
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmitPayment').respond(doneWithPaymentDetails);
            renewalPaymentcontroller.navToReceipt();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/renewalreceipt/guid');
        });

        it('should test navToReceipt when form is valid and transaction status is true and when different billing address is used', function () {
            form.$invalid = false;
            renewalPaymentcontroller.billingaddress = false;
            renewalPaymentcontroller.diffRenewalPayment = {
                firstname: "testfirstname",
                middlename: "testmiddlename",
                lastname: "testlastname",
                address1: "testaddress1",
                address2: "testaddress2",
                address3:"testaddress3"
            }
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmitPayment').respond(doneWithPaymentDetails);
            renewalPaymentcontroller.navToReceipt();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/renewalreceipt/guid');
        });

        it('should test navToReceipt when form is valid and transaction status is false', function () {
            form.$invalid = false;
            renewalPaymentcontroller.billingaddress = true;
            renewalPaymentcontroller.paymentBillingDetails = {
                GrandTotalAmount: 0
            };
            doneWithPaymentDetails.trasactionresult.Success = false;
            httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmitPayment').respond(doneWithPaymentDetails);
            renewalPaymentcontroller.navToReceipt();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/paymentfailure/renewal/guid');
        });

    });
});