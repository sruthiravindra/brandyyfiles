using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brands_brandusers : System.Web.UI.Page
{
    public static ConnectionClass ConnObj = null;
    public int Cnt;

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
        LoadUsers();
    }
    private void LoadUsers()
    {
        SessionState.EditId = 0;
        Cnt = 1;
        //divBrandWelcome.InnerHtml = "Welcome (" + SessionState._BrandAdmin.brand_name + "), All Campaigns";

        SqlCommand cmd = new SqlCommand("sp_Brand_GetUsers");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {

            RepTab.DataSource = ConnObj.DataSet.Tables[0];
            RepTab.DataBind();
        }
        else
        {
            lblNoCampaigns.Visible = true;
        }
    }

    protected void btn_Status_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        Int64 id = Convert.ToInt64(commandArgs[0]);
        bool status = (Convert.ToBoolean(commandArgs[1]) == true) ? false : true ;

        SqlCommand cmd = new SqlCommand("sp_update_brands_user_status");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@user_id", id );
        cmd.Parameters.AddWithValue("@active_flag", status);
        cmd.Parameters.AddWithValue("@updated_by", SessionState._BrandAdmin.user_id);        
        ConnObj.GetDataTab(cmd);

        if (ConnObj.IsSuccess)
        {            
            LoadUsers();
        }
    }
    protected void btn_Edit_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        Int64 id = Convert.ToInt64(commandArgs[0]);
        SessionState.EditId = id;
        Response.Redirect(SessionState.WebsiteURLBrand + "brandusers-create.aspx");
    }
    
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        Response.Redirect( SessionState.WebsiteURLBrand + "brandusers-create.aspx");
    }
}