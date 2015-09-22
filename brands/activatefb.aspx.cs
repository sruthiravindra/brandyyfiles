using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brands_activatefb : System.Web.UI.Page
{
    public static ConnectionClass ConnObj = null;
  
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

    private void FirstPos()
    {
    }

    [System.Web.Services.WebMethod(true)]
    public static FBLoginDetails ActivateUser(FBLoginDetails _FBLoginDetails)
    {
        if ((SessionState._BrandAdmin != null))
        {
            // get user long lived access token and other profile details
            importfbbranddetails t = new importfbbranddetails();
            string longlivedtoken = t.getUserLongLivedAccessToken(_FBLoginDetails.AccessToken, SessionState._BrandAdmin.user_id);
           

            SqlCommand cmd = new SqlCommand("sp_insert_brands_social_media");
            cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
            cmd.Parameters.AddWithValue("@sm_name", _FBLoginDetails.Name);
            cmd.Parameters.AddWithValue("@sm_email", _FBLoginDetails.Email);
            cmd.Parameters.AddWithValue("@sm_id", 1);
            cmd.Parameters.AddWithValue("@sm_uid", _FBLoginDetails.ID);
            cmd.Parameters.AddWithValue("@profile_url", _FBLoginDetails.ProfileUrl);
            cmd.Parameters.AddWithValue("@profile_img_link", _FBLoginDetails.ProfileImageUrl);
            cmd.Parameters.AddWithValue("@token", longlivedtoken);
            cmd.Parameters.AddWithValue("@created_by", SessionState._BrandAdmin.user_id);
            cmd.Parameters.AddWithValue("@sm_desc", "Facebook");
            ConnObj.GetDataTab(cmd);

            if (ConnObj.IsSuccess == true & ConnObj.DataTab != null & ConnObj.DataTab.Rows.Count > 0)
            {
                if (Convert.ToInt64(ConnObj.DataTab.Rows[0]["brand_sm_id"]) == 0)
                {
                    _FBLoginDetails.Message = "Invalid details";
                    _FBLoginDetails.LoginSuccessRedirectHomePage = SessionState.WebsiteURLBrand + "socialmedias.aspx";
                }
                else
                {
                    _FBLoginDetails.Message = "Success";
                    SessionState.EditId_2 = 1;
                    SessionState.ActivityID = Convert.ToInt64(ConnObj.DataTab.Rows[0]["brand_sm_id"]);
                    _FBLoginDetails.LoginSuccessRedirectHomePage = SessionState.WebsiteURLBrand + "socialmediapage-create.aspx";
                }

                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "Alert2", "closeAndRefresh();", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "closeAndRefresh", "closeAndRefresh();", true);                                  
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "val", "closeAndRefresh();",true);
            }
        }
        else
        {
            _FBLoginDetails.Message = "Your Login session has expired";
            _FBLoginDetails.LoginSuccessRedirectHomePage = SessionState.WebsiteURLBrand;
        }

        return _FBLoginDetails; ;
    }

}