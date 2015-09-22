using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brands_brand_create_campaign_popup_1 : System.Web.UI.Page
{
    public static ConnectionClass ConnObj = null;
    public DataSet campaign_types = null;
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
            if (SessionState._Campaign.campaign_objective != 0)
            {
                ShowSelectedCampaign(SessionState._Campaign.campaign_objective);
            }
            else
            {
                GetBrandObjectiveTypes(); ;
            }
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
        SessionState._Campaign = new Campaign(0, SessionState._BrandAdmin.brand_id);
        SessionState._Campaign.campaign_objective = id;
        SessionState._Campaign.campaign_name = "";
        UserControl uc;
        switch (id)
        {
            case 1:
                uc = (UserControl)Page.LoadControl("uc2/create_campaign_1.ascx");
                ucc1.Controls.Add(uc);
                break;
            case 2:
                uc = (UserControl)Page.LoadControl("uc/create_campaign_2.ascx");
                ucc1.Controls.Add(uc);
                break;
            case 3:
                uc = (UserControl)Page.LoadControl("uc/create_campaign_3.ascx");
                ucc1.Controls.Add(uc);
                break;
            case 4:
                uc = (UserControl)Page.LoadControl("uc/create_campaign_4.ascx");
                ucc1.Controls.Add(uc);
                break;
            case 5:
                uc = (UserControl)Page.LoadControl("uc/create_campaign_5.ascx");
                ucc1.Controls.Add(uc);
                break;
            case 6:
                uc = (UserControl)Page.LoadControl("uc/create_campaign_6.ascx");
                ucc1.Controls.Add(uc);
                break;
            case 7:
                uc = (UserControl)Page.LoadControl("uc/create_campaign_7.ascx");
                ucc1.Controls.Add(uc);
                break;
            case 8:
                uc = (UserControl)Page.LoadControl("uc/create_campaign_8.ascx");
                ucc1.Controls.Add(uc);
                break;
            case 9:
                uc = (UserControl)Page.LoadControl("uc/create_campaign_9.ascx");
                ucc1.Controls.Add(uc);
                break;
            case 10:
                uc = (UserControl)Page.LoadControl("uc/create_campaign_10.ascx");
                ucc1.Controls.Add(uc);
                break;
        }


    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] commandArgs = btn.CommandArgument.ToString().Split(new char[] { ',' });
        byte id = Convert.ToByte(commandArgs[0]);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "val", "alert('here')", true);
        

        ShowSelectedCampaign(id);
        
    }
}