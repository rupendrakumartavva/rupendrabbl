/**
 * @ngdoc service
 * @name DCRA.TimerFactory
 * @description 
 * # TimerFactory
 * Timer Factory
 */

(function () {

    'use strict';
    angular.module('DCRA').factory('TimerFactory', ['$interval', 'authService', 'requestService', '$location', 'SessionFactory', timerfactory]);
    function timerfactory($interval, authService, requestService, $location, SessionFactory) {

        var timerservice = {};

        var timer, seconds = 0, refreshtokentimer, refreshtokenhit = false;

        timerservice.startTimer = function () {
            localStorage.setItem("refreshtokenhit", "false");
            localStorage.setItem("logouthit", "false");
            timer = $interval(function () {
                if (JSON.parse(localStorage.getItem('ls.authorizationData')) != null && JSON.parse(localStorage.getItem('ls.authorizationData')).freeToken != null) {
                    if (authService.authentication.isAuth && !JSON.parse(localStorage.getItem('ls.authorizationData')).freeToken) {
                        if (Date.now() - localStorage.getItem('lastlyTouched') >= 1800 * 1000) {
                            timerservice.timeExpired();
                        } else if (Date.now() - localStorage.getItem('lastlyTouched') >= 1380 * 1000) {
                            timerservice.showInactivePopup();
                        }
                    } else {
                        timerservice.timeExpired();
                    }
                }
            }, 1000);
        };


        timerservice.update_lastlytouched_time = function () {
            localStorage.setItem('lastlyTouched', Date.now());
        }

        timerservice.timeExpired = function () {
            var authData = JSON.parse(localStorage.getItem('ls.authorizationData'));
            if (authData != null && !authData.freeToken) {
                if (localStorage.getItem("logouthit") == "false") {
                    localStorage.setItem("logouthit", "true");
                    var tokenData = { RefreshTokenId: authData.refreshToken };
                    requestService.UserLogout(tokenData).then(function (data) {
                        localStorage.setItem("logouthit", "false");
                        $('#logoutidalog').modal('hide');
                        authService.logOut();
                        SessionFactory.setSessionAsClear();
                        $interval.cancel(timer);
                        $location.path('/login');
                    });
                }

            } else {
                $('#logoutidalog').modal('hide');
                authService.authentication.isAuth = false;
                authService.authentication.userName = "";
                authService.authentication.useRefreshTokens = false;
                SessionFactory.setSessionAsClear();
                $interval.cancel(timer);
                $location.path('/login');
            }
        };

        timerservice.stopTimer = function () {
            $interval.cancel(timer);
        }

        timerservice.getTokenExpireTime = function () {
            return JSON.parse(localStorage.getItem('ls.authorizationData')).expiresin;
        }

        timerservice.updateTokenExpireTime = function () {
            var token_data = JSON.parse(localStorage.getItem('ls.authorizationData'));
            if (token_data != null && token_data.expiresin != null) {
                token_data.expiresin = token_data.expiresin - 60;
                localStorage.setItem('ls.authorizationData', JSON.stringify(token_data));
            }
        }

        //timerservice.refreshTokenWithTime = function () {
        //    refreshtokentimer = $interval(function () {
        //        if (authService.authentication.isAuth) {
        //            timerservice.updateTokenExpireTime();
        //            if (timerservice.getTokenExpireTime() < 300) {
        //                if (localStorage.getItem("refreshtokenhit") == "false") {
        //                    localStorage.setItem("refreshtokenhit", "true");
        //                    authService.refreshToken().then(function (respose) {
        //                        localStorage.setItem("refreshtokenhit", "false");
        //                        timerservice.refreshTokenWithTime();
        //                    });
        //                    $interval.cancel(refreshtokentimer);
        //                }
        //            }
        //        }
        //    }, 60 * 1000);
        //}

        timerservice.refreshTokenWithTime = function () {
            refreshtokentimer = $interval(function () {
                if (authService.authentication.isAuth && localStorage.getItem("refreshtokenhit") == "false") {
                    if (Date.now() - localStorage.getItem("routechanged") > 27 * 60 * 1000) {
                        localStorage.setItem("refreshtokenhit", "true");
                        authService.refreshToken().then(function (respose) {
                            localStorage.setItem("refreshtokenhit", "false");
                            localStorage.setItem("routechanged", Date.now());
                            timerservice.refreshTokenWithTime();
                        });
                        $interval.cancel(refreshtokentimer);
                    }
                }
            }, 1000);
        }

        //timerservice.watchexpiredin = function () {
        //    var watchedseconds = 0, timercalled = false;
        //    var expiresintime = JSON.parse(localStorage.getItem('ls.authorizationData')).expiresin;
        //    console.log(expiresintime);
        //    var watchexpiredin = $interval(function () {
        //        watchedseconds++;
        //        if (watchedseconds > 20 && JSON.parse(localStorage.getItem('ls.authorizationData')).expiresin == expiresintime && !timercalled) {
        //            timerservice.refreshTokenWithTime();
        //            timercalled = true;
        //            watchedseconds = 0;
        //        }
        //    }, 1000);
        //}

        timerservice.generatefreetoken = function () {
            $interval(function () {
                if (!authService.authentication.isAuth)
                    authService.freeToken();
            }, 23 * 60 * 1000);
        }

        timerservice.showInactivePopup = function () {
            $('#logoutidalog .modal-body').html("<h3 class='success'>You have been inactive for 23 minutes. In 7 minutes you will be automatically logged out</h3>");
            $('#logoutidalog').modal('show');
            $("#logoff").click(function (e) {
                timerservice.timeExpired();
            });
            $("#continue").click(function (e) {
                $('#logoutidalog').modal('hide');
                timerservice.update_lastlytouched_time();
            });
        }

        return timerservice;
    };
})();