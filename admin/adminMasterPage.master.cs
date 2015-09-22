using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_adminMasterPage : System.Web.UI.MasterPage
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

        if ((!Page.IsPostBack) && (SessionState._BrandyyAdmin != null))
        {
            ;
        }
        else if (SessionState._BrandyyAdmin != null)
        {
            //LoadCampaignObjectiveForm();
        }
        else
        {
            Response.Redirect(SessionState.WebsiteURLAdmin);
        }

        // get accessing page
        string page = HttpContext.Current.Request.Url.AbsolutePath;
        string[] page_arr = page.Split('/');
        switch (page_arr[2])
        {
            case "brandcampaigns.aspx": lblPageTitle.Text = "CAMPAIGNS"; break;
            case "brandpages.aspx": lblPageTitle.Text = "PAGES"; break;
            case "brand-create-page.aspx": lblPageTitle.Text = "PAGES"; break;
            case "brand-create-campaign-1.aspx":
                lblPageTitle.Text = "ADD/EDIT CAMPAIGNS"; break;
            case "brand-create-campaign-2.aspx":
                lblPageTitle.Text = "EDIT CAMPAIGNS / MANAGE ACTIONS"; break;
            case "brand-create-campaign-3ii.aspx / MANAGE ACTION POINTS":
                lblPageTitle.Text = "EDIT CAMPAIGNS"; break;
            case "brand-create-campaign-3iii.aspx":
                lblPageTitle.Text = "EDIT CAMPAIGNS / MANAGE COUPONS"; break;
            case "brand-update-campaign-1.aspx":
                lblPageTitle.Text = "UPDATE CAMPAIGNS"; break;
            case "campaignview.aspx":
                lblPageTitle.Text = "CAMPAIGN SUMMARY"; break;
            case "activity-verification.aspx": lblPageTitle.Text = "VERIFY ACTIVITY"; break;
            default: lblPageTitle.Text = "WELCOME TO BRANDS ADMIN PANEL"; break;
        }
        DisplayMenu();
        //test.Text = page;
        lblbrandname.Text = SessionState._BrandyyAdmin.admin_name;

    }
    #endregion    
    protected void DisplayMenu()
    {

        ConnectionClass ConnObj = new ConnectionClass();
        SqlCommand cmd1 = new SqlCommand("sp_select_admin_userpanelpermission");
        cmd1.Parameters.AddWithValue("@user_id", SessionState._BrandyyAdmin.admin_id);
        ConnObj.GetDataTab(cmd1);
        foreach (DataRow row in ConnObj.DataTab.Rows)
        {
            switch (Convert.ToString(row["panle_srtName"]))
            {
                //case "a1":
                //    a1.Visible = true;
                //    acc.Visible = true;
                //    break;
                //case "a2":
                //    a2.Visible = true;
                //    acc.Visible = true;
                //    break;
                //case "m1":
                //    m1.Visible = true;
                //    mark.Visible = true;
                //    break;
                //case "m2":
                //    m2.Visible = true;
                //    mark.Visible = true;
                //    break;
                //case "m3":
                //    m3.Visible = true;
                //    mark.Visible = true;
                //    break;
                case "s1":
                    s1.Visible = true;
                    stng1.Visible = true;
                    break;
                case "s2":
                    s2.Visible = true;
                    stng1.Visible = true;
                    break;
                //case "s3":
                //    s3.Visible = true;
                //    stng.Visible = true;
                //    break;
                //case "s4":
                //    s4.Visible = true;
                //    stng.Visible = true;
                //    break;
                //case "c1":
                //    c1.Visible = true;
                //    break;
                //case "v1":
                //    v1.Visible = true;
                //    break;
                default:
                    break;
            }
        }
    }
}
