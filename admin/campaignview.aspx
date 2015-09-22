<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/admin/adminMasterPage.master" CodeFile="campaignview.aspx.cs" Inherits="brands_campaignview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
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
                                                        <span class="label label-danger" style="<%#(Convert.ToByte(Eval("campaign_status")) == 1 ? "display:none" : "")%>">Not Active</span>
                                                        <%#Eval("campaign_name") %></h2>                                                    
                                                    <p>
                                                        <strong>Objective:</strong> <%#Eval("campaign_objective_text") %></p>
                                                    <%--<p>
                                                        <strong>Campaign Type:</strong> <img src="<%=SessionState.WebsiteURLAdmin+ "images/campaign_type_"+SessionState._Campaign.campaign_type+".png"%>" /></p>  
                                                    <p>
                                                        <div class="fb-post" data-href="<%=SessionState._Campaign.val4 %>" data-width="500"></div>
                                                    </p> --%>                                                 
                                                    <p>
                                                        <strong>Campaign Schedule:</strong> <%#Eval("campaign_execution_period") %></p>   
                                                    <p>
                                                        Reward Whom: <strong><%#Eval("reward_whom_val")%></strong></p>                                                                                                        
                                                    <p>
                                                        <strong>When To pay the reward:</strong> <%#Eval("reward_when_str")%></p>                                                                                                       
                                                    <p>
                                                        <strong>Do you want the reward details to be displayed to user:</strong> <%#Eval("display_reward_to_user_str")%></p> 
                                                     <p>
                                                        <strong>Do you want user to perform all actions:</strong> <%#Eval("display_all_actions_str")%></p> 
                                                    <p>
                                                        <strong>Target: </strong> <%#Eval("target_all_users")%></p>                                                     
                                                    <p>
                                                        <strong>Target Country: </strong> <%#Eval("target_countries").ToString().Replace("&lt;","<").Replace("&gt;",">")%></p>
                                                    <p>
                                                        <strong>Target Age:</strong> <%#Eval("target_age")%></p>
                                                    <p>
                                                        <strong>Target Gender:</strong> <%#Eval("target_gender")%></p>                                                    
                                                </div>                                                
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                <table class="table">
                                                        <tr>                                                           
                                                            <th>Actions To Perform</th>
                                                            
                                                            <asp:Repeater runat="server" ID="repTab_header">
                                                                <ItemTemplate>
                                                                    <th id="Th1" runat="server"  visible='<%#Eval("visiblestate") %>'>
                                                                     <%# Eval("header") %>
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
                                                                    <td id="Td1" runat="server"  visible='<%#DataBinder.Eval(Container.DataItem, "visiblestate")%>' >
                                                                        <%#DataBinder.Eval(Container.DataItem, "data")%>
                                                                    </td>
                                                                </ItemTemplate>                                                                        
                                                           </asp:Repeater>                                                             
                                                            
                                                        </tr>             
                                                            </ItemTemplate>
                                                        </asp:Repeater>     
                                                </table>                                                
                                            </div>
                                            <asp:Repeater runat="server" ID="repTab_Cnts">
                                                <ItemTemplate>
                                                    <div class="row text-center divider" style="display:<%#(SessionState._Campaign.reward_type != 3)?"":"none" %>">
                                                <%--<div class="col-xs-12 col-sm-4 emphasis">
                                                    <h2>
                                                        <strong><%#Eval("total_reward_pending") %> <%#(SessionState._Campaign.reward_type ==1)?"USD":"%" %></strong></h2>
                                                    <p>
                                                        <small>Rewards Pending Verification</small>
                                                    </p>                                                    
                                                </div>
                                                <div class="col-xs-12 col-sm-4 emphasis">
                                                    <h2>
                                                        <strong class="bg-red"> <%#Eval("total_reward_approved") %> <%#(SessionState._Campaign.reward_type ==1)?"USD":"%" %> </strong></h2>
                                                    <p>
                                                        <small>Approved Rewards</small>
                                                    </p>                                                    
                                                </div>
                                                <div class="col-xs-12 col-sm-4 emphasis">
                                                    <h2>
                                                        <strong> <%#Eval("total_reward_rewarded") %> <%#(SessionState._Campaign.reward_type ==1)?"USD":"%" %> </strong></h2>
                                                    <p>
                                                        <small>Rewarded</small>
                                                    </p>                                                    
                                                </div>--%>
                                                <%--<div class="col-xs-12 col-sm-4 emphasis" id="divDailyBudget" runat="server">
                                                    <h2>
                                                        <strong><%=SessionState._Campaign.daily_budget.ToString("0.00")%> USD</strong></h2>
                                                    <p>
                                                        <small>Daily Budget</small>
                                                    </p>                                                    
                                                </div>
                                                <div class="col-xs-12 col-sm-4 emphasis" id="divOverallBudget" runat="server">
                                                    <h2>
                                                        <strong><%=SessionState._Campaign.overall_budget.ToString("0.00")%> USD</strong></h2>
                                                    <p>
                                                        <small>Overall Budget</small>
                                                    </p>                                                    
                                                </div>--%>
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
                                                <p runat="server" ID="lnkPickLuckydraw" Visible="false" >&nbsp;<asp:LinkButton runat="server" OnClick="lnkPickLuckydraw_Click" role="button" class="btn btn-default">Pick A Lucky Draw Winner</asp:LinkButton>
                                                    <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="top" title="Clicking on this will set a user as winner for the lucky draw.</br> Multiple users can be set as winner by clicking multiple times.</br> Please note: This operation is irreversible.</br> A Final lucky draw SHOULD be performed after the campaign ends so that every participant is given a chance"></i>
                                                    <div id="divLuckyDrawWinner" runat="server" class="row" style="margin-left:20px;" visible="false">                                                        
                                                    </div>
                                                </p>
                                                <div class="panel panel-blue">                                                    
                                                    <div class="panel-heading">Campaign Activities</div>
                                                    <div class="panel-body">
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
                                        </p>                                         
                                        
                                    </div><!-- /.item -->                                    
                                </div>
                            </div>
                                                                      </td>                                                                      
                                                                      <td>
                                                                          <small class="label label-danger"> <%#Eval("verification_score") %> / 10</small><br />
                                                                          <small class="label label-danger"> <%#Eval("verification_status_str")%></small><br />
                                                                          <small class="label label-danger" style="display:<%#( Convert.ToString( Eval("verification_log") ).Trim() == "" ) ? "none" : "" %>"> <%#Eval("verification_log") %></small><br />                                                                          
                                                                      </td>
                                                                      <td>
                                                                          <%#Eval("reward_amount") %> BP
                                                                      </td>
                                                                      <td>
                                                                          <span class='reward_status_<%#Eval("reward_status") %>' ><%#Eval("reward_status_str") %></span>                                                                         
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
