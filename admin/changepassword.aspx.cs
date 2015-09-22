using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class brands_changepassword : System.Web.UI.Page
{

    public static ConnectionClass ConnObj = null;

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

        if ((!Page.IsPostBack) && (SessionState._BrandyyAdmin != null))
        {
            FirstPos();
        }
        else if (SessionState._BrandyyAdmin != null)
        {
            ;
        }
        else
        {
            Response.Redirect(SessionState.WebsiteURLAdmin);
        }

    }
    #endregion

    private void FirstPos()
    {

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        divAlert.Visible = false;
        SqlCommand cmd = new SqlCommand("sp_Brand_ChangePassword");
        cmd.Parameters.AddWithValue("@user_id", SessionState._BrandyyAdmin.admin_id);
        cmd.Parameters.AddWithValue("@oldpassword", txtold.Text.Trim());
        cmd.Parameters.AddWithValue("@newpassword", txtnew.Text.Trim());
        cmd.Parameters.AddWithValue("@type", "ADMIN");
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
"alert('You password is updated successfully .');", true);
        }
        else
        {
            divAlert.Visible = true;
            lblErrMsg.Text = "Wrong Password";
        }
    }
}