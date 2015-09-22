using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IchooseIT.DAL;
using System.IO;

public partial class brands_activity_verification_list : System.Web.UI.Page
{
    public Int64 Cnt = 0;
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
        GetCampaignActivitiesDetails(0, 10);
    }

    private void GetCampaignActivitiesDetails(int verification_status, int reward_status)
    {
        SqlCommand cmd = new SqlCommand("sp_brandyy_GetCampaignDefaultActivities");
        cmd.Parameters.AddWithValue("@campaign_id", SessionState._Campaign.campaign_id);
        cmd.Parameters.AddWithValue("@verification_status", verification_status);
        cmd.Parameters.AddWithValue("@reward_status", reward_status);
        ConnObj.GetDataSet(cmd);        

        ConnObj.GetDataSet(cmd);
        RepTab.DataSource = null;
        RepTab.DataBind();
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            RepTab.DataSource = ConnObj.DataSet.Tables[0];
            RepTab.DataBind();
        }
        else
        {
            btnStartVerification.Visible = false;
            lblNoCampaigns.Visible = true;
        }

    }

    protected void lnk_User_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        SessionState.EditId = Convert.ToInt64(btn.CommandArgument);
        Response.Redirect(SessionState.WebsiteURLBrand + "activity-listing.aspx");
    }
    protected void btnStartVerification_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        SessionState.EditId = 0;
        Response.Redirect(SessionState.WebsiteURLBrand + "activity-verification.aspx");        
    }
    protected void btnVerifyActivity_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        SessionState.EditId = Convert.ToInt64(btn.CommandArgument);
        Response.Redirect(SessionState.WebsiteURLBrand + "activity-verification.aspx");        
    }
}