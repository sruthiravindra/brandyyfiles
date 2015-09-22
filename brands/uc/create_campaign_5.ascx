<%@ Control Language="C#" AutoEventWireup="true" CodeFile="create_campaign_5.ascx.cs" Inherits="brands_uc_create_campaign_5" %>

<div class="form-group">
    <table style="width:100%">        
        <tr>
            <td style="background-color:#fafafa">
                <asp:CheckBox runat="server" ID="chk_Action_2" Text="&nbsp;Checkin Our Store On Facebook" Checked="true" />
                &nbsp;                
                <asp:CustomValidator runat="server" ID="CustomValidator1" ValidationGroup="save" onservervalidate="valCheckbox1_ServerValidate" ErrorMessage="Select Action" ForeColor="Red" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                       <div class="form-group">
                                                           <label for="txtReward_1" class="control-label">
                                                              Specify the location/checkin url from facebook* <i class="fa fa-info-circle" title="Copy the url from the browsing bar"></i></label>
                                                           <div>
                                                               <div  class="col-md-12">
                                                                   <asp:TextBox ID="txtCheckinUrl" runat="server" CssClass="form-control" placeholder="Checkin Url" Columns="50"/>                                                                                                                    
                                                                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="Specify a valid url" ValidationGroup="save" ControlToValidate="txtCheckinUrl" ForeColor="Red"/>
                                                               </div>
                                                           </div>
                                                       </div>
                                                        </ContentTemplate>                                                         
                                                    </asp:UpdatePanel>
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

