using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IchooseIT.DAL;

public partial class syncbrandcampaignactivities : System.Web.UI.Page
{
    public ConnectionClass ConnObj = null; 
    Dictionary<Int64, string> page_tokens = new Dictionary<Int64, string>();
    Dictionary<Int64, string> page_verifier = new Dictionary<Int64, string>();    
    List<fbuser> fbsm_id = new List<fbuser>();
    List<fbuser> twsm_id = new List<fbuser>();
    List<fbuser> instasm_id = new List<fbuser>();
    CommonVariableCodes _CommonVariableCodes = new CommonVariableCodes();
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

        if (!Page.IsPostBack)
        {
            GetActiveCampaigns();
        }

    }
    #endregion

    #region private functions

    private void GetActiveCampaigns()
    {
        

        SqlCommand cmd = new SqlCommand("sp_select_brands_campaigns_by_status");
        cmd.Parameters.AddWithValue("@status", _CommonVariableCodes.campaign_status_active);
        ConnObj.GetDataSet(cmd);        
        if (ConnObj.IsSuccess == true && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            getAllUsers();
 
            importfbbranddetails obj = new importfbbranddetails();
            importtwitterbranddetails obj_tw = new importtwitterbranddetails();
            importinstabranddetails obj_insta = new importinstabranddetails();
            DataTable _DataTable = ConnObj.DataSet.Tables[0];
            foreach (DataRow tbl in _DataTable.Rows)
            {
                cmd = new SqlCommand("sp_select_brands_campaigns_action");
                cmd.Parameters.AddWithValue("@campaign_id", tbl["campaign_id"]);
                ConnObj.GetDataSet(cmd);
                if (ConnObj.IsSuccess == true && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
                {
                    DataTable _DataTable_Action = ConnObj.DataSet.Tables[0];
                    foreach (DataRow tbl2 in _DataTable_Action.Rows)
                    {
                        
                        #region reward setup

                        Int32 no_of_friends = 0;
                        Int32 no_of_likes = 0;
                        Int32 no_of_shares = 0;
                        decimal reward_per_user = 0;
                        decimal reward_on_friend = 0;
                        decimal reward_on_likes = 0;
                        decimal reward_on_shares = 0;
                        decimal max_brandyy_points = 0;

                        max_brandyy_points = Convert.ToDecimal(tbl["max_brandyy_points"]);

                        reward_per_user = Convert.ToDecimal(tbl2["reward_user"]);
                        reward_on_friend = Convert.ToDecimal(tbl2["reward_per_friend"]);
                        reward_on_likes = Convert.ToDecimal(tbl2["reward_per_like"]);
                        reward_on_shares = Convert.ToDecimal(tbl2["reward_per_share"]);

                        #endregion
                        
                        switch (Convert.ToByte( tbl2["campaign_type"] ))
                        {
                            case 1: obj.checkIfUserLikesAPage(getAccessToken(Convert.ToInt64(tbl2["val_1"])), fbsm_id, Convert.ToString(tbl2["val_2"]), Convert.ToInt32(tbl2["brand_id"]), Convert.ToInt64(tbl2["campaign_id"]), Convert.ToInt64(tbl2["action_id"]),
                                max_brandyy_points, no_of_friends, no_of_likes, no_of_shares, reward_per_user, reward_on_friend, reward_on_likes, reward_on_shares); break;
                            case 2: obj_tw.checkIfUserLikesAPage(getAccessToken(Convert.ToInt64(tbl2["val_1"])), twsm_id, Convert.ToString(tbl2["val_2"]), Convert.ToInt32(tbl2["brand_id"]), Convert.ToInt64(tbl2["campaign_id"]), Convert.ToInt64(tbl2["action_id"]),
                                max_brandyy_points, no_of_friends, no_of_likes, no_of_shares, reward_per_user, reward_on_friend, reward_on_likes, reward_on_shares); break;
                            case 3: obj_tw.checkIfUserSharedAPost(getAccessToken(Convert.ToInt64(tbl2["val_1"])), twsm_id, Convert.ToString(tbl2["val_2"]), Convert.ToInt32(tbl2["brand_id"]), Convert.ToInt64(tbl2["campaign_id"]), Convert.ToInt64(tbl2["action_id"]),
                                max_brandyy_points, no_of_friends, no_of_likes, no_of_shares, reward_per_user, reward_on_friend, reward_on_likes, reward_on_shares); break;
                            case 4: obj_tw.checkIfUserLikedAPost(getAccessToken(Convert.ToInt64(tbl2["val_1"])), twsm_id, Convert.ToString(tbl2["val_2"]), Convert.ToInt32(tbl2["brand_id"]), Convert.ToInt64(tbl2["campaign_id"]), Convert.ToInt64(tbl2["action_id"]),
                                max_brandyy_points, no_of_friends, no_of_likes, no_of_shares, reward_per_user, reward_on_friend, reward_on_likes, reward_on_shares); break;
                            case 5:
                                obj.checkIfUserSharedAPost(getAccessToken(Convert.ToInt64(tbl2["val_1"])), fbsm_id, Convert.ToString(tbl2["val_2"]), Convert.ToInt32(tbl2["brand_id"]), Convert.ToInt64(tbl2["campaign_id"]), Convert.ToInt64(tbl2["action_id"]),
                                max_brandyy_points, no_of_friends, no_of_likes, no_of_shares, reward_per_user, reward_on_friend, reward_on_likes, reward_on_shares); break;
                            case 6: obj.checkIfUserLikesAPost(getAccessToken(Convert.ToInt64(tbl2["val_1"])), fbsm_id, Convert.ToString(tbl2["val_2"]), Convert.ToInt32(tbl2["brand_id"]), Convert.ToInt64(tbl2["campaign_id"]), Convert.ToInt64(tbl2["action_id"]),
                                max_brandyy_points, no_of_friends, no_of_likes, no_of_shares, reward_per_user, reward_on_friend, reward_on_likes, reward_on_shares); break;
                            case 8: obj.checkIfUserCheckedIn(getAccessToken(Convert.ToInt64(tbl2["val_1"])), fbsm_id, Convert.ToString(tbl2["val_3"]), Convert.ToInt32(tbl2["brand_id"]), Convert.ToInt64(tbl2["campaign_id"]), Convert.ToInt64(tbl2["action_id"]),
                                max_brandyy_points, no_of_friends, no_of_likes, no_of_shares, reward_per_user, reward_on_friend, reward_on_likes, reward_on_shares); break;
                            case 9: obj_tw.checkIfUserHasAddedAnyPost(getAccessToken(Convert.ToInt64(tbl2["val_1"])), getAccessVerifier(Convert.ToInt64(tbl2["val_1"])),twsm_id, Convert.ToString(tbl2["val_2"]), Convert.ToString(tbl2["val_3"]), Convert.ToString(tbl2["val_4"]), Convert.ToString(tbl["campaign_start"]), Convert.ToInt32(tbl2["brand_id"]), Convert.ToInt64(tbl2["campaign_id"]), Convert.ToInt64(tbl2["action_id"]),
                                max_brandyy_points, no_of_friends, no_of_likes, no_of_shares, reward_per_user, reward_on_friend, reward_on_likes, reward_on_shares); break;
                            case 10: obj.checkIfUserHasAddedAnyPost(getAccessToken(Convert.ToInt64(tbl2["val_1"])), fbsm_id, Convert.ToString(tbl2["val_2"]), Convert.ToString(tbl2["val_3"]), Convert.ToString(tbl2["val_4"]), Convert.ToString(tbl["campaign_start"]), Convert.ToInt32(tbl2["brand_id"]), Convert.ToInt64(tbl2["campaign_id"]), Convert.ToInt64(tbl2["action_id"]),
                                max_brandyy_points, no_of_friends, no_of_likes, no_of_shares, reward_per_user, reward_on_friend, reward_on_likes, reward_on_shares); break;
                            case 17: obj_insta.checkIfUserLikesAPage(getAccessToken(Convert.ToInt64(tbl2["val_1"])), instasm_id, Convert.ToString(tbl2["val_2"]), Convert.ToInt32(tbl2["brand_id"]), Convert.ToInt64(tbl2["campaign_id"]), Convert.ToInt64(tbl2["action_id"]),
                                max_brandyy_points, no_of_friends, no_of_likes, no_of_shares, reward_per_user, reward_on_friend, reward_on_likes, reward_on_shares); break;
                            case 18: obj_insta.checkIfUserLikedAPost(getAccessToken(Convert.ToInt64(tbl2["val_1"])), instasm_id, Convert.ToString(tbl2["val_2"]), Convert.ToInt32(tbl2["brand_id"]), Convert.ToInt64(tbl2["campaign_id"]), Convert.ToInt64(tbl2["action_id"]),
                            max_brandyy_points, no_of_friends, no_of_likes, no_of_shares, reward_per_user, reward_on_friend, reward_on_likes, reward_on_shares); break;
                            case 19: obj_insta.checkIfUserHasAddedAnyPost(getAccessToken(Convert.ToInt64(tbl2["val_1"])), instasm_id, Convert.ToString(tbl2["val_1"]) + Convert.ToString(tbl2["val_2"]), Convert.ToString(tbl2["val_3"]), Convert.ToString(tbl2["val_4"]), Convert.ToString(tbl["campaign_start"]), Convert.ToInt32(tbl2["brand_id"]), Convert.ToInt64(tbl2["campaign_id"]), Convert.ToInt64(tbl2["action_id"]),
                                max_brandyy_points, no_of_friends, no_of_likes, no_of_shares, reward_per_user, reward_on_friend, reward_on_likes, reward_on_shares); break;                            
                        }
                    }
                }

                // Update camapign status if end of date approched
                if (Convert.ToByte(tbl["schedule_type"]) == _CommonVariableCodes.schedule_type_periodic)
                {
                    if (DateTime.Now > Convert.ToDateTime( tbl["campaign_end"] ))
                    {
                        UpdateCampaignStatus( Convert.ToInt64( tbl["campaign_id"]));
                    }
                }
            }
        }
        //Response.Redirect( SessionState.WebsiteURL + "syncactivitiesverification.aspx");
    }

    #endregion

    private void UpdateCampaignStatus(Int64 campaign_id)
    {
        //Brands_CampaignClass bc = new Brands_CampaignClass();
        //bc.Select(campaign_id);
        //if (bc.IsSuccess)
        //{
        //    bc.TabObj.campaign_status = 0;
        //    bc.TabObj.log_text = "Campaign has reached its end of date.";
        //    bc.Update();
        //}
    }

    private string getAccessToken(Int64 page_id)
    {
        string token="";
        if (page_tokens.ContainsKey(page_id)) token = page_tokens[page_id];
        else
        {
            // get user access token
            SqlCommand cmd = new SqlCommand("sp_Get_brands_sm_token_by_page_id");
            cmd.Parameters.AddWithValue("@page_id", page_id);
            ConnectionClass ConnObj1 = new ConnectionClass();
            ConnObj1.GetDataSet(cmd);
            if (ConnObj1.IsSuccess && ConnObj1.DataSet.Tables.Count > 0 && ConnObj1.DataSet.Tables[0].Rows.Count > 0)
            {
                page_tokens.Add(page_id, Convert.ToString(ConnObj1.DataSet.Tables[0].Rows[0]["token"]));
                page_verifier.Add(page_id, Convert.ToString(ConnObj1.DataSet.Tables[0].Rows[0]["verifier"]));
                token = page_tokens[page_id];
                
            }
            ConnObj1.ReleaseConnection();
        }
        return token;
    }
    private string getAccessVerifier(Int64 page_id)
    {
        string token="";
        if (page_verifier.ContainsKey(page_id)) token = page_verifier[page_id];        
        return token;
    }
    private void getAllUsers()
    {
        #region get all users access token
        ConnectionClass ConnObj_u = new ConnectionClass();
        // get user access token
        SqlCommand cmd = new SqlCommand("sp_Get_all_users");
        cmd.Parameters.AddWithValue("@sm_id", 1);
        ConnObj_u.GetDataSet(cmd);
        if (ConnObj_u.IsSuccess && ConnObj_u.DataSet.Tables.Count > 0 && ConnObj_u.DataSet.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ConnObj_u.DataSet.Tables[0].Rows)
            {
                fbuser obj1 = new fbuser();
                obj1.sm_uid = Convert.ToString(dr["sm_uid"]);
                obj1.reg_uid = Convert.ToInt64(dr["reg_uid"]);
                obj1.token = "";
                obj1.no_of_friends = Convert.ToInt32(dr["no_of_friends"]);
                fbsm_id.Add(obj1);
            }
        }

        // get user access token
        cmd = new SqlCommand("sp_Get_all_users");
        cmd.Parameters.AddWithValue("@sm_id", 2);
        ConnObj_u.GetDataSet(cmd);
        if (ConnObj_u.IsSuccess && ConnObj_u.DataSet.Tables.Count > 0 && ConnObj_u.DataSet.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ConnObj_u.DataSet.Tables[0].Rows)
            {
                fbuser obj1 = new fbuser();
                obj1.sm_uid = Convert.ToString(dr["sm_uid"]);
                obj1.reg_uid = Convert.ToInt64(dr["reg_uid"]);
                obj1.token = "";
                obj1.no_of_friends = Convert.ToInt32(dr["no_of_friends"]);
                twsm_id.Add(obj1);
            }
        }

        cmd = new SqlCommand("sp_Get_all_users");
        cmd.Parameters.AddWithValue("@sm_id", 3);
        ConnObj_u.GetDataSet(cmd);
        if (ConnObj_u.IsSuccess && ConnObj_u.DataSet.Tables.Count > 0 && ConnObj_u.DataSet.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ConnObj_u.DataSet.Tables[0].Rows)
            {
                fbuser obj1 = new fbuser();
                obj1.sm_uid = Convert.ToString(dr["sm_uid"]);
                obj1.reg_uid = Convert.ToInt64(dr["reg_uid"]);
                obj1.token = "";
                obj1.no_of_friends = Convert.ToInt32(dr["no_of_friends"]);
                instasm_id.Add(obj1);
            }
        }


        #endregion
    }
}
