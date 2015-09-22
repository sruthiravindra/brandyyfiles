<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="brands_default" %>
<%@ Register Src="uc/footer.ascx" TagName="footer" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
  <head>
    <meta charset="utf-8" />
    <title>Brandyy | The Brandy Store.</title>
    <meta name="HandheldFriendly" content="true" />
    <meta name="MobileOptimized" content="320" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <meta itemprop="name" content="Brandyy" />
    <meta itemprop="description" content="The Brandy Store." />
    <meta itemprop="author" content="Brandyy, http://brandyy.com" />
    <meta itemprop="copyright" content="Brandyy (c) 2015" />
    <!-- Add to homescreen for Chrome on Android -->
    <meta name="mobile-web-app-capable" content="yes" />
    <link rel="icon" sizes="192x192" href="./img/brand/touch/chrome-touch-icon-192x192.png" />
    <!-- Add to homescreen for Safari on iOS -->
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="apple-mobile-web-app-title" content="Brandyy" />
    <link rel="apple-touch-icon-precomposed" href="./img/brand/apple-touch-icon-precomposed.png" />
    <!-- Tile icon for Win8 (144x144 + tile color) -->
    <meta name="msapplication-TileImage" content="./img/brand/touch/ms-touch-icon-144x144-precomposed.png" />
    <meta name="msapplication-TileColor" content="#3372DF" />
    <!-- SEO: If your mobile URL is different from the desktop URL, add a canonical link to the desktop page https://developers.google.com/webmasters/smartphone-sites/feature-phones -->
    <!--
<link rel="canonical" href="http://www.example.com/">
-->
    <link rel="icon" href="./img/favicon.png" type="image/x-icon" />
    <!-- For third-generation iPad with high-resolution Retina display: -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="./img/brand/favicon_swasa_144px.png" />
    <!-- For iPhone with high-resolution Retina display: -->
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="./img/brand/favicon_swasa_114px.png" />
    <!-- For first- and second-generation iPad: -->
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="./img/brand/favicon_swasa_72px.png" />
    <!-- For non-Retina iPhone, iPod Touch, and Android 2.1+ devices: -->
    <link rel="apple-touch-icon-precomposed" href="./img/brand/favicon_swasa_57px.png" />
    <!------------------------ STYLES --------------------------------->
         <link href="<%=SessionState.WebsiteURL+ "css/style.css"%>" rel="stylesheet" type="text/css" />
          <link href="<%=SessionState.WebsiteURL+ "css/sprites.css"%>" rel="stylesheet" type="text/css" />
            <link href="<%=SessionState.WebsiteURL+ "css/bootstrap.css"%>" rel="stylesheet" type="text/css" />
             <link href="<%=SessionState.WebsiteURL+ "css/responsive-utils.min.css"%>" rel="stylesheet" type="text/css" />
              <link href="<%=SessionState.WebsiteURLBrand+ "css/StyleSheet1.css"%>" rel="stylesheet" type="text/css" />
    <!------------------------ / STYLES --------------------------------->
  </head>
  <body role="document" class="homepage" itemscope="itemscope" itemtype="http://schema.org/WebPage">
    <div class="site-container" id="site-container">
              <header role="banner" class="site_header" id="site_header" >
        <div class="container-fluid">
          <div class="site_logo">
                <a href='<%=SessionState.WebsiteURL %>'>
            <img src='<%=SessionState.WebsiteURL%>images/logo-2.png' alt="logo" />
                    </a>
          </div>
          <ul class='site_nav collapse inversed' role="menu" id="menu_collapse">
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
                <span>Faqs</span>
              </a>
            </li>
            <li role='menuitem'>
                        <a href='<%=SessionState.WebsiteURLBrand %>default.aspx?0'>
                <div class="menu-icons-user"></div>
                <span>Login</span>
              </a>
            </li>
            <li role='menuitem'>
              <a href='<%=SessionState.WebsiteURLBrand %>default.aspx?1'>
                <div class="menu-icons-useradd"></div>
                <span>Register</span>
              </a>
            </li>
          </ul>
          <button type="button" id="menu_toggler" class="btn btn-default md-hide"
          data-toggle="collapse" href="#menu_collapse" aria-expanded="false"
          aria-controls="menu_collapse">
            <span class="glyphicon glyphicon glyphicon-menu-hamburger" aria-hidden="true"></span>
          </button>
        </div>
      </header>
                            <form runat="server" id="form2">
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
        <div class="landingpage">
               <div class="landing-wrap" >
                            <div class="content-box">
                         <div class="login-form">
                         <h3>SIGN IN WITH Brand Account</h3>
            <div>
                	<div style="float:left; width:98%">
                        <asp:Label ID="lblLogError" runat="server" Text="" ForeColor="Red"></asp:Label>
            				</div>
                                	<div style="float:left; width:98%">
                                <asp:TextBox ID="txtLogEmail" runat="server" placeholder="Email ID"  Height="35px" class="form-control"></asp:TextBox> 
            				</div>
                                <div  style="float:right; width:2%">

                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="txtLogEmail" ValidationGroup="Login" ForeColor="Red" ></asp:RequiredFieldValidator>   
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                        ErrorMessage="*" ControlToValidate="txtLogEmail" ValidationGroup="Login"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        Display="Dynamic"  ForeColor="Red">    </asp:RegularExpressionValidator>
                               
                                </div>
                   <div style="float:left; width:98%">
                                <asp:TextBox ID="txtLogPass" runat="server" placeholder="Password"  Height="35px" class="form-control" TextMode="Password"></asp:TextBox> 
            				</div>
                                <div  style="float:left; width:2%">
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="txtLogPass" ValidationGroup="Login" ForeColor="Red" ></asp:RequiredFieldValidator>   
                                </div>
                <div style="float:left; width:79%">
                               <span > <asp:LinkButton ID="lnkForgotPass" runat="server" OnClick="lnkForgotPass_Click"  ><span style="color:#0061b2">Forgot password ?</span></asp:LinkButton></span>
                </div>
                 <div  style="float:left; width:10%">
                                                 <asp:Button ID="btnLogin" runat="server" Text="Login" class="btn btn-green btn-sm" ValidationGroup="Login" Font-Size="14px" OnClick="btnLogin_Click"  />
                     </div>
              
                 <div id="divMail" runat="server" visible="false" style="margin-bottom:5px;">
                       <div style="float:left; width:98%">   <asp:TextBox ID="txtMail" runat="server" placeholder="Enter registration email" CssClass="form-control" Height="36px"></asp:TextBox> </div>
                                 <div  style="float:left; width:15%;  margin-top: 14px;  margin-left: -75px;">
                                     <asp:Button ID="btnMail" runat="server" Text="Send Mail" class="btn btn-danger btn-sm" ValidationGroup="sendMail" OnClick="btnMail_Click" />
                                     </div>
                      <div  style="float:left; width:2%">
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="txtMail" ValidationGroup="sendMail" ForeColor="Red"></asp:RequiredFieldValidator>
                          </div>
                           <div style="float:left; width:84%"> 
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                        ErrorMessage="*Invalid Email" ControlToValidate="txtMail" ValidationGroup="sendMail"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        Display="Dynamic"  ForeColor="Red">    </asp:RegularExpressionValidator>
                       </div>
                    </div>
                    
                            </div>
                                         <div  style="float:left; width:100%;margin-top:30px;">
                                  <p style="text-align:center">
                                      <a href='<%=SessionState.WebsiteURL%>login.aspx' class="btn btn-primary btn-lg">Login / Register as Brandyy User</a>
                                    
                                  </p>
                                  </div>
                        </div> 
                        
                            <div class="divOr">
                                <span style="font-size:18px;color:#3eb53f;font-weight:bold">OR</span>
                            </div>

                        <div class="contact-form" id="divReg">
                        	<h3>SIGN UP FOR Brand Account</h3>
                        	
                                 <div>
                                     	<div style="float:left; width:98%">
                        <asp:Label ID="lblregError" runat="server" Text="" ForeColor="Red"></asp:Label>
            				</div>
                                	<div style="float:left; width:98%">
                                <asp:TextBox ID="txtFName" runat="server" placeholder="Name"  Height="35px" class="form-control"></asp:TextBox> 
            				</div>
                                <div  style="float:left; width:2%">
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtFName" ValidationGroup="Register" ForeColor="Red" ></asp:RequiredFieldValidator>   
                                </div>   
                            </div>
                                <div>
                                	<div style="float:left; width:98%">
                                         <asp:TextBox ID="txtRegEmail" runat="server" placeholder="Email"  Height="35px" class="form-control"></asp:TextBox>
                                        </div>
                                          <div  style="float:right;width:2%">
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtRegEmail" ValidationGroup="Register" ForeColor="Red"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ErrorMessage="*" ControlToValidate="txtRegEmail" ValidationGroup="Register"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        Display="Dynamic"  ForeColor="Red">    </asp:RegularExpressionValidator>
                                              </div>
                                    </div>
                                <div>
                                	<div style="float:left; width:98%">
            			 <asp:TextBox ID="txtRegPass" runat="server" placeholder="Password"  TextMode="Password" Height="35px" class="form-control"></asp:TextBox>
                                   </div>
                                    <div  style="float:right;width:2%">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtRegPass" ValidationGroup="Register" ForeColor="Red"></asp:RequiredFieldValidator>
                                                   </div>
                                    </div>

                                     <div style="float:left; width:98%">
                       <asp:TextBox ID="txtConPass" runat="server" TextMode="Password" placeholder="Confirm Password" class="form-control" Height="35px"></asp:TextBox>
                    </div>
                              <div style="float:right; width:2%">
                            
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtConPass" ValidationGroup="Register" ForeColor="Red"></asp:RequiredFieldValidator>
                                  <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ValidationGroup="Register" ForeColor="Red" ControlToCompare="txtRegPass" ControlToValidate="txtConPass"></asp:CompareValidator>
                                  </div>
                            
                            <div style="float:left; width:10%;   margin-top: 15px;">
                                   <span>Logo </span>
                                   </div>
                             <div style="float:left; width:30%; margin-top: 15px;">
                                <asp:FileUpload ID="FileUpload1" runat="server" Font-Size="Smaller" BorderStyle="NotSet" Font-Strikeout="False" />
                            </div>
                                <div style="float:left; width:2%">
  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ValidationGroup="Register" ControlToValidate="FileUpload1" ForeColor="Red" />
                    
                                  </div>
                                 <div style="float:right; width:25%;  margin-top: 10px;">
                                   <asp:Button ID="btnRegister" runat="server" Text="Register" class="btn btn-green btn-sm" ValidationGroup="Register"  Font-Size="14px" OnClick="btnRegister_Click"/>
                                     </div>
                             
                        </div>
                    </div>  
                     
               </div>
            </div>
                                        </ContentTemplate>
                                            <Triggers>
                                      <asp:PostBackTrigger ControlID="btnRegister" />
                                  </Triggers>
                                    </asp:UpdatePanel>
               </form>
                 
    </div>
                  <uc1:footer ID="footer" runat="server" />
</body>
</html>
    <!------------------------ SCRIPTS --------------------------------->
    <script src='<%=SessionState.WebsiteURL+ "js/jquery.min.js"%>'></script>
    <script src='<%=SessionState.WebsiteURL+ "js/bootstrap.min.js"%>'></script>

    <!------------------------ / SCRIPTS --------------------------------->
<script>
    $(document).ready(function () {

        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1);
        if (hashes == "1") {
            document.getElementById('txtFName').focus();
        }
        else {
            document.getElementById('txtLogEmail').focus();
        }
    })

</script>
 