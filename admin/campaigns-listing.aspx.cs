using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IchooseIT.DAL;

public partial class admin_campaigns_listing : System.Web.UI.Page
{
    public int Cnt;
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

        if ((!Page.IsPostBack) && (SessionState._BrandyyAdmin != null))
        {
            FirstPos();
        }
        else if (SessionState._BrandyyAdmin != null)
        {
            ;
        }
        else
        {
            Response.Redirect(SessionState.WebsiteURLAdmin);
        }

    }
    #endregion

    private void FirstPos()
    {
        SessionState.EditId = 0;
        SessionState._Campaign = null;
        Cnt = 1;
        

                        // pending verification


        SqlCommand cmd = new SqlCommand("sp_Admin_GetCampaigns");
        cmd.Parameters.AddWithValue("@verification_status", 1);
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {

            RepTab.DataSource = ConnObj.DataSet.Tables[0];
            RepTab.DataBind();
        }
        else
        {
            RepTab.DataSource = null;
            RepTab.DataBind();
            lblNoCampaigns.Visible = true;
        }

                        // all campaigns

        cmd = new SqlCommand("sp_Admin_GetCampaigns");
        cmd.Parameters.AddWithValue("@verification_status", 0);
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {

            Repeater1.DataSource = ConnObj.DataSet.Tables[0];
            Repeater1.DataBind();
        }
        else
        {
            lblNoCampaigns.Visible = true;
        }

    }


    #region onclick events
    protected void btn_CreateCampaign_Click(object sender, EventArgs e)
    {
        SessionState._Campaign = new Campaign(0, SessionState._BrandAdmin.brand_id);

        Response.Redirect(SessionState.WebsiteURL + "admin/brand-create-campaign-1.aspx");
    }
    protected void btn_Status_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        Int64 id = Convert.ToInt64(commandArgs[0]);
        Int32 brand_id = Convert.ToInt32(commandArgs[2]);
        byte status = Convert.ToByte(commandArgs[1]);

        // case: the verfication still pending
        if (Convert.ToInt16(commandArgs[2]) == 0)
        {
            return;
        }
        // case: the verified
        else
        {
            SqlCommand cmd = new SqlCommand("sp_update_brands_campaign_status");
            cmd.Parameters.AddWithValue("@brand_id", brand_id);
            cmd.Parameters.AddWithValue("@campaign_id", id);
            cmd.Parameters.AddWithValue("@updated_by", SessionState._BrandyyAdmin.admin_id);
            cmd.Parameters.AddWithValue("@campaign_status", (status == (byte)1) ? (byte)0 : (byte)1);
            ConnObj.GetDataTab(cmd);
            FirstPos();

            //Brands_CampaignClass _Brands_CampaignClass = new Brands_CampaignClass();
            //_Brands_CampaignClass.Select(id);
            //if (_Brands_CampaignClass.IsSuccess)
            //{
            //    byte res_status = (status == (byte)1) ? (byte)0 : (byte)1;
            //    _Brands_CampaignClass.TabObj.campaign_status = res_status;
            //    _Brands_CampaignClass.Update();

            //    FirstPos();
            //}
        }


    }
    protected void btn_Verification_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        Int64 id = Convert.ToInt64(commandArgs[0]);
        Int32 brand_id = Convert.ToInt32(commandArgs[1]);
        byte status = Convert.ToByte(commandArgs[3]);

        SqlCommand cmd = new SqlCommand("sp_update_brands_campaign_verification");
        cmd.Parameters.AddWithValue("@brand_id", brand_id);
        cmd.Parameters.AddWithValue("@campaign_id", id);
        cmd.Parameters.AddWithValue("@updated_by", SessionState._BrandyyAdmin.admin_id);
        cmd.Parameters.AddWithValue("@verification_status", status);
        cmd.Parameters.AddWithValue("@log_text", "");
        ConnObj.GetDataTab(cmd);
        FirstPos();
    }
    protected void btn_Edit_Click(object sender, EventArgs e)
    {
        
    }
    protected void RepTab_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
            
    }
    protected void btn_View_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        Int64 id = Convert.ToInt64(commandArgs[0]);
        int brand_id = Convert.ToInt32(commandArgs[1]);
        SessionState.EditId = id;

        SessionState._Campaign = new Campaign(id, brand_id);

        Response.Redirect(SessionState.WebsiteURL + "admin/campaignview.aspx");
    }
    #endregion

    #region private functions
    private void GetCampaignBasicDetails()
    {
        //_Brands_CampaignClass.Select(SessionState._Campaign.campaign_id);

        //if (_Brands_CampaignClass.IsSuccess)
        //{
        //    SessionState._Campaign.campaign_id = _Brands_CampaignClass.TabObj.campaign_id;
        //    SessionState._Campaign.brand_id = _Brands_CampaignClass.TabObj.brand_id;
        //    SessionState._Campaign.campaign_objective = Convert.ToByte(_Brands_CampaignClass.TabObj.campaign_objective);
        //    SessionState._Campaign.campaign_name = _Brands_CampaignClass.TabObj.campaign_name;
        //    SessionState._Campaign.schedule_type = Convert.ToByte(_Brands_CampaignClass.TabObj.schedule_type);
        //    SessionState._Campaign.campaign_start = Convert.ToDateTime(_Brands_CampaignClass.TabObj.campaign_start);
        //    SessionState._Campaign.campaign_end = Convert.ToDateTime(_Brands_CampaignClass.TabObj.campaign_end);
        //    SessionState._Campaign.campaign_status = Convert.ToByte(_Brands_CampaignClass.TabObj.campaign_status);
        //    SessionState._Campaign.verification_status = Convert.ToByte(_Brands_CampaignClass.TabObj.verification_status);
        //    SessionState._Campaign.daily_budget = Convert.ToDecimal(_Brands_CampaignClass.TabObj.daily_buget);
        //    SessionState._Campaign.overall_budget = Convert.ToDecimal(_Brands_CampaignClass.TabObj.overall_budget);
        //    SessionState._Campaign.reward_type = Convert.ToByte(_Brands_CampaignClass.TabObj.reward_type);
        //    SessionState._Campaign.reward_amount = Convert.ToDecimal(_Brands_CampaignClass.TabObj.reward_amount);
        //    SessionState._Campaign.reward_percent = Convert.ToDecimal(_Brands_CampaignClass.TabObj.reward_percent);
        //    SessionState._Campaign.reward_product = _Brands_CampaignClass.TabObj.reward_product;
        //    SessionState._Campaign.reward_whom = Convert.ToByte(_Brands_CampaignClass.TabObj.reward_whom);
        //    SessionState._Campaign.reward_whom_2_nth = Convert.ToInt64(_Brands_CampaignClass.TabObj.reward_whom_2_nth);
        //    SessionState._Campaign.reward_whom_3_no_of = _Brands_CampaignClass.TabObj.reward_whom_3_no_of;
        //    SessionState._Campaign.reward_whom_3_max = _Brands_CampaignClass.TabObj.reward_whom_3_max;
        //}
    }



    private void GetCampaignTargetDetails()
    {
        SessionState._Campaign.target_country = "";
        SqlCommand cmd = new SqlCommand("sp_IChooseIT_GetCampaignTarget");
        cmd.Parameters.AddWithValue("@campaign_id", SessionState._Campaign.campaign_id);

        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            SessionState._Campaign.target_country = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["country"]);
        }
    }
    #endregion
    protected void btn_CreateCampaign_Click1(object sender, EventArgs e)
    {

    }
    
}