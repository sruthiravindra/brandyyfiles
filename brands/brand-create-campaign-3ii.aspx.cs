using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IchooseIT.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
public partial class brands_brand_create_campaign_3ii : System.Web.UI.Page
{   
    ConnectionClass ConnObj = null;
    Boolean flg = false;
    static Dictionary<byte, Int64> alreadyexistingactions = new Dictionary<byte, Int64>();
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
            //LoadCampaignObjectiveForm();
        }
        else
        {
            Response.Redirect(SessionState.WebsiteURLBrand);
        }

        ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "ShowOnLoadEvents1", "ShowOnLoadEvents()", true);

    }
    #endregion

    protected void FirstPos()
    {
        alreadyexistingactions = new Dictionary<byte, Int64>();       


        string[] headers = new string[1];
        Actions_Reward_To _Actions_Reward_To = new Actions_Reward_To();
        if (SessionState._Campaign.reward_type == 3)
        {
            ;
        }
        else
        {
            string possible_cols_string = _Actions_Reward_To.campaign_objective_settings[SessionState._Campaign.campaign_objective];
            headers = possible_cols_string.Split(',');       
        }

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
        GetPossibleActionsForObjective(dt);

        #region set show points controls

        #region get value for show points
        // set reward type details
        SqlCommand cmd = new SqlCommand("sp_Brand_GetCampaignRewardWhen");
        cmd.Parameters.AddWithValue("@campaign_id", Convert.ToInt32(SessionState._Campaign.campaign_id));
        cmd.Parameters.AddWithValue("@brand_id", Convert.ToInt32(SessionState._Campaign.brand_id));
        ConnObj.GetDataSet(cmd);
        
        #endregion

        // display show points to user control
        if (SessionState._Campaign.reward_type != 3)
        {
            divShowPoints.Visible = true;

            if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ConnObj.DataSet.Tables[0].Rows[0];
                drpDisplayRewardDetails.SelectedValue = Convert.ToBoolean(dr["display_reward_to_user"]) == true ? "1" : "0"; 
                drpAllActionsComp.SelectedValue = Convert.ToBoolean(dr["all_actions_compulsory"]) == true ? "1" : "0"; 
            }
        }
        else
        {
            if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ConnObj.DataSet.Tables[0].Rows[0];
                drpAllActionsComp.SelectedValue = Convert.ToString(dr["all_actions_compulsory"]);
            }
            drpDisplayRewardDetails.SelectedValue = "0";
            lbl_RewardError.Visible = true;
            lbl_RewardError.Text = "No points to set for reward type 'Product'. Continue to view Campaign Summary";
        }

        #endregion

        

    }

    private void GetPossibleActionsForObjective(DataTable dt)
    {
        DataTable dt_1 = new DataTable();
        dt_1.Columns.Add(new DataColumn("action", typeof(string)));
        dt_1.Columns.Add(new DataColumn("id", typeof(int)));        

        // get actions supported by the objective        
        SqlCommand cmd = new SqlCommand("sp_Get_Campaign_Objective_Actions");
        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(SessionState._Campaign.campaign_objective));
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ConnObj.DataSet.Tables[0].Rows)
            {
                if (SessionState._Campaign.actions[Convert.ToByte(dr["id"])] == null )
                {
                    continue;
                }
                else if ( Convert.ToInt64( SessionState._Campaign.actions[Convert.ToByte(dr["id"])].action_id ) == 0)
                {
                    continue;
                }

                dt_1.Rows.Add(dr["user_display_name"],
                    dr["id"]
                   );

                if ((Convert.ToByte(dr["id"]) == 9) || (Convert.ToByte(dr["id"]) == 10) || (Convert.ToByte(dr["id"]) == 19))
                {
                    divAllowOnePost.Visible = true;
                }
            }
            repTab_ActionNames.DataSource = dt_1;
            repTab_ActionNames.DataBind();
        }        
    }

    // get if any action created for this campaign
    private string[] FillData( byte campaign_type )
    {
        
        string[] setdata = new string[10];        
            setdata[1] = "0";
            setdata[2] = "0";
            setdata[3] = "0";
            setdata[4] = "0";
            setdata[5] = "0";
            setdata[6] = "0";
            setdata[7] = "0";
            flg = false; 
            
        SqlCommand cmd = new SqlCommand("sp_Brand_GetCampaign_Actions");
        cmd.Parameters.AddWithValue("@campaign_id", Convert.ToInt32(SessionState._Campaign.campaign_id));
        cmd.Parameters.AddWithValue("@campaign_type", campaign_type);
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            flg = true;
            DataRow dr = ConnObj.DataSet.Tables[0].Rows[0];

            if (alreadyexistingactions.ContainsKey(campaign_type) != true)
            {
                alreadyexistingactions.Add(campaign_type, Convert.ToInt64(dr["action_id"]));
            }

            
            setdata[1] = Convert.ToString(dr["reward_user"]);
            setdata[2] = Convert.ToString(dr["reward_per_friend"]);
            setdata[3] = Convert.ToString(dr["reward_per_like"]);
            setdata[4] = Convert.ToString(dr["reward_per_share"]);           

            if ((Convert.ToByte(dr["campaign_type"]) == 9) || (Convert.ToByte(dr["campaign_type"]) == 10) || (Convert.ToByte(dr["campaign_type"]) == 19))
            {
                chkAllowMoreThanOnePost.Checked = true ;
            }
        }

        return setdata;
    }

    private void DisplayUC()
    {
        //UserControl uc;
        //uc = (UserControl)Page.LoadControl("uc/create_campaign_" + Convert.ToString(SessionState._Campaign.campaign_objective) + ".ascx");
        //div_UC.Controls.Add(uc);        
    }
    
    private void CreateOrUpdateActions()
    {       

        // loop through the possible set of actions and perform the required action
        foreach (RepeaterItem item in repTab_ActionNames.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField campaign_type_fld = (HiddenField)item.FindControl("hdn_CampaignType");
                byte campaign_type = Convert.ToByte(campaign_type_fld.Value);
                CheckBox chk = (CheckBox)item.FindControl("chk_Action");
                if (chk.Checked == true)
                {
                    cmd_across = new SqlCommand("sp_update_brands_campaigns_action_reward");

                    SessionState.EditId_2 = alreadyexistingactions[campaign_type];

                    
                    SetRewardDetailsForInsertUpdate(item);

                    UpdateAction();
                    SessionState.EditId_2 = 0;
                }             
               
            }
        }

    }
    
    private void UpdateAction()
    {
        cmd_across.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd_across.Parameters.AddWithValue("@campaign_id", SessionState._Campaign.campaign_id);
        cmd_across.Parameters.AddWithValue("@action_id", SessionState.EditId_2);
        cmd_across.Parameters.AddWithValue("@created_by", SessionState._BrandAdmin.user_id);

        ConnObj.GetDataSet(cmd_across);


        if (ConnObj.IsSuccess)
        {
            ;
        }
        else
        {
            lbl_RewardError.Text = ConnObj.Message;
            lbl_RewardError.ForeColor = System.Drawing.Color.Red;
            lbl_RewardError.Visible = true;
        }
    }
   
    
    private void SetRewardDetailsForInsertUpdate(RepeaterItem Item)
    {
        Repeater Repeater2 = (Repeater)Item.FindControl("repTab_content");
        #region get reward what details
        int col = 1;
        foreach (Control rptItem in Repeater2.Controls)
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
                    cmd_across.Parameters.AddWithValue("@reward_user", val);
                    break;
                case 2:
                    cmd_across.Parameters.AddWithValue("@reward_per_friend", val);
                    break;
                case 3:
                    cmd_across.Parameters.AddWithValue("@reward_per_like", val);
                    break;
                case 4:
                    cmd_across.Parameters.AddWithValue("@reward_per_share", val);
                    break;
            }
            col++;
        }

        #endregion
    }
    private void UpdateRewardWhenAndDisplayRewardDetails()
    {
        SqlCommand cmd = new SqlCommand("sp_update_brands_campaigns_additional");
        cmd.Parameters.AddWithValue("@campaign_id", SessionState._Campaign.campaign_id);
        cmd.Parameters.AddWithValue("@display_reward_to_user", ((drpDisplayRewardDetails.SelectedValue=="1")?true:false));
        cmd.Parameters.AddWithValue("@all_actions_compulsory", ((drpAllActionsComp.SelectedValue == "1") ? true : false));
        ConnObj.GetDataSet(cmd);
    }

    #region protected functions    
    protected void btn_Update_Click(object sender, EventArgs e)
    {
        lbl_RewardError.Text = "";
        CreateOrUpdateActions();

        // update reward when and display reward to user option
        UpdateRewardWhenAndDisplayRewardDetails();

        if (lbl_RewardError.Text == "")
        {
            Response.Redirect(SessionState.WebsiteURL + "brands/campaignview.aspx");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(SessionState.WebsiteURL + "brands/brand-create-campaign-1.aspx");
    }
          
    protected void txtRewards_TextChanged(object sender, EventArgs e)
    {
        //decimal total = 0;
        //foreach (RepeaterItem rptItem in repTab_content.Items)
        //{
        //    TextBox txtQty = (TextBox)rptItem.FindControl("txtRewards");
        //    if (txtQty.Visible == true)
        //    {
        //        if (txtQty.Text != "")
        //        {
        //            total += Convert.ToDecimal(txtQty.Text);
        //        }
        //    }
        //}
        //txtTotal.Text = Convert.ToString(total);
    }
    #endregion

    protected void repTab_ActionNames_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            byte campaign_type = Convert.ToByte(drv["id"]);            
            CheckBox chk = (CheckBox)e.Item.FindControl("chk_Action");
            Repeater Repeater2 = (Repeater)e.Item.FindControl("repTab_content");

            string[] headers = new string[1];
            string[] main_headers = new string[1];
            Actions_Reward_To _Actions_Reward_To = new Actions_Reward_To();
            string possible_cols_string = _Actions_Reward_To.campaign_settings[campaign_type];
            headers = possible_cols_string.Split(',');

            possible_cols_string = _Actions_Reward_To.campaign_objective_settings[SessionState._Campaign.campaign_objective];
            main_headers = possible_cols_string.Split(',');
            
            

            // get the campaign type settings from database
            string[] setdata = FillData(campaign_type);
            chk.Checked = flg;

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("header", typeof(string)));
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("col_visiblestate", typeof(bool)));
            dt.Columns.Add(new DataColumn("visiblestate", typeof(bool)));
            dt.Columns.Add(new DataColumn("data", typeof(string)));

            for (int i = 1; i < _Actions_Reward_To.column_headers.Length; i++)
            {
                dt.Rows.Add(_Actions_Reward_To.column_headers[i],
                    i,
                    ((Array.IndexOf(main_headers, Convert.ToString(i)) > -1) ? true : false),
                    ((Array.IndexOf(headers, Convert.ToString(i)) > -1) ? true : false),
                    setdata[i]
                    );
            }

            

            Repeater2.DataSource = dt;
            Repeater2.DataBind();
        }
    }
    protected void chk_Action_CheckedChanged(object sender, EventArgs e)
    {

    }
    
    protected void ViewActionSettings_Click(object sender, EventArgs e)
    {
        //SessionState._Campaign.firstpos = true;
        //div_UC.Visible = true;
        //DisplayUC();
        //SessionState._Campaign.firstpos = false;
    }
}