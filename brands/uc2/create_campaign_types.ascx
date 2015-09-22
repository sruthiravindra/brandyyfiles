<%@ Control Language="C#" AutoEventWireup="true" CodeFile="create_campaign_types.ascx.cs" Inherits="brands_uc2_create_campaign_types" %>


                                        <div class="row">

            <div class="col-md-6" id="divCampaingTypes" runat="server">
            <div class="box box-primary" style="min-height: 450px;vertical-align:top">
                                            <h3 style="font-size:16px" class="text-black">&nbsp;CHOOSE AN OBJECTIVE FOR YOUR CAMPAIGN</h3>  
                                                <table class="table table-hover" style="text-align:left;">                                                                                                            
                                                    <tr><td>
                                                   <asp:Repeater ID="Repeater1" runat="server">
                                                     <ItemTemplate>
                                                         <!-- Active Campaigns -->
                                                        <div class="col-lg-6" style="">
                                        
                                                        <div class="box box-solid" style="height: 120px;background:white;border:2px solid #eee;">
                                            
                                                            <div class="createcampaign">
                                                                <div style="clear:both"></div>
                                                
                                                                <div class="imgbox2">
                                                                <div class="imgbox1">                                                    
                                                                        <img src='<%=SessionState.WebsiteURLAdmin+ "images/campaign_types/campaign_type_"%><%#Eval("id")%>.png' />
                                                                </div>
                                                                </div>                                                                                                
                                                                <asp:LinkButton runat="server" ID="LinkButton1" OnClientClick=<%# "setCamapignType('" + Eval("id") +"','" + Eval("name") +"')"%>><%#Eval("name") %></asp:LinkButton>                                                
                                                            </div>
                                                            <!-- /.chat -->
                                
                                                        </div><!-- /.box (latest updates) --> 
                                                        </div>
                                                       <%--<tr>                                                                                
                                                        <td style="width:5%;text-align:center"><img src='<%=SessionState.WebsiteURLAdmin+ "images/campaign_types/campaign_type_"%><%#Eval("id")%>.png' /></td>
                                                        <td style="width:95%;cursor:pointer" title="<%#Eval("name") %>">
                                                            <asp:LinkButton runat="server" ID="LinkButton1" OnClientClick=<%# "setCamapignType('" + Eval("id") +"','" + Eval("name") +"')"%>><%#Eval("name") %></asp:LinkButton>
                                                        </td>
                                                       </tr> --%>                                                                       
                                                     </ItemTemplate>
                                                   </asp:Repeater>
                                                  </td></tr>
                                                </table>
                                        </div>
                </div>
            <div class="col-md-8" id="ucc1" runat="server" style="min-height: 450px;vertical-align:top"></div>
</div>
                                        