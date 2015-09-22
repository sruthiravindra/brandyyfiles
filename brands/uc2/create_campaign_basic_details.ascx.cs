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

public partial class brands_uc2_create_campaign_basic_details : System.Web.UI.UserControl
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

        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "ShowOnLoadEvents1", "ShowOnLoadEvents()", true);
        //ScriptManager.RegisterStartupScript(UpdatePanel_Main, UpdatePanel_Main.GetType(), "ShowOnLoadEvents1", "ShowOnLoadEvents()", true);

    }
    #endregion

    private void FirstPos()
    {
        Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), "val", "fnOnUpdateValidators();");


        BasicFormForstPos();
        FillTargetAllUsers();
        FillCountries();
        FillAge(13, 65);
        FillGender();        
    }
    private void BasicFormForstPos()
    {
        txtCampaignName1.Text = SessionState._Campaign.campaign_name;

        if (SessionState.EditId > 0)
        {
            btn_Panel2.Visible = false;
            FillControls();
        }
        else
        {
            btn_Update.Visible = false;
            btn_ManageActions.Visible = false;
        }

        FillRewardWhom();
    }


    #region private functions

    private void FillTargetAllUsers()
    {
        if (SessionState._Campaign.target_all_users)
        {
            chkAllUsers2.Checked = true;
        }
        else
        {
            chkAllUsers2.Checked = false;
        }
    }
    private void FillCountries()
    {
        SqlCommand cmd = new SqlCommand("sp_Get_Countries");
        cmd.Parameters.AddWithValue("@id", 0);

        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            DrpCountry2.DataSource = ConnObj.DataSet.Tables[0];
            DrpCountry2.DataTextField = "country_name";
            DrpCountry2.DataValueField = "country_id";
            DrpCountry2.DataBind();

            if (SessionState._Campaign.target_all_users == false)
            {
                foreach (ListItem li in DrpCountry2.Items) // (int i = 0; i < DrpCountry2.Items; i++)
                {
                    if (SessionState._Campaign.target_country.Contains("{" + li.Value + "}"))
                    {
                        li.Selected = true;
                    }
                }
                //.SelectedValue = "1,2";
            }
        }
    }

    private void FillAge(int start, int end)
    {
        int i = 0; int j = 0;
        for (i = start; i <= end; i++)
        {
            ListItem item = new ListItem(i.ToString(), i.ToString());
            drp_AgeFrom.Items.Add(item);
        }

        drp_AgeFrom.SelectedIndex = 0;
        drp_AgeTill.SelectedValue = Convert.ToString(start);

        for (i = start; i <= end; i++)
        {
            ListItem item = new ListItem(i.ToString(), i.ToString());
            drp_AgeTill.Items.Add(item);
            j++;
        }
        drp_AgeTill.SelectedIndex = j;
        drp_AgeTill.SelectedValue = Convert.ToString(i - 1);

        if (SessionState._Campaign.target_all_users == false)
        {
            if (SessionState._Campaign.target_from_age > 0) drp_AgeFrom.SelectedValue = Convert.ToString(SessionState._Campaign.target_from_age);
            if (SessionState._Campaign.target_to_age > 0) drp_AgeTill.SelectedValue = Convert.ToString(SessionState._Campaign.target_to_age);
        }
    }

    private void FillGender()
    {
        ListItem item = new ListItem("ALL", "0");
        drp_Gender.Items.Add(item);

        ListItem item1 = new ListItem("Female", "F");
        drp_Gender.Items.Add(item1);
        ListItem item2 = new ListItem("Male", "M");
        drp_Gender.Items.Add(item2);

        if (SessionState._Campaign.target_all_users == false)
        {
            if (SessionState._Campaign.target_gender != "0") drp_Gender.SelectedValue = SessionState._Campaign.target_gender;
        }
    }
    

    
    private void FillControls()
    {
        GetCampaignBasicDetails();
        GetCampaignTargetDetails();

        txtCampaignName1.Text = SessionState._Campaign.campaign_name;
        txtOverallBudget.Text = Convert.ToString(SessionState._Campaign.overall_budget);
        txtMaxBrandyyPoints.Text = Convert.ToString(SessionState._Campaign.max_brandyy_points);

        txtStartDate1.Text = Convert.ToString(SessionState._Campaign.campaign_start);
        txtEndDate1.Text = Convert.ToString(SessionState._Campaign.campaign_end);

        txtRewardWhom_2.Text = Convert.ToString(SessionState._Campaign.reward_whom_2_nth);
        txtRewardWhom_3_1.Text = SessionState._Campaign.reward_whom_3_no_of;
        txtRewardWhom_3_2.Text = SessionState._Campaign.reward_whom_3_max;
        drpRewardWhnType.SelectedValue = Convert.ToString(SessionState._Campaign.reward_when_type);
        txtRewardWhenDate.Text = Convert.ToString(SessionState._Campaign.reward_when_date);

        switch (SessionState._Campaign.reward_whom)
        {
            case 1: radio1.Checked = true; break;
            case 2: radio2.Checked = true; break;
            case 3: radio3.Checked = true; break;
        }

        if (SessionState._Campaign.reward_when_type == 3)
        {
            divRewardWhenDate.Visible = true;
        }

        #region schedule dates
        if (SessionState._Campaign.schedule_type == 1)
        {
            rdScheduleDaily.Checked = true;

        }
        else
        {
            rdScheduleDateBased.Checked = true;
            divScheduleDates.Visible = true;
        }
        #endregion



        // fill action details
        SqlCommand cmd = new SqlCommand("sp_Brand_GetCampaign_Actions");
        cmd.Parameters.AddWithValue("@campaign_id", Convert.ToInt32(SessionState._Campaign.campaign_id));
        cmd.Parameters.AddWithValue("@campaign_type", 0);
        ConnObj.GetDataSet(cmd);

        SessionState._Campaign.actions = new campaign_action[Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["campaign_type_max_cnt"])];
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ConnObj.DataSet.Tables[0].Rows)
            {
                campaign_action ca = new campaign_action();
                ca.action_id = Convert.ToInt64(dr["action_id"]);
                ca.campaign_type = Convert.ToByte(dr["campaign_type"]);
                ca.val1 = Convert.ToString(dr["val_1"]);
                ca.val2 = Convert.ToString(dr["val_2"]);
                ca.val3 = Convert.ToString(dr["val_3"]);
                ca.val4 = Convert.ToString(dr["val_4"]);
                ca.displayval1 = "";
                SessionState._Campaign.actions[ca.campaign_type] = ca;
            }
        }
    }



    private void FillRewardWhom()
    {
        switch (SessionState._Campaign.reward_whom)
        {
            case 1: radio1.Checked = true; break;
            case 2: radio2.Checked = true; break;
            case 3: radio3.Checked = true; break;
        }

        txtRewardWhom_2.Text = Convert.ToString(SessionState._Campaign.reward_whom_2_nth);
        txtRewardWhom_3_1.Text = SessionState._Campaign.reward_whom_3_no_of;
        txtRewardWhom_3_2.Text = SessionState._Campaign.reward_whom_3_max;
    }



    private void GetCampaignBasicDetails()
    {
        SqlCommand cmd = new SqlCommand("sp_select_brands_campaigns");
        cmd.Parameters.AddWithValue("@campaign_id", SessionState.EditId);
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ConnObj.DataSet.Tables[0].Rows[0];
            SessionState._Campaign.campaign_id = Convert.ToInt64(dr["campaign_id"]);
            SessionState._Campaign.brand_id = Convert.ToInt32(dr["brand_id"]);
            SessionState._Campaign.campaign_objective = Convert.ToByte(dr["campaign_objective"]);
            SessionState._Campaign.campaign_name = Convert.ToString(dr["campaign_name"]);
            SessionState._Campaign.overall_budget = Convert.ToInt64(dr["overall_budget"]);
            SessionState._Campaign.max_brandyy_points = Convert.ToDecimal(dr["max_brandyy_points"]);
            SessionState._Campaign.schedule_type = Convert.ToByte(dr["schedule_type"]);
            SessionState._Campaign.campaign_start = Convert.ToDateTime(dr["campaign_start"]);
            SessionState._Campaign.campaign_end = Convert.ToDateTime(dr["campaign_end"]);
            SessionState._Campaign.campaign_status = Convert.ToByte(dr["campaign_status"]);
            SessionState._Campaign.verification_status = Convert.ToByte(dr["verification_status"]);
            SessionState._Campaign.reward_whom = Convert.ToByte(dr["reward_whom"]);
            SessionState._Campaign.reward_whom_2_nth = Convert.ToInt64(dr["reward_whom_2_nth"]);
            SessionState._Campaign.reward_whom_3_no_of = Convert.ToString(dr["reward_whom_3_no_of"]);
            SessionState._Campaign.reward_whom_3_max = Convert.ToString(dr["reward_whom_3_max"]);
            SessionState._Campaign.reward_when_type = Convert.ToByte(dr["reward_when_type"]);
            SessionState._Campaign.reward_when_date = Convert.ToDateTime(dr["reward_when_date"]);
        }
        else
        {
            Response.Redirect(SessionState.WebsiteURLBrand + "brandcampaigns.aspx");
        }
    }
    private void GetCampaignTargetDetails()
    {
        SessionState._Campaign.target_country = "";
        SqlCommand cmd = new SqlCommand("sp_IChooseIT_GetCampaignTarget");
        cmd.Parameters.AddWithValue("@campaign_id", SessionState._Campaign.campaign_id);

        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            SessionState._Campaign.target_all_users = Convert.ToBoolean(ConnObj.DataSet.Tables[0].Rows[0]["all_users"]);
            SessionState._Campaign.target_from_age = Convert.ToInt32(ConnObj.DataSet.Tables[0].Rows[0]["from_age"]);
            SessionState._Campaign.target_to_age = Convert.ToInt32(ConnObj.DataSet.Tables[0].Rows[0]["to_age"]);
            SessionState._Campaign.target_country = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["country"]);
            SessionState._Campaign.target_gender = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["gender"]);
        }
    }
    private void GetPotentialReach()
    {
        var selected = DrpCountry2.GetSelectedIndices().ToList();
        var selectedValues = (from c in selected
                              select DrpCountry2.Items[c].Value).ToList();

        string countries = "0";
        string countries_and = "";
        for (int i = 0; i < selectedValues.Count; i++)
        {
            if (countries == "0") countries = "";
            countries += countries_and + " " + selectedValues[i].ToString() + " ";
            countries_and = ",";
        }

        SqlCommand cmd = new SqlCommand("sp_Admin_GetPotentialReach");
        cmd.Parameters.AddWithValue("@allusers", chkAllUsers2.Checked);
        cmd.Parameters.AddWithValue("@gender", drp_Gender.SelectedValue);
        cmd.Parameters.AddWithValue("@fromage", drp_AgeFrom.SelectedValue);
        cmd.Parameters.AddWithValue("@toage", drp_AgeTill.SelectedValue);
        cmd.Parameters.AddWithValue("@countries", countries);


        ConnObj.GetDataSet(cmd);
        lbkCalReach.Text = "<br>Potential Reach: 0 people";
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            lbkCalReach.Text = "<br>Potential Reach: " + Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["cnt"]) + " people";
        }
    }
    private void AddNewCampaign()
    {


        SqlCommand cmd = new SqlCommand("sp_insert_brands_campaign");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@campaign_objective", SessionState._Campaign.campaign_objective);
        cmd.Parameters.AddWithValue("@campaign_name", txtCampaignName1.Text.Trim());
        cmd.Parameters.AddWithValue("@overall_budget", txtOverallBudget.Text.Trim());
        cmd.Parameters.AddWithValue("@max_brandyy_points", txtMaxBrandyyPoints.Text.Trim());
        cmd.Parameters.AddWithValue("@external_campaign_name", "");
        cmd.Parameters.AddWithValue("@campaign_desc", "");
        cmd.Parameters.AddWithValue("@campaign_status", _CommonVariableCodes.campaign_status_inactive);
        cmd.Parameters.AddWithValue("@verification_status", _CommonVariableCodes.verification_status_inactive);
        cmd.Parameters.AddWithValue("@reward_whom", SessionState._Campaign.reward_whom);
        cmd.Parameters.AddWithValue("@display_reward_to_user", true);
        cmd.Parameters.AddWithValue("@all_actions_compulsory", false);
        cmd.Parameters.AddWithValue("@created_by", SessionState._BrandAdmin.user_id);
        cmd.Parameters.AddWithValue("@modified_by", SessionState._BrandAdmin.user_id);
        cmd.Parameters.AddWithValue("@log_text", "");

        #region schedule dates
        if (rdScheduleDaily.Checked == true)
        {
            cmd.Parameters.AddWithValue("@schedule_type", _CommonVariableCodes.schedule_type_daily);
            cmd.Parameters.AddWithValue("@campaign_start", DateTime.Now);
            cmd.Parameters.AddWithValue("@campaign_end", "");
        }
        else
        {
            cmd.Parameters.AddWithValue("@schedule_type", _CommonVariableCodes.schedule_type_periodic);
            cmd.Parameters.AddWithValue("@campaign_start", Convert.ToDateTime(txtStartDate1.Text.Trim()));
            cmd.Parameters.AddWithValue("@campaign_end", Convert.ToDateTime(txtEndDate1.Text.Trim()));
        }
        #endregion

        #region reward whom
        switch (SessionState._Campaign.reward_whom)
        {
            case 2:
                cmd.Parameters.AddWithValue("@reward_whom_2_nth", Convert.ToInt64(txtRewardWhom_2.Text));
                break;
            case 3:
                cmd.Parameters.AddWithValue("@reward_whom_3_no_of", Convert.ToString(txtRewardWhom_3_1.Text));
                cmd.Parameters.AddWithValue("@reward_whom_3_max", txtRewardWhom_3_2.Text);
                break;
        }
        #endregion

        #region reward when

        cmd.Parameters.AddWithValue("@reward_when_type", Convert.ToByte(drpRewardWhnType.SelectedValue));

        if (Convert.ToByte(drpRewardWhnType.SelectedValue) != 3)
        {
            cmd.Parameters.AddWithValue("@reward_when_date", Convert.ToDateTime("01/01/1900"));
        }
        else
        {
            cmd.Parameters.AddWithValue("@reward_when_date", Convert.ToDateTime(txtRewardWhenDate.Text));
        }
        #endregion

        ConnObj.GetDataTab(cmd);

        if (ConnObj.IsSuccess)
        {
            SessionState.EditId = Convert.ToInt64(ConnObj.DataTab.Rows[0]["campaign_id"]);
            SetSessionData();

            CreateNewCampaignTarget();

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", "setStep4()", true);
            //Response.Redirect(SessionState.WebsiteURLBrand + "brand-create-campaign.aspx?gotostep=4");
        }
    }
    private void UpdateCampaign()
    {

        SqlCommand cmd = new SqlCommand("sp_update_brands_campaign");
        cmd.Parameters.AddWithValue("@campaign_id", SessionState.EditId);
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@campaign_objective", SessionState._Campaign.campaign_objective);
        cmd.Parameters.AddWithValue("@campaign_name", txtCampaignName1.Text.Trim());
        cmd.Parameters.AddWithValue("@overall_budget", txtOverallBudget.Text.Trim());
        cmd.Parameters.AddWithValue("@max_brandyy_points", txtMaxBrandyyPoints.Text.Trim());
        cmd.Parameters.AddWithValue("@external_campaign_name", "");
        cmd.Parameters.AddWithValue("@campaign_desc", "");
        cmd.Parameters.AddWithValue("@campaign_status", _CommonVariableCodes.campaign_status_inactive);
        cmd.Parameters.AddWithValue("@verification_status", _CommonVariableCodes.verification_status_inactive);
        cmd.Parameters.AddWithValue("@reward_whom", SessionState._Campaign.reward_whom);
        cmd.Parameters.AddWithValue("@reward_whom_2_nth", 0);
        cmd.Parameters.AddWithValue("@reward_whom_3_no_of", "");
        cmd.Parameters.AddWithValue("@reward_whom_3_max", "");
        cmd.Parameters.AddWithValue("@display_reward_to_user", true);
        cmd.Parameters.AddWithValue("@all_actions_compulsory", false);
        cmd.Parameters.AddWithValue("@created_by", SessionState._BrandAdmin.user_id);
        cmd.Parameters.AddWithValue("@modified_by", SessionState._BrandAdmin.user_id);
        cmd.Parameters.AddWithValue("@log_text", "");

        #region schedule dates
        if (rdScheduleDaily.Checked == true)
        {
            cmd.Parameters.AddWithValue("@schedule_type", _CommonVariableCodes.schedule_type_daily);
            cmd.Parameters.AddWithValue("@campaign_start", DateTime.Now);
            cmd.Parameters.AddWithValue("@campaign_end", "");
        }
        else
        {
            cmd.Parameters.AddWithValue("@schedule_type", _CommonVariableCodes.schedule_type_periodic);
            cmd.Parameters.AddWithValue("@campaign_start", Convert.ToDateTime(txtStartDate1.Text.Trim()));
            cmd.Parameters.AddWithValue("@campaign_end", Convert.ToDateTime(txtEndDate1.Text.Trim()));
        }
        #endregion

        #region reward whom
        switch (SessionState._Campaign.reward_whom)
        {
            case 2:
                cmd.Parameters.AddWithValue("@reward_whom_2_nth", Convert.ToInt64(txtRewardWhom_2.Text));
                break;
            case 3:
                cmd.Parameters.AddWithValue("@reward_whom_3_no_of", Convert.ToString(txtRewardWhom_3_1.Text));
                cmd.Parameters.AddWithValue("@reward_whom_3_max", txtRewardWhom_3_2.Text);
                break;
        }
        #endregion

        #region reward when

        cmd.Parameters.AddWithValue("@reward_when_type", Convert.ToByte(drpRewardWhnType.SelectedValue));

        if (Convert.ToByte(drpRewardWhnType.SelectedValue) != 3)
        {
            cmd.Parameters.AddWithValue("@reward_when_date", Convert.ToDateTime("01/01/1900"));
        }
        else
        {
            cmd.Parameters.AddWithValue("@reward_when_date", Convert.ToDateTime(txtRewardWhenDate.Text));
        }
        #endregion

        ConnObj.GetDataTab(cmd);

        if (ConnObj.IsSuccess)
        {
            SetSessionData();

            UpdateCampaignTarget();

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", "setStep4()", true);
            Response.Redirect(SessionState.WebsiteURLBrand + "brand_create_campaign.aspx?gotostep=4");
        }
    }

    private void CreateNewCampaignTarget()
    {
        SqlCommand cmd = new SqlCommand("sp_insert_brands_campaign_target");
        cmd.Parameters.AddWithValue("@campaign_id", SessionState._Campaign.campaign_id);


        if (chkAllUsers2.Checked)
        {
            cmd.Parameters.AddWithValue("@all_users", true);
            cmd.Parameters.AddWithValue("@country", "");
            cmd.Parameters.AddWithValue("@from_age", 0);
            cmd.Parameters.AddWithValue("@to_age", 0);
            cmd.Parameters.AddWithValue("@gender", '0');
        }
        else
        {
            cmd.Parameters.AddWithValue("@all_users", false);
            cmd.Parameters.AddWithValue("@country", SetSelectedCountryToSession());
            cmd.Parameters.AddWithValue("@from_age", Convert.ToInt32(drp_AgeFrom.SelectedValue));
            cmd.Parameters.AddWithValue("@to_age", Convert.ToInt32(drp_AgeTill.SelectedValue));
            cmd.Parameters.AddWithValue("@gender", Convert.ToChar(drp_Gender.SelectedValue));
        }

        ConnObj.GetDataTab(cmd);
    }


    private void UpdateCampaignTarget()
    {
        SqlCommand cmd = new SqlCommand("sp_update_brands_campaign_target");
        cmd.Parameters.AddWithValue("@campaign_id", SessionState._Campaign.campaign_id);

        if (chkAllUsers2.Checked)
        {
            cmd.Parameters.AddWithValue("@all_users", true);
            cmd.Parameters.AddWithValue("@country", "");
            cmd.Parameters.AddWithValue("@from_age", 0);
            cmd.Parameters.AddWithValue("@to_age", 0);
            cmd.Parameters.AddWithValue("@gender", '0');
        }
        else
        {
            cmd.Parameters.AddWithValue("@all_users", false);
            cmd.Parameters.AddWithValue("@country", SetSelectedCountryToSession());
            cmd.Parameters.AddWithValue("@from_age", Convert.ToInt32(drp_AgeFrom.SelectedValue));
            cmd.Parameters.AddWithValue("@to_age", Convert.ToInt32(drp_AgeTill.SelectedValue));
            cmd.Parameters.AddWithValue("@gender", Convert.ToChar(drp_Gender.SelectedValue));
        }

        ConnObj.GetDataTab(cmd);
    }



    private string SetSelectedCountryToSession()
    {
        var selected = DrpCountry2.GetSelectedIndices().ToList();
        var selectedValues = (from c in selected
                              select DrpCountry2.Items[c].Value).ToList();

        string countries = "";
        string countries_and = "";
        for (int i = 0; i < selectedValues.Count; i++)
        {
            countries += countries_and + "{" + selectedValues[i].ToString() + "}";
            countries_and = ",";
        }

        return countries;
    }
    private void SetSessionData()
    {
        SessionState._Campaign.campaign_id = Convert.ToInt64(ConnObj.DataTab.Rows[0]["campaign_id"]);
        SessionState._Campaign.campaign_objective = Convert.ToByte(ConnObj.DataTab.Rows[0]["campaign_objective"]);
        SessionState._Campaign.brand_id = Convert.ToInt32(ConnObj.DataTab.Rows[0]["brand_id"]);
        SessionState._Campaign.campaign_name = Convert.ToString(ConnObj.DataTab.Rows[0]["campaign_name"]);
        SessionState._Campaign.overall_budget = Convert.ToInt64(ConnObj.DataTab.Rows[0]["overall_budget"]);
        SessionState._Campaign.max_brandyy_points = Convert.ToDecimal(ConnObj.DataTab.Rows[0]["max_brandyy_points"]);
        SessionState._Campaign.schedule_type = Convert.ToByte(ConnObj.DataTab.Rows[0]["schedule_type"]);
        SessionState._Campaign.campaign_start = Convert.ToDateTime(ConnObj.DataTab.Rows[0]["campaign_start"]);
        SessionState._Campaign.campaign_end = Convert.ToDateTime(ConnObj.DataTab.Rows[0]["campaign_end"]);
        SessionState._Campaign.campaign_status = Convert.ToByte(ConnObj.DataTab.Rows[0]["campaign_status"]);
        SessionState._Campaign.verification_status = Convert.ToByte(ConnObj.DataTab.Rows[0]["verification_status"]);
        SessionState._Campaign.reward_whom = Convert.ToByte(ConnObj.DataTab.Rows[0]["reward_whom"]);
        SessionState._Campaign.reward_whom_2_nth = Convert.ToInt64(ConnObj.DataTab.Rows[0]["reward_whom_2_nth"]);
        SessionState._Campaign.reward_whom_3_no_of = Convert.ToString(ConnObj.DataTab.Rows[0]["reward_whom_3_no_of"]);
        SessionState._Campaign.reward_whom_3_max = Convert.ToString(ConnObj.DataTab.Rows[0]["reward_whom_3_max"]);
    }
    #endregion




    #region protected functions


    protected void btn_Panel2_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            AddNewCampaign();
        }
        else
        {
            lblValidationErrors.Text = "Check page For Incomplete Data";
            lblValidationErrors.Visible = true;
            //ScriptManager.RegisterStartupScript(UpdatePanel_Main, UpdatePanel_Main.GetType(), "HideStatusNotification1", "HideStatusNotification()", true);
        }

    }
    protected void btn_Update_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            UpdateCampaign();
        }
        else
        {
            lblValidationErrors.Text = "Check page For Incomplete Data";
            lblValidationErrors.Visible = true;
            //ScriptManager.RegisterStartupScript(UpdatePanel_Main, UpdatePanel_Main.GetType(), "HideStatusNotification1", "HideStatusNotification()", true);
        }

    }
    protected void btn_ManageActions_Click(object sender, EventArgs e)
    {
        Response.Redirect(SessionState.WebsiteURLBrand + "brand-create-campaign-2.aspx");
        //Response.Redirect(SessionState.WebsiteURL + "brands/brand-create-campaign-3ii.aspx");
    }

    protected void radio1_CheckedChanged(object sender, EventArgs e)
    {
        SessionState._Campaign.reward_whom = 1;
    }
    protected void radio2_CheckedChanged(object sender, EventArgs e)
    {
        SessionState._Campaign.reward_whom = 2;
    }
    protected void radio3_CheckedChanged(object sender, EventArgs e)
    {
        SessionState._Campaign.reward_whom = 3;
    }
    protected void btn_Continue_Click(object sender, EventArgs e)
    {
        Response.Redirect(SessionState.WebsiteURL + "brands/brand-create-campaign-3ii.aspx");
    }
    protected void btn_Panel1_Click(object sender, EventArgs e)
    {

    }
    protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
    {
        divScheduleDates.Visible = true;
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        divScheduleDates.Visible = false;
    }
    protected void drp_AgeTill_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void drp_AgeFrom_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void drpRewardWhnType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToByte(drpRewardWhnType.SelectedValue) == 3)
        {
            divRewardWhenDate.Visible = true;
        }
        else
        {
            divRewardWhenDate.Visible = false;
        }
    }
    protected void valDateRange_ServerValidate(object source, ServerValidateEventArgs args)
    {

        DateTime minDate = DateTime.Parse("2005/01/01");
        DateTime maxDate = DateTime.Parse("9999/12/28");
        DateTime dt;

        args.IsValid = (DateTime.TryParse(args.Value, out dt)
                        && dt <= maxDate
                        && dt >= minDate);

    }
    protected void lnkCalReach_Click(object sender, EventArgs e)
    {
        GetPotentialReach();
    }
    #endregion
    
}
