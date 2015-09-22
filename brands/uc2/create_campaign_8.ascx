<%@ Control Language="C#" AutoEventWireup="true" CodeFile="create_campaign_8.ascx.cs" Inherits="brands_uc2_create_campaign_8" %>
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
                                        <span class="input-group-addon">Copy location/checkin url from facebook</span>
                                        <asp:TextBox ID="txtCheckinUrl" runat="server" CssClass="form-control" placeholder="Checkin Url" Columns="50"/>                                        
                                    </div>                    
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="Specify a valid url" ValidationGroup="save" ControlToValidate="txtCheckinUrl" ForeColor="Red"/>                    
                </div>  
            </td>
        </tr>
        <tr>
            <td>
                <div>&nbsp;</div>                           
                                            <div class="form-actions text-center pal">   
                                           <asp:Label ID="lblValidationErrors" runat="server" CssClass="my_status_notification label label-red" Visible="false"></asp:Label>                                                                                   
                                           <asp:Button runat="server" Text="Register Page" ID="btn_AddPage" CssClass="btn btn-primary" />&nbsp;
                                           <asp:Button runat="server" Text="Continue" ValidationGroup="save" ID="btn_Next" OnClick="btn_Next_Click" CssClass="btn btn-primary" />
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
