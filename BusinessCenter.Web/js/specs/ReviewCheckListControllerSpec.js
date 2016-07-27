describe('Review CheckList Controller Spec', function () {

    var $scope, controller, httpBackend, mockservice, reviewChecklistController, localStore, basePath, utilityfac, routeParams;
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, $routeParams, UtilityFactory,appConstants) {
        rootscope = $rootScope.$new();
        $scope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'path');
        utilityfac = UtilityFactory;
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
        basePath = appConstants.apiServiceBaseUri;
        httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
        httpBackend.when('POST', basePath + 'api/BBLApplication/ServiceCheckList').respond(data);
        reviewChecklistController = controller('ReviewCheckListController', {
            $scope: $scope, $rootScope: rootscope,
            $location: $location, mockservice: mockservice,
            routeparams:routeParams,utilityFac:utilityfac
        });

    }));

    var data = {
        "MasterId": "39d78d76-8ab7-4a4b-9843-18aabfb91508",
        "ApplicationFee": 0.0,
        "CategoryLicenseFee": 0.0000,
        "EndorsementFee": 0.0000,
        "SubTotal": 0.0000,
        "TechFee": 0.0000,
        "TotalFee": 72.6000,
        "Isehop": true,
        "ExtraAmount": 0.0,
        "Extradays": "ACTIVE",
        "DetailedCategoryList": [
           {
               "Endorsement": "General Business",
               "CategoryId": "BDABCA0B-8B80-4CC6-8836-6D5A751B6C54",
               "LicenseCategory": "Charitable Exempt",
               "Units": "NA ",
               "ApplicationFee": 0.0,
               "CategoryLicenseFee": 0.0000,
               "EndorsementFee": 0.0000,
               "SubTotal": 0.0000,
               "TechFee": 0.0000,
               "TotalFee": 0.0000,
               "CategoryCode": "4001",
               "IsRaoFeeApplied": false,
               "LicenseDuration": "TWO (2) YEAR"
           },
           {
               "Endorsement": "General Business",
               "CategoryId": "1B69B449-ECAB-41AE-89A9-E2F76329A52B",
               "LicenseCategory": "General Business Licenses",
               "Units": "NA ",
               "ApplicationFee": 0.0,
               "CategoryLicenseFee": 0.0000,
               "EndorsementFee": 0.0000,
               "SubTotal": 0.0000,
               "TechFee": 0.0000,
               "TotalFee": 0.0000,
               "CategoryCode": "4003",
               "IsRaoFeeApplied": false,
               "LicenseDuration": "TWO (2) YEAR"
           },
           {
               "Endorsement": "General Business",
               "CategoryId": "F8DD1CE1-6645-4A58-8739-0B018565B20C",
               "LicenseCategory": "Online Music Business",
               "Units": "NA",
               "ApplicationFee": 0.0,
               "CategoryLicenseFee": 0.0,
               "EndorsementFee": 0.0,
               "SubTotal": 0.0,
               "TechFee": 0.0,
               "TotalFee": 0.0,
               "CategoryCode": "4003",
               "IsRaoFeeApplied": false,
               "LicenseDuration": "TWO (2) YEAR"
           }
        ],
        "SubQuestion": [
           {
               "Question": "Would you like a two (2) or four (4) year license?",
               "Option": null,
               "Answer": "Two (2) year",
               "Type": "RadioButton",
               "StartRange": 0,
               "EndRange": 0,
               "QuestionFor": null,
               "CategoryId": null,
               "keyIdentifying": null
           },
           {
               "Question": "Will this business be located in the District of Columbia?",
               "Option": null,
               "Answer": "YES",
               "Type": "RadioButton",
               "StartRange": 0,
               "EndRange": 0,
               "QuestionFor": null,
               "CategoryId": null,
               "keyIdentifying": null
           },
           {
               "Question": "Will this Business be Home based?",
               "Option": null,
               "Answer": "YES",
               "Type": "RadioButton",
               "StartRange": 0,
               "EndRange": 0,
               "QuestionFor": null,
               "CategoryId": null,
               "keyIdentifying": null
           },
           {
               "Question": "Do you have a Home Occupancy Permit (HOP) from Office of Zoning?",
               "Option": null,
               "Answer": "NO",
               "Type": "RadioButton",
               "StartRange": 0,
               "EndRange": 0,
               "QuestionFor": null,
               "CategoryId": null,
               "keyIdentifying": null
           },
           {
               "Question": "Is this business already registered with DCRA’s Corporations Division?",
               "Option": null,
               "Answer": "YES",
               "Type": "RadioButton",
               "StartRange": 0,
               "EndRange": 0,
               "QuestionFor": null,
               "CategoryId": null,
               "keyIdentifying": null
           },
           {
               "Question": "Which Tax Identification Number is associated with your business: Federal Employer Identification Number (FEIN) or  Social Security Number (SSN)?",
               "Option": null,
               "Answer": "FEIN",
               "Type": "RadioButton",
               "StartRange": 0,
               "EndRange": 0,
               "QuestionFor": null,
               "CategoryId": null,
               "keyIdentifying": null
           }
        ]
    }



    it('should test init', function () {
        
        httpBackend.flush();
        expect(reviewChecklistController.reviewcheckList).toBeDefined();
        expect(reviewChecklistController.reviewcheckList.SubQuestion.length).toBeGreaterThan(0);
    });

    it('should test navToChecklist', function () {
        routeParams.guid = 'guid';
        reviewChecklistController.navToChecklist();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/appchecklist/guid');
    });


    it('should test navToApply', function () {
        reviewChecklistController.navToApply();
        httpBackend.flush();
        expect($location.path).toHaveBeenCalledWith('/newbblwelcome');
    });

    it('should test navToMyBBL', function () {
        reviewChecklistController.navToMyBBL();
        expect($location.path).toHaveBeenCalledWith('/mybbl');
    });

    it('should test expandCollapse', function () {
        var event = {target:""};
        reviewChecklistController.expandCollapse(event);
    });

});