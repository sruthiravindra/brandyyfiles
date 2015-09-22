<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/brands/brandsMasterPage.master" CodeFile="campaignview.aspx.cs" Inherits="brands_campaignview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                        Campaign Summary
                        <small>View summary on campaign settings, activities and status</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li class="active"><a href="<%=SessionState.WebsiteURLBrand %>homepage.aspx"> Home </a></li> 
                        <li class="active"><a href="<%=SessionState.WebsiteURLBrand %>brandcampaigns.aspx"> All Campaigns </a></li> 
                        <li class="active"><a href="#">Campaign Summary</a></li>                        
                    </ol>
                </section>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!--BEGIN CONTENT-->
                <section class="content">
                    <div class="box box-primary">
                                <div class="panel">
                                    <div class="panel-body">
                                        <div class="profile">
                                            <div style="margin-bottom: 15px" class="row">
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <ItemTemplate>                                                        
                                                <div class="col-xs-12 col-sm-12">                                                                                                        
                                                    <h2>
                                                        <span class="label label-danger" style="<%#(Convert.ToByte(Eval("campaign_status")) == 1 ? "display:none" : "font-size:16px;")%>">                                                            
                                                            Not Active
                                                        </span>
                                                        <%#Eval("campaign_name") %>
                                                        <span style="<%#(Convert.ToByte(Eval("campaign_status")) == 1 ? "display:none" : "")%>">                                                            
                                                            <asp:LinkButton runat="server" ID="btnSendVerification" OnClick="btnSendVerification_Click" CssClass="btn btn-primary">Click here to send for verification</asp:LinkButton>
                                                        </span>
                                                    </h2> 
                                                    <table class="table" style="width:100%;">
                                                        <tr>
                                                            <td style="width:20%"><strong>  <span>General Details</span></strong></td>
                                                            <td style="width:40%"><strong>  <span>Objective  </span></strong></td>
                                                            <td style="width:40%"><span style="color:#999"><%#Eval("campaign_objective_text") %></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td><strong>  <span>Campaign Schedule  </span></strong></td>
                                                            <td><span style="color:#999"><%#Eval("campaign_execution_period") %></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td><strong>  <span>Reward Details</span></strong></td>
                                                            <td><strong>  <span>Reward Whom  </span></strong></td>
                                                            <td><span style="font-size:14px;padding:2px" class="bg-green"><%#Eval("reward_whom_val")%></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td><strong>  <span>Overall Campaign Budget  </span></strong></td>
                                                            <td><span style="font-size:14px;padding:2px" class="bg-green"><%# Convert.ToInt64(Eval("overall_budget"))%> BP</span></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td><strong>  <span>Max branddy points per activity  </span></strong></td>
                                                            <td><span style="font-size:14px;padding:2px" class="bg-green"><%# Convert.ToInt64(Eval("max_brandyy_points"))%> BP</span></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td><strong>  <span>When To pay the reward  &nbsp;<i class="fa fa-info-circle" data-toggle="tooltip" data-placement="right" title="Possible values are - Immediately, After Campaign End Date, On Specified Date"></i></span></strong></td>
                                                            <td><span style="color:#999"><%#Eval("reward_when_str") %></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td><strong>  <span>Do you want the reward details to be displayed to user  &nbsp;<i class="fa fa-info-circle" data-toggle="tooltip" data-placement="right" title="If this value is set to 'Yes', the brandyy point calculation for actions will be displayed on the 'Offer' page to the participant"></i></span></strong></td>
                                                            <td><span style="color:#999"><%#Eval("display_reward_to_user_str") %></span></td>
                                                        </tr>                                                       
                                                        <tr>
                                                            <td><strong>  <span>Target Details &nbsp;<i class="fa fa-info-circle" data-toggle="tooltip" data-placement="right" title="User who can participate in this campaign. Only qualifying users will be rewarded"></i></span></strong></td>
                                                            <td><strong>  <span>Target  </span></strong></td>
                                                            <td><span style="color:#999"><%#Eval("target_all_users") %></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td><strong>  <span>Target Country </span></strong></td>
                                                            <td><span style="color:#999"><%#Eval("target_countries") %></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td><strong>  <span>Target Age </span></strong></td>
                                                            <td><span style="color:#999"><%#Eval("target_age") %></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td><strong>  <span>Target Gender </span></strong></td>
                                                            <td><span style="color:#999"><%#Eval("target_gender") %></span></td>
                                                        </tr>
                                                         <tr>
                                                            <td><strong>Action Details</strong></td>
                                                            <td><strong>  <span>Do you want user to perform all actions  </span></strong></td>
                                                            <td><span style="color:#999"><%#Eval("display_all_actions_str") %></span></td>
                                                        </tr>
                                                    </table> 
                                                    <span style="display:none">
                                                    <p>
                                                        <strong>  <span>Objective : </span></strong> <span style="color:#999"><%#Eval("campaign_objective_text") %></span></p>                                                                                                  
                                                    <p>
                                                        <strong>  <span">Campaign Schedule : </span></strong> <span style="color:#999"><%#Eval("campaign_execution_period") %></span></p>   
                                                    <p>
                                                         <strong>  <span>Reward Whom : </span> </strong><span style="font-size:14px;padding:2px" class="bg-green"><%#Eval("reward_whom_val")%></span></p> 
                                                    <p>
                                                         <strong>  <span>Overall Campaign Budget : </span></strong> <span style="font-size:14px;padding:2px" class="bg-green"><%# Convert.ToInt64(Eval("overall_budget"))%> BP</span></></p>                                                                                                        
                                                    <p>
                                                         <strong>  <span>Max branddy points per activity : </span></strong> <span style="font-size:14px;padding:2px" class="bg-green"><%# Convert.ToInt64(Eval("max_brandyy_points"))%> BP</span></></p>                                                                                                        
                                                    <p>
                                                        <strong>  <span>When To pay the reward : </span></strong> <span style="color:#999"><%#Eval("reward_when_str")%></span></p>                                                                                                       
                                                    <p>
                                                        <strong>  <span>Do you want the reward details to be displayed to user : </span></strong><span style="color:#999"> <%#Eval("display_reward_to_user_str")%></span></p> 
                                                     <p>
                                                        <strong>  <span">Do you want user to perform all actions : </span></strong> <span style="color:#999"><%#Eval("display_all_actions_str")%></span></p> 
                                                    <p>
                                                        <strong>  <span>Target : </span> </strong> <span style="color:#999"><%#Eval("target_all_users")%></span></p>                                                     
                                                    <p>
                                                        <strong>  <span">Target Country : </span></strong><span style="color:#999"> <%#Eval("target_countries").ToString().Replace("&lt;","<").Replace("&gt;",">")%></span></p>
                                                    <p>
                                                        <strong>  <span>Target Age : </span></strong> <span style="color:#999"><%#Eval("target_age")%></span></p>
                                                    <p>
                                                        <strong>  <span>Target Gender : </span></strong> <span style="color:#999"><%#Eval("target_gender")%></span></p>    
                                                    </span>
                                                                                                    
                                                </div>                                                
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                <div>&nbsp;</div>
                                                <div class="col-xs-12 col-sm-12">
                                                <table class="table">
                                                        <tr>                                                           
                                                            <th>   <span><strong>Actions To Perform</strong></span></th>
                                                            
                                                            <asp:Repeater runat="server" ID="repTab_header">
                                                                <ItemTemplate>
                                                                    <th id="Th1" runat="server" style="text-align:center"   visible='<%#Eval("visiblestate") %>'>
                                                                      <span><strong><%# Eval("header") %></strong></span>  
                                                                    </th>
                                                                </ItemTemplate>                                                                        
                                                            </asp:Repeater>  
                                                        </tr>
                                                    <asp:Repeater runat="server" ID="repTab_ActionNames" OnItemDataBound="repTab_ActionNames_ItemDataBound">
                                                            <ItemTemplate>
                                                        <tr>                                                            
                                                            <td>
                                                                <img src='<%=SessionState.WebsiteURLAdmin+ "images/campaign_types/campaign_type_"%><%#DataBinder.Eval(Container.DataItem, "id")%>.png' />
                                                                <div runat="server" id="action_details"></div>   
                                                            </td>                                                            
                                                            <asp:Repeater runat="server" ID="repTab_content">
                                                                <ItemTemplate>                                                                    
                                                                    <td id="Td1" runat="server" style="text-align:center"  visible='<%#DataBinder.Eval(Container.DataItem, "visiblestate")%>' >
                                                                        <%#DataBinder.Eval(Container.DataItem, "data")%> BP
                                                                    </td>
                                                                </ItemTemplate>                                                                        
                                                           </asp:Repeater>                                                             
                                                            
                                                        </tr>             
                                                            </ItemTemplate>
                                                        </asp:Repeater>     
                                                </table>  
                                                </div>                                              
                                            </div>
                                            <asp:Repeater runat="server" ID="repTab_Cnts">
                                                <ItemTemplate>
                                                    <div class="row text-center divider" style="display:<%#(SessionState._Campaign.reward_type != 3)?"":"none" %>">                                                
                                            </div>
                                                    <div class="row text-center divider">
                                                <div class="col-xs-12 col-sm-3 emphasis">
                                                    <h2>
                                                        <strong><%#Eval("total_activity_pending") %></strong></h2>
                                                    <p>
                                                        <small>Pending Activities</small>
                                                    </p>                                                    
                                                </div>
                                                <div class="col-xs-12 col-sm-3 emphasis">
                                                    <h2>
                                                        <strong><%#Eval("total_activity_rejected") %></strong></h2>
                                                    <p>
                                                        <small>Rejected Activities</small>
                                                    </p>                                                    
                                                </div>
                                                <div class="col-xs-12 col-sm-3 emphasis">
                                                    <h2>
                                                        <strong><%#Eval("total_activity_approved") %> | <%#Eval("total_activity_rewarded") %></strong></h2>
                                                    <p>
                                                        <small>Approved | Rewarded Activities</small>
                                                    </p>                                                    
                                                </div>                                                
                                                <div class="col-xs-12 col-sm-3 emphasis bg-blue">
                                                    <h2>
                                                        <strong><%#Eval("total_activity") %></strong></h2>
                                                    <p>
                                                        <small>Total Activities</small>
                                                    </p>                                                    
                                                </div>
                                            </div>                                            
                                                </ItemTemplate>
                                            </asp:Repeater>
                                                                                        
                                            </div>
                                            <div style="margin-bottom: 15px" class="row">                                                
                                                <div class="panel panel-blue">                                                    
                                                    <div class="panel-heading"> <h2> <span>Campaign Activities</span></h2></div>
                                                    <div class="panel-body" style="padding-top:0px;">
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                        <table class="table table-hover">
                                                            <thead>     
                                                            <tr>
                                                                  <th>Performed By</th>
                                                                  <th><asp:DropDownList runat="server" ID="drpFilterVerified" OnSelectedIndexChanged="drpFilterVerified_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="10">Activity Score</asp:ListItem>
                                                                        <asp:ListItem Value="1">Verified</asp:ListItem>
                                                                        <asp:ListItem Value="0">Pending</asp:ListItem>
                                                                    </asp:DropDownList></th>                                                                  
                                                                  <th><asp:DropDownList runat="server" ID="drpFilterReward" OnSelectedIndexChanged="drpFilterReward_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="10">Reward</asp:ListItem>
                                                                        <asp:ListItem Value="0">Pending</asp:ListItem>
                                                                        <asp:ListItem Value="1">Rewarded</asp:ListItem>
                                                                        <asp:ListItem Value="2">Approved</asp:ListItem>
                                                                        <asp:ListItem Value="3">Rejected</asp:ListItem>
                                                                    </asp:DropDownList></th>
                                                              </tr>
                                                            </thead>
                                                            <tbody>                                                                
                                                                <asp:Repeater ID="RepTab" runat="server">
                                                                <ItemTemplate>
                                                                  <tr>
                                                                      <td>
                                                                          <div class="box <%#( Convert.ToByte( Eval("verification_status") ) == 3 ) ? "box-danger" : ( Convert.ToByte( Eval("verification_status") ) == 2 ) ? "box-success" : "box-info" %>">
                                    <div class="box-body chat" id="chat-box" style="overflow: hidden; width: auto;">
                            <!-- chat item -->
                                    <div class="item">
                                        <img src="<%#Eval("profile_image_url") %>" alt="user image" class="online">
                                        <p class="message">
                                            <asp:LinkButton runat="server" CssClass="name" ID="lnk_User" OnClick="lnk_User_Click" CommandArgument='<%#Eval("activity_id")%>'>
                                                <small class="text-muted pull-right"><i class="fa fa-clock-o"></i> <%#Eval("created_on") %></small>
                                                <%#Eval("name") %>
                                            </asp:LinkButton>        
                                            <%#Eval("campaign_type_desc") %>
                                            <br /><%# Convert.ToString(Eval("reward_status")) == "1" ? "Rewarded on : "+Eval("reward_date") : "" %>
                                        </p>                                         
                                        
                                    </div><!-- /.item -->                               
                                </div>
                            </div>
                                                                      </td>                                                                      
                                                                      <td>
                                                                          <small class="label label-default"> <%#Eval("verification_score") %> / 10</small><br />
                                                                          <small class="label label-<%#(Convert.ToByte( Eval("verification_status") )== 1) ? "info" : "danger" %>"> <%#Eval("verification_status_str")%></small><br />
                                                                          <small class="label label-danger" style="display:<%#( Convert.ToString( Eval("verification_log") ).Trim() == "" ) ? "none" : "" %>"> <%#Eval("verification_log") %></small><br />                                                                          
                                                                      </td>
                                                                      <td>
                                                                          <small class="label label-default"> <%#Convert.ToInt64(Eval("reward_amount")) %> BP</small><br />
                                                                          <small class="label label-<%#( (Convert.ToByte( Eval("reward_status") )== 1) ? "success" : ( (Convert.ToByte( Eval("reward_status") )== 3) ? "danger" : "" ) ) %>"> <%#Eval("reward_status_str") %></small>                                                                          
                                                                      </td>                                                                      
                                                                  </tr>
                                                                  </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>                    
                </section><!-- /.content -->
                <!--END CONTENT-->        
      <script src="<%=SessionState.WebsiteURLAdmin+ "custom-js/activity-listing.js"%>"></script>
      <link rel="stylesheet" href="<%=SessionState.WebsiteURLBrand+ "css/campaign_listing.css"%>">  

<div id="fb-root"></div>
<script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.0";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>

<style type="text/css">
    .verification_status_0 {
        background-color:#dc6767;color:#fff
    }
    .verification_status_1 {
        background-color:#f2994b;color:#fff
    }
    .verification_status_2 {
        background-color:#5cb85c;color:#fff        
    }
    .verification_status_3 {
        background-color:#d9534f;color:#fff
    } 
    .reward_status_ {
        color:#fff
    }   
    .reward_status_0 {
        color:#fff
    } 
    .reward_status_1 {
        background-color:#5cb85c;color:#fff 
    }
    .reward_status_2 {
        background-color:#f2994b;color:#fff         
    }
    .reward_status_3 {
       
        background-color:#dc6767;color:#fff
    }
</style>                                    

<!-- FlexSlider -->
	<link rel="stylesheet" href="<%=SessionState.WebsiteURLBrand+ "styles/flexslider.css"%>" type="text/css" media="screen" />
  <script defer src="<%=SessionState.WebsiteURLBrand+ "js/jquery.flexslider.js"%>"></script>
<script type="text/javascript">
    $(window).load(function () {
        $('.flexslider').flexslider({
            animation: "slide",
            animationLoop: false,
            itemWidth: 210,
            itemMargin: 5
        });
    });

   

</script>
 <!-- FlexSlider End -->

<!-- Tooltip start -->
<script src="<%=SessionState.WebsiteURLBrand+ "jquery-ui-1.11.2.custom/jquery-ui.min.js"%>" type="text/javascript"></script>
<script type="text/javascript">
    $(window).load(function () {
        $("[data-toggle='tooltip'], [data-hover='tooltip']").tooltip({
            content: function (callback) {
                callback($(this).prop('title').replace('|', '<br />'));
            },
            position: { my: "left top+15 center", at: "right center" }
        });
    });

</script>    
<!-- Tooltip end -->
      </ContentTemplate>
     
       </asp:UpdatePanel>
</asp:Content>
