using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using IchooseIT.DAL;
using oAuthExample;



public partial class _myprofile : System.Web.UI.Page
{
    ConnectionClass ConnObj = null;

    #region First and last calling
    ProjectInitUnloadCalling _ProjectInitUnloadCalling = new ProjectInitUnloadCalling();
    protected void Page_Init(Object sender, EventArgs e)
    {
        _ProjectInitUnloadCalling.Page_Init();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        _ProjectInitUnloadCalling.Page_Unload();
        ReleaseInstance();
    }
    private void ReleaseInstance()
    {
        if (ConnObj != null)
        {
            ConnObj.ReleaseConnection();
        }

    }
    private void CreateInstance()
    {
        if (ConnObj == null)
        {
            ConnObj = new ConnectionClass();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        CreateInstance();
        if ((!Page.IsPostBack) && (SessionState._SignInUser != null))
        {
            FirstPos();
        }
        else if (SessionState._SignInUser == null)
        {
            Response.Redirect(SessionState.WebsiteURL + "login.aspx");
        }      
    }
    #endregion


    protected void FirstPos()
    {
        FillSMDetails();  

    }


    private void FillSMDetails()
    {
        SqlCommand cmd = new SqlCommand("sp_brandyy_GetUserSocialMedias");
        cmd.Parameters.AddWithValue("@reg_uid", SessionState._SignInUser.reg_uid);
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            rpSocialProf.DataSource = ConnObj.DataSet.Tables[0];
            rpSocialProf.DataBind();

            foreach (DataRow dr in ConnObj.DataSet.Tables[0].Rows)
            {
                if (Convert.ToString(dr["registered"]) == "0")
                {
                    if (Convert.ToString(dr["id"]) == "1")
                    {
                        dr["name"] = "<div class=\"reveal socialbtnFB\" ><div  id=\"facebookregisterbtn\">" +
                "<fb:login-button length=\"long\" size=\"large\" scope=\"public_profile,email,user_friends,user_about_me,user_actions.books,user_actions.fitness,user_actions.music, "
                + "user_actions.news,user_actions.video,user_activities,user_birthday,user_education_history,user_events,user_games_activity,user_groups,"
                            + "user_hometown,user_interests,user_interests,user_location,user_photos,user_relationships,user_relationship_details,"
                            + "user_religion_politics,user_status,user_tagged_places,user_videos,user_website,user_website,user_about_me,"
                            + "user_actions.books,user_activities,user_events,user_games_activity,user_groups,user_likes\" onlogin=\"checkLoginState();\">Activate Facebook" +
                "</fb:login-button>" +
                "<div id=\"status\"></div>" +
                "</div>" +
                "<div id=\"facebooklikebtn\" style=\"display:none\">" +
                "<div class=\"fb-like\" data-href=\"https://www.facebook.com/cooseit\" data-layout=\"box_count\" data-action=\"like\" " +
                "data-show-faces=\"false\" data-share=\"false\"></div>" +
                "</div>" +
                "</div>";
                    }
                    else if (Convert.ToString(dr["id"]) == "2")
                    {
                        //dr["name"] = "<div class=\"socialbtn twitter_login\" >"
                        //    +"<asp:ImageButton ID=\"imgTwitterLogin\" runat=\"server\" ImageUrl=\"~/images/loginwithtwitter.png\""
                        //    +" CausesValidation=\"false\" Height=\"25px\" Width=\"157px\" OnClick=\"imgTwitterLogin_Click\"/>"
                        //    +"</div>";
                        dr["name"] = "<div class=\"socialbtn twitter_login\" >"
                            + "<a href='twitter.aspx'  style='cursor:pointer'>Activate Twitter</a>"
                            //+ "<a href='#' onclick='ActivateTwitter()' style='cursor:pointer'><img src='" + SessionState.WebsiteURL + "images/activatetwitter.png' style=\"height:25px;width:157px;\"/></a>"
                            + "</div>";
                        dr["name"] = "";
                    }
                    else if (Convert.ToString(dr["id"]) == "3")
                    {
                        var client_id = System.Configuration.ConfigurationManager.AppSettings["instagram.clientid"].ToString();
                        var redirect_uri = System.Configuration.ConfigurationManager.AppSettings["instagram.redirecturi"].ToString();
                        dr["name"] = "<div>"
                            + "<a href='" + "https://api.instagram.com/oauth/authorize/?client_id=" + client_id + "&redirect_uri=" + redirect_uri + "&response_type=code" + "' style='cursor:pointer'><img src='" + SessionState.WebsiteURL + "images/activateinsta.png' style=\"height:35px;width:157px;margin-top:0px;\"/></a>"
                            + "</div>";
                    }
                    else if (Convert.ToString(dr["id"]) == "4")
                    {
                        //var client_id = System.Configuration.ConfigurationManager.AppSettings["instagram.clientid"].ToString();
                        //var redirect_uri = System.Configuration.ConfigurationManager.AppSettings["instagram.redirecturi"].ToString();
                        //dr["name"] = "<div style=\"vertical-align:middle\">Activate<script src=\"https://apis.google.com/js/client:platform.js\" async defer></script>"
                        //                + "<span id=\"signinButton\">"
                        //                  + "<span "
                        //                    + "data-width=\"iconOnly\" "
                        //                    + "class=\"g-signin\" "
                        //                    + "data-callback=\"signinCallback\" "
                        //                    + "data-clientid=\"" + Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["GooglePlus.clientid"]) + "\" "
                        //                    + "data-cookiepolicy=\"single_host_origin\" "
                        //                    + "data-requestvisibleactions=\"http://schema.org/AddAction\" "
                        //                    + "data-scope=\"https://www.googleapis.com/auth/plus.profile.emails.read\">"
                        //                  + "</span>"
                        //                + "</span></div>";

                        dr["name"] = "<div >"
                            + "<a onclick='testlogin()'>Activate</a>"
                            + "</div>";
                    }
                    else
                    {
                        dr["name"] = "";
                    }
                }
                else
                {
                    dr["name"] = "";
                    if (Convert.ToString(dr["id"]) == "1")
                    {
                        dr["name"] += "<div  class=\"reveal socialbtnFB\" ><div  id=\"facebookregisterbtn\">" +
                "<fb:login-button length=\"long\" size=\"large\" scope=\"public_profile,email,user_friends,user_about_me,user_actions.books,user_actions.fitness,user_actions.music, "
                + "user_actions.news,user_actions.video,user_activities,user_birthday,user_education_history,user_events,user_games_activity,user_groups,"
                            + "user_hometown,user_interests,user_interests,user_location,user_photos,user_relationships,user_relationship_details,"
                            + "user_religion_politics,user_status,user_tagged_places,user_videos,user_website,user_website,user_about_me,"
                            + "user_actions.books,user_activities,user_events,user_games_activity,user_groups,user_likes\" onlogin=\"checkLoginState();\">Sync Facebook" +
                "</fb:login-button>" +
                "<div id=\"status\"></div>" +
                "</div>" +
                "<div id=\"facebooklikebtn\" style=\"display:none\">" +
                "<div class=\"fb-like\" data-href=\"https://www.facebook.com/cooseit\" data-layout=\"box_count\" data-action=\"like\" " +
                "data-show-faces=\"false\" data-share=\"false\"></div>" +
                "</div>" +
                "</div>";
                    }
                    else if (Convert.ToString(dr["id"]) == "2")
                    {
                        //dr["name"] = "<div class=\"socialbtn twitter_login\" >"
                        //    +"<asp:ImageButton ID=\"imgTwitterLogin\" runat=\"server\" ImageUrl=\"~/images/loginwithtwitter.png\""
                        //    +" CausesValidation=\"false\" Height=\"25px\" Width=\"157px\" OnClick=\"imgTwitterLogin_Click\"/>"
                        //    +"</div>";
                        dr["name"] += "<div class=\"socialbtn twitter_login\" >"
                            + "<a href='twitter.aspx' style='cursor:pointer'>Activate Twitter</a>"
                            //+ "<a href='#' onclick='ActivateTwitter()' style='cursor:pointer'><img src='" + SessionState.WebsiteURL + "images/activatetwitter.png' style=\"height:25px;width:157px;\"/></a>"
                            + "</div>";
                        dr["name"] = "";
                    }
                    else if (Convert.ToString(dr["id"]) == "3")
                    {
                        var client_id = System.Configuration.ConfigurationManager.AppSettings["instagram.clientid"].ToString();
                        var redirect_uri = System.Configuration.ConfigurationManager.AppSettings["instagram.redirecturi"].ToString();
                        dr["name"] = "<div>"
                            + "<a href='" + "https://api.instagram.com/oauth/authorize/?client_id=" + client_id + "&redirect_uri=" + redirect_uri + "&response_type=code" + "' style='cursor:pointer'><img src='" + SessionState.WebsiteURL + "images/syncinsta.png' style=\"height:35px;width:157px;margin-top:0px;\"/></a>"
                            + "</div>";
                    }
                    else if (Convert.ToString(dr["id"]) == "4")
                    {
                        dr["name"] = "<div >"
                            + "<a onclick='testlogin()'>Sync</a>"
                            + "</div>";
                    }
                    else
                    {
                        dr["name"] = "";
                    }
                }
            }

            RepTab.DataSource = ConnObj.DataSet.Tables[0];
            RepTab.DataBind();
        }
        else
        {
            ;
        }

    }

    protected void rpSocialProf_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Int64 sm_id = Convert.ToInt64(e.CommandArgument);
        ConnectionClass conn = new ConnectionClass();
        SqlCommand cmd = new SqlCommand("sp1_brandyy_Set_User_Photo");
        cmd.Parameters.AddWithValue("@reg_uid", SessionState._SignInUser.reg_uid);
        cmd.Parameters.AddWithValue("@sm_id", sm_id);
        cmd.Parameters.Add("@returnurl", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;
        conn.ExecuteNonQuery(cmd);
        SessionState._SignInUser.profileurl = cmd.Parameters["@returnurl"].Value.ToString();
    }

    #region Page Method
    [System.Web.Services.WebMethod(true)]
    public static FBLoginDetails ActivateFBUser(FBLoginDetails _FBLoginDetails)
    {
        if ((SessionState._SignInUser != null))
        {
            ConnectionClass conn = new ConnectionClass();
            SqlCommand cmd = new SqlCommand("sp1_brandyy_select_SelectBySmUID");
            cmd.Parameters.AddWithValue("@sm_uid", _FBLoginDetails.ID);
            cmd.Parameters.AddWithValue("@sm_id", 1);
            conn.GetDataSet(cmd);

            if (conn.IsSuccess && conn.DataSet.Tables[0].Rows.Count > 0  )
            {
                // check if the logged in user is same as the fb user registered in our database
                if (Convert.ToString(conn.DataSet.Tables[0].Rows[0]["email"]) == Convert.ToString(_FBLoginDetails.Email))
                {
                    // get user long lived access token and other profile details
                    importfbuserdetails t = new importfbuserdetails();
                    string longlivedtoken = t.getUserLongLivedAccessToken(_FBLoginDetails.AccessToken, SessionState._SignInUser.reg_uid);
                    InsertIntoRegistrationTbl_Fb(_FBLoginDetails, longlivedtoken);
                    t.getUserFriends(_FBLoginDetails.AccessToken, SessionState._SignInUser.reg_uid);
                    _FBLoginDetails.Message = "SUCCESS";
                    _FBLoginDetails.LoginSuccessRedirectHomePage = "myprofile.aspx";
                }
                else
                {
                    _FBLoginDetails.Message = "There is another user registered with the same account.<br>Please contact admin if there is a genuine breach.";
                }
            }
            else
            {

                // get user long lived access token and other profile details
                importfbuserdetails t = new importfbuserdetails();
                string longlivedtoken = t.getUserLongLivedAccessToken(_FBLoginDetails.AccessToken, SessionState._SignInUser.reg_uid);
                InsertIntoRegistrationTbl_Fb(_FBLoginDetails, longlivedtoken);
                t.getUserFriends(_FBLoginDetails.AccessToken, SessionState._SignInUser.reg_uid);
                _FBLoginDetails.Message = "SUCCESS";
                _FBLoginDetails.LoginSuccessRedirectHomePage = "myprofile.aspx";
            }
        }
        else
        {
            _FBLoginDetails.Message = "Your Login session has expired";
            _FBLoginDetails.LoginSuccessRedirectHomePage = "login.aspx";
        }

        return _FBLoginDetails; ;
    }

    #endregion

    #region insert for fb
    private static void InsertIntoRegistrationTbl_Fb(FBLoginDetails _FBLoginDetails, string accessToken)
    {
        ConnectionClass conn = new ConnectionClass();
        SqlCommand cmd1 = new SqlCommand("sp_user_update_UserProfileDetails");
        cmd1.Parameters.AddWithValue("@reg_uid", SessionState._SignInUser.reg_uid);
        cmd1.Parameters.AddWithValue("@sm_id", 1);
        cmd1.Parameters.AddWithValue("@name", _FBLoginDetails.Name);
        cmd1.Parameters.AddWithValue("@fname", _FBLoginDetails.FName);
        cmd1.Parameters.AddWithValue("@lname", _FBLoginDetails.LName);
        cmd1.Parameters.AddWithValue("@email", _FBLoginDetails.Email);
        cmd1.Parameters.AddWithValue("@gender", (_FBLoginDetails.Gender == "female") ? 'F' : 'M');
        cmd1.Parameters.AddWithValue("@profile_img_link", _FBLoginDetails.ProfileImageUrl);
        cmd1.Parameters.AddWithValue("@no_of_friends", _FBLoginDetails.NoOfFriends);
        cmd1.Parameters.AddWithValue("@no_of_likes", "0");
        cmd1.Parameters.AddWithValue("@profile_url", _FBLoginDetails.ProfileUrl);
        cmd1.Parameters.AddWithValue("@sm_uid", _FBLoginDetails.ID);
        cmd1.Parameters.AddWithValue("@token", accessToken);
        conn.ExecuteNonQuery(cmd1);
    }
    #endregion
}