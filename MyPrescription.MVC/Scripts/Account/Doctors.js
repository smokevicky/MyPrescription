$(document).ready(function () {
    $("#DoctorsListItem").addClass("active");

    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;

    var userId = $("#userIdSessionVariable").val();

    //toggle between insert and update
    var isEdit = false;
    var editableDoctorId = null;

    IntializeDataTable = function () {
        table = $('#example').DataTable({
            responsive: true,
            ajax: {
                url: '/doctorapi/getdoctordetails',
                dataType: "json",
                dataSrc: function (data) {
                    return data.doctorModelList;
                }
            },
            //aaData: Never use aaData ,
            columns: [
                { data: "row" },
                { data: "isPrimary" },
                { data: "name" },
                { data: "address" },
                { data: "phoneNo" },
                { data: "doctorId" },
                { data: "doctorId" }
            ],            
            columnDefs: [{
                targets: 1,
                mRender: function (data, type, full) {
                    if (data == 1) {
                        return "<i class='fa fa-thumb-tack fa-2x theme-color' aria-hidden='true'></i>";
                    }
                    return "-";                    
                }
            }, {
                responsivePriority: 2,
                targets: 5,
                mRender: function (data, type, full) {
                    return '<a data-doctorId=' + data + ' class="edit-btn btn btn-warning">' +
                        '<i class="fa fa-pencil" aria-hidden="true"></i></a>';
                }
            }, {
                responsivePriority: 1,
                targets: 6,
                mRender: function (data, type, full) {
                    return '<a data-doctorId=' + data + ' class="delete-btn btn btn-danger">' +
                        '<i class="fa fa-trash" aria-hidden="true"></i></a>';
                }
            }, {
                aTargets: [5, 6],
                bSortable: false
            }],
            lengthMenu: [5, 7, 10, 15],
            pageLength: 7,
            deferRender: true,
            scrollY: "61vh",
            scrollCollapse: true
        });
    }

    IntializeDataTable();

    UpdateDataTable = function () {
        table.ajax.reload(null, false);
    }           

    // Add a new record
    $("#addBtn").click(function () {
        isEdit = false;
        ResetModal();
        $('#addNewModal').modal('show');
    });

    // Modal form submit
    $("#modalAddBtn").click(function () {
        var dName = $("#doctorName").val();
        var dAddress = $("#doctorAddress").val();
        var dPhoneNo = $("#doctorPhoneNo").val();
        var dPhoneNo2 = $("#doctorPhoneNo2").val();
        var dEmail = $("#doctorEmail").val();
        var dHospital = $("#hospitalsList").val();
        var dPrimary = $("#doctorPrimaryMark").is(':checked') ? 1 : 0;

        if ((dName == "") || (dAddress == "") || (dPhoneNo == "") || (dEmail == "") || (dHospital == "")) {
            return;
        }
        else {

            if (!regex.test(dEmail)) {
                $("#emailValidationDiv").text("Please Enter Email in proper format i.e. abc@xyz.com");
                $("#emailValidationDiv").removeClass("hidden");
                $("#hospitalValidationDiv").addClass("hidden");
            }
            else if (dHospital == "0") {
                $("#emailValidationDiv").addClass("hidden");
                $("#hospitalValidationDiv").text("Please choose a valid Hospital");
                $("#hospitalValidationDiv").removeClass("hidden");
            }
            else {
                $("#inputDiv").hide();
                $("#notificationDiv").show();
                $("#notificationDiv").html("<div class='text-center'>" +
                    "<img height='35' src='/Resources/Images/ripple.gif' />Please wait...</div>");
                

                if (isEdit == false) {
                    var doctorModelObject = {
                        name : dName,
                        address: dAddress,
                        phoneNo: dPhoneNo,
                        phoneNo2: dPhoneNo2,
                        email: dEmail,
                        hospitalId: dHospital,
                        isPrimary: dPrimary
                    }

                    setTimeout(function () {
                        $.ajax({
                            url: '/doctorapi/addnewdoctor',
                            type: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            data: JSON.stringify(doctorModelObject),
                            dataType: "json",
                            success: function (data) {                                
                                if (data == true) {                                    
                                    setTimeout(function () {
                                        $('#addNewModal').modal('hide');
                                        Notify("Added Successfully", "success");
                                    }, 500);
                                    //updating data table without reseting selected page
                                    UpdateDataTable();
                                }
                                else {                                    
                                    setTimeout(function () {
                                        $('#addNewModal').modal('hide');
                                        Notify("Unknown error has occured. Kindly retry the same", "danger");
                                    }, 500);
                                }
                            }
                        });
                    }, 500);
                }
                else {
                    var doctorModelObject = {
                        doctorId: editableDoctorId,
                        name: dName,
                        address: dAddress,
                        phoneNo: dPhoneNo,
                        phoneNo2: dPhoneNo2,
                        email: dEmail,
                        hospitalId: dHospital,
                        isPrimary: dPrimary
                    }

                    setTimeout(function () {
                        $.ajax({
                            url: '/doctorapi/updatedoctordetails',
                            type: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            data: JSON.stringify(doctorModelObject),
                            dataType: "json",
                            success: function (data) {
                                if (data == true) {
                                    setTimeout(function () {
                                        $('#addNewModal').modal('hide');
                                        Notify("Updated Successfully", "success");
                                    }, 500);
                                    //updating data table without reseting selected page
                                    UpdateDataTable();
                                }
                                else {
                                    setTimeout(function () {
                                        $('#addNewModal').modal('hide');
                                        Notify("Unknown error has occured. Kindly retry the same", "danger");
                                    }, 500);
                                }
                            }
                        });
                    }, 500);
                }
            }
        }

        return false;
    });

    // Edit a record
    $('#example').on('click', 'a.edit-btn', function (e) {
        e.preventDefault();
        var doctorId = $(this).attr("data-doctorId");        

        isEdit = true;
        ResetModal();

        FillData(doctorId, false);

        $('#addNewModal').modal('show');
    });

    function FillData(doctorId, disabledState) {
        $.ajax({
            url: '/doctorapi/getsingledoctordetails',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ doctorId: doctorId }),
            dataType: "json",
            success: function (data) {
                ResetModal();
                //filling data
                $("#doctorName").val(data.name);
                $("#doctorAddress").val(data.address);
                $("#doctorPhoneNo").val(data.phoneNo);
                $("#doctorPhoneNo2").val(data.phoneNo2);
                $("#doctorEmail").val(data.email);
                $("#doctorPhoneNo").val(data.phoneNo);
                $("#hospitalsList").val(data.hospitalId);

                if (data.isPrimary == 1) {
                    $("#doctorPrimaryMark").attr('checked', true);
                    $("#doctorPrimaryMark").bootstrapToggle('on');

                    $("#noteDiv").removeClass("note-unmarked").removeClass("note-info").addClass('note-marked');
                    $("#noteText").html("This Doctor is the current Primary Marked Doctor. You can't unselect him/her.<br />");
                    $("#noteText").append("If you want to mark any other doctor as Primary " +
                        "then goto that particular doctor details and Mark him/her as Primary.");
                    $("#doctorPrimaryMark").attr('disabled', true);
                }
                else {
                    $("#doctorPrimaryMark").attr('checked', false);
                    $("#doctorPrimaryMark").bootstrapToggle('off');
                }

                //enable editing
                isEdit = true;
                editableDoctorId = doctorId;

                //add disabled to all input fields
                if (disabledState == true) {
                    //input disabled
                    $("#inputDiv .form-control").addClass('well').attr('disabled', true);
                    $("#doctorPrimaryMark").attr('disabled', true);

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
    };

    // Delete a record
    $('#example').on('click', 'a.delete-btn', function (e) {
        e.preventDefault();
        var doctorId = $(this).attr("data-doctorId");

        var dialog = $("<p class=''>Are you sure you want to delete this hospital record?</p>").dialog({
            buttons: {
                "Yes": function () {
                    $.ajax({
                        url: '/doctorapi/deletedoctor',
                        type: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ doctorId: doctorId }),
                        dataType: "json",
                        success: function (data) {
                            if (data == true) {
                                dialog.dialog('close');
                                Notify("Deleted Successfully", "success");
                                UpdateDataTable();
                            }
                            else {
                                dialog.dialog('close');
                                Notify("Couldn't delete. Try refreshing the page", "danger");
                                UpdateDataTable();
                            }
                        }
                    });
                },
                "No": function () {
                    dialog.dialog('close');
                    Notify("Nothing is deleted", "info");
                }
            }
        });

    });

    ResetModal = function() {
        //resetting the modal to default
        $("#notificationDiv").hide();
        $("#inputDiv").show();
        $("#addNewModal").find('form')[0].reset();
        $("#emailValidationDiv").addClass("hidden");
        $("#hospitalValidationDiv").addClass("hidden");
        $("#doctorPrimaryMark").attr("checked", false).bootstrapToggle('off');
        $("#hospitalsList").val("0");

        //removing disabled states
        $("#inputDiv .form-control").removeClass('well').attr('disabled', false);
        $("#doctorPrimaryMark").attr('disabled', false);

        //resetting note
        $("#noteDiv").removeClass("note-marked").removeClass("note-info").addClass("note-unmarked");;
        $("#noteText")
            .text("If you select this Doctor as the primary mark, " +
                "then the existing primary marked Doctor will be unmarked.");

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
        } else {
            $("#modalAddBtn").text("Add");
        }
    };

    $("#modalCancelBtn").click(function () {
        $('#addNewModal').modal('hide');
    });

});