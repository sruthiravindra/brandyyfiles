using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IchooseIT.DAL;
using System.Data;

public partial class brands_brand_update_campaign_1 : System.Web.UI.Page
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

       

    }
    #endregion

    private void FirstPos()
    {
        if (SessionState.EditId > 0)
        {            
            FillCampaignDetails();            
        }
    }

    private void FillCampaignDetails()
    {
        SqlCommand cmd = new SqlCommand("sp_select_brands_campaigns");
        cmd.Parameters.AddWithValue("@campaign_id", SessionState.EditId);
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ConnObj.DataSet.Tables[0].Rows[0];
            txtCampaignName1.Text = Convert.ToString(dr["campaign_name"]);
            txtStartDate1.Text = Convert.ToString(dr["campaign_start"]);
            txtEndDate1.Text = Convert.ToString(dr["campaign_end"]);
            drpDisplayRewardDetails.SelectedValue = Convert.ToBoolean(dr["display_reward_to_user"]) == true ? "1" : "0";
            drpAllActionsComp.SelectedValue = Convert.ToBoolean(dr["all_actions_compulsory"]) == true ? "1" : "0";
            txtMaxBrandyyPoints.Text = Convert.ToString(dr["max_brandyy_points"]);            
            
            if (Convert.ToByte(dr["schedule_type"]) == _CommonVariableCodes.schedule_type_daily)
            {
                rdScheduleDaily.Checked = true;
                divScheduleDates.Visible = false;
            }
            else
            {
                rdScheduleDaily.Checked = false;
                divScheduleDates.Visible = true;
            }
        }
    }
    private void EditNewCampaign()
    {
        SqlCommand cmd = new SqlCommand("sp_update_brands_campaign_active");
        cmd.Parameters.AddWithValue("@campaign_name", txtCampaignName1.Text.Trim());
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@campaign_id", SessionState.EditId);
        cmd.Parameters.AddWithValue("@modified_by", SessionState._BrandAdmin.user_id);
        cmd.Parameters.AddWithValue("@display_reward_to_user", ((drpDisplayRewardDetails.SelectedValue == "1") ? true : false));
        cmd.Parameters.AddWithValue("@all_actions_compulsory", ((drpAllActionsComp.SelectedValue == "1") ? true : false));
        cmd.Parameters.AddWithValue("@max_brandyy_points", txtMaxBrandyyPoints.Text.Trim());        
        int sch_type;
        #region schedule dates
        if (rdScheduleDaily.Checked == true)
        {
            sch_type=_CommonVariableCodes.schedule_type_daily;
            cmd.Parameters.AddWithValue("@schedule_type", _CommonVariableCodes.schedule_type_daily);
            cmd.Parameters.AddWithValue("@campaign_start", DateTime.Now);
            cmd.Parameters.AddWithValue("@campaign_end", "");
        }
        else
        {
            sch_type = _CommonVariableCodes.schedule_type_periodic;
            cmd.Parameters.AddWithValue("@schedule_type", _CommonVariableCodes.schedule_type_periodic);
            cmd.Parameters.AddWithValue("@campaign_start", Convert.ToDateTime(txtStartDate1.Text.Trim()));
            cmd.Parameters.AddWithValue("@campaign_end", Convert.ToDateTime(txtEndDate1.Text.Trim()));
        }
        #endregion

        ConnObj.GetDataTab(cmd);

        if (ConnObj.IsSuccess)
        {            
            lblErrorMsg.Text = "Campaign Updated Successfully";            
        }
    }
    protected void btn_Panel2_Click(object sender, EventArgs e)
    {
        if (SessionState.EditId > 0)
        {
            EditNewCampaign();
        }        
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
    protected void btn_Actions_Click(object sender, EventArgs e)
    {
        Response.Redirect(SessionState.WebsiteURL + "brands/brand-create-campaign-2.aspx");
    }
}