using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brands_uc2_create_campaign_objectives : System.Web.UI.UserControl
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
            FirstPos();
        }
        else
        {
            Response.Redirect(SessionState.WebsiteURLBrand);
        }

    }
    #endregion

    private void FirstPos()
    {        
        GetBrandObjectives();
    }

    private void GetBrandObjectives()
    {
        SqlCommand cmd = new SqlCommand("sp_Get_Campaign_Objective");
        cmd.Parameters.AddWithValue("@id", 0);
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {            
            Repeater1.DataSource = ConnObj.DataSet.Tables[0];
            Repeater1.DataBind();
        }

    }

    #region onclick events

    protected void RepTab_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "CreateCampaign")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            SessionState.EditId = 0;
            SessionState.EditId_2 = 0;
            SessionState._Campaign = new Campaign(0, SessionState._BrandAdmin.brand_id);
            //SessionState._Campaign.campaign_objective = 1;
            SessionState._Campaign.campaign_name2 = commandArgs[0];
        }
    }

    #endregion

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }   

}