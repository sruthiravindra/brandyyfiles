using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_brandyy_points : System.Web.UI.Page
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
        displayBrandyyPoints();
    }
    #endregion


    protected void displayBrandyyPoints()
    {
        SqlCommand cmd = new SqlCommand("sp_select_admin_brandyy_packages");
        ConnObj.GetDataSet(cmd);
        rpListing.DataSource = ConnObj.DataSet.Tables[0];
        rpListing.DataBind();
    }
    protected void rpListing_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "status")
        {
            SqlCommand cmd = new SqlCommand("sp_update_admin_offerStatus");
            cmd.Parameters.AddWithValue("@package_id", e.CommandArgument); 
            ConnObj.ExecuteNonQuery(cmd);
            displayBrandyyPoints();
        }

        if (e.CommandName == "Edit")
        {
            SqlCommand cmd = new SqlCommand("sp_select_admin_BrandyyPoints");
            cmd.Parameters.AddWithValue("@package_id", e.CommandArgument);
            ConnObj.GetDataSet(cmd);
            hdnID.Value = Convert.ToString(e.CommandArgument);
            txtpoints.Text = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["brandyy_points"]);
            txtusd.Text = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["package_usd"]);
            txtdate.Text = Convert.ToDateTime(ConnObj.DataSet.Tables[0].Rows[0]["package_valid"]).ToString("dd/MM/yyyy");
            btnSave.Text = "UPDATE";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (btnSave.Text == "SAVE")
        {
            SqlCommand cmd = new SqlCommand("sp_Insert_admin_BrandyyPoints");
            cmd.Parameters.AddWithValue("@brandyy_points", txtpoints.Text.Trim());
            cmd.Parameters.AddWithValue("@package_usd", txtusd.Text.Trim());
            cmd.Parameters.AddWithValue("@package_valid", DateTime.ParseExact(txtdate.Text, @"d/M/yyyy",
 System.Globalization.CultureInfo.InvariantCulture));
            ConnObj.ExecuteNonQuery(cmd);
            Response.Redirect(SessionState.WebsiteURLAdmin + "brandyy-points.aspx");
        }
        else
        {
            SqlCommand cmd = new SqlCommand("sp_Update_admin_BrandyyPoints");
            cmd.Parameters.AddWithValue("@brandyy_points", txtpoints.Text.Trim());
            cmd.Parameters.AddWithValue("@package_usd", txtusd.Text.Trim());
            cmd.Parameters.AddWithValue("@package_valid", DateTime.ParseExact(txtdate.Text, @"d/M/yyyy",
    System.Globalization.CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("@package_id", hdnID.Value);
            ConnObj.ExecuteNonQuery(cmd);
            Response.Redirect(SessionState.WebsiteURLAdmin + "brandyy-points.aspx");

        }
    }

}