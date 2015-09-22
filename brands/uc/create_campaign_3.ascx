<%@ Control Language="C#" AutoEventWireup="true" CodeFile="create_campaign_3.ascx.cs" Inherits="brands_uc_create_campaign_3" %>

<div class="form-group">
    <table style="width:100%">
        <tr>
            <td style="background-color:#fafafa">
                <asp:CheckBox runat="server" ID="chk_Action_1" Text="&nbsp;Like Our Fb Page" Checked="true" />
                &nbsp;
                <asp:CheckBox runat="server" ID="chk_Action_2" Text="&nbsp;Share Our Fb Post" Checked="true" />
                &nbsp;
                <asp:CheckBox runat="server" ID="chk_Action_3" Text="&nbsp;Like Our Fb Post" Checked="true" />
                <asp:CustomValidator runat="server" ID="CustomValidator1" ValidationGroup="save" onservervalidate="valCheckbox1_ServerValidate" ErrorMessage="Select Action" ForeColor="Red" />
            </td>
        </tr>
        <tr>
            <td style="border-bottom:1px solid #e5e5e5">
                <div>&nbsp</div>
                <div>&nbsp</div>
                <div style="font-size:18px">SET ACTION SPECIFIC DETAILS</div>               
            </td>
        </tr>
        <tr>
            <td>
                <div>&nbsp</div>
                                                                       <div class="form-group">
                                                           <label for="txtReward_1" class="control-label">
                                                                      Page * <i class="fa fa-info-circle"></i></label>
                                                           <div>
                                                               <div  class="col-md-12">
                                                                   <asp:DropDownList ID="drpPages2" runat="server" Width="250" AutoPostBack="True" OnSelectedIndexChanged="drpPages2_SelectedIndexChanged">
                                                                    </asp:DropDownList> 
                                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Select A Page"
                                             ControlToValidate="drpPages2" ValidationGroup="agent" InitialValue="0" ForeColor="Red"/>
                                                                   <asp:HiddenField ID="hiddenPageURL2" runat="server" />
                                                                    <asp:Label ID="lblNoPages2" runat="server" Visible="false" BorderStyle="Dotted">You have not configured any pages. Please goto settings and ADD PAGES.</asp:Label>
                                                               </div>
                                                           </div>
                                                       </div>
                                                        <div class="form-group">
                                                            <label for="txtReward_1" class="control-label">
                                                                      Select Post* <i class="fa fa-info-circle"></i></label>
                                                            <div class="col-lg-12">     
                                                                <asp:TextBox runat="server" ID="txtSelectedPost" TextMode="MultiLine" Rows="2" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>                                                               
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select A Post"
                                             ControlToValidate="txtSelectedPost" ValidationGroup="agent" InitialValue="" ForeColor="Red"/>
                                                                        <div runat="server" class="list-group" style="min-height:30px; max-height: 400px; overflow-y:auto;" id="divPagePostsList" visible="false">                                                                        
                                                                        <asp:Repeater ID="rep_PagePosts2" runat="server" OnItemCommand="rep_PagePosts2_ItemCommand">                                                                            
                                                                            <ItemTemplate>                                                                                
                                                                                <asp:LinkButton runat="server" ID="myposts" CssClass="list-group-item" CommandName="SetPostSession"  CommandArgument='<%#Eval("post_id")+ "," +Eval("post_url")%>' >
                                                                                    <img src="<%# Eval("img_url")%>" id='postImgurl_<%# Eval("post_id")%>'>
                                                                                    <p id='postdesc_<%# Eval("post_id")%>'><div runat="server" id="mypostsdiv"><%# Eval("post")%></div></p>
                                                                                    <p><%# Eval("created_on")%></p>                                                                                        
                                                                                </asp:LinkButton>                                                                                
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>   
                                                                            </div>
                                                                    </div>
                                                        </div>
                                                                    <asp:Label ID="lblPagePostsMsg2" runat="server" Visible="false" BorderStyle="Dotted"></asp:Label>
  
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

 