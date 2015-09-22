<%@ Page Title="" Language="C#" MasterPageFile="~/brands/brandsMasterPage.master" AutoEventWireup="true" CodeFile="brand_transactions.aspx.cs" Inherits="brands_transactions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                        Transactions                        
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>homepage.aspx"> Home </a></li>                         
                        <li class="active">Transactions</li>                        
                    </ol>
                </section>
    <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>                        
                        <small>brandyy point transactions and cash transactions</small>
                    </h1>
                    <ol class="helptext breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>help.aspx?position=4"><i class="fa fa-fw fa-question-circle"></i> Help: What are transactions?</a></li>                        
                    </ol>
                </section>
     <section class="content">   
                    <div class="row">
                        <section class="col-lg-6 connectedSortable"> 
                    <!-- purchase trasactions-->
                            <div class="box box-info">
                                <div class="box-header">
                                    <h3 class="box-title"><i class="fa fa-fw fa-list-alt"></i> Brandyy Points Transactions</h3>                                    
                                </div>
                                <div class="box-body chat" id="divCamapignActivities">
                                                                              <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>   
                                          <asp:Repeater ID="rpTransactions" runat="server" >
                                                 <ItemTemplate>
                    <div class="item">
                                        <p><a href="#" class="name">                                                
                                                &nbsp;
                                            </a></p>
                                        <p class="message" >
                                            <ul style="list-style-type:none">
                                                <li style="margin-left:-10px;">
                                                     <a href='<%= SessionState.WebsiteURLBrand + "brandyypoints-package.aspx" %>' >
                                                    <span class="text-light-blue"><%#Eval("trans_log") %></span>
                                                     </a>
                                                </li>
                                                <li style="margin-left:-10px;">
                                                       <small ><i class="fa fa-clock-o"></i> <%#Eval("trans_date") %></small>
                                                </li>
                                            </ul>
                                        </p>                                        
                                    </div>

                                             </ItemTemplate>
                                              </asp:Repeater>
                </ContentTemplate></asp:UpdatePanel>
                                </div>
                            </div>

                            </section>
                        <section class="col-lg-6 connectedSortable"> 
                             <!-- cash transactions-->
                            <div class="box box-warning">
                                <div class="box-header">
                                    <h3 class="box-title"><i class="fa fa-clipboard"></i>  &nbsp;Cash Transactions</h3>                                    
                                </div>
                                <div class="box-body chat" id="divTransactions">
                                           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>   
                                          <asp:Repeater ID="rpAcitivies" runat="server" OnItemCommand="rpAcitivies_ItemCommand" >
                                                 <ItemTemplate>
                    <div class="item">
                                        <p><a href="#" class="name">                                                
                                                &nbsp;
                                            </a></p>
                                        <p class="message" >
                                            <ul style="list-style-type:none">
                                                <li style="margin-left:-10px;">
                                                    <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("campaign_id") + "," + Eval("reward_date") %>' CommandName="View">
                                                           <span class="text-light-blue" style="font-size:15px;"><%#Eval("name") %>
                                              (Brandyy Points : <%#Eval("reward_amount") %> )</span></asp:LinkButton>
                                                </li>
                                                <li style="margin-left:-10px;">
                                                           <small ><i class="fa fa-clock-o"></i> <%#Eval("reward_date") %></small>
                                                </li>
                                            </ul>
                                         
                                        </p>                                        
                                    </div>

                                             </ItemTemplate>
                                              </asp:Repeater>
                </ContentTemplate></asp:UpdatePanel>
                                </div>
                            </div>
      
                        </section>
                    </div>           
         </section>           
</asp:Content>

