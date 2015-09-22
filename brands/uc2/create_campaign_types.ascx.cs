using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brands_uc2_create_campaign_types : System.Web.UI.UserControl
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
        GetBrandObjectiveTypes();

        if (SessionState._Campaign.create_campaign_step == 2)
        {
            divCampaingTypes.Attributes["class"] = "col-md-4";
            ShowSelectedCampaign(SessionState._Campaign.campaign_objective);

        }
    }

    private void GetBrandObjectiveTypes()
    {
        SqlCommand cmd = new SqlCommand("sp_Get_Campaign_Type");
        cmd.Parameters.AddWithValue("@id", 0);
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            string grouping = Convert.ToString(SessionState._Campaign.campaign_name2);
            DataTable tbl = ConnObj.DataSet.Tables[0].Select("grouping = '" + grouping + "'").CopyToDataTable();

            Repeater1.DataSource = tbl;
            Repeater1.DataBind();
        }

    }
    private void ShowSelectedCampaign(byte id)
    {
        SessionState.EditId = 0;
        SessionState.EditId_2 = 0;        
        SessionState._Campaign.campaign_objective = id;        
        UserControl uc;
        uc = (UserControl)Page.LoadControl("uc2/create_campaign_" + id + ".ascx");
        ucc1.Controls.Add(uc);
    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        byte id = Convert.ToByte(commandArgs[0]);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "val", "alert('here')", true);

    }

    protected void lnkStep2_Click(object sender, EventArgs e)
    {
        SessionState._Campaign.create_campaign_step = 0;
        Response.Redirect(SessionState.WebsiteURLBrand + "brand-create-campaign.aspx?pos=reload");
    }
}