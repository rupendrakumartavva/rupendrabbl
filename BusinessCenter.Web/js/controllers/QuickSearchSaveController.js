(function () {
    'use strict';

    var controllerId = 'QuickSearchSaveController';
    angular.module('DCRA').controller(controllerId, ['$scope', '$rootScope', '$location', 'requestService', '$window', 'appConstants', 'authService', QuickSearchSaveController]);

    function QuickSearchSaveController($scope, $rootScope, $location, requestService, $window, appConstants, authService) {
        $('#routeChangeMessage').hide();
        $('#messagePopup').show();
        $scope.contentSection = [
         { cId: 'c01', num: 10 },
         { cId: 'c02', num: 25 },
         { cId: 'c03', num: 50 }
        ];
        $scope.chcekboxSort = false;
        $scope.currentPage = 1;
        $scope.pageSize = 10;
        $scope.savedItems = [];
        $scope.previousElement = '';
        $scope.itemPage = 10;
        $scope.confirmed = {};
        $scope.deleteselected = 0;
        $scope.filterByTypeKeyword = '';
        $scope.Wish = {};
        $scope.Wish.pageSize = 10;
        $scope.Wish.pageIndex = 1;
        $scope.Wish.UserId = localStorage.userId;
        $scope.Wish.DisplayType = "All";
        var cnt = 0, i, popup_flag = 0, paginationno = '';
        init();

        /*
        * Function: init
        * init (initialize) method: first method to be executed on controller load.
        */

        function init() {
            $("#dvLoadingSection").css("display", "block");
            $("#dvMainsection").css("display", "none");
            authService.refreshToken().then(function () {
                jQuery('[data-toggle="tooltip"]').tooltip();

                $('.save_error_msg').hide();
                getTotalRecordsCount();
                if (localStorage.pagenumber != undefined) {
                    $scope.currentPage = localStorage.pagenumber;
                }

                if (localStorage.getItem('itemsperpage') != null) {
                    var itemsperpage = JSON.parse(localStorage.getItem('itemsperpage'));
                    $scope.confirmed = $scope.contentSection[parseInt(itemsperpage.cId.substring(2)) - 1];
                    //console.log($scope.confirmed);
                    $scope.itemPage = itemsperpage.num;
                } else {
                    $scope.confirmed = $scope.contentSection[0];
                    $scope.itemPage = 10;
                }
                if (localStorage.filterByType != undefined) {
                    $scope.filterByTypeKeyword = localStorage.filterByType;
                }
                jQuery("input[type='checkbox'], input[type='radio']").uniform();
            }, function () {
                $location.path("/login");
            });
            //$("#divData").css("display", "block");
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
                $scope.navToRemove(obj, e);
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

                var obj = {};
                obj.testid = localStorage.userId;
                obj.UserId = localStorage.userId;
                obj.pageIndex = $scope.currentPage;
                obj.pageSize = $scope.itemPage;
                obj.DisplayType = $scope.filterByTypeKeyword;
                if (obj.DisplayType == null || obj.DisplayType == "") {
                    obj.DisplayType = "All";
                }
                var serializedobject = serializeData(obj);
                //console.log(serializedobject)
                window.open(appConstants.apiServiceBaseUri + 'api/Download/QuickSearchSave_GeneratedDocument/?reft=' + encryRefreshToken + '&obj=' + serializedobject, '_self');
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
        // Last Update date : 26-07-2015
        // Description      : This method is used for checking and setting the page number when ever the page is being redirected from the QSS page
        // Last Modification:

        //-------------------------------------------------------------------

        $scope.$on('$routeChangeSuccess', function (event, current, previous) {
            if (previous != undefined) {
                if (previous.originalPath == '/quicksearchresult') {
                    localStorage.pagenumber = 1;
                    $scope.currentPage = 1;
                }
            }
        });

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This method is setting the pagenumber to one when the page navigates from the current page
        // Last Modification:

        //-------------------------------------------------------------------

        $scope.$on('$locationChangeStart', function (event, next, current) {
            localStorage.pagenumber = 1;
            localStorage.removeItem('itemsperpage');
            localStorage.filterByType = '';
        });

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
            }
        };

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Api-Method       : MySavedResults/SelectAll
        // Last Update date : 26-07-2015
        // Description      : This Method is used to get the total record count.
        // Last Modification:

        //-------------------------------------------------------------------

        function getTotalRecordsCount() {
            var respgtotal = requestService.GetSaveCountData($scope.Wish);
            respgtotal.success(function (totalcount) {
                mySavedRecords();
                $scope.SavedLisenceTypeCounts = totalcount;
            });
            respgtotal.error(function (datatotal) {
                console.log("Error");
            });
        }

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Api-Method       : MySavedResults/All
        // Last Update date : 26-07-2015
        // Description      : This Method is used to get the total data of the saved records.
        // Last Modification:

        //-------------------------------------------------------------------

        function mySavedRecords() {
            var respgtotal = requestService.GetSaveData($scope.Wish);
            respgtotal.success(function (data) {
                $scope.savedItems = data;
                //console.log(JSON.stringify($scope.savedItems));
                //for (var i = 0; i < $scope.savedItems.length; i++) {
                //    $scope.savedItems[i].id = i + 1;
                //}
                var count = parseInt($scope.savedItems.length);
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
                if (parseInt(count) >= 1) {
                    $('#noResultsdata').css('display', 'none');
                    $('#noResultsKey').css('display', 'block');
                    $('#btnPrint').css('display', 'block');
                    $('#myList').css('display', 'block');
                    //$('#lblShow').css('display', 'block');
                } else {
                    $('#noResultsdata').css('display', 'block');
                    $('#noResultsKey').css('display', 'none');
                    $('#btnPrint').css('display', 'none');
                    $('#myList').css('display', 'none');
                    //$('#lblShow').css('display', 'none');
                }

                if ($scope.savedItems.length == 0) {
                    $('.save_error_msg').show();
                }
            });
            respgtotal.error(function (datatotal) {
                console.log("Error");
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            });
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
        // Api-Method       : MySavedResults/Deletesingle
        // Last Update date : 26-07-2015
        // Description      : This Method is used to delete the single data.
        // Last Modification:

        //-------------------------------------------------------------------

        $scope.navToRemove = function (record, event, src, secid) {
            if (!/Android|webOS|iPhone|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
                $('.checker span').css('left', '50px');
            }
            $('.checker span').css('top', '7px');
            $('#KeyWord').html('');
            $('#routeChangeMessage').hide();
            $('#messagePopup').show();
            record.UserId = record.CreatedBy = localStorage.userId;
            $scope.record = record;
            $scope.event = event;
            $scope.source = src;
            $scope.sectionid = secid;
            if (localStorage.showMessagePopup == undefined || localStorage.showMessagePopup == "false" || localStorage.showMessagePopup == false || localStorage.showMessagePopup == 'undefined') {
                $("#dvMainsection").css("display", "block");
                $("#dvLoadingSection").css("display", "none");
                $('#delete_message').modal('show');
            } else {
                $scope.delete_record();
            }
        }

        $scope.lableClick = function (record, event, src, secid) {
            if ($(event).hasClass('heart_text1'))
                $scope.navToRemove(record, $(event).parent(), src, secid);
        }
        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Api-Method       : MySavedResults/Deletesingle
        // Last Update date : 26-07-2015
        // Description      : This Method is used to delete the single data.
        // Last Modification:

        //-------------------------------------------------------------------

        $scope.delete_record = function () {
            $('#KeyWord').html('');
            angular.element(document.getElementsByClassName('glyphicon save_ico')).css("pointer-events", "none");
            var resp = requestService.Deletesingle($scope.record);
            resp.success(function (data) {
                $($scope.event).closest("section").remove();
                $scope.SavedLisenceTypeCounts[0].RecordCount = $scope.SavedLisenceTypeCounts[0].RecordCount - 1;
                $scope.SavedLisenceTypeCounts[0][$scope.source + "Count"] = $scope.SavedLisenceTypeCounts[0][$scope.source + "Count"] - 1;

                for (var i = 0; i < $scope.savedItems.length; i++) {
                    if ($scope.savedItems[i].id == $scope.sectionid) {
                        paginationno = $scope.savedItems.indexOf($scope.savedItems[i]);
                        $scope.savedItems.splice(paginationno, 1);
                        break;
                    }
                }
                var count = parseInt($scope.savedItems.length);

                if (parseInt(count) >= 1) {
                    $('#noResultsdata').css('display', 'none');
                    $('#noResultsKey').css('display', 'block');
                    $('#btnPrint').css('display', 'block');
                    $('#myList').css('display', 'block');
                } else {
                    $('#noResultsdata').css('display', 'block');
                    $('#noResultsKey').css('display', 'none');
                    $('#btnPrint').css('display', 'none');
                    $('#myList').css('display', 'none');
                }
                angular.element(document.getElementsByClassName('glyphicon save_ico')).css("pointer-events", "auto");
            });
            resp.error(function (data) {
                angular.element(document.getElementsByClassName('glyphicon save_ico')).css("pointer-events", "auto");
                console.log("Error");
                $("#dvLoadingSection").css("display", "none");
                $("#dvMainsection").css("display", "block");
            });
        }

        $scope.cancel_delete = function () {
            localStorage.showMessagePopup = false;
            $scope.deleteselected = 0;
            $("#delete_status").attr('checked', false);
            $('input:checkbox').removeAttr('checked');
            $('#uniform-delete_status span').removeClass('checked');
            $("#dvLoadingSection").css("display", "none");
            $("#dvMainsection").css("display", "block");
            $('#delete_message').modal('hide');
        }

        $('#delete_status').change(function () {
            localStorage.showMessagePopup = $(this).is(":checked") ? true : false;
        });

        $scope.navToQuickSearch = function () {
            $rootScope.LoginType = "";
            $location.path('/quicksearch');
        };

        $scope.navToQuickSearchResult = function () {
            $location.path('/quicksearchresult')
        };

        $scope.selectOption = function () {
            $scope.currentPage = 1;
            $scope.itemPage = $scope.confirmed.num;
            localStorage.setItem('itemsperpage', JSON.stringify($scope.confirmed));
        };

        $scope.navToModal = function (itemId) {
            $('#cancelModal' + itemId + ' .modal-body').html("<h3 class='error_message'>Please confirm that you would like to remove this saved search result from your page</h3>");
            $('#cancelModal' + itemId).modal('show');
        };

        $scope.sortClick = function () {
            $('#checkboxModal').modal('show');
        };

        //-------------------------------------------------------------------

        // Created By       : CodeIT DevTeam
        // Last Update date : 26-07-2015
        // Description      : This Method is used for filtering the data by type
        // Last Modification:

        //-------------------------------------------------------------------

        $scope.filterByType = function (type, id) {
            if ($scope.previousElement != '') {
                $scope.previousElement.removeClass('activebtn');
            }
            var clickedElement = angular.element(document.querySelector('#' + id));
            $scope.previousElement = angular.element(document.querySelector('#' + id));
            clickedElement.addClass('activebtn');
            $('#KeyWord').html('');
            localStorage.filterByType = type;
            $scope.filterByTypeKeyword = type;
            $scope.currentPage = 1;
        }

        $scope.setChcekboxSort = function (sortValue) {
            $scope.chcekboxSort = sortValue;
        };

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
        };
    }
})();