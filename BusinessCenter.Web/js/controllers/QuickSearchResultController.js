(function () {
    'use strict';
    var controllerId = 'QuickSearchResultController';
    angular.module('DCRA').controller(controllerId, ['$scope', '$rootScope', '$location', 'requestService', '$timeout', 'authService', 'appConstants', 'popupFactory', QuickSearchResultController]);

    function QuickSearchResultController($scope, $rootScope, $location, requestService, $timeout, authService, appConstants, popupFactory) {
        $scope.contentSection = [
           { cId: 'c01', num: 10 },
           { cId: 'c02', num: 25 },
           { cId: 'c03', num: 50 }
        ];
        $scope.currentPage = 1;
        $scope.previousElement = '';
        $scope.confirmed = {};
        $scope.savedSearch = [];
        $scope.totalNames = {
            "BBL": "Business License",
            "CORP": "Corporate Registration",
            "OPLA": "Professional License",
            "CBE": "Certified Business Enterprise",
            "ABRA": "Alcoholic Beverage License",
            "All": "All"
        };
        $scope.itemPage = 10;
        $scope.chcekboxSort = false;
        $scope.searchData = {};
        $scope.SearchInput = {};
        $scope.filterByTypeKeyword = '';

        init();

        /*
        * Function: init
        * init (initialize) method: first method to be executed on controller load.
        */
        function init() {
            $('#dvLoadingSection').css('display', 'block');
            $("#dvMainsection").css("display", "none");
            if (authService.authentication.isAuth) {
                authService.refreshToken().then(function () {
                    loadQuicksearchResult();
                }, function () {
                    $location.path("/login");
                });
            } else {
                loadQuicksearchResult();
            }
        }

        function loadQuicksearchResult() {
            $scope.filterByTypeKeyword = localStorage.filterByType;
            $scope.currentPage = localStorage.pagenumber;
            if (angular.isDefined($rootScope.searchResults)) {
                $('#dvLoadingSection').css('display', 'none');
                $("#dvMainsection").css("display", "block");
                $scope.searchData = $rootScope.searchResults[0].SearchFinalData;
                $scope.LisenceTypeCounts = $rootScope.searchResults[0].SearchCount[0];
            }

            if (localStorage.getItem('searchData')) {
                $scope.SearchInput = JSON.parse(localStorage.getItem('searchData'));
                $scope.SearchInput.isChanged = false;

                if ((localStorage.userId == undefined) || localStorage.userId == undefined) {
                    $scope.SearchInput.Userid = 0;
                } else {
                    $scope.SearchInput.Userid = localStorage.userId;
                }
                if (localStorage.getItem('saveData')) {
                    if (authService.authentication.isAuth) {
                        saveData(JSON.parse(localStorage.getItem('saveData')), localStorage.getItem("eventobj"));
                        localStorage.removeItem("saveData");
                        localStorage.removeItem("eventobj");
                    }
                }
                window.setTimeout(function () {
                    if (!angular.isDefined($rootScope.searchResults))
                        getSearchData($scope.SearchInput);
                }, 1200);
            }
            else
                $location.path('/quicksearch');
            $('a[data-toggle="tooltip"]').tooltip({
                animated: 'fade',
                placement: 'right'
            });

            if (localStorage.getItem('itemsperpage') != null) {
                var itemsperpage = JSON.parse(localStorage.getItem('itemsperpage'));
                $scope.confirmed = $scope.contentSection[parseInt(itemsperpage.cId.substring(2)) - 1];
                $scope.itemPage = itemsperpage.num;
            } else {
                $scope.confirmed = $scope.contentSection[0];
                $scope.itemPage = 10;
            }

            if ($scope.SearchInput.DisplayType != undefined && $scope.SearchInput.DisplayType != '') {
                $scope.SearchCriteria = "Search Criteria : ";
                $scope.SearchCriteriaData = [];
                var items = $scope.SearchInput.DisplayType.split('-');

                var str = '';
                for (var i = 0; i < items.length - 1; i++) {
                    $scope.SearchCriteriaData[i] = $scope.totalNames[items[i]];
                }
            }

            if ($scope.SearchInput.companyName != undefined || $scope.SearchInput.companyName != '') {
                $scope.companyName = "Business Name : ";
                $scope.companyNameData = $scope.SearchInput.companyName;
            }

            if ($scope.SearchInput.licenseName != undefined || $scope.SearchInput.licenseName != '') {
                $scope.licenseName = "License Number : ";
                $scope.licenseNameData = $scope.SearchInput.licenseName;
            }

            if ($scope.SearchInput.firstName != undefined || $scope.SearchInput.firstName != '') {
                $scope.firstName = "First Name : ";
                $scope.firstNameData = $scope.SearchInput.firstName;
            }

            if ($scope.SearchInput.lastName != undefined || $scope.SearchInput.lastName != '') {
                $scope.lastName = "Last Name : ";
                $scope.lastNameData = $scope.SearchInput.lastName;
            }

            //-------------------------------------------------------------------

            // Created By       : CodeIT DevTeam
            // Last Update date : 26-07-2015
            // Description      : This Method is navigated to the quick search save page if it is logged in otherwise navigated to the login page.
            // Last Modification:

            //-------------------------------------------------------------------

            $scope.navToQuickSearchSave = function (e) {
                if (!e.ctrlKey) {
                    $rootScope.LoginType = "";
                    localStorage.filterByType = '';
                    if (!authService.authentication.isAuth) {
                        localStorage.path = '/quicksearchsave';
                        window.setTimeout(function () {
                            $('#quicksearchresultmodel .modal-body').html("<h3 class='error_message'>Please login to view your saved Quick Search records.</h3>");
                            $('#quicksearchresultmodel').modal('show');
                        }, 200);
                        return;
                    }
                    $location.path('/quicksearchsave');
                }
            };
        }

        function getSearchData(searchData) {
            $("#dvMainsection").css("display", "none");
            $("#dvLoadingSection").css("display", "block");
            var resp = requestService.GetSearchData(searchData);
            resp.success(function (data) {
                //console.log(data);
                $scope.searchData = data[0].SearchFinalData;
                $scope.LisenceTypeCounts = data[0].SearchCount[0];

                $("#dvMainsection").css("display", "block");
                $("#dvLoadingSection").css("display", "none");
            });
            resp.error(function (data) {
                console.log("Error");
            });
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      :  This method is setting the pagenumber to one when the page navigates from the current page.
        //                        the pagenumber is not set when the page gets redirected to login
        // Last Modification:

        //-------------------------------------------------------------------

        $scope.$on('$routeChangeStart', function (event, next, current) {
            localStorage.removeItem('itemsperpage');
            if (next.originalPath != "/login") {
                localStorage.pagenumber = 1;
                localStorage.removeItem('searchData');
                $rootScope.searchResults = undefined;
            }
            //localStorage.filterByType = '';
        });

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This Method is used to check the right click functionality.
        // Last Modification:

        //-------------------------------------------------------------------

        $scope.checkrightClick = function (url, e) {
            e.preventDefault();
            (e.which === 3) || (e.ctrlKey) ? e.target.href = url : e.target.href = 'javascript:void(0)';
        }

        $scope.setDisabled = function (data, id) {
            if (data == 0) {
                $('#' + id).css('background-color', '#1268ac');
                return true;
            }
            return false;
        }

        $scope.checkEnter = function (e, src) {
            if (e.which == 13) {
                $scope.expandCollapse(e, src);
            }
        }

        $scope.scrollPage = function (id) {
            $('html, body').animate({
                scrollTop: $("#" + id).offset().top
            }, 1000);
        }

        $scope.checkEnterOperations = function (obj, e) {
            if (e.which === 13) {
                if ($((e).target).hasClass('glyphicon-heart-empty')) {
                    $scope.navToSave(obj, $(e.target));
                } else {
                    $scope.navToDelete(obj, $(e.target));
                }
            }
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This Method is used to print the data.
        // Last Modification:

        //-------------------------------------------------------------------

        $scope.printItmes = function () {
            var authData = JSON.parse(localStorage.getItem('ls.authorizationData'));
            var obj = angular.copy($scope.SearchInput);
            angular.forEach($scope.LisenceTypeCounts, function (value, key) {
                obj[key] = value;
            });
            obj.CompanyName = obj.companyName;
            obj.FirstName = obj.firstName;
            obj.LicenseName = obj.licenseName;
            obj.LastName = obj.lastName;
            obj.pageIndex = $scope.currentPage;
            obj.pageSize = $scope.itemPage;
            obj.FilterKeyword = $scope.filterByTypeKeyword;

            var serializedobject = serializeData(obj);

            if (authService.authentication.isAuth) {
                if (authData) {
                    var token = 'Bearer ' + authData.token;
                    var refreshtoken = authData.guiToken;
                    var key = CryptoJS.enc.Utf8.parse('8080808080808080');
                    var iv = CryptoJS.enc.Utf8.parse('8080808080808080');
                    var encryRefreshToken = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(refreshtoken), key,
                    {
                        keySize: 128 / 8,
                        iv: iv,
                        mode: CryptoJS.mode.CBC,
                        padding: CryptoJS.pad.Pkcs7
                    });

                    window.open(appConstants.apiServiceBaseUri + 'api/Download/QuickSearch_GeneratedDocument/?reft=' + encryRefreshToken + '&obj=' + serializedobject, '_self');
                }
            } else {
                window.open(appConstants.apiServiceBaseUri + 'api/Download/QuickSearch_GeneratedDocumentNA/?reft=' + "freeAccess" + '&obj=' + serializedobject, '_self');
            }
        }

        function serializeData(data) {
            // If this is not an object, defer to native stringification.
            if (!angular.isObject(data)) {
                return ((data == null) ? "" : data.toString());
            }

            var buffer = [];

            // Serialize each key in the object.
            for (var name in data) {
                if (!data.hasOwnProperty(name)) {
                    continue;
                }

                var value = data[name];

                buffer.push(
                    encodeURIComponent(name) + "=" + encodeURIComponent((value == null) ? "" : value)
                );
            }

            // Serialize the buffer and clean it up for transportation.
            var source = buffer.join("&").replace(/%20/g, "+");
            return (source);
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Api-Method       : MySavedResults/All
        // Last Update date : 26-07-2015
        // Description      : This Method is shows the saved records of the user.
        // Last Modification:

        //-------------------------------------------------------------------

        //function checkName(key) {
        //    if (key.indexOf('Name') != -1)
        //        return true;
        //    return false;
        //}

        $scope.modal_close = function () {
            $('#quicksearchresultmodel').modal('hide');
            localStorage.path = undefined;
            localStorage.SaveData = null;
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Api-Method       : Search/All
        // Last Update date : 26-07-2015
        // Description      : This Method is used to search the data that is entered in filter text field.
        // Last Modification:

        //-------------------------------------------------------------------

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Api-Method       : MySavedResults/AddToMyList
        // Last Update date : 26-07-2015
        // Description      : This Method is used to save the data to my saved results
        // Last Modification:

        //-------------------------------------------------------------------

        $scope.navToSave = function (item, event) {
            $('#KeyWord').html('');
            /* Check for logged in user */
            if (!authService.authentication.isAuth) {
                localStorage.path = '/quicksearchresult';
                localStorage.setItem('saveData', JSON.stringify(item));
                localStorage.setItem('eventobj', event);
                window.setTimeout(function () {
                    $('#quicksearchresultmodel .modal-body').html("<h3 class='error_message'>Please log in to save your search result.</h3>");
                    $('#quicksearchresultmodel').modal('show');
                }, 200);
                return;
            }
            saveData(item, event);
        };

        $scope.loginRedirect = function () {
            $('#quicksearchresultmodel').modal('hide');
            $rootScope.searchResults = undefined;
            $timeout(function () {
                $location.path('/login');
            }, 400);
            //window.setTimeout(function () {
            //    window.location.hash = '#/login';
            //}, 400);
        }

        $scope.remainSame = function () {
            $("#dvMainsection").css("display", "block");
            $("#dvLoadingSection").css("display", "none");
            $('#quicksearchresultmodel').modal('hide');
            localStorage.removeItem('saveData');
            localStorage.path = undefined;
        }

        function saveData(item, e) {
            if (item != null) {
                item.UserId = item.CreatedBy = localStorage.userId;
                angular.element(document.getElementsByClassName('glyphicon save_ico')).css("pointer-events", "none");
                var response = requestService.AddToMyList(item);
                response.success(function (data) {
                    $scope.SearchInput.Userid = localStorage.userId;

                    if (data.status == "success") {
                        if (localStorage.path == '/quicksearchresult') {
                            popupFactory.showpopup("Selected record is saved successfully to your My Saved Search Results page.", "", { config: { buttons: '1' } });
                            localStorage.path = '';
                        } else {
                            $(e).find('p').html(' Remove');
                            $scope.searchData[parseInt($(e).closest("section").attr("id")) - 1].WishList = true;
                        }
                    } else {
                        localStorage.path = '';
                        popupFactory.showpopup("Selected record is already saved in your My Saved Search Results page.", "", { config: { buttons: '1' } });
                    }
                    angular.element(document.getElementsByClassName('glyphicon save_ico')).css("pointer-events", "auto");
                });
                response.error(function (data) {
                    popupFactory.showpopup("Error occured while saving to the database.", "", { config: { buttons: '1' } });
                    angular.element(document.getElementsByClassName('glyphicon save_ico')).css("pointer-events", "auto");
                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");
                });
            }
        }

        /*
            This method is used to filter with entered text With-Out EntityId
        */
        var re = new Object();
        re.test = function (teststring) {
            teststring = '' + teststring;
            return teststring.toLowerCase().indexOf($scope.filterByTypeKeyword.toLowerCase()) != -1 ? true : false;
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This Method is used for , which fields to be filtered.
        // Last Modification:

        //-------------------------------------------------------------------

        $scope.filterSomeProperties = function (obj) {
            return !$scope.filterByTypeKeyword || re.test(obj.Source) ||
                        re.test(obj.CompanyName) ||

                re.test(obj.LeftNameResultTop) ||
                re.test(obj.LeftNameResultMiddle) ||
                re.test(obj.LeftNameResultBottom) ||
                re.test(obj.MiddleNameResultTop) ||
                re.test(obj.MiddleNameResultMiddle) ||
                re.test(obj.MiddleNameResultBottom) ||
                re.test(obj.RightNameResultTop) ||
                re.test(obj.RightNameResultMiddle1) ||
                re.test(obj.RightNameResultMiddle2) ||
                re.test(obj.RightNameResultBottom) ||
                re.test(obj.ExpantionResult1) ||
                re.test(obj.ExpantionResult2) ||
                re.test(obj.ExpantionResult3) ||
                re.test(obj.ExpantionResult4) ||
                re.test(obj.ExpantionResult5) ||
                re.test(obj.ExpantionResult6) ||
                re.test(obj.SourceFullName) ||
                re.test(obj.LastUpdateDateName) ||
                re.test(obj.LastUpdateDate) ||
                re.test(obj.LeftNameMiddle1Text);
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This Method is used for validating the text entered in text field(i.e. exceeding maximum limit)
        // Last Modification:

        //-------------------------------------------------------------------

        $scope.ShowMessage = function () {
            localStorage.filterByType = '';
            localStorage.pagenumber = 1;
            $scope.currentPage = 1;
            $('#KeyWord').html('');
            if ($('#keywordfield').val().length > 100) {
                $('#KeyWord').html("Your keyword search can be no longer than 100 characters maximum.");
            } else {
                localStorage.filterByType = $('#keywordfield').val();
                if ($scope.previousElement != '') {
                    $scope.previousElement.removeClass('activebtn');
                }
                // $scope.filterByTypeKeyword = localStorage.filterByType;
            }
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Api-Method       : MySavedResults/Deletesingle
        // Last Update date : 26-07-2015
        // Description      : This Method is used to delete the single data.
        // Last Modification:

        //-------------------------------------------------------------------

        $scope.navToDelete = function (data, e) {
            $('#KeyWord').html('');
            data.UserId = localStorage.userId;
            var response = requestService.Deletesingle(data);
            response.success(function (data) {
                $scope.searchData[parseInt($(e).closest("section").attr("id")) - 1].WishList = false;
                $(e).find('p').html(' Save');
                popupFactory.showpopup("Selected record is deleted successfully from your My Saved Search Results page.", "", { config: { buttons: '1' } });
            });
            response.error(function (data) {
                popupFactory.showpopup("Problem in Deleting record.", "", { config: { buttons: '1' } });
            });
        }

        $scope.manageUserList = function (item, e) {
            if ($(e).hasClass('glyphicon-heart-empty')) {
                $scope.navToSave(item, e);
            } else if ($(e).hasClass('glyphicon-heart')) {
                $scope.navToDelete(item, e);
            }
        }

        $scope.labelClick = function (item, e) {
            $scope.manageUserList(item, $(e).parent());
        }

        $scope.navToQuickSearch = function (e) {
            if (!e.ctrlKey) {
                $rootScope.LoginType = "";
                $rootScope.filterByType = undefined;
                $rootScope.filterByType = '';
                $location.path('/quicksearch');
            }
        };

        $scope.selectOption = function () {
            $scope.currentPage = 1;
            $scope.itemPage = $scope.confirmed.num;
            localStorage.setItem('itemsperpage', JSON.stringify($scope.confirmed));
        };

        $scope.sortClick = function () {
            $('#checkboxModal').modal('show');
        }

        $scope.helpClick = function (helpText) {
            $('#helpTextModal .modal-body').html(helpText);
            $('#helpTextModal').modal('show');
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This Method is used to expand the data.
        // Last Modification:

        //-------------------------------------------------------------------

        $scope.expandCollapse = function (event, type) {
            $('#KeyWord').html('');
            if ($(event.target).hasClass('arrow-right')) {
                $(event.target).addClass('arrow-bottom').removeClass('arrow-right');
                $(event.target).parent().parent().css('border-bottom', '0px solid');
                $(event.target).parent().parent().parent().find('.details').show();
            }
            else {
                $(event.target).addClass('arrow-right').removeClass('arrow-bottom');
                $(event.target).parent().parent().parent().find('.details').hide();
            }
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This Method is used for filtering the data by type
        // Last Modification:

        //-------------------------------------------------------------------

        $scope.filterByType = function (type, id) {
            localStorage.pagenumber = 1;
            $scope.currentPage = 1;
            if ($scope.previousElement != '') {
                $scope.previousElement.removeClass('activebtn');
            }
            var clickedElement = angular.element(document.querySelector('#' + id));
            $scope.previousElement = angular.element(document.querySelector('#' + id));
            clickedElement.addClass('activebtn');
            $('#KeyWord').html('');
            localStorage.filterByType = type;
            $scope.filterByTypeKeyword = type;
        }
    }
})();