describe('', function () {
    var $scope, controller, httpBackend, mockservice, renewalcleanhands, localStore, appconstants, routeParams;
    var basePath, renewalutilityfactory, compile, windowobj, timeoutobj;

    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, $routeParams, RenewalUtilityFactory, $window, $timeout, appConstants, $compile) {
        rootscope = $rootScope.$new();
        $scope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'path');
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
        routeParams = $routeParams;
        routeParams.guid = 'guid';
        renewalutilityfactory = RenewalUtilityFactory;
        appconstants = appConstants;
        basePath = appconstants.apiServiceBaseUri;
        compile = $compile;
        windowobj = $window;
        timeoutobj = $timeout;

        var fulladdressdata = {
            "MasterId": "f225a37b-ebed-4a35-bb11-2b3ed124641c", "FullAddress": "2001 L ST NW", "TaxNumber": "27-1712188"
, "UserId": "2bfe7e43-d343-4fd5-9997-5bda0b7ce25e", "UserBblAssociateId": "5", "EntityId": "10030757", "CreatedDate"
: "03/23/2016", "CurrentYear": "2016", "BusinessType": "BUSINESS LICENSE", "TaxType": "FEIN", "SubmissionLicense"
: "LREN14006506", "BusinessOwner": "PARTNERSHIP FOR A HEALTHIER AMERICA INC.", "tradeName": "NA"
        },
            renewaltaxvalidation = {"MasterId":"f225a37b-ebed-4a35-bb11-2b3ed124641c","EntityId":"10030757","LicenseNumber":"LREN14006506"
,"UserId":"2bfe7e43-d343-4fd5-9997-5bda0b7ce25e","GrandTotalAmount":0.0,"CorpNumber":"","IsCorp":true
,"CorpStatus":null,"TaxStatus":null,"TaxNumber":null,"SubCategoryName":null,"CategoryName":null,"ActivityName"
:null,"Endoresement":null,"CategoryId":null,"ActivityId":null,"SubCategoryID":null,"LicenseAmount":0.0,"EndorsementFee":0.0,"ApplicationFee":0.0,"TechFee":0.0,"RAOFee":0.0,"IsCorpRegistration":false,"IsCleanHands"
        : false,
"DocumentList": [{
    "MasterId": "f225a37b-ebed-4a35-bb11-2b3ed124641c", "SubmissionId": "LREN14006506"
        ,"SubmissionCategoryID":6,"ApprovedBy":"","DocRequired":"Notice of BusinessTax Registration","Agency"
        :"OTR","Division":"Business Tax Service Center","Div":"BTSC","FileName":"","FileLocation":"","DocStatus"
        : "Open", "Description": "A document indicating the business has completed the Combined Business Tax form(FR-500).",
        "Endorsement": "General Business", "License": "Charitable Solicitation", "CategoryID": "1331"
        ,"ShortName":"BusTaxRegistration","IsUpload":false,"UploadFileName":"","CheckListType":"Document","CategoryCode"
        :"4002","LicenseName":"LREN14006506"}],"ServiceCheckList":null,"BblAddress":null,"BblCity":null,"BblState"
    :null,"BblZip":null,"LapsedAmount":0.0,"ExpiredAmount":0.0,"Extradays":"Lapsed","RenewalLicenseCode"
    :null,"LrenNumber":"LREN14006506","UserBblAssociateId":"5","LicenseDuration":0,"RenewStatus":"Data","InitalDocumet"
    :null,"CurrentDate":null,"NameofLicense":null,"BusinessOwnerName":null,"SubmissionLicense":null,"ContactFirstName"
    :null,"ContactMiddleName":null,"ContactLastName":null,"App_Type":null}
        gettaxrevenue = [{"SubmissionTaxRevenueId":2,"MasterId":"f225a37b-ebed-4a35-bb11-2b3ed124641c","TaxRevenueNumber":"27-1712188"
,"TaxRevenueType":"FEIN/SSN","CreatdedDate":"2016-03-23T11:48:39.007","UpdatedDate":"2016-03-23T11:48:39.007","FullName":"aa","BusinessOwnerRoles":"Owner","IsIAgree":true}];

        httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
        httpBackend.when('POST', basePath + 'api/Renew/fulladdress').respond(fulladdressdata);
        httpBackend.when('POST', basePath + 'api/Renew/GetTaxRevenue').respond(gettaxrevenue);
        httpBackend.when('POST', basePath + 'api/Renew/RenewTaxValidation').respond(renewaltaxvalidation);

        renewalcleanhands = controller('RenewalCleanHandsController',
                {
                    $scope: $scope, $rootScope: rootscope,
                    $location: $location, mockservice: mockservice,
                    appConstants: appconstants, $routeParams: routeParams,
                    RenwalUtilityFactory: renewalutilityfactory, window: windowobj,
                    timeout: timeoutobj
                });
        $scope.vm = renewalcleanhands;
        var element = angular.element(
               '<form name="vm.renewalcleanhands">' +
               '<input ng-model="vm.taxnumber" name="signature" />' +
               '</form>'
               );
        compile(element)($scope);
        form = $scope.vm.renewalcleanhands;

    }));

    it('should test init', function () {
        //httpBackend.flush();
        renewalcleanhands.taxrevenue = gettaxrevenue;
        expect(renewalcleanhands.taxrevenue.length).toBeGreaterThan(0);
    });

    it('should test navToMyBBL', function () {
        renewalcleanhands.navToMyBBL();
        expect($location.path).toHaveBeenCalledWith('/mybbl');
    });
    
    it('should test navToPrevious', function () {
        spyOn(windowobj.history, 'back');
        renewalcleanhands.navToPrevious();
        expect(windowobj.history.back).toHaveBeenCalled();
    });

    it('should test restrictMaxLength', function () {
        renewalcleanhands.taxrevenue = {};
        renewalcleanhands.taxrevenue.number = "1111111111111111";
        renewalcleanhands.restrictMaxLength();
        expect(renewalcleanhands.taxrevenue.number.length).toEqual(11);

    });

    it('should test checkSignWithFullName when fullname and signature matches', function () {
        renewalcleanhands.taxrevenue = {};
        renewalcleanhands.taxrevenue.signature = "test";
        renewalcleanhands.taxrevenue.FullName = "test";
        renewalcleanhands.checkSignWithFullName();
        expect(form.signature.$valid).toBeTruthy();
    });

    it('should test checkSignWithFullName when fullname and signature mismatches', function () {
        renewalcleanhands.taxrevenue = {};
        renewalcleanhands.taxrevenue.signature = "test";
        renewalcleanhands.taxrevenue.FullName = "test1";
        renewalcleanhands.checkSignWithFullName();
        expect(form.signature.$valid).toBeFalsy();
    });

    it('should test checkSignWithFullName when fullname and signature mismatches', function () {
        renewalcleanhands.checkSignWithFullName();
        expect(renewalcleanhands.signatureMismatch).toBeFalsy();
    });

    //it('should test navigateAnyWay', function () {
    //    renewalcleanhands.navigationPath = "/testpath"
    //    renewalcleanhands.navigateAnyWay();
    //    expect($location.path).toHaveBeenCalledWith('/testpath');
    //});

    //it('should test stayOnThisPage ', function () {
    //    renewalcleanhands.stayOnThisPage();
    //    timeoutobj.flush();
    //});

    it('should test navToSupportingDocs when form is valid', function () {
        form.$invalid = false;
        renewalcleanhands.navToSupportingDocs();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/renewal/supportingdocs/step2/guid');
    });

    it('should test navToSupportingDocs when form is invalid', function () {
        //httpBackend.flush();
        form.$invalid = true;        
        renewalcleanhands.navToSupportingDocs();       
    });    

});