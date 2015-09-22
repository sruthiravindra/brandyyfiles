<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adminMasterPage.master" AutoEventWireup="true" CodeFile="brandyy-points.aspx.cs" Inherits="admin_brandyy_points" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <link href="<%=SessionState.WebsiteURLAdmin %>css/timepicker/bootstrap-timepicker.min.css" rel="stylesheet"/>
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>                
<!-- Main content -->
                <section class="content">
                    <div style="margin-bottom:10px;">
                        <div class="input-group">
                            <table>
                                <tr>
                                    <td>
                                         <asp:TextBox ID="txtpoints" runat="server"  CssClass="form-control" Width="200px"  placeholder="Enter Brandyy Points" onkeypress="return numbersonly(this, event)"></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save" ControlToValidate="txtpoints"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                           <asp:TextBox ID="txtusd" runat="server"  CssClass="form-control" Width="200px" placeholder="Enter USD" onkeypress="return numbersonly(this, event)"></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save" ControlToValidate="txtusd"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                             <asp:TextBox ID="txtdate" runat="server"  CssClass="form-control" Width="200px" data-inputmask="'alias': 'dd/mm/yyyy'" data-mask="" placeholder="Enter Date"></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save" ControlToValidate="txtdate"></asp:RequiredFieldValidator>

                                    </td>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="SAVE" ValidationGroup="Save"  class="btn btn-primary" OnClick="btnSave_Click" />
                                        <asp:HiddenField ID="hdnID" runat="server" />
                                    </td>
                                </tr>
                            </table>
                           
 

                                        </div>
                    </div>
                    <div class="box  box-primary">
                                <div class="box-body table-responsive no-padding">                                    
                                <table class="table table-hover">
                                    <thead>
                                    <tr>
                                          <th>Brandyy Points</th>                                          
                                          <th>USD</th>                                          
                                          <th>Offer Valid</th>                                          
                                          <th>Status</th>        
                                          <th>&nbsp;</th>
                                          <th>&nbsp;</th>
                                      </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rpListing" runat="server" OnItemCommand="rpListing_ItemCommand">
                                        <ItemTemplate>
                                          <tr>                                          
                                              <td>
                                                  <%#Eval("brandyy_points") %>
                                              </td>                                              
                                              <td>
                                                  <%#Eval("package_usd") %>                                                  
                                              </td>
                                              <td>                                                  
                                                  <%# Convert.ToDateTime(Eval("package_valid")).ToString("dd/MM/yyyy") %>  
                                              </td>                            
                                              <td>
   <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-default btn-sm" CommandArgument='<%#Eval("package_id")%>'  CommandName="status"><i class='<%#Convert.ToBoolean(Eval("package_status")) ? "fa fa-square text-green" : "fa fa-square text-red" %>'></i> </asp:LinkButton>
                   
                                                  </td>                                                 
                                              <td><asp:LinkButton ID="btn_Edit" runat="server" role="button" class="btn btn-danger"  CommandArgument='<%#Eval("package_id")%>'  CommandName="Edit">Edit</asp:LinkButton></td>                                              
                                          </tr>
                                          </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div><!-- /.box-body -->
                            </div>                    
                </section><!-- /.content -->
                </ContentTemplate>
                 </asp:UpdatePanel>
          <!-- InputMask -->
        <script src="<%=SessionState.WebsiteURLAdmin %>js/plugins/input-mask/jquery.inputmask.js" type="text/javascript"></script>
        <script src="<%=SessionState.WebsiteURLAdmin %>js/plugins/input-mask/jquery.inputmask.date.extensions.js" type="text/javascript"></script>
        <script src="<%=SessionState.WebsiteURLAdmin %>js/plugins/input-mask/jquery.inputmask.extensions.js" type="text/javascript"></script>
       <!-- bootstrap time picker -->
        <script src="<%=SessionState.WebsiteURLAdmin %>js/plugins/timepicker/bootstrap-timepicker.min.js" type="text/javascript"></script>
    <script src='<%=SessionState.WebsiteURLAdmin+ "custom-js/Only-Numbers.js"%>' type="text/javascript"></script>
     <script type="text/javascript">
         $(function () {
             //Datemask dd/mm/yyyy
             $("#datemask").inputmask("dd/mm/yyyy", { "placeholder": "dd/mm/yyyy" });
             //Datemask2 mm/dd/yyyy
             $("[data-mask]").inputmask();
             //Timepicker
             $(".timepicker").timepicker({
                 showInputs: false
             });
         });
        </script>

</asp:Content>

