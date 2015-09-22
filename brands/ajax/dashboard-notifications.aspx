<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dashboard-notifications.aspx.cs" Inherits="brands_ajax_dashboard_notifications" %>

<!-- chat item -->
<form id="Form1" runat="server">
    <table class="table">
                                        <tbody>
<asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
        <tr>
                                            <td style="vertical-align:top;border:1px solid white;"><%=++Cnt %>.</td>
                                            <td style="border:1px solid white;">
                                                <img src="<%=SessionState.WesiteImagesLoadURL %>socialmedia/<%#Eval("id")%>.png" alt=""/> <span>Account <%#Eval("sm_name") %></span>
                                            </td>
                                            <td style="border:1px solid white;width:250px;">
                                                <small class="text-muted pull-right"><%#Eval("name") %></small>
                                            </td>                                            
                                        </tr>                                    
        </ItemTemplate>
    </asp:Repeater>
                                       <tr>
                                            <td style="vertical-align:top;border:1px solid white;"><%=++Cnt %>.</td>
                                            <td style="border:1px solid white;" colspan="2">
                                                There are <strong class="bg-red">&nbsp;<%=pending_activities_verification %>&nbsp;</strong> user activities pending for verification
                                                <small class="text-muted pull-right"><a class="btn btn-primary" href="<%=SessionState.WebsiteURLBrand %>activity-verification-overview.aspx">Verify</a></small>
                                            </td>                                            
                                        </tr>            

</tbody></table>
</form>