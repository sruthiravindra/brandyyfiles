using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brands_brand_create_campaign_3i : System.Web.UI.Page
{
    ConnectionClass ConnObj = null;
    SqlCommand cmd_across = null;

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
        CreateOrUpdateActions(SessionState._Campaign.campaign_objective);
    }

    #region insert update delete
    private void CreateOrUpdateActions(byte campaign_type)
    {

        cmd_across = new SqlCommand("sp_insert_brands_campaigns_action");
        SessionState.EditId_2 = 0;
        CreateAction(campaign_type);
        
    }
    private void CreateAction(byte campaign_type)
    {
        cmd_across.Parameters.AddWithValue("@campaign_type", Convert.ToByte(campaign_type));
        cmd_across.Parameters.AddWithValue("@val_1", SessionState._Campaign.actions[campaign_type].val1);
        cmd_across.Parameters.AddWithValue("@val_2", SessionState._Campaign.actions[campaign_type].val2);
        cmd_across.Parameters.AddWithValue("@val_3", SessionState._Campaign.actions[campaign_type].val3);
        cmd_across.Parameters.AddWithValue("@val_4", SessionState._Campaign.actions[campaign_type].val4);

        cmd_across.Parameters.AddWithValue("@action_id", 0);
        cmd_across.Parameters.AddWithValue("@campaign_id", SessionState._Campaign.campaign_id);
        cmd_across.Parameters.AddWithValue("@brand_id", SessionState._Campaign.brand_id);
        cmd_across.Parameters.AddWithValue("@name", SessionState._Campaign.campaign_name);
        cmd_across.Parameters.AddWithValue("@action_status", SessionState._Campaign.campaign_status);
        

        ConnObj.GetDataTab(cmd_across);

        if (ConnObj.IsSuccess)
        {
            SessionState._Campaign.actions[campaign_type].action_id = Convert.ToInt64(ConnObj.DataTab.Rows[0]["action_id"]);
            SessionState.EditId_2 = SessionState._Campaign.actions[campaign_type].action_id;            
            UpdateActionReward();
            SessionState.EditId_2 = 0;
            
        }
        else
        {
            //lblErrorMsg.Text = ConnObj.Message;
            //lblErrorMsg.ForeColor = System.Drawing.Color.Red;
            //lblErrorMsg.Visible = true;
        }
    }

    private void UpdateActionReward()
    {
        SqlCommand cmd = new SqlCommand("sp_update_brands_campaigns_action_reward");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@campaign_id", SessionState._Campaign.campaign_id);
        cmd.Parameters.AddWithValue("@action_id", SessionState.EditId_2);
        cmd.Parameters.AddWithValue("@created_by", SessionState._BrandAdmin.user_id);

        cmd.Parameters.AddWithValue("@reward_user", SessionState._Campaign.reward_user);
        cmd.Parameters.AddWithValue("@reward_per_friend", SessionState._Campaign.reward_per_friend);
        cmd.Parameters.AddWithValue("@reward_per_like", SessionState._Campaign.reward_per_like);
        cmd.Parameters.AddWithValue("@reward_per_share", SessionState._Campaign.reward_per_share);

        ConnObj.GetDataSet(cmd);


        if (ConnObj.IsSuccess)
        {
           Response.Redirect(SessionState.WebsiteURL + "brands/campaignview.aspx");
        }
        else
        {
            
        }
    }
    #endregion
}