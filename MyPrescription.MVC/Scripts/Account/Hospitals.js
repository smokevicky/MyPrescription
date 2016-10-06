$(document).ready(function () {
    var userId = $("#userIdSessionVariable").val();

    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;

    var pageStart = 1;

    //default sort
    sortBy = "serial-up";

    //default page size
    var pageSize = 5;
    $("#pageSizeDropup option[value='" + pageSize + "']").attr("selected", "selected");

    var selectedPageBtn = 1;

    //toggle between insert and update
    var isEdit = false;
    var editableHospitalId = null;

    $("#HospitalsListItem").addClass("active");

    $('#addNewModal').modal({
        backdrop: 'static',
        keyboard: false,
        show: false
    });

    //$("#addBtn").click(function () {
    //    isEdit = false;

    //    ResetModal();

    //    $('#addNewModal').modal('show');
    //});

    $("#modalCancelBtn").click(function () {
        $('#addNewModal').modal('hide');
    });

    $("#modalAddBtn").click(function () {
        var hName = $("#hospitalName").val();
        var hAddress = $("#hospitalAddress").val();
        var hPhoneNo = $("#hospitalPhoneNo").val();
        var hPhoneNo2 = $("#hospitalPhoneNo2").val();
        var hEmail = $("#hospitalEmail").val();
        var hPrimary = $("#hospitalPrimaryMark").is(':checked') ? 1 : 0;

        if ((hName == "") || (hAddress == "") || (hPhoneNo == "") || (hEmail == "")) {
            return;
        }
        else {

            if (!regex.test(hEmail)) {
                $("#emailValidationDiv").text("Please Enter Email in proper format i.e. abc@xyz.com");
                $("#emailValidationDiv").removeClass("hidden");
            }
            else {                
                $("#inputDiv").hide();
                $("#notificationDiv").show();
                $("#notificationDiv").html("<div class='text-center'>" +
                    "<img height='35' src='/Resources/Images/ripple.gif' />Please wait...</div>");

                if (isEdit == false) {
                    var hospitalModelObject = {
                        name: hName,
                        address: hAddress,
                        phoneNo: hPhoneNo,
                        phoneNo2: hPhoneNo2,
                        email: hEmail,
                        userId: userId,
                        isPrimary: hPrimary
                    };

                    AddOrUpdateAjaxCall("add", hospitalModelObject);
                }
                else {
                    var hospitalModelObject = {
                        hospitalId: editableHospitalId,
                        name: hName,
                        address: hAddress,
                        phoneNo: hPhoneNo,
                        phoneNo2: hPhoneNo2,
                        email: hEmail,
                        isPrimary: hPrimary,
                        userId: userId
                    };

                    AddOrUpdateAjaxCall("update", hospitalModelObject);
                }                
            }
        }

        return false;
    });

    function AddOrUpdateAjaxCall(actionName, hospitalModelObject) {
        var uri = "";
        var successNotification = "";

        if (actionName == "add") {
            uri = "addnewhospital";
            successNotification = "Added";
        }
        else {
            uri = "updatehospitaldetails";
            successNotification = "Updated";
        }

        setTimeout(function () {
            $.ajax({
                url: '/hospitalapi/' + uri,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(hospitalModelObject),
                //dataType: "json",
                success: function (data) {
                    console.log(data);
                    if (data == "True") {
                        $("#notificationDiv").html("<div class='success text-center'>"+ successNotification +" Successfully</div>");
                        setTimeout(function () {
                            $('#addNewModal').modal('hide');
                        }, 500);
                        UpdateGrid();
                    }
                    else {
                        $("#notificationDiv").html("<div class='error text-center'>Unknown error has occured. Kindly retry the same.</div>");
                        setTimeout(function () {
                            $('#addNewModal').modal('hide');
                        }, 500);
                    }
                }
            });
        }, 500);
    }

    UpdateGrid = function () {
        var hospitalRequestModelObject = {
            pageStart: pageStart,
            pageSize: pageSize,
            userId: userId,
            sortBy : sortBy
        };

        $.ajax({
            url: '/hospitalapi/gethospitaldetails',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(hospitalRequestModelObject),
            dataType: "json",
            success: function (data) {
                //Empty
                if (data.statusCode == 0) {
                    $("#hospitalGrid").html($("#EmptyDiv").html());
                }
                else if (data.statusCode == 1) {
                    $("#hospitalGrid").html("");
                    $(".pagination").html("");

                    //showing count on badge
                    $("#hospitalsCountBadge").text(data.rowCount);

                    //Calculating no of page buttons to be created
                    var noOfButtons = data.rowCount / pageSize;

                    //creating page buttons
                    for (i = 0; i < noOfButtons; ) {
                        $("#BtnDiv > li > .btn-no").attr("data-btn-no", ++i).text(i);
                        $(".pagination").append($("#BtnDiv").html());
                    }

                    //adding active class to clicked page btn
                    $($(".pagination > li > a[data-btn-no='"+ selectedPageBtn +"']").closest('li')).addClass("active");
                    
                    var j = 0;

                    //writing data rows
                    for (i = pageStart; i < (pageSize * (selectedPageBtn - 1)) + data.hospitalModelList.length + 1; i++) {
                        $("#dataDiv > .row > .serial-no").text(i);

                        //showing pin icon if isPrimary
                        if (data.hospitalModelList[j].isPrimary == 1) {
                            $("#dataDiv > .row > .primary-mark").html("<i class='fa fa-thumb-tack fa-2x' aria-hidden='true'></i>");
                        }
                        else {
                            $("#dataDiv > .row > .primary-mark").text("-");
                        }

                        $("#dataDiv > .row > .hospital-name").text(data.hospitalModelList[j].name);
                        $("#dataDiv > .row > .hospital-address").text(data.hospitalModelList[j].address);
                        $("#dataDiv > .row > .hospital-phone").text(data.hospitalModelList[j].phoneNo);

                        //creating dynamic buttons
                        $("#dataDiv > .row > .hospital-edit > a.btn-edit").attr('data-hospitalId', data.hospitalModelList[j].hospitalId);
                        $("#dataDiv > .row > .hospital-view > a.btn-view").attr('data-hospitalId', data.hospitalModelList[j].hospitalId);
                        $("#dataDiv > .row > .hospital-delete > a.btn-delete").attr('data-hospitalId', data.hospitalModelList[j].hospitalId);

                        j++;

                        $("#hospitalGrid").append($("#dataDiv").html());
                    }

                    //writing total no of records
                    $("#totalNoOfRecords").text("Total No of Records : " + data.rowCount);
                }
                //Error
                else {
                    $("#errorText").text(data.error);
                    $("#hospitalGrid").html($("#ErrorDiv").html());
                }
            }
        });
    }

    //UpdateGrid();

    //$(".pagination").on("click", "li a.btn-no", function () {
    //    var btnNo = $(this).attr("data-btn-no");
    //    pageStart = (pageSize * (btnNo - 1)) + 1;

    //    selectedPageBtn = btnNo;

    //    UpdateGrid();
    //});

    //$("#pageSizeDropup").change(function () {
    //    pageSize = $(this).val();
    //    selectedPageBtn = 1;
    //    pageStart = 1;        

    //    UpdateGrid();
    //});

    $("#hospitalGrid").on("click", ".btn-delete", function () {
        var hospitalId = $(this).attr("data-hospitalId");

        var hospitalModelObject = {
            hospitalId: hospitalId,
            userId: userId
        };

        var dialog = $("<p class=''>Are you sure you want to delete this hospital record?</p>").dialog({
            buttons: {
                "Yes": function () {                    
                    $.ajax({
                        url: '/hospitalapi/deletehospital',
                        type: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(hospitalModelObject),
                        //dataType: "json",
                        success: function (data) {
                            console.log(data);
                            if (data == "True") {
                                Notify("Deleted Successfully", "success");
                                dialog.dialog('close');
                                UpdateGrid();
                            }
                            else {
                                Notify("Couldn't delete. Try refreshing the page", "danger");
                                dialog.dialog('close');
                                UpdateGrid();
                            }
                        }
                    });
                },
                "No": function () {
                    dialog.dialog('close');
                }
            }
        });                
    });

    //$("#hospitalGrid").on("click", ".btn-edit", function () {
    //    var hospitalId = $(this).attr("data-hospitalId");

    //    isEdit = true;
    //    ResetModal();

    //    FillData(hospitalId, false);
        
    //    $('#addNewModal').modal('show');
    //});

    //$("#hospitalGrid").on("click", ".btn-view", function () {
    //    var hospitalId = $(this).attr("data-hospitalId");

    //    FillData(hospitalId, true);        

    //    $('#addNewModal').modal('show');            
    //});

    function FillData(hospitalId, disabledState) {
        var hospitalModelObject = {
            hospitalId: hospitalId,
            userId: userId
        };
        $.ajax({
            url: '/hospitalapi/getsinglehospitaldetails',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(hospitalModelObject),
            dataType: "json",
            success: function (data) {
                ResetModal();

                //filling data
                $("#hospitalName").val(data.name);
                $("#hospitalAddress").val(data.address);
                $("#hospitalPhoneNo").val(data.phoneNo);
                $("#hospitalPhoneNo2").val(data.phoneNo2);
                $("#hospitalEmail").val(data.email);
                $("#hospitalPhoneNo").val(data.phoneNo);

                if (data.isPrimary == 1) {
                    $("#hospitalPrimaryMark").attr('checked', true);
                    $("#hospitalPrimaryMark").bootstrapToggle('on');

                    $("#noteDiv").removeClass("note-unmarked").removeClass("note-info").addClass('note-marked');
                    $("#noteText").html("This is the current Primary Marked Hospital. You can't unselect it.<br />");
                    $("#noteText").append("If you want to mark another hospital as Primary " +
                        "then goto that hospital details and Mark it as Primary.");
                    $("#hospitalPrimaryMark").attr('disabled', true);
                }
                else {
                    $("#hospitalPrimaryMark").attr('checked', false);
                    $("#hospitalPrimaryMark").bootstrapToggle('off');
                }

                //enable editing
                isEdit = true;
                editableHospitalId = hospitalId;

                //add disabled to all input fields
                if (disabledState == true) {
                    //input disabled
                    $("#inputDiv .form-control").addClass('well').attr('disabled', true);
                    $("#hospitalPrimaryMark").attr('disabled', true);

                    //filling note with logged dates
                    $("#noteDiv").removeClass("note-unmarked").addClass("note-info");
                    $("#noteText").text("Creation Date  : " + data.createdOn);
                    $("#noteText").append("<br />Last Updation Date : " + data.updatedOn);

                    //hiding buttons
                    $("#modalAddBtn").hide();
                    $("#modalResetBtn").hide();
                    $("#modalCancelBtn").text("Done");
                }
            }
        });
    }

    ResetModal = function () {
        //resetting the modal to default
        $("#notificationDiv").hide();
        $("#inputDiv").show();
        $("#addNewModal").find('form')[0].reset();
        $("#emailValidationDiv").addClass("hidden");
        $("#hospitalPrimaryMark").attr("checked", false);
        $("#hospitalPrimaryMark").bootstrapToggle('off');

        //removing disabled states
        $("#inputDiv .form-control").removeClass('well').attr('disabled', false);
        $("#hospitalPrimaryMark").attr('disabled', false);

        //resetting note
        $("#noteDiv").removeClass("note-marked").removeClass("note-info").addClass("note-unmarked");;        
        $("#noteText").text("If you select this Hospital as the primary mark, " +
            "then the existing primary marked Hospital will be unmarked.");

        //resetting buttons
        $("#modalAddBtn").show();
        $("#modalResetBtn").show();
        $("#modalCancelBtn").text("Cancel");

        //resetting toggle button
        $("#hospitalPrimaryMark").attr('checked', false);
        $("#hospitalPrimaryMark").bootstrapToggle('off');

        //setting text depending on action
        if (isEdit == true) {
            $("#modalAddBtn").text("Update");
        }
        else {
            $("#modalAddBtn").text("Add");
        }
    }

    $(".arrow-down").hide();    

    $(".hover-color").click(function () {
        $(this).find(".arrow-up, .arrow-down").toggle()
        $(this).siblings().removeClass("sort-active");
        $(this).addClass("sort-active");
        sortBy = $($(this).find("span:visible")).attr("data-sort");        

        UpdateGrid();
    });

});