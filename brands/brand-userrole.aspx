<%@ Page Title="" Language="C#" MasterPageFile="~/brands/brandsMasterPage.master" AutoEventWireup="true" CodeFile="brand-userrole.aspx.cs" Inherits="brands_brand_userrole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <section class="content-header">
                    <h1>
                        Roles / Permissions                        
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>homepage.aspx">Home</a></li>
                          <li><a href="<%=SessionState.WebsiteURLBrand %>brandusers.aspx"> Users/Roles</a></li>
                        <li class="active">Roles / Permissions</li>
                    </ol>
                </section>
    <section class="content-header">
                    <h1>                        
                        <small>Create New Roles / Add or Remove role permissions </small>
                    </h1>
                    <ol class="helptext breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>help.aspx#2_1"><i class="fa fa-fw fa-question-circle"></i> Help: How to Update Users Profile/Roles/Permissions</a></li>                        
                    </ol>
                </section>
     <section class="content"> 
               <section class="row" >
               <section class="col-md-6">
                            <div class="box box-info" style="overflow-y: auto;
height: 485px;">     
                                  <div class="box-header">
                                    <h3 class="box-title"> Roles</h3>                                    
                                </div>

                                <div class="box-body" >
                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtRole" runat="server" placeholder="Add New Role" class="form-control"></asp:TextBox>
                                        <span class="input-group-btn" >
                                            <asp:Button ID="Button1" runat="server" Text="Save"  class="btn btn-primary" OnClick="Button1_Click" ValidationGroup="Save" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save" ControlToValidate="txtRole"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <asp:Label ID="lblError" runat="server" Text="" CssClass="text-red" ></asp:Label>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                                           <asp:Repeater ID="rpRoles" runat="server" OnItemCommand="rpRoles_ItemCommand" >
                                         <ItemTemplate>
                                        <div class="box-body" style="padding-left:0px;padding-bottom: 3px;">
                                <div class="alert alert-info alert-dismissable" style="padding: 10px;margin-left:0px;margin-bottom: 0px;background-color:#fff;  width: 485px;
  margin-left: 10px;" id="info" runat="server">
                                                      <b style="margin-right:30px;">  <%# Convert.ToString(Eval("role_name")) %></b>
                                               <span  class="close" style="margin-right: 60px;opacity: .8">
   <asp:LinkButton ID="lnkView" runat="server" class="close" CommandArgument='<%#Eval("role_id") %>' CommandName="Delete" OnClientClick='<%# "return DeleteConfirmation(" + Convert.ToString(Eval("num_of_linked_permissions")) + "," + Convert.ToString(Eval("num_of_linked_users")) +")"%>' ><i class="fa fa-trash-o" data-toggle="tooltip"  data-placement="right" title="" data-original-title="Delete this role"></i></asp:LinkButton></span>
                                            <span  class="close" style="  left: 0px;font-size:18px;  opacity: .8; text-shadow:none;margin-top: -3px;">   <asp:LinkButton ID="lnkPer" runat="server" CommandArgument='<%#Eval("role_id") + "," + Convert.ToString(Eval("role_name")) %>' CommandName="Permission"  CssClass="btn btn-success btn-sm" ForeColor="White">View Permission </asp:LinkButton></span>
                                    </div>
                                            </div>
                           
                                             </ItemTemplate>
                                              </asp:Repeater>    
                    </ContentTemplate>
                                          </asp:UpdatePanel>         
                                    </div>
                                </section>
                                <section class="col-md-6">
                   <div class="box box-danger" style="overflow-y: auto;
height: 485px;">     
                         <div class="box-header">
                                    <h3 class="box-title"> Permissions</h3>                     
                                </div>
                                <div class="box-body" >
                                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                                    <asp:HiddenField ID="hdnRoleid" runat="server" />
                                       <asp:HiddenField ID="hdnrolName" runat="server" />
                <div class="alert alert-info alert-dismissable" runat="server" id="divWarning" visible="false">
                                        <i class="fa fa-info"></i>
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                    </div>

                                    
                <div id="Permisison" runat="server">
                    <div class="input-group input-group-sm">
                    <span class="input-group-btn" >
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary" /></span>
                        </div>
 <asp:CheckBoxList ID="chkPermission" runat="server" AutoPostBack="True"  CellPadding="5"  Font-Size="14px" OnSelectedIndexChanged="chkPermission_SelectedIndexChanged"  />                
                    </div>
                </ContentTemplate>
                                          </asp:UpdatePanel>    
                                    </div>
                       </div>
                 </section>
</section>
          </section>
<script>
    function DeleteConfirmation(num_of_linked_permissions, num_of_linked_users) {
        if (num_of_linked_users > 0) {
            alert("There are active users linked to this role. You need to first unlink the linked users.");
            return false;
        }
        else {
            return confirm('  Please ensure that you have unlinked all the permissions.  Are you sure to delete this role ?  ');
        }
        
        return false;
    }
</script>
</asp:Content>

