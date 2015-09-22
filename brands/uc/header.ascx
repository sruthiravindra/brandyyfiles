<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header.ascx.cs" Inherits="brands_uc_header" %>
    <header role="banner" class="site_header" id="site_header" style="padding-left: 10px;
  padding-right: 10px;">
        <div class="container-fluid">
          <div class="site_logo">
            <a href="<%=SessionState.WebsiteURLBrand %>homepage.aspx"><img src='<%= SessionState.WebsiteURL %>images/logo.png' alt="logo" /></a>
          </div>
          <ul class='site_nav collapse inversed' role="menu" id="menu_collapse" >
            <li role='menuitem'>
              <a href='#'>
                <div class="menu-icons-earn earn-hover"></div>
                <span>Earn</span>
              </a>
            </li>
            <li role='menuitem'>
              <a href='#'>
                <div class="menu-icons-shop shop-hover"></div>
                <span>Shop</span>
              </a>
            </li>
            <li role='menuitem'>
              <a href='#'>
                <div class="menu-icons-faqs faqs-hover"></div>
                <span >Faqs</span>
              </a>
            </li>
            <li role='menuitem'>
              <a href='<%=SessionState.WebsiteURLBrand %>logout.aspx'>
                <div class="menu-icons-user"></div>
                <span>Logout</span>
              </a>
            </li>
            <li role='menuitem'>
              <a href='#'>
                <div class="menu-icons-useradd"></div>
                <span><%= SessionState._BrandAdmin.username %></span>
              </a>
            </li>
          </ul>
        </div>        
      </header>
