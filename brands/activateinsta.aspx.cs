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

public partial class brands_activateinsta : System.Web.UI.Page
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
            parameters.Add("redirect_uri", System.Configuration.ConfigurationManager.AppSettings["instagram.brands.redirecturi"].ToString());
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

            SqlCommand cmd = new SqlCommand("sp_insert_brands_social_media");
            cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
            cmd.Parameters.AddWithValue("@sm_name", (string)jsResult["user"]["username"]);
            cmd.Parameters.AddWithValue("@sm_email", (string)jsResult["user"]["username"]);
            cmd.Parameters.AddWithValue("@sm_id", 3);
            cmd.Parameters.AddWithValue("@created_by", SessionState._BrandAdmin.user_id);
            cmd.Parameters.AddWithValue("@sm_desc", "");
            cmd.Parameters.AddWithValue("@sm_uid", (string)jsResult["user"]["id"]);
            string str = "https://instagram.com/" + (string)jsResult["user"]["username"];
            cmd.Parameters.AddWithValue("@profile_url", str);
            cmd.Parameters.AddWithValue("@profile_img_link", (string)jsResult["user"]["profile_picture"]);
            cmd.Parameters.AddWithValue("@token", (string)jsResult["access_token"]);

            ConnObj.GetDataTab(cmd);
            if (ConnObj.IsSuccess == true & ConnObj.DataTab != null & ConnObj.DataTab.Rows.Count > 0)
            {
                if (Convert.ToInt64(ConnObj.DataTab.Rows[0]["brand_sm_id"]) == 0)
                {
                    Response.Redirect(SessionState.WebsiteURLBrand + "socialmedias.aspx");
                }
                else
                {
                    SessionState.EditId_2 = 3;
                    SessionState.ActivityID = Convert.ToInt64(ConnObj.DataTab.Rows[0]["brand_sm_id"]);
                    Response.Redirect(SessionState.WebsiteURLBrand + "socialmediapage-create.aspx");
                }

                //
            }
        }
        catch (Exception ex)
        {
        }
    }
}