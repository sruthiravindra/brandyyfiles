using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using IchooseIT.DAL;
public class SignInUser
{
    public Int64 reg_uid = 0;
    public string reg_email = "";
    public string reg_fname = "";
    public string reg_lname = "";
    public string sm_uid = "";
    public string profileurl = "";
    public SignInUser(Int64 reg_uid, string reg_email, string reg_fname, string reg_lname, string sm_uid, string profileurl)
    {
        this.reg_uid = reg_uid;
        this.reg_email = reg_email;
        this.reg_fname = reg_fname;
        this.reg_lname = reg_lname;
        this.sm_uid = sm_uid;
        this.profileurl = profileurl;
    }
}


public class BrandAdmin
{
    public Int32 brand_id = 0;
    public string brand_name = "";
    public Int64 user_id = 0;
    public string username { get; set; }
    public string useremail { get; set; }
    public string password { get; set; }
    public string created_date_time { get; set; }
    public string steps { get; set; }
    public Int64 callCount = 0;
    public BrandAdmin(Int32 brand_id, string brand_name)
    {
        this.brand_id = brand_id;
        this.brand_name = brand_name;               
    }
}

public class BrandyyAdmin
{
    public Int32 admin_id { get; set; }
    public string admin_name { get; set; }
    public DateTime created_date_time { get; set; }

    public BrandyyAdmin(Int32 admin_id, string admin_name)
    {
        this.admin_id = admin_id;
        this.admin_name = admin_name;
    }
}

public class FBLoginDetails : Common
{
    public string Name { get; set; }
    public string FName { get; set; }
    public string LName { get; set; }
    public string ID { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public string ProfileImageUrl { get; set; }
    public string ProfileUrl { get; set; }
    public Int16 NoOfFriends { get; set; }
    public string FriendsList { get; set; }
    public string AccessToken { get; set; }
    public string LoginSuccessRedirectHomePage { get; set; }
}

public class GPLoginDetails : Common
{
    public string Name { get; set; }
    public string FName { get; set; }
    public string LName { get; set; }
    public string ID { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public string ProfileImageUrl { get; set; }
    public string ProfileUrl { get; set; }
    public Int16 NoOfFriends { get; set; }
    public string FriendsList { get; set; }
    public string AccessToken { get; set; }
    public string LoginSuccessRedirectHomePage { get; set; }
}

public class BrandAdminLoginDetails : Common
{
    public string useremail { get; set; }
    public string password { get; set; }
    public string LoginSuccessRedirectHomePage { get; set; }
}

public class Campaign
{
    public Int64 campaign_id = 0;
    public int brand_id = 0;
    
    public string campaign_name = "";
    public string campaign_name2 = "";
    public string campaign_desc = "";
    public byte campaign_objective { get; set; }
    public byte schedule_type { get; set; }
    
    public DateTime campaign_start ;
    public DateTime campaign_end ;
    public byte campaign_status = 0;
    public byte verification_status { get; set; }
    public decimal reward_amount = 0;
    public decimal reward_amount_max { get; set; }
    public decimal reward_percent = 0;
    public decimal reward_percent_max { get; set; }
    public decimal reward_point = 0;
    public string reward_product { get; set; }
    public string reward_3 = "0";
    public string reward_4 = "";
    public string reward_5 = "";
    public string reward_max_user = "";    
    public byte reward_type = 0;
    public decimal daily_budget { get; set; }
    public Int64 overall_budget { get; set; }
    public decimal max_brandyy_points { get; set; }
    
    public string step = "";
    public string campaign_type_desc { get; set; }

    public bool target_all_users { get; set; }
    public string target_country = "";
    public int target_from_age { get; set; }
    public int target_to_age { get; set; }
    public string target_gender { get; set; }


    public byte reward_whom { get; set; }
    public string reward_whom_3_no_of { get; set; }
    public string reward_whom_3_max { get; set; }
    public Int64 reward_whom_2_nth { get; set; }

    public byte reward_when_type { get; set; }
    public DateTime reward_when_date { get; set; }
    public string reward_date = "";

    public decimal reward_user { get; set; }
    public decimal reward_per_friend { get; set; }
    public decimal reward_per_like { get; set; }
    public decimal reward_per_share { get; set; }

    public campaign_action[] actions { get; set; }

    public bool firstpos { get; set; }
    public byte create_campaign_step { get; set; }

    public Campaign(Int64 campaign_id, int brand_id )
    {
        this.campaign_id = campaign_id;
        this.brand_id = brand_id ;
        this.actions = new campaign_action[Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["campaign_type_max_cnt"])]; 
        
        this.campaign_name = "";
        this.campaign_name2 = "";
        this.campaign_desc = "";
        
        this.schedule_type = 1;
        this.campaign_start = DateTime.Now;        
        this.campaign_status = 0;
        this.verification_status = 0;
        this.reward_amount = 0;
        this.reward_percent = 0;
        this.reward_product = "";
        this.reward_3 = "";
        this.reward_4 = "";
        this.reward_5 = "";
        this.reward_max_user = "";
        this.target_country = "";
        this.reward_type = 1;
        
        this.step = "1";
        this.campaign_type_desc = "";
        this.target_all_users = true;
        this.target_from_age = 0;
        this.target_to_age = 0;
        this.target_gender = "0";
        this.daily_budget = 0;
        this.overall_budget = 0;
        this.campaign_objective = 0;

        this.reward_whom = 1;
        this.reward_whom_3_no_of = "";
        this.reward_whom_3_max = "";
        this.reward_whom_2_nth = 0;

    }
}
public class campaign_action
{
    public Int64 action_id = 0;
    public byte campaign_type = 0;

    public decimal reward_user { get; set; }
    public decimal reward_per_friend { get; set; }
    public decimal reward_per_like { get; set; }
    public decimal reward_per_share { get; set; }
    
    public string val1 = "";
    public string val2 = "";
    public string val3 = "";
    public string val4 = "";
    public string val5 = "";
    public string val6 = "";
    public string val7 = "";
    public string displayval1 = "";
    public campaign_action()
    {
        this.action_id = 0;
        this.campaign_type = 0;
        this.reward_user=0;
        this.reward_per_friend=0;
        this.reward_per_like=0;
        this.reward_per_share=0;        

        this.val1 = "";
        this.val2 = "";
        this.val3 = "";
        this.val4 = "";
        this.val5 = "";
        this.val6 = "";
        this.val7 = "";
        this.displayval1 = "";

    }
}
public class fbuser
{
    public Int64 reg_uid { get; set; }
    public string sm_uid { get; set; }
    public string token { get; set; }
    public Int32 no_of_friends { get; set; }
    public Int32 no_of_likes { get; set; }
    public Int32 no_of_shares { get; set; }
    public DateTime token_created_on { get; set; }
}

public class UserActivity
{
    public Int64 reg_uid { get; set; }
    public Int64 activity_id { get; set; }
    public Int64 brand_id { get; set; }
    public Int64 action_id { get; set; }
    public Int64 campaign_id { get; set; }
    public string verification_str { get; set; }
    public string brand_name { get; set; }
    public byte campaign_type { get; set; }
    public string campaign_type_desc { get; set; }
    public DateTime created_on { get; set; }
    public bool verification_status { get; set; }
    public bool verification_score { get; set; }

    public string val_1 { get; set; }
    public string val_2 { get; set; }
    public string val_3 { get; set; }
    public string val_4 { get; set; }
    
}

