<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="_register" %>
<%@ Register Src="footer.ascx" TagName="footer" TagPrefix="uc1" %>
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
    <link href='./css/bootstrap.css' rel='stylesheet' />
    <link href='./css/sprites.css' rel='stylesheet' />
    <link href='./css/style.css' rel='stylesheet' />
          <link href='./css/StyleSheet.css' rel='stylesheet' />
    <link href='./css/responsive-utils.min.css' rel='stylesheet' />
    <!------------------------ / STYLES --------------------------------->
  </head>
  <body role="document" class="homepage" itemscope="itemscope" itemtype="http://schema.org/WebPage">
    <div class="site-container" id="site-container">
      <header role="banner" class="site_header" id="site_header" >
        <div class="container-fluid">
          <div class="site_logo">
                <a href='<%=SessionState.WebsiteURL %>'>
            <img src="img/logo-2.png" alt="logo" />
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
                        <a href='<%=SessionState.WebsiteURL %>login.aspx'>
                <div class="menu-icons-user"></div>
                <span>Login</span>
              </a>
            </li>
            <li role='menuitem'>
              <a href='<%=SessionState.WebsiteURL %>register.aspx'>
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
                          <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
               <div class="landingpage">
               <div class="landing-wrap"  >
                   <div class="step1-left">
                       <img  src="<%=SessionState.WebsiteURL %>img/step1.png"/><br />
                       <%--<span>2nd Step - Aenean ac pulvinar magna</span>--%>
                       <p>Integer placerat, nulla porttitor facilisis malesuada, velit nibh finibus lacus, in sollicitudin justo augue nec lorem. Mauris scelerisque mauris sed pulvinar auctor. Aenean.
                           Integer placerat, nulla porttitor facilisis malesuada, velit nibh finibus lacus, in sollicitudin justo augue nec lorem. Mauris scelerisque mauris sed pulvinar auctor. Aenean.
                              Integer placerat, nulla porttitor facilisis malesuada, velit nibh finibus lacus, in sollicitudin justo augue nec lorem. Mauris scelerisque mauris sed pulvinar auctor. Aenean.
                       </p>
                   </div>
             <div class="contact-form" id="divReg">
                        	<h3>SIGN UP FOR Brandyy</h3>
                        	
                                 <div>
                                     	<div style="float:left; width:98%">
                        <asp:Label ID="lblregError" runat="server" Text="" ForeColor="Red"></asp:Label>
            				</div>
                                	<div style="float:left; width:98%">
                                <asp:TextBox ID="txtFName" runat="server" placeholder="First Name"  Height="35px" class="form-control"></asp:TextBox> 
            				</div>
                                <div  style="float:left; width:2%">
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtFName" ValidationGroup="Register" ForeColor="Red" ></asp:RequiredFieldValidator>   
                                </div>   
                                     	<div style="float:left; width:98%">
                                <asp:TextBox ID="txtLName" runat="server" placeholder="Last Name"  Height="35px" class="form-control"></asp:TextBox> 
            				</div>
                                <div  style="float:left; width:2%">
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ControlToValidate="txtLName" ValidationGroup="Register" ForeColor="Red" ></asp:RequiredFieldValidator>   
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
                               <div style="float:left; width:18%;   margin-top: 15px;">
                                   <span>D.O.B</span>
                                   </div>
                            <div style="float:left; width:25%">
                                <asp:DropDownList ID="drpDay" runat="server" Height="35px" class="form-control"></asp:DropDownList>
                            </div>
                                <div style="float:left; width:2%">
                            
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ControlToValidate="drpDay" ValidationGroup="Register" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    
                                  </div>
                            <div style="float:left; width:25%">
                                <asp:DropDownList ID="drpMonth" runat="server" Height="35px" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpMonth_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                                <div style="float:left; width:2%">
                            
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ControlToValidate="drpMonth" ValidationGroup="Register" ForeColor="Red" InitialValue="0" ></asp:RequiredFieldValidator>
                    
                                  </div>
                            <div style="float:left; width:25%">
                                <asp:DropDownList ID="drpYear" runat="server" Height="35px" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                                <div style="float:left; width:2%"">
                            
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" ControlToValidate="drpYear" ValidationGroup="Register" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    
                                  </div>
                            <div style="float:left; width:43%; ">
                                     <asp:DropDownList ID="drpCountry" runat="server" Height="35px" class="form-control"></asp:DropDownList>
                                   </div>
                    <div style="float:left; width:2%"">
                            
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="drpCountry" ValidationGroup="Register" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                    
                                  </div>
                             <div style="float:left; width:25%">
                                <asp:DropDownList ID="drpGender" runat="server" Height="35px" class="form-control">
                                    <asp:ListItem Value="G">Gender</asp:ListItem>
                                    <asp:ListItem Value="M">Male</asp:ListItem>
                                    <asp:ListItem Value="F">Female</asp:ListItem>
                                 </asp:DropDownList>
                            </div>
                                <div style="float:left; width:2%">
                            
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" ControlToValidate="drpGender" ValidationGroup="Register" ForeColor="Red" InitialValue="G"></asp:RequiredFieldValidator>
                    
                                  </div>
                                 <div style="float:right; width:25%;  margin-top: 10px;">
                                   <asp:Button ID="btnRegister" runat="server" Text="Register" class="btn btn-green btn-sm" ValidationGroup="Register"  Font-Size="14px" OnClick="btnRegister_Click"/>
                                     </div>
                                <br />
                                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                             
                        </div>
                </div>
           </div>
   

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
               </form>
    </div>
             <uc1:footer ID="footer" runat="server" />
</body>
</html>
    <!------------------------ SCRIPTS --------------------------------->
    <script src='./js/jquery.min.js'></script>
    <script src='./js/bootstrap.min.js'></script>

    <!------------------------ / SCRIPTS --------------------------------->

 