$(document).ready(function () {

    //fetching UserId
    var userId = $("#userIdSessionVariable").val();

    //setting pageValues
    var pageStart = 1;
    var pageSize = 4;
    $("#pageSizeDropup option[value='" + pageSize + "']").attr("selected", "selected");

    var selectedPageBtn = 1;

    //keeps list of uploaded files
    var files = [];
    var allowedFileTypes = ['jpg', 'jpeg', 'png', 'doc', 'pdf', 'docx'];
    var allowedFileSize = 10048576; //10mb

    $("#VaultListItem").addClass("active");

    $("#addNewBtn").click(function () {
        ResetModal();
        $("#addNewModal").modal('show');
    });

    $("#modalCancelBtn").click(function () {
        $("#addNewModal").modal('hide');
    });

    /*DateRange Start--*/
    var start = moment().subtract(29, 'days');
    var end = moment();

    function cb(start, end) {
        $('#dateRange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
    }

    $('#dateRange').daterangepicker({
        startDate: start,
        endDate: end,
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        }
    }, cb);

    cb(start, end);
    /*DateRange END--*/

    DropZoneReset = function () {
        var dropZoneDefaultHtml = "<i class='fa fa-cloud-upload fa-4x' aria-hidden='true'></i><br />Drop files here to upload to vault";
        $("#dropZone").html(dropZoneDefaultHtml);
        $("#dropZone").removeClass('color').removeClass('error-file-upload').removeClass('dropZone-shake').addClass('default');
    }

    DropZoneReset();

    DragOver = function (event) {
        event.stopPropagation();
        event.preventDefault();

        $("#dropZone").html("<i class='fa fa-cloud-upload fa-4x' aria-hidden='true'></i><br />Drop file(s)");
        $("#dropZone").removeClass('color').removeClass('error-file-upload').removeClass('dropZone-shake').addClass('color');
    };

    RefreshDropZone = function () {
        $("#dropZone").removeClass('color').removeClass('default').removeClass('error-file-upload').removeClass('dropZone-shake');
        $("#dropZone").html("");

        if (files.length == 0) {
            DropZoneReset();
        }
        else {        
            /* Consolidate the output element. */
            var form = $('#vaultForm');
            var data = new FormData(form);

            for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i]);
            $("#dropZone").append("<div class='row list-of-files'> \
                                      <div class='text-left col-xs-11'>" +
                                          files[i].name +
                                      "</div> \
                                      <a class='col-xs-1 btn-file-delete' data-file='" + files[i].name + "'> \
                                          <i class='fa fa-remove' aria-hidden='true'></i> \
                                      </a> \
                                   </div>");
            }
        }        
    };

    CheckIfAlreadyAdded = function (currentFileName) {
        var result = $.grep(files, function (e) {
            return e.name == currentFileName;
        });
        if (result.length == 0) {
            return false;
        }
        return true;
    };

    CheckFileType = function (currentFileName) {
        var extension = currentFileName.replace(/^.*\./, '').toLowerCase();
        var result = jQuery.inArray(extension, allowedFileTypes);

        if (result == -1) {
            return false;
        }
        return true;
    };

    CheckFileSize = function (currentFileSize) {
        if (currentFileSize < allowedFileSize) {
            return true;
        }
        return false;
    }

    //Checking all constraints before adding to queue
    CheckEligibility = function (currentFile) {
        if (CheckIfAlreadyAdded(currentFile.name) == false) {
            if (CheckFileType(currentFile.name) == true) {
                if (CheckFileSize(currentFile.size) == true) {
                    return true;
                }
                else {
                    Notify("File size limit exceeded. Check Instructions", "danger");
                }
            }
            else {
                Notify("Invalid File Type", "danger");
            }
        }
        else {
            Notify("File(s) already exist(s) in the Upload Queue", "danger");
        }
        return false;
    };

    Drop = function (event) {
        event.stopPropagation();
        event.preventDefault();

        // Read the list of all the dropped files.
        var tempFiles = event.dataTransfer.files;

        for (var i = 0; i < tempFiles.length; i++) {
            if (CheckEligibility(tempFiles[i]) == true) {
                files.push(tempFiles[i]);
            }
            else {
                //If multiple files are added at the same time
                if (tempFiles.length > 1) {
                    Notify("Some files were not valid. Check Instructions", 'danger');
                }
            }
        }
        
        RefreshDropZone();
    };

    DragLeave = function () {
        DropZoneReset();
    };

    HideAllValidations = function () {
        $("#vNameValidation, #hospitalsListValidation, #doctorsListValidation, \
            #vaultDateValidation, #typesListValidation").hide();
    };

    ResetModal = function () {
        //resetting the modal to default
        $("#notificationDiv").hide();
        $("#inputDiv").show();
        $("#addNewModal").find('form')[0].reset();

        //resetting note
        $("#noteDiv").removeClass("note-marked").removeClass("note-info").addClass("note-unmarked");;
        $("#noteText").html("One Vault can have multiple files in it.\
                            <br/>Allowed File Types : jpg, jpeg, png, doc, pdf, docx.\
                            <br/>Max File Size : 10Mb.");

        //resetting buttons
        $("#modalAddBtn").show();
        $("#modalResetBtn").show();
        $("#modalCancelBtn").text("Cancel");

        //Hiding all validation divs
        HideAllValidations();
        DropZoneReset();

        //setting max date for date
        var today = new Date().toISOString().split('T')[0];
        $("#vaultDate").attr('max', today);

        //Restting files in upload queue
        files = [];
    };

    $("#dropZone").on("click", ".btn-file-delete",  function () {
        var fileName = $(this).attr("data-file");
        files = $.grep(files, function (e) {
            return e.name != fileName;
        });
        RefreshDropZone();
    });

    Validate = function (vName, vHospital, vDoctor, vDate, vType) {
        HideAllValidations();
        var errorCount = 0;
        if ((vName == "")) {
            $("#vaultName").focus();
            $("#vNameValidation").show();
            errorCount++;
        }            
        if (vHospital == 0) {
            $("#hospitalsListValidation").show();
            errorCount++;
        }
        if (vDoctor == 0) {
            $("#doctorsListValidation").show();
            errorCount++;
        }
        if (vDate == "") {
            $("#vaultDateValidation").show();
            errorCount++;
        }
        if (vType == 0) {
            $("#typesListValidation").show();
            errorCount++;
        }
        if (files.length == 0) {
            $("#dropZone").html("<i class='fa fa-exclamation-triangle fa-4x' aria-hidden='true'></i><br/>\
                                You must upload a file.<br/>Drag and Drop a file to upload.");
            $("#dropZone").removeClass('color').removeClass('default')
                .addClass('error-file-upload').removeClass('dropZone-shake').addClass('dropZone-shake');
            errorCount++;
        }
        if (errorCount == 0) {
            return true;
        }
        return false;
    }

    $("#modalAddBtn").click(function () {
        vName = $("#vaultName").val();
        vHospital = $("#hospitalsList").val();
        vDoctor = $("#doctorsList").val();
        vDate = $("#vaultDate").val();
        vType = $("#typesList").val();

        if (Validate(vName, vHospital, vDoctor, vDate, vType)) {
            $("#inputDiv").hide();
            $("#notificationDiv").show();
            $("#notificationDiv").html("<div class='text-center'><img height='35' src='../Resources/Images/ripple.gif' />Please wait...</div>");
            
            var formData = new FormData();
            for (var i = 0; i < files.length; i++) {
                formData.append("file_"+[i], files[i]);
            }
            formData.append("vaultName", vName);
            formData.append("hospitalId", vHospital);
            formData.append("doctorId", vDoctor);
            formData.append("date", vDate);
            formData.append("recordId", vType);

            //Ajax call for saving into db
            setTimeout(function () {
                $.ajax({
                    url: './api/vault/addnewvault',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        if (data == true) {
                            setTimeout(function () {
                                $('#addNewModal').modal('hide');
                                Notify("Added successfully", 'success');
                            }, 500);
                            UpdateCards();
                        }
                        else {
                            setTimeout(function () {
                                $('#addNewModal').modal('hide');
                                Notify("Some error occured", 'danger');
                            }, 500);
                        }
                    },
                    error: function (error) {
                    }
                });
            }, 500);
        }

        return false;
    });

    UpdateCards = function () {
        var vaultRequestModelObject = {
            pageStart: pageStart,
            pageSize: pageSize
        }

        $.ajax({
            url: './api/vault/getvaultdetails',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(vaultRequestModelObject),
            dataType: "json",
            success: function (data) {                
                if (data.statusCode == 0) {
                    $("#vaultCards").html($("#EmptyDiv").html());
                }
                else if (data.statusCode == 1) {
                    $("#vaultCards").html("");
                    $(".pagination").html("");

                    //showing count on badge
                    $("#vaultsCountBadge").text(data.rowCount);

                    //Calculating no of page buttons to be created
                    var noOfButtons = data.rowCount / pageSize;

                    //creating page buttons
                    for (i = 0; i < noOfButtons;) {
                        $("#BtnDiv > li > .btn-no").attr("data-btn-no", ++i).text(i);
                        $(".pagination").append($("#BtnDiv").html());
                    }

                    //adding active class to clicked page btn
                    $($(".pagination > li > a[data-btn-no='" + selectedPageBtn + "']").closest('li')).addClass("active");

                    var j = 0;

                    //writing data rows
                    for (i = pageStart; i < (pageSize * (selectedPageBtn - 1)) + data.list.length + 1; i++) {
                        $("#dataDiv .card").attr("data-vaultId", data.list[j].vaultId);

                        $("#dataDiv .serial-no").text("#" + data.list[j].row);

                        $("#dataDiv .vault-name").text(data.list[j].vaultName);
                        $("#dataDiv .record-type").text(data.list[j].recordType);

                        //writing noOfFiles
                        var noOfFiles = data.list[j].noOfFiles;
                        $("#dataDiv .no-of-files").text(noOfFiles + " files");

                        //setting color based on noOfFiles
                        if (noOfFiles == 0) {
                            $("#dataDiv .no-of-files").removeClass('text-success').addClass('text-danger');
                        }
                        else {
                            $("#dataDiv .no-of-files").removeClass('text-danger').addClass('text-success');
                        }
                        
                        $("#dataDiv .doctor-name").text(data.list[j].doctorName);
                        $("#dataDiv .hospital-name").text(data.list[j].hospitalName);
                        $("#dataDiv .vault-date").text(data.list[j].date);

                        //creating dynamic buttons
                        $("#dataDiv .btn-download").attr('data-vaultId', data.list[j].vaultId);
                        $("#dataDiv .btn-delete").attr('data-vaultId', data.list[j].vaultId);

                        j++;

                        $("#vaultCards").append($("#dataDiv").html());
                    }

                    //writing total no of records
                    $("#totalNoOfVaults").text("Total No of Vaults : " + data.rowCount);
                }
                else {
                    $("#errorText").text(data.error);
                    $("#vaultCards").html($("#ErrorDiv").html());
                    Notify("Some error occured: "+ data.error, 'danger');
                }
            },
            error: function (error) {
            }
        });
    };

    UpdateCards();

    $(".pagination").on("click", "li a.btn-no", function () {
        var btnNo = $(this).attr("data-btn-no");
        pageStart = (pageSize * (btnNo - 1)) + 1;

        selectedPageBtn = btnNo;

        UpdateCards();
    });

    $("#pageSizeDropup").change(function () {
        pageSize = $(this).val();
        selectedPageBtn = 1;
        pageStart = 1;

        UpdateCards();
    });

    $("#vaultCards").on("click", ".btn-download", function (e) {
        var vaultId = $(this).attr("data-vaultId");

        window.open("Download.aspx?vaultId=" + vaultId, "_blank");
        Notify("Download has started", "success");
    });

    $("#vaultCards").on("click", ".btn-delete", function () {
        var vaultId = $(this).attr("data-vaultId");

        var dialog = $("<p class=''>Are you sure you want to delete this vault? \
                        <span class='text-danger'>All the files inside will be deleted.</span></p>").dialog({
            buttons: {
                "Yes": function () {
                    var vaultModelObject = {
                        vaultId: vaultId
                    };

                    $.ajax({
                        url: './api/vault/deletevault',
                        type: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(vaultModelObject),
                        dataType: "json",
                        success: function (data) {
                            if (data == true) {
                                Notify("Deleted Successfully",'success');
                                dialog.dialog('close');
                                UpdateCards();
                            }
                            else {
                                Notify("Couldn't delete. Try refreshing the page",'danger');
                                dialog.dialog('close');
                                UpdateCards();
                            }
                        }
                    });
                },
                "No": function () {
                    dialog.dialog('close');
                    Notify('Nothing is deleted','info')
                }
            }
        });
    });

    ResetDisplayFilesModal = function () {
        $("#filesDiv").html("");
        $("#infoName, #infoHospital, #infoDoctor, #infoDate, #infoType, #infoCreatedOn").html("");
    };

    FillData = function (vaultId) {
        ResetDisplayFilesModal();

        var vaultModelObject = {
            vaultId: vaultId
        }

        $.ajax({
            url: './api/vault/getsinglevaultdetails',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(vaultModelObject),
            dataType: "json",
            success: function (data) {
                console.log(data);
                if (data.statusCode == 1) {
                    $("#displayFilesModalHeader").text("Details of " + data.vaultName);

                    //filling info details
                    $("#infoName").text(data.vaultName);
                    $("#infoHospital").text(data.hospitalName);
                    $("#infoDoctor").text(data.doctorName);
                    $("#infoDate").text(data.date);
                    $("#infoType").text(data.recordType);
                    $("#infoCreatedOn").text(data.createdDate);

                    var extension;

                    //filling file details
                    for(i = 0; i < data.filesList.length; i++){
                        $("#fileDiv .file-name").text(data.filesList[i].fileName);
                        $("#fileDiv .created-on").text(data.filesList[i].createdOn);                                                

                        //adding attributes data-extension and data-fileId to view button
                        extension = data.filesList[i].fileName.substr((data.filesList[i].fileName.lastIndexOf('.') + 1));
                        $("#fileDiv .btn-view").attr("data-extension", extension);
                        $("#fileDiv .btn-view").attr("data-fileId", data.filesList[i].fileId);

                        //adding attribute data-fileId to download button
                        $("#fileDiv .btn-download").attr("data-fileId", data.filesList[i].fileId);

                        $("#filesDiv").append($("#fileDiv").html());
                    }
                }
                else {
                    Notify("Something went wrong",'danger');
                }
            }
        });
    };

    $("#filesDiv").on("click", ".btn-download", function () {
        var fileId = $(this).attr("data-fileId");

        window.open("Download.aspx?fileId=" + fileId, "_blank");
        Notify("Download has started", "success");
    });

    //Pops up a new window at the center of the screen
    function PopupCenter(url, title, w, h) {
        // Fixes dual-screen position                         Most browsers      Firefox
        var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
        var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

        var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
        var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

        var left = ((width / 2) - (w / 2)) + dualScreenLeft;
        var top = ((height / 2) - (h / 2)) + dualScreenTop;
        var newWindow = window.open(url, title, 'scrollbars=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);

        // Puts focus on the newWindow
        if (window.focus) {
            newWindow.focus();
        }
    }

    $("#filesDiv").on("click", ".btn-view", function () {
        var fileId = $(this).attr("data-fileId");
        var extension = $(this).attr("data-extension");

        var newWidth = $(window).width() - 100;
        var newHeight = $(window).height() - 50;

        PopupCenter("/file=" + fileId + "." + extension, "FILE", newWidth, newHeight);
    });

    $("#vaultCards").on("click", ".card-top", function () {
        var vaultId = $(this).closest('.card').attr("data-vaultId");

        FillData(vaultId);

        $("#displayFilesModal").modal('show');
    });

    $("#btnDone").click(function () {
        $("#displayFilesModal").modal('hide');
    });

    if (window.File && window.FileList && window.FileReader) {
        /************************************ 
         * All the File APIs are supported. * 
         * Entire code goes here.           *
         ************************************/


        /* Setup the Drag-n-Drop listeners. */
        var dropZone = document.getElementById('dropZone');
        dropZone.addEventListener('dragover', DragOver, false);
        dropZone.addEventListener('drop', Drop, false);

    }
    else {
        alert('Sorry! this browser does not support HTML5 File APIs.');
    }
});
