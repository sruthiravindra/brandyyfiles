using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using IchooseIT.DAL;


public partial class brands_activity_listing : System.Web.UI.Page
{
    public static ConnectionClass ConnObj = null;
    public int Cnt;
    string medias = "";
    public byte reward_type = 0;
    public Int64 current_activity_id = 0;
    public byte current_media_id = 0;

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
        if (SessionState._BrandAdmin == null)
        {
            Response.Redirect(SessionState.WebsiteURLBrand);
        }
        else if ((!Page.IsPostBack))
        {
            FirstPos();
        }
        else
        {
            // Response.Redirect(SessionState.WebsiteURL + "login.aspx");
        }

    }
    #endregion

    private void FirstPos()
    {
        GetActivity();

    }

    private void GetActivity()
    {
        SqlCommand cmd = new SqlCommand("sp_brandyy_GetAllVerifyActivityDetails");
        cmd.Parameters.AddWithValue("@status", 10);
        cmd.Parameters.AddWithValue("@brand_id", 0);
        cmd.Parameters.AddWithValue("@activity_id", SessionState.EditId);
        cmd.Parameters.AddWithValue("@start_row", 0);
        cmd.Parameters.AddWithValue("@end_row", 1);
        cmd.Parameters.AddWithValue("@reg_uid", 0);
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet != null && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {


            DataRow dr = ConnObj.DataSet.Tables[0].Rows[0];
            Int64 selected_user = Convert.ToInt64(dr["reg_uid"]);

            GetUserProfile(selected_user);
            GetCampaignBasicDetails(Convert.ToInt64(dr["campaign_id"]), Convert.ToInt32(dr["brand_id"]));
            GetCurrentActivityDetails(Convert.ToInt64(dr["id"]), Convert.ToInt64(dr["campaign_id"]), Convert.ToInt32(dr["brand_id"]), Convert.ToInt64(dr["reg_uid"]),
                Convert.ToInt64(dr["action_id"]), Convert.ToByte(dr["campaign_type"]));
            GetPreviousActivity(selected_user);

        }
    }

    private void GetUserProfile(Int64 selected_user)
    {
        SqlCommand cmd = new SqlCommand("sp_GetUserProfile");
        cmd.Parameters.AddWithValue("@reg_uid", selected_user);
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet != null && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            medias = ConnObj.DataSet.Tables[0].Rows[0]["social_medias_registered"].ToString();

            RepTab.DataSource = ConnObj.DataSet.Tables[0];
            RepTab.DataBind();
        }
    }

    private void GetCampaignBasicDetails(Int64 campaign_id, Int32 brand_id)
    {
        // get campaign objective            
        SqlCommand cmd = new SqlCommand("sp_Brand_GetCampaigns_Summary");
        cmd.Parameters.AddWithValue("@brand_id", brand_id);
        cmd.Parameters.AddWithValue("@campaign_id", campaign_id);
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {            
            RepTab_CampaignDetails.DataSource = ConnObj.DataSet.Tables[0];
            RepTab_CampaignDetails.DataBind();
        }


    }

    private void GetCurrentActivityDetails(Int64 activity_id, Int64 campaign_id, Int32 brand_id, Int64 reg_uid, Int64 action_id, byte campaign_type)
    {
        if (campaign_type == 9)
        {
            GetTwActivity(activity_id, campaign_id, brand_id, reg_uid, action_id);
        }
        else if (campaign_type == 10)
        {
            GetFBActivity(activity_id, campaign_id, brand_id, reg_uid, action_id);
        }
        else if (campaign_type == 19)
        {
            GetInstaActivity(activity_id, campaign_id, brand_id, reg_uid, action_id);
        }
        else
        {
            GetActivity(activity_id, campaign_id, brand_id, reg_uid, action_id);
        }
    }
    private void GetFBActivity(Int64 activity_id, Int64 campaign_id, Int32 brand_id, Int64 reg_uid, Int64 action_id)
    {
        // get campaign objective            
        SqlCommand cmd = new SqlCommand("sp_brandyy_GetSMPost_Details");
        cmd.Parameters.AddWithValue("@post_id", activity_id);
        cmd.Parameters.AddWithValue("@brand_id", brand_id);
        cmd.Parameters.AddWithValue("@campaign_id", campaign_id);
        cmd.Parameters.AddWithValue("@reg_uid", reg_uid);
        cmd.Parameters.AddWithValue("@action_id", action_id);
        cmd.Parameters.AddWithValue("@Action", "Facebook");
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            current_activity_id = Convert.ToInt64(ConnObj.DataSet.Tables[0].Rows[0]["activity_id"]);
            RepTab_FBCurrentActivity.DataSource = ConnObj.DataSet.Tables[0];
            RepTab_FBCurrentActivity.DataBind();
            current_media_id = 1;
        }
    }
    private void GetTwActivity(Int64 activity_id, Int64 campaign_id, Int32 brand_id, Int64 reg_uid, Int64 action_id)
    {
        // get campaign objective            
        SqlCommand cmd = new SqlCommand("sp_brandyy_GetSMPost_Details");
        cmd.Parameters.AddWithValue("@post_id", activity_id);
        cmd.Parameters.AddWithValue("@brand_id", brand_id);
        cmd.Parameters.AddWithValue("@campaign_id", campaign_id);
        cmd.Parameters.AddWithValue("@reg_uid", reg_uid);
        cmd.Parameters.AddWithValue("@action_id", action_id);
        cmd.Parameters.AddWithValue("@Action", "Twitter");
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            current_activity_id = Convert.ToInt64(ConnObj.DataSet.Tables[0].Rows[0]["activity_id"]);
            RepTab_FBCurrentActivity.DataSource = ConnObj.DataSet.Tables[0];
            RepTab_FBCurrentActivity.DataBind();
            current_media_id = 2;
        }
    }
    private void GetInstaActivity(Int64 activity_id, Int64 campaign_id, Int32 brand_id, Int64 reg_uid, Int64 action_id)
    {
        // get campaign objective            
        SqlCommand cmd = new SqlCommand("sp_brandyy_GetSMPost_Details");
        cmd.Parameters.AddWithValue("@post_id", activity_id);
        cmd.Parameters.AddWithValue("@brand_id", brand_id);
        cmd.Parameters.AddWithValue("@campaign_id", campaign_id);
        cmd.Parameters.AddWithValue("@reg_uid", reg_uid);
        cmd.Parameters.AddWithValue("@action_id", action_id);
        cmd.Parameters.AddWithValue("@Action", "Insta");
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            current_activity_id = Convert.ToInt64(ConnObj.DataSet.Tables[0].Rows[0]["activity_id"]);
            RepTab_FBCurrentActivity.DataSource = ConnObj.DataSet.Tables[0];
            RepTab_FBCurrentActivity.DataBind();
            current_media_id = 3;
        }
    }
    private void GetActivity(Int64 activity_id, Int64 campaign_id, Int32 brand_id, Int64 reg_uid, Int64 action_id)
    {
        // get campaign objective            
        SqlCommand cmd = new SqlCommand("sp_brandyy_GetGeneralActivity_Details");
        cmd.Parameters.AddWithValue("@activity_id", activity_id);
        cmd.Parameters.AddWithValue("@brand_id", brand_id);
        cmd.Parameters.AddWithValue("@campaign_id", campaign_id);
        cmd.Parameters.AddWithValue("@reg_uid", reg_uid);
        cmd.Parameters.AddWithValue("@action_id", action_id);
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            current_activity_id = Convert.ToInt64(ConnObj.DataSet.Tables[0].Rows[0]["activity_id"]);
            RepTab_GCurrentActivity.DataSource = ConnObj.DataSet.Tables[0];
            RepTab_GCurrentActivity.DataBind();
        }
    }

    private void GetPreviousActivity(Int64 selected_user)
    {
        SqlCommand cmd = new SqlCommand("sp_brandyy_GetUserActivities");
        cmd.Parameters.AddWithValue("@reg_uid", selected_user);

        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            RepTab_PreviousActivities.DataSource = ConnObj.DataSet.Tables[0];
            RepTab_PreviousActivities.DataBind();
        }
    }

    private void FirstPos_1()
    {

        Cnt = 1;

        SqlCommand cmd = new SqlCommand("sp_IchooseIT_GetAllActivities");
        cmd.Parameters.AddWithValue("@status", 0);
        cmd.Parameters.AddWithValue("@brand_id", 0);
        cmd.Parameters.AddWithValue("@activity_id", 0);
        if (SessionState.UserID > 0)
        {
            cmd.Parameters.AddWithValue("@reg_uid", SessionState.UserID);
        }
        else
        {
            cmd.Parameters.AddWithValue("@reg_uid", 0);
        }
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet != null && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            decimal deci = 0.0000m;
            foreach (DataRow dr in ConnObj.DataSet.Tables[0].Rows)
            {
                /*string test = Convert.ToString( dr["c_code"] );
                if ( ( test == null ) || (test=="")) {
                    dr["coupon_code"] = 0; 
                }*/
                // case: its not compulsory to purchase //  
                if ((dr["coupon_code_percent"].ToString().Trim() == "0") && (Convert.ToDecimal(dr["coupon_code"]) == deci))
                {
                    dr["c_code"] = "";
                    dr["coupon_code"] = 0;

                    if (dr["reward_likes_unit"].ToString().Trim() == "AED") dr["fb_likes_rewards"] = Convert.ToDecimal(dr["fb_likes_rewards"]).ToString("0.000");
                    else dr["fb_likes_rewards"] = Convert.ToDecimal(dr["fb_likes_percent"]).ToString("0.000");

                    if (dr["reward_friends_unit"].ToString().Trim() == "AED") dr["fb_friends_rewards"] = Convert.ToDecimal(dr["fb_friends_rewards"]).ToString("0.000");
                    else dr["fb_friends_rewards"] = Convert.ToDecimal(dr["fb_friends_percent"]).ToString("0.000");
                }
                else
                {
                    // case : user has not purchased
                    string test = Convert.ToString(dr["c_code"]);
                    if ((test == null) || (test == ""))
                    {
                        dr["c_code"] = "";
                        dr["coupon_code"] = 0;
                        int test1 = Convert.ToInt32(dr["brand_id"]);
                        decimal test2 = Convert.ToDecimal(dr["coupon_code"]);
                        if (dr["reward_likes_unit"].ToString().Trim() == "AED") dr["fb_likes_rewards"] = Convert.ToDecimal(dr["fb_likes_rewards"]).ToString("0.000");
                        else dr["fb_likes_rewards"] = Convert.ToDecimal(dr["fb_likes_percent"]).ToString("0.000");

                        if (dr["reward_friends_unit"].ToString().Trim() == "AED") dr["fb_friends_rewards"] = Convert.ToDecimal(dr["fb_friends_rewards"]).ToString("0.000");
                        else dr["fb_friends_rewards"] = Convert.ToDecimal(dr["fb_friends_percent"]).ToString("0.000");
                    }
                    else
                    {
                        if (dr["reward_code_flike_unit"].ToString().Trim() == "AED") dr["fb_likes_rewards"] = Convert.ToDecimal(dr["code_fb_likes_aed_rewards"]).ToString("0.000");
                        else dr["fb_likes_rewards"] = Convert.ToDecimal(dr["code_fb_likes_percent_rewards"]).ToString("0.000");

                        if (dr["reward_code_ffriend_unit"].ToString().Trim() == "AED") dr["fb_friends_rewards"] = Convert.ToDecimal(dr["code_fb_friends_aed_rewards"]).ToString("0.000");
                        else dr["fb_friends_rewards"] = Convert.ToDecimal(dr["code_fb_friends_percent_rewards"]).ToString("0.000");

                        if (dr["reward_code_unit"].ToString().Trim() == "AED") dr["coupon_code"] = Convert.ToDecimal(dr["coupon_code"]).ToString("0.000");
                        else dr["coupon_code"] = Convert.ToDecimal(dr["coupon_code_percent"]).ToString("0.000");

                        dr["reward_likes_unit"] = dr["reward_code_flike_unit"];
                        dr["reward_friends_unit"] = dr["reward_code_ffriend_unit"];
                    }
                }
            }

            RepTab.DataSource = ConnObj.DataSet.Tables[0];
            RepTab.DataBind();
        }
        else
        {
            ;
        }
    }

    protected void DrpActivityStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        //DropDownList drp = (DropDownList)sender;
        //RepeaterItem gvrow = (RepeaterItem)drp.NamingContainer;

        //HiddenField HiddenField1 = (HiddenField)gvrow.FindControl("HiddenField1");
        //Int64 rowindex = Convert.ToInt64(HiddenField1.Value);


        //// update database
        //_IChooseIT_ActivitiesClass.TabObj = new IChooseIT_Activity();
        //_IChooseIT_ActivitiesClass.SelectTabObj(rowindex);
        //_IChooseIT_ActivitiesClass.TabObj.status = Convert.ToInt32(drp.SelectedItem.Value);
        //_IChooseIT_ActivitiesClass.TabObj.id = rowindex;
        //_IChooseIT_ActivitiesClass.Update();

        //if (_IChooseIT_ActivitiesClass.IsSuccess)
        //{
        //    Label Label1 = (Label)gvrow.FindControl("Label1");
        //    Label1.Text = "updated successfully";
        //    //FirstPos();
        //}
        //else
        //{
        //    Label Label1 = (Label)gvrow.FindControl("Label1");
        //    Label1.Text = "failed";
        //    //FirstPos();
        //}

    }

    protected void displayUser_Click(object sender, CommandEventArgs e)
    {
        SessionState.UserID = Convert.ToInt64(e.CommandArgument);
        Response.Redirect("user-listing.aspx");
    }
    protected void displayUserActivity_Click(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        SessionState.UserID = Convert.ToInt64(commandArgs[0]);
        SessionState.ActivityID = Convert.ToInt64(commandArgs[1]);
        Response.Redirect("user-activity-details.aspx");
    }

    [System.Web.Services.WebMethod(true)]
    public static string GetIPTCData(String md5)
    {
        SqlCommand cmd = new SqlCommand("sp_IchooseIT_CheckDuplicateImages");
        cmd.Parameters.AddWithValue("@md5", md5);
        ConnObj.GetDataSet(cmd);

        if (Convert.ToInt16(ConnObj.DataSet.Tables[0].Rows[0]["cnt"]) > 1)
        {
            return "exits";
        }
        else
        {
            return "not exists";
        }

    }

    [System.Web.Services.WebMethod(true)]
    public static string GetIPTCData_1(String imageurl)
    {
        string data;
        data = "img=" + imageurl;
        byte[] byteArray = Encoding.UTF8.GetBytes(data);
        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8081/www/google/testimgmd5.php");
        httpWebRequest.ContentType = "application/x-www-form-urlencoded";
        httpWebRequest.ContentLength = byteArray.Length;
        httpWebRequest.Method = "POST";
        httpWebRequest.Accept = "*/*";
        httpWebRequest.Headers.Add("Authorization", "Basic reallylongstring");

        //Get the stream that holds request data by calling the GetRequestStream method.
        Stream dataStream = httpWebRequest.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();

        //Send the request to the server by calling GetResponse. This method returns an object containing the server's response. The returned WebResponse object's type is determined by the scheme of the request's URI
        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            string md5content = streamReader.ReadToEnd();
            streamReader.Close();
            return md5content;
        }
    }
    protected void btn_Approve_Click(object sender, EventArgs e)
    {

    }
    protected void btn_Reject_Click(object sender, EventArgs e)
    {

    }
    protected void RepTab_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string media_str = "";
            foreach (string str in medias.Split('~'))
            {
                if (str.Trim() == "") continue;
                media_str += "<small class=\"label label-success\">" + str + "</small>&nbsp;";
            }
            ((HtmlGenericControl)e.Item.FindControl("divMedias")).InnerHtml = media_str;

        }
    }
    protected void lnk_User_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        SessionState.EditId = Convert.ToInt64(btn.CommandArgument);
        Response.Redirect(SessionState.WebsiteURLBrand + "activity-listing.aspx");
    }
}