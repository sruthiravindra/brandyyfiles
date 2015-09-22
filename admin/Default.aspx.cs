using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_Default : System.Web.UI.Page
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
    }
    #endregion

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("sp_select_admin_user_master_login");
        cmd.Parameters.AddWithValue("@useremail", txtadminusername.Text.Trim());
        cmd.Parameters.AddWithValue("@password", txtadminuserpswd1.Text.Trim());
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && (ConnObj.DataSet.Tables.Count > 0) && (ConnObj.DataSet.Tables[0].Rows.Count > 0))
        {
            DataRow dr = ConnObj.DataSet.Tables[0].Rows[0];
            SessionState._BrandyyAdmin = new BrandyyAdmin(Convert.ToInt32(dr["id"]), Convert.ToString(dr["first_name"]));
            SessionState._BrandyyAdmin.created_date_time = Convert.ToDateTime(dr["created_date_time"]);
            Response.Redirect(SessionState.WebsiteURLAdmin + "homepage.aspx");
        }
        else
        {
            divAlert.Visible = true;
            lblErrMsg.Text = "Invalid username/password";
        }
    }
    protected void lnkForgotPass_Click(object sender, EventArgs e)
    {
        divMail.Visible = true;
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        divAlert.Visible = false;
        string pass = Convert.ToString(System.Guid.NewGuid()).Replace("-", "");
        SqlCommand cmd = new SqlCommand("sp_select_brandy_userPassword");
        cmd.Parameters.AddWithValue("@useremail", txtRegEmail.Text.Trim());
        cmd.Parameters.AddWithValue("@password", pass);
        cmd.Parameters.AddWithValue("@type", "AMDIN");
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0)
        {
            forgotpass.EnquiryTemplates obj = new forgotpass.EnquiryTemplates(txtRegEmail.Text.Trim(), pass, Server.MapPath("~/templates/password.html"));
            txtRegEmail.Text = "";
            divMail.Visible = false;
            divAlert.Visible = true;
            lblErrMsg.Text = "Your account password has been e-mailed to you.";
        }
        else
        {
            divAlert.Visible = true;
            lblErrMsg.Text = "Invalid Registration Email";
        }



    }
}