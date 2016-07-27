jQuery(document).ready(function () {
    jQuery(document).click(function (e) {//reverse click function to shrink the search box
        if (!jQuery(e.target).is('.search_box, .search_box *,.search,.navbar-toggle *,.navbar-nav *')) {
            jQuery('.search_box').removeClass('active_search');
            jQuery('.search_box .btn.btn-default').fadeOut(300);
        }
    });
});

function testKey(e) {
    if (e.which === 13) {
        e.preventDefault();
        $('#collapseExample').toggle();
    }
};

function toggleclick(id) {
    $('#collapseExample,#collapseExample_nolicenses').toggle();

    if ($("#collapseExample,#collapseExample_nolicenses").is(":visible")) {
        $("#" + id).html("Help With This Page (close)");
    } else {
        $("#" + id).html("Help With This Page (Click Here)");
    }
};