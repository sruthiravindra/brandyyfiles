<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/brands/brandsMasterPage.master" CodeFile="activity-verification-list.aspx.cs" Inherits="brands_activity_verification_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                        Activities Awaiting Verification                        
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>homepage.aspx"> Home </a></li> 
                        <li><a href="<%=SessionState.WebsiteURLBrand %>activity-verification-overview.aspx"> Activity Verification Overview</a></li> 
                        <li class="active">Activities Listing</li>                        
                    </ol>
                </section> 
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>                        
                        <small><strong>Selected Campaign - </strong><%=SessionState._Campaign.campaign_name %></small>
                    </h1>
                    <ol class="helptext breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>help.aspx?#3_1"><i class="fa fa-fw fa-question-circle"></i> Help: Where can we get the list of pending activities</a></li>                        
                    </ol>
                </section> 
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>                
<!-- Main content -->
                <section class="content">
                    <div style="margin-bottom:10px;text-align:right">
                        <asp:LinkButton runat="server" ID="btnStartVerification" OnClick="btnStartVerification_Click" CssClass="btn btn-primary">Start Verification Process</asp:LinkButton>                       
                    </div>                    
                    <div class="box  box-primary">
                                <div class="box-body table-responsive no-padding">                                    
                                <table class="table table-hover">
                                    <thead>
                                    <tr>     
                                          <th>#</th>                                     
                                          <th>User Activites</th>                                          
                                          <th>Score</th>                                          
                                          <th>Reward</th>                                          
                                          <th>&nbsp;</th>                                                                                    
                                      </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepTab" runat="server">
                                        <ItemTemplate>
                                                                  <tr>
                                                                      <td><%=++Cnt %></td>
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
                                                                          <small class="label label-danger" style="display:<%#( Convert.ToString( Eval("verification_log") ).Trim() == "" ) ? "none" : "" %>"> <%#Eval("verification_log") %></small><br />                                                                          
                                                                      </td>
                                                                      <td>
                                                                          <small class="label label-default"> <%#Convert.ToInt64(Eval("reward_amount")) %> BP</small><br />
                                                                          <small class="label label-<%#( (Convert.ToByte( Eval("reward_status") )== 1) ? "success" : ( (Convert.ToByte( Eval("reward_status") )== 3) ? "danger" : "" ) ) %>"> <%#Eval("reward_status_str") %></small>                                                                          
                                                                      </td> 
                                                                      <td>
                                                                          <asp:LinkButton runat="server" ID="btnVerifyActivity" OnClick="btnVerifyActivity_Click" CommandArgument='<%#Eval("activity_id")%>' CssClass="btn btn-primary">Verify </asp:LinkButton>                                                                          
                                                                      </td>                                                                     
                                                                  </tr>
                                                                  </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                  
                                <asp:Label runat="server" ID="lblNoCampaigns" Visible="false">&nbsp;No Activities Pending For Verification.</asp:Label>
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
<script type="text/javascript">
    $(window).load(function () {
        $("[data-toggle='tooltip'], [data-hover='tooltip']").tooltip({
            content: function (callback) {
                callback($(this).prop('title').replace('|', '<br />'));
            }
        });

        // Switch toggle
        $('.Switch').click(function () {
            $(this).toggleClass('On').toggleClass('Off');
        });
    });
</script>

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
      
     
        </ContentTemplate>     
       </asp:UpdatePanel>
</asp:Content>
              
