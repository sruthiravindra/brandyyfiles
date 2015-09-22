using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IchooseIT.DAL;


public partial class syncactivitiesverification : System.Web.UI.Page
{
    public ConnectionClass ConnObj = null;
    public Dictionary<Int32, decimal> AdminVerificationPoints= new Dictionary<Int32, decimal>();
    string verification_log = "";
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
            GetVerificationPendingActivities();
        }

    }
    #endregion

    private void GetVerificationPendingActivities()
    {
        GetAdminVerificationList();

        SqlCommand cmd = new SqlCommand("sp_brandyy_GetPendingActivities");
        int status = 0;
        cmd.Parameters.AddWithValue("@status", status);
        ConnObj.GetDataSet(cmd);

        #region set points
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            foreach( DataRow dr in ConnObj.DataSet.Tables[0].Rows )
            {
                // get the campaign type for the activity
                byte campaign_type = Convert.ToByte( dr["campaign_type"] );

                decimal points = 0;
                verification_log = "";

                // verification 1 == based on campaign type
                // currently none avaialble
                points += AdminVerificationPoints[1];

                // verification 2 == based on campaign target 
                points += PerformTargetBasedVerification(dr);

                // verification 3 == based on no. of friends on facebook
                points += Convert.ToInt64( dr["fb_friends"] ) * AdminVerificationPoints[3];

                // verification4 == based on no. of followers on twitter
                points += Convert.ToInt64( dr["twitter_followers"] ) * AdminVerificationPoints[4];
                
                // verification 5 == previously blocked activity by admin
                points += Convert.ToInt64( dr["admin_blocked_activity"] ) * AdminVerificationPoints[5];

                // verification 6 == previously blocked by any user
                points += Convert.ToInt64( dr["ichooseit_user_blocked"] )* AdminVerificationPoints[6];

                // verification 7 == rewards received before
                //points += dr["ichooseit_user_blocked"] * AdminVerificationPoints[7];

                // verification 8 == number of friends common with ichooseit
                points += Convert.ToInt64( dr["ichooseit_common_friends"] ) * AdminVerificationPoints[8];

                // verification 9 == active users
                points += Convert.ToInt64(dr["active_user"]) * AdminVerificationPoints[9];

                // verification 10 == fb_verified
                points += Convert.ToInt64(dr["fb_verified"]) * AdminVerificationPoints[10];

                // verification 11 == twitter_verified
                points += Convert.ToInt64(dr["twitter_verified"]) * AdminVerificationPoints[11];

                // verification 12 == friend of a blocked user
                points += Convert.ToInt64(dr["friend_of_blocked_user"]) * AdminVerificationPoints[12];

                // based on matchnig profile details
                points += Convert.ToInt64(dr["match_email_id"]) * AdminVerificationPoints[13];
                //points += Convert.ToInt64(dr["match_dob"]) * AdminVerificationPoints[13];

                UpdateVerficationScore(points, Convert.ToInt64(dr["brand_id"]), Convert.ToInt64(dr["activity_id"]), Convert.ToInt64(dr["campaign_id"]), campaign_type, Convert.ToByte(dr["reward_when_type"]), Convert.ToByte(dr["reward_whom"]),
                    Convert.ToBoolean(dr["all_actions_compulsory"]), Convert.ToString(dr["compulsory_actions"]), Convert.ToString(dr["actions_performed"]));
            }
        }
        #endregion

        UpdateNoCampaignActivities();
        

    }
    private void GetAdminVerificationList()
    {
        SqlCommand cmd = new SqlCommand("sp_Admin_Verification_Logic");        
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            foreach( DataRow dr in ConnObj.DataSet.Tables[0].Rows )
            {
                AdminVerificationPoints.Add( Convert.ToInt32( dr["verification_id"] ), Convert.ToDecimal( dr["point"]) );
            }
        }
        
    }

    private decimal PerformTargetBasedVerification(DataRow dr)
    {
        decimal point = 0;

        bool t = Convert.ToBoolean(dr["all_users"]);
        // case: all users is selected
        if ((dr["all_users"] != null) && (Convert.ToBoolean( dr["all_users"] ) == true))
        {
            point += AdminVerificationPoints[2];
            return point;
        }

        // get country for the user
        if ((dr["country"] != null) && (dr["country"] != ""))
        {
            string[] country = dr["country"].ToString().Split(',');
            if (country.Contains(dr["user_country"]))
            {
                point += AdminVerificationPoints[2];
            }
            else { verification_log += "<br>Target country does not match"; return AdminVerificationPoints[14]; }

        }
        // get age for user
        if ((dr["from_age"] != null) && (dr["from_age"] != ""))
        {
            if ((Convert.ToInt64(dr["user_age"]) >= Convert.ToInt64(dr["from_age"])) && (Convert.ToInt64(dr["user_age"]) <= Convert.ToInt64(dr["to_age"])))
            {
                point += AdminVerificationPoints[2];
            }
            else { verification_log += "<br>Target age does not match"; return AdminVerificationPoints[14]; }
        }
        // get gender for user
        if ((dr["gender"] != null) && (dr["gender"] != ""))
        {
            if (dr["gender"] == dr["user_gender"])
            {
                point += AdminVerificationPoints[2];
            }
            else { verification_log += "<br>Target gender does not match"; return AdminVerificationPoints[14]; }
        }


        return point;
    }

    private void UpdateVerficationScore(decimal points, Int64 brand_id, Int64 activity_id, Int64 campaign_id, Byte campaign_type, byte reward_when_type, byte reward_whom, bool all_actions_compulsory,
        string compulsory_actions, string actions_performed)
    {
        int verify_status = 0;byte reward_status = 0;
        switch (campaign_type)
        {
            case 1: 
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 8:
            case 13:
            case 14:
            case 15:
            case 16:
                verify_status=1;
                break;
        }
        if (campaign_id == 8)
        {
            verify_status = 1;
            reward_status = 3;
        }

        if (verify_status == 1)
        {
            reward_status = 2;// approved

            if (reward_whom == 3)
            {
                reward_status = 0;
            }

            // case : campaign set as reward immediately
            if (reward_when_type == 1)
            {
                reward_status = 1;// rewarded
            }

            
        }

        if (points < -5)
        {
            verify_status = 1;
            reward_status = 3;
            verification_log = "Not part of any campaign";
        }


        #region check if user has performed all the required actions

        if (all_actions_compulsory == true)
        {
            if (compulsory_actions != actions_performed)
            {
                verify_status = 0;
                reward_status = 0;
                verification_log = "<br>All complusory actions not yet performed";
            }
        }

        #endregion


        SqlCommand cmd = new SqlCommand("sp_brandyy_UpdateActivityVerifPoints");
        cmd.Parameters.AddWithValue("@points", points);        
        cmd.Parameters.AddWithValue("@status", verify_status);
        cmd.Parameters.AddWithValue("@reward_status", reward_status);
        cmd.Parameters.AddWithValue("@brand_id", brand_id);        
        cmd.Parameters.AddWithValue("@id", activity_id);
        cmd.Parameters.AddWithValue("@verification_log", verification_log);
        cmd.Parameters.Add("@returnval", SqlDbType.Bit).Direction = ParameterDirection.Output;
        ConnObj.GetDataTab(cmd);

        if (ConnObj.IsSuccess)
        {
            //bool updated_status = Convert.ToBoolean( cmd.Parameters["@returnval"].Value );

            //// case : activity is updated and status is approved
            //if ((updated_status == true) && (reward_status == 1))
            //{
            //    VoucherMailSend vm = new VoucherMailSend("");
            //    vm.ConnObj = ConnObj;
            //    if (vm.SetActivityCoupon(activity_id, campaign_id) == true)
            //    {
            //        vm.ReadyTemplate();
            //        vm.SendEMail();
            //    }
            //}
        }
    }

    private void UpdateNoCampaignActivities()
    {
        SqlCommand cmd = new SqlCommand("sp_IChooseIT_UpdateNOCampaignActivities");
        ConnObj.GetDataTab(cmd);
    }
  
}