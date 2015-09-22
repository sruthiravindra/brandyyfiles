<%@ Page Title="" Language="C#" MasterPageFile="~/brands/brandsMasterPage.master" AutoEventWireup="true" CodeFile="brandyypoints-package.aspx.cs" Inherits="brands_brandyypoints_package" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                        Your Wallet                        
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>homepage.aspx"> Home </a></li>                         
                        <li class="active">Your Wallet</li>                        
                    </ol>
                </section>
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>                        
                        <small>Brandyy Points wallet And Cash wallet</small>
                    </h1>
                    <ol class="helptext breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>help.aspx?position=3"><i class="fa fa-fw fa-question-circle"></i> Help: How to Manage Your Wallet</a></li>                        
                    </ol>
                </section>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate> 
                <!-- Main content -->
                <section class="content">                    
                   <div class="row">
                       <section class="col-md-6">
                       <!-- Your cash Wallet-->
                            <div class="box box-info" style="height:350px;">
                                <div class="box-header">
                                    <h3 class="box-title"><i class="fa fa-fw fa-money"></i> Your Cash Wallet</h3>                                    
                                </div>
                                <div class="box-body">

                                   <div class="row text-center divider" style="margin-left:20%;">
                                     <div class="col-xs-12 col-sm-7 emphasis bg-blue">
                                        <h2><strong>0 USD</strong></h2>
                                           <p>
                                                <small>Remaining</small>
                                           </p>                                                    
                                     </div>
                                  </div>
                                   
                                  <table style="margin-left:20%;margin-top:30px;">
                                      <tr>
                                          <td>
                                              <a class="btn btn-primary">Withdraw</a>
                                              &nbsp;&nbsp;
                                              <a class="btn btn-primary">Add</a>
                                          </td>
                                          <td>

                                          </td>
                                      </tr>
                                  </table>
                                    
                                </div>
                                <!-- /.chat -->
                                
                            </div><!-- /.box (Your cash Wallet) -->    
                       </section>                       
                       <section class="col-md-6">
                       <!-- Your brandyy points Wallet-->
                            <div class="box box-success" style="height:350px;">
                                <div class="box-header">
                                    <h3 class="box-title"><i class="fa fa-fw fa-list-alt"></i> Your Brandyy Points Wallet</h3>                                    
                                </div>
                                <div class="box-body">

                                   <div class="row text-center divider" style="margin-left:20%;">
                                     <div class="col-xs-12 col-sm-7 emphasis bg-green">
                                        <h2><strong><%= pointsrem %> BP</strong></h2>                                         
                                           <p>                                               
                                                <small>Remaining</small>
                                           </p>                                                    
                                     </div>
                                  </div>
                                    <table style="margin-left:20%;margin-top:30px;">
                                      <tr>
                                          <td>
                                              <a class="btn btn-primary">Withdraw</a>
                                              &nbsp;&nbsp;
                                              <a class="btn btn-primary">Add</a>
                                          </td>
                                          <td>

                                          </td>
                                      </tr>
                                  </table>
                                    
                                </div>                                
                                <!-- /.chat -->
                                
                            </div><!-- /.box (Your brandyy points Wallet) -->    
                       </section>
                              <asp:Repeater ID="rpOffers" runat="server" OnItemCommand="rpOffers_ItemCommand" Visible="false">
                                         <ItemTemplate>
                                              <div class="col-md-4">
                                              <div class="box box-primary"  >
                                <div class="box-header">
                                    <h3 class="box-title" >Get <%#Eval("brandyy_points") %> brandyy points for <%# Convert.ToInt64(Eval("package_usd"))%> USD</h3>
                                </div>
                                <div class="box-body chart-responsive">
                                    <div class="chart" id="revenue-chart" style="height: 60px;">
                                        <div style="width: 100px;"> <asp:LinkButton ID="lnkBuy" runat="server"  class="btn btn-primary" Width="100px" CommandArgument='<%#Eval("package_id") %>' CommandName="Package" >BUY</asp:LinkButton></div>
                                    </div>
                                     <div style="color:#3c8dbc">Offer valid till <%# Convert.ToDateTime(Eval("package_valid")).ToString("dd MMMM yyyy")%> </div>
                                </div><!-- /.box-body -->
<asp:HiddenField ID="hdnlog" runat="server" Value='<%# "Purchased " + Eval("brandyy_points").ToString() + " Brandyy Points for " + Eval("package_usd").ToString() + " USD"%>' />
                            </div>        
                                                  </div>                                     
                                        </ItemTemplate>
                                    </asp:Repeater>
                    </div><!-- /.row -->                  
                </section><!-- /.content -->
             </ContentTemplate>     
       </asp:UpdatePanel>
</asp:Content>
