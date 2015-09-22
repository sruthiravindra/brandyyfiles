<%@ Page Language="C#" MasterPageFile="~/admin/adminMasterPage.master" AutoEventWireup="true" CodeFile="brands-listing.aspx.cs" Inherits="brands_listing" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 


<section class="content">
    <div class="row">
                        <section class="col-lg-6"> 
                    <div class="box box-primary">
                                <div class="box-body table-responsive no-padding"> 
                                <table class="table table-hover">
                                    <thead>
                                    <tr class="heading">
                                        <th>#</th>
                                        <th>&nbsp;</th>
                                        <th>Brand</th>                                                                                
                                        <th>Status</th>                                        
                                      </tr>
                                    </thead>
                                    <tbody>                                                
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate> 
                                       <asp:Repeater ID="RepTab" runat="server" OnItemCommand="RepTab_ItemCommand">
                                        <ItemTemplate>
                                          <tr>
                                              <td><%= Cnt++%></td>
                                              <td><img src="<%=SessionState.WebsiteURLBrand %>uploads/logos/<%#Eval("id")%>.png" style="width:60px;" alt=""/></td>                                                                                                                                          
                                              <td><%#Eval("name")%></td>                                                                                                                                          
                                              <td><asp:Button ID="Button1" runat="server" CommandName="ActivateDeactivate" CommandArgument='<%#Eval("id") + "," + Eval("active_flag") +"," + Eval("name") %>' Text='<%#(Convert.ToBoolean(Eval("active_flag"))) ? "Active" : "Inactive"%>' /></td>                                              
                                          </tr>
                                          </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                        </ContentTemplate>      
                                    </asp:UpdatePanel>
                                </table>
                                <asp:Label runat="server" ID="lblNoCampaigns" Visible="false">No Brands Created. Click on "Create Brand" to create a new brand.</asp:Label>
                            </div><!-- /.box-body -->
                            </div>           
                            </section>
        </div>         
                </section><!-- /.content -->
                <!--END CONTENT-->      
      </asp:Content>
      