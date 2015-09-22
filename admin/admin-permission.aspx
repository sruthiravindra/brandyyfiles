<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/admin/adminMasterPage.master" CodeFile="admin-permission.aspx.cs" Inherits="admin_admin_permission" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <section class="content"> 
          <div>
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                       <div class="row" style="margin-top:20px;">
             <div class="col-md-4">
                   <div class="box box-primary">     
                                <div class="box-header" style="cursor: move; margin:10px;">
<asp:DropDownList ID="drpUser" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpUser_SelectedIndexChanged"  class="form-control" Width="315px"  Font-Size="16px" ></asp:DropDownList><br />
 <asp:CheckBoxList ID="chkPermission" runat="server" AutoPostBack="True"  CellPadding="5"  Font-Size="14px"  />
                <asp:Button ID="btnSave" runat="server" Text="SAVE" OnClick="btnSave_Click" class="btn btn-primary" ValidationGroup="Save"/>
                                   <a href='<%= SessionState.WebsiteURLAdmin + "adminusers.aspx" %>' class="btn btn-primary" >BACK</a>
                                    </div>
                       </div>
                 </div>
                           </div>
                </ContentTemplate>
                                          </asp:UpdatePanel>
          </div>
          </section>
</asp:Content>

