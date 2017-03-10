<%@ Page Title="" Language="C#" MasterPageFile="~/Account/Account.Master" AutoEventWireup="true" CodeBehind="Hospitals.aspx.cs" 
    Inherits="MyPrescription.Account.Hospitals" ClientIDMode="Static" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Hospitals</title>
    <link href="../CSS/Account/Hospitals.css" rel="stylesheet" />       
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper">        

            <div class="wrapper-heading">                
                <h3>Hospitals</h3>
                <br />

                <div class="row text-bold">
                    <div class="col-xs-1 hover-color sort-active" id="gridSerialNo" aria-disabled="true" >
                        Sl. No.
                    </div>
                    <div class="col-xs-1 hover-color" id="primaryHeader">
                        Primary(<span class="arrow-up" data-sort="primary-up">&#9650;</span><span class="arrow-down" data-sort="primary-down">&#9660;</span>)
                    </div>
                    <div class="col-xs-2 hover-color">
                        Name(<span class="arrow-up" data-sort="name-up">&#9650;</span><span class="arrow-down" data-sort="name-down">&#9660;</span>)
                    </div>
                    <div class="col-xs-3 hover-color" id="addressHeader">
                        Address(<span class="arrow-up" data-sort="address-up">&#9650;</span><span class="arrow-down" data-sort="address-down">&#9660;</span>)
                    </div>
                    <div class="col-xs-2 hover-color">
                        Ph No(<span class="arrow-up" data-sort="ph-up">&#9650;</span><span class="arrow-down" data-sort="ph-down">&#9660;</span>)
                    </div>
                    <div class="col-xs-1">
                        Edit
                    </div>
                    <div class="col-xs-1">
                        View
                    </div>
                    <div class="col-xs-1">
                        Delete
                    </div>
                </div>

                <input type="hidden" id="userIdSessionVariable" runat="server" />
            </div>

            <div class="wrapper-body" id="hospitalGrid">
              
            </div>

            <div class="wrapper-footer">

                <div class="col-sm-3 col-xs-12">                    
                     <div class="col-sm-6">                      
                         <b>Select Page Size:</b>
                     </div>
                     <div class="col-sm-6">
                         <select class="form-control" id="pageSizeDropup">
                             <option value="5">5</option>
                             <option value="7">7</option>
                             <option value="10">10 (Recommended)</option>
                             <option value="15">15</option>
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
                    <div id="totalNoOfRecords" class="text-danger"></div>
                </div> 

                <div class="col-sm-3 col-xs-12">
                    <a class="btn btn-action pull-right" id="addBtn">Add</a>
                </div>

            </div>
    </div>

    <!--Reference Divs-->
        
    <div id="dataDiv" class="hidden">
        <div class="row">
            <div class="col-xs-1 serial-no">
                #1
            </div>
            <div class="col-xs-1 text-center primary-mark">
                <i class="fa fa-thumb-tack fa-2x" aria-hidden="true"></i>
            </div>
            <div class="col-xs-2 hospital-name">
                #NAME
            </div>
            <div class="col-xs-3 hospital-address">
                #ADDRESS
            </div>
            <div class="col-xs-2 hospital-phone">
                #PHONE
            </div>
            <div class="col-xs-1 hospital-edit">
                <a class="btn btn-warning btn-edit" data-hospitalId="#HOSPITALID"><i class="fa fa-pencil" aria-hidden="true"></i></a>
            </div>
            <div class="col-xs-1 hospital-view">
                <a class="btn btn-info btn-view" data-hospitalId="#HOSPITALID"><i class="fa fa-table" aria-hidden="true"></i></a>
            </div>
            <div class="col-xs-1 hospital-delete">
                <a class="btn btn-danger btn-delete" data-hospitalId="#HOSPITALID"><i class="fa fa-trash" aria-hidden="true"></i></a>
            </div>
        </div>
    </div>

    <div id="EmptyDiv" class="hidden">
        <div class="h3 text-center text-muted">
            <i class='fa fa-chain-broken fa-4x' aria-hidden='true'></i>
            <br />
            There's nothing here
            <br />
            Add new hospital details by clicking on the
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

    <div id="BtnDiv" class="hidden">
        <li><a data-btn-no="#1" class="btn-no">#1</a></li>
    </div>

    <!-- addNewModal -->
    <div class="modal fade" id="addNewModal" role="dialog">
        <div class="modal-dialog">              
          <div class="modal-content">
              <form>                  
                               
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Add new Hospital details</h4>
                  </div>
                  
                  <div class="modal-body" id="modalBody">

                      <div id="inputDiv">

                        <div class="row form-group ">
                            <div class="col-xs-3 vertialCenter">
                                Hospital Name:
                            </div>
                            <div class="col-xs-9">
                                <input type="text" id="hospitalName" class="form-control" placeholder="Enter Hospital Name" 
                                    maxlength="100" required="required" />
                            </div>
                        </div>

                        <div class="row form-group">
                            <div class="col-xs-3">
                                Address:
                            </div>
                            <div class="col-xs-9">
                                <input type="text" id="hospitalAddress" class="form-control" placeholder="Enter address" 
                                    maxlength="100" required="required" />
                            </div>
                        </div>

                        <div class="row form-group">
                            <div class="col-xs-3">
                                Phone No:
                            </div>
                            <div class="col-xs-9">
                                <input type="number" id="hospitalPhoneNo" class="form-control" placeholder="Enter primary phone no"
                                    maxlength="10" required="required"
                                    oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" />                                
                            </div>
                        </div>

                        <div class="row form-group">
                            <div class="col-xs-3">
                                Phone No:
                            </div>
                            <div class="col-xs-9">
                                <input type="number" id="hospitalPhoneNo2" class="form-control" 
                                    placeholder="Enter secondary phone no (optional)" 
                                    maxlength="11" 
                                    oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" /> 
                            </div>
                        </div>

                        <div class="row form-group">
                            <div class="col-xs-3">
                                EMail-Id:
                            </div>
                            <div class="col-xs-9">
                                <input type="email" id="hospitalEmail" class="form-control" 
                                    placeholder="Enter Email-Id of the Hospital" 
                                    maxlength="50" required="required" />
                            </div>                            
                        </div>

                        <div class="row">
                            <div class="col-xs-3">
                            </div>
                            <div class="col-xs-9 error" id="emailValidationDiv">
                            </div>
                        </div>

                        <div class="row form-group">
                            <div class="col-xs-5">
                                Mark as Primary ???
                            </div>
                            <div class="col-xs-7">
                                <input id="hospitalPrimaryMark" type="checkbox" data-toggle="toggle" 
                                    data-on="<i class='fa fa-thumb-tack' aria-hidden='true'></i> Marked" 
                                    data-off="<i class='fa fa-thumb-tack fa-rotate-180' aria-hidden='true'></i> Unmarked" 
                                    data-width="110" data-onstyle="action" />                           
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
                    <a class="btn btn-default modal-cancel-btn" id="modalCancelBtn"></a>
                    <button type="reset" class="btn btn-warning modal-reset-btn" id="modalResetBtn">Reset</button>
                    <button type="submit" class="btn btn-action" id="modalAddBtn"></button>
                  </div>

              </form>  
          </div>      
        </div>
      </div>

    <script src="../Scripts/Account/Hospitals.js"></script>
</asp:Content>
