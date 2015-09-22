<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dashboard-campaign-activities.aspx.cs" Inherits="brands_ajax_dashboard_campaign_activities" %>


<!-- chat item -->
<form id="Form1" runat="server">
<asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
                                    <div class="item">
                                        <p><a href="#"  class="item">                                                
                                                <img src="<%#Eval("profile_image_url") %>" />
                                            </a></p>
                                        <p class="message">
                                            <a href="#" class="name">
                                                <small class="text-muted pull-right"><i class="fa fa-clock-o"></i> <%#Eval("insertdatetime") %></small>
                                               <%#Eval("name") %>
                                            </a>
                                            <%#Eval("campaign_type_desc") %>
                                        </p>                                        
                                    </div><!-- /.item -->
        </ItemTemplate>
    </asp:Repeater>
</form>