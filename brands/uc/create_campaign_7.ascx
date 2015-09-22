<%@ Control Language="C#" AutoEventWireup="true" CodeFile="create_campaign_7.ascx.cs" Inherits="brands_uc_create_campaign_7" %>

<div class="form-group">
    <table style="width:100%">
        <tr>
            <td style="background-color:#fafafa">
                <asp:CheckBox runat="server" ID="chk_Action_2" Text="&nbsp;Post about us on FB" Checked="true" />     
            </td>
        </tr>             
        <tr>
            <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="form-group">
                                                           <label for="txtReward_1" class="control-label">
                                                                     <span class="text-light-blue">FB Page</span> * <i class="fa fa-info-circle"></i></label>
                                                           <div>
                                                               <div  class="col-md-12" style="padding-left:0px;">
                                                                   <asp:DropDownList ID="DrpPages1" runat="server" Width="250" AutoPostBack="True" OnSelectedIndexChanged="DrpPages1_SelectedIndexChanged">
                                                                    </asp:DropDownList> 
                                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Select A Page"
                                             ControlToValidate="DrpPages1" ValidationGroup="agent" InitialValue="0" ForeColor="Red"/>
                                                                   <asp:Label ID="lblTag1" runat="server" class="text-light-blue"></asp:Label>
                                                                    <asp:Label ID="lblNoPages1" runat="server" Visible="false" BorderStyle="Dotted">You have not configured any pages. Please goto settings and ADD PAGES.</asp:Label>
                                                               </div>
                                                           </div>
                                                       </div>
                        
                    
                                            </ContentTemplate>
                        </asp:UpdatePanel>

            </td>
        </tr>
        <tr>
            <td>                <div>&nbsp</div></td>
        </tr>
         <tr>
            <td>                <div>&nbsp;</div></td>
        </tr>
        <tr>
            <td>
               
                <asp:CheckBox runat="server" ID="chk_Action_1" Text="&nbsp;Tweet about us on twitter" Checked="true" />          
            </td>
        </tr>  
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="form-group">
                                                           <label for="txtReward_1" class="control-label">
                                                                     <span class="text-light-blue">Twitter Page</span> * <i class="fa fa-info-circle"></i></label>
                                                           <div>
                                                               <div  class="col-md-12"  style="padding-left:0px;">
                                                                   <asp:DropDownList ID="DrpPages2" runat="server" Width="250" AutoPostBack="True" OnSelectedIndexChanged="DrpPages2_SelectedIndexChanged">
                                                                    </asp:DropDownList> 
                                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select A Page"
                                             ControlToValidate="DrpPages2" ValidationGroup="agent" InitialValue="0" ForeColor="Red"/>
                                                                   <asp:Label ID="lblTag2" runat="server" class="text-light-blue"></asp:Label>
                                                                    <asp:Label ID="lblNoPages2" runat="server" Visible="false" BorderStyle="Dotted">You have not configured any pages. Please goto settings and ADD PAGES.</asp:Label>
                                                               </div>
                                                           </div>
                                                       </div>
                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
              <tr>
            <td>                <div>&nbsp;</div></td>
        </tr>
         <tr>
            <td>                <div>&nbsp;</div></td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox runat="server" ID="chk_Action_3" Text="&nbsp;Post a pic about us on Insta" Checked="true" />  
                <asp:CustomValidator runat="server" ID="CustomValidator1" ValidationGroup="save" onservervalidate="valCheckbox1_ServerValidate" ErrorMessage="Select Action" ForeColor="Red" />            
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="form-group">
                                                           <label for="txtReward_1" class="control-label">
                                                                     <span class="text-light-blue">Instagram Page</span> * <i class="fa fa-info-circle"></i></label>
                                                           <div>
                                                               <div  class="col-md-12"  style="padding-left:0px;">
                                                                   <asp:DropDownList ID="DrpPages3" runat="server" Width="250" AutoPostBack="True" OnSelectedIndexChanged="DrpPages3_SelectedIndexChanged">
                                                                    </asp:DropDownList> 
                                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select A Page"
                                             ControlToValidate="DrpPages3" ValidationGroup="agent" InitialValue="0" ForeColor="Red"/>
                                                                   <asp:HiddenField ID="HiddenField2" runat="server" />
                                                                   <asp:Label ID="lblTag3" runat="server" class="text-light-blue"></asp:Label>
                                                                    <asp:Label ID="lblNoPages3" runat="server" Visible="false" BorderStyle="Dotted">You have not configured any pages. Please goto settings and ADD PAGES.</asp:Label>
                                                               </div>
                                                           </div>
                                                       </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
         <tr>
            <td>                <div>&nbsp;</div></td>
        </tr>
         <tr>
            <td>                <div>&nbsp;</div></td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                <div class="form-group">
                                                                               <label for="txtReward_1" class="control-label">
                                                                                         <span  class="text-light-blue"> Any additional hashtags and text.</span> * <i class="fa fa-info-circle"></i></label>
                                                                               <div  class="col-md-12" style="padding-left:0px;">                                                                   
                                                                                       <asp:TextBox ID="txtHashtags4" OnTextChanged="txtHashtags4_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control" placeholder="(Any optional #HASHTAG specific to campaign)" Columns="30"/>
                                                                               </div>
                                                                           </div>
                      <div>&nbsp;</div>
                    <div class="form-group">
                                                                               <div  class="col-md-12" style="padding-left:0px;">
                                                                                       <asp:TextBox ID="txtDefaultText" OnTextChanged="txtDefaultText_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control" placeholder="(Any Default Text The Post Should Contain?)" Columns="50"/>
                                                                               </div>
                                                                           </div>
                    <asp:Label ID="lblNohastag4" runat="server" Visible="false" BorderStyle="Dotted"></asp:Label>
                        </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <div>&nbsp;</div>                           
                                        <div class="form-actions text-right pal">                                           
                                           <asp:Label ID="lblValidationErrors" runat="server" CssClass="my_status_notification label label-red" Visible="false"></asp:Label>                                          
                                           <asp:Button runat="server" Text="Back" ValidationGroup="save" ID="btn_Back" OnClick="btn_Back_Click" CssClass="btn btn-primary" />
                                           <asp:Button runat="server" Text="Next" ValidationGroup="save" ID="btn_Next" OnClick="btn_Next_Click" CssClass="btn btn-primary" />
                                           <asp:Label ID="lblErrorMsg" runat="server" ForeColor="red"></asp:Label>
                                        </div>
            </td>
        </tr>
    </table>
    
</div>

 


                                                            