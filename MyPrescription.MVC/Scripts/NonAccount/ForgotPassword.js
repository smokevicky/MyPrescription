$(document).ready(function () {
    $("#RecoverBtn").click(function () {
        var email = $("#ForgotEmail").val();
        if (email == "") {
            return;
        }
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;

        if (regex.test(email)) {
            //checking email is registered or not            
            $("#OutputDiv").html("<img height='35' src='/Resources/Images/ripple.gif' /> Please wait a moment...");
            setTimeout(function () {
                $.ajax({
                    type: "GET",
                    url: "/userapi/isavailable",
                    data: { 'stringValue': email },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data == false) {

                            //send email
                            //no success function
                            $.ajax({
                                type: "GET",
                                url: "/NonAccount/SendForgotPasswordEmailAndFlag",
                                data: { 'stringValue': email },
                                contentType: "application/json; charset=utf-8"
                            });
                            //end
                            
                            $("#ForgotEmail").hide();
                            $("#RecoverBtn").hide();
                            $("#LargeDiv").html("<span class='text-success'>Instructions have been sent to your Email-id : <b>" +
                                email + "</b>. <br />Kindly check your Email to continue further.</span>");
                            $("#OutputDiv").html("");

                            //Redirecting to SignIn.aspx after 4 secs.
                            setTimeout(function () {
                                window.location.href = "/Home/Index";
                            },4000);
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