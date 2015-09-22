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

public partial class brands_activity_verification_overview : System.Web.UI.Page
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
        GetCampaignActivitiesDetails();
    }

    private void GetCampaignActivitiesDetails()
    {
        Int32 b_id = SessionState._BrandAdmin.brand_id;
        SqlCommand cmd = new SqlCommand("sp_brandyy_GetAllPendingActivitiesOverview");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@verification_status", 0);
        ConnObj.GetDataSet(cmd);
        
        RepTab.DataSource = null;
        RepTab.DataBind();
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            RepTab.DataSource = ConnObj.DataSet.Tables[0];
            RepTab.DataBind();
        }

    }

    protected void lnk_User_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { '~' });
        SessionState.EditId = Convert.ToInt64(commandArgs[0]);
        SessionState._Campaign = new Campaign(SessionState.EditId, SessionState._BrandAdmin.brand_id);
        SessionState._Campaign.campaign_name = Convert.ToString(commandArgs[1]);
        Response.Redirect(SessionState.WebsiteURLBrand + "activity-verification-list.aspx");
    }
    protected void btn_View_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        Int64 id = Convert.ToInt64(commandArgs[0]);
        SessionState.EditId = id;

        SessionState._Campaign = new Campaign(id, SessionState._BrandAdmin.brand_id);

        Response.Redirect(SessionState.WebsiteURL + "brands/campaignview.aspx");
    }
}