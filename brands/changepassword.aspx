<%@ Page Title="" Language="C#" MasterPageFile="~/brands/brandsMasterPage.master" AutoEventWireup="true" CodeFile="changepassword.aspx.cs" Inherits="brands_changepassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                        Change Password                        
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>homepage.aspx"> Home </a></li>                         
                        <li class="active">Change Password</li>                        
                    </ol>
                </section>
    <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>                        
                        <small>Change your login password</small>
                    </h1>
                    <ol class="helptext breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>help.aspx#2_2"><i class="fa fa-fw fa-question-circle"></i> Help: How can I change my password</a></li>                        
                    </ol>
                </section>
      <section class="content">
                    <div class="row">                        
                    <div class="col-lg-12">
                      <div class="box box-primary" >                                                             
                          <div style="overflow: hidden;" class="box-body">
                              <asp:UpdatePanel ID="UpdatePanel_Main" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <div class="box-body"  >
                                           <div class="row">
                                           <div class="form-group col-md-4">                                                        
                                               <label for="txtold" class="control-label">
                                                        Old Password * <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="right" title="Enter your old password"></i></label>
                                               <div>
                                                   <div  class=" col-md-12">
                                                       <asp:TextBox ID="txtold" runat="server"  CssClass="form-control" TextMode="Password" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="Enter your old password"
                            ValidationGroup="save" ControlToValidate="txtold" InitialValue="" ForeColor="Red" />
                                                   </div>
                                               </div>
                                           </div>
                                           </div>
                                           <div class="row">
                                            <div class="form-group  col-md-4" id="divPassword" runat="server">                                                                                              
                                               <label for="txtnew" class="control-label">
                                                          New Password * <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Please enter the new password"></i></label>
                                               <div>
                                                   <div  class="col-md-12">
                                                       <asp:TextBox TextMode="Password" ID="txtnew" runat="server"  CssClass="form-control"/>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter new password"
                            ValidationGroup="save" ControlToValidate="txtnew" InitialValue="" ForeColor="Red" />
                                                   </div>
                                               </div>
                                           </div>
                                            <div class="form-group  col-md-4" id="divPasswordConfirm" runat="server">                                                                                              
                                               <label for="txtConfirmPassword" class="control-label">
                                                          Confirm Password * <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Please retype the password to confirm that it matches with the new password"></i></label>
                                               <div>
                                                   <div  class="col-md-12">
                                                       <asp:TextBox TextMode="Password" ID="txtConfirmPassword" runat="server"  CssClass="form-control"/>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Retype password"
                            ValidationGroup="save" ControlToValidate="txtConfirmPassword" InitialValue="" ForeColor="Red" />
                                                       <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password doesn’t match"  ValidationGroup="save" ControlToCompare="txtnew" ControlToValidate="txtConfirmPassword" ForeColor="Red"></asp:CompareValidator>
                                                   </div>
                                               </div>
                                           </div>
                                           </div>
                                            <div class="form-actions text-left pal   col-md-8">                       
                                           <asp:Button runat="server" Text="Change Password" ID="btnUpdate" CssClass="btn btn-primary" OnClick="btnUpdate_Click" ValidationGroup="save"/>      

                                       </div>
                                       
                                       </div> 
                                     </div>                                      
                                              <div class="alert alert-danger alert-dismissable" id="divAlert" runat="server" visible="false"  style="margin-top:100px;">
                                        <i class="fa fa-ban"></i>
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <b>Alert!</b> <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
                                    </div>
                                </ContentTemplate>
                              </asp:UpdatePanel>
                          </div>
                          
                        </div>
                  </div>
                            </div>                    
                </section>
</asp:Content>

