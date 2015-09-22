<%@ Page Language="C#" AutoEventWireup="true" CodeFile="activatefb.aspx.cs" Inherits="brands_activatefb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
    <div class="socialbtn fb_login"> 

                         <%-- <div class="socialbtn fb_login" > --%>
                    <!--<img src="images/loginwithfb.png" alt=""/>-->
                                              <!--
                          Below we include the Login Button social plugin. This button uses
                          the JavaScript SDK to present a graphical Login button that triggers
                          the FB.login() function when clicked.
                        -->
                        <fb:login-button  length="long" size="large" scope="public_profile,email,user_friends,user_about_me,user_actions.books,user_actions.fitness,user_actions.music,
                            user_actions.news,user_actions.video,user_activities,user_birthday,user_education_history,user_events,user_games_activity,user_groups,
                            user_hometown,user_interests,user_interests,user_location,user_photos,user_relationships,user_relationship_details,
                            user_religion_politics,user_status,user_tagged_places,user_videos,user_website,user_website,user_about_me,
                            user_actions.books,user_activities,user_events,user_games_activity,user_groups,user_likes,user_posts" onlogin="checkLoginState();">Activate Facebook
                        </fb:login-button>                
                        <div id="status"></div>
            </div>
                </ContentTemplate>
            </asp:UpdatePanel>
    </form>
<script src="<%=SessionState.WebsiteURLBrand+ "custom-js/fb/activate_fb.js"%>"></script>
</body>
</html>
