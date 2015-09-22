<%@ Page Language="C#" MasterPageFile="~/admin/adminMasterPage.master" AutoEventWireup="true" CodeFile="campaigns-listing.aspx.cs" Inherits="admin_campaigns_listing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 


                <!--BEGIN CONTENT-->
                <div class="page-content">
                    <div id="tab-general">
                        <div class="row mbl">
                            <div class="col-lg-12">
                        <div class="panel panel-white" style="background:#FFF;">
                            <div class="panel-heading">
                                Campaigns Pending Verification
                            </div>
                            <div class="panel-body">
                                <table class="table table-hover">
                                    <thead>
                                    <tr>  
                                          <th>Brand</th>                                           
                                          <th>Name</th>                                                                                    
                                          <th>Schedule</th>
                                          <th>Reward</th>                                                                                    
                                          <th>Verification Status</th>                                                                                    
                                          <th>View</th>
                                      </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RepTab" runat="server">
                                        <ItemTemplate>
                                          <tr>
                                              <td><img src="<%=SessionState.WebsiteURLBrand %>uploads/logos/<%#Eval("brand_id") %>.png" style="width:60px;" /></td>                                              
                                              <td><%#Eval("campaign_name") %></td>
                                              <td>
                                                  <p><%#Eval("campaign_execution_period") %></p>

                                              </td>
                                              <td>                                                  
                                                  <p>To <%#Eval("reward_whom_val") %></p>                                                                                                    
                                              </td>                                              
                                              <td>
                                                  <asp:Button runat="server" ID="btn_Verification" role="button"  CssClass='verification_status_2 btn' Text='Approve' OnClick="btn_Verification_Click" CommandArgument='<%#Eval("campaign_id")+","+ Eval("brand_id")+","+ Eval("campaign_status") + ",2" %>' />
                                                  <asp:Button runat="server" ID="Button1" role="button"  CssClass='verification_status_3 btn' Text='Reject' OnClick="btn_Verification_Click" CommandArgument='<%#Eval("campaign_id")+","+ Eval("brand_id")+","+ Eval("campaign_status") + ",3" %>' />
                                              </td>                                              
                                              <td><asp:LinkButton ID="btn_View" runat="server" role="button" class="btn btn-default" OnClick="btn_View_Click" CommandArgument='<%#Eval("campaign_id") + "," + Eval("brand_id")%>' >View</asp:LinkButton></td>
                                          </tr>
                                          </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                <asp:Label runat="server" ID="lblNoCampaigns" Visible="false">No Pending Campaigns.</asp:Label>
                            </div>
                        </div>
                    </div>                              
                        </div>
                        <div class="row mbl">
                            <div class="col-lg-12">
                        <div class="panel panel-white" style="background:#FFF;">
                            <div class="panel-heading">
                                All Campaigns
                            </div>
                            <div class="panel-body">
                                <table class="table table-hover">
                                    <thead>
                                    <tr>  
                                          <th>Brand</th>                                           
                                          <th>Name</th>                                                                                    
                                          <th>Schedule</th>
                                          <th>Reward</th>                                                                                    
                                          <th>Verification Status</th>
                                          <th>Camapign Status</th>                                          
                                          <th>View</th>
                                      </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="Repeater1" runat="server">
                                        <ItemTemplate>
                                          <tr>
                                              <td><img src="<%=SessionState.WebsiteURLBrand %>uploads/logos/<%#Eval("brand_id") %>.png" style="width:60px;" /></td>                                              
                                              <td><%#Eval("campaign_name") %></td>
                                              <td>
                                                  <p><%#Eval("campaign_execution_period") %></p>

                                              </td>
                                              <td>                                                  
                                                  <p>To <%#Eval("reward_whom_val") %></p>                                                                                                    
                                              </td>                                              
                                              <td>
                                                  <span class='label label-sm <%#( Convert.ToInt16( Eval("verification_status") ) >=0 )? "verification_status_" + Convert.ToString(Eval("verification_status")) : "" %>'><%#Eval("verification_name")%></span>
                                                  </td>
                                              <td>                                                  
                                                  <span class='label label-sm <%#( Convert.ToInt16( Eval("campaign_status") ) == 0)? "label-warning" : "label-success" %>'><%#( Convert.ToByte( Eval("campaign_status") ) == 1 ) ? "active" : "not active" %></span>
                                              </td>                                              
                                              <td><asp:LinkButton ID="btn_View" runat="server" role="button" class="btn btn-default" OnClick="btn_View_Click" CommandArgument='<%#Eval("campaign_id") + "," + Eval("brand_id")%>' >View</asp:LinkButton></td>
                                          </tr>
                                          </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                <asp:Label runat="server" ID="Label1" Visible="false">No Campaigns Created.</asp:Label>
                            </div>
                        </div>
                    </div>                              
                        </div>
                    </div>
                </div>
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
                <!--END CONTENT-->      
      </asp:Content>
      