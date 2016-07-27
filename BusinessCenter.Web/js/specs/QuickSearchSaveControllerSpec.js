describe('QuickSearch Result Controller Spec', function () {


    var $scope, controller, httpBackend, mockservice, quickSearchSaveController, localStore, windowObj, basePath;
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, $window, appConstants) {
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
        httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
        controller = $controller;
        windowObj = $window;

    }));

    var deletesingle_data = {
        "EntityId": 10034413, "DataSource": "BBL", "LicenseNumber": null, "CompanyName": null, "CreatedBy": "2bfe7e43-d343-4fd5-9997-5bda0b7ce25e", "UserId": "2bfe7e43-d343-4fd5-9997-5bda0b7ce25e"
    }
    var savedData = [
        {
            "id": 1, "WishList": false, "EntityID": 40299178, "Source": "CORP", "CompanyName": null, "LicenseNumber": null,
            "FirstName": null, "LastName": null, "LeftNameTop": "Entity Name : ", "LeftNameMiddle": "File Number : ",
            "LeftNameBottom": "Next Report Year : ", "MiddleNameTop": "Entity Address : ", "MiddleNameMiddle": null,
            "MiddleNameBottom": null, "RightNameTop": "Model Type : ", "RightNameMiddle1": "", "RightNameMiddle2": "Entity Status : ",
            "RightNameBottom": null, "Expantion1": "Effective Date : ", "Expantion2": "Locale : ", "Expantion3": null, "Expantion4": null,
            "Expantion5": null, "Expantion6": null, "LeftNameResultTop": "GSG CO., INCORPORATED", "LeftNameResultMiddle": "C0000001564",
            "LeftNameResultBottom": "2016", "MiddleNameResultTop": "1350 CLIFTON ST. ST NW STE# 504W WASHINGTON DC20009", "MiddleNameResultMiddle": null,
            "MiddleNameResultBottom": null, "RightNameResultTop": "FOR-PROFITCORPORATION", "RightNameResultMiddle1": "", "RightNameResultMiddle2": "REVOKED",
            "RightNameResultBottom": null, "ExpantionResult1": "Jul  6 2011 12:00AM", "ExpantionResult2": "DOMESTIC", "ExpantionResult3": null,
            "ExpantionResult4": null, "ExpantionResult5": null, "ExpantionResult6": null, "SourceFullName": "Corporate Registration",
            "LastUpdateDateName": "Last Retrieved On : ", "LastUpdateDate": "Nov  8 2015 12:00AM", "LeftNameMiddleLabel1": null, "LeftNameMiddle1Text": null
        },
        {
            "id": 2, "WishList": false, "EntityID": 30000595, "Source": "CBE", "CompanyName": null, "LicenseNumber": null, "FirstName": null,
            "LastName": null, "LeftNameTop": "Company Name : ", "LeftNameMiddle": "CBE Number : ", "LeftNameBottom": "Preference Points : ",
            "MiddleNameTop": "Business Address : ", "MiddleNameMiddle": null, "MiddleNameBottom": null, "RightNameTop": null, "RightNameMiddle1": "Expiration Date : ",
            "RightNameMiddle2": null, "RightNameBottom": "Organization Type : ", "Expantion1": "COMPANY DESCRIPTION : ", "Expantion2": "PREFERENCE POINTS DETAIL : ",
            "Expantion3": "BUSINESS CONTACT : ", "Expantion4": "BUSINESS PHONE : ", "Expantion5": "BUSINESS EMAIL : ", "Expantion6": null,
            "LeftNameResultTop": "Agencyq, Inc.", "LeftNameResultMiddle": "LS6072015", "LeftNameResultBottom": "5",
            "MiddleNameResultTop": "1825 K STREET NW, Suite 500, WASHINGTON, DC 20006", "MiddleNameResultMiddle": null, "MiddleNameResultBottom": null,
            "RightNameResultTop": null, "RightNameResultMiddle1": "7/3/2015 12:00:00 AM", "RightNameResultMiddle2": null, "RightNameResultBottom": "Corporation",
            "ExpantionResult1": "AgencyQ is a digital marketing agency that excels in four core services: Digital Marketing, Technology, Strategy and Design. Headquartered in Washington, DC, agencyQ serves a global portfolio of commercial, government, and association and nonprofit organizations.",
            "ExpantionResult2": "2 for LBE; 3 for SBE", "ExpantionResult3": "Sean Breen", "ExpantionResult4": "2027769090", "ExpantionResult5": "Sean.Breen@agencyq.com",
            "ExpantionResult6": null, "SourceFullName": "Certified Business Enterprise", "LastUpdateDateName": "Last Retrieved On : ", "LastUpdateDate": "6/22/2015 12:00:00 AM",
            "LeftNameMiddleLabel1": null, "LeftNameMiddle1Text": null
        }
    ];

    var selectall = [
        {
            "ID": 0, "RecordCount": "146", "NoofRecords": 0, "ABRAID": 0, "Source": null, "Name": null, "ABRACount": "5", "BBLCount": "43", "CBECount": "10",
            "CORPCount": "67", "OPLACount": "21", "TotalSearchList": null, "FinalData": null, "ExcededBbl": null, "ExcededOpla": null, "ExcededCorp": null,
            "ExcededAbra": null, "ExcededCbp": null, "ExcededCount": 0, "ExcededRegulatoryEntities": null
        }
    ];

    describe('when user is loggedin', function () {
        beforeEach(function () {
            localStorage.loggedin = 2;
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/MySavedResults/SelectAll').respond(selectall);
            httpBackend.when('POST', basePath + 'api/MySavedResults/All').respond(savedData);
            httpBackend.when('POST', basePath + 'api/MySavedResults/Deletesingle').respond(deletesingle_data);
            
            quickSearchSaveController = controller('QuickSearchSaveController', { $scope: $scope, $rootScope: rootscope, $location: $location, mockservice: mockservice, windowObj: windowObj });
        });

        it('should test init when loggedin is zero', function () {
            httpBackend.flush();
            expect(angular.equals($scope.SavedLisenceTypeCounts, selectall)).toBeTruthy();
        });

        it('should test filterSomeProperties', function () {
            $scope.filterByTypeKeyword = "DOMESTIC";
            var result = $scope.filterSomeProperties(savedData[0]);
            httpBackend.flush();
            expect(result).toBeTruthy();
        });

        it('should test navToRemove', function () {
            localStorage.userId = 1234;
            localStorage.showMessagePopup = true;
            var e = {};
            httpBackend.when('POST', basePath + 'api/MySavedResults/Deletesingle').respond({ "status": "success" });
            $scope.navToRemove(savedData[0], e, savedData[0].Source, savedData[0].id);
            httpBackend.flush();
            expect($scope.SavedLisenceTypeCounts[0].RecordCount).toBe(selectall[0].RecordCount- 1);
        });

        //it('should test cancel_delete', function () {
        //    $scope.cancel_delete();
        //    httpBackend.flush();
        //    expect(localStorage.showMessagePopup).toBeFalsy();
        //    expect($scope.deleteselected).toBe(0);
        //});

        it('should test navToQuickSearch', function () {
            $scope.navToQuickSearch();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/quicksearch');
        });

        it('should test navToQuickSearchResult', function () {
            $scope.navToQuickSearchResult();
            httpBackend.flush();
            expect($location.path).toHaveBeenCalledWith('/quicksearchresult');
        });

        it('should test selectOption', function () {
            $scope.selectOption();
            httpBackend.flush();
            expect($scope.currentPage).toBe(1);
        });

        it('should test filterByType', function () {
            $scope.filterByType('testtype','testid');
            httpBackend.flush();
            expect($scope.currentPage).toBe(1); 
            expect($scope.filterByTypeKeyword).toBe("testtype");
        });

        it('should test setChcekboxSort', function () {
            $scope.setChcekboxSort('testtype');
            httpBackend.flush();
            expect($scope.chcekboxSort).toBe('testtype');
        });

        it('should test setDisabled when data is 0', function () {
            $scope.setDisabled('0','id');
            httpBackend.flush();
        });

        it('should test setDisabled when data not equal to 0', function () {
            $scope.setDisabled('1', 'id');
            httpBackend.flush();
        });

        it('should test checkEnter when e is 13', function () {
            var e = $.Event("keypress");
            e.which = 13;
            $scope.checkEnter(e, 'src');                
            httpBackend.flush();
        });

        //it('should test scrollPage', function () {
        //    $scope.scrollPage('id');
        //    httpBackend.flush();
        //});

        it('should test checkEnterOperations when e is 13', function () {
            var e = $.Event("keypress");
            e.which = 13;
            var obj = {};
            $scope.checkEnterOperations(obj, e);
            httpBackend.flush();
        });

        it('should test navToModal ', function () {
            $scope.navToModal('id');
            httpBackend.flush();
        });

        it('should test sortClick ', function () {
            $scope.sortClick();
            httpBackend.flush();
        });

        it('should test expandCollapse ', function () {
            $scope.expandCollapse('event', 'type');
            httpBackend.flush();
        });

    });

});