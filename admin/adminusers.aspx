<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/admin/adminMasterPage.master"  CodeFile="adminusers.aspx.cs" Inherits="admin_adminusers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>                
<!-- Main content -->
                <section class="content">
                    <div style="margin-bottom:10px;">
   <a href='<%= SessionState.WebsiteURLAdmin + "adminusers-create.aspx" %>' class="btn btn-warning" >Add New User</a>
                      <span style="margin:5px;"></span>
                           <a href='<%= SessionState.WebsiteURLAdmin + "admin-userrole.aspx" %>' class="btn btn btn-danger" >Add User Role</a> <span style="margin:5px;"></span>
   <a href='<%= SessionState.WebsiteURLAdmin + "admin-permission.aspx" %>' class="btn btn-success" >User Access Permission</a>
                    </div>
                    <div class="box  box-primary">
                                <div class="box-body table-responsive no-padding">                                    
                                <table class="table table-hover">
                                    <thead>
                                    <tr>
                                          <th>#</th>   
                                          <th>Name</th>                                          
                                          <th>Email</th>                                          
                                          <th>Role</th>                                          
                                          <th>Created</th>                                                                                    
                                          <th>Updated</th>                                                                                    
                                          <th>Active</th>                                                                                    
                                          <th>&nbsp;</th>
                                          <th>&nbsp;</th>
                                      </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepTab" runat="server">
                                        <ItemTemplate>
                                          <tr>
                                              <td>
                                                  <%=++Cnt %>
                                              </td>                                              
                                              <td>
                                                  <%#Eval("first_name") %> <%#Eval("last_name") %>
                                              </td>                                              
                                              <td>
                                                  <%#Eval("email") %>                                                  
                                              </td>
                                              <td>                                                  
                                                  <%#Eval("role_name") %>  
                                              </td>                                              
                                              <td>
                                                  <%#Eval("created_date_time") %>
                                              </td>                                              
                                              <td>
                                                  <%#Eval("updated_date_time") %>
                                              </td>                                              
                                              <td>
                                                  <!-- Round Switch -->
	                                            <asp:LinkButton  runat="server"  ID="btn_Status" OnClick="btn_Status_Click" CommandArgument='<%#Eval("id")+","+ Eval("active_flag") %>' ToolTip='<%#( Convert.ToByte( Eval("active_flag") ) == 1 ) ? "Active" : "Inactive" %>'>
                                                    <div id="Div1" runat="server"  class='<%#( Convert.ToBoolean( Eval("active_flag") ) == true ) ? "Switch Round  On" : "Switch Round Off" %>'>		
                                                    <div class="Toggle"></div>            
    	                                            </div>	
	                                            </asp:LinkButton>                                                  
                                                  </td>                                                 
                                              <td><asp:LinkButton ID="btn_Edit" runat="server" role="button" class="btn btn-default" OnClick="btn_Edit_Click" CommandArgument='<%#Eval("id")%>' >Edit</asp:LinkButton></td>                                              
                                          </tr>
                                          </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                  
                                <asp:Label runat="server" ID="lblNoCampaigns" Visible="false">No Users Created. Click on "Create User" to add a new user.</asp:Label>
                            </div><!-- /.box-body -->
                            </div>                    
                </section><!-- /.content -->
             

                <script src="<%=SessionState.WebsiteURLAdmin+ "jquery-ui-1.11.2.custom/jquery-ui.min.js"%>" type="text/javascript"></script>
    <link rel="stylesheet" href="<%=SessionState.WebsiteURLAdmin+ "jquery-ui-1.11.2.custom/jquery-ui.css"%>" />
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
              