<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/brands/brandsMasterPage.master"  CodeFile="brandcampaigns.aspx.cs" Inherits="brands_brandcampaigns" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                         Campaigns
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>homepage.aspx"> Home </a></li> 
                        <li class="active">Campaigns</li>                         
                    </ol>
                </section> 
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>                        
                        <small>All Active And Inactive Campaigns</small>
                    </h1>
                    <ol class="helptext breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>help.aspx?#3_1"><i class="fa fa-fw fa-question-circle"></i> Help: How can I create a campaign</a></li>                        
                    </ol>
                </section> 
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>                
<!-- Main content -->
                <section class="content">
                    <div class="box box-primary"><!-- active campaigns -->
                                <div class="box-body table-responsive no-padding">                                    
                                <table class="table table-hover">
                                    <thead>
                                    <tr>
                                          <th>Active Campaigns</th>                                          
                                          <th>Schedule</th>
                                          <th>Overall Budget</th>     
                                          <th>Reward Whom</th>                                                                                    
                                          <th>Verification <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="right" title="This column specifies if the &#013; campaign has been verified by the brandyy admin. &#013; Once you complete creating a campaign&#013;click 'Not Sent' to send for verification"></i></th>
                                          <th>Status <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="right" title="This column specifies if a campaign is active/inactive&#013;Click on the switch to change the campaign status.&#013;Only a verified campaign status can be modified."></i></th>
                                          <th>&nbsp;</th>
                                          <th>&nbsp;</th>
                                      </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepTab" runat="server">
                                        <ItemTemplate>
                                          <tr>
                                              <td>
                                                  <%#Eval("campaign_name") %>
                                                  <p class="label label-orange " style="display:<%#( Convert.ToByte( Eval("campaign_status") ) == 1 ) ? "none" : "" %>"><%#Eval("log_text") %></p>

                                              </td>                                              
                                              <td>
                                                  <p><%#Eval("campaign_execution_period") %></p>                                                                                                    
                                              </td>
                                              <td>
                                                  <p><%#Convert.ToInt64(Eval("overall_budget"))%> BP</p>                                                                                                    
                                              </td>
                                              <td>                                                  
                                                  <p>To <%#Eval("reward_whom_val") %></p>                                                                                                    
                                              </td>                                              
                                              <td><asp:Button runat="server" ID="btn_Verification"  role="button" CssClass='<%#( Convert.ToInt16( Eval("verification_status") ) >=0 )? "verification_status_" + Convert.ToString(Eval("verification_status")) +" btn" : "btn btn-default" %>' Text='<%#Eval("verification_name")%>' OnClick="btn_Verification_Click" CommandArgument='<%#Eval("campaign_id")+","+ Eval("campaign_status") + "," + Eval("verification_status") %>' /></td>
                                              <td>
                                                  <!-- Round Switch -->
	                                            <asp:LinkButton  runat="server"  ID="btn_Status"  OnClick="btn_Status_Click" CommandArgument='<%#Eval("campaign_id")+","+ Eval("campaign_status") + "," + Eval("verification_status") %>' ToolTip='<%#( Convert.ToByte( Eval("campaign_status") ) == 1 ) ? "Active" : "Inactive" %>'>
                                                    <div runat="server"  class='<%#( Convert.ToByte( Eval("campaign_status") ) == 1 ) ? "Switch Round  On" : "Switch Round Off" %>'>		
                                                    <div class="Toggle"></div>            
    	                                            </div>	
	                                            </asp:LinkButton>                                                  
                                                  </td>                                                 
                                              <td><asp:LinkButton ID="btn_Edit" runat="server" role="button" class="btn btn-default" OnClick="btn_Edit_Click" CommandArgument='<%#Eval("campaign_id") + "," + Eval("campaign_status")%>' >Edit</asp:LinkButton></td>
                                              <td><asp:LinkButton ID="btn_View" runat="server" role="button" class="btn btn-default" OnClick="btn_View_Click" CommandArgument='<%#Eval("campaign_id")%>' >View</asp:LinkButton></td>
                                          </tr>
                                          </ItemTemplate>
                                        </asp:Repeater>
                                        <tr><td colspan="8" style="text-align:center"><asp:Label runat="server" ID="lblNoActiveCampaigns" Visible="false">
                                            &nbsp;No Active Campaigns To List. 
                                            <br /><a href="<%=SessionState.WebsiteURLBrand %>brand-create-campaign.aspx">Click here</a> to start creating new campaigns.
                                            <br />OR
                                            <br />Activate already created campaigns.

                                                                                      </asp:Label></td></tr>
                                    </tbody>
                                </table>                                  
                                
                            </div><!-- /.box-body -->
                            </div> 
                    <div class="box box-primary"><!-- inactive campaigns -->
                                <div class="box-body table-responsive no-padding">                                    
                                <table class="table table-hover">
                                    <thead>
                                    <tr>
                                          <th>InActive Campaigns</th>                                          
                                          <th>Schedule</th>
                                          <th>Overall Budget</th>     
                                          <th>Reward Whom</th>                                                                                    
                                          <th>Verification <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="right" title="This column specifies if the &#13; campaign has been verified by the brandyy admin. &#013; Once you complete creating a campaign&#013;click 'Not Sent' to send for verification"></i></th>
                                          <th>Status <i class="fa fa-info-circle" data-toggle="tooltip" data-placement="right" title="This column specifies if a campaign is active/inactive  &#13; Click on the switch to change the campaign status.&#013;Only a verified campaign status can be modified."></i></th>
                                          <th>&nbsp;</th>
                                          <th>&nbsp;</th>
                                      </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepTabInactive" runat="server">
                                        <ItemTemplate>
                                          <tr>
                                              <td>
                                                  <%#Eval("campaign_name") %>
                                                  <p class="label label-orange " style="display:<%#( Convert.ToByte( Eval("campaign_status") ) == 1 ) ? "none" : "" %>"><%#Eval("log_text") %></p>

                                              </td>                                              
                                              <td>
                                                  <p><%#Eval("campaign_execution_period") %></p>                                                                                                    
                                              </td>
                                              <td>
                                                  <p><%#Convert.ToInt64(Eval("overall_budget"))%> BP</p>                                                                                                    
                                              </td>
                                              <td>                                                  
                                                  <p>To <%#Eval("reward_whom_val") %></p>                                                                                                    
                                              </td>                                              
                                              <td><asp:Button runat="server" ID="btn_Verification"  role="button" CssClass='<%#( Convert.ToInt16( Eval("verification_status") ) >=0 )? "verification_status_" + Convert.ToString(Eval("verification_status")) +" btn" : "btn btn-default" %>' Text='<%#Eval("verification_name")%>' OnClick="btn_Verification_Click" CommandArgument='<%#Eval("campaign_id")+","+ Eval("campaign_status") + "," + Eval("verification_status") %>' /></td>
                                              <td>
                                                  <!-- Round Switch -->
	                                            <asp:LinkButton  runat="server"  ID="btn_Status"  OnClick="btn_Status_Click" CommandArgument='<%#Eval("campaign_id")+","+ Eval("campaign_status") + "," + Eval("verification_status") %>' ToolTip='<%#( Convert.ToByte( Eval("campaign_status") ) == 1 ) ? "Active" : "Inactive" %>'>
                                                    <div id="Div1" runat="server"  class='<%#( Convert.ToByte( Eval("campaign_status") ) == 1 ) ? "Switch Round  On" : "Switch Round Off" %>'>		
                                                    <div class="Toggle"></div>            
    	                                            </div>	
	                                            </asp:LinkButton>                                                  
                                                  </td>                                                 
                                              <td><asp:LinkButton ID="btn_Edit" runat="server" role="button" class="btn btn-default" OnClick="btn_Edit_Click" CommandArgument='<%#Eval("campaign_id") + "," + Eval("campaign_status")%>' >Edit</asp:LinkButton></td>
                                              <td><asp:LinkButton ID="btn_View" runat="server" role="button" class="btn btn-default" OnClick="btn_View_Click" CommandArgument='<%#Eval("campaign_id")%>' >View</asp:LinkButton></td>
                                          </tr>
                                          </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                  
                                <asp:Label runat="server" ID="lblNoInActiveCampaigns" Visible="false">No InActive Campaigns. </asp:Label>
                            </div><!-- /.box-body -->
                            </div>                    
                </section><!-- /.content -->
             

                <script src="<%=SessionState.WebsiteURLBrand+ "jquery-ui-1.11.2.custom/jquery-ui.min.js"%>" type="text/javascript"></script>
    <link rel="stylesheet" href="<%=SessionState.WebsiteURLBrand+ "jquery-ui-1.11.2.custom/jquery-ui.css"%>" />
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
</style>

<style type="text/css">
/*------------------------------------------------*/
/* Switch SECTION START*/
/*------------------------------------------------*/
.Switch {
position: relative;
display: inline-block;
font-size: 1.6em;
font-weight: bold;
color: #ccc;
text-shadow: 0px 1px 1px rgba(255,255,255,0.8);
height: 18px;
padding: 6px 6px 5px 6px;
border: 1px solid #ccc;
border: 1px solid rgba(0,0,0,0.2);
border-radius: 4px;
background: #ececec;
box-shadow: 0px 0px 4px rgba(0,0,0,0.1), inset 0px 1px 3px 0px rgba(0,0,0,0.1);
cursor: pointer;
}

body.IE7 .Switch { width: 78px; }

.Switch span { display: inline-block; width: 35px; }
.Switch span.On { color: #33d2da; }

.Switch .Toggle {
position: absolute;
top: 1px;
width: 37px;
height: 25px;
border: 1px solid #ccc;
border: 1px solid rgba(0,0,0,0.3);
border-radius: 4px;
background: #fff;
background: -moz-linear-gradient(top,  #ececec 0%, #ffffff 100%);
background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#ececec), color-stop(100%,#ffffff));
background: -webkit-linear-gradient(top,  #ececec 0%,#ffffff 100%);
background: -o-linear-gradient(top,  #ececec 0%,#ffffff 100%);
background: -ms-linear-gradient(top,  #ececec 0%,#ffffff 100%);
background: linear-gradient(top,  #ececec 0%,#ffffff 100%);

box-shadow: inset 0px 1px 0px 0px rgba(255,255,255,0.5);
z-index: 999;

-webkit-transition: all 0.15s ease-in-out;
-moz-transition: all 0.15s ease-in-out;
-o-transition: all 0.15s ease-in-out;
-ms-transition: all 0.15s ease-in-out;
}

.Switch.On .Toggle { left: 2%; }
.Switch.Off .Toggle { left: 54%; }


/* Round Switch */
.Switch.Round {
padding: 0px 20px;
border-radius: 40px;
}

body.IE7 .Switch.Round { width: 1px; }

.Switch.Round .Toggle {
border-radius: 40px;
width: 14px;
height: 14px;
}

.Switch.Round.On .Toggle { left: 3%; background: #33d2da; }
.Switch.Round.Off .Toggle { left: 58%; }
</style>
<script type="text/javascript">
    $(window).load(function () {
        $("[data-toggle='tooltip'], [data-hover='tooltip']").tooltip({
            content: function (callback) {
                callback($(this).prop('title').replace('|', '&#013;'));
            }
        });
    });
</script>      
     
        </ContentTemplate>     
       </asp:UpdatePanel>
</asp:Content>
              