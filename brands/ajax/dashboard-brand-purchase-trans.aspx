<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dashboard-brand-purchase-trans.aspx.cs" Inherits="brands_ajax_dashboard_brand_purchase_trans" %>

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
                                                <small class="text-muted pull-right"><i class="fa fa-clock-o"></i> <%#Eval("trans_date") %></small>
                                               <%#Eval("username") %>
                                            </span>
                                            <%#Eval("trans_log") %>
                                        </p>                                        
                                    </div><!-- /.item -->
        </ItemTemplate>
    </asp:Repeater>
</form>