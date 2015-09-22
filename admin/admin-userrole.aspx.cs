using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_admin_userrole : System.Web.UI.Page
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
        FillUserRole();
    }

    protected void FillUserRole()
    {
        SqlCommand cmd = new SqlCommand("sp_select_admin_userrole");        
        ConnObj.GetDataSet(cmd);
        rpRoles.DataSource = ConnObj.DataSet.Tables[0];
        rpRoles.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        divAlert.Visible = false;
        SqlCommand cmd = new SqlCommand("sp_insert_admin_userrole");        
        cmd.Parameters.AddWithValue("@role_name", txtRole.Text.Trim());
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0)
        {
            FillUserRole();
            txtRole.Text = "";
        }
        else
        {
            divAlert.Visible = true;
            lblErrMsg.Text = "Role already exists";
        }
    }
    protected void rpRoles_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            SqlCommand cmd = new SqlCommand("sp_delete_admin_userrole");
            cmd.Parameters.AddWithValue("@role_id", e.CommandArgument);
            ConnObj.ExecuteNonQuery(cmd);
            FillUserRole();

        }
    }
}