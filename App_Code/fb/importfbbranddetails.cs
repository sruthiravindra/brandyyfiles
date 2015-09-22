using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Facebook;
using Newtonsoft.Json.Linq;
/// <summary>
/// this class is called when user has not logged in and the scheduler is syncing data
/// </summary>
public class importfbbranddetails
{
    ConnectionClass ConnObj = new ConnectionClass();
    CommonVariableCodes _CommonVariableCodes = new CommonVariableCodes();
	public importfbbranddetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void getUserswhoLikeOurPost(string post_id)
    {
        //https://graph.facebook.com/548383975297602/likes
        //https://graph.facebook.com/IchooseitBranding
    }

    // check if user likes a post
    public void checkIfUserLikesAPost(string accessToken, List<fbuser> user, string post_id, Int32 brand_id, Int64 campaign_id, Int64 action_id,
                        decimal max_brandyy_points, Int32 no_of_friends,
                        Int32 no_of_likes, Int32 no_of_shares, decimal reward_per_user, decimal reward_on_friend,
                        decimal reward_on_likes, decimal reward_on_shares, string actionUrl, string action_post_text, string action_post_image_url)
    {
        var client = new FacebookClient(accessToken);

        dynamic posts = client.Get("/" + post_id + "/likes");
        
        //Loop through the returned users
        foreach (var i in posts["data"])
        {
            decimal points = 0;
            fbuser t = user.FirstOrDefault(item => item.sm_uid == i["id"]);
            // check if the post link contains the post id
            if (t != null)
            {                
                ConnObj = new ConnectionClass();
                SqlCommand cmd = new SqlCommand("sp1_brandyy_User_Activities_Insert");
                cmd.Parameters.AddWithValue("@reg_uid", t.reg_uid);
                cmd.Parameters.AddWithValue("@brand_id", brand_id);
                cmd.Parameters.AddWithValue("@campaign_id", campaign_id);
                cmd.Parameters.AddWithValue("@action_id", action_id);                
                cmd.Parameters.AddWithValue("@created_on", "");
                cmd.Parameters.AddWithValue("@pid", "");

                                // reward table details
                if (reward_per_user > 0)
                {
                    points = reward_per_user;
                }
                if (reward_on_friend > 0)
                {
                    // get number of friends for the user
                    no_of_friends = t.no_of_friends;
                    points += reward_on_friend * no_of_friends;
                }
                decimal reward_amount = (points > max_brandyy_points) ? max_brandyy_points : points;

                cmd.Parameters.AddWithValue("@reward_amount", reward_amount);              
                cmd.Parameters.AddWithValue("@no_of_friends", no_of_friends);
                cmd.Parameters.AddWithValue("@no_of_likes", no_of_likes);
                cmd.Parameters.AddWithValue("@no_of_shares", no_of_shares);
                cmd.Parameters.AddWithValue("@reward_per_user", reward_per_user);
                cmd.Parameters.AddWithValue("@reward_on_friend", reward_on_friend);
                cmd.Parameters.AddWithValue("@reward_on_likes", reward_on_likes);
                cmd.Parameters.AddWithValue("@reward_on_shares", reward_on_shares);
                cmd.Parameters.AddWithValue("@action_url", actionUrl);
                cmd.Parameters.AddWithValue("@activity_text", action_post_text);
                cmd.Parameters.AddWithValue("@acitivity_img", action_post_image_url);
                cmd.Parameters.AddWithValue("@returnid", SqlDbType.BigInt).Direction = ParameterDirection.Output; 

                ConnObj.GetDataTab(cmd);
                ConnObj.ReleaseConnection();
                break;
            }
        }

    }
    public void checkIfUserLikesAPage(string accessToken, List<fbuser> user, string page_id, Int32 brand_id, Int64 campaign_id, Int64 action_id,
                        decimal max_brandyy_points, Int32 no_of_friends,
                        Int32 no_of_likes, Int32 no_of_shares, decimal reward_per_user, decimal reward_on_friend,
                        decimal reward_on_likes, decimal reward_on_shares,string actionUrl, string action_post_text, string action_post_image_url)
    {
        var client = new FacebookClient(accessToken);

        dynamic posts = client.Get("/" + page_id + "?fields=context.fields%28friends_who_like%29");

        //Loop through the returned users
        foreach (var i in posts["context"]["friends_who_like"]["data"])
        {
            decimal points = 0;
            fbuser t = user.FirstOrDefault(item => item.sm_uid == i["id"]);
            // check if the post link contains the post id
            if (t != null)
            {
                ConnObj = new ConnectionClass();
                SqlCommand cmd = new SqlCommand("sp1_brandyy_User_Activities_Insert");
                cmd.Parameters.AddWithValue("@reg_uid", t.reg_uid);
                cmd.Parameters.AddWithValue("@brand_id", brand_id);
                cmd.Parameters.AddWithValue("@campaign_id", campaign_id);
                cmd.Parameters.AddWithValue("@action_id", action_id);
                cmd.Parameters.AddWithValue("@created_on", "");
                cmd.Parameters.AddWithValue("@pid", "");

                // reward table details
                if (reward_per_user > 0)
                {
                    points = reward_per_user;
                }
                if (reward_on_friend > 0)
                {
                    // get number of friends for the user
                    no_of_friends = t.no_of_friends;
                    points += reward_on_friend * no_of_friends;
                }
                decimal reward_amount = (points > max_brandyy_points) ? max_brandyy_points : points;

                cmd.Parameters.AddWithValue("@reward_amount", reward_amount);
                cmd.Parameters.AddWithValue("@no_of_friends", no_of_friends);
                cmd.Parameters.AddWithValue("@no_of_likes", no_of_likes);
                cmd.Parameters.AddWithValue("@no_of_shares", no_of_shares);
                cmd.Parameters.AddWithValue("@reward_per_user", reward_per_user);
                cmd.Parameters.AddWithValue("@reward_on_friend", reward_on_friend);
                cmd.Parameters.AddWithValue("@reward_on_likes", reward_on_likes);
                cmd.Parameters.AddWithValue("@reward_on_shares", reward_on_shares);
                cmd.Parameters.AddWithValue("@action_url", actionUrl);
                cmd.Parameters.AddWithValue("@activity_text", action_post_text);
                cmd.Parameters.AddWithValue("@acitivity_img", action_post_image_url);
                cmd.Parameters.AddWithValue("@returnid", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                ConnObj.GetDataTab(cmd);
                ConnObj.ReleaseConnection();
                break;
            }

        }
    }    
    public void checkIfUserSharedAPost(string accessToken, List<fbuser> user, string post_id, Int32 brand_id, Int64 campaign_id, Int64 action_id,
                        decimal max_brandyy_points, Int32 no_of_friends,
                        Int32 no_of_likes, Int32 no_of_shares, decimal reward_per_user, decimal reward_on_friend,
                        decimal reward_on_likes, decimal reward_on_shares, string actionUrl, string action_post_text, string action_post_image_url)
    {
        var client = new FacebookClient(accessToken);       
        dynamic posts = client.Get("/" + post_id + "/sharedposts");

        //Loop through the returned users
        foreach (var i in posts["data"])
        {
            decimal points = 0;
            fbuser t = user.FirstOrDefault(item => item.sm_uid == i["from"]["id"]);
            // check if the post link contains the post id
            if (t != null)
            {
                ConnObj = new ConnectionClass();
                SqlCommand cmd = new SqlCommand("sp1_brandyy_User_Activities_Insert");
                cmd.Parameters.AddWithValue("@reg_uid", t.reg_uid);
                cmd.Parameters.AddWithValue("@brand_id", brand_id);
                cmd.Parameters.AddWithValue("@campaign_id", campaign_id);
                cmd.Parameters.AddWithValue("@action_id", action_id);
                cmd.Parameters.AddWithValue("@created_on", i["created_time"]);
                cmd.Parameters.AddWithValue("@pid", "");

                // reward table details
                if (reward_per_user > 0)
                {
                    points = reward_per_user;
                }
                if (reward_on_friend > 0)
                {
                    // get number of friends for the user
                    no_of_friends = t.no_of_friends;
                    points += reward_on_friend * no_of_friends;
                }
                if (reward_on_shares > 0)
                {
                    // get number of friends for the user
                    no_of_friends = t.no_of_friends;
                    points += reward_on_friend * no_of_friends;
                }
                decimal reward_amount = (points > max_brandyy_points) ? max_brandyy_points : points;

                cmd.Parameters.AddWithValue("@reward_amount", reward_amount);
                cmd.Parameters.AddWithValue("@no_of_friends", no_of_friends);
                cmd.Parameters.AddWithValue("@no_of_likes", no_of_likes);
                cmd.Parameters.AddWithValue("@no_of_shares", no_of_shares);
                cmd.Parameters.AddWithValue("@reward_per_user", reward_per_user);
                cmd.Parameters.AddWithValue("@reward_on_friend", reward_on_friend);
                cmd.Parameters.AddWithValue("@reward_on_likes", reward_on_likes);
                cmd.Parameters.AddWithValue("@reward_on_shares", reward_on_shares);
                cmd.Parameters.AddWithValue("@action_url", actionUrl);
                cmd.Parameters.AddWithValue("@activity_text", action_post_text);
                cmd.Parameters.AddWithValue("@acitivity_img", action_post_image_url);
                cmd.Parameters.AddWithValue("@returnid", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                ConnObj.GetDataTab(cmd);
                ConnObj.ReleaseConnection();
                break;
            }
        }

    }

    public void checkIfUserHasAddedAnyPost(string accessToken, List<fbuser> user, string hashtag, string defaulttext, string tag, String last_cron_run, Int32 brand_id, Int64 campaign_id, Int64 action_id,
                        decimal max_brandyy_points, Int32 no_of_friends,
                        Int32 no_of_likes, Int32 no_of_shares, decimal reward_per_user, decimal reward_on_friend,
                        decimal reward_on_likes, decimal reward_on_shares)
    {
        var client = new FacebookClient(accessToken);
        dynamic posts;
        if (( last_cron_run == "" ) )
        {
            posts = client.Get("/" + tag + "/tagged");
        }
        else
        {
            //posts = client.Get("/" + tag + "/tagged?since=" + last_cron_run);
            posts = client.Get("/" + tag + "/tagged");
        }
        
        //Loop through the returned users
        foreach (var i in posts["data"])
        {

            Int32 brand_id1; Int64 campaign_id1; Int64 action_id1;

            string message = i["message"];            
            string[] hashtag_arr = hashtag.Split('#');
            bool valid = true;
            foreach (string hash in hashtag_arr)
            {
                if (!(message.Contains(hash.Trim())))
                {
                    valid = false;
                    break;
                }
            }
            if (valid == false)
            {
                //continue;
            }

            // case : default text defined for campaign 
            if (defaulttext != "")
            {
                if (!message.Contains(defaulttext))
                {
                    valid = false;
                    //continue;
                }
            }


            fbuser t = user.FirstOrDefault(item => item.sm_uid == i["from"].id);
            // check if the post link contains the post id
            if (t != null)
            {
                
                ConnObj = new ConnectionClass();
                SqlCommand cmd = new SqlCommand("sp1_brandyy_User_Activities_Insert");

                // case : valid
                if (valid == true)
                {
                    brand_id1 = brand_id; campaign_id1 = campaign_id; action_id1 = action_id;
                    cmd.Parameters.AddWithValue("@reg_uid", t.reg_uid);
                    cmd.Parameters.AddWithValue("@brand_id", brand_id);
                    cmd.Parameters.AddWithValue("@campaign_id", campaign_id);
                    cmd.Parameters.AddWithValue("@action_id", action_id);
                    cmd.Parameters.AddWithValue("@created_on", Convert.ToDateTime(i["created_time"]));
                    cmd.Parameters.AddWithValue("@pid", i["id"]);
                    decimal points = 0;
                    no_of_friends = 0;
                    no_of_likes = 0;
                    no_of_shares = 0;
                    // reward table details
                    if (reward_per_user > 0)
                    {
                        points += reward_per_user;
                    }
                    if (reward_on_friend > 0)
                    {
                        // get number of friends for the user
                        no_of_friends = t.no_of_friends;
                        points += (no_of_friends * reward_on_friend);
                    }
                    if (reward_on_likes > 0)
                    {
                        if (i.ContainsKey("likes"))
                        {
                            no_of_likes = i["likes"].data.Count;
                        }
                        points += (no_of_likes * reward_on_likes);
                    }
                    if (no_of_shares > 0)
                    {
                        if (i.ContainsKey("shares"))
                        {
                            no_of_shares = i["shares"].Count;
                        }
                        points += (no_of_shares * reward_on_shares);
                    }
                    decimal reward_amount = (points > max_brandyy_points) ? max_brandyy_points : points;

                    cmd.Parameters.AddWithValue("@reward_amount", reward_amount);
                    cmd.Parameters.AddWithValue("@no_of_friends", no_of_friends);
                    cmd.Parameters.AddWithValue("@no_of_likes", no_of_likes);
                    cmd.Parameters.AddWithValue("@no_of_shares", no_of_shares);
                    cmd.Parameters.AddWithValue("@reward_per_user", reward_per_user);
                    cmd.Parameters.AddWithValue("@reward_on_friend", reward_on_friend);
                    cmd.Parameters.AddWithValue("@reward_on_likes", reward_on_likes);
                    cmd.Parameters.AddWithValue("@reward_on_shares", reward_on_shares);
                    if (i.ContainsKey("link"))
                    {
                        cmd.Parameters.AddWithValue("@action_url", i["link"]);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@action_url", i.ContainsKey("actions") ? Convert.ToString(i["actions"][0]["link"]) : "");
                    }
                    cmd.Parameters.AddWithValue("@activity_text", i["message"]);
                    cmd.Parameters.AddWithValue("@acitivity_img", i.ContainsKey("picture") ? i["picture"]: "");
                    cmd.Parameters.AddWithValue("@returnid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                    ConnObj.GetDataTab(cmd);
                }
                else
                {
                    brand_id1 = _CommonVariableCodes.brandyy_brand_id; campaign_id1 = _CommonVariableCodes.brandyy_campaign_id; action_id1 = _CommonVariableCodes.brandyy_action_id_fb;
                    cmd.Parameters.AddWithValue("@reg_uid", t.reg_uid);
                    cmd.Parameters.AddWithValue("@brand_id", brand_id1);
                    cmd.Parameters.AddWithValue("@campaign_id", campaign_id1);
                    cmd.Parameters.AddWithValue("@action_id", action_id1);                                
                    cmd.Parameters.AddWithValue("@created_on", Convert.ToDateTime(i["created_time"]));
                    cmd.Parameters.AddWithValue("@pid", i["id"]);
                    cmd.Parameters.AddWithValue("@reward_amount", 0);
                    cmd.Parameters.AddWithValue("@no_of_friends", 0);
                    cmd.Parameters.AddWithValue("@no_of_likes", 0);
                    cmd.Parameters.AddWithValue("@no_of_shares", 0);
                    cmd.Parameters.AddWithValue("@reward_per_user", 0);
                    cmd.Parameters.AddWithValue("@reward_on_friend", 0);
                    cmd.Parameters.AddWithValue("@reward_on_likes", 0);
                    cmd.Parameters.AddWithValue("@reward_on_shares", 0);
                    if (i.ContainsKey("link"))
                    {
                        cmd.Parameters.AddWithValue("@action_url", i["link"]);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@action_url", i.ContainsKey("actions") ? Convert.ToString(i["actions"][0]["link"]) : "");
                    }
                    cmd.Parameters.AddWithValue("@activity_text", i["message"]);
                    cmd.Parameters.AddWithValue("@acitivity_img", i.ContainsKey("picture") ? i["picture"] : "");
                    cmd.Parameters.AddWithValue("@returnid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                    ConnObj.GetDataTab(cmd);
                }

                string id = cmd.Parameters["@returnid"].Value.ToString();

                if (id == "")
                {
                    continue;
                }

                Int64 activity_id = Convert.ToInt64(id);
                
                // insert facebook post
                cmd = new SqlCommand("sp1_brandyy_User_FB_Activities_Insert");
                cmd.Parameters.AddWithValue("@Action", "Facebook");
                cmd.Parameters.AddWithValue("@activity_id", activity_id);
                cmd.Parameters.AddWithValue("@reg_uid", t.reg_uid);
                cmd.Parameters.AddWithValue("@brand_id", brand_id1);
                cmd.Parameters.AddWithValue("@campaign_id", campaign_id1);
                cmd.Parameters.AddWithValue("@action_id", action_id1);
                cmd.Parameters.AddWithValue("@created_on", Convert.ToDateTime(i["created_time"]));
                cmd.Parameters.AddWithValue("@updated_time", Convert.ToDateTime(i["updated_time"]));
                if (i.ContainsKey("picture"))
                {
                    cmd.Parameters.AddWithValue("@sm_img_link", i["picture"]);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@sm_img_link", "");
                }
                if (i.ContainsKey("link"))
                {
                    cmd.Parameters.AddWithValue("@link", i["link"]);
                }
                else
                {
                    if (i.ContainsKey("actions"))
                    {
                        cmd.Parameters.AddWithValue("@link", Convert.ToString(i["actions"][0]["link"]));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@link", "");
                    }
                    
                }
                cmd.Parameters.AddWithValue("@sm_desc", i["message"]);
                
                cmd.Parameters.AddWithValue("@fb_post_id", i["id"]);

                ConnObj.GetDataTab(cmd);
                ConnObj.ReleaseConnection();                
            }
        }

    }

    public void getPosts(string accessToken, string post_id)
    {
        var client = new FacebookClient(accessToken);
        dynamic posts;
        posts = client.Get("/" + post_id + "/sharedposts");

        //Loop through the returned users
        foreach (var i in posts["data"])
        {

            ;
        }

    }
    public void checkIfUserCheckedIn(string accessToken, List<fbuser> user, string page_id, Int32 brand_id, Int64 campaign_id, Int64 action_id,
                        decimal max_brandyy_points, Int32 no_of_friends,
                        Int32 no_of_likes, Int32 no_of_shares, decimal reward_per_user, decimal reward_on_friend,
                        decimal reward_on_likes, decimal reward_on_shares, string actionUrl, string action_post_text, string action_post_image_url)
    {
        // database will have the list of pages that a user likes already
        // we only need to cross check with the database

        // get the list of user who like the given page
        ConnObj = new ConnectionClass();
        SqlCommand cmd = new SqlCommand("sp_IChooseIT_Get_Users_WhoCheckedIn");
        cmd.Parameters.AddWithValue("@place_id", page_id);
        cmd.Parameters.AddWithValue("@sm_id", 1);
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            ConnectionClass ConnObj2 = new ConnectionClass();
            foreach (DataRow dr in ConnObj.DataSet.Tables[0].Rows)
            {
                decimal points = 0;

                SqlCommand cmd1 = new SqlCommand("sp1_brandyy_User_Activities_Insert");
                cmd1.Parameters.AddWithValue("@reg_uid", dr["reg_uid"]);
                cmd1.Parameters.AddWithValue("@brand_id", brand_id);
                cmd1.Parameters.AddWithValue("@campaign_id", campaign_id);
                cmd1.Parameters.AddWithValue("@action_id", action_id);
                cmd1.Parameters.AddWithValue("@created_on", dr["created_time"]);
                cmd1.Parameters.AddWithValue("@pid", "");

                // reward table details
                if (reward_per_user > 0)
                {
                    points = reward_per_user;
                }
                if (reward_on_friend > 0)
                {
                    fbuser t = user.FirstOrDefault(item => item.reg_uid == Convert.ToInt64(dr["reg_uid"]));
                    // get number of friends for the user
                    no_of_friends = t.no_of_friends;
                    points += (reward_on_friend * no_of_friends);
                }

                decimal reward_amount = (points > max_brandyy_points) ? max_brandyy_points : points;

                cmd1.Parameters.AddWithValue("@reward_amount", reward_amount);
                cmd1.Parameters.AddWithValue("@no_of_friends", no_of_friends);
                cmd1.Parameters.AddWithValue("@no_of_likes", no_of_likes);
                cmd1.Parameters.AddWithValue("@no_of_shares", no_of_shares);
                cmd1.Parameters.AddWithValue("@reward_per_user", reward_per_user);
                cmd1.Parameters.AddWithValue("@reward_on_friend", reward_on_friend);
                cmd1.Parameters.AddWithValue("@reward_on_likes", reward_on_likes);
                cmd1.Parameters.AddWithValue("@reward_on_shares", reward_on_shares);
                cmd1.Parameters.AddWithValue("@action_url", actionUrl);
                cmd1.Parameters.AddWithValue("@activity_text", action_post_text);
                cmd1.Parameters.AddWithValue("@acitivity_img", action_post_image_url);
                cmd1.Parameters.AddWithValue("@returnid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                ConnObj2.GetDataTab(cmd1);
            }

            ConnObj2.ReleaseConnection();
            ConnObj.ReleaseConnection();
        }
    }

    public void testcheckIfUserHasAddedAnyPost(string accessToken)
    {
        var client = new FacebookClient(accessToken);
        dynamic posts;
        posts = client.Get("/172918806177456/feed");

        //Loop through the returned users
        foreach (var i in posts["data"])
        {
            string msg = i["message"];
        }
    }
}