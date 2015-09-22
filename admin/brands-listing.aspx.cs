using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IchooseIT.DAL;
using System.Data.SqlClient;

public partial class brands_listing : System.Web.UI.Page
{
    
    public int Cnt = 1;

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
        SessionState.EditId = 0;

        SqlCommand cmd = new SqlCommand("sp_select_brands_master");        
        ConnObj.GetDataSet(cmd);

        RepTab.DataSource = ConnObj.DataSet.Tables[0];
        RepTab.DataBind();
    }

    protected void RepTab_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

        if( e.CommandName == "ActivateDeactivate" )
        {
            int id = Convert.ToInt32(commandArgs[0]);
            bool active_inactive = Convert.ToBoolean(commandArgs[1]);
            string name = Convert.ToString(commandArgs[2]);

            SqlCommand cmd = new SqlCommand("sp_update_brands_master");
            cmd.Parameters.AddWithValue("@brand_id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@updated_by", SessionState._BrandyyAdmin.admin_id);
            cmd.Parameters.AddWithValue("@active_flag", !active_inactive);
            ConnObj.GetDataTab(cmd);

            FirstPos();
        }

    }

}