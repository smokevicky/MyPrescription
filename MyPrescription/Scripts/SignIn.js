
$(document).ready(function () {

    $("#loadingDiv").hide();
    $("#loadingDiv").html("<img src='../Resources/Images/signInLoad.gif' />");

    $("#logIn").click(function () {

        var email = $("#username").val();
        var pwd = $("#password").val();
        if ((email == "") || (pwd == ""))  {
            return;
        }
        else {
            var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (regex.test(email) && (pwd.length >= 8)) {
                $.when($("#frmDiv").hide("slide", { direction: "left" }, 200)).then(function () {
                    $("#loadingDiv").fadeIn('slow'); // Alerts 200
                });
                setTimeout(serverBtnClick, 2000);                
            }
            else {
                $('#popover').popover('show');
            }
        }
        return false;
    });

    serverBtnClick = function () {
        $("#logInServer").click();
    }

    $("#popover").popover({

        html: true,
        content: function () {
            return $("#popover-content").html();
        },
        title: function () {
            return $("#popover-title").html();
        }

    });
})

