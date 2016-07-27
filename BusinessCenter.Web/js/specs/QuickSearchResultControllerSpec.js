describe('QuickSearch Result Controller Spec', function () {

    var selectall_data = [{
        "SearchCount": [{
            "ID": 0, "RecordCount": "1", "NoofRecords": 0, "ABRAID": 0, "Source": null, "Name": null, "ABRACount"
        : "0", "BBLCount": "0", "CBECount": "0", "CORPCount": "1", "OPLACount": "0", "TotalSearchList": null, "FinalData"
        : null, "ExcededBbl": "", "ExcededOpla": "", "ExcededCorp": "", "ExcededAbra": "", "ExcededCbp": "", "ExcededCount"
        : 0, "ExcededRegulatoryEntities": ""
        }], "SearchFinalData": [{
            "id": 1, "WishList": false, "EntityID": 40057509, "Source"
            : "CORP", "CompanyName": null, "LicenseNumber": null, "FirstName": null, "LastName": null, "LeftNameTop": "EntityName : ", "LeftNameMiddle": "File Number : ", "LeftNameBottom": "Next Report Year : ",
            "MiddleNameTop": "EntityAddress : ", "MiddleNameMiddle": null, "MiddleNameBottom": null, "RightNameTop": "Model Type : ", "RightNameMiddle1"
        : "", "RightNameMiddle2": "Entity Status : ", "RightNameBottom": null, "Expantion1": "Effective Date : ", "Expantion2"
        : "Locale : ", "Expantion3": null, "Expantion4": null, "Expantion5": null, "Expantion6": null, "LeftNameResultTop"
        : "BABCOCK ENTERPRISES, INC.", "LeftNameResultMiddle": "296003", "LeftNameResultBottom": "2016", "MiddleNameResultTop"
        : "", "MiddleNameResultMiddle": null, "MiddleNameResultBottom": null, "RightNameResultTop": "CORPORATION (FOR PROFIT)", "RightNameResultMiddle1": "", "RightNameResultMiddle2": "WITHDRAWN", "RightNameResultBottom": null
        , "ExpantionResult1": "Nov 13 2009 12:00AM", "ExpantionResult2": "FOREIGN", "ExpantionResult3": null, "ExpantionResult4"
            : null, "ExpantionResult5": null, "ExpantionResult6": null, "SourceFullName": "Corporate Registration", "LastUpdateDateName"
            : "Last Retrieved On : ", "LastUpdateDate": "Feb  7 2016  9:17PM", "LeftNameMiddleLabel1": null, "LeftNameMiddle1Text"
            : null
        }]
    }]
    var $scope, controller, httpBackend, mockservice, ApplicationChecklistController, localStore, timeout, basePath, authservice, appconstants;
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, $timeout, appConstants, authService) {
        basePath = appConstants.apiServiceBaseUri;
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
                }
            };
        })();

        Object.defineProperty(window, 'localStorage', { value: localStore });
        mockservice = requestService;
        httpBackend = $httpBackend;
        authservice = authService;
        appconstants = appConstants;
        httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
        controller = $controller;
        timeout = $timeout;

    }));

    var searchData = [
        {
            "SearchCount":
            [
                {
                    "ID": 0, "RecordCount": "129", "NoofRecords": 0, "ABRAID": 0, "Source": null, "Name": null, "ABRACount": "4", "BBLCount": "22", "CBECount": "1", "CORPCount": "100", "OPLACount": "2", "TotalSearchList": null, "FinalData": null, "ExcededBbl": "", "ExcededOpla": "", "ExcededCorp": ",Corporate Registration", "ExcededAbra": "", "ExcededCbp": "", "ExcededCount": 1, "ExcededRegulatoryEntities": ",Corporate Registration"
                }
            ],
            "SearchFinalData":
                [
                    { "id": 1, "WishList": false, "EntityID": 10010690, "Source": "BBL", "CompanyName": null, "LicenseNumber": null, "FirstName": null, "LastName": null, "LeftNameTop": "Corporation Name :", "LeftNameMiddle": "Trade Name : ", "LeftNameBottom": "License Category : ", "MiddleNameTop": "Premise Address : ", "MiddleNameMiddle": null, "MiddleNameBottom": null, "RightNameTop": "License Number : ", "RightNameMiddle1": "License Expiration Date : ", "RightNameMiddle2": "License Status : ", "RightNameBottom": null, "Expantion1": "License Endorsement/Category", "Expantion2": null, "Expantion3": null, "Expantion4": null, "Expantion5": null, "Expantion6": null, "LeftNameResultTop": "", "LeftNameResultMiddle": "ABC CLEANER", "LeftNameResultBottom": "General Business Licenses", "MiddleNameResultTop": "6301 GEORGIA AVE NW WASHINGTON DC 20011", "MiddleNameResultMiddle": null, "MiddleNameResultBottom": null, "RightNameResultTop": "400314001639", "RightNameResultMiddle1": "6/30/2016", "RightNameResultMiddle2": "Active", "RightNameResultBottom": "", "ExpantionResult1": "General Business - General Business Licenses", "ExpantionResult2": null, "ExpantionResult3": null, "ExpantionResult4": null, "ExpantionResult5": null, "ExpantionResult6": null, "SourceFullName": "Business License", "LastUpdateDateName": "Last Retrieved On : ", "LastUpdateDate": "11/8/2015 12:00:00 AM", "LeftNameMiddleLabel1": "Licensee Name : ", "LeftNameMiddle1Text": "WI Seo" },
                    { "id": 2, "WishList": false, "EntityID": 10014134, "Source": "BBL", "CompanyName": null, "LicenseNumber": null, "FirstName": null, "LastName": null, "LeftNameTop": "Corporation Name :", "LeftNameMiddle": "Trade Name : ", "LeftNameBottom": "License Category : ", "MiddleNameTop": "Premise Address : ", "MiddleNameMiddle": null, "MiddleNameBottom": null, "RightNameTop": "License Number : ", "RightNameMiddle1": "License Expiration Date : ", "RightNameMiddle2": "License Status : ", "RightNameBottom": null, "Expantion1": "License Endorsement/Category", "Expantion2": null, "Expantion3": null, "Expantion4": null, "Expantion5": null, "Expantion6": null, "LeftNameResultTop": "", "LeftNameResultMiddle": "ABC CLEANING", "LeftNameResultBottom": "General Business Licenses", "MiddleNameResultTop": "3461 SUMMIT CT NE WASH DC 20018", "MiddleNameResultMiddle": null, "MiddleNameResultBottom": null, "RightNameResultTop": "400315903300", "RightNameResultMiddle1": "5/31/2017", "RightNameResultMiddle2": "Active", "RightNameResultBottom": "", "ExpantionResult1": "General Business - General Business Licenses", "ExpantionResult2": null, "ExpantionResult3": null, "ExpantionResult4": null, "ExpantionResult5": null, "ExpantionResult6": null, "SourceFullName": "Business License", "LastUpdateDateName": "Last Retrieved On : ", "LastUpdateDate": "11/8/2015 12:00:00 AM", "LeftNameMiddleLabel1": "Licensee Name : ", "LeftNameMiddle1Text": "JANET Carter" }
                ]
        }];

    var searchInput = {
        "companyName": "abc", "pageSize": 10, "pageIndex": 1, "isChanged": false,
        "Userid": "2bfe7e43-d343-4fd5-9997-5bda0b7ce25e", "SearchType": "All-",
        "DisplayType": "All-", "KeyType": "search"
    };

    //describe('testing init when there is no data in rootscope or localstorage', function () {
    //    beforeEach(function () {
    //        localStorage.removeItem("searchData");
    //        quicksearchresultController = controller('QuickSearchResultController',
    //            {
    //                $scope: $scope, $rootScope: rootscope, $location: $location,
    //                mockservice: mockservice, timeout: timeout,
    //                authService: authservice, appConstants: appconstants
    //            });
    //    });
    //    it('should test init method when there is no data in rootscope or localstorage', function () {
    //        expect($location.path).toHaveBeenCalledWith('/quicksearch');
    //    });
    //});

    describe('should test when localstorage value is searchData is defined', function () {
        beforeEach(function () {
            localStorage.setItem('searchData', JSON.stringify(searchInput));
            localStorage.userId = "2bfe7e43-d343-4fd5-9997-5bda0b7ce25e";
            //$scope.SearchInput = {
            //    "companyName": "aaa", "pageSize": 10, "pageIndex": 1, "isChanged": false, "Userid": "2bfe7e43-d343-4fd5-9997-5bda0b7ce25e"
            //    , "SearchType": "CORP-OPLA-CBE-", "DisplayType": "CORP-OPLA-CBE-", "KeyType": "search"
            //};

            quicksearchresultController = controller('QuickSearchResultController', {
                $scope: $scope, $rootScope: rootscope,
                $location: $location, mockservice: mockservice, timeout: timeout,
                authService: authservice, appConstants: appconstants

            });
        });

        it('should test when localstorage value is searchData is defined', function () {
            expect($scope.SearchInput.isChanged).toBeFalsy();
            expect($scope.SearchInput.Userid).toBe("2bfe7e43-d343-4fd5-9997-5bda0b7ce25e");
            //expect($scope.itemPage).toBe(10);
        });

        it('should test navToQuickSearchSave', function () {
            var e = {};
            e.ctrlKey = false;
            localStorage.loggedin = 2;
            authservice.authentication = { isAuth: false }
            $scope.navToQuickSearchSave(e);
            expect(localStorage.path).toBe('/quicksearchsave');
        });


        it('should test labelClick', function () {
            localStorage.userId = 1;

            httpBackend.when('POST', basePath + 'api/MySavedResults/Deletesingle').respond({ "status": "success" });
            $scope.labelClick({ "test": "testdata", "WishList": true });
            //httpBackend.flush();
        });

        it('should test navToQuickSearch', function () {
            var e = {};
            e.ctrlKey = false;
            $scope.navToQuickSearch(e);
            expect($location.path).toHaveBeenCalledWith('/quicksearch');
        });



        it('should test selectOption', function () {
            $scope.selectOption();
            expect($scope.currentPage).toBe(1);
        });

        it('should test filterByType', function () {
            $scope.filterByType("testkey", "testid")
            expect($scope.filterByTypeKeyword).toBe("testkey")
        });

        it('should test setDisabled', function () {
            var e = { which: 13 };
            expect($scope.setDisabled(0, "testid")).toBeTruthy();
        });

        it('should test setDisabled', function () {
            expect($scope.setDisabled(1, "testid")).toBeFalsy();
        });

        it('should test checkEnter', function () {
            var e = { which: 13 };
            $scope.checkEnter(e, "testid")
            expect($scope.filterByTypeKeyword).toBe("testkey")
        });

        it('should test checkEnterOperations', function () {
            var e = { which: 13 };
            httpBackend.when('POST', basePath + 'api/MySavedResults/Deletesingle').respond({ "status": "success" });
            $scope.checkEnterOperations({ testobj: "obj" }, e);
            //expect($scope.filterByTypeKeyword).toBe("testkey")
        });

        it('should test modal_close', function () {
            $scope.modal_close()
            expect(localStorage.SaveData).toBe('null');
            expect(localStorage.path).toBeDefined();
        });

        it('should test navToSave method when user is logged in', function () {
            localStorage.loggedin = 0;
            $scope.navToSave({ "testprp1": "testdata" }, "testevnt");
            expect(localStorage.path).toBe('/quicksearchresult');
        });

        it('should test navtosave method when user is logged in', function () {
            localStorage.loggedin = 2;
            localStorage.path = '/quicksearchresult';
            authservice.authentication = { isAuth: true }
            httpBackend.when('POST', basePath + 'api/MySavedResults/AddToMyList').respond({ "status": "success" });
            $scope.navToSave({ "testprp1": "testdata" }, "testevnt");
            httpBackend.flush();
            expect(localStorage.path).toBe('');
        });

        it("should test loginRedirect", function () {
            $scope.loginRedirect();
            timeout.flush();
            expect($location.path).toHaveBeenCalledWith("/login");
        });

        it("should test remainSame", function () {
            $scope.remainSame();
            expect(localStorage.path).toEqual("undefined");
        });

        it('should test filterSomeProperties', function () {
            $scope.filterByTypeKeyword = "testkey";
            expect($scope.filterSomeProperties({ "Source": "testkey" })).toBeTruthy();
        });

        //it('should test ShowMessage', function () {
        //    $scope.filterByTypeKeyword = "test";
        //    $scope.ShowMessage();
        //    expect(localStorage.pagenumber).toBe(1);
        //    expect(localStorage.filterByType).toBe("test");
        //});

        //it('should test navToDelete', function () {
        //    localStorage.userId = 1;

        //    httpBackend.when('POST', basePath + 'api/MySavedResults/Deletesingle').respond({ "status": "success" });
        //    $scope.navToDelete({ "test": "testdata","WishList":true});
        //    httpBackend.flush();
        //});

        it('should test manageUserList', function () {
            localStorage.userId = 1;

            httpBackend.when('POST', basePath + 'api/MySavedResults/Deletesingle').respond({ "status": "success" });
            $scope.manageUserList({ "test": "testdata", "WishList": true });
            //httpBackend.flush();
        });

        it('should test sortClick', function () {
            $scope.sortClick({ "Source": "testkey" })
        });

        it('should test helpClick', function () {
            $scope.helpClick("testdata for help function")
        });

    });

});