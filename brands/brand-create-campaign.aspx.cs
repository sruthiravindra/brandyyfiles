using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brands_brand_create_campaign : System.Web.UI.Page
{

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
    }
    private void CreateInstance()
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        CreateInstance();

        //SessionState.EditId = 0;
        //SessionState.EditId_2 = 0;
        //SessionState._Campaign.campaign_objective = 16;
        //UserControl uc;
        //uc = (UserControl)Page.LoadControl("uc2/create_campaign_" + 16 + ".ascx");
        //ucc1.Controls.Add(uc);
        //return;
       
        if ((!Page.IsPostBack) && (SessionState._BrandAdmin != null))
        {
            if (Request.Params["gotostep"] != null)
            {
                if (Request.Params["gotostep"] == "2")
                {
                    SetActiveAs_2();
                }
                else if (Request.Params["gotostep"] == "3")
                {
                    SetActiveAs_3();
                }
                else if (Request.Params["gotostep"] == "4")
                {
                    SetActiveAs_4();
                }
            }
            else
            {
                SetActiveAs_1();
            }
        }
        else if (SessionState._BrandAdmin != null)
        {
            if (current_step.Value == "1")
            {
                SetActiveAs_1();
            }
            else if (current_step.Value == "2")
            {
                SetActiveAs_2();                
            }
            else if (current_step.Value == "3")
            {
                SetActiveAs_3();
            }
            else if (current_step.Value == "4")
            {
                SetActiveAs_4();
            }
        }
        //else if (SessionState._BrandAdmin != null)
        //{
        //    ShowSelectedPos();
        //}
        //else
        //{
        //    Response.Redirect(SessionState.WebsiteURLBrand);
        //}

    }
    #endregion


    private void GetBrandObjectives()
    {
        //UserControl uc;
        //uc = (UserControl)Page.LoadControl("uc2/create_campaign_objectives.ascx");
        //ucc1.Controls.Add(uc);
       
    }
    private void SetActiveAs_1()
    {
        Tab1.Attributes["class"] = "active";
        Tab2.Attributes["class"] = "";
        Tab3.Attributes["class"] = "";
        Tab4.Attributes["class"] = "";
        UserControl uc;
        uc = (UserControl)Page.LoadControl("uc2/create_campaign_objectives.ascx");
        ucc1.Controls.Add(uc);
        MainView.ActiveViewIndex = 0;
    }
    private void SetActiveAs_2()
    {
        Tab1.Attributes["class"] = "";
        Tab2.Attributes["class"] = "active";
        Tab3.Attributes["class"] = "";
        Tab4.Attributes["class"] = "";

        UserControl uc;
        uc = (UserControl)Page.LoadControl("uc2/create_campaign_types.ascx");
        ucc2.Controls.Add(uc);
        MainView.ActiveViewIndex = 1;
    }
    public void SetActiveAs_3()
    {
        Tab1.Attributes["class"] = "";
        Tab2.Attributes["class"] = "";
        Tab3.Attributes["class"] = "active";
        Tab4.Attributes["class"] = "";

        UserControl uc;
        uc = (UserControl)Page.LoadControl("uc2/create_campaign_basic_details.ascx");
        ucc3.Controls.Add(uc);        
        MainView.ActiveViewIndex = 2;
    }
    private void SetActiveAs_4()
    {
        Tab1.Attributes["class"] = "";
        Tab2.Attributes["class"] = "";
        Tab3.Attributes["class"] = "";
        Tab4.Attributes["class"] = "active";

        UserControl uc;
        uc = (UserControl)Page.LoadControl("uc2/create_campaign_reward_details.ascx");
        ucc4.Controls.Add(uc);
        MainView.ActiveViewIndex = 3;
    }
    [System.Web.Services.WebMethod(true)]
    public static bool SetSession(string grouping)
    {
        SessionState.EditId = 0;

        SessionState._Campaign = new Campaign(0, SessionState._BrandAdmin.brand_id);
        SessionState._Campaign.create_campaign_step = 1;
        //SessionState._Campaign.campaign_objective = 1;
        SessionState._Campaign.campaign_name2 = grouping;

        return true;
    }

    [System.Web.Services.WebMethod(true)]
    public static bool setCamapignType(byte id,string name)
    {
        SessionState.EditId = 0;
        SessionState._Campaign.create_campaign_step = 2;        
        SessionState._Campaign.campaign_objective = id;
        SessionState._Campaign.campaign_name = name;
        return true;
    }
    protected void lnk_Step1_Click(object sender, EventArgs e)
    {
        //SetActiveAs_1();
    }
    protected void lnk_Step2_Click(object sender, EventArgs e)
    {
        //SetActiveAs_2();
    }
    protected void lnk_Step3_Click(object sender, EventArgs e)
    {
        //SetActiveAs_3();
        
    }
    protected void lnk_Step4_Click(object sender, EventArgs e)
    {
        //SetActiveAs_4();
    }

    
}
