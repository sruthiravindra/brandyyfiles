<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/brands/brandsMasterPage.master"  CodeFile="socialmediapage-create.aspx.cs" Inherits="brands_socialmediapage_create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
     <!-- Main content -->
                <section class="content">
                    <div class="row">                        
                    <div class="col-lg-9">
                      <div class="box box-primary">        
                          <div class="box-header">
                                    <h3 class="box-title">Configure Page</h3>
                                </div><!-- /.box-header -->                            
                          <div style="overflow: hidden;" class="box-body">
                              <asp:UpdatePanel ID="UpdatePanel_Main" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <div class="box-body">
                                           <div class="form-group">                                                                                              
                                               <label for="txtReward_1" class="control-label">
                                                          Page Tag <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="This is the page tag that will be extracted based on the page url provided"></i></label>
                                               <div>
                                                   <div  class="col-md-12">
                                                       <asp:TextBox ID="txtPageName" runat="server"  CssClass="form-control" ReadOnly="true"/>                                            
                                                   </div>
                                               </div>
                                           </div>                                          
                                           <div class="form-group">                                                                                              
                                               <label for="txtReward_1" class="control-label">
                                                          Page Url * <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Please ensure that you are the admin for this page is similar to what you have configured for the social media"></i></label>
                                               <div>
                                                   <div  class="col-md-12">
                                                       <asp:TextBox ID="txtPageUrl" runat="server"  CssClass="form-control"/>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Specify a Page Url"
                            ValidationGroup="save" ControlToValidate="txtPageUrl" InitialValue="" ForeColor="Red" />
                                                   </div>
                                               </div>
                                           </div>
                                    
                                            <div class="form-actions text-right pal">                                           
                                           <asp:Label ID="lblValidationErrors" runat="server" CssClass="my_status_notification label label-red" Visible="false"></asp:Label>
                                           <asp:Button runat="server" Text="Create Page" ValidationGroup="save" ID="btn_Panel2" OnClick="btn_Panel2_Click" CssClass="btn btn-primary" />                                                                                        
                                           <asp:Button runat="server" Text="Update Page" ValidationGroup="save" ID="btn_Update" OnClick="btn_Update_Click" CssClass="btn btn-primary" Visible="false" />                                                                        
                                           <asp:Button runat="server" Text="Back" ID="btn_Back" OnClick="btn_Back_Click" CssClass="btn btn-primary" />                
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
