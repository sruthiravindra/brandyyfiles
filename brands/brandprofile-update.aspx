<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/brands/brandsMasterPage.master"  CodeFile="brandprofile-update.aspx.cs" Inherits="brands_brandprofile_update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                        Update Brand Name / Logo                        
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>homepage.aspx"> Home </a></li>                         
                        <li class="active">Update Brand Name/Logo</li>                        
                    </ol>
                </section> 
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>                        
                        <small>Update basic details like brand name and logo</small>
                    </h1>                    
                    <ol class="helptext breadcrumb ">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>help.aspx#2_0"><i class="fa fa-fw fa-question-circle"></i> Help: How to update brand logo and name</a></li>                        
                    </ol>
                    
                </section> 
     <!-- Main content -->
                <section class="content">
                    <div class="row">                        
                    
                        <asp:UpdatePanel ID="UpdatePanel_Main" runat="server">
                                <ContentTemplate>
                      <div class="col-lg-6">
                      <div class="box box-info">        
                          <div style="overflow: hidden;" class="box-body">
                              
                                    <div>
                                        <div class="box-body">
                                            <div class="form-group col-md-12" >
                                            <div class="logo" style="text-align:center;">
                                        <img src="<%=SessionState.WebsiteURLBrand + "uploads/logos/" + SessionState._BrandAdmin.brand_id + ".png" %>" alt="user image" class="offline" style="width:150px;">
                                                <p style="text-align:center;border-top:1px solid;margin-top:5px;"><br />
                                                    <div  class=" col-md-14" style="padding-left:0px;">
                                                       <div class="input-group input-group-sm">
                                                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />                                                        
                                                    <span class="input-group-btn" >
                                                        <asp:Button runat="server" Text="Upload Logo" ValidationGroup="upload" ID="btn_UploadLogo" OnClick="btn_UploadLogo_Click" CssClass="btn btn-primary" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* choose file" ValidationGroup="upload" ControlToValidate="FileUpload1" ForeColor="Red" />
                                                        </span>
                                                           </div></div>
                                                </p>                                        
                                        </div>
                                        </div>

                                                   
                                           <div class="form-group col-md-6" style="padding-left: 80px;display:none">                                                                                              
                                               <label for="txtBrandName" class="control-label">
                                                          <span class="text-light-blue">Brand Status</span> * <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Provide User Status"></i></label>
                                               <div>
                                                   <div  class=" col-md-6" style="padding-left:0px;">
                                                       <asp:RadioButtonList runat="server" ID="rdStatus">
                                                           <asp:ListItem Value="1">Active</asp:ListItem>
                                                           <asp:ListItem Value="0">Inactive</asp:ListItem>                                                           
                                                       </asp:RadioButtonList>                                                       
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Brand Status"
                            ValidationGroup="save" ControlToValidate="rdStatus" InitialValue="" ForeColor="Red" />
                                                   </div>
                                               </div>
                                           </div> 
 
                                                  <div class="form-group col-md-6" style="padding-left: 80px;display:none">                                         
                                                                                                                                                                                                            
                                               </div>                                                               
                                       </div> 
                                     </div>                                      
                                        
                                
                          </div>
                        </div>
                        </div>

                      <div class="col-lg-6">
                      <div class="box box-success">        
                          <div style="overflow: hidden;height:273px;" class="box-body">
                              <div class="box-header">
                                    <h3 class="box-title"> Specify the Brand Name</h3>                                    
                                </div>
                              <div class="form-group col-md-12">                                                                                              
                                               <label for="txtBrandName" class="control-label">
                                                          <span class="text-light-blue">Brand Name</span> * <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Provide Brand Name"></i></label>
                                               <div>
                                                   <div  class=" col-md-14" style="padding-left:0px;">
                                                       <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtBrandName" runat="server" CssClass="form-control" ></asp:TextBox>
                                        <span class="input-group-btn" >
                                            <asp:Button ID="btn_Update" runat="server" Text="Update Name"  class="btn btn-primary" OnClick="btn_Update_Click" ValidationGroup="save" />
                                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="red"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="Specify Brand Name"
                            ValidationGroup="save" ControlToValidate="txtBrandName" InitialValue="" ForeColor="Red" />
                                            <asp:Label ID="lblValidationErrors" runat="server" CssClass="my_status_notification label label-red" Visible="false"></asp:Label>                                           
                                        </span>
                                    </div>
                                                                    
                                            
                                                   </div>
                                               </div>
                                           </div>

                              </div>
                        </div>
                        </div>
                          </ContentTemplate>
                                  <Triggers>
                                      <asp:PostBackTrigger ControlID="btn_UploadLogo" />
                                  </Triggers>
                              </asp:UpdatePanel>
                  
                            </div>                    
                </section><!-- /.content -->
           
      <!--BEGIN CONTENT-->
        
     <!--END CONTENT-->      
     
</asp:Content>

