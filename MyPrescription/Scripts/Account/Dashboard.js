$(document).ready(function () {
    $("#DashboardListItem").addClass("active");

    var userName = $("#nameSessionVariable").val();

    Notify("Welcome " + userName);

});