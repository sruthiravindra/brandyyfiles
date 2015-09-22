using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IchooseIT.DAL;
public partial class brands_create_campaign_reward_details : System.Web.UI.UserControl
{
    CommonVariableCodes _CommonVariableCodes = new CommonVariableCodes();
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
            // FirstPos();
        }
        else if (SessionState._BrandAdmin != null)
        {
            FirstPos();
        }
        else
        {
            Response.Redirect(SessionState.WebsiteURLBrand);
        }

        ScriptManager.RegisterStartupScript(UpdatePanel_Main, UpdatePanel_Main.GetType(), "ShowOnLoadEvents1", "ShowOnLoadEvents()", true);

    }
    #endregion

    private void FirstPos()
    {
        Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), "val", "fnOnUpdateValidators();");
        FillActionRewards();
    }
    // get if any action created for this campaign
    private string[] FillData(byte campaign_type)
    {

        string[] setdata = new string[10];
        setdata[1] = "0";
        setdata[2] = "0";
        setdata[3] = "0";
        setdata[4] = "0";
        setdata[5] = "0";
        setdata[6] = "0";
        setdata[7] = "0";


        SqlCommand cmd = new SqlCommand("sp_Brand_GetCampaign_Actions");
        cmd.Parameters.AddWithValue("@campaign_id", Convert.ToInt32(SessionState._Campaign.campaign_id));
        cmd.Parameters.AddWithValue("@campaign_type", campaign_type);
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ConnObj.DataSet.Tables[0].Rows[0];

            setdata[1] = Convert.ToString(dr["reward_user"]);
            setdata[2] = Convert.ToString(dr["reward_per_friend"]);
            setdata[3] = Convert.ToString(dr["reward_per_like"]);
            setdata[4] = Convert.ToString(dr["reward_per_share"]);
        }

        return setdata;
    }
    private void FillActionRewards()
    {
        string[] headers = new string[1];
        Actions_Reward_To _Actions_Reward_To = new Actions_Reward_To();
        string possible_cols_string = _Actions_Reward_To.campaign_objective_settings[SessionState._Campaign.campaign_objective];
        headers = possible_cols_string.Split(',');

        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("header", typeof(string)));
        dt.Columns.Add(new DataColumn("id", typeof(int)));
        dt.Columns.Add(new DataColumn("column_id", typeof(int)));
        dt.Columns.Add(new DataColumn("visiblestate", typeof(bool)));

        for (int i = 1; i < _Actions_Reward_To.column_headers.Length; i++)
        {
            dt.Rows.Add(_Actions_Reward_To.column_headers[i],
                i, i,
                ((Array.IndexOf(headers, Convert.ToString(i)) > -1) ? true : false)
                );
        }
        repTab_header.DataSource = dt;
        repTab_header.DataBind();
        GetPossibleActionsForObjective();
    }

    private void GetPossibleActionsForObjective()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("header", typeof(string)));
        dt.Columns.Add(new DataColumn("id", typeof(int)));
        dt.Columns.Add(new DataColumn("col_visiblestate", typeof(bool)));
        dt.Columns.Add(new DataColumn("visiblestate", typeof(bool)));
        dt.Columns.Add(new DataColumn("data", typeof(string)));

        string[] headers = new string[1];
        string[] main_headers = new string[1];
        Actions_Reward_To _Actions_Reward_To = new Actions_Reward_To();
        string possible_cols_string = _Actions_Reward_To.campaign_settings[SessionState._Campaign.campaign_objective];
        headers = possible_cols_string.Split(',');

        possible_cols_string = _Actions_Reward_To.campaign_objective_settings[SessionState._Campaign.campaign_objective];
        main_headers = possible_cols_string.Split(',');

        // get the campaign type settings from database
        string[] setdata = FillData(SessionState._Campaign.campaign_objective);

        for (int i = 1; i < _Actions_Reward_To.column_headers.Length; i++)
        {
            dt.Rows.Add(_Actions_Reward_To.column_headers[i],
                i,
                ((Array.IndexOf(main_headers, Convert.ToString(i)) > -1) ? true : false),
                ((Array.IndexOf(headers, Convert.ToString(i)) > -1) ? true : false),
                setdata[i]
                );
        }

        repTab_content.DataSource = dt;
        repTab_content.DataBind();
    }
   
    private void SetRewardDetailsForInsertUpdate()
    {
        #region get reward what details
        int col = 1;
        foreach (Control rptItem in repTab_content.Controls)
        {
            TextBox txtQty = (TextBox)rptItem.FindControl("txtRewards");
            decimal val = 0;
            if (txtQty != null)
            {
                if (txtQty.Text != "")
                {
                    val = Convert.ToDecimal(txtQty.Text);
                }
            }

            switch (col)
            {
                case 1:
                    SessionState._Campaign.reward_user = val;
                    break;
                case 2:
                    SessionState._Campaign.reward_per_friend = val;
                    break;
                case 3:
                    SessionState._Campaign.reward_per_like = val;
                    break;
                case 4:
                    SessionState._Campaign.reward_per_share = val;
                    break;
            }
            col++;
        }

        #endregion
    }

    #region insert update delete
    private void CreateOrUpdateActions(byte campaign_type)
    {
        
        
        SessionState.EditId_2 = 0;
        CreateAction(campaign_type);

    }
    private void CreateAction(byte campaign_type)
    {
        SqlCommand cmd_across = new SqlCommand("sp_insert_brands_campaigns_action");
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
        SetRewardDetailsForInsertUpdate();

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
    protected void btn_Update_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

            CreateOrUpdateActions(SessionState._Campaign.campaign_objective);
        }
        else
        {
            lblValidationErrors.Text = "Check page For Incomplete Data";
            lblValidationErrors.Visible = true;
            ScriptManager.RegisterStartupScript(UpdatePanel_Main, UpdatePanel_Main.GetType(), "HideStatusNotification1", "HideStatusNotification()", true);
        }

    }
}