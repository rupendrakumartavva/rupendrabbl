(function () {
    'use strict';

    var controllerId = 'QuickSearchController';
    angular.module('DCRA').controller(controllerId,
		['$scope', '$rootScope', '$location', 'requestService', '$parse', '$http', 'authService', QuickSearchController]);

    function QuickSearchController($scope, $rootScope, $location, requestService, $parse, $http, authService) {

        $scope.selectedCheckBoxes = {};
        $rootScope.Searchinput = {};
        $scope.search = {};
        $scope.Company = '';
        $scope.FirstName = '';
        $scope.LastName = '';
        $scope.sendRequest = true;
        $scope.suggestionFlag = 0;
        var suggestionKey = '';
        var autoSuggestCount = 0;
        var autoSuggestKey = '';
        var previousname = '';
        var divids = {
            "FirstName": "firstName",
            "LastName": "lastName",
            "Company": "companyName"
        };

        var displayNames = {
            "firstName": "First Name",
            "lastName": "Last Name"
        };

        var namesReg = /^[a-zA-Z0-9 !#$%&*'"~`=+?;@()///,.:_-]+$/;
        var licenseReg = /^[a-zA-Z0-9 !#&*'"~`=+?;@()///,._-]+$/;
        var repeatcount = 0;
        var stringlength = 0;
        var temp = 0;

        $('#Company').hide();
        $('#FirstName').hide();
        $('#LastName').hide();





        init();

        /*
       * Function: init
       * init (initialize) method: first method to be executed on controller load. 
       */
        function init() {
            $("#dvLoadingSection").css("display", "block");
            $("#dvMainsection").css("display", "none");
            if (authService.authentication.isAuth) {
                authService.refreshToken().then(function () {
                    $("#submit_search").show();
                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");
                }, function () {
                    $location.path("/login");
                });
            } else {
                authService.freeToken().then(function () {
                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");
                }, function () {
                    $("#dvLoadingSection").css("display", "none");
                    $("#dvMainsection").css("display", "block");
                });
            }

            localStorage.removeItem('saveData');
            localStorage.path = undefined;
            localStorage.filterByType = '';
            $scope.disablecheckbox = false;
            $scope.enableButton = false;
        }


        $scope.setErrorMsg = function (id) {
            $('#' + id).html('');
            $('#error_msgforChcekBox').html('');
            $('#licenseName').html('');
            $('#error_msg').html('');
        }
        //$('#error_msg').html('No Records Found');

        $scope.checktabpress = function (id) {
            $('#' + id).hide();
        }



        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This Method is used for validating license name field 
        // Last Modification: 

        //-------------------------------------------------------------------

        function checklicenseName(license) {
            $scope.sfendRequest = true;
            if (license != '') {
                if (!licenseReg.test(license)) {
                    var str = license;
                    $('#licenseName').html('');
                    if (str.indexOf('*') != -1) {
                        $scope.sendRequest = false;
                        $('#licenseName').html('You cannot execute a wild card search for licenses').focus();
                    } else if (str.indexOf(' ') != -1 || str.indexOf(',') != -1) {
                        $scope.sendRequest = false;
                        $('#licenseName').html('License field can not contain any spaces or commas').focus();
                    } else {
                        $scope.sendRequest = false;
                        $('#licenseName').html('You cannot enter special characters in the license field').focus();
                    }
                } else {
                    if (license.length < 3) {
                        $scope.sendRequest = false;
                        $('#licenseName').html('Your search should be atleast 3 characters long').focus();
                    } else if (license.length > 100) {
                        $scope.sendRequest = false;
                        $('#licenseName').html('Your search can be no longer than 100 characters maximum').focus();
                    }
                }
            }
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This Method is used for validating text fields when Astric is being typed  
        // Last Modification: 

        //-------------------------------------------------------------------


        function checkforAstric(givenString, name) {
            $scope.sendRequest = true;
            repeatcount = 0;
            if (givenString.indexOf('*') != -1) {
                $scope.sendRequest = true;
                for (var i = 0; i < givenString.length; i++) {
                    if (givenString[i] == '*') {
                        repeatcount++;
                    }
                }

                if (repeatcount == givenString.length) {
                    $('#' + name).html('You cannot enter a wildcard  in the middle of a phrase').focus();
                    repeatcount = 0;
                    $scope.sendRequest = false;
                }

                if (repeatcount == 1) {

                    if (!(givenString.lastIndexOf('*') == givenString.length - 1 || givenString.lastIndexOf('*') == 0)) {
                        $scope.sendRequest = false;
                        $('#' + name).html('You cannot enter a wildcard  in the middle of a phrase').focus();
                        repeatcount = 0;
                    }
                    else {
                        if (givenString.length <= 3) {
                            $scope.sendRequest = false;
                            $('#' + name).html('You must enter at least 3 characters when conducting a wild card search').focus();
                        }
                    }
                }
                if (repeatcount == 2) {
                    if (givenString.indexOf('*') == 0 && givenString.lastIndexOf('*') == givenString.length - 1) {
                        if (givenString.length == 4 || givenString.length == 3) {
                            $scope.sendRequest = false;
                            $('#' + name).html('You must enter at least 3 characters when conducting a wild card search').focus();
                        } else {
                            $('#' + name).html('');
                            $scope.sendRequest = true;
                        }
                    } else {
                        $scope.sendRequest = false;
                        $('#' + name).html('You cannot enter a wildcard  in the middle of a phrase').focus();
                    }
                    repeatcount = 0;
                }

                if (repeatcount >= 3) {
                    $scope.sendRequest = false;
                    $('#' + name).html('You cannot enter a wildcard  in the middle of a phrase').focus();
                    repeatcount = 0;
                }
            }
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This Method is used for validating text fields.  
        // Last Modification: 

        //-------------------------------------------------------------------


        $scope.search_validate = function (search) {
            temp = 0;
            $scope.sendRequest = true;
            $('#error_msg').html('');
            if (Object.keys(search).length > 0) {
                for (var i = 0; i < Object.keys(search).length; i++) {
                    var name = Object.keys(search)[i];

                    if ($scope.search[name] != '') {
                        if (name == "licenseName") {
                            checklicenseName($scope.search[name]);
                        } else {
                            if (!namesReg.test($scope.search[name])) {
                                var name_test = name == 'companyName' ? 'Business Name' : displayNames[name];
                                var str = 'You cannot enter special characters in the ' + name_test + ' field';
                                $('#' + name).html(str).focus();
                                $scope.sendRequest = false;
                            } else {
                                if (($scope.search[name]).indexOf('*') != -1) {
                                    checkforAstric($scope.search[name], name);
                                } else {
                                    if ($scope.search[name].length < 3) {
                                        $scope.sendRequest = false;
                                        $('#' + name).html('Your search should be atleast 3 characters long').focus();
                                    }
                                    if ($scope.search[name].length > 100) {
                                        $scope.sendRequest = false;
                                        $('#' + name).html('Your search can be no longer than 100 characters maximum').focus();
                                    }
                                }
                            }
                        }
                    } else {
                        temp++;
                    }
                }

                if (temp == Object.keys(search).length) {
                    $scope.sendRequest = false;
                    $('#error_msg').html('Please input search information into one of the search fields below').focus();
                    temp = 0;
                } else {
                    navTocheckLicense();
                    BindData();
                }

            } else {
                $('#error_msg').html('Please input search information into one of the search fields below').focus();
            }
            //if ((($scope.search.companyName != '') && ($scope.search.companyName != undefined)) || (($scope.search.licenseName != '') && ($scope.search.licenseName != undefined)) || (($scope.search.firstName != '') && ($scope.search.firstName != undefined))
            //    || (($scope.search.lastName != '') && ($scope.search.lastName != undefined))) {
            //    console.log("asdfdssd");

            //} else {
            //    $('#error_msg').html('Please input search information into one of the search fields below').focus();
            //}
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This Method is used for validating checkbox.   
        // Last Modification: 

        //-------------------------------------------------------------------

        function navTocheckLicense() {
            if ((($scope.BBL == undefined) || ($scope.BBL == false))
                && (($scope.CORP == undefined) || ($scope.CORP == false))
                && (($scope.OPLA == undefined) || ($scope.OPLA == false))
                && (($scope.CBE == undefined) || ($scope.CBE == false))
                && (($scope.ABRA == undefined) || ($scope.ABRA == false))
                && (($scope.selectAll == undefined) || ($scope.selectAll == false))) {
                $('#error_msgforChcekBox').html("Please select a license type").focus();
                $scope.sendRequest = false;
            }
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Api-Method       : Search/' + name, searchKey
        // Last Update date : 26-07-2015
        // Description      : This Method is used to get suggestions for wild card search(i.e.autofill concept). 
        // Last Modification: 

        //-------------------------------------------------------------------


        $scope.getSuggestions = function (key, name) {
            $('#error_msg').html('');
            if (key.length == 0)
                $('#' + name).hide();

            if (key.length < 3) {
                switch (name) {
                    case 'Company':
                        $scope.Company = [];
                        break;
                    case 'FirstName':
                        $scope.FirstName = [];
                        break;
                    case 'LastName':
                        $scope.LastName = [];
                        break;
                }
            }

            $('#' + divids[name]).html('');
            if (previousname != name) {
                autoSuggestKey = '';
            }
            autoSuggestCount = key.length;
            if (key.length != 0) {
                $scope.enableLicense = true;
            } else {
                if ((($scope.search.companyName != '') && ($scope.search.companyName != undefined)) || (($scope.search.firstName != '') && ($scope.search.firstName != undefined))
                    || (($scope.search.lastName != '') && ($scope.search.lastName != undefined))) {
                    $scope.enableLicense = true;
                } else {
                    (key.length != 0) ? $scope.enableLicense = true : $scope.enableLicense = false;
                }
            }
            (key.indexOf('*') != -1) ? autoSuggestCount = key.length - 1 : autoSuggestCount = key.length;
            if (autoSuggestCount == 2) {
                $('#' + name).show();
                $scope.dataavail = false;
                var resp = requestService.Autofill({ SearchKeyWord: key }, name);
                resp.success(function (data) {

                    switch (name) {
                        case 'Company':
                            $scope.Company = data;
                            $scope.dataavail = true;
                            break;
                        case 'FirstName':
                            $scope.FirstName = data;
                            $scope.dataavail = true;
                            break;
                        case 'LastName':
                            $scope.LastName = data;
                            $scope.dataavail = true;
                            break;
                    }
                    $('#' + name).hide();
                    autoSuggestKey = key;
                    previousname = name;
                });
                resp.error(function (data) {
                    console.log("Error");
                });
            }
        }


        $scope.$on('$locationChangeStart', function (event, next, current) {
            localStorage.pagenumber = 1;
        });


        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This Method is used for validating checkbox and if selectAll checkbox is selected then other checkboxes should become disable  
        // Last Modification: 

        //-------------------------------------------------------------------

        $scope.check_checkBox = function (name, value) {
            $('#error_msgforChcekBox').html('');
            if (name == "All") {
                var keys = Object.keys($scope.selectedCheckBoxes);
                for (var i = 0; i < keys.length; i++) {
                    $parse(keys[i]).assign($scope, false);
                }
                $scope.selectedCheckBoxes = {};
                $scope.disablecheckbox = value;
                if (value == true)
                    $scope.disablecheckbox = true;
            }
            if (value) {
                $scope.selectedCheckBoxes[name] = value;

            } else {
                delete $scope.selectedCheckBoxes[name];
            }
            $scope.enableButton = value;
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This Method is used for disabling the license field if one of the remaining fields are used. 
        // Last Modification: 

        //-------------------------------------------------------------------

        $scope.disableOtherThanLicense = function () {
            $('#error_msgforChcekBox').html('');
            $('#licenseName').html('');
            if ($scope.search.licenseName.length >= 1) {

                $("#searchitemid").prop('disabled', true);
                $("#searchitemln").prop('disabled', true);
                $("#searchitemfn").prop('disabled', true);
                $('#licenseName').html('');
                $('#firstname').html('');
                $('#lastname').html('');
            } else {
                $('#error_msg').html('');
                $("#searchitemid").prop('disabled', false);
                $("#searchitemln").prop('disabled', false);
                $("#searchitemfn").prop('disabled', false);
            }
        };

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Api-Method       : Search/SelectAll
        // Last Update date : 26-07-2015
        // Description      : This Method is used for binding the data to the wild card search and is shown in QSR page.
        // Last Modification: 

        //-------------------------------------------------------------------

        function BindData() {
            if ($scope.sendRequest == true) {
                $("#dvMainsection").css("display", "none");
                $("#dvLoadingSection").css("display", "block");
                $scope.search.pageSize = 10;
                $scope.search.pageIndex = 1;
                $scope.search.isChanged = false;
                if (localStorage.userId == undefined)
                { $scope.search.Userid = 0; }
                else { $scope.search.Userid = localStorage.userId; }
                $scope.search.SearchType = '';

                var flag = 0;
                $.each($scope.selectedCheckBoxes, function (name, value) {
                    if (flag == 0) {
                        $scope.search.SearchType = $scope.search.DisplayType = '';
                    }
                    $scope.search.SearchType = $scope.search.SearchType + name + '-';
                    $scope.search.DisplayType = $scope.search.SearchType;
                    flag++;
                });
                $rootScope.InitailSearch = $scope.search.SearchType;
                $scope.search.KeyType = "search";
                localStorage.setItem('searchData', JSON.stringify($scope.search));
                getSearchData(JSON.parse(localStorage.getItem('searchData')));

            }
        }

        function getSearchData(searchData) {
            $("#dvLoadingSection").css("display", "block");
            var resp = requestService.GetSearchData(searchData);
            resp.success(function (data) {
                if (data[0].SearchCount[0].RecordCount == 0) {
                    $("#dvMainsection").css("display", "block");
                    $("#dvLoadingSection").css("display", "none");
                    $('#error_msg').html('No Results Found');
                    $scope.search = {};
                    $scope.BBL = $scope.CBE = $scope.CORP = $scope.ABRA = $scope.selectAll = $scope.OPLA = undefined;
                    $scope.disablecheckbox = false;
                    $scope.selectedCheckBoxes = {};
                    $("#searchitemid").prop('disabled', false);
                    $("#searchitemln").prop('disabled', false);
                    $("#searchitemfn").prop('disabled', false);
                    $scope.enableLicense = false;
                    $('input:checkbox').removeAttr('checked');
                } else {
                    $rootScope.searchResults = data;
                    $location.path('/quicksearchresult');
                }


            });
            resp.error(function (data) {
                console.log("Error");
            });
        }


        $scope.navToQuickSearchSave = function () {
            $location.path('/quicksearchsave');
        };

    }
})();