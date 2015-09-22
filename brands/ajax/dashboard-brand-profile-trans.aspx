<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dashboard-brand-profile-trans.aspx.cs" Inherits="brands_ajax_dashboard_brand_profile_trans" %>

<!-- chat item -->
<form runat="server">
<asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
                                    <div class="item">
                                        <p><a href="#" class="name">                                                
                                                &nbsp;
                                            </a></p>
                                        <p class="message">
                                            <span class="name">
                                                <small class="text-muted pull-right"><i class="fa fa-clock-o"></i> <%#Eval("op_datetime") %></small>
                                               <%#Eval("username") %>
                                            </span>
                                            <%#Eval("log_text") %>
                                        </p>                                        
                                    </div><!-- /.item -->
        </ItemTemplate>
    </asp:Repeater>
</form>