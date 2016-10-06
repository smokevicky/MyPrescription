
$(document).ready(function () {

    $("#loadingDiv").hide();
    $("#loadingDiv").html("<img src='/Resources/Images/signInLoad2.gif' />");

    $("#logIn").click(function () {

        var email = $("#username").val();
        var pwd = $("#password").val();
        if ((email == "") || (pwd == ""))  {
            return;
        }
        else {
            var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (regex.test(email) && (pwd.length >= 8)) {
                $.when($("#frmDiv").hide("slide", { direction: "left" }, 200))
                    .then(function() {
                        $("#loadingDiv").fadeIn('slow');
                    });

                setTimeout(function() {
                        SignIn(email, pwd);
                    },
                    2000);
            }
            else {
                $('#popover').popover('show');
            }
        }
        return false;
    });

    SignIn = function (email, password) {
        var userModelObject = {
            email: email,
            password: password
        }

        $.ajax({
            type: "POST",
            url: "/nonaccount/signin",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(userModelObject),
            dataType: "json",
            success: function(statusCode) {
                if (statusCode == 0) {
                    $("#invalidText").removeClass("hidden");
                }
                else if (statusCode == 1) {
                    $("#invalidText").addClass("hidden");
                    window.location = "/NonAccount/VerifyAccountActivation";
                } else {
                    Notify("Some error has occured", "danger");
                }
            }
        });
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

