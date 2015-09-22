<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/admin/adminMasterPage.master" CodeFile="admin-userrole.aspx.cs" Inherits="admin_admin_userrole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <section class="content"> 
          <div>
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                  <a href='<%= SessionState.WebsiteURLAdmin + "adminusers.aspx" %>' class="btn btn-primary" >BACK</a>

               <div class="row" style="margin-top:20px;">
             <div class="col-md-5">
                            <div class="box box-primary">     
                                <div class="box-header" style="cursor: move;margin:15px;">
                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtRole" runat="server" placeholder="Enter User Role" class="form-control"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:Button ID="Button1" runat="server" Text="Save"  class="btn btn-info btn-flat" OnClick="Button1_Click" ValidationGroup="Save" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save" ControlToValidate="txtRole"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>


                                           <asp:Repeater ID="rpRoles" runat="server" OnItemCommand="rpRoles_ItemCommand" >
                                         <ItemTemplate>
                                        <div class="box-body" style="padding-left:0px;">
                                        <div class="alert alert-info alert-dismissable" style="padding-left: 15px;margin-left:0px;margin-bottom: 0px;"/>
                                        <b style="margin-right:30px;"><%# Convert.ToString(Eval("role_name")) %></b>
   <asp:LinkButton ID="lnkView" runat="server" class="close" CommandArgument='<%#Eval("role_id") %>' CommandName="Delete" OnClientClick="return confirm('   Are you sure to delete this role   ')" ><i class="fa fa-trash-o"></i></asp:LinkButton>
                                        </div>
                                        </div>
                                             </ItemTemplate>
                                              </asp:Repeater>

                                      <div class="alert alert-danger alert-dismissable" id="divAlert" runat="server" visible="false">
                                        <i class="fa fa-ban"></i>
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <b>Alert!</b> <asp:Label runat="server" ID="lblErrMsg"></asp:Label>
                                    </div>
                                    </div>
                                </div>
                 </div>
                </ContentTemplate>
                                          </asp:UpdatePanel>
          </div>
          </section>
</asp:Content>

