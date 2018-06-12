$(document).ready(function () {    
    
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var isEmailValid = false;

    $("#signupPwd").focus(function () {
        $('#popover-password').popover('show');
    });

    $("#signupPwd").blur(function () {
        $('#popover-password').popover('hide');
    });

    $("#popover-password").popover({
        html: true,
        content: function () {
            return $("#popover-password-content").html();
        },
        title: function () {
            return $("#popover-password-title").html();
        }
    });

    $("#popover-passwordConfirm").popover({
        html: true,
        content: function () {
            return $("#popover-passwordConfirm-content").html();
        },
        title: function () {
            return $("#popover-passwordConfirm-title").html();
        }
    });

    $("#popover-email").popover({
        html: true,
        content: function () {
            return $("#popover-email-content").html();
        },
        title: function () {
            return $("#popover-email-title").html();
        }
    });

    $("#signupEmail").popover({
        html: true,
        container:'body',
        content: function () {
            return $("#popover-emailCheck-content").html();
        },
        title: function () {
            return $("#popover-emailCheck-title").html();
        }
    });

    $("#signupEmail").keyup(function () {
        debugger;
        var email = $("#signupEmail").val();
        if (email != "") {
            if (regex.test(email)) {
                $("#popover-email").popover('hide');

                $("#popover-emailCheck-title").html("<b>Please wait a moment...</b>");
                $("#popover-emailCheck-content").html("<img height='35' src='Resources/Images/ripple.gif' /> Please wait a moment...");
                $('#signupEmail').popover('show');

                setTimeout(function () {
                    $.ajax({
                        type: "POST",
                        url: "./api/user/isavailable",
                        data: { 'testUserModel.email': email },
                        //data:  { 'stringValue': email },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",                        
                        success: function (data) {
                            //data = $($.parseXML(data)).find("boolean").text();
                            if (data == true) {
                                $("#popover-emailCheck-title").html("<b>Email is Not Registered</b>");
                                $("#popover-emailCheck-content").html("<span class='text-success'> &#10004; Available for SignUp </span>");
                                $('#signupEmail').popover('show');
                                isEmailValid = true;
                            }
                            else {
                                $("#popover-emailCheck-title").html("<b>EMail-Id is already registered</b>");
                                $("#popover-emailCheck-content").html("<div class='text-warning'>Some has already registered using that Email-Id. <br />If that's you, click on <a href='SignIn.aspx'>LogIn</a> to head back to the LogIn page. <br />Otherwise use a fresh Email-Id.</div>");
                                $('#signupEmail').popover('show');
                                isEmailValid = false;
                            }
                        }
                    });
                }, 500);
            }
            else {
                $('#signupEmail').popover('hide');
                $("#popover-email").popover('show');
            }
        }
        else {
            $('#signupEmail').popover('hide');
            $("#popover-email").popover('show');
        }
    });

    $("#signupEmail").blur(function () {       
        $('#signupEmail').popover('hide');
        $("#popover-email").popover('hide');
    });

    $("#signupFrmSubmit").click(function () {       
        var fname = $("#signupFname").val();
        var lname = $("#signupLname").val();
        var email = $("#signupEmail").val();
        var pwd = $("#signupPwd").val();
        var pwdc = $("#signupPwdc").val();
        var checkState = $("#signupCheckbox:checked").val();        

        if ((fname == "") || (email == "") || (pwd == "") || (pwdc == "") ||(checkState != "0")) {
            return;
        }
        
        if (isEmailValid == true) {
            if (pwd.length >= 8) {
                if (pwd == pwdc) {
                    $("#signupFrmSubmitServer").click();
                }
                else {
                    $('#popover-passwordConfirm').popover('show');
                }
            }
            else {
                $('#popover-password').popover('show');
            }
        }
        else {
            $("#signupEmail").keyup();
        }

        return false;
    });
    
});