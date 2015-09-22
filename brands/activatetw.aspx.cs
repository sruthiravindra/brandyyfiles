using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using oAuthExample;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;

public partial class brands_activatetw : System.Web.UI.Page
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

        if ((!Page.IsPostBack) && (SessionState._BrandAdmin != null))
        {            
            FirstPos();
        }
        else if (SessionState._BrandAdmin != null)
        {
            ;
        }
        else
        {
            Response.Redirect(SessionState.WebsiteURLBrand);
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
        oAuth.CallBackUrl = SessionState.WebsiteURLBrand + "activatetw.aspx";
        if (Request["oauth_token"] == null)
        {
            Response.Redirect(oAuth.AuthorizationLinkGet());
        }
        else
        {
            //Get the access token and secret.
            oAuth.AccessTokenGet(Request["oauth_token"], Request["oauth_verifier"]);
            verifier =Request["oauth_verifier"];
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

            SqlCommand cmd = new SqlCommand("sp_insert_brands_social_media");
            cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
            cmd.Parameters.AddWithValue("@sm_name", Convert.ToString(o["name"]));
            cmd.Parameters.AddWithValue("@sm_email", Convert.ToString(o["screen_name"]));
            cmd.Parameters.AddWithValue("@sm_id", 2);
            cmd.Parameters.AddWithValue("@created_by", SessionState._BrandAdmin.user_id);
            cmd.Parameters.AddWithValue("@sm_desc", "Twitter");            
            cmd.Parameters.AddWithValue("@sm_uid", Convert.ToString(o["id"]));            
            string str = "https://twitter.com/" + Convert.ToString(o["screen_name"]);
            cmd.Parameters.AddWithValue("@profile_url", str);
            cmd.Parameters.AddWithValue("@profile_img_link", Convert.ToString(o["profile_image_url"]));
            cmd.Parameters.AddWithValue("@token", Convert.ToString(token));
            cmd.Parameters.AddWithValue("@verifier", Convert.ToString(verifier));
            
            ConnObj.GetDataTab(cmd);
            if (ConnObj.IsSuccess == true & ConnObj.DataTab != null & ConnObj.DataTab.Rows.Count > 0)
            {
                if (Convert.ToInt64(ConnObj.DataTab.Rows[0]["brand_sm_id"]) == 0)
                {
                    Response.Redirect(SessionState.WebsiteURLBrand + "socialmedias.aspx");
                }
                else
                {
                    SessionState.EditId_2 = 2;
                    SessionState.ActivityID = Convert.ToInt64(ConnObj.DataTab.Rows[0]["brand_sm_id"]);
                    Response.Redirect(SessionState.WebsiteURLBrand + "socialmediapage-create.aspx");
                }
                
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Alert2", "closeAndRefresh();", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "closeAndRefresh", "closeAndRefresh();", true);                                  
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "val", "closeAndRefresh();",true);
            }
        }
        catch (Exception ex)
        {
        }
    }
}