using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class brands_brandcampaigns : System.Web.UI.Page
{    
    public static ConnectionClass ConnObj = null;
    public int Cnt;
    CommonVariableCodes _CommonVariableCodes = new CommonVariableCodes();

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
        LoadCampaigns();

    }

    private void LoadCampaigns()
    {
        SessionState.EditId = 0;
        Cnt = 1;
        //divBrandWelcome.InnerHtml = "Welcome (" + SessionState._BrandAdmin.brand_name + "), All Campaigns";

        SqlCommand cmd = new SqlCommand("sp_Brand_GetCampaigns");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@campaign_status", _CommonVariableCodes.campaign_status_active);
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {            
            RepTab.DataSource = ConnObj.DataSet.Tables[0];
            RepTab.DataBind();

        }
        else
        {
            lblNoActiveCampaigns.Visible = true;
        }

        // load inactive campaigns
        cmd = new SqlCommand("sp_Brand_GetCampaigns");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@campaign_status", _CommonVariableCodes.campaign_status_inactive);
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            RepTabInactive.DataSource = ConnObj.DataSet.Tables[0];
            RepTabInactive.DataBind();

        }
        else
        {
            lblNoInActiveCampaigns.Visible = true;
        }
    }

    #region onclick events
    protected void btn_CreateCampaign_Click(object sender, EventArgs e)
    {
        SessionState._Campaign = new Campaign(0, SessionState._BrandAdmin.brand_id);

        Response.Redirect( SessionState.WebsiteURL + "brands/brand-create-campaign-1.aspx" );
    }
    protected void btn_Status_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        Int64 id = Convert.ToInt64(commandArgs[0]);
        byte status = Convert.ToByte( commandArgs[1] );

        // case: the verfication still pending
        if (Convert.ToInt16(commandArgs[2]) != 2)
        {
            return;
        }
        // case: the verified
        else
        {
            SqlCommand cmd = new SqlCommand("sp_update_brands_campaign_status");
            cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
            cmd.Parameters.AddWithValue("@campaign_id", id);
            cmd.Parameters.AddWithValue("@updated_by", SessionState._BrandAdmin.user_id);
            cmd.Parameters.AddWithValue("@campaign_status", (status == (byte)1) ? (byte)0 : (byte)1);
            ConnObj.GetDataTab(cmd);
            LoadCampaigns();
        }

        
    }
    protected void btn_Verification_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        Int64 id = Convert.ToInt64(commandArgs[0]);
        byte status = Convert.ToByte(commandArgs[2]);
        // case: verified
        if (Convert.ToInt16(commandArgs[2]) == 2)
        {
            return;
        }        
        else
        {
            SqlCommand cmd = new SqlCommand("sp_update_brands_campaign_verification");
            cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
            cmd.Parameters.AddWithValue("@campaign_id", id);
            cmd.Parameters.AddWithValue("@updated_by", SessionState._BrandAdmin.user_id);
            cmd.Parameters.AddWithValue("@verification_status", 1);
            cmd.Parameters.AddWithValue("@log_text", "");
            ConnObj.GetDataTab(cmd);
            LoadCampaigns();
        }
    }
    protected void btn_Edit_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        Int64 id = Convert.ToInt64(commandArgs[0]);
        SessionState.EditId = id;

        SessionState._Campaign = new Campaign(id, SessionState._BrandAdmin.brand_id);

        
        
        //

        

     
        // case: the campaign is not running
        if (Convert.ToInt16(commandArgs[1]) == 0)
        {
            Response.Redirect(SessionState.WebsiteURL + "brands/brand-create-campaign-1.aspx");
        }
        // case: the campaign is running
        else
        {
            //Response.Redirect(SessionState.WebsiteURL + "brands/brand-create-campaign-4.aspx");
            Response.Redirect(SessionState.WebsiteURL + "brands/brand-update-campaign-1.aspx");
        }
     
        
    }
    protected void RepTab_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "LoadCampaignTypeForm")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            SessionState._Campaign.campaign_objective = Convert.ToByte(commandArgs[0]);
            SessionState._Campaign.campaign_name = commandArgs[1];

            Response.Redirect(SessionState.WebsiteURLBrand + "brand-create-campaign-1.aspx");
        }
        if (e.CommandName == "CreateCampaign")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            SessionState._Campaign = new Campaign(0, SessionState._BrandAdmin.brand_id);
            SessionState._Campaign.campaign_objective = Convert.ToByte(commandArgs[0]);
            SessionState._Campaign.campaign_name = commandArgs[1];
            Response.Redirect(SessionState.WebsiteURL + "brands/brand-create-campaign-1.aspx");
        }
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
    #endregion

    #region private functions
    

    

    
    #endregion
    protected void btn_CreateCampaign_Click1(object sender, EventArgs e)
    {
        
    }
    
}