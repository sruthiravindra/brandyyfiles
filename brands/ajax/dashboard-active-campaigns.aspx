<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dashboard-active-campaigns.aspx.cs" Inherits="brands_ajax_dashboard_active_campaigns" %>

<!-- chat item -->
<form id="Form1" runat="server">    
    <table class="table">
                                        <tbody>
                                       
<asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
                                        <tr>
                                            <td style="vertical-align:top;border:1px solid white;"><%=++Cnt %>.</td>
                                            <td style="border:1px solid white;"><a href="#" class="name" onclick="LoadCampaignView('<%#Eval("campaign_id")%>')">                                                
                                                    <%#Eval("campaign_name") %>
                                                </a><br />
                                                Max Brandyy Points To Win <%#Eval("max_brandyy_points") %> BP
                                            </td>
                                            <td style="border:1px solid white;">
                                                <small class="text-muted pull-left" title="Campaign running since"><i class="fa fa-clock-o"></i> <%#Eval("campaign_start") %> </small><br />
                                                <small class="text-muted pull-left" title="Campaign valid till" style='display:<%#(Convert.ToByte(Eval("schedule_type") ) == 2 ? "" : "none" )%>'><i class="fa fa-clock-o"></i> - <%#Eval("campaign_end") %> </small>
                                            </td>                                            
                                        </tr>
        

                                    <!-- /.item -->
        </ItemTemplate>
    </asp:Repeater>
        <tr><td colspan="6" style="text-align:center"><asp:Label runat="server" ID="lblNoActiveCampaigns" Visible="false">&nbsp;No Active Campaigns To List. 
            <br /><a href="<%=SessionState.WebsiteURLBrand %>brand-create-campaign.aspx">Click here</a> to start creating new campaigns.
            <br />OR
            <br /><a href="<%=SessionState.WebsiteURLBrand %>brandcampaigns.aspx">Click here</a> to activate already created campaigns.
                                                      </asp:Label></td></tr>
        </tbody></table>

</form>
