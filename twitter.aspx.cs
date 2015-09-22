using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using oAuthExample;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;

public partial class twitter : System.Web.UI.Page
{
    public ConnectionClass ConnObj = null;
    string token = "";
    string verifier = "";
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
        else if (SessionState._SignInUser != null)
        {
            ;
        }
        else
        {
            Response.Redirect(SessionState.WebsiteURL);
        }

    }
    #endregion
    protected void FirstPos()
    {

        string url = "";
        string xml = "";


        oAuthTwitter oAuth = new oAuthTwitter();
        oAuth.ConsumerKey = System.Configuration.ConfigurationManager.AppSettings["consumerkey"];
        oAuth.ConsumerSecret = System.Configuration.ConfigurationManager.AppSettings["consumersecret"];
        oAuth.CallBackUrl = SessionState.WebsiteURL + "twitter.aspx";
        if (Request["oauth_token"] == null)
        {
            Response.Redirect(oAuth.AuthorizationLinkGet());
        }
        else
        {
            //Get the access token and secret.
            oAuth.AccessTokenGet(Request["oauth_token"], Request["oauth_verifier"]);
            verifier = Request["oauth_verifier"];
            token = Request["oauth_token"];
            if ((oAuth.TokenSecret.Length > 0))
            {
                //We now have the credentials, so make a call to the Twitter API.
                url = "https://api.twitter.com/1.1/account/verify_credentials.json?test=test&include_entities=true&skip_status=true";
                // url = "https://api.twitter.com/1.1/search/tweets.json";
                xml = oAuth.oAuthWebRequest(oAuthTwitter.Method.GET, url, string.Empty);
                CheckAndRegister(xml);
            }
        }
    }
    private void CheckAndRegister(string xml)
    {
        try
        {
            JObject o = JObject.Parse(xml);

            SqlCommand cmd1 = new SqlCommand("sp_user_update_UserProfileDetails");
            cmd1.Parameters.AddWithValue("@reg_uid", SessionState._SignInUser.reg_uid);
            cmd1.Parameters.AddWithValue("@sm_id", 2);
            cmd1.Parameters.AddWithValue("@name",  Convert.ToString(o["screen_name"]));
            cmd1.Parameters.AddWithValue("@fname",  Convert.ToString(o["name"]));
            cmd1.Parameters.AddWithValue("@lname", "");
            cmd1.Parameters.AddWithValue("@email", Convert.ToString(o["screen_name"]));
            cmd1.Parameters.AddWithValue("@gender", "");
            cmd1.Parameters.AddWithValue("@profile_img_link", Convert.ToString(o["profile_image_url"]));
            cmd1.Parameters.AddWithValue("@no_of_friends", Convert.ToString(o["followers_count"]));
            cmd1.Parameters.AddWithValue("@no_of_likes", "0");
            cmd1.Parameters.AddWithValue("@profile_url", "https://twitter.com/" + Convert.ToString(o["screen_name"]));
            cmd1.Parameters.AddWithValue("@sm_uid", Convert.ToString(o["id"]));
            cmd1.Parameters.AddWithValue("@token","");
            ConnObj.ExecuteNonQuery(cmd1);
            if(ConnObj.IsSuccess)
            {
            Response.Redirect(SessionState.WebsiteURL + "myprofile.aspx");
            }
        }
        catch (Exception ex)
        {
        }
    }
}