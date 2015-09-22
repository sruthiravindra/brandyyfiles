<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/brands/brandsMasterPage.master" CodeFile="store-purchase-rewards-add.aspx.cs" Inherits="brands_store_purchase_rewards_add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                        Add Store Purchase Rewards                        
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>homepage.aspx"> Home </a></li>                         
                        <li class="active">Add Store Purchase Rewards</li>                        
                    </ol>
                </section> 
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>                        
                        <small>Add Store Purchase Rewards</small>
                    </h1>                    
                    <ol class="helptext breadcrumb ">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>help.aspx#2_0"><i class="fa fa-fw fa-question-circle"></i> Help: How to Add Store Purchase Rewards</a></li>                        
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
                                           <div class="row">
                                           <div class="form-group col-md-12">                                                        
                                               <label for="txtold" class="control-label">
                                                       Select The Campaign User Has Participated In* <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="right" title="You Can only select from active 'Reward Loyalty' Campaign"></i></label>
                                               <div>
                                                   <div  class="col-md-12">
                                                       <asp:DropDownList runat="server" ID="drpCampaigns"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="No Camapign Selected"
                            ValidationGroup="save" ControlToValidate="drpCampaigns" InitialValue="" ForeColor="Red" />
                                                   </div>
                                               </div>
                                           </div>
                                           </div>
                                           <div class="row">
                                            <div class="form-group  col-md-4" id="divPassword" runat="server">                                                                                              
                                               <label for="txtnew" class="control-label">
                                                          Enter User email Id * <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="This should be the email id that the user uses to login to brandyy"></i></label>
                                               <div>
                                                   <div  class="col-md-12">
                                                       <asp:TextBox ID="txtEmailID" runat="server"  CssClass="form-control"/>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="This Email Id doesnt exist"
                            ValidationGroup="save" ControlToValidate="txtEmailID" InitialValue="" ForeColor="Red" />
                                                   </div>
                                               </div>
                                           </div>                                            
                                           </div>
                                           <div class="row">
                                            <div class="form-group  col-md-4" runat="server">                                                                                              
                                               <label for="txtnew" class="control-label">
                                                          Enter the bill amount * <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="The bill amoutn will be used to calculate the brandyy points"></i></label>
                                               <div>
                                                   <div  class="col-md-12">
                                                       <asp:TextBox ID="txtBillAmount" runat="server"  CssClass="form-control"/>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter the bill amount"
                            ValidationGroup="save" ControlToValidate="txtBillAmount" InitialValue="" ForeColor="Red" />
                                                   </div>
                                               </div>
                                           </div>      
                                            <div class="form-group  col-md-4" runat="server">                                                                                              
                                               <label for="txtnew" class="control-label">
                                                          Enter the invoice number <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Invoice number id optional. Only required in case on enquiry"></i></label>
                                               <div>
                                                   <div  class="col-md-12">
                                                       <asp:TextBox ID="txtInvoiceNumber" runat="server"  CssClass="form-control"/>                                            
                                                   </div>
                                               </div>
                                           </div>                                      
                                           </div>
                                            <div class="form-actions text-left pal   col-md-8">                       
                                           <asp:Button runat="server" Text="Add Reward" ID="btnUpdate" CssClass="btn btn-primary" OnClick="btnUpdate_Click" ValidationGroup="save"/>      

                                       </div>
                                            <div class="form-actions text-left pal   col-md-8">                       
                                                <asp:Label runat="server" ID="lblRewardBrandyyPoints" ForeColor="Red"></asp:Label>
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

