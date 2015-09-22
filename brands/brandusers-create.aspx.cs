using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brands_brandusers_create : System.Web.UI.Page
{
    public static ConnectionClass ConnObj = null;
    public string page_title = "Add New";

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
        FillUserRole();
        if (SessionState.EditId > 0)
        {
            page_title = "Edit";
            FillUserDetails();
            btn_Panel2.Visible = false;
            btn_Update.Visible = true;
            divPassword.Visible = false;
            divPasswordConfirm.Visible = false;
        }

    }

    private void FillUserDetails()
    {
        SqlCommand cmd = new SqlCommand("sp_select_brands_user");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@user_id", SessionState.EditId);
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess == true && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            txtUserName.Text = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["username"]);
            txtEmailID.Text = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["useremail"]);
            drpRole.SelectedValue = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["role_id"]); 
        }
        else
        {
            Response.Redirect( SessionState.WebsiteURLBrand + "brandusers.aspx" );
        }
    }


    private void AddUsers()
    {
        SqlCommand cmd = new SqlCommand("sp_insert_brands_users");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@username", txtUserName.Text.Trim());
        cmd.Parameters.AddWithValue("@useremail", txtEmailID.Text.Trim());
        cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());        
        cmd.Parameters.AddWithValue("@role_id", drpRole.SelectedValue);        
        cmd.Parameters.AddWithValue("@active_flag", true);        
        cmd.Parameters.AddWithValue("@created_by", SessionState._BrandAdmin.user_id);        
        cmd.Parameters.AddWithValue("@updated_by", SessionState._BrandAdmin.user_id);
        ConnObj.GetDataTab(cmd);
        if (ConnObj.IsSuccess)
        {
            Response.Redirect(SessionState.WebsiteURLBrand + "brandusers.aspx");
        }
    }
    private void UpdateUsers()
    {
        SqlCommand cmd = new SqlCommand("sp_update_brands_users");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@user_id", SessionState.EditId);
        cmd.Parameters.AddWithValue("@username", txtUserName.Text.Trim());
        cmd.Parameters.AddWithValue("@useremail", txtEmailID.Text.Trim());
        cmd.Parameters.AddWithValue("@updated_by", SessionState._BrandAdmin.user_id);
        cmd.Parameters.AddWithValue("@role_id", drpRole.SelectedValue);
        ConnObj.GetDataTab(cmd);
        if (ConnObj.IsSuccess)
        {
            Response.Redirect(SessionState.WebsiteURLBrand + "brandusers.aspx");
        }
    }
    protected void btn_Panel2_Click(object sender, EventArgs e)
    {
        if (UserValidation())
        {
            AddUsers();
        }
        else
        {
            lblerror.Visible = true;
        }
    }
    protected void btn_Update_Click(object sender, EventArgs e)
    {
        if (UserValidation())
        {
            UpdateUsers();
        }
        else
        {
            lblerror.Visible = true;
        }
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect(SessionState.WebsiteURLBrand + "brandusers.aspx");
    }

    protected void FillUserRole()
    {
        SqlCommand cmd = new SqlCommand("sp_select_brand_userrole");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        ConnObj.GetDataSet(cmd);
        drpRole.DataSource = ConnObj.DataSet.Tables[0];
        drpRole.DataTextField = "role_name";
        drpRole.DataValueField = "role_id";
        drpRole.DataBind();
        drpRole.Items.Insert(0, new ListItem("Select User Role", "0"));
        drpRole.SelectedIndex = 0;

    }

    protected bool UserValidation()
    {
        lblerror.Visible = false;
        SqlCommand cmd = new SqlCommand("sp_select_brands_useremailvalidation");
        cmd.Parameters.AddWithValue("@useremail",txtEmailID.Text.Trim());
        cmd.Parameters.AddWithValue("@user_id", SessionState.EditId);
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}