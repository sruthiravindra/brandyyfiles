using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brands_brandsMasterPage : System.Web.UI.MasterPage
{
    bool display_verification = true;
    bool display_store_verification = true;
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

        if ((!Page.IsPostBack) && (SessionState._BrandAdmin != null))
        {
            ;
        }
        else if (SessionState._BrandAdmin != null)
        {
            //LoadCampaignObjectiveForm();
        }
        else
        {
            Response.Redirect(SessionState.WebsiteURLBrand);
        }

        // get accessing page
        string page = HttpContext.Current.Request.Url.AbsolutePath;
        string[] page_arr = page.Split('/');
        string arrow = "<i class='fa fa-angle-double-right'></i>";
        switch (page_arr[2])
        {
            case "brandprofile-update.aspx": lblPageTitle.Text = "SETTINGS " + arrow + " BRAND PROFILE SETTINGS"; break;
            case "brandusers.aspx": lblPageTitle.Text = "SETTING " + arrow + " USERS LISTING"; break;
            case "brandusers-create.aspx": lblPageTitle.Text = "SETTINGS " + arrow + " ADD / EDIT USER"; break;
            case "socialmedias.aspx": lblPageTitle.Text = "SETTINGS " + arrow + " PAGES"; break;
            case "socialmediapage-create.aspx": lblPageTitle.Text = "SETTINGS " + arrow + " ADD / EDIT SOCIAL MEDIA ACCOUNT"; break;
            case "brandcampaigns.aspx": lblPageTitle.Text = "MARKETING " + arrow + " CAMPAIGNS"; break;                        
            case "brand-create-campaign.aspx":
                lblPageTitle.Text = "MARKETING " + arrow + " CREATE CAMPAIGN - SELECT CAMPAIGN TYPE"; break;
            case "brand-create-campaign-1.aspx":
                lblPageTitle.Text = "MARKETING " + arrow + " ADD/EDIT CAMPAIGN BASIC SETTINGS"; break;
            case "brand-create-campaign-2.aspx":
                lblPageTitle.Text = "MARKETING " + arrow + " EDIT CAMPAIGNS / MANAGE ACTIONS"; break;
            case "brand-create-campaign-3ii.aspx / MANAGE ACTION POINTS":
                lblPageTitle.Text = "MARKETING " + arrow + " EDIT CAMPAIGNS"; break;
            case "brand-create-campaign-3iii.aspx":
                lblPageTitle.Text = "MARKETING " + arrow + " EDIT CAMPAIGNS / MANAGE COUPONS"; break;
            case "brand-update-campaign-1.aspx":
                lblPageTitle.Text = "MARKETING " + arrow + " UPDATE CAMPAIGNS"; break;
            case "campaignview.aspx":
                lblPageTitle.Text = "MARKETING " + arrow + " CAMPAIGN SUMMARY"; break;
            case "activity-verification.aspx": lblPageTitle.Text = "MARKETING " + arrow + " VERIFY ACTIVITY"; break;
            default: lblPageTitle.Text = "Welcome To "+SessionState._BrandAdmin.brand_name+"'s Admin Panel"; break;
        }
        DisplayMenu();

    }
    #endregion    

    protected void DisplayMenu()
    {
        ConnectionClass ConnObj = new ConnectionClass();
        SqlCommand cmd1 = new SqlCommand("sp_select_brand_userpanelpermission");
        cmd1.Parameters.AddWithValue("@user_id", SessionState._BrandAdmin.user_id);
        ConnObj.GetDataTab(cmd1);

        //CheckIfActivitiesWaitingVerification();
        PrintChildItems(ConnObj.DataTab);
        ConnObj.ReleaseConnection();
        
    }

    private void PrintChildItems( DataTable tbl )
    {
        ConnectionClass ConnObj = new ConnectionClass();
        SqlCommand cmd = new SqlCommand("sp_select_brand_panel");        
        ConnObj.GetDataTab(cmd);

        string close_tag_level_1 = "";
        string close_tag_level_2 = "";
        string visible_row = "";
        foreach (DataRow row in ConnObj.DataTab.Rows)
        {
            visible_row = " style='display:none' ";
            if (tbl.Select("panel_id='"+Convert.ToString(row["panel_id"]+"'")).Length > 0)
            {
                visible_row = " style='display:' ";
            }

            if (Convert.ToInt64(row["panel_id"]) == 24 && display_store_verification == false)
            {
                visible_row = " style='display:none' ";
            }
            if (Convert.ToInt64(row["panel_id"]) == 25 && display_verification == false)
            {
                visible_row = " style='display:none' ";
            }

            if (Convert.ToInt16(row["menu_level"]) == 1)
            {
                lblMenu.Text += close_tag_level_2 + close_tag_level_1;
                close_tag_level_1 = "";
                close_tag_level_2 = "";
                lblMenu.Text += "<div id=\"" + Convert.ToInt64(row["panel_id"]) + "\" runat=\"server\" style=\"text-align:center;background-color:#dbd3d3;border-top:1px solid #fff;line-height:37px\">"
                    + "<strong>" + Convert.ToString(row["panel_name"]) + "</strong></div>";
                if (Convert.ToBoolean(row["has_child"]))
                {
                    lblMenu.Text += "<ul class=\"sidebar-menu\">";
                    close_tag_level_1 = "</ul>" + close_tag_level_1;
                }
            }
            if (Convert.ToInt16(row["menu_level"]) == 2)
            {
                lblMenu.Text += close_tag_level_2;
                close_tag_level_2 = "";
                if (Convert.ToBoolean(row["has_child"]))
                {
                    lblMenu.Text += "<li id=\"" + Convert.ToInt64(row["panel_id"]) + "\" runat=\"server\"  class=\"treeview\" " + visible_row + ">"
                                + "<a href=\"" + SessionState.WebsiteURLBrand + Convert.ToString(row["url"]) + "\">"
                                    + "<i class=\"fa fa-th\"></i> <span>" + Convert.ToString(row["panel_name"]) + "</span>"
                                    +"<i class=\"fa fa-angle-left pull-right\"></i>"
                                + "</a>"                           
                            +"<ul class=\"treeview-menu\">";
                    close_tag_level_2 = "</ul></li>";
                }
                else
                {
                    lblMenu.Text += "<li id=\"" + Convert.ToInt64(row["panel_id"]) + "\" runat=\"server\" " + visible_row + ">"
                                + "<a href=\"" + SessionState.WebsiteURLBrand + Convert.ToString(row["url"]) + "\">"
                                    + "<i class=\"fa fa-th\"></i> <span>" + Convert.ToString(row["panel_name"]) + "</span>"
                                + "</a>"
                            + "</li>";
                }
            }
            if (Convert.ToInt16(row["menu_level"]) == 3)
            {
                lblMenu.Text += "<li id=\"" + Convert.ToInt64(row["panel_id"]) + "\" runat=\"server\" " + visible_row + ">"
                    + "<a href=\"" + SessionState.WebsiteURLBrand + Convert.ToString(row["url"]) + "\">"
                    + "<i class=\"fa fa-angle-double-right\"></i> " + Convert.ToString(row["panel_name"]) + "</a></li>";
            }

            
        }
        close_tag_level_1 = close_tag_level_2 + close_tag_level_1;
        if (close_tag_level_1 != "")
        {
            lblMenu.Text += close_tag_level_1;
        }
    }

    private void CheckIfActivitiesWaitingVerification()
    {
        Int32 b_id = SessionState._BrandAdmin.brand_id;
        SqlCommand cmd = new SqlCommand("sp_brandyy_GetAllPendingActivitiesOverview");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@verification_status", 0);

        ConnectionClass ConnObj1 = new ConnectionClass();
        ConnObj1.GetDataSet(cmd);
        if (ConnObj1.IsSuccess && ConnObj1.DataSet.Tables.Count > 0 && ConnObj1.DataSet.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ConnObj1.DataSet.Tables[0].Rows)
            {
                if (Convert.ToInt64(dr["num_of_pending_activities"]) > 0)
                {
                    display_verification = true;
                    break;
                }
            }
        }
        ConnObj1.ReleaseConnection();
    }
}
