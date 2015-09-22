using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class brands_brand_permission : System.Web.UI.Page
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
    protected void FirstPos()
    {
        FillUserRole();
        FillBrandPanel();
        FillPermission();
    }

    protected void FillUserRole()
    {
        SqlCommand cmd = new SqlCommand("sp_select_brand_userrole");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        ConnObj.GetDataSet(cmd);
        drpUser.DataSource = ConnObj.DataSet.Tables[0];
        drpUser.DataTextField = "role_name";
        drpUser.DataValueField = "role_id";
        drpUser.DataBind();
        drpUser.Items.Insert(0, new ListItem("Select User Role", "0"));
        drpUser.SelectedIndex = 0;

    }

    protected void FillBrandPanel()
    {
        SqlCommand cmd = new SqlCommand("sp_select_brand_panel");
        ConnObj.GetDataTab(cmd);
        chkPermission.DataSource = ConnObj.DataTab;
        chkPermission.DataTextField = "panel_name";
        chkPermission.DataValueField = "panel_id";
        chkPermission.DataBind();
    }

    protected void FillPermission()
    {
        chkPermission.Items.Cast<ListItem>().Select(n => n).ToList().ForEach(n => n.Selected = false);
        SqlCommand cmd = new SqlCommand("sp_select_brand_userpermission");
        cmd.Parameters.AddWithValue("@role_id", drpUser.SelectedValue);
        ConnObj.GetDataSet(cmd);
        List<int> list = ConnObj.DataSet.Tables[0].AsEnumerable().Select(dr => dr.Field<int>("panel_id")).ToList();
        chkPermission.Items.Cast<ListItem>().Where(n => list.Contains(Convert.ToInt32(n.Value))).Select(n => n).ToList().ForEach(n => n.Selected = true);
    }
    protected void drpUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPermission();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("sp_insert_brand_UserPermission");
        cmd.Parameters.AddWithValue("@role_id", drpUser.SelectedValue);
        cmd.Parameters.AddWithValue("@panel_id", String.Join(",", (chkPermission.Items.Cast<ListItem>().Where(li => li.Selected).ToList()).Select(v => v.Value).ToList()));
        ConnObj.ExecuteNonQuery(cmd);
        if (ConnObj.IsSuccess)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
"alert('Permission applied.');", true);
            FillPermission();
        }

    }
}