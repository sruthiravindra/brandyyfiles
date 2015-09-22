using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Drawing;
using System.Data.SqlTypes;
public partial class brands_brandyypoints_package : System.Web.UI.Page
{
    public static ConnectionClass ConnObj = null;
    public Int64 points = 0;
    public Int64 pointsused = 0;
    public Int64 pointsrem = 0;
    public decimal usd = 0;
    public decimal USDused = 0;
    public decimal USDrem = 0;
    
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
        DisplayPoints();
        DisplaytotalPointsUsed();
    }
    private void DisplayPoints()
    {
        SqlCommand cmd = new SqlCommand("sp_select_brandBrandyyPoints");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            points = (ConnObj.DataSet.Tables[0].Rows[0]["brandyy_points"] == DBNull.Value) ? 0 : Convert.ToInt64(ConnObj.DataSet.Tables[0].Rows[0]["brandyy_points"]);
            usd = (ConnObj.DataSet.Tables[0].Rows[0]["package_usd"] == DBNull.Value) ? 0 : Convert.ToInt64(ConnObj.DataSet.Tables[0].Rows[0]["package_usd"]);
        }
    }
    private void DisplaytotalPointsUsed()
    {
        SqlCommand cmd = new SqlCommand("sp_select_brandtotalPointsUsed");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            pointsused = (ConnObj.DataSet.Tables[0].Rows[0]["reward_amount"] == DBNull.Value) ? 0 : Convert.ToInt64(ConnObj.DataSet.Tables[0].Rows[0]["reward_amount"]);
            USDused = (pointsused * usd) / points;
            pointsrem = points - pointsused;
            USDrem = usd - USDused;
        }
    }

    protected void DisplayOffers()
    {
        SqlCommand cmd = new SqlCommand("sp_select_brandyy_packages");
        ConnObj.GetDataSet(cmd);
        rpOffers.DataSource = ConnObj.DataSet.Tables[0];
        rpOffers.DataBind();
    }
    protected void rpOffers_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Package")
        {

            HiddenField hdnlog = (HiddenField)e.Item.FindControl("hdnlog");
            SqlCommand cmd = new SqlCommand("sp_insert_bpPurchase");
            cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
            cmd.Parameters.AddWithValue("@package_id", e.CommandArgument);
            cmd.Parameters.AddWithValue("@user_id", SessionState._BrandAdmin.user_id);
            cmd.Parameters.AddWithValue("@trans_log", hdnlog.Value);
            ConnObj.ExecuteNonQuery(cmd);
            if (ConnObj.IsSuccess)
            {
                Response.Redirect(SessionState.WebsiteURLBrand + "brand_transactions.aspx");
            }
        }
    }
}