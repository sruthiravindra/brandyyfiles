<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/brands/brandsMasterPage.master" CodeFile="activity-verification-overview.aspx.cs" Inherits="brands_activity_verification_overview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                        User Activity Verification Overview                        
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>homepage.aspx"> Home </a></li>                         
                        <li class="active">User Activity Verification Overview</li>                        
                    </ol>
                </section> 
       <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>                        
                        <small>Campaign wise overview of activities pending for verification</small>
                    </h1>
                    <ol class="helptext breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>help.aspx?#3_1"><i class="fa fa-fw fa-question-circle"></i> Help: Where can we get the list of pending activities</a></li>                        
                    </ol>
                </section> 
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>                
<!-- Main content -->
                <section class="content">                                     
                    <div class="box  box-primary">
                                <div class="box-body table-responsive no-padding">                                    
                                <table class="table table-hover">
                                    <thead>
                                    <tr>                                          
                                          <th>Campaign</th>                                         
                                          <th>No. Of Activities Awaiting Verification&nbsp;<i class="fa fa-info-circle" data-toggle="tooltip" data-placement="right" title="This column specifies the number of pending/total activities"></i></th>
                                          <th>&nbsp;</th>                                                                                                                          
                                          <th>&nbsp;</th>                                                                                    
                                      </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepTab" runat="server">
                                        <ItemTemplate>
                                                                  <tr>
                                                                      <td>
                                                                          <asp:LinkButton runat="server" ID="btn_View" OnClick="btn_View_Click" CommandArgument='<%#Eval("campaign_id") %>'><%#Eval("campaign_name") %></asp:LinkButton>
                                                                          
                                                                      </td>                                                                     
                                                                     <td>
                                                                          <%#Eval("num_of_pending_activities") %> Activities
                                                                      </td>
                                                                      <td>
                                                                          <%#Eval("num_of_pending_activities")%>/<%#Eval("num_of_activities") %> Activities Awaiting Verification
                                                                          
                                                                          <div class="progress sm">
                                                    <div class="progress-bar progress-bar-red" style="width: <%#Convert.ToInt64(Eval("num_of_pending_activities"))*100/Convert.ToInt64(Eval("num_of_activities"))%>%;"></div>
                                                </div>
                                                                      </td>
                                                                      <td>
                                                                          <asp:LinkButton ID="lnk_User" runat="server" OnClick="lnk_User_Click" CommandArgument='<%#Eval("campaign_id")+"~"+Eval("campaign_name") %>' CssClass="btn btn-primary" Visible='<%#Convert.ToInt16( Eval("num_of_pending_activities"))==0 ? false : true%>'>Verify</asp:LinkButton>                                                                          
                                                                      </td>                                                                     
                                                                  </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                  
                                <asp:Label runat="server" ID="lblNoCampaigns" Visible="false">No Users Created. Click on "Create User" to add a new user.</asp:Label>
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
              
