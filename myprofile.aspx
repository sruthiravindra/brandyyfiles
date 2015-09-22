<%@ Page Language="C#" AutoEventWireup="true" CodeFile="myprofile.aspx.cs" Inherits="_myprofile" %>
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
                        <a href='<%=SessionState.WebsiteURL %>logout.aspx'>
                <div class="menu-icons-user"></div>
                <span>Logout</span>
              </a>
            </li>
            <li role='menuitem'>
              <a href='#'>
                <div class="menu-icons-useradd"></div>
                <span><%=SessionState._SignInUser.reg_fname +" " + SessionState._SignInUser.reg_lname%></span>
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
                   <div class="step2-left">
                       <img  src="<%=SessionState.WebsiteURL %>img/step2.png"/><br />
                       <%--<span>2nd Step - Aenean ac pulvinar magna</span>--%>
                       <p>Integer placerat, nulla porttitor facilisis malesuada, velit nibh finibus lacus, in sollicitudin justo augue nec lorem. Mauris scelerisque mauris sed pulvinar auctor. Aenean.
                           Integer placerat, nulla porttitor facilisis malesuada, velit nibh finibus lacus, in sollicitudin justo augue nec lorem. Mauris scelerisque mauris sed pulvinar auctor. Aenean.
                           Integer placerat, nulla porttitor facilisis malesuada, velit nibh finibus lacus.
                       </p>
                   </div>
                   <div  class="step2-right">
                   <div class="prof-img">
      <img alt="" src='<%=SessionState._SignInUser.profileurl %>'> <br />
                       <span><%=SessionState._SignInUser.reg_fname +" " + SessionState._SignInUser.reg_lname%></span>
                   <div class="social-hidden" >
                  <asp:Repeater ID="rpSocialProf" runat="server" OnItemCommand="rpSocialProf_ItemCommand">
                        <ItemTemplate>
                      <asp:LinkButton ID="btn_SyncFBTw" runat="server"  CommandArgument='<%#Eval("id") %>'
                          style="cursor:pointer"
                           Visible='<%#( Convert.ToInt16(Eval("registered")) == 0 ) ? false : true %>'>&nbsp; <img src="<%=SessionState.WebsiteURLAdmin %>images/socialmedia/<%#Eval("id")%>.png" style="width:30px;" /></asp:LinkButton>                   
                      </ItemTemplate>
                    </asp:Repeater>       
                       </div>        
                           </div>           
                <div style="clear:both"></div>
                   <div class="social-wrap">
                     
<%--                       <div class="border-b"><span></span></div>--%>
                       <div  class="social-wrap-box">
                             <h3>Sync your social media accounts</h3>
                <asp:Repeater ID="RepTab" runat="server">
                        <ItemTemplate>
                            <ul>
                                <li>
                                    
                                        <div class="span<%#Eval("id")%>" style="width: 200px;
    height: 200px; border-radius:5%;">
      <div class="reveal" >
          <div style="clear:both"></div>
          <div class="imgbox">
  <img src="<%=SessionState.WebsiteURL %>images/social/<%#Eval("social")%>.png" alt=""/>
          </div>
          <span ><%#Convert.ToString(Eval("social")) %></span>
        <div class="hidden">
            <div class="caption">
                <div class="centered">
                    <h4 style="color:#fff">  <div class="registrationstatus <%#( Convert.ToInt16(Eval("registered")) == 0 ) ? "unregistered" : "registered" %>"><%#( Convert.ToInt16(Eval("registered")) == 0 ) ? "Register Now" : "Registered" %></div>   </h4>
                            <div class="<%#( Convert.ToInt16(Eval("registered")) == 0 ) ? "" : "" %>" > 
                                <%#Eval("name")%>
                                <div id="Div2" class="socialbtn twitter_login" runat="server" visible='<%#( ( Convert.ToByte(Eval("id")) == 2)) ? true : false %>'  >
                                  <a href='<%=SessionState.WebsiteURL%>twitter.aspx'>
                                        <img src="images/<%#( ( Convert.ToByte(Eval("id")) == 2) && (Convert.ToBoolean(Eval("registered")) == false)) ? "activatetwitter.png" : "refreshtwitter.png" %>"  alt="Sync Twitter" style="width:157px;height:25px;margin-top:0px;"/>
                                  </a>                                                                  
                            </div>     
                               </div>  
                </div>
            </div>
        </div>
      </div>
  </div>
                                </li>
                            </ul>
                                     
                        </ItemTemplate>
                </asp:Repeater>    
                           </div>                        
                           </div>
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
<script src="<%=SessionState.WebsiteURL+ "custom-js/activate_fb.js"%>"></script>

    <!------------------------ / SCRIPTS --------------------------------->

 