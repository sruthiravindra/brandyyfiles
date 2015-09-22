<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Src="footer.ascx" TagName="footer" TagPrefix="uc1" %>
<!DOCTYPE html>

<html class="no-js" lang="en">
<head>
    <meta charset="utf-8">
    <title>Brandyy | The Brandy Store.</title>
    <meta name="HandheldFriendly" content="true" />
    <meta name="MobileOptimized" content="320" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0">
    <meta itemprop="name" content="Brandyy" />
    <meta itemprop="description" content="The Brandy Store." />
    <meta itemprop="author" content="Brandyy, http://brandyy.com" />
    <meta itemprop="copyright" content="Brandyy (c) 2015" />
    <!-- Add to homescreen for Chrome on Android -->
    <meta name="mobile-web-app-capable" content="yes">
    <link rel="icon" sizes="192x192" href="img/brand/touch/chrome-touch-icon-192x192.png">
    <!-- Add to homescreen for Safari on iOS -->
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="apple-mobile-web-app-title" content="Brandyy">
    <link rel="apple-touch-icon-precomposed" href="img/brand/apple-touch-icon-precomposed.png">
    <!-- Tile icon for Win8 (144x144 + tile color) -->
    <meta name="msapplication-TileImage" content="img/brand/touch/ms-touch-icon-144x144-precomposed.png">
    <meta name="msapplication-TileColor" content="#3372DF">
    <!-- SEO: If your mobile URL is different from the desktop URL, add a canonical link to the desktop page https://developers.google.com/webmasters/smartphone-sites/feature-phones -->
    <!--
<link rel="canonical" href="http://www.example.com/">
-->
    <link rel="icon" href="img/favicon.png" type="image/x-icon">
    <!-- For third-generation iPad with high-resolution Retina display: -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="img/brand/favicon_swasa_144px.png">
    <!-- For iPhone with high-resolution Retina display: -->
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="img/brand/favicon_swasa_114px.png">
    <!-- For first- and second-generation iPad: -->
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="img/brand/favicon_swasa_72px.png">
    <!-- For non-Retina iPhone, iPod Touch, and Android 2.1+ devices: -->
    <link rel="apple-touch-icon-precomposed" href="img/brand/favicon_swasa_57px.png">
    <!------------------------ STYLES --------------------------------->
    <link href='css/owl.carousel.css' rel='stylesheet' />
    <link href='css/owl.theme.css' rel='stylesheet' />
    <link href='css/slider-pro.min.css' rel='stylesheet' />
    <link href='css/bootstrap.css' rel='stylesheet' />
    <link href='css/animate.min.css' rel='stylesheet' />
    <link href='css/sprites.css' rel='stylesheet' />
    <link href='css/style.css' rel='stylesheet' />
    <link href='css/responsive-utils.min.css' rel='stylesheet' />
    <!------------------------ / STYLES --------------------------------->
    <script src="js/modernizr.min.js"></script>
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
<script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
<script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
<![endif]-->
  </head>
  <body role="document" class="homepage_secondary" itemscope="itemscope"
  itemtype="http://schema.org/WebPage">
    <div class="site-container" id="site-container">
      <header role="banner" class="site_header secondary_header" id="site_header">
        <div class="container-fluid">
          <div class="site_logo">
              <a href='<%=SessionState.WebsiteURL %>'>
            <img src="img/logo-2.png" alt="logo" />
                  </a>
          </div>
          <ul class='site_nav inversed sm-visible' role="menu"> <!--Desktop menu-->
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
             <% if(SessionState._SignInUser == null) { %>
            <li role='menuitem'>
              <a href='<%=SessionState.WebsiteURL%>login.aspx'>
                <div class="menu-icons-user"></div>
                <span>Login</span>
              </a>
            </li>
            <li role='menuitem'>
                   <a href='<%=SessionState.WebsiteURL%>register.aspx'>
                <div class="menu-icons-useradd"></div>
                <span>Register</span>
              </a>
            </li>
              <% } 
                 else
                 { %>
            <li role='menuitem'>
              <a href='<%=SessionState.WebsiteURL%>logout.aspx'>
                <div class="menu-icons-user"></div>
                <span>Logout</span>
              </a>
            </li>
            <li role='menuitem'>
                   <a href="#">
                <div class="menu-icons-useradd"></div>
                <span><%=SessionState._SignInUser.reg_fname +" " + SessionState._SignInUser.reg_lname %></span>
              </a>
            </li>
              <% } 
                 %>
     

          </ul>
        </div>
      </header>

      <main role="main">
        <section class="home_banner banner_secondary" id="home_banner">
          <img src="img/banner.jpg" class="img-responsive banner_img">
        </section>
     <!--     Mobile view scrool Menu-->
                <header role="banner" class="site_header fixed sm-hide hide" id="site_header_mobile">
        <div class="container-fluid">
          <div class="site_logo">
                <a href='<%=SessionState.WebsiteURL %>'>
            <img src="img/logo-2.png" class="ios4-visible" alt="logo" />
            <img src="img/logo-2_small.png" class="ios4-hide" alt="logo" />
                    </a>
          </div>
          <ul class='site_nav inversed collapse' role="menu" id="menu_collapse">
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
              <% if(SessionState._SignInUser==null) { %>
            <li role='menuitem'>
                    <a href='<%=SessionState.WebsiteURL%>login.aspx'>
                <div class="menu-icons-user"></div>
                <span>Login</span>
              </a>
            </li>
            <li role='menuitem'>
                    <a href='<%=SessionState.WebsiteURL%>register.aspx'>
                <div class="menu-icons-useradd"></div>
                <span>Register</span>
              </a>
            </li>
<% }
   else
    { %>
            <li role='menuitem'>
                    <a href='<%=SessionState.WebsiteURL%>logout.aspx'>
                <div class="menu-icons-user"></div>
                <span>Logout</span>
              </a>
            </li>
            <li role='menuitem'>
                    <a href='#'>
                <div class="menu-icons-useradd"></div>
                <span><%=SessionState._SignInUser.reg_fname +" " + SessionState._SignInUser.reg_lname %></span>
              </a>
            </li>
<% }
    %>
            
          </ul>
          <button type="button" id="menu_toggler" class="btn btn-default md-hide"
          data-toggle="collapse" href="#menu_collapse" aria-expanded="false"
          aria-controls="menu_collapse">
            <span class="glyphicon glyphicon glyphicon-menu-hamburger" aria-hidden="true"></span>
          </button>
        </div>
      </header>
    
         <!-- Mobile View Menu-->
            <div class="container-fluid"> 
                <ul class='mobile_nav sm-hide' role="menu">
                    <% if(SessionState._SignInUser==null) { %>
 <li role='menuitem'>
                 <a href='<%=SessionState.WebsiteURL%>login.aspx'>
                <div class="menu-icons-user user-hover"></div>
                <span style="font-size:20px;">Login</span>
              </a>
            </li>
            <li role='menuitem'>
                    <a href='<%=SessionState.WebsiteURL%>register.aspx'>
                <div class="menu-icons-useradd useradd-hover"></div>
                <span  style="font-size:20px;">Register</span>
              </a>
            </li>
<% }
   else
   { %>
 <li role='menuitem'>
                 <a href='<%=SessionState.WebsiteURL%>logout.aspx'>
                <div class="menu-icons-user user-hover"></div>
                <span style="font-size:20px;">Logout</span>
              </a>
            </li>
            <li role='menuitem'>
                    <a href='#'>
                <div class="menu-icons-useradd useradd-hover"></div>
                <span  style="font-size:20px;"><%=SessionState._SignInUser.reg_fname %></span>
              </a>
            </li>
<% }  %>
                
               <li role='menuitem'>
              <a href='#'>
                <div class="menu-icons-earn earn-hover"></div>
                <span  style="font-size:20px;">Earn</span>
              </a>
            </li>
           <li role='menuitem'>
              <a href='#'>
                <div class="menu-icons-shop shop-hover"></div>
                <span  style="font-size:20px;">Shop</span>
              </a>
            </li>
          </ul>
                </div>

          <div class="mob-works_action" style="text-align:center"> <!--View button for mobile-->
              <ul>
                  <li>
                           <div class="btn btn-success radius-lg btn-lg" style="padding: 5px;
  font-size: 16px;border-radius:0%; width:35% ">
          <a type="button"  href="#"  style="color:#fff">
            Faqs
        </a>
                       </div>
                        <div class="btn btn-success radius-lg btn-lg" style="padding: 5px;
  font-size: 16px;border-radius:0%; width:62%">
          <a type="button"  href="#legend_1"  class="scrooldown">
            See how it works
        </a>
                       </div>
                  </li>
              </ul>
          </div>
               <div class="works_action" ><!-- View button for desktop-->
                   <div class="btn btn-success radius-lg btn-lg" style="  border-radius: 9em;  padding: 5px 8px;
  font-size: 16px; ">
          <a type="button"  href="#legend_1" class="scrooldown"
          id="scroll-page">
            See how it works
        </a>
                       </div>
        </div>
        <header class="section_header legend-1" id="legend_1">
          <div class="container-fluid">
            <div class="animations">
              <h1>1st Step - Aenean ac pulvinar magna</h1>
              <p>Integer placerat, nulla porttitor facilisis malesuada, velit
                nibh finibus lacus, in sollicitudin justo augue nec lorem.
                Mauris scelerisque mauris sed pulvinar auctor. Aenean ac
                pulvinar magna? Maecenas pretium erat nunc, id ornare enim
                efficitur non? Mauris scelerisque, elit eu rhoncus congue,
                urna felis placerat nisl, sit amet finibus diam lorem in
                orci.</p>
            </div>
          </div>
        </header>
        <section class="section_img legend-1"></section>
        <header class="section_header legend-2">
          <div class="container-fluid">
            <div class="animations">
              <h1>2nd Step - Aenean ac pulvinar magna</h1>
              <p>Integer placerat, nulla porttitor facilisis malesuada, velit
                nibh finibus lacus, in sollicitudin justo augue nec lorem.
                Mauris scelerisque mauris sed pulvinar auctor. Aenean ac
                pulvinar magna? Maecenas pretium erat nunc, id ornare enim
                efficitur non? Mauris scelerisque, elit eu rhoncus congue,
                urna felis placerat nisl, sit amet finibus diam lorem in
                orci.</p>
            </div>
          </div>
        </header>
        <section class="section_img legend-2"></section>
        <header class="section_header legend-3">
          <div class="container-fluid">
            <div class="animations">
              <h1>3rd Step - Aenean ac pulvinar magna</h1>
              <p>Integer placerat, nulla porttitor facilisis malesuada, velit
                nibh finibus lacus, in sollicitudin justo augue nec lorem.
                Mauris scelerisque mauris sed pulvinar auctor. Aenean ac
                pulvinar magna? Maecenas pretium erat nunc, id ornare enim
                efficitur non? Mauris scelerisque, elit eu rhoncus congue,
                urna felis placerat nisl, sit amet finibus diam lorem in
                orci.</p>
            </div>
          </div>
        </header>
        <section class="section_img legend-3"></section>
        <section class="latest_offers">
          <div class="container">
            <header class="section_title">
              <img src="img/badge.png" class="centered" alt="badge" />
              <h2>Our Latest Offers</h2>
            </header>
            <div id="brands_carousel" class="carousel slide" data-ride="carousel">
              <div class="container-fluid">
                <div class="owl_brands_carousel">
                  <div id="owl-demo" class="owl-carousel">
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/brand-1.png" class="img-responsive centered" alt="brand"
                          />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/brand-2.png" class="img-responsive centered" alt="brand"
                          />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/brand-3.png" class="img-responsive centered" alt="brand"
                          />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/brand-4.png" class="img-responsive centered" alt="brand"
                          />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/brand-5.png" class="img-responsive centered" alt="brand"
                          />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/brand-6.png" class="img-responsive centered" alt="brand"
                          />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/brand-1.png" class="img-responsive centered" alt="brand"
                          />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/brand-2.png" class="img-responsive centered" alt="brand"
                          />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/brand-3.png" class="img-responsive centered" alt="brand"
                          />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/brand-4.png" class="img-responsive centered" alt="brand"
                          />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/brand-5.png" class="img-responsive centered" alt="brand"
                          />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/brand-6.png" class="img-responsive centered" alt="brand"
                          />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </section>
        <section class="latest_products">
          <div class="container">
            <header class="section_title">
              <img src="img/labels.png" class="centered" alt="badge" />
              <h2>Our Latest Products</h2>
            </header>
            <div id="products_carousel" class="carousel slide" data-ride="carousel">
              <div class="container-fluid">
                <div class="owl_brands_carousel">
                  <div id="owl-demo-2" class="owl-carousel">
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/item-1.png" class="img-responsive centered" alt="brand" />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/item-2.png" class="img-responsive centered" alt="brand" />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/item-3.png" class="img-responsive centered" alt="brand" />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/item-4.png" class="img-responsive centered" alt="brand" />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/item-5.png" class="img-responsive centered" alt="brand" />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/item-6.png" class="img-responsive centered" alt="brand" />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/item-1.png" class="img-responsive centered" alt="brand" />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/item-2.png" class="img-responsive centered" alt="brand" />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/item-3.png" class="img-responsive centered" alt="brand" />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/item-4.png" class="img-responsive centered" alt="brand" />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/item-5.png" class="img-responsive centered" alt="brand" />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                    <div class="item">
                      <a href="#" class="item-anchor">
                        <figure>
                          <img src="img/item-6.png" class="img-responsive centered" alt="brand" />
                        </figure>
                        <p class="brand_points">51</p>
                        <p class="brand_price">$27.99</p>
                      </a>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </section>
        <section class="user_subscribe">
          <div class="container">
            <header class="section_title">
              <img src="img/add_user.png" class="centered" alt="badge" />
              <h2>Sign up today to earn BRANDYY POINTS
                <span class="block">for
                  <span class="text-primary">free rewards!</span>
                </span>
              </h2>
            </header>
            <div class="actions">
              <button type="button" class="btn btn-primary btn-lg">
                Signup Now!
              </button>
              <button type="button" class="btn btn-default btn-lg">
                OR
              </button>
              <button type="button" class="btn btn-success btn-lg">
                Member Login
              </button>
            </div>
          </div>
        </section>
      </main>
      <div class="scroll_top_section">
        <div class="scroll_top_holder">
          <a href="#site_header" class="scroll_top_button" id="scroll-page1"></a>
        </div>
      </div>
      <div class="floater">
<!--        <p>
          Follow
          <span class="block"> Us on:</span>
        </p>-->
        <a href="#" class="fb_float">
          <img src="images/Facebook.png" alt="fb" />
        </a>
        <a href="#">
          <img src="images/Twitter.png" alt="twitter" />
        </a>
           <a href="#">
          <img src="images/Instagram.png" alt="twitter" />
        </a>
      </div>
    </div>
      <uc1:footer ID="footer" runat="server" />
    <!------------------------ SCRIPTS --------------------------------->
    <script src='js/jquery.min.js'></script>
    <script src='js/bootstrap.min.js'></script>
    <script src='js/vendors/owl.carousel.min.js'></script>
    <script src='js/vendors/jquery.nicescroll.min.js'></script>
    <script src='js/vendors/jQuery.resizeEnd.min.js'></script>
    <script src='js/vendors/jquery.sliderPro.min.js'></script>
    <script src='js/vendors/TweenMax.min.js'></script>
    <script src='js/vendors/ScrollMagic.min.js'></script>
    <script src='js/vendors/jquery.ScrollMagic.min.js'></script>
    <script src='js/vendors/animation.gsap.min.js'></script>
    <script src='js/vendors/debug.addIndicators.min.js'></script>
    <script src='js/script.js'></script>
    <!------------------------ / SCRIPTS --------------------------------->
  </body>


</html>