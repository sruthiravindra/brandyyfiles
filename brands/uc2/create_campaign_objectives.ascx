<%@ Control Language="C#" AutoEventWireup="true" CodeFile="create_campaign_objectives.ascx.cs" Inherits="brands_uc2_create_campaign_objectives" %>



                                        <div class="row" style="min-height:520px">
                                            <div class="col-lg-12" style="text-align:right">
                                                <a class="btn btn-primary" href="<%=SessionState.WebsiteURLBrand %>brand-create-campaign-objectives.aspx">List All objectives</a>                                                
                                            </div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate> 
<asp:Repeater ID="Repeater1" runat="server" OnItemCommand="RepTab_ItemCommand" OnItemDataBound="Repeater1_ItemDataBound">
                                         <ItemTemplate>
                                             
                                        <!-- Active Campaigns -->
                                        <div class="col-lg-2" style="">
                                        
                                        <div class="box box-solid" style="height: 120px;background:white;border:2px solid #eee;">
                                            
                                            <div class="createcampaign">
                                                <div style="clear:both"></div>
                                                
                                                <div class="imgbox2" id='imgGrouping_<%#Eval("grouping").ToString().Replace(" & ","_") %>'>
                                                <div class="imgbox1">                                                    
                                                        <img src="<%=SessionState.WesiteImagesLoadURL %>socialmedia/<%#Eval("icon") %>.png" alt=""  />
                                                </div>
                                                </div>                                                                                                
                                                <asp:LinkButton runat="server" ID="LinkButton1" OnClientClick=<%# "setSession('" + Eval("grouping").ToString().Replace(" & ","_") +"')"%>>                                                
                                                

                                                    Some Text Here Some Text Here Some Text Here Some Text Here 
                                                </asp:LinkButton>
                                            </div>
                                            <!-- /.chat -->
                                
                                        </div><!-- /.box (latest updates) --> 
                                        </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                </ContentTemplate>
    </asp:UpdatePanel>
                                            </div>
                                        

