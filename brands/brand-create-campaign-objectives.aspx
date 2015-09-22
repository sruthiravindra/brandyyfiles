<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/brands/brandsMasterPage.master" CodeFile="brand-create-campaign-objectives.aspx.cs" Inherits="brands_brand_create_campaign_objectives" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
     <link href="<%=SessionState.WebsiteURLBrand+ "css/StyleSheet1.css"%>" rel="stylesheet" type="text/css" />
       <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                         All Campaign Objectives
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>homepage.aspx"> Home </a></li> 
                        <li><asp:LinkButton runat="server" id="lnkBackToCreate" OnClick="lnkBackToCreate_Click">Create Campaign</asp:LinkButton></li>
                        <li class="active">All Campaign Objectives</li>                         
                    </ol>
                </section> 
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>                        
                        <small>Select One of the campaign objectives and create a corresponding campaign</small>
                    </h1>
                    <ol class="helptext breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>help.aspx?#3_1"><i class="fa fa-fw fa-question-circle"></i> Help: How can I create a campaign</a></li>                        
                    </ol>
                </section> 
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate> 
                <!-- Main content -->
                <section class="content">
                    <div class="box  box-primary">                        
                                
                                <div class="box-body no-padding">
                                    <table class="table">
                                        <tr>
                                            <th style="width: 10px">#</th>
                                            <th>Campaign Objectives</th>                                            
                                            <th style="width: 40px">&nbsp;</th>
                                        </tr>
                                        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="RepTab_ItemCommand">                                            
                                         <ItemTemplate>     
                                             <%#Eval("grouping") %>                                        
                                             <tr>
                                                <td><%=++Cnt %></td>
                                                <td>
                                                    <span><%#Eval("name") %></span></td>
                                                <td>
                                                    <asp:Button ID="btn_CreateCampaign" runat="server" role="button" class="btn btn-primary" Text="Create Campaign" CommandName="CreateCampaign"  CommandArgument='<%#Eval("id")+","+ Eval("name")+","+ Eval("user_display_desc")%>' />      
                                                </td>                                                
                                            </tr>                                             
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    </table>
                                </div><!-- /.box-body -->
                            </div>                    
                </section><!-- /.content -->
            </ContentTemplate>     
       </asp:UpdatePanel>

            
</asp:Content>

                                    