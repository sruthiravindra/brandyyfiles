<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/admin/adminMasterPage.master"  CodeFile="homepage.aspx.cs" Inherits="admin_homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate> 
                <!-- Main content -->
                <section class="content">                    
                </section><!-- /.content -->
             </ContentTemplate>     
       </asp:UpdatePanel>
</asp:Content>
