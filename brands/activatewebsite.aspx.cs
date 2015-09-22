using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brands_activatewebsite : System.Web.UI.Page
{
    public ConnectionClass ConnObj = null;
    
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
        SqlCommand cmd = new SqlCommand("sp_insert_brands_social_media");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@sm_name", "Website");
        cmd.Parameters.AddWithValue("@sm_email", "Website");
        cmd.Parameters.AddWithValue("@sm_id", 4);
        cmd.Parameters.AddWithValue("@created_by", SessionState._BrandAdmin.user_id);
        cmd.Parameters.AddWithValue("@sm_desc", "");
        cmd.Parameters.AddWithValue("@sm_uid", "Website");        
        cmd.Parameters.AddWithValue("@profile_url", "Website");
        cmd.Parameters.AddWithValue("@profile_img_link", "Website");
        cmd.Parameters.AddWithValue("@token", "Website");

        ConnObj.GetDataTab(cmd);
        if (ConnObj.IsSuccess == true & ConnObj.DataTab != null & ConnObj.DataTab.Rows.Count > 0)
        {
            if (Convert.ToInt64(ConnObj.DataTab.Rows[0]["brand_sm_id"]) == 0)
            {
                Response.Redirect(SessionState.WebsiteURLBrand + "socialmedias.aspx");
            }
            else
            {
                SessionState.EditId_2 = 4;
                SessionState.ActivityID = Convert.ToInt64(ConnObj.DataTab.Rows[0]["brand_sm_id"]);
                Response.Redirect(SessionState.WebsiteURLBrand + "socialmediapage-create.aspx");
            }
        }
    }
}