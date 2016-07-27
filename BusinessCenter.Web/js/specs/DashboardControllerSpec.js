describe('DashBoard Controller Test', function () {

    var $scope = {}, controller, httpBackend, mockservice, dashboardcontroller, basePath;
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants) {
        basePath = appConstants.apiServiceBaseUri;
        rootscope = $rootScope.$new();
        $location = _$location_;
        spyOn($location, 'path');
        mockservice = requestService;
        httpBackend = $httpBackend;
        controller = $controller;
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
    }));

    describe('when user is logged in', function () {
        beforeEach(function () {
            localStorage.userFirstName = "testusername";
            httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
            httpBackend.when('POST', basePath + 'api/MySavedResults/mycount').respond({ "status": "3" });
            httpBackend.when('POST', basePath + 'api/BBLAssociation/UserBblExpCount').respond({ "Result": 0 });
            dashboardcontroller = controller('DashboardController', { $scope: $scope, $rootScope: rootscope, $location: $location, requestService: mockservice });
        });

        it('should test init', function () {
            httpBackend.flush();
            expect($scope.userFirstName).toBe("testusername");
            expect($scope.result.status).toBe('3');
            expect($scope.list).toBe(0);
        });


        it('should test navToCorp', function () {
            httpBackend.flush();
            $scope.navToCorp();
        });

        //it('should test navigateAnyWay', function () {
        //    httpBackend.flush();
        //    $scope.navigateAnyWay();
        //});

        it('should test cancelNavigation', function () {
            httpBackend.flush();
            $scope.cancelNavigation();
        });

        //it('should test checkrightClick when controlkey is pressed', function () {
        //    var e = {
        //        function(){
                    
        //        }
        //    };
        //    e.target={};
        //    e.ctrlKey=true;
        //    $scope.checkrightClick('testurl', e);
        //    expect(e.target.href).toBe('testurl');
        //});

    });
});