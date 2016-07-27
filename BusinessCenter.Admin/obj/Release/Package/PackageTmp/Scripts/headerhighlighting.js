$(function () {
    $("#tabs").tabs();
});

function geturl() {
    $('#sad').removeClass('activeclass');
    var url = window.location.href;
    var type = url.substring(url.lastIndexOf('=') + 1);
    var str = window.location.pathname;

    var strarr = str.split('/');
    if (strarr[1] === 'SuperAdmin' && strarr[2] === 'Dashboard') {
        $('#sad').addClass('activeclass');
    }
    if (strarr[1] == 'BBL' && strarr[2] == 'TransferLicense') {
        $('#bbl').addClass('activeclass');
    }
    if (strarr[1] == 'SuperAdmin' && strarr[2] == 'CustomerDashboard') {
        $('#Superadminuser').addClass('activeclass');
        $('#Adminuser').addClass('activeclass');
    }

    if (strarr[1] == 'BBL' && strarr[2] == 'CustomerSubmissions') {
        $('#Superadminuser').addClass('activeclass');
        $('#Adminuser').addClass('activeclass');
    }
    if (strarr[1] == 'BBL' && strarr[2] == 'ApplicationReview') {
        $('#bbl').addClass('activeclass');
        $('#bbl').addClass('activeclass');
    }
    if (strarr[1] === 'Account' && strarr[2] === 'MyProfile') {
        $('#bbl1').addClass('activeclass');
        $('#myprofile').addClass('activeclass');
    }
    if (strarr[1] === 'Admin' && strarr[2] === 'Dashboard') {
        $('#AdminDashboard').addClass('activeclass');
    }
    if (strarr[1] === 'Admin' && strarr[2] === 'Home') {
        $('#Adminhome').addClass('activeclass');
    }
    if (strarr[1] === 'Admin' && strarr[2] === 'CustomerHome') {
        $('#Adminuser').addClass('activeclass');
    }
    if (strarr[1] === 'Account' && strarr[2] === 'Register' && type == 'S') {
        $('#accountmanagement').addClass('activeclass');
        $('#Superadminhome').addClass('activeclass');
    }
    if (strarr[1] === 'Account' && strarr[2] === 'Register' && type == 'A') {
        $('#admina').addClass('activeclass');
        $('#accountmanagement').addClass('activeclass');
        $('#Adminhome').addClass('activeclass');
    }

    if (strarr[1] === 'Account' && strarr[2] === 'Register' && type == 'M') {
        $('#accountmanagement').addClass('activeclass');
        $('#admin').addClass('activeclass');
    }

    if (strarr[1] === 'BBL' && strarr[2] === 'BusinessActivities' || strarr[2] === 'SubmissionMaster'
        || strarr[2] === 'PrimaryCategories' || strarr[2] === 'SecondaryCategories' || strarr[2] === 'Documents' || strarr[2] === 'CategoryFees') {
        $('#bbl').addClass('dropdown-toggle activeclass');
    }
    if (strarr[1] === 'BBL' && strarr[2] === 'PrimaryCategories') {
        $('#bbl').addClass('activeclass');
    }
    if (strarr[1] === 'BBL' && strarr[2] === 'CreatePrimaryCategory') {
        $('#bbl').addClass('activeclass');
    }
    if (strarr[1] === 'BBL' && strarr[2] === 'UpdatePrimaryCategory') {
        $('#bbl').addClass('activeclass');
    }
    if (strarr[1] === 'BBL' && strarr[2] === 'CreateDocuments') {
        $('#bbl').addClass('activeclass');
    }
    if (strarr[1] === 'BBL' && strarr[2] === 'UpdateDocuments') {
        $('#bbl').addClass('activeclass');
    }

    if (strarr[1] === 'BBL' && strarr[2] === 'SecondaryCategories') {
        $('#bbl').addClass('activeclass');
    }

    if (strarr[1] === 'BBL' && strarr[2] === 'SubCategories') {
        $('#bbl').addClass('activeclass');
    }

    if (strarr[1] === 'BBL' && strarr[2] === 'TransferLicense') {
        $('#Superadminuser').addClass('activeclass');
        $('#Adminuser').addClass('activeclass');
        $('#bbl').removeClass('activeclass');
    }

    if (strarr[1] === 'BBL' && strarr[2] === 'Queue') {
        $('#queue').addClass('activeclass');
    }

    if (strarr[1] === 'BBL' && strarr[2] === 'Errors') {
        $('#ErrorContent').addClass('activeclass');
        $('#AdminError').addClass('activeclass');
    }
    if (strarr[1] === 'BBL' && strarr[2] === 'CreateError') {
        $('#ErrorContent').addClass('activeclass');
        $('#AdminError').addClass('activeclass');
    }
    if (strarr[1] === 'BBL' && strarr[2] === 'UpdateError') {
        $('#ErrorContent').addClass('activeclass');
        $('#AdminError').addClass('activeclass');
    }
    if (strarr[1] === 'BBL' && strarr[2] === 'BusinessComapreData') {
        $('#queue').addClass('activeclass');

    }
    if (strarr[1] === 'BBL' && strarr[2] === 'DocumentDetails') {
        $('#queue').addClass('activeclass');

    }
    if (strarr[1] === 'BBL' && strarr[2] === 'RenewalLicenseDetails') {
        $('#queue').addClass('activeclass');
    }

    /* Account Management */

    if (strarr[1] === 'SuperAdmin' && strarr[2] === 'Home') {
        $('#accountmanagement').addClass('activeclass');
    }

    if (strarr[1] === 'Admin' && strarr[2] === 'ManagerHome') {
        $('#accountmanagement').addClass('activeclass');
    }

    if (strarr[1] === 'Admin' && strarr[2] === 'Home') {
        $('#accountmanagement').addClass('activeclass');
    }

    if (strarr[1] === 'Admin' && strarr[2] === 'CustomerHome') {
        $('#accountmanagement').addClass('activeclass');

    }

    if (strarr[1] === 'SuperAdmin' && strarr[2] === 'Home') {
        $('#Superadminhome').addClass('activeclass');
    }
    if (strarr[1] === 'Admin' && strarr[2] === 'ManagerHome') {
        $('#admin').addClass('activeclass');
    }
    if (strarr[1] === 'Admin' && strarr[2] === 'Home') {
        $('#admina').addClass('activeclass');
    }
    if (strarr[1] === 'Admin' && strarr[2] === 'CustomerHome') {

        $('#Superadminuser').addClass('activeclass');
    }

    /* superadmin highlight */
    if (strarr[1] === 'Account' && strarr[2] === 'CustomerProfile') {
        $('#accountmanagement').addClass('activeclass');
        $('#Adminuser').addClass('activeclass');
        $('#Superadminuser').addClass('activeclass');
    }

    /* Business Activity */
    if (strarr[1] === 'BBL' && strarr[2] === 'BusinessActivities') {
        $('#businessactivity').addClass('activeclass');
    }

    /* Profile page */
    if (strarr[1] === 'Account' && strarr[2] === 'Profile' && type == 'S') {
        $('#accountmanagement').addClass('activeclass');
        $('#Superadminhome').addClass('activeclass');
    }
    if (strarr[1] === 'Account' && strarr[2] === 'Profile' && type == 'A') {
        $('#accountmanagement').addClass('activeclass');
        $('#admina').addClass('activeclass');
        $('#Adminhome').addClass('activeclass');
    }

    if (strarr[1] === 'Account' && strarr[2] === 'Profile' && type == 'M') {
        $('#accountmanagement').addClass('activeclass');
        $('#admin').addClass('activeclass');
    }

    /* Customer */
    if (strarr[1] === 'SuperAdmin' && strarr[2] === 'CustomerDashboard') {
        $('#accountmanagement').addClass('activeclass');
        $('#Superadminuser').addClass('activeclass');
    }

    if (strarr[1] === 'BBL' && strarr[2] === 'CustomerSubmissions') {
        $('#accountmanagement').addClass('activeclass');
        $('#Superadminuser').addClass('activeclass');
    }

    if (strarr[1] === 'BBL' && strarr[2] === 'ViewCheckList' || strarr[2] === 'InformationVerification' || strarr[2] === 'ViewReceipt' || strarr[2] === 'SubmissionInformationDetails') {
        $('#accountmanagement').addClass('activeclass');
        $('#Superadminuser').addClass('activeclass');
        $('#Adminuser').addClass('activeclass');
    }

    if (strarr[1] === 'BBL' && strarr[2] === 'TransferAudit') {
        $('#Audit').addClass('activeclass');
    }
}
