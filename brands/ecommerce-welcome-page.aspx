<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/brands/brandsMasterPage.master" CodeFile="ecommerce-welcome-page.aspx.cs" Inherits="brands_ecommerce_welcome_page" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>
                        Sale Catalog                      
                    </h1>                    
                    <ol class="breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>homepage.aspx"> Home </a></li>                         
                        <li class="active">Sale Catalog</li>                        
                    </ol>
                </section> 
     <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>                        
                        <small>Welcome To You e-Commerce Panel</small>
                    </h1>
                    <ol class="helptext breadcrumb">
                        <li><a href="<%=SessionState.WebsiteURLBrand %>help.aspx#2_1"><i class="fa fa-fw fa-question-circle"></i> Help: Why is my sale catalog panel not activated?</a></li>                        
                    </ol>
                </section>
     <!-- Main content -->
                <section class="content">
                    <div class="row">                        
                    <div class="col-lg-12">
                      <h3>Only verified customer can use this panel.
                                  <br />Please contact Our Admin To get this panel activated.</h3>
                  </div>
                            </div>                    
                </section><!-- /.content -->
           
      <!--BEGIN CONTENT-->
        
     <!--END CONTENT-->      
     
</asp:Content>

