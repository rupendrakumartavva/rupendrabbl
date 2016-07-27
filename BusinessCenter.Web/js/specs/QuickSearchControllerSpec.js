describe('Quick Search Controller Test', function () {

    var $scope, rootscope, controller, httpBackend, mockservice, quicksearchcontroller, key, name, basePath, localStore, parse, http, authservice, $location;

    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants, $parse, $http, authService) {
        rootscope = $rootScope.$new();
        $scope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'search');
        spyOn($location, 'path');
        mockservice = requestService;
        httpBackend = $httpBackend;
        controller = $controller;
        $scope.key = key;
        $scope.name = name;
        $scope.length = {};
        basePath = appConstants.apiServiceBaseUri;
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
                }
            };
        })();
        parse = $parse;
        authservice = authService;
        http = $http;

        Object.defineProperty(window, 'localStorage', { value: localStore });
    }));

    var searchData = [
                {
                    "SearchCount":
                      [
                          {
                              "ID": 0, "RecordCount": "22", "NoofRecords": 0, "ABRAID": 0, "Source": null, "Name": null, "ABRACount": "0", "BBLCount": "22", "CBECount": "0", "CORPCount": "0", "OPLACount": "0", "TotalSearchList": null, "FinalData": null, "ExcededBbl": "", "ExcededOpla": "", "ExcededCorp": "", "ExcededAbra": "", "ExcededCbp": "", "ExcededCount": 0, "ExcededRegulatoryEntities": ""
                          }],
                    "SearchFinalData":
                        [
                            {
                                "id": 1, "WishList": false, "EntityID": 10010690, "Source": "BBL", "CompanyName": null, "LicenseNumber": null, "FirstName": null,
                                "LastName": null, "LeftNameTop": "Corporation Name :", "LeftNameMiddle": "Trade Name : ", "LeftNameBottom": "License Category : ", "MiddleNameTop": "Premise Address : ", "MiddleNameMiddle": null, "MiddleNameBottom": null, "RightNameTop": "License Number : ", "RightNameMiddle1": "License Expiration Date : ", "RightNameMiddle2": "License Status : ", "RightNameBottom": null, "Expantion1": "License Endorsement/Category", "Expantion2": null, "Expantion3": null, "Expantion4": null, "Expantion5": null, "Expantion6": null, "LeftNameResultTop": "", "LeftNameResultMiddle": "ABC CLEANER", "LeftNameResultBottom": "General Business Licenses", "MiddleNameResultTop": "6301 GEORGIA AVE NW WASHINGTON DC 20011", "MiddleNameResultMiddle": null, "MiddleNameResultBottom": null, "RightNameResultTop": "400314001639", "RightNameResultMiddle1": "6/30/2016", "RightNameResultMiddle2": "Active", "RightNameResultBottom": "", "ExpantionResult1": "General Business - General Business Licenses", "ExpantionResult2": null, "ExpantionResult3": null, "ExpantionResult4": null, "ExpantionResult5": null, "ExpantionResult6": null, "SourceFullName": "Business License", "LastUpdateDateName": "Last Retrieved On : ", "LastUpdateDate": "11/8/2015 12:00:00 AM", "LeftNameMiddleLabel1": "Licensee Name : ", "LeftNameMiddle1Text": "WI Seo"
                            }, {
                                "id": 2, "WishList": false, "EntityID": 10014134, "Source": "BBL", "CompanyName": null, "LicenseNumber": null, "FirstName":
                                    null, "LastName": null, "LeftNameTop": "Corporation Name :", "LeftNameMiddle": "Trade Name : ", "LeftNameBottom": "License Category : ", "MiddleNameTop": "Premise Address : ", "MiddleNameMiddle": null, "MiddleNameBottom": null, "RightNameTop": "License Number : ", "RightNameMiddle1": "License Expiration Date : ", "RightNameMiddle2": "License Status : ", "RightNameBottom": null, "Expantion1": "License Endorsement/Category", "Expantion2": null, "Expantion3": null, "Expantion4": null, "Expantion5": null, "Expantion6": null, "LeftNameResultTop": "", "LeftNameResultMiddle": "ABC CLEANING", "LeftNameResultBottom": "General Business Licenses", "MiddleNameResultTop": "3461 SUMMIT CT NE WASH DC 20018", "MiddleNameResultMiddle": null, "MiddleNameResultBottom": null, "RightNameResultTop": "400315903300", "RightNameResultMiddle1": "5/31/2017", "RightNameResultMiddle2": "Active", "RightNameResultBottom": "", "ExpantionResult1": "General Business - General Business Licenses", "ExpantionResult2": null, "ExpantionResult3": null, "ExpantionResult4": null, "ExpantionResult5": null, "ExpantionResult6": null, "SourceFullName": "Business License", "LastUpdateDateName": "Last Retrieved On : ", "LastUpdateDate": "11/8/2015 12:00:00 AM", "LeftNameMiddleLabel1": "Licensee Name : ", "LeftNameMiddle1Text": "JANET Carter"
                            }
                        ]
                }];
    var data = [
               "columbia cab association",
               "north american electric reliability",
               "affordable hair gallery, ", "harlan laboratories", "abroad one", "capitol alliance for neighborhood stabilization",
               "blue line cab", "spiritual tabernacle international & spirit of the lord worship & deliverance center", "melco laboratories"];


    describe('should test when logged in', function () {
        beforeEach(function () {
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/Search/SelectAll').respond(searchData);
            httpBackend.when('POST', basePath + 'api/Search/Company').respond(data);
            httpBackend.when('POST', basePath + 'api/Search/FirstName').respond(data);
            httpBackend.when('POST', basePath + 'api/Search/LastName').respond(data);
            authservice.authentication = { isAuth: true };
            quicksearchcontroller = controller('QuickSearchController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, requestService: mockservice,
                $parse: parse, $http: http, authservice: authservice
            });
        });

        it('should test init', function () {
            expect($scope.enableButton).toBeFalsy();
            expect($scope.disablecheckbox).toBeFalsy();
        });

        it('should test setErrorMsg', function () {
            $scope.setErrorMsg();
        });

        it('should test checktabpress', function () {
            $scope.checktabpress("testid");
        });

        it('should test search_validate when company Name or firstname contains astricts', function () {
            $scope.search = { "firstname": "tes*", "companyName": "testcompany" };
            $scope.search_validate($scope.search);
            expect($scope.sendRequest).toBeFalsy();
        });

        it('should test search_validate when company Name contains astricts', function () {
            $scope.search = { "firstname": "te*", "companyName": "testcompany" };
            $scope.search_validate($scope.search);
            expect($scope.sendRequest).toBeFalsy();
        });

        it('should test search_validate when company Name is correct and a license type is selected', function () {
            $scope.BBL = true;
            $scope.search = { "firstname": "test", "companyName": "testcompany" };
            $scope.search_validate($scope.search);
            httpBackend.flush();
            expect($scope.search.pageSize).toBe(10);
            expect($location.path).toHaveBeenCalledWith('/quicksearchresult');
        });

        it('should test getSuggestions method with first name', function () {
            $scope.getSuggestions('ab', 'FirstName');
            httpBackend.flush();
            expect($scope.dataavail).toBeTruthy();
            expect($scope.FirstName).toEqual(data);
        });

        it('should test getSuggestions with company', function () {
            $scope.getSuggestions('ab', 'Company');
            httpBackend.flush();
            expect($scope.dataavail).toBeTruthy();
            expect($scope.Company).toEqual(data);
        });

        it('should test getSuggestions with last name', function () {
            $scope.getSuggestions('ab', 'LastName');
            httpBackend.flush();
            expect($scope.dataavail).toBeTruthy();
            expect($scope.LastName).toEqual(data);
        });

        it('should test getSuggestions', function () {
            $scope.getSuggestions('ab*', 'Company');
            httpBackend.flush();
            expect($scope.dataavail).toBeTruthy();
            expect($scope.Company).toEqual(data);
        });

        it('should test check_checkBox Method', function () {
            $scope.selectedCheckBoxes = {};
            $scope.check_checkBox('test', true);
            expect($scope.disablecheckbox).toBeFalsy();
            expect($scope.selectedCheckBoxes[name]).toBe(undefined);
        });

    });





   

    //it('should test disableOtherThanLicense Method when license name is greater than one', function () {
    //    $scope.search.licenseName = "test";
    //    $scope.disableOtherThanLicense();
    //    //httpBackend.flush();
    //});

    //it('should test disableOtherThanLicense Method when license name is less than one', function () {
    //    $scope.search.licenseName = "t";
    //    $scope.disableOtherThanLicense();
    //    //httpBackend.flush();
    //});


    //it('should test navToQuickSearchSave', function () {
    //    $scope.navToQuickSearchSave();
    //    //httpBackend.flush();
    //    expect($location.path).toHaveBeenCalledWith('/quicksearchsave');
    //});
    ////it('should test navToLogin', function () {
    ////    $scope.navToLogin();
    ////    httpBackend.flush();
    ////    expect($location.path).toHaveBeenCalledWith('/login');
    ////});
    ////it('should test navToLogin', function () {
    ////    localStorage.loggedin = 2;
    ////    $scope.navToLogin();
    ////    httpBackend.flush();
    ////    expect($location.path).toHaveBeenCalledWith('/dashboard');
    ////});

    //it('should test search_validate', function () {
    //    $scope.search = { "firstname": "testname", "companyName": "testcompany" };
    //    $scope.search_validate($scope.search);
    //    //httpBackend.flush();
    //    expect($scope.sendRequest).toBeFalsy();
    //});
});