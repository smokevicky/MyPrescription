$(document).ready(function () {
    $("#RecoverBtn").click(function () {
        var email = $("#ForgotEmail").val();
        if (email == "") {
            return;
        }
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;

        if (regex.test(email)) {
            //checking email is registered or not            
            $("#OutputDiv").html("<img height='35' src='Resources/Images/ripple.gif' /> Please wait a moment...");
            setTimeout(function () {
                $.ajax({
                    type: "GET",
                    url: "./api/user/isavailable",
                    data: { 'stringValue': email },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {                    
                        if (data == false) {

                            //send email
                            $("#SendMailBtn").click();
                            
                            $("#ForgotEmail").hide();
                            $("#RecoverBtn").hide();
                            $("#LargeDiv").html("<span style='color:green'>Instructions have been sent to your Email-id : <b>" + email + "</b>. <br />Kindly check your Email to continue further.</span>");
                            $("#OutputDiv").html("");

                            //Redirecting to SignIn.aspx after 2 secs.
                            setTimeout(function () {
                                location = 'SignIn.aspx';
                            },2000);
                        }
                        else {                            
                            $("#OutputDiv").html("<span style='color:red'>Email-Id is not registered. Check again.</span>");                            
                        }
                    }
                });
            }, 500);            
        }
        else {            
            $("#OutputDiv").html("<span style='color:red'>Entered EMail-Id is not in a proper format i.e. abc@xyz.com</span>");
        }
        return false;
    });
});