using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class brands_store_purchase_rewards_add : System.Web.UI.Page
{
    public static ConnectionClass ConnObj = null;
   Int64 user_id = 0;

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
        SqlCommand cmd = new SqlCommand("sp_Brand_LoyaltyCampaign");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);

        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            drpCampaigns.DataSource = ConnObj.DataSet.Tables[0];
            drpCampaigns.DataTextField = "campaign_name";
            drpCampaigns.DataValueField = "campaign_action";
            drpCampaigns.DataBind();
        }
    }

    #region save reward
    private void SaveReward()
    {
        Int64 reward_user = 0;
        Int64 points = 0;
        decimal max_brandyy_points;
        string campaign_action = drpCampaigns.SelectedValue;
        SqlCommand cmd = new SqlCommand("sp_Brand_LoyaltyCampaign");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@campaign_id", campaign_action.Split('_')[0]);

        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            reward_user = Convert.ToInt64(ConnObj.DataSet.Tables[0].Rows[0]["reward_user"]);
            max_brandyy_points = Convert.ToInt64(ConnObj.DataSet.Tables[0].Rows[0]["max_brandyy_points"]);
        }
        else
        {
            return;
        }

        
        cmd = new SqlCommand("sp1_brandyy_User_Activities_Insert");
        cmd.Parameters.AddWithValue("@reg_uid", user_id);
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@campaign_id", campaign_action.Split('_')[0]);
        cmd.Parameters.AddWithValue("@action_id", campaign_action.Split('_')[1]);
        cmd.Parameters.AddWithValue("@created_on", DateTime.Now);
        cmd.Parameters.AddWithValue("@pid",  txtInvoiceNumber.Text.Trim());


        points = reward_user * Convert.ToInt64(txtBillAmount.Text.Trim());

        decimal reward_amount = (points > max_brandyy_points) ? max_brandyy_points : points;

        cmd.Parameters.AddWithValue("@reward_amount", reward_amount);
        cmd.Parameters.AddWithValue("@no_of_friends", 0);
        cmd.Parameters.AddWithValue("@no_of_likes", 0);
        cmd.Parameters.AddWithValue("@no_of_shares", 0);
        cmd.Parameters.AddWithValue("@reward_per_user", txtBillAmount.Text.Trim());
        cmd.Parameters.AddWithValue("@reward_on_friend", 0);
        cmd.Parameters.AddWithValue("@reward_on_likes", 0);
        cmd.Parameters.AddWithValue("@reward_on_shares", 0);
        cmd.Parameters.AddWithValue("@returnid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
        ConnObj.GetDataTab(cmd);

        lblRewardBrandyyPoints.Text = "You are rewarded <span style='font-size:24px;'><code>" + reward_amount + " bp</code></span>";

    }
    private bool ValidateDetails()
    {
        // check if user exists
        SqlCommand cmd = new SqlCommand("sp_select_user_exists");
        cmd.Parameters.AddWithValue("@useremail", txtEmailID.Text.Trim());

        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            user_id = Convert.ToInt64(ConnObj.DataSet.Tables[0].Rows[0]["reg_uid"]);
            return true;
        }
        return false;
    }
    #endregion

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ValidateDetails() == true)
        {
            SaveReward();
        }
        else
        {
            lblRewardBrandyyPoints.Text = "Not A Valid User";
        }
    }
}