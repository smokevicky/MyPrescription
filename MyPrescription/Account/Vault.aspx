<%@ Page Title="" Language="C#" MasterPageFile="~/Account/Account.Master" AutoEventWireup="true" CodeBehind="Vault.aspx.cs" 
    Inherits="MyPrescription.Account.Vault" ClientIDMode="Static" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Vault</title>
    <link rel="stylesheet" type="text/css" href="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.css" />
    <link href="../CSS/Account/Vault.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="wrapper">
        <input type="hidden" id="userIdSessionVariable" runat="server" />
        <div class="wrapper-heading">
            <h3>Vault</h3>
            Filter by Date: 
            <div id="dateRange" class="form-control">
                <i class="glyphicon glyphicon-calendar fa fa-calendar"></i>&nbsp;
                <span></span>
                <b class="caret"></b>
            </div>
        </div>
        <div class="wrapper-body" id="vaultCards">

        </div>

        <div class="wrapper-footer">
            <div class="col-sm-3 col-xs-12">                    
                     <div class="col-sm-6">                      
                         <b>Select Page Size:</b>
                     </div>
                     <div class="col-sm-6">
                         <select class="form-control" id="pageSizeDropup">
                             <option value="2">2</option>
                             <option value="4">4 (Recommended)</option>
                             <option value="8">8</option>
                             <option value="12">12</option>
                         </select>
                      </div>
                </div>                
                
                <div class="col-sm-3 col-xs-12">
                    <div class="text-center">
                        <ul class="pagination">
                            
                        </ul>
                    </div>
                </div>
                
                <div class="col-sm-3 col-xs-12 text-center text-bold h4">
                    <div id="totalNoOfVaults" class="text-danger"></div>
                </div>

                <div class="col-sm-3 col-xs-12">
                    <a class="btn btn-action" id="addNewBtn">Add</a>
                </div>
        </div>
    </div>

        <!-- addNewModal -->
    <div class="modal fade" id="addNewModal" role="dialog">
        <div class="modal-dialog">              
          <div class="modal-content">
              <form runat="server" id="vaultForm" enctype="multipart/form-data">                  
                               
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Add new Vault details</h4>
                  </div>
                  
                  <div class="modal-body" id="modalBody">

                      <div id="inputDiv">

                        <div class="row" id="vNameValidation">
                            <div class="col-xs-3"></div>
                            <div class="col-xs-9 error">
                                You must give a vault Name
                            </div>
                        </div>

                        <div class="row form-group ">
                            <div class="col-xs-3 ">
                                Vault Name:<span class="error">&nbsp;*</span>
                            </div>
                            <div class="col-xs-9">
                                <input type="text" id="vaultName" class="form-control" placeholder="Enter a Name for the Vault"
                                    maxlength="100" required="required" />
                            </div>
                        </div>
                                                  
                        <div class="row" id="hospitalsListValidation">
                            <div class="col-xs-3"></div>
                            <div class="col-xs-9 error">
                                You must select a hospital
                            </div>
                        </div>

                        <div class="row form-group">
                            <div class="col-xs-3">
                                Related Hospital:<span class="error">&nbsp;*</span>
                            </div>
                            <div class="col-xs-9">                                
                                <asp:DropDownList ID = "hospitalsList" runat="server" required="required" class="form-control" >
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row" id="doctorsListValidation">
                            <div class="col-xs-3"></div>
                            <div class="col-xs-9 error">
                                You must select a doctor
                            </div>
                        </div>

                        <div class="row form-group">
                            <div class="col-xs-3">
                                Related Doctor:<span class="error">&nbsp;*</span>
                            </div>
                            <div class="col-xs-9">                                
                                <asp:DropDownList ID = "doctorsList" runat="server" required="required" class="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row" id="vaultDateValidation">
                            <div class="col-xs-3"></div>
                            <div class="col-xs-9 error">
                                You must select a date
                            </div>
                        </div>

                        <div class="row form-group">
                            <div class="col-xs-3">
                                Date:<span class="error">&nbsp;*</span>
                            </div>
                            <div class="col-xs-9">
                                <input type="date" id="vaultDate" class="form-control" required="required" />
                            </div>
                        </div>

                        <div class="row" id="typesListValidation">
                            <div class="col-xs-3"></div>
                            <div class="col-xs-9 error">
                                You must select a type
                            </div>
                        </div>

                        <div class="row form-group">
                            <div class="col-xs-3">
                                Record Type:<span class="error">&nbsp;*</span>
                            </div>
                            <div class="col-xs-9">                                
                                <asp:DropDownList ID = "typesList" runat="server" required="required" class="form-control text-capitalize" >
                                </asp:DropDownList>
                            </div>                          
                        </div>

                        <div class="row form-group">
                            <div class="col-xs-12">
                                <div id="dropZone" ondragleave="DragLeave()">
                                    
                                </div>
                            </div>
                        </div>

                        <div class="row form-group text-bold" id="noteDiv">
                            <div class="col-xs-3">
                                Note:
                            </div>
                            <div class="col-xs-9" id="noteText">
                                
                            </div>
                        </div>

                      </div>

                      <div id="notificationDiv">

                      </div>

                  </div>
                  
                  <div class="modal-footer">
                    <a class="btn btn-default modal-cancel-btn" id="modalCancelBtn">Cancel</a>
                    <button type="reset" class="btn btn-warning modal-reset-btn" id="modalResetBtn">Reset</button>
                    <button type="submit" class="btn btn-action" id="modalAddBtn">Add</button>
                  </div>

              </form>  
          </div>      
        </div>
      </div>

        <!-- display files modal-->
    <div class="modal fade" id="displayFilesModal" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header text-capitalize" id="displayFilesModalHeader">
                    
                </div>
                <div class="modal-body" id="displayFilesModalBody">
                    <div class="row">
                        <div class="col-sm-8" id="filesDiv">

                        </div>
                        <div class="col-sm-4" id="informationDiv">
                            Vault Name: <span class="text-bold text-capitalize" id="infoName">#VNAME</span><br/>
                            Hospital Name: <span class="text-bold" id="infoHospital">#HNAME</span><br/>
                            Doctor Name: <span class="text-bold" id="infoDoctor">#DNAME</span><br/>
                            Date: <span class="text-bold" id="infoDate">#DATE</span><br/>
                            Type: <span class="text-bold text-capitalize" id="infoType">#TYPE</span><br/>
                            Created On: <span class="text-bold" id="infoCreatedOn">#CREATEDON</span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <a class="btn btn-action" id="btnDone">Done</a>
                </div>
            </div>
        </div>
    </div>

        <!--Reference Divs-->    
    <div id="fileDiv" class="hidden">
        <div class="file">
            <div class="row">
                <div class="col-sm-8">
                    <span class="text-bold h3 file-name">#NAME</span>
                    <br />
                    <div class="text-info">
                        Created On: <span class="created-on">#CREATEDON</span>
                    </div>
                </div>
                <div class="col-sm-2">
                    <a class="btn btn-info btn-view" data-fileId="" title="View this file">
                        <i class="fa fa-eye" aria-hidden="true"></i>
                    </a>
                </div>
                <div class="col-sm-2">
                    <a class="btn btn-success btn-download" data-fileId="" title="Download this file">
                        <i class="fa fa-cloud-download" aria-hidden="true"></i>
                    </a>
                </div>
            </div>
        </div>
    </div>

    <div id="BtnDiv" class="hidden">
        <li><a data-btn-no="#1" class="btn-no">#1</a></li>
    </div>

    <div id="dataDiv" class="hidden">
        <div class="col-sm-6">
            <div class="card" data-vaultId="">
                <div class="text-capitalize">
                    <div class="card-top">
                        <div class="col-xs-12 h3"><b><span class="serial-no">#1</span> <span class="vault-name">#NAME</span></b></div>
                        <p class="col-xs-12 record-type text-info">#TYPE</p>
                        <p class="col-xs-12 h4 no-of-files">#NOOFFILES</p>                
                        <div class="col-xs-12 h4">Prescribed by <span class="text-bold doctor-name">#DOCTOR</span></div>
                        <p class="col-xs-12">from <span class="text-bold hospital-name">#HOSPITAL</span></p>
                        <p class="col-xs-12">on <span class="text-bold vault-date">#DATE</span></p>
                    </div>
                    <div class="text-center card-bottom">
                        <div class="col-xs-6">
                            <a class="btn btn-info btn-download" data-vaultId="#VAULTID"
                                data-toggle="tooltip" data-placement="bottom" title="Download all the files in the vault(.zip)" >
                                    <i class="fa fa-download" aria-hidden="true"></i></a>
                        </div>
                        <div class="col-xs-6">
                            <a class="btn btn-danger btn-delete" data-vaultId="#VAULTID"
                                data-toggle="tooltip" data-placement="bottom" title="Delete the vault incluing all its files" >
                                    <i class="fa fa-trash" aria-hidden="true"></i>   </a>
                        </div>
                    </div>
                </div>                    
            </div>
        </div>
    </div>

    <div id="EmptyDiv" class="hidden">
        <div class="h3 text-center text-muted">
            <i class='fa fa-chain-broken fa-4x' aria-hidden='true'></i>
            <br />
            There's nothing here
            <br />
            Add new vaults by clicking on the
            <br />
            <a class="btn btn-action disabled">Add</a>
            <br />
            button to see them here.
        </div>
    </div>

    <div id="ErrorDiv" class="hidden">
        <div class="error h3 text-center">
            <i class="fa fa-exclamation-triangle" aria-hidden="true"></i>
            <br />
            Error has occured.
            <div id="errorText"></div>
        </div>        
    </div>

    <script type="text/javascript" src="//cdn.jsdelivr.net/momentjs/latest/moment.min.js"></script>
    <script type="text/javascript" src="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.js"></script>    
    <script src="../Scripts/Account/Vault.js"></script>
</asp:Content>
