<%@ Control Language="C#" AutoEventWireup="true" CodeFile="create_campaign_13.ascx.cs" Inherits="brands_uc2_create_campaign_13" %>


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
        <tr >
            <td>
                <asp:CheckBox runat="server" ID="chk_Action_1" Text="&nbsp;Like Our Fb Page" Checked="true" style="display:none"/>
                <asp:CustomValidator runat="server" ID="CustomValidator1" ValidationGroup="save" onservervalidate="valCheckbox1_ServerValidate" ErrorMessage="Select Action" ForeColor="Red" />
                
            </td>
        </tr>
        <tr>
            <td>                
                <div  class="col-md-12">
                    <div class="input-group" runat="server" id="divPage">
                                        <span class="input-group-addon">Choose a Page</span>
                                        <asp:DropDownList ID="DrpPages1" runat="server" AutoPostBack="True"  CssClass="form-control" OnSelectedIndexChanged="DrpPages1_SelectedIndexChanged"></asp:DropDownList> 
                                        
                                    </div>
                    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Select A Page"
                                             ControlToValidate="DrpPages1" ValidationGroup="agent" InitialValue="0" ForeColor="Red"/>
                    <asp:HiddenField ID="hiddenPageURL1" runat="server" />
                    <asp:Label ID="lblNoPages1" runat="server" Visible="false">
                        <div class="callout callout-danger">
                                        <h4>You have not registered any page with us!</h4>
                                        <p>Please click on <a href="<%=SessionState.WebsiteURLBrand %>socialmedias.aspx">REGISTER PAGE</a>.</p>
                                    </div>
                    </asp:Label>
                </div>  
            </td>
        </tr>
        <tr>
            <td>
                <div>&nbsp;</div>                           
                                            <div class="form-actions text-center pal">   
                                           <asp:Label ID="lblValidationErrors" runat="server" CssClass="my_status_notification label label-red" Visible="false"></asp:Label>                                                                                   
                                           <asp:Button runat="server" Text="Register Page" ID="btn_AddPage" CssClass="btn btn-primary" />&nbsp;
                                           <asp:Button runat="server" Text="Continue" ValidationGroup="save" ID="btn_Next" OnClick="btn_Next_Click" OnClientClick="setStep3()"  CssClass="btn btn-primary" />
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
