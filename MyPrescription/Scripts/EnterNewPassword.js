$(document).ready(function () {
    var token = $("#token").val();
    var isPasswordValid = false;

    $("#ConfirmBtn").click(function () {
        var registeredEmail = $("#RegisteredEmail").val();
        if (registeredEmail == "") {
            return;
        }
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (regex.test(registeredEmail)) {
            $("#RegisteredEmail").hide();
            $("#ConfirmBtn").hide();
            $("#EmailOutputDiv").html("<img height='35' src='Resources/Images/ripple.gif' /> Please wait a moment...");

            //confirm identity
            setTimeout(function () {
                $.ajax({
                    type: "GET",
                    url: "./api/user/checkemailfromtoken/" + token,
                    //data: { 'token': token },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data == registeredEmail) {
                            //show inputboxes
                            $("#LargeText").html("<span style='color:green'>Identity Confirmed</span>");
                            $("#confirmIdentity").hide();
                            $("#enterNewPassword-row").removeClass("hidden");
                        }
                        else {
                            $("#LargeText").html("<span style='color:red'>IDENTITY NOT CONFIRMED. TAKING YOU BACK TO THE HOME PAGE.</span>");
                            $("#EmailOutputDiv").html("");
                            setTimeout(function () {
                                location = 'SignIn.aspx';
                            }, 2000);
                        }
                    }
                });
            }, 500);

            
        }
        else{
            $("#EmailOutputDiv").html("<span style='color:red'> Enter Email in proper format i.e. abc@xyz.com</span>")
        }

        return false;
    });

    $("#Password").popover({
        html: true,
        content: function () {
            return $("#popover-password-content").html();
        },
        title: function () {
            return $("#popover-password-title").html();
        }
    });

    $("#confirmPassword").popover({
        html: true,
        content: function () {
            return $("#popover-passwordConfirm-content").html();
        },
        title: function () {
            return $("#popover-passwordConfirm-title").html();
        }
    });

    $("#Password").focus(function () {
        $("#Password").popover('show');
    });

    $("#Password").blur(function () {
        $("#Password").popover('hide');
    });

    $("#ResetBtn").click(function () {
        var password = $("#Password").val();
        var confirmPassword = $("#confirmPassword").val();

        if((password == "") || (confirmPassword == "")){
            return;
        }

        if (password.length >= 8) {
            if (password == confirmPassword) {
                //valid
                $("#confirmPassword").popover('hide');

                $("#ResetBtn").hide();
                $("#LargeText").html("<span style='color:green'>Resetting</span>");
                $("#FinalOutputDiv").html("<img height='35' src='Resources/Images/ripple.gif' /> Resetting... Please wait a moment...");

                setTimeout(function () {
                    $("#ResetBtnServer").click();
                },1000);
            }
            else {
                $("#confirmPassword").popover('show');
            }
        }
        else {
            $("#Password").popover('show');
        }

        return false;
    });
});