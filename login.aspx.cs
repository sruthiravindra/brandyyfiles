using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class _login : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            FirstPos();
        }
    }
    #endregion


    protected void FirstPos()
    {
        lblLogError.Text = "";
    }
    protected void lnkForgotPass_Click(object sender, EventArgs e)
    {
        divMail.Visible = true;
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        lblLogError.Text = "";
        SqlCommand cmd = new SqlCommand("sp_select_user_master_login");
        cmd.Parameters.AddWithValue("@useremail", txtLogEmail.Text.Trim());
        cmd.Parameters.AddWithValue("@password", txtLogPass.Text.Trim());
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && (ConnObj.DataSet.Tables.Count > 0) && (ConnObj.DataSet.Tables[0].Rows.Count > 0))
        {
            DataRow dr = ConnObj.DataSet.Tables[0].Rows[0];
            SessionState._SignInUser = new SignInUser(Convert.ToInt64(dr["reg_uid"]),
                  Convert.ToString(dr["reg_email"]),
                  Convert.ToString(dr["reg_fname"]),
                  Convert.ToString(dr["reg_lname"]),
                  Convert.ToString(""),
                  Convert.ToString(dr["profile_image_url"]));
            Response.Redirect(SessionState.WebsiteURL + "myaccount.aspx");
        }
        else
        {
            lblLogError.Text = "* Invalid username/password";
        }
    }
    protected void btnMail_Click(object sender, EventArgs e)
    {
        string pass = Convert.ToString(System.Guid.NewGuid()).Replace("-", "");
        SqlCommand cmd = new SqlCommand("sp_select_user_master_password");
        cmd.Parameters.AddWithValue("@useremail", txtMail.Text.Trim());
        cmd.Parameters.AddWithValue("@password", pass);
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0)
        {
            forgotpass.EnquiryTemplates obj = new forgotpass.EnquiryTemplates(txtMail.Text.Trim(), pass, Server.MapPath("~/templates/password.html"));
            txtMail.Text = "";
            divMail.Visible = false;
            lblLogError.Text = "* Your account password has been e-mailed to you.";
        }
        else
        {
            lblLogError.Text = "* Invalid Registration Email";
        }

    }
}