<%@ Control Language="C#" AutoEventWireup="true" CodeFile="footer.ascx.cs" Inherits="tmp_footer" %>
      <footer role="contentinfo">
        <div class="container padding-t-md">
          <div class="row">
            <div class="col-sm-6 col-md-3">
              <h4 class="list_heading">Get to know us</h4>
              <ul>
                <li> <a href="#">Home</a> </li>
                <li> <a href="#">About Us</a> </li>
                <li> <a href="#">Contact Us</a> </li>
              </ul>
            </div>
            <div class="col-sm-6 col-md-3">
              <h4 class="list_heading">Get Candyy Points</h4>
              <ul>
                <li> <a href="#">Earn</a> </li>
                <li> <a href="#">Shop</a> </li>
              </ul>
            </div>
            <div class="clearfix hidden-md hidden-lg"></div>
            <div class="col-sm-6 col-md-3">
              <h4 class="list_heading">Let Us Help You</h4>
              <ul>
                <li> <a href="#">Faqs</a> </li>
                <li> <a href="<%=SessionState.WebsiteURL%>login.aspx">User Login / Register</a> </li>
                 <li> <a href="<%=SessionState.WebsiteURLBrand%>default.aspx">Brand Login / Register</a> </li>
              </ul>
            </div>
            <div class="col-sm-6 col-md-3 text-center">
              <div class="footer_logo">
                <img src="img/footer_logo.png" alt="logo" />
              </div>
              <ul class="countries_list">
                <li>
                  <a href="#">USA</a>
                </li>
                <li>
                  <a href="#">INDIA</a>
                </li>
                <li>
                  <a href="#">CHINA</a>
                </li>
              </ul>
            </div>
          </div>
          <div class="site_footer">
            <div class="left">
            </div>
            <div class="right">
              <p class="text-white">© Copyright 2015 Brandyy. All rights reserved.</p>
            </div>
          </div>
        </div>
      </footer>
