using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IchooseIT.DAL;
using System.IO;
public partial class brands_campaignview : System.Web.UI.Page
{    
    public string reward_type_text = "";
    public string reward_text = "";
    public string target_all_users = "";
    public string[] headers_gb = new string[1];
    
    ConnectionClass ConnObj = null;
    public static string displaycountries = "";

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
        else if (SessionState._Campaign == null)
        {
            Response.Redirect(SessionState.WebsiteURLBrand + "brandcampaigns.aspx");
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
        GetCampaignBasicDetails();
        GetCampaignActionDetails();        
        GetCampaignTotalCnts();
        GetCampaignActivitiesDetails(10,10);
    }

    private void GetCampaignBasicDetails()
    {
        // get campaign objective            
        SqlCommand cmd = new SqlCommand("sp_Brand_GetCampaigns_Summary");
        cmd.Parameters.AddWithValue("@brand_id", Convert.ToInt32(SessionState._Campaign.brand_id));
        cmd.Parameters.AddWithValue("@campaign_id", Convert.ToInt64(SessionState._Campaign.campaign_id));
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            SessionState._Campaign.campaign_objective = Convert.ToByte( ConnObj.DataSet.Tables[0].Rows[0]["campaign_objective"] );
            SessionState._Campaign.reward_whom = Convert.ToByte(ConnObj.DataSet.Tables[0].Rows[0]["reward_whom"]);                        
            if (Convert.ToByte(ConnObj.DataSet.Tables[0].Rows[0]["schedule_type"]) == 2)
            {
                SessionState._Campaign.campaign_end = Convert.ToDateTime(ConnObj.DataSet.Tables[0].Rows[0]["campaign_end"]);
            }
            SessionState._Campaign.campaign_status = Convert.ToByte(ConnObj.DataSet.Tables[0].Rows[0]["campaign_status"]);
            Repeater1.DataSource = ConnObj.DataSet.Tables[0];
            Repeater1.DataBind();
        }
    }

    private void GetCampaignActionDetails()
    {
        string[] headers = new string[1];
        Actions_Reward_To _Actions_Reward_To = new Actions_Reward_To();
        string possible_cols_string = _Actions_Reward_To.campaign_objective_settings[SessionState._Campaign.campaign_objective];
        headers = possible_cols_string.Split(',');
        headers_gb = headers;
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("header", typeof(string)));
        dt.Columns.Add(new DataColumn("id", typeof(int)));
        dt.Columns.Add(new DataColumn("column_id", typeof(int)));
        dt.Columns.Add(new DataColumn("visiblestate", typeof(bool)));

        for (int i = 1; i < _Actions_Reward_To.column_headers.Length; i++)
        {
            dt.Rows.Add(_Actions_Reward_To.column_headers[i],
                i, i,
                ((Array.IndexOf(headers, Convert.ToString(i)) > -1) ? true : false)
                );
        }

        repTab_header.DataSource = dt;
        repTab_header.DataBind();

        byte ct=0;
        SqlCommand cmd = new SqlCommand("sp_Brand_GetCampaign_Actions");
        cmd.Parameters.AddWithValue("@campaign_id", SessionState._Campaign.campaign_id);
        cmd.Parameters.AddWithValue("@campaign_type", ct);

        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            repTab_ActionNames.DataSource = ConnObj.DataSet.Tables[0];
            repTab_ActionNames.DataBind();  
        }  
    }    

    private void GetCampaignTargetDetails()
    {
        SessionState._Campaign.target_country = "";        
        SqlCommand cmd = new SqlCommand("sp_IChooseIT_GetCampaignTarget");
        cmd.Parameters.AddWithValue("@campaign_id", SessionState._Campaign.campaign_id);

        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToBoolean(ConnObj.DataSet.Tables[0].Rows[0]["all_users"]) == true)
            {
                target_all_users = "All Users";
                return;
            }

            target_all_users = "";

            string country_str = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["country"]);

            if ((country_str.Trim() != "") && (country_str != null))
            {
                country_str = country_str.Replace('{', ' ');
                country_str = country_str.Replace('}', ' ');

                // get the countries for the given list
                cmd = new SqlCommand("sp_Admin_GetCountry");
                cmd.Parameters.AddWithValue("@countries", country_str);
                ConnObj.GetDataSet(cmd);
                if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ConnObj.DataSet.Tables[0].Rows)
                    {
                        SessionState._Campaign.target_country += "<span class='label label-green mrs'>" + dr["country_name"] + "</span>";
                    }
                }
            }           
        }
    }

    private void GetCampaignTotalCnts()
    {
        // get campaign objective            
        SqlCommand cmd = new SqlCommand("sp_Brand_GetCampaigns_Cnt_Summary");
        cmd.Parameters.AddWithValue("@brand_id", Convert.ToInt32(SessionState._Campaign.brand_id));
        cmd.Parameters.AddWithValue("@campaign_id", Convert.ToInt64(SessionState._Campaign.campaign_id));
        cmd.Parameters.AddWithValue("@reward_type", Convert.ToByte(SessionState._Campaign.reward_type));
        
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            repTab_Cnts.DataSource = ConnObj.DataSet.Tables[0];
            repTab_Cnts.DataBind();
        }
    }
    private void GetCampaignActivitiesDetails(int verification_status, int reward_status)
    {
        SqlCommand cmd = new SqlCommand("sp_brandyy_GetCampaignDefaultActivities");
        cmd.Parameters.AddWithValue("@campaign_id", SessionState._Campaign.campaign_id);
        cmd.Parameters.AddWithValue("@verification_status", verification_status);
        cmd.Parameters.AddWithValue("@reward_status", reward_status);
        cmd.Parameters.AddWithValue("@reward_date", SessionState._Campaign.reward_date);

        ConnObj.GetDataSet(cmd);
        RepTab.DataSource = null;
        RepTab.DataBind();
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            RepTab.DataSource = ConnObj.DataSet.Tables[0];
            RepTab.DataBind();
        }

    }

    protected void repTab_ActionNames_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            int campaign_type = Convert.ToInt32(drv["id"]);

            Control divControl = e.Item.FindControl("action_details"); 
            
            switch( campaign_type )
            {
                case 1: 
                case 2:
                case 17: 
                divControl.Controls.Add(new LiteralControl("<span> " + Convert.ToString(drv["user_display_name"]) + "</span>" +
                    "<br><span>Page: " + drv["action_page"] + "</span>"));
                    break;
                case 3:
                case 4:                
                case 5:
                case 6:
                case 18: divControl.Controls.Add(new LiteralControl("<span> " + Convert.ToString(drv["user_display_name"]) + "</span>" +
                    "<br><span>Post : <a href='" + drv["val_4"] + "' target='_blank'> "+drv["val_3"]+" </a></span>"));
                    break;
                case 8: divControl.Controls.Add(new LiteralControl("<span><a href='" + drv["val_2"] + "' target='_blank'> Location </a></span>"));
                    break;
                case 9:
                case 10:
                case 19: divControl.Controls.Add(new LiteralControl("<span>" + Convert.ToString(drv["user_display_name"]) + "</span>" +
                    "<br><span>Post Content with hastag </span>"
                    + "<br><span class='bg-info'>@" + drv["val_4"] + " " +drv["val_2"] + " " + drv["val_3"] + "<bR>" + drv["val_3"] + "</span>"));
                    break;
                case 13: divControl.Controls.Add(new LiteralControl("<span> " + Convert.ToString(drv["user_display_name"]) + "</span>" +
                     "<br><span>Page: " + drv["action_page"] + "</span>"));
                    break;
                case 16: divControl.Controls.Add(new LiteralControl("<span>" + Convert.ToString(drv["user_display_name"]) + "</span>" 
                          + "<div class=\"flexslider\"><ul class=\"slides\">"
                          + LoadImageGallery()
                          + "</ul></div>"));
                                break;
            }
            
            Repeater Repeater2 = (Repeater)e.Item.FindControl("repTab_content");

            string[] headers = new string[1];
            Actions_Reward_To _Actions_Reward_To = new Actions_Reward_To();
            if (SessionState._Campaign.reward_type == 3)
            {
                ;
            }
            else
            {
                //string possible_cols_string = _Actions_Reward_To.campaign_settings[campaign_type];
                //headers = possible_cols_string.Split(',');

                headers = headers_gb; // changed to always pick the total columns so that the borders are displayed properly
            }



            // get the campaign type settings from database
            string[] setdata = FillData(campaign_type);
           

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("header", typeof(string)));
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("visiblestate", typeof(bool)));
            dt.Columns.Add(new DataColumn("data", typeof(string)));

            for (int i = 1; i < _Actions_Reward_To.column_headers.Length; i++)
            {
                dt.Rows.Add(_Actions_Reward_To.column_headers[i],
                    i,
                    ((Array.IndexOf(headers, Convert.ToString(i)) > -1) ? true : false),
                    setdata[i]
                    );
            }



            Repeater2.DataSource = dt;
            Repeater2.DataBind();
        }
    }

    private string LoadImageGallery()
    {

        string imgs = "";
        string path = Server.MapPath("~/brands/campaigns/16/" + Convert.ToString(SessionState._BrandAdmin.brand_id) + "/" + Convert.ToString(SessionState._Campaign.campaign_id));
        if (Directory.Exists(path))
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/brands/campaigns/16/" + Convert.ToString(SessionState._BrandAdmin.brand_id) + "/" + Convert.ToString(SessionState._Campaign.campaign_id)));            
            foreach (string filePath in filePaths)
            {
                string fileName = Path.GetFileName(filePath);
                imgs += "<li><img class='thumbnail' src='" + "campaigns/16/" + Convert.ToString(SessionState._BrandAdmin.brand_id) + "/" + Convert.ToString(SessionState._Campaign.campaign_id) + "/" + fileName + "' /></li>";            
            }
        }

        return imgs;
    }

    // get if any action created for this campaign
    private string[] FillData(int campaign_type)
    {
        string[] setdata = new string[10];
        setdata[1] = "0";
        setdata[2] = "0";
        setdata[3] = "0";
        setdata[4] = "0";
        setdata[5] = "0";
        setdata[6] = "0";
        setdata[7] = "0";
        

        SqlCommand cmd = new SqlCommand("sp_Brand_GetCampaign_Actions");
        cmd.Parameters.AddWithValue("@campaign_id", Convert.ToInt64(SessionState._Campaign.campaign_id));
        cmd.Parameters.AddWithValue("@campaign_type", campaign_type);
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            
            DataRow dr = ConnObj.DataSet.Tables[0].Rows[0];
            setdata[1] = Convert.ToString( Convert.ToInt64( dr["reward_user"] ));
            setdata[2] = Convert.ToString(Convert.ToInt64( dr["reward_per_friend"]));
            setdata[3] = Convert.ToString(Convert.ToInt64( dr["reward_per_like"]));
            setdata[4] = Convert.ToString(Convert.ToInt64( dr["reward_per_share"]));
        }

        return setdata;
    }    

    #region protected functions
    protected void lnk_UserActivity_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        SessionState.UserID = Convert.ToInt64(btn.CommandArgument);
        SessionState.EditId = 0;
        Response.Redirect(SessionState.WebsiteURLBrand + "activity-listing.aspx");
    }
    protected void lnk_User_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        SessionState.EditId = Convert.ToInt64(btn.CommandArgument);
        Response.Redirect(SessionState.WebsiteURLBrand + "activity-listing.aspx");
    }
    
    protected void drpFilterVerified_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCampaignActivitiesDetails(Convert.ToInt16(drpFilterVerified.SelectedValue), Convert.ToInt16(drpFilterReward.SelectedValue));

    }
    protected void drpFilterReward_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCampaignActivitiesDetails(Convert.ToInt16(drpFilterVerified.SelectedValue), Convert.ToInt16(drpFilterReward.SelectedValue));
    }
    #endregion


    protected void btnSendVerification_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        
        Int64 id = SessionState._Campaign.campaign_id;
        
        SqlCommand cmd = new SqlCommand("sp_update_brands_campaign_verification");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@campaign_id", id);
        cmd.Parameters.AddWithValue("@updated_by", SessionState._BrandAdmin.user_id);
        cmd.Parameters.AddWithValue("@verification_status", 1);
        cmd.Parameters.AddWithValue("@log_text", "");
        ConnObj.GetDataTab(cmd);
        btn.Text = "&nbsp;Successfully sent for verification&nbsp;";
        btn.CssClass = "bg-blue small";
    }
}