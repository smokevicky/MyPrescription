$(document).ready(function () {
    $("#DashboardListItem").addClass("active");

    var userName = $("#nameSessionVariable").val();

    Notify("Welcome " + userName);

    setTimeout(function () {
        $(".tile-content").val(50);
    }, 2000);
    
});