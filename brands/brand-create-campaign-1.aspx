﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/brands/brandsMasterPage.master"  CodeFile="brand-create-campaign-1.aspx.cs" Inherits="brands_brand_create_campaign_1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
<!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                         Create Campaign - Step II                      
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>homepage.aspx"> Home </a></li> 
                        <li><a href="<%=SessionState.WebsiteURLBrand %>brand-create-campaign.aspx"> Create Campaign </a></li> 
                        <li class="active">Create Campaign - Step I</li>                         
                    </ol>
                </section> 
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>                        
                        <small>Setup basic campaign details. Selected Objective - <%=SessionState._Campaign.campaign_name %></small>
                    </h1>
                    <ol class="helptext breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>help.aspx?#3_1"><i class="fa fa-fw fa-question-circle"></i> Help: What are the campaign basic details</a></li>                        
                    </ol>
                </section> 
     <!-- Main content -->
                <section class="content">

<div class="col-lg-12" style="">
<div class="nav-tabs-custom">
                                <ul class="nav nav-tabs">
                                    <li><a href="#" class="text-muted">STEP I</a></li>                                                                        
                                    <li><asp:LinkButton runat="server" ID="lnkStep2" OnClick="lnkStep2_Click" CssClass="text-muted">STEP II</asp:LinkButton></li>                                                                        
                                    <li class="active"><a href="#tab_1" data-toggle="tab">STEP III - SETUP CAMPAIGN BASIC DETAILS</a></li>
                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab_1">
                    <div class="row">                        
                    <div class="col-lg-12">
                      <div class="box box-primary">        
                          <div class="box-header">
                                    <h3 class="box-title">Basic Settings</h3>
                                </div><!-- /.box-header -->                            
                          <div style="overflow: hidden;" class="box-body">
                              <asp:UpdatePanel ID="UpdatePanel_Main" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <div class="box-body">
                                           <div class="form-group">                                                                                              
                                               <label for="txtReward_1" class="control-label">
                                                 <span class="text-light-blue">Campaign Name</span> * <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Provide a unique name to your campaign"></i></label>
                                               <div>
                                                   <div  class="col-md-12" style="padding-left:0px;">
                                                       <asp:TextBox ID="txtCampaignName1" runat="server"  CssClass="form-control"/>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="Specify a Campaign Name"
                            ValidationGroup="save" ControlToValidate="txtCampaignName1" InitialValue="" ForeColor="Red" />
                                                   </div>
                                               </div>
                                           </div>
                                           
                                           <div >&nbsp;</div>
                                           <div class="form-group" style="border-bottom:1px solid #e5e5e5">
                                               <label for="txtReward_1" class="control-label">
                                                        <span class="text-light-blue">Schedule</span> * <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Your campaign will either run continuously starting today or within a date range you select"></i></label>
                                           </div>
                                           <div class="form-group">
                                                   <div  class="col-md-12" style="padding-left:0px;">
                                                       <asp:RadioButton ID="rdScheduleDaily" runat="server" GroupName="schedule" Text="Run my campaign set continuously starting today" AutoPostBack="true" Checked="true" OnCheckedChanged="RadioButton1_CheckedChanged" />                                                         
                                                   </div>
                                                   <div  class="col-md-12" style="padding-left:0px;">                                                       
                                                       <asp:RadioButton ID="rdScheduleDateBased" GroupName="schedule" runat="server" Text="Set a start and end date" OnCheckedChanged="RadioButton3_CheckedChanged" AutoPostBack="true" />  
                                                   </div>
                                               </div>                                              
                                           <div class="form-group">
                                               <div class="row" id="divScheduleDates" runat="server" visible="false">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label for="txtStartDate1" class="control-label">
                                                          Start Date *</label>
                                                                <div class="input-icon right">
                                                                    <asp:TextBox ID="txtStartDate1" runat="server"  CssClass="form-control" ClientIDMode="Static"></asp:TextBox>                                            
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Specify start date" ValidationGroup="save" ControlToValidate="txtStartDate1" InitialValue="" ForeColor="Red" />
                                                                    <asp:CustomValidator runat="server" ID="CustomValidator1" ControlToValidate="txtStartDate1" ValidationGroup="save" onservervalidate="valDateRange_ServerValidate" ErrorMessage="enter valid date" ForeColor="Red" />                                                                    
                                                                    </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label for="txtStartDate1" class="control-label">
                                                          End Date *</label>
                                                                <div class="input-icon right">
                                                                    <asp:TextBox ID="txtEndDate1" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>                                                                    
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Specify end date" ValidationGroup="save" ControlToValidate="txtEndDate1" InitialValue="" ForeColor="Red" />
                                                                    <asp:CustomValidator runat="server" ID="valDateRange" ControlToValidate="txtEndDate1" ValidationGroup="save" onservervalidate="valDateRange_ServerValidate" ErrorMessage="enter valid date" ForeColor="Red" />                                                                    
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                           </div>                                            
                                            

                                      
                                  <div>&nbsp;</div>
                                    <div class="form-group" style="border-bottom:1px solid #e5e5e5">                                                       
                                                        <label for="txtReward_1" class="control-label">
                                                            <span class="text-light-blue">Who earns the brandyy points</span> * 
                                                            <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Select who is eligible to get the reward.</br> Every participant (OR) One lucky draw winner"></i>

                                                        </label>
                                           </div>
                                    
                                    <div style="overflow: hidden;" class="portlet-body pan">
                                        <div class="form-body pal">                                            
                                            <div class="form-group">                                                       
                                                        <label for="txtReward_1" class="control-label">
                                                           <asp:RadioButton ID="radio1" runat="server" GroupName="whomtoreward" OnCheckedChanged="radio1_CheckedChanged" AutoPostBack="true" /> Every User 
                                                            <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Every user who successfully participates will be rewarded"></i></label>                                                        
                                            </div>                                                    
                                            <div class="form-group" style="display:none">                                                       
                                                        <label for="txtReward_1" class="control-label">
                                                           <asp:RadioButton ID="radio2" runat="server" GroupName="whomtoreward" OnCheckedChanged="radio2_CheckedChanged" AutoPostBack="true" /> <asp:TextBox ID="txtRewardWhom_2" runat="server"  Columns="10" placeholder="(Nth)" />th User <i class="fa fa-info-circle"></i></label>                                                                                                                
                                            </div>
                                            <div class="form-group">                                                       
                                                        <label for="txtReward_1" class="control-label">
                                                           <asp:RadioButton ID="radio3" runat="server" GroupName="whomtoreward" OnCheckedChanged="radio3_CheckedChanged" AutoPostBack="true" /> 
                                                            <asp:TextBox ID="txtRewardWhom_3_1" runat="server" Columns="1" placeholder="1" onkeypress="return numbersonly(this, event)" /> Lucky draw User(s) <asp:TextBox ID="txtRewardWhom_3_2" runat="server"  Columns="10" placeholder="(Optional)" visible="false" /> 
                                                            <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Lucky draw User. </br>By default all users will be given entry into the lucky draw contest. </br> You can perform lucky draw at any point of the campaign and any number of times."></i>
                                                        </label>
                                            </div>           
                                        </div>                                        
                                    </div>
                               <div>&nbsp;</div>
                                    <div class="form-group" style="border-bottom:1px solid #e5e5e5">                                                       
                                                        <label for="txtReward_1" class="control-label">
                                                            <strong>Budget</strong> * 
                                                            <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Define what should be the max brandyy point that a user can by performing an activity"></i>
                                                        </label>                                                                                                  
                                           </div>
                                    <div style="overflow: hidden;" class="portlet-body pan">
                                        <div class="form-body pal">                                            
                                            <table style="width:100%;">                                            
                                            <tbody id="Tbody1" runat="server"> 
                                                <tr>
                                                     <td  style="width:30%; vertical-align:top;">Overall campaign budget</td>
                                                     <td  style="width:60%; vertical-align:top;">
                                                         <asp:TextBox ID="txtOverallBudget" runat="server" Columns="10"/> BP
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Specify Overall Campaign Budget" ValidationGroup="save" ControlToValidate="txtOverallBudget" InitialValue="" ForeColor="Red" />                                                        
                                                     </td>
                                                </tr>    
                                                <tr>
                                                     <td  style="vertical-align:top;">Max brandyy points per activity</td>
                                                     <td  style="vertical-align:top;">
                                                         <asp:TextBox ID="txtMaxBrandyyPoints" runat="server" Columns="10"/> BP                                                             
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Specify max BP" ValidationGroup="save" ControlToValidate="txtMaxBrandyyPoints" InitialValue="" ForeColor="Red" />                                                        
                                                     </td>
                                                </tr>
                                            </tbody>                    
                                        </table>         
                                        </div>                                        
                                    </div>
                                <div>&nbsp;</div>
                                    <div class="form-group" style="border-bottom:1px solid #e5e5e5">                                                       
                                                        <label for="txtReward_1" class="control-label">
                                                            <strong>Set Brandyy Points breakup for a activity</strong> * 
                                                            <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Define what should be the max brandyy point that a user can by performing an activity"></i>
                                                        </label>                                                                                                  
                                           </div>
                                    <div style="overflow: hidden;" class="portlet-body pan">
                                        <div class="form-body pal">                                            
                                            <div class="form-group">
                                                <table class="table">
                                                        <tr style="background-color:#fafafa">                                
                                                            <asp:Repeater runat="server" ID="repTab_header">
                                                                <ItemTemplate>                                                                    
                                                                    <th id="Th1" runat="server"  visible='<%#Eval("visiblestate") %>'> <span class="text-light-blue"> <%# Eval("header") %></span>                                                                    
                                                                    </th>
                                                                </ItemTemplate>                                                                        
                                                            </asp:Repeater>  
                                                        </tr>    
                                                        <tr>                                                      
                                                            <asp:Repeater runat="server" ID="repTab_content">
                                                                <ItemTemplate>                                                                    
                                                                    <td id="Td1" runat="server"  visible='<%#DataBinder.Eval(Container.DataItem, "col_visiblestate")%>' >
                                                                        <asp:TextBox ID="txtRewards" runat="server" Columns="10"  visible='<%#DataBinder.Eval(Container.DataItem, "visiblestate")%>' Text='<%#DataBinder.Eval(Container.DataItem, "data")%>' />                                                                                                                                        
                                                                    </td>
                                                                </ItemTemplate>                                                                        
                                                           </asp:Repeater>   
                                                        </tr>                          
                                                    </table>
                                            </div>
                                        </div>
                                    </div>
                                <div>&nbsp;</div>
                                    <div class="form-group" style="border-bottom:1px solid #e5e5e5">                                                       
                                                        <label for="txtReward_1" class="control-label">
         <span class="text-light-blue">When To credit the brandyy points?</span> * 
                                                            <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Define when the user reward will be delivered to the user. </br>1. immediately when user particiaptes and is verified by the system </br>2. after the end of campaign </br>3. you can specify the date when the reward needs to be delivered"></i>
                                                        </label>                                                                                                  
                                           </div>     
                                    <div style="overflow: hidden;" class="portlet-body pan">
                                        <div class="form-body pal">                                            
                                            <div class="form-group">                                                       
                                                        <asp:DropDownList ID="drpRewardWhnType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpRewardWhnType_SelectedIndexChanged">
                                                            <asp:ListItem Value="1">Immediately When Action Performed</asp:ListItem>
                                                            <asp:ListItem Value="2">After Campaign End Date</asp:ListItem>
                                                            <asp:ListItem Value="3">After Specified Date</asp:ListItem>
                                                        </asp:DropDownList>                                                
                                            </div>                                                    
                                            <div class="form-group" runat="server" id="divRewardWhenDate" visible="false"> 
                                                <asp:TextBox ID="txtRewardWhenDate" runat="server"></asp:TextBox> 
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Specify date" ValidationGroup="save" ControlToValidate="txtRewardWhenDate" InitialValue="" ForeColor="Red" />
                                                <asp:CustomValidator runat="server" ID="CustomValidator2" ControlToValidate="txtRewardWhenDate" ValidationGroup="save" onservervalidate="valDateRange_ServerValidate" ErrorMessage="enter valid date" ForeColor="Red" />                                                
                                            </div>                                                          
                                        </div>                                        
                                    </div>

                                <div>&nbsp;</div>
                                    <div class="form-group" style="border-bottom:1px solid #e5e5e5">                                                       
                                                        <label for="txtReward_1" class="control-label">
    <span class="text-light-blue">Whom do you want your campaign to target</span> * <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="By default all users can participate in your campaign. You can narrow down the audience by their country, age or gender.</br>Note: This may reduce the potential reach since some users may have not set these details."></i></label>                                                                                                  
                                           </div>                                    
                                    <div style="overflow: hidden;" class="portlet-body pan">
                                        <div class="form-body pal">                                            
                                            <table style="width:100%;">                                            
                                            <tbody id="tblStep2" runat="server">     
                                                <tr>
                                                     <td  style="width:20%; vertical-align:top;">All Users</td>
                                                     <td  style="width:80%; vertical-align:top;"><asp:CheckBox runat="server" ID="chkAllUsers2"/></td>
                                                </tr>
                                                <tr>
                                                     <td  style="vertical-align:top;">Country</td>
                                                     <td  style="vertical-align:top;"><asp:ListBox ID="DrpCountry2" runat="server" Width="250" SelectionMode="Multiple" CssClass="chosen-select" /></asp></td>
                                                </tr>
                                                <tr>
                                                    <td>Age</td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="drp_AgeFrom" OnSelectedIndexChanged="drp_AgeFrom_SelectedIndexChanged" />
                                                        <asp:DropDownList runat="server" ID="drp_AgeTill" OnSelectedIndexChanged="drp_AgeTill_SelectedIndexChanged" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Gender</td>
                                                    <td><asp:DropDownList runat="server" ID="drp_Gender" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:LinkButton ID="lnkCalReach" runat="server" OnClick="lnkCalReach_Click">[Calculate potential reach]</asp:LinkButton>
                                                        <asp:Label ID="lbkCalReach" runat="server"></asp:Label>

                                                    </td>
                                                </tr>                                                                                                                 
                                            </tbody>                    
                                        </table>         
                                        </div>                                        
                                    </div>
                                
                                            <div class="form-actions text-right pal">                                           
                                           <asp:Label ID="lblValidationErrors" runat="server" CssClass="my_status_notification label label-red" Visible="false"></asp:Label>
                                           <asp:Button runat="server" Text="Create Campaign" ValidationGroup="save" ID="btn_Panel2" OnClick="btn_Panel2_Click" CssClass="btn btn-primary" />                                                                                        
                                           <asp:Button runat="server" Text="Update Campaign" ValidationGroup="save" ID="btn_Update" OnClick="btn_Update_Click" CssClass="btn btn-primary" />                                                                                                                                   
                                           <asp:Button runat="server" Text="Manage Actions" ValidationGroup="save" ID="btn_ManageActions" OnClick="btn_ManageActions_Click" CssClass="btn btn-primary" />                                                                                        
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
                                        </div>
                                        </div><!-- /.tab-pane -->                                    
                                </div><!-- /.tab-content -->
                            </div>
                  
                </section><!-- /.content -->
           
      <!--BEGIN CONTENT-->
        
     <!--END CONTENT-->      
     

     <script src="<%=SessionState.WebsiteURLBrand+ "custom-js/jquery-ui.min.js"%>" type="text/javascript"></script>
    <link rel="stylesheet" href="<%=SessionState.WebsiteURLBrand+ "custom-js/jquery-ui.css"%>" />
    
    <script type="text/javascript">

        function ShowOnLoadEvents() {            
            $("[id$=txtStartDate1]").datepicker({
            });
            $("[id$=txtEndDate1]").datepicker({
            });
            $("[id$=txtRewardWhenDate]").datepicker({});

            $("[data-toggle='tooltip'], [data-hover='tooltip']").tooltip({
                content: function (callback) {
                    callback($(this).prop('title').replace('|', '<br />'));
                },
                position: { my: "left top+15 center", at: "right center" }
            });

            //SelectMultipleCountries();
        }
        $(window).load(function () {
            $("[data-toggle='tooltip'], [data-hover='tooltip']").tooltip({
                content: function(callback) { 
                    callback($(this).prop('title').replace('|', '<br />')); 
                },
                position: { my: "left top+15 center", at: "right center" }
            });
        });
    </script>
     <!-- validation notification start -->
     <script type="text/javascript">      
         function fnOnUpdateValidators() {            
             for (var i = 0; i < Page_Validators.length; i++) {
                 var val = Page_Validators[i];
                 var ctrl = document.getElementById(val.controltovalidate);
                 if (ctrl != null && ctrl.style != null) {
                     if (!val.isvalid)
                         ctrl.style.background = '#FFAAAA';
                     else
                         ctrl.style.backgroundColor = '';
                 }
             }
         }
         function HideStatusNotification() {
             
             $('.my_status_notification').fadeOut(5000, function () {
                 $(this).html(""); //reset label after fadeout
             });
             
         }         

     </script>
     <style type="text/css">
    .my_status_notification
    {
        box-shadow: 10px 10px 5px #888888;
    }
         </style>
         <script src='<%=SessionState.WebsiteURLAdmin+ "custom-js/Only-Numbers.js"%>' type="text/javascript"></script>
     <!-- validation notification end -->

     
</asp:Content>