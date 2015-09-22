<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/brands/brandsMasterPage.master" CodeFile="brand-update-campaign-1.aspx.cs" Inherits="brands_brand_update_campaign_1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate> 
                <!-- Main content -->
                <section class="content">
                    <div class="row">                        
                    <div class="col-lg-9">
                      <div class="box  box-primary">                                    
                          <div style="overflow: hidden;" class="portlet-body">
                              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>                                    
                                    <div style="margin:15px;">
                                        <div class="form-body pal">
                                           <div class="form-group">
                                               <label for="txtReward_1" class="control-label">
                                                           <span class="text-light-blue">Campaign Name</span> * <i class="fa fa-info-circle"></i></label>
                                               <div>
                                                   <div  class="col-md-12" style="padding-left:0px;">
                                                       <asp:TextBox ID="txtCampaignName1" runat="server"  CssClass="form-control"/>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="Specify a Campaign Name"
                            ValidationGroup="save" ControlToValidate="txtCampaignName1" InitialValue="" ForeColor="Red" />
                                                   </div>
                                               </div>
                                           </div>
                                           <div class="form-group" style=" margin-bottom: 0px;">
                                               <label for="txtReward_1" class="control-label">
                                                           <span class="text-light-blue">Schedule</span>   * <i class="fa fa-info-circle"></i></label>                                               
                                           </div>
                                           <div class="form-group">
                                                   <div  class="col-md-12" style="padding-left:0px;">
                                                       <asp:RadioButton ID="rdScheduleDaily" runat="server" GroupName="schedule" Text="Run my campaign set continuously starting today" AutoPostBack="true" Checked="true" OnCheckedChanged="RadioButton1_CheckedChanged" />                                                         
                                                   </div>
                                                   <div  class="col-md-12" style="padding-left:0px;">                                                       
                                                       <asp:RadioButton ID="rdScheduleDateBased" GroupName="schedule" runat="server" Text="Set a start and end date" OnCheckedChanged="RadioButton3_CheckedChanged" AutoPostBack="true" />  
                                                   </div>
                                               </div>

                                                  <div>&nbsp;</div>
                                    <div class="form-group" style=" margin-bottom: 0px;">                                                       
                                                        <label for="txtReward_1" class="control-label">
                                                         <span class="text-light-blue">Max Brandyy Points Per Activity</span> * 
                                                            <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Define what should be the max brandyy point that a user can by performing an activity"></i>
                                                        </label>                                                                                                  
                                           </div>
                                            <div class="form-group" >                                                       
                                                        <label for="txtReward_1" class="control-label">                                                           
                                                            <asp:TextBox ID="txtMaxBrandyyPoints" runat="server" Columns="10"/>                                                             
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Specify max BP" ValidationGroup="save" ControlToValidate="txtMaxBrandyyPoints" InitialValue="" ForeColor="Red" />                                                        </label>
                                                
                                            </div> 
                                            <div style="border-bottom:1px solid #e5e5e5">&nbsp;</div>
                                           <div class="row" id="divScheduleDates" runat="server" visible="false">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label for="txtStartDate1" class="control-label">
                                                          Start Date * <i class="fa fa-info-circle"></i></label>
                                                                <div class="input-icon right">
                                                                    <asp:TextBox ID="txtStartDate1" runat="server"  CssClass="form-control" ClientIDMode="Static"></asp:TextBox>                                                                                                                                        
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select Campaign Start Date"
                                ValidationGroup="save" ControlToValidate="txtStartDate1" InitialValue="" ForeColor="Red" />
                                                                    </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label for="txtStartDate1" class="control-label">
                                                          End Date * <i class="fa fa-info-circle"></i></label>
                                                                <div class="input-icon right">
                                                                    <asp:TextBox ID="txtEndDate1" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select Campaign End Date"
                                                            ValidationGroup="save" ControlToValidate="txtEndDate1" InitialValue="" ForeColor="Red" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                           
                                            <div runat="server" id="divShowPoints">
                                    <div class="portlet-header">
                                        <div class="caption">  <span class="text-light-blue"> Do you want the points calculation to be displayed to user on 'Offers' Page</span></div>
                                    </div>
                                     <div style="overflow: hidden;" class="portlet-body pan">
                                        <div class="form-body pal">                                            
                                            <div class="form-group">                                                       
                                                        <asp:DropDownList ID="drpDisplayRewardDetails" runat="server">
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                        </asp:DropDownList>
                                            </div>                                                                                                                                          
                                        </div>                                        
                                    </div>    
                                        </div>

                                            <div>
                                    <div class="portlet-header">
                                        <div class="caption">  <span class="text-light-blue">Is it compulsory for user to perform all actions <br />(applicalbe when campaign has multiple actions)</span>  </div>
                                    </div>
                                     <div style="overflow: hidden;" class="portlet-body pan">
                                        <div class="form-body pal">                                            
                                            <div class="form-group">                                                       
                                                        <asp:DropDownList ID="drpAllActionsComp" runat="server">
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>                                                            
                                                        </asp:DropDownList>
                                            </div>                                                                                                                                          
                                        </div>                                        
                                    </div>    
                                        </div>

                                      

                                       </div>
                                       <div class="form-actions text-right pal">                                           
                                           <asp:Label ID="lblErrorMsg" runat="server" ForeColor="red"></asp:Label>
                                           <asp:Button runat="server" Text="Update Campaign" ValidationGroup="save" ID="btn_Panel2" OnClick="btn_Panel2_Click" CssClass="btn btn-primary" />                                           
                                       </div>
                                    </div>
                                </ContentTemplate>
                              </asp:UpdatePanel>
                          </div>
                        </div>
                        </div>
                            </div> 
                  </section><!-- /.content -->
             </ContentTemplate>     
       </asp:UpdatePanel>
     <!--END CONTENT--> 
     
     <script src="<%=SessionState.WebsiteURLBrand+ "js/jquery-1.11.0.min.js"%>" type="text/javascript"></script>
      
     <script src="<%=SessionState.WebsiteURLBrand+ "jquery-ui-1.11.2.custom/jquery-ui.min.js"%>" type="text/javascript"></script>
    <link rel="stylesheet" href="<%=SessionState.WebsiteURLBrand+ "jquery-ui-1.11.2.custom/jquery-ui.css"%>" />
    <script type="text/javascript">
        function ShowOnLoadEvents() {
            $("[id$=txtStartDate1]").datepicker({
            });
            $("[id$=txtEndDate1]").datepicker({
            });
            $("[id$=txtRewardWhenDate]").datepicker({});

            $("[data-toggle='tooltip'], [data-hover='tooltip']").tooltip({
                position: { my: "left top+15 center", at: "right center" }
            });

            SelectMultipleCountries();
        }
  </script>
</asp:Content>