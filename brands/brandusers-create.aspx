<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/brands/brandsMasterPage.master"  CodeFile="brandusers-create.aspx.cs" Inherits="brands_brandusers_create" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                        <%=page_title %> User                        
                    </h1>                    
                    <ol class="breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>homepage.aspx"> Home </a></li> 
                        <li><a href="<%=SessionState.WebsiteURLBrand %>brandusers.aspx"> Users/Roles </a></li> 
                        <li class="active"><%=page_title %> User</li>                        
                    </ol>
                </section> 
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>                        
                        <small><%=page_title %> user basic profile details</small>
                    </h1>
                    <ol class="helptext breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>help.aspx#2_1"><i class="fa fa-fw fa-question-circle"></i> Help: How to Update Users Profile/Roles/Permissions</a></li>                        
                    </ol>
                </section>
     <!-- Main content -->
                <section class="content">
                    <div class="row">                        
                    <div class="col-lg-12">
                      <div class="box box-primary" >        
                          <div style="overflow: hidden;" class="box-body">
                              <asp:UpdatePanel ID="UpdatePanel_Main" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <div class="box-body"  >
                <asp:Label ID="lblerror" class="text-red"  runat="server" Text="User already exists" Visible="false"></asp:Label>
                                           <div class="form-group col-md-8">
                                               <label for="txtUserName" class="control-label">
                                                          User Name * <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Provide User Name"></i></label>
                                               <div>
                                                   <div  class=" col-md-12">
                                                       <asp:TextBox ID="txtUserName" runat="server"  CssClass="form-control"/>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="Specify User Name"
                            ValidationGroup="save" ControlToValidate="txtUserName" InitialValue="" ForeColor="Red" />
                                                   </div>
                                               </div>
                                           </div>   
                                           <div class="form-group  col-md-8">                                                       
                                               <label for="txtEmailID" class="control-label">
                                                          Email ID * <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Please ensure that you are the admin for this page is similar to what you have configured for the social media"></i></label>
                                               <div>
                                                   <div  class="col-md-12">
                                                       <asp:TextBox ID="txtEmailID" runat="server"  CssClass="form-control"/>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Specify email id"
                            ValidationGroup="save" ControlToValidate="txtEmailID" InitialValue="" ForeColor="Red" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"  ValidationGroup="save" ControlToValidate="txtEmailID"  ForeColor="Red" ErrorMessage="Invalid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ></asp:RegularExpressionValidator>
                                                   </div>
                                               </div>
                                           </div>    
                                           <div class="form-group  col-md-6" id="divPassword" runat="server">                                                                                              
                                               <label for="txtPassword" class="control-label">
                                                          Password * <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Please ensure that you are the admin for this page is similar to what you have configured for the social media"></i></label>
                                               <div>
                                                   <div  class="col-md-12">
                                                       <asp:TextBox TextMode="Password" ID="txtPassword" runat="server"  CssClass="form-control"/>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Specify password"
                            ValidationGroup="save" ControlToValidate="txtPassword" InitialValue="" ForeColor="Red" />
                                                   </div>
                                               </div>
                                           </div>
                                           <div class="form-group  col-md-6" id="divPasswordConfirm" runat="server">                                                                                              
                                               <label for="txtConfirmPassword" class="control-label">
                                                          Confirm Password * <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Please ensure that you are the admin for this page is similar to what you have configured for the social media"></i></label>
                                               <div>
                                                   <div  class="col-md-12">
                                                       <asp:TextBox TextMode="Password" ID="txtConfirmPassword" runat="server"  CssClass="form-control"/>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Confirm Password"
                            ValidationGroup="save" ControlToValidate="txtConfirmPassword" InitialValue="" ForeColor="Red" />
                                                       <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password doesn’t match"  ValidationGroup="save" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ForeColor="Red"></asp:CompareValidator>
                                                   </div>
                                               </div>
                                           </div>
                                           <div class="form-group col-md-6">                                                                                              
                                               <label for="txtUserName" class="control-label">
                                                          User Role * <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Provide User Name"></i></label>
                                               <div>
                                                   <div  class=" col-md-12">
                                <asp:DropDownList ID="drpRole" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Specify User Role"
                            ValidationGroup="save" ControlToValidate="drpRole" InitialValue="0" ForeColor="Red" />
                                                   </div>
                                               </div>
                                           </div>                                     
                                           
                                            
                                            <div class="form-actions text-left pal   col-md-8" style="margin-left:120px;">                                           
                                           <asp:Label ID="lblValidationErrors" runat="server" CssClass="my_status_notification label label-red" Visible="false"></asp:Label>
                                           <asp:Button runat="server" Text="Add New User" ValidationGroup="save" ID="btn_Panel2" OnClick="btn_Panel2_Click"  CssClass="btn btn-primary" />                                                                                        
                                           <asp:Button runat="server" Text="Edit User" ValidationGroup="save" ID="btn_Update" OnClick="btn_Update_Click" CssClass="btn btn-primary" Visible="false" />                                                                        
                                           <asp:Button runat="server" Text="BACK" ID="btn_Back" OnClick="btn_Back_Click" CssClass="btn btn-primary" />                
                                           <asp:Label ID="lblErrorMsg" runat="server" ForeColor="red"></asp:Label>

                                       </div>
                                       </div> 
                                     </div>                                      
                                        
                                </ContentTemplate>
                              </asp:UpdatePanel>
                          </div>
                        </div>
                  </div>
                            </div>                    
                </section><!-- /.content -->
           
      <!--BEGIN CONTENT-->
        
     <!--END CONTENT-->      
     
</asp:Content>

