<%@ Control Language="C#" AutoEventWireup="true" CodeFile="create_campaign_16.ascx.cs" Inherits="brands_uc2_create_campaign_16" %>

<div class="box box-primary" style="min-height:350px;height:350px">
<div class="form-group">
    <div class="row">&nbsp;</div>
    <div class="row">
        <div class="col-md-12" style="text-align:center">
            <img src="<%=SessionState.WebsiteURLAdmin+ "images/campaign_types/campaign_type_"%><%=SessionState._Campaign.campaign_objective %>.png" alt="user image" class="online">
            <h3 style="font-size:16px" class="text-black">&nbsp;<%=SessionState._Campaign.campaign_name %></h3>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12" style="text-align:center">
    <table style="width:100%">
        <tr>
            <td>
                <asp:CheckBox runat="server" ID="chk_Action_1" Text="&nbsp;Like Our Fb Page" Checked="true" style="display:none"/>
                <asp:CustomValidator runat="server" ID="CustomValidator1" ValidationGroup="save" onservervalidate="valCheckbox1_ServerValidate" ErrorMessage="Select Action" ForeColor="Red" />
                
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                <div  class="col-md-6">
                <div class="form-group">
                                            <label for="fileSlideShow">Select Photo You Want User To View</label>
                                            <div class="input-group input-group-sm">
                                                                <asp:FileUpload ID="fileSlideShow" runat="server"></asp:FileUpload>                                             
                                                                <span class="input-group-btn">
                                                                    <asp:LinkButton runat="server" CssClass="btn btn-primary"  ID="lnkUploadToGallery"  OnClick="lnkUploadToGallery_Click">Upload To Gallery</asp:LinkButton>                                                                    
                                                                </span>
                                            </div>                                            
                                        </div>                
                </div>
                                    </ContentTemplate>
                    <Triggers>
                    <asp:PostBackTrigger ControlID="lnkUploadToGallery" />
                    </Triggers> 
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <div  class="col-md-12">
                    <!-- start slider -->
                    <div>                      
                      <div class="flexslider">
                      <ul class="slides">
                        <asp:Repeater runat="server" ID="repSlideShow">
                            <ItemTemplate>
                            <li>
                              <img class="thumbnail" src="<%# DataBinder.Eval(Container.DataItem,"Value") %>" title="<%# (DataBinder.Eval(Container.DataItem,"Text").ToString()).Split('.')[0].ToString() %>" alt="<%# (DataBinder.Eval(Container.DataItem,"Text").ToString()).Split('.')[0].ToString() %>" />
                                <asp:LinkButton runat="server" ID="btn_RemovePhoto">[Delete]</asp:LinkButton>                    
                            </li>
                            </ItemTemplate>
                        </asp:Repeater>            
                      </ul>
                    </div>         

                    </div>               
                    <!-- end slider -->                                                                                   
                </div>
            </td>
        </tr>        
        <tr>
            <td>
                <div>&nbsp;</div>                           
                                            <div class="form-actions text-center pal">   
                                           <asp:Label ID="lblValidationErrors" runat="server" CssClass="my_status_notification label label-red" Visible="false"></asp:Label>                                                                                                                              
                                           <asp:Button runat="server" Text="Continue" ValidationGroup="save" ID="btn_Next" OnClick="btn_Next_Click" OnClientClick="setStep3();" CssClass="btn btn-primary" />
                                           <asp:Label ID="lblErrorMsg" runat="server" ForeColor="red"></asp:Label>
                                             </div>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
    </div>
    </div>
</div>
</div>

