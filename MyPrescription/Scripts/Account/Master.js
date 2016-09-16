$(document).ready(function () {
    $("#ChangeDiv").hide();   

    $("#ProfilePicLarge").mouseenter(function () {
        $("#ProfilePicLarge").css("-webkit-filter", "brightness(60%)");
        $("#ChangeDiv").show();
    });

    $("#ProfilePicLarge").mouseleave(function () {
        $("#ChangeDiv").hide();
        $("#ProfilePicLarge").css("-webkit-filter", "brightness(100%)");
    });

    $("#ChangeDiv").mouseenter(function () {
        $("#ProfilePicLarge").mouseenter();
    });

    $("#ChangeDiv").mouseleave(function () {
        $("#ProfilePicLarge").mouseleave();
    });

    $("#ProfilePic").click(function () {
        $('.small-info-dropdown').show();
    });

    $("#ChangeDiv").click(function () {
        window.location.href = "Profile.aspx?tab=ProfilePicture";
    });

    $(document).mouseup(function (e) {
        var container = $(".small-info-dropdown");
        var toggelContainer = $("#ProfilePic");

        if (!container.is(e.target) // if the target of the click isn't the container...
            && container.has(e.target).length === 0) // ... nor a descendant of the container
        {
            container.hide();
        }
    });

    Notify = function (text, color) {
        $("#snackbar").text(text);

        //setting snackbar color
        switch (color) {
            case 'info': $("#snackbar").css("background-color", "rgba(0, 39, 40, 0.9)");
                break;
            case 'warning': $("#snackbar").css("background-color", "orangered");
                break;
            case 'danger': $("#snackbar").css("background-color", "#e93a54");
                break;
            case 'success': $("#snackbar").css("background-color", "#1b8745");
                break;
            default: $("#snackbar").css("background-color", "rgba(0, 39, 40, 0.9)");
        }

        $("#snackbar").addClass('show');
        setTimeout(function () {
            $("#snackbar").removeClass('show');
        }, 2500);
    }

    GetBadgeCount = function () {
        $.ajax({
            url: '/api/user/getbadgecount',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            success: function (data) {
                if (data.statusCode == 1) {
                    $("#dashboardCountBadge").html("<span>0 Updates</span>");
                    $("#profileCountBadge").text("20%");
                    $("#hospitalsCountBadge").text(data.hospitalCount);
                    $("#doctorsCountBadge").text(data.doctorCount);
                    $("#vaultsCountBadge").text(data.vaultCount);
                }
                else {
                    Notify("Some error has occured", 'danger');
                }
            }
        });
    };

    GetBadgeCount();
});

