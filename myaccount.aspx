<%@ Page Language="C#" AutoEventWireup="true" CodeFile="myaccount.aspx.cs" Inherits="myaccount" %>
<%@ Register Src="footer.ascx" TagName="footer" TagPrefix="uc1" %>

<!DOCTYPE html>
<!--[if lt IE 7]>      <html class="no-js lt-ie9 lt-ie8 lt-ie7"> <![endif]-->
<!--[if IE 7]>         <html class="no-js lt-ie9 lt-ie8"> <![endif]-->
<!--[if IE 8]>         <html class="no-js lt-ie9"> <![endif]-->
<!--[if gt IE 8]><!-->
<!-- saved from url=(0014)about:internet -->
<html class="no-js">
<!--<![endif]-->
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
          <link href='./css/activities.css' rel='stylesheet' />
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
       <%--          start  Activity--%>
                   <div class="landingpage">
                       <div class="landing-wrap">
                                 <div class="profileheader">
                                     <ul>
                                         <li>
                                              <img alt="" src="images/profile.jpg" id="ProfileImg" runat="server" class="profilepic" />
                                         </li>
                                         <li>
                                             <span>Recent Campaign Activities</span>
                                         </li>
                                         <li>
                                                <asp:ImageButton ID="imgRefresh" runat="server"   ImageUrl="~/images/refresh.png" OnClick="ImageButton1_Click" style="height: 16px" />
                                         </li>
                                         <li>
                                                  <asp:Label ID="lblLoading" runat="server" Text=""></asp:Label>
                      <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div  id="showloder" runat="server">
                            Loading.. <img  id="loader" height=" " border="0" alt="Loading..." src='<%=SessionState.WebsiteURL+"images/loading_16.png"%>' style="margin-bottom: 20px;">
                        </div>  
                    </ProgressTemplate>
                </asp:UpdateProgress>
                                         </li>
                                     </ul>

              </div>
<div class="acitivity">
                               <asp:Repeater ID="RepTab" runat="server" OnItemCommand="RepTab_ItemCommand" >                      
                        <ItemTemplate>
                           <div class="activity-social">
                                  <img src='<%=SessionState.WebsiteURL%>images/social/<%#Eval("user_display_desc")%>.png' style="width:50px;" />
                               <div style="clear:both"></div>
                           </div>
                            <div class="acitivity-div">
                            <div class="acitivity-info">
                               <div class='<%# Convert.ToString(Eval("activity_text")) != ""  || Convert.ToString(Eval("acitivity_img")) != "" ? "acitivity-left" : "acitivity-left-only" %>'>
                                   <ul>
                                       <li>
<h3>You <%#Eval("campaign_type_desc")%></h3>
                                       </li>
                                       <li>
<span>on <%# Convert.ToDateTime(Eval("created_on")).ToString("MMMM dd, yyyy  hh:mm tt") %></span>
                                       </li>
                                   </ul>
                                       <a href='<%#Eval("action_url")%>'>
                                           <div class='<%# Convert.ToString(Eval("activity_text")) != ""  || Convert.ToString(Eval("acitivity_img")) != "" ? "activity-action" : "activity-action-hideborder" %>'>
                                               <div>
                                         <div class='<%# Convert.ToString(Eval("acitivity_img")) != "" ? "activity-action-imgdisplay" : "activity-action-imghide" %>'">
 <img src= '<%#Eval("acitivity_img")%>'  class='<%# Convert.ToString(Eval("activity_text")) != "" ? "activity-action-imgleft" : "activity-action-imgcenter" %>'/>
</div>
                                                   
                                                       <p class='<%# Convert.ToString(Eval("acitivity_img")) != "" ? "activity-action-text" : "" %>'><%#Eval("activity_text")%></p>
                                                       </div>
                                                 <div style="clear:both"></div> 
                                              </div>
                                                  </a> 
                                   </div>
                                <div class="acitivity-right">
                                    <ul>
                                            <li >
                                             <img src="<%=SessionState.WebsiteURLBrand %>uploads/logos/<%#Eval("brand_id")%>.png"  style="width:100px;" />
                                        </li>
                                        <li>
                <img src='<%=SessionState.WebsiteURL%>img/points.png' style="width:30px;margin-top: -20px;" /><span>Brandyy Points : <%# Convert.ToInt64(Eval("reward_amount")) %> </span>
                                            
                                        </li>
                                        <li>
                                            <div class=" verification_status_<%#Convert.ToString( Eval("reward_status"))%> statuswrap">
                                             <span style="color:#fff" > <%#(( Convert.ToString( Eval("verification_status_str") )=="Verified")? Eval("reward_status_str"): Eval("verification_status_str"))%></span></div>
                                                 <span style="color:red;font-weight: bold;font-size:10px;"><%#Eval("verification_log") %> </span><br />
                                        </li>
                                    
                                    </ul>

                                           
                                <%--           <div class="size">
                  <img src="<%=SessionState.WebsiteURL %>img/down.gif"  class="field"/>
	<ul class="list">
		<li style="display:<%#(( Convert.ToDecimal( Eval("reward_per_user") )==0)?"none":"")%>"><span>Points (Per User) :</span> <%# Convert.ToInt64(Eval("reward_per_user")) %></li>
		<li style="display:<%#(( Convert.ToDecimal( Eval("reward_on_friend") )==0)?"none":"")%>"><span >Points (Per no. of friends) :</span> <%#Eval("no_of_friends") %>x<%#Convert.ToInt64(Eval("reward_on_friend")) %></li>
		<li style="display:<%#(( Convert.ToDecimal( Eval("reward_on_likes") )==0)?"none":"")%>"><span>Points (Per no. of likes) :</span> <%#Eval("no_of_likes") %>x<%#Convert.ToInt64(Eval("reward_on_likes")) %></li>
		<li style="display:<%#(( Convert.ToDecimal( Eval("reward_on_shares") )==0)?"none":"")%>"><span>Points (Per no. of shares) : </span><%#Eval("no_of_shares") %>x<%#Convert.ToInt64(Eval("reward_on_shares")) %></li>
	</ul>
                                                                  </div>--%>
  
              </div>                                               
                                                   
                                </div>   
                                <div style="clear:both"></div>     
                                </div> 
                    </ItemTemplate>
                 </asp:Repeater> 
    </div>
                                <div class="activity-offers">
                                 <div class="activity-TotalReward">
                                     <span>Total Brandyy Points</span>
                                     <h1>120</h1>
                                 </div>
                                    <div style="clear:both"></div>
                                       <div class="activity-offerlist">
                                     <span>Offers you may Interested In</span>
                                         <div>
                                             <ul >
                                                 <li style="float:left;width:80px;">
                                                      <img src="<%=SessionState.WebsiteURL %>img/brand-1.png"  style="width:80px;" />
                                                 </li>
                                               <li style="float:right;width:150px;">
                                                   Add a tweet on Twitter and get 100 Brandyy Points
                                               </li>

                                             </ul>
                                               <ul >
                                                 <li style="float:left;width:80px;">
                                                        <img src="<%=SessionState.WebsiteURL %>img/brand-2.png"  style="width:80px;" />
                                                 </li>
                                               <li style="float:right;width:150px;">
                                                   Add a Post on Facebook and get Athletic and Fashion Sneaker
                                               </li>

                                             </ul>
                                             <ul >
                                                 <li style="float:left;width:80px;">
                                                         <img src="<%=SessionState.WebsiteURL %>img/brand-3.png"  style="width:80px;" />
                                                 </li>
                                               <li style="float:right;width:150px;">
                                                   Add a tweet on Twitter and get 100 Brandyy Points
                                               </li>

                                             </ul>
                                               <ul >
                                                 <li style="float:left;width:80px;">
                                                        <img src="<%=SessionState.WebsiteURL %>img/brand-4.png"  style="width:80px;" />
                                                 </li>
                                               <li style="float:right;width:150px;">
                                                   Add a Post on Facebook and get Athletic and Fashion Sneaker
                                               </li>

                                             </ul>
                                              </ul>
                                               <ul >
                                                 <li style="float:left;width:80px;">
                                                        <img src="<%=SessionState.WebsiteURL %>img/brand-5.png"  style="width:80px;" />
                                                 </li>
                                               <li style="float:right;width:150px;">
                                                   Add a Post on Facebook and get Athletic and Fashion Sneaker
                                               </li>

                                             </ul>
                                              </ul>
                                               <ul >
                                                 <li style="float:left;width:80px;">
                                                        <img src="<%=SessionState.WebsiteURL %>img/brand-6.png"  style="width:80px;" />
                                                 </li>
                                               <li style="float:right;width:150px;">
                                                   Add a Post on Facebook and get Athletic and Fashion Sneaker
                                               </li>

                                             </ul>
                                         </div>
                                 </div>
                           </div>
  </div>
                   </div>
<%--  End Acitivity--%> 
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
<style type="text/css">
    .verification_status_0 {
        background:#0060b6;color:white;
    }
    .verification_status_1 {
        background:#31b03a;color:white
    }
    .verification_status_2 {
        background:#ff4e00;color:white        
    }
    .verification_status_3 {
        background:red;color:white
    }
</style>   

<script>
    (function ($) {
        $.fn.styleddropdown = function () {
            return this.each(function () {
                var obj = $(this)
                obj.find('.field').click(function () { //onclick event, 'list' fadein
                    obj.find('.list').fadeToggle();
                    $(document).keyup(function (event) { //keypress event, fadeout on 'escape'
                        if (event.keyCode == 27) {
                            obj.find('.list').fadeOut(400);
                        }
                    });

                    obj.find('.list').hover(function () { },
                        function () {
                            $(this).fadeOut(400);
                        });
                });

                obj.find('.list li').click(function () { //onclick event, change field value with selected 'list' item and fadeout 'list'
                    obj.find('.field')
                        .val($(this).html())
                        .css({
                            'background': '#fff',
                            'color': '#333'
                        });
                    obj.find('.list').fadeOut(400);
                });
            });
        };
    })(jQuery);

    $(function () {
        $('.size').styleddropdown();
    });
    </script>