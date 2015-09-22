using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net;
using System.Data.SqlClient;

public partial class Instagram : System.Web.UI.Page
{
    public ConnectionClass ConnObj = null;
    string token = ""; static string code = string.Empty;
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
        if (!String.IsNullOrEmpty(Request["code"]) && !IsPostBack)
        {
            //Get the authenticated code from redirect URL
            code = Request["code"].ToString();
            GetProfileDetails();
        }
    }

    public void GetProfileDetails()//Get User Profile Details
    {

        try
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("client_id", System.Configuration.ConfigurationManager.AppSettings["instagram.clientid"].ToString());
            parameters.Add("client_secret", System.Configuration.ConfigurationManager.AppSettings["instagram.clientsecret"].ToString());
            parameters.Add("grant_type", "authorization_code");
            parameters.Add("redirect_uri", System.Configuration.ConfigurationManager.AppSettings["instagram.redirecturi"].ToString());
            parameters.Add("code", code);
            WebClient client = new WebClient();
            var result = client.UploadValues("https://api.instagram.com/oauth/access_token", "POST", parameters);
            var response = System.Text.Encoding.Default.GetString(result);

            CheckAndRegister(response);

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void CheckAndRegister(string xml)
    {
        try
        {
            var jsResult = (JObject)JsonConvert.DeserializeObject(xml);

            SqlCommand cmd1 = new SqlCommand("sp_user_update_UserProfileDetails");
            cmd1.Parameters.AddWithValue("@reg_uid", SessionState._SignInUser.reg_uid);
            cmd1.Parameters.AddWithValue("@sm_id", 3);
            cmd1.Parameters.AddWithValue("@name", (string)jsResult["user"]["username"]);
            cmd1.Parameters.AddWithValue("@fname", (string)jsResult["user"]["username"]);
            cmd1.Parameters.AddWithValue("@lname", "");
            cmd1.Parameters.AddWithValue("@email", (string)jsResult["user"]["username"]);
            cmd1.Parameters.AddWithValue("@gender", "");
            cmd1.Parameters.AddWithValue("@profile_img_link", (string)jsResult["user"]["profile_picture"]);
            cmd1.Parameters.AddWithValue("@no_of_friends", "");
            cmd1.Parameters.AddWithValue("@no_of_likes", "0");
            cmd1.Parameters.AddWithValue("@profile_url", "https://instagram.com/" + (string)jsResult["user"]["username"]);
            cmd1.Parameters.AddWithValue("@sm_uid", (string)jsResult["user"]["id"]);
            cmd1.Parameters.AddWithValue("@token", "");
            ConnObj.ExecuteNonQuery(cmd1);

            if (ConnObj.IsSuccess)
            {
                Response.Redirect(SessionState.WebsiteURL + "myprofile.aspx");
            }
        }

        catch (Exception ex)
        {
        }
    }
}