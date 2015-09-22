<%@ Control Language="C#" AutoEventWireup="true" CodeFile="create_campaign_1.ascx.cs" Inherits="brands_uc_create_campaign_1" %>

<div class="form-group">
    <table style="width:100%">
        <tr style="display:none">
            <td style="background-color:#fafafa"><asp:CheckBox runat="server" ID="chk_Action_1" Text="&nbsp;Like Our Fb Page" Checked="true" />
                <asp:CustomValidator runat="server" ID="CustomValidator1" ValidationGroup="save" onservervalidate="valCheckbox1_ServerValidate" ErrorMessage="Select Action" ForeColor="Red" />
            </td>
        </tr>
        <tr>
            <td>
                <label for="txtReward_1" class="control-label">
                Page * <i class="fa fa-info-circle"></i></label>
                <div  class="col-md-12">                                                                                            
                    <asp:DropDownList ID="DrpPages1" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="DrpPages1_SelectedIndexChanged">
                    </asp:DropDownList> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Select A Page"
                                             ControlToValidate="DrpPages1" ValidationGroup="agent" InitialValue="0" ForeColor="Red"/>
                    <asp:HiddenField ID="hiddenPageURL1" runat="server" />
                    <asp:Label ID="lblNoPages1" runat="server" Visible="false" BorderStyle="Dotted">You have not configured any pages. Please click on <a href="<%=SessionState.WebsiteURLBrand %>socialmedias.aspx">ADD PAGES</a>.</asp:Label>
                </div>  
            </td>
        </tr>
        <tr>
            <td>
                <div>&nbsp;</div>                           
                                            <div class="form-actions text-right pal">   
                                           <asp:Label ID="lblValidationErrors" runat="server" CssClass="my_status_notification label label-red" Visible="false"></asp:Label>                                        
                                           <asp:Button runat="server" Text="Back" ID="btn_Back" OnClick="btn_Back_Click" CssClass="btn btn-primary" />
                                           <asp:Button runat="server" Text="Next" ValidationGroup="save" ID="btn_Next" OnClick="btn_Next_Click" CssClass="btn btn-primary" />
                                           <asp:Label ID="lblErrorMsg" runat="server" ForeColor="red"></asp:Label>
                                             </div>
            </td>
        </tr>
    </table>
    
</div>
