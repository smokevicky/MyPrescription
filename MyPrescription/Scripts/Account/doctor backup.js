$(document).ready(function () {
    $("#DoctorsListItem").addClass("active");

    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;

    var userId = $("#userIdSessionVariable").val();

    //toggle between insert and update
    var isEdit = false;
    var editableDoctorId = null;

    table = $('#example').DataTable({
        responsive: true,
        ajax: {
            url: '/api/doctor/getdoctordetails',
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
            {
                data: null,
                className: "center",
                defaultContent: '<a href="" class="editor_edit btn btn-warning"><i class="fa fa-pencil" aria-hidden="true"></i></a>'
            },
            {
                data: null,
                className: "center",
                defaultContent: '<a href="" class="editor_remove btn btn-danger"><i class="fa fa-trash" aria-hidden="true"></i></a>'
            }
        ],
        aoColumnDefs: [{ "bSortable": false, "aTargets": [5, 6] }],
        columnDefs: [
                    { responsivePriority: 2, targets: 5 },
                    { responsivePriority: 1, targets: 6 }
        ],
        lengthMenu: [5, 7, 10, 15],
        pageLength: 10,
        deferRender: true,
        scrollY: "61vh",
        scrollCollapse: true
    });

    //UpdateCount = function () {
    //    alert(table.data().count());
    //}

    UpdateDataTable = function () {
        table.ajax.reload(null, false);
        //UpdateCount();
    }

    UpdateDataTable();

    //$(table.column(2).header()).addClass('all');
    //table.responsive.rebuild();
    //table.responsive.recalc();            

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
                $("#notificationDiv").html("<div class='text-center'><img height='35' src='../Resources/Images/ripple.gif' />Please wait...</div>");
                

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
                            url: '/api/doctor/addnewdoctor',
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
                    alert("edit here");
                }
            }
        }

        return false;
    });

    // Edit a record
    $('#example').on('click', 'a.editor_edit', function (e) {
        e.preventDefault();
        alert('edit');
    });

    // Delete a record
    $('#example').on('click', 'a.editor_remove', function (e) {
        e.preventDefault();
        alert('delete');
    });

    ResetModal = function () {
        //resetting the modal to default
        $("#notificationDiv").hide();
        $("#inputDiv").show();
        $("#addNewModal").find('form')[0].reset();
        $("#emailValidationDiv").addClass("hidden");
        $("#hospitalValidationDiv").addClass("hidden");
        $("#doctorPrimaryMark").attr("checked", false).bootstrapToggle('off');

        //removing disabled states
        $("#inputDiv .form-control").removeClass('well').attr('disabled', false);
        $("#doctorPrimaryMark").attr('disabled', false);        

        //resetting note
        $("#noteDiv").removeClass("note-marked").removeClass("note-info").addClass("note-unmarked");;
        $("#noteText").text("If you select this Hospital as the primary mark, then the existing primary marked Hospital will be unmarked.");

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

    $("#modalCancelBtn").click(function () {
        $('#addNewModal').modal('hide');
    });

});