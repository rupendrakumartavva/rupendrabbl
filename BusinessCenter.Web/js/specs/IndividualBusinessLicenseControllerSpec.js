describe('Individual Business Controller', function () {
    var $scope, controller, httpBackend, mockservice, indiBussController, localStore, appconstants, basePath, routeparams, utilityfactory, bblsubmissionfactory, compile, sessionfactory, popupfactory, errorfactory;
    var authservice, localstorageservice;
    beforeEach(module('DCRA'));
    beforeEach(inject(function ($controller, $rootScope, _$location_, requestService, $httpBackend, appConstants, $routeParams,
        UtilityFactory, BBLSubmissionFactory, $compile, SessionFactory, popupFactory, errorFactory, authService, localStorageService) {
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
        appconstants = appConstants;
        basePath = appconstants.apiServiceBaseUri;
        routeparams = $routeParams;
        routeparams.guid = 'guid';
        utilityfactory = UtilityFactory;
        compile = $compile;
        bblsubmissionfactory = BBLSubmissionFactory;
        sessionfactory = SessionFactory;
        popupfactory = popupFactory;
        errorfactory = errorFactory;
        authservice = authService;
        localstorageservice = localStorageService;
    }));


    var individualdata = [{
        City: "dfs",
        CompanyBusinessLicense: "302012000019",
        CompanyName: "WASTE MANAGEMENT OF MARYLAND INC",
        Country: "dsf",
        DateofBirth: "11/02/1994",
        ExpirationDate: "12/04/2015",
        EyeColor: "asdf",
        FirstName: "sd",
        HairColor: "dsaf",
        Height: "12",
        HeightIn: "12",
        IdentificationCard: "asdf",
        LastName: "dsf",
        MasterId: "2365a487-dd42-457c-b13f-264f311cfe6b",
        MiddleName: "sd",
        State_Province: "dsf",
        StateofIssuance: "afdsf",
        UserId: "D0A8903E-AD68-4E09-80BC-DC9C31FBBDE2",
        Weight: "12"
    }], submissionstatus_data = { "Status": "Draft", "MasterId": "2a15f952-2c93-41f7-806e-21bbcf7f2571", "TradeName": "NA", "BusinessStructure": "SOLE PROPRIETORSHIP", "IsCorporationDivision": false, "IsCoporateRegistration": true, "IsResidentAgent": false, "IsIndividual": true, "IsBusinessMustbeinDc": true, "AppType": "I", "IsFEIN": false, "DocSubmType": "", "PaymentId": null, "PaymentStatus": null, "CorporationStatus": "ACTIVE", "BusinessOwnerName": "test", "CurrentYear": "2016", "CreatedDate": "03/21/2016", "SelectedMailType": "", "PremisesAddress": "1234  28TH Street NW   Washington District of Columbia United States 20007", "BusinessName": "test", "IsCategorySelfCertification": false },
    binddropdown_data = { "StreetList": [{ "StreetType": "Alley", "StreetCode": "AL" }, { "StreetType": "Avenue", "StreetCode": "AVE" }, { "StreetType": "Boulevard", "StreetCode": "BLVD" }, { "StreetType": "Bridge", "StreetCode": "BRG" }, { "StreetType": "Circle", "StreetCode": "CIR" }, { "StreetType": "Court", "StreetCode": "CT" }, { "StreetType": "Crescent", "StreetCode": "CRES" }, { "StreetType": "Drive", "StreetCode": "DR" }, { "StreetType": "Driveway", "StreetCode": "DRWY" }, { "StreetType": "Expressway", "StreetCode": "EXPY" }, { "StreetType": "Freeway", "StreetCode": "FWY" }, { "StreetType": "Gardens", "StreetCode": "GDNS" }, { "StreetType": "Green", "StreetCode": "GRN" }, { "StreetType": "Kys", "StreetCode": "KYS" }, { "StreetType": "Lane", "StreetCode": "LN" }, { "StreetType": "Mews", "StreetCode": "MEWS" }, { "StreetType": "Parkway", "StreetCode": "PKWY" }, { "StreetType": "Place", "StreetCode": "PL" }, { "StreetType": "Plaza", "StreetCode": "PLZ" }, { "StreetType": "Promenade", "StreetCode": "PROM" }, { "StreetType": "Road", "StreetCode": "RD" }, { "StreetType": "Row", "StreetCode": "ROW" }, { "StreetType": "Square", "StreetCode": "SQ" }, { "StreetType": "Street", "StreetCode": "ST" }, { "StreetType": "Terrace", "StreetCode": "TER" }, { "StreetType": "Walk", "StreetCode": "WALK" }, { "StreetType": "Way", "StreetCode": "WAY" }], "CountryList": [{ "CountryCode": "AF", "CountryName": "Afghanistan", "Status": true }, { "CountryCode": "AX", "CountryName": "Aland Islands", "Status": true }, { "CountryCode": "AL", "CountryName": "Albania", "Status": true }, { "CountryCode": "DZ", "CountryName": "Algeria", "Status": true }, { "CountryCode": "AS", "CountryName": "American Samoa", "Status": true }, { "CountryCode": "AD", "CountryName": "Andorra", "Status": true }, { "CountryCode": "AO", "CountryName": "Angola", "Status": true }, { "CountryCode": "AI", "CountryName": "Anguilla", "Status": true }, { "CountryCode": "AQ", "CountryName": "Antarctica", "Status": true }, { "CountryCode": "AG", "CountryName": "Antigua and Barbuda", "Status": true }, { "CountryCode": "AR", "CountryName": "Argentina", "Status": true }, { "CountryCode": "AM", "CountryName": "Armenia", "Status": true }, { "CountryCode": "AW", "CountryName": "Aruba", "Status": true }, { "CountryCode": "AU", "CountryName": "Australia", "Status": true }, { "CountryCode": "AT", "CountryName": "Austria", "Status": true }, { "CountryCode": "AZ", "CountryName": "Azerbaijan", "Status": true }, { "CountryCode": "BS", "CountryName": "Bahamas", "Status": true }, { "CountryCode": "BH", "CountryName": "Bahrain", "Status": true }, { "CountryCode": "BD", "CountryName": "Bangladesh", "Status": true }, { "CountryCode": "BB", "CountryName": "Barbados", "Status": true }, { "CountryCode": "BY", "CountryName": "Belarus", "Status": true }, { "CountryCode": "BE", "CountryName": "Belgium", "Status": true }, { "CountryCode": "BZ", "CountryName": "Belize", "Status": true }, { "CountryCode": "BJ", "CountryName": "Benin", "Status": true }, { "CountryCode": "BM", "CountryName": "Bermuda", "Status": true }, { "CountryCode": "BT", "CountryName": "Bhutan", "Status": true }, { "CountryCode": "BO", "CountryName": "Bolivia", "Status": true }, { "CountryCode": "BA", "CountryName": "Bosnia and Herzegovina", "Status": true }, { "CountryCode": "BW", "CountryName": "Botswana", "Status": true }, { "CountryCode": "BV", "CountryName": "Bouvet Island", "Status": true }, { "CountryCode": "BR", "CountryName": "Brazil", "Status": true }, { "CountryCode": "IO", "CountryName": "British Indian Ocean Territory", "Status": true }, { "CountryCode": "VG", "CountryName": "British Virgin Islands", "Status": true }, { "CountryCode": "BN", "CountryName": "Brunei", "Status": true }, { "CountryCode": "BG", "CountryName": "Bulgaria", "Status": true }, { "CountryCode": "BF", "CountryName": "Burkina Faso", "Status": true }, { "CountryCode": "BI", "CountryName": "Burundi", "Status": true }, { "CountryCode": "KH", "CountryName": "Cambodia", "Status": true }, { "CountryCode": "CM", "CountryName": "Cameroon", "Status": true }, { "CountryCode": "CA", "CountryName": "Canada", "Status": true }, { "CountryCode": "CV", "CountryName": "Cape Verde", "Status": true }, { "CountryCode": "KY", "CountryName": "Cayman Islands", "Status": true }, { "CountryCode": "CF", "CountryName": "Central African Republic", "Status": true }, { "CountryCode": "TD", "CountryName": "Chad", "Status": true }, { "CountryCode": "CL", "CountryName": "Chile", "Status": true }, { "CountryCode": "CN", "CountryName": "China", "Status": true }, { "CountryCode": "CX", "CountryName": "Christmas Island", "Status": true }, { "CountryCode": "CC", "CountryName": "Cocos Islands", "Status": true }, { "CountryCode": "CO", "CountryName": "Colombia", "Status": true }, { "CountryCode": "KM", "CountryName": "Comoros", "Status": true }, { "CountryCode": "CG", "CountryName": "Congo", "Status": true }, { "CountryCode": "CK", "CountryName": "Cook Islands", "Status": true }, { "CountryCode": "CR", "CountryName": "Costa Rica", "Status": true }, { "CountryCode": "CI", "CountryName": "Cote d'Ivoire", "Status": true }, { "CountryCode": "HR", "CountryName": "Croatia", "Status": true }, { "CountryCode": "CU", "CountryName": "Cuba", "Status": true }, { "CountryCode": "CY", "CountryName": "Cyprus", "Status": true }, { "CountryCode": "CZ", "CountryName": "Czech Republic", "Status": true }, { "CountryCode": "DK", "CountryName": "Denmark", "Status": true }, { "CountryCode": "DJ", "CountryName": "Djibouti", "Status": true }, { "CountryCode": "DM", "CountryName": "Dominica", "Status": true }, { "CountryCode": "DO", "CountryName": "Dominican Republic", "Status": true }, { "CountryCode": "EC", "CountryName": "Ecuador", "Status": true }, { "CountryCode": "EG", "CountryName": "Egypt", "Status": true }, { "CountryCode": "SV", "CountryName": "El Salvador", "Status": true }, { "CountryCode": "GQ", "CountryName": "Equatorial Guinea", "Status": true }, { "CountryCode": "ER", "CountryName": "Eritrea", "Status": true }, { "CountryCode": "EE", "CountryName": "Estonia", "Status": true }, { "CountryCode": "ET", "CountryName": "Ethiopia", "Status": true }, { "CountryCode": "FK", "CountryName": "Falkland Islands", "Status": true }, { "CountryCode": "FO", "CountryName": "Faroe Islands", "Status": true }, { "CountryCode": "FJ", "CountryName": "Fiji", "Status": true }, { "CountryCode": "FI", "CountryName": "Finland", "Status": true }, { "CountryCode": "FR", "CountryName": "France", "Status": true }, { "CountryCode": "GF", "CountryName": "French Guiana", "Status": true }, { "CountryCode": "PF", "CountryName": "French Polynesia", "Status": true }, { "CountryCode": "TF", "CountryName": "French Southern Territories", "Status": true }, { "CountryCode": "GA", "CountryName": "Gabon", "Status": true }, { "CountryCode": "GM", "CountryName": "Gambia", "Status": true }, { "CountryCode": "GE", "CountryName": "Georgia", "Status": true }, { "CountryCode": "DE", "CountryName": "Germany", "Status": true }, { "CountryCode": "GH", "CountryName": "Ghana", "Status": true }, { "CountryCode": "GI", "CountryName": "Gibraltar", "Status": true }, { "CountryCode": "GR", "CountryName": "Greece", "Status": true }, { "CountryCode": "GL", "CountryName": "Greenland", "Status": true }, { "CountryCode": "GD", "CountryName": "Grenada", "Status": true }, { "CountryCode": "GP", "CountryName": "Guadeloupe", "Status": true }, { "CountryCode": "GU", "CountryName": "Guam", "Status": true }, { "CountryCode": "GT", "CountryName": "Guatemala", "Status": true }, { "CountryCode": "GN", "CountryName": "Guinea", "Status": true }, { "CountryCode": "GW", "CountryName": "Guinea-Bissau", "Status": true }, { "CountryCode": "GY", "CountryName": "Guyana", "Status": true }, { "CountryCode": "HT", "CountryName": "Haiti", "Status": true }, { "CountryCode": "HM", "CountryName": "Heard Island And McDonald Islands", "Status": true }, { "CountryCode": "HN", "CountryName": "Honduras", "Status": true }, { "CountryCode": "HK", "CountryName": "Hong Kong", "Status": true }, { "CountryCode": "HU", "CountryName": "Hungary", "Status": true }, { "CountryCode": "IS", "CountryName": "Iceland", "Status": true }, { "CountryCode": "IN", "CountryName": "India", "Status": true }, { "CountryCode": "ID", "CountryName": "Indonesia", "Status": true }, { "CountryCode": "IR", "CountryName": "Iran", "Status": true }, { "CountryCode": "IQ", "CountryName": "Iraq", "Status": true }, { "CountryCode": "IE", "CountryName": "Ireland", "Status": true }, { "CountryCode": "IL", "CountryName": "Israel", "Status": true }, { "CountryCode": "IT", "CountryName": "Italy", "Status": true }, { "CountryCode": "JM", "CountryName": "Jamaica", "Status": true }, { "CountryCode": "JP", "CountryName": "Japan", "Status": true }, { "CountryCode": "JO", "CountryName": "Jordan", "Status": true }, { "CountryCode": "KZ", "CountryName": "Kazakhstan", "Status": true }, { "CountryCode": "KE", "CountryName": "Kenya", "Status": true }, { "CountryCode": "KI", "CountryName": "Kiribati", "Status": true }, { "CountryCode": "KW", "CountryName": "Kuwait", "Status": true }, { "CountryCode": "KG", "CountryName": "Kyrgyzstan", "Status": true }, { "CountryCode": "LA", "CountryName": "Laos", "Status": true }, { "CountryCode": "LV", "CountryName": "Latvia", "Status": true }, { "CountryCode": "LB", "CountryName": "Lebanon", "Status": true }, { "CountryCode": "LS", "CountryName": "Lesotho", "Status": true }, { "CountryCode": "LR", "CountryName": "Liberia", "Status": true }, { "CountryCode": "LY", "CountryName": "Libya", "Status": true }, { "CountryCode": "LI", "CountryName": "Liechtenstein", "Status": true }, { "CountryCode": "LT", "CountryName": "Lithuania", "Status": true }, { "CountryCode": "LU", "CountryName": "Luxembourg", "Status": true }, { "CountryCode": "MO", "CountryName": "Macao", "Status": true }, { "CountryCode": "MK", "CountryName": "Macedonia", "Status": true }, { "CountryCode": "MG", "CountryName": "Madagascar", "Status": true }, { "CountryCode": "MW", "CountryName": "Malawi", "Status": true }, { "CountryCode": "MY", "CountryName": "Malaysia", "Status": true }, { "CountryCode": "MV", "CountryName": "Maldives", "Status": true }, { "CountryCode": "ML", "CountryName": "Mali", "Status": true }, { "CountryCode": "MT", "CountryName": "Malta", "Status": true }, { "CountryCode": "MH", "CountryName": "Marshall Islands", "Status": true }, { "CountryCode": "MQ", "CountryName": "Martinique", "Status": true }, { "CountryCode": "MR", "CountryName": "Mauritania", "Status": true }, { "CountryCode": "MU", "CountryName": "Mauritius", "Status": true }, { "CountryCode": "YT", "CountryName": "Mayotte", "Status": true }, { "CountryCode": "MX", "CountryName": "Mexico", "Status": true }, { "CountryCode": "FM", "CountryName": "Micronesia", "Status": true }, { "CountryCode": "MD", "CountryName": "Moldova", "Status": true }, { "CountryCode": "MC", "CountryName": "Monaco", "Status": true }, { "CountryCode": "MN", "CountryName": "Mongolia", "Status": true }, { "CountryCode": "MS", "CountryName": "Montserrat", "Status": true }, { "CountryCode": "MA", "CountryName": "Morocco", "Status": true }, { "CountryCode": "MZ", "CountryName": "Mozambique", "Status": true }, { "CountryCode": "MM", "CountryName": "Myanmar", "Status": true }, { "CountryCode": "NA", "CountryName": "Namibia", "Status": true }, { "CountryCode": "NR", "CountryName": "Nauru", "Status": true }, { "CountryCode": "NP", "CountryName": "Nepal", "Status": true }, { "CountryCode": "NL", "CountryName": "Netherlands", "Status": true }, { "CountryCode": "AN", "CountryName": "Netherlands Antilles", "Status": true }, { "CountryCode": "NC", "CountryName": "New Caledonia", "Status": true }, { "CountryCode": "NZ", "CountryName": "New Zealand", "Status": true }, { "CountryCode": "NI", "CountryName": "Nicaragua", "Status": true }, { "CountryCode": "NE", "CountryName": "Niger", "Status": true }, { "CountryCode": "NG", "CountryName": "Nigeria", "Status": true }, { "CountryCode": "NU", "CountryName": "Niue", "Status": true }, { "CountryCode": "NF", "CountryName": "Norfolk Island", "Status": true }, { "CountryCode": "KP", "CountryName": "North Korea", "Status": true }, { "CountryCode": "MP", "CountryName": "Northern Mariana Islands", "Status": true }, { "CountryCode": "NO", "CountryName": "Norway", "Status": true }, { "CountryCode": "OM", "CountryName": "Oman", "Status": true }, { "CountryCode": "PK", "CountryName": "Pakistan", "Status": true }, { "CountryCode": "PW", "CountryName": "Palau", "Status": true }, { "CountryCode": "PS", "CountryName": "Palestine", "Status": true }, { "CountryCode": "PA", "CountryName": "Panama", "Status": true }, { "CountryCode": "PG", "CountryName": "Papua New Guinea", "Status": true }, { "CountryCode": "PY", "CountryName": "Paraguay", "Status": true }, { "CountryCode": "PE", "CountryName": "Peru", "Status": true }, { "CountryCode": "PH", "CountryName": "Philippines", "Status": true }, { "CountryCode": "PN", "CountryName": "Pitcairn", "Status": true }, { "CountryCode": "PL", "CountryName": "Poland", "Status": true }, { "CountryCode": "PT", "CountryName": "Portugal", "Status": true }, { "CountryCode": "PR", "CountryName": "Puerto Rico", "Status": true }, { "CountryCode": "QA", "CountryName": "Qatar", "Status": true }, { "CountryCode": "RE", "CountryName": "Reunion", "Status": true }, { "CountryCode": "RO", "CountryName": "Romania", "Status": true }, { "CountryCode": "RU", "CountryName": "Russia", "Status": true }, { "CountryCode": "RW", "CountryName": "Rwanda", "Status": true }, { "CountryCode": "SH", "CountryName": "Saint Helena", "Status": true }, { "CountryCode": "KN", "CountryName": "Saint Kitts And Nevis", "Status": true }, { "CountryCode": "LC", "CountryName": "Saint Lucia", "Status": true }, { "CountryCode": "PM", "CountryName": "Saint Pierre And Miquelon", "Status": true }, { "CountryCode": "VC", "CountryName": "Saint Vincent And The Grenadines", "Status": true }, { "CountryCode": "WS", "CountryName": "Samoa", "Status": true }, { "CountryCode": "SM", "CountryName": "San Marino", "Status": true }, { "CountryCode": "ST", "CountryName": "Sao Tome And Principe", "Status": true }, { "CountryCode": "SA", "CountryName": "Saudi Arabia", "Status": true }, { "CountryCode": "SN", "CountryName": "Senegal", "Status": true }, { "CountryCode": "CS", "CountryName": "Serbia and Montenegro", "Status": true }, { "CountryCode": "SC", "CountryName": "Seychelles", "Status": true }, { "CountryCode": "SL", "CountryName": "Sierra Leone", "Status": true }, { "CountryCode": "SG", "CountryName": "Singapore", "Status": true }, { "CountryCode": "SK", "CountryName": "Slovakia", "Status": true }, { "CountryCode": "SI", "CountryName": "Slovenia", "Status": true }, { "CountryCode": "SB", "CountryName": "Solomon Islands", "Status": true }, { "CountryCode": "SO", "CountryName": "Somalia", "Status": true }, { "CountryCode": "ZA", "CountryName": "South Africa", "Status": true }, { "CountryCode": "GS", "CountryName": "South Georgia And The South Sandwich Islands", "Status": true }, { "CountryCode": "KR", "CountryName": "South Korea", "Status": true }, { "CountryCode": "ES", "CountryName": "Spain", "Status": true }, { "CountryCode": "LK", "CountryName": "Sri Lanka", "Status": true }, { "CountryCode": "SD", "CountryName": "Sudan", "Status": true }, { "CountryCode": "SR", "CountryName": "Suriname", "Status": true }, { "CountryCode": "SJ", "CountryName": "Svalbard And Jan Mayen", "Status": true }, { "CountryCode": "SZ", "CountryName": "Swaziland", "Status": true }, { "CountryCode": "SE", "CountryName": "Sweden", "Status": true }, { "CountryCode": "CH", "CountryName": "Switzerland", "Status": true }, { "CountryCode": "SY", "CountryName": "Syria", "Status": true }, { "CountryCode": "TW", "CountryName": "Taiwan", "Status": true }, { "CountryCode": "TJ", "CountryName": "Tajikistan", "Status": true }, { "CountryCode": "TZ", "CountryName": "Tanzania", "Status": true }, { "CountryCode": "TH", "CountryName": "Thailand", "Status": true }, { "CountryCode": "CD", "CountryName": "The Democratic Republic Of Congo", "Status": true }, { "CountryCode": "TL", "CountryName": "Timor-Leste", "Status": true }, { "CountryCode": "TG", "CountryName": "Togo", "Status": true }, { "CountryCode": "TK", "CountryName": "Tokelau", "Status": true }, { "CountryCode": "TO", "CountryName": "Tonga", "Status": true }, { "CountryCode": "TT", "CountryName": "Trinidad and Tobago", "Status": true }, { "CountryCode": "TN", "CountryName": "Tunisia", "Status": true }, { "CountryCode": "TR", "CountryName": "Turkey", "Status": true }, { "CountryCode": "TM", "CountryName": "Turkmenistan", "Status": true }, { "CountryCode": "TC", "CountryName": "Turks And Caicos Islands", "Status": true }, { "CountryCode": "TV", "CountryName": "Tuvalu", "Status": true }, { "CountryCode": "VI", "CountryName": "U.S. Virgin Islands", "Status": true }, { "CountryCode": "UG", "CountryName": "Uganda", "Status": true }, { "CountryCode": "UA", "CountryName": "Ukraine", "Status": true }, { "CountryCode": "AE", "CountryName": "United Arab Emirates", "Status": true }, { "CountryCode": "GB", "CountryName": "United Kingdom", "Status": true }, { "CountryCode": "US", "CountryName": "United States", "Status": true }, { "CountryCode": "UM", "CountryName": "United States Minor Outlying Islands", "Status": true }, { "CountryCode": "UY", "CountryName": "Uruguay", "Status": true }, { "CountryCode": "UZ", "CountryName": "Uzbekistan", "Status": true }, { "CountryCode": "VU", "CountryName": "Vanuatu", "Status": true }, { "CountryCode": "VA", "CountryName": "Vatican", "Status": true }, { "CountryCode": "VE", "CountryName": "Venezuela", "Status": true }, { "CountryCode": "VN", "CountryName": "Vietnam", "Status": true }, { "CountryCode": "WF", "CountryName": "Wallis And Futuna", "Status": true }, { "CountryCode": "EH", "CountryName": "Western Sahara", "Status": true }, { "CountryCode": "YE", "CountryName": "Yemen", "Status": true }, { "CountryCode": "ZM", "CountryName": "Zambia", "Status": true }, { "CountryCode": "ZW", "CountryName": "Zimbabwe", "Status": true }] },
    stateslistbasedoncode = { "Status": [{ "StateCode": "AK", "StateName": "Alaska", "CountryCode": "US" }, { "StateCode": "AL", "StateName": "Alabama", "CountryCode": "US" }, { "StateCode": "AR", "StateName": "Arkansas", "CountryCode": "US" }, { "StateCode": "AZ", "StateName": "Arizona", "CountryCode": "US" }, { "StateCode": "CA", "StateName": "California", "CountryCode": "US" }, { "StateCode": "CO", "StateName": "Colorado", "CountryCode": "US" }, { "StateCode": "CT", "StateName": "Connecticut", "CountryCode": "US" }, { "StateCode": "DC", "StateName": "District of Columbia", "CountryCode": "US" }, { "StateCode": "DE", "StateName": "Delaware", "CountryCode": "US" }, { "StateCode": "FL", "StateName": "Florida", "CountryCode": "US" }, { "StateCode": "GA", "StateName": "Georgia", "CountryCode": "US" }, { "StateCode": "HI", "StateName": "Hawaii", "CountryCode": "US" }, { "StateCode": "IA", "StateName": "Iowa", "CountryCode": "US" }, { "StateCode": "ID", "StateName": "Idaho", "CountryCode": "US" }, { "StateCode": "IL", "StateName": "Illinois", "CountryCode": "US" }, { "StateCode": "IN", "StateName": "Indiana", "CountryCode": "US" }, { "StateCode": "KS", "StateName": "Kansas", "CountryCode": "US" }, { "StateCode": "KY", "StateName": "Kentucky", "CountryCode": "US" }, { "StateCode": "LA", "StateName": "Louisiana", "CountryCode": "US" }, { "StateCode": "MA", "StateName": "Massachusetts", "CountryCode": "US" }, { "StateCode": "MD", "StateName": "Maryland", "CountryCode": "US" }, { "StateCode": "ME", "StateName": "Maine", "CountryCode": "US" }, { "StateCode": "MI", "StateName": "Michigan", "CountryCode": "US" }, { "StateCode": "MN", "StateName": "Minnesota", "CountryCode": "US" }, { "StateCode": "MO", "StateName": "Missouri", "CountryCode": "US" }, { "StateCode": "MS", "StateName": "Mississippi", "CountryCode": "US" }, { "StateCode": "MT", "StateName": "Montana", "CountryCode": "US" }, { "StateCode": "NC", "StateName": "North Carolina", "CountryCode": "US" }, { "StateCode": "ND", "StateName": "North Dakota", "CountryCode": "US" }, { "StateCode": "NE", "StateName": "Nebraska", "CountryCode": "US" }, { "StateCode": "NH", "StateName": "New Hampshire", "CountryCode": "US" }, { "StateCode": "NJ", "StateName": "New Jersey", "CountryCode": "US" }, { "StateCode": "NM", "StateName": "New Mexico", "CountryCode": "US" }, { "StateCode": "NV", "StateName": "Nevada", "CountryCode": "US" }, { "StateCode": "NY", "StateName": "New York", "CountryCode": "US" }, { "StateCode": "OH", "StateName": "Ohio", "CountryCode": "US" }, { "StateCode": "OK", "StateName": "Oklahoma", "CountryCode": "US" }, { "StateCode": "OR", "StateName": "Oregon", "CountryCode": "US" }, { "StateCode": "PA", "StateName": "Pennsylvania", "CountryCode": "US" }, { "StateCode": "PR", "StateName": "Puerto Rico", "CountryCode": "US" }, { "StateCode": "RI", "StateName": "Rhode Island", "CountryCode": "US" }, { "StateCode": "SC", "StateName": "South Carolina", "CountryCode": "US" }, { "StateCode": "SD", "StateName": "South Dakota", "CountryCode": "US" }, { "StateCode": "TN", "StateName": "Tennessee", "CountryCode": "US" }, { "StateCode": "TX", "StateName": "Texas", "CountryCode": "US" }, { "StateCode": "UT", "StateName": "Utah", "CountryCode": "US" }, { "StateCode": "VA", "StateName": "Virginia", "CountryCode": "US" }, { "StateCode": "VI", "StateName": "Virgin Islands", "CountryCode": "US" }, { "StateCode": "VT", "StateName": "Vermont", "CountryCode": "US" }, { "StateCode": "WA", "StateName": "Washington", "CountryCode": "US" }, { "StateCode": "WI", "StateName": "Wisconsin", "CountryCode": "US" }, { "StateCode": "WV", "StateName": "West Virginia", "CountryCode": "US" }, { "StateCode": "WY", "StateName": "Wyoming", "CountryCode": "US" }] };
    
    var errormessages = [
      { "ShortName": "incompleteData", "ErrrorMessage": "All requested information is required in order to save the data you entered.  Please select [OK] to exit without saving or [CANCEL] to stay on the page." },
      { "ShortName": "ehop_inEligible", "ErrrorMessage": "Given your response, you are ineligible for an Expedited Home Occupancy Permit.  Please select [Return to Checklist] and visit the DC Office of Zoning at 1100 4th St., SW or call 202-442-4400 to obtain an HOP." },
      { "ShortName": "verifyandcontinuemessage", "ErrrorMessage": "To revise any of your responses, select [Cancel] and then select the [Return to Checklist] button. To proceed, select [Confirm]." },
      { "ShortName": "corpnodata", "ErrrorMessage": "The File Number you entered does not match the Corporations records. Please check your entry and try again. If your entry does not match, please select [Return to Checklist] and visit the DCRA Corporations Division at 1100 4th St., SW or call  202-442-4400." },
      { "ShortName": "feinssnNonCompliance", "ErrrorMessage": "According to Office of Tax and Revenue (OTR) records, the FEIN you entered is not in compliance with the District of Columbia's Clean Hands requirements. Please click on Tax and Revenue link below to know how to proceed further." },
      { "ShortName": "corpSearchNotClicked", "ErrrorMessage": "You must select [Search Corp Online].  Please click [OK] to proceed without saving the entered data or click [CANCEL] to stay on the page." },
      { "ShortName": "allfieldsNotFilled", "ErrrorMessage": "Please complete all the required fields." },
      { "ShortName": "renewalNavigation", "ErrrorMessage": "To save any updates/changes, please select [CANCEL] to stay on the page or [OK] to leave without saving." },
      { "ShortName": "navigateaway", "ErrrorMessage": "To save any updates/changes, please select [CANCEL] to stay on the page or [OK] to leave without saving." },
      { "ShortName": "hopallfieldsNotFilled", "ErrrorMessage": "Please complete all fields." }, { "ShortName": "corpFileNumberError", "ErrrorMessage": "Please enter your Corporate Registration File Number." },
      { "ShortName": "createChecklistnavigation", "ErrrorMessage": "The data you have selected/entered so far will be lost. You must complete all of the pre-application questions and create a checklist to save your data. Do you want to exit without saving?" },
      { "ShortName": "NextButtonIncompleteData", "ErrrorMessage": "Please provide all requested data and select [Next]." },
      { "ShortName": "ehopSelectionErrorMsg", "ErrrorMessage": "You must select [Confirm eHOP Eligibility].  Please click [OK] to proceed without saving the entered data or click [CANCEL] to stay on the page." },
      { "ShortName": "corp_number_failedstatus", "ErrrorMessage": "According to the Corporations Division's files, the Status of your Corporate Registration is {0}. You must resolve any  issues prior to submitting your Business License application. Please select Return to Checklist and visit the DCRA Corporations Division at 1100 4th St., SW, or call 202-442-4400." },
      { "ShortName": "createCheckList", "ErrrorMessage": "Making changes after your Checklist is created will require you to discard your Application and start a new Application from the beginning. To proceed and create your Checklist select [Confirm]. To review and revise your responses select [Cancel] and select the Revise button on the bottom of the page." },
      { "ShortName": "corp_businessstructure_mismatch", "ErrrorMessage": "The Business Structure you selected in the Pre-Application Questions does not match the Corporations records. Please check your entry and try again. If your entry does not match, please select [Return to Checklist] and visit the DCRA Corporations Division at 1100 4th St., SW or call 202-442-4400." },
      { "ShortName": "donothaveCofo", "ErrrorMessage": "The data selected/entered for this CofO will not be retained. Select [OK] to proceed with this option or [Cancel] to retain the data as presented." },
      { "ShortName": "searchNotClicked", "ErrrorMessage": "Please select [Search Zoning], or click [OK] to proceed without saving, or select [CANCEL] to stay on the page." }, { "ShortName": "renewalpaymentallfieldsNotFilled", "ErrrorMessage": "Please provide all requested data." }
    ];

    describe('when user is logged in', function () {

        describe('when guid is not available', function () {
            beforeEach(function () {
                spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(false);
                httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
                httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "UNDERREVIEW" });
                httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
                spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
                indiBussController = controller('IndividualBusinessLicenseController',
                    {
                        $scope: $scope, $rootScope: rootscope, $location: $location,
                        mockservice: mockservice, appconstants: appconstants,
                        $routeParams: routeparams, UtilityFactory: utilityfactory,
                        BBLSubmissionFactory: bblsubmissionfactory,
                        sessionFactory: sessionfactory, popupFactory: popupfactory,
                        errorFactory: errorfactory, authService: authservice
                    });

                indiBussController.individual = { DateofBirth: "02/5/1212" };
                $scope.vm = indiBussController;
                var element = angular.element(
                       '<form name="vm.update_form">' +
                       '<input ng-model="vm.datepicker" name="datepicker" />' +
                       '</form>'
                       );
                compile(element)($scope);
                form = $scope.vm.update_form;
                

            });

            it('should test init when status is underreview', function () {
                httpBackend.flush();
                expect($location.path).toHaveBeenCalledWith('/mybbl');
            });
        })


        describe('when status is underreview', function () {
            beforeEach(function () {
                spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
                httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
                httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond({ "Status": "UNDERREVIEW" });
                httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
                spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });

                indiBussController = controller('IndividualBusinessLicenseController',
                    {
                        $scope: $scope, $rootScope: rootscope, $location: $location,
                        mockservice: mockservice, appconstants: appconstants,
                        $routeParams: routeparams, UtilityFactory: utilityfactory,
                        BBLSubmissionFactory: bblsubmissionfactory,
                        sessionFactory: sessionfactory, popupFactory: popupfactory,
                        errorFactory: errorfactory, authService: authservice
                    });

                indiBussController.individual = { DateofBirth: "02/5/1212" };
                $scope.vm = indiBussController;
                var element = angular.element(
                       '<form name="vm.update_form">' +
                       '<input ng-model="vm.datepicker" name="datepicker" />' +
                       '</form>'
                       );
                compile(element)($scope);
                form = $scope.vm.update_form;

            });

            it('should test init when status is underreview', function () {
                httpBackend.flush();
                expect($location.path).toHaveBeenCalledWith('/mybbl');
            });
        });

        describe('when status is not underreview and with no initial data', function () {

            beforeEach(function () {
                spyOn(utilityfactory, 'ifGuidAvailable').and.returnValue(true);
                httpBackend.when('POST', basePath + 'authtoken').respond({ "token": "success" });
                httpBackend.when('POST', basePath + 'api/BBLAssociation/SubmissionStatus').respond(submissionstatus_data);
                httpBackend.when('GET', basePath + 'api/BBLApplication/binddropdown').respond(binddropdown_data);
                httpBackend.when('POST', basePath + 'api/BBLApplication/StateListBasedOnCode').respond(stateslistbasedoncode);
                httpBackend.when('POST', basePath + 'api/BBLApplication/GetSubIndividuals').respond(individualdata);                
                httpBackend.when('POST', basePath + 'api/BBLApplication/subinddelete').respond(true);
                httpBackend.when('GET', basePath + 'api/BBLApplication/GetAllMessages').respond(errormessages);
                spyOn(localstorageservice, 'get').and.returnValue({ useRefreshTokens: true, refreshToken: 'token' });
                indiBussController = controller('IndividualBusinessLicenseController', {
                    $scope: $scope, $rootScope: rootscope, $location: $location,
                    mockservice: mockservice, appconstants: appconstants,
                    $routeParams: routeparams, UtilityFactory: utilityfactory,
                    BBLSubmissionFactory: bblsubmissionfactory,
                    sessionFactory: sessionfactory, popupFactory: popupfactory,
                    errorFactory: errorfactory, authService: authservice
                });
                indiBussController.individual = {};
                indiBussController.individual.DateofBirth = "02/5/1212";
                indiBussController.individual.CompanyBusinessLicense = "123456";
                $scope.vm = indiBussController;
                var element = angular.element(
                       '<form name="vm.update_form">' +
                       '<input ng-model="vm.datepicker" name="datepicker" />' +
                       '</form>'
                       );
                compile(element)($scope);
                form = $scope.vm.update_form;
            });




            //it('should test dontNavigate', function () {
            //    indiBussController.dontNavigate();
            //    httpBackend.flush();
            //    expect(indiBussController.navigate).toBeFalsy();
            //});

            //it('should test dontNavigate', function () {
            //    indiBussController.navigationPath = 'test';
            //    indiBussController.navigateAnyway();
            //    httpBackend.flush();
            //    expect(indiBussController.navigate).toBeTruthy();
            //    expect($location.path).toHaveBeenCalledWith('/test');
            //});

            //it('should test dontNavigate', function () {
            //    indiBussController.setErrorMsg();
            //    httpBackend.flush();
            //    expect(indiBussController.businessLicenseData).toBe('');
            //});

            it('should test checklicense when status is no data', function () {
                httpBackend.when('POST', basePath + 'api/BBLApplication/ValidateLicenceNum').respond({ "Status": "NoData" });
               
                //indiBussController.individual.CompanyBusinessLicense = "123456";
                //indiBussController.checklicense();
                httpBackend.flush();
                // expect(indiBussController.businessLicenseData).toEqual("123456");
            });

            //it('should test checklicense when status is true', function () {
            //    httpBackend.when('POST', basePath + 'api/BBLApplication/ValidateLicenceNum').respond({ "Status": "TestName" });
            //    indiBussController.individual = {};
            //    indiBussController.individual.CompanyBusinessLicense = "123456";
            //    indiBussController.checklicense();
            //    httpBackend.flush();
            //    expect(indiBussController.businessLicenseData).toEqual("");
            //    expect(indiBussController.BusinessNameDisable).toBeTruthy();
            //    expect(indiBussController.individual.CompanyName).toEqual("TestName");
            //});

            it('should test checkAndExit when individual object is not defined', function () {
                form.$invalid = false;
                httpBackend.when('POST', basePath + 'api/BBLApplication/ValidateLicenceNum').respond({ "Status": "TestName" });
                httpBackend.when('POST', basePath + 'api/BBLApplication/SubIndividual').respond("2");
                
                indiBussController.checkAndExit('appchecklist');
                httpBackend.flush();
                expect($location.path).toHaveBeenCalledWith('/appchecklist/guid');
            });

            //it('should test checkAndExit when individual object is defined', function () {
            //    indiBussController.individual = individualdata;
            //    indiBussController.individual.DateofBirth = undefined;
            //    indiBussController.individual.ExpirationDate = undefined;
            //    httpBackend.when('POST', basePath + 'api/BBLApplication/ValidateLicenceNum').respond({ "Status": "NoData" });
            //    indiBussController.checkAndExit('testpath');
            //    httpBackend.flush();
            //    expect($location.path).toHaveBeenCalledWith('/testpath');
            //});

            it('should test checkAndExit when individual object is defined and form is valid', function () {
                httpBackend.when('POST', basePath + 'api/BBLApplication/ValidateLicenceNum').respond({ "Status": "TestName" });
                httpBackend.when('POST', basePath + 'api/BBLApplication/SubIndividual').respond("2");
                indiBussController.individual = individualdata;
                indiBussController.individual.DateofBirth = undefined;
                indiBussController.individual.ExpirationDate = undefined;
                indiBussController.checkAndExit('testpath');
                httpBackend.flush();
                expect($location.path).toHaveBeenCalledWith('/testpath');
            });

            it('should test selectcountryoption', function () {
                indiBussController.individual.Country = "US";
                indiBussController.selectcountryoption(false);
                expect(indiBussController.validateCountry).toBeTruthy();
            });

            it('should test selectcountryoption', function () {
                indiBussController.individual.Country = "AUS";
                indiBussController.selectcountryoption(true);
                expect(indiBussController.validateCountry).toBeFalsy();
            });

        });

    });
});