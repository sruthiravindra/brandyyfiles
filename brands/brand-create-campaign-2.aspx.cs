using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brands_brand_create_campaign_2 : System.Web.UI.Page
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

        if ((!Page.IsPostBack) && (SessionState._BrandAdmin != null))
        {
            FirstPos();
        }
        else if (SessionState._BrandAdmin != null)
        {
            LoadCampaignObjectiveForm();
        }
        else
        {
            Response.Redirect(SessionState.WebsiteURLBrand);
        }

    }
    #endregion

    private void FirstPos()
    {

        ActionFormFirstPos();

    }

    private void ActionFormFirstPos()
    {
        LoadCampaignObjectiveForm();
    }


    #region private functions
    private void LoadCampaignObjectiveForm()
    {
        UserControl uc;
        switch (SessionState._Campaign.campaign_objective)
        {
            case 1:
                uc = (UserControl)Page.LoadControl("uc/create_campaign_1.ascx");
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

    #endregion
}