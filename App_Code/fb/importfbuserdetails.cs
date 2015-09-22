using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Facebook;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
/// <summary>
/// this class is called when user has not logged in and the scheduler is syncing data
/// </summary>
public class importfbuserdetails
{
    ConnectionClass ConnObj = new ConnectionClass();
    public importfbuserdetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void getAllPotsDetails(string reg_uid, string accessToken, Int64 no_of_friends)
    {
        var client = new FacebookClient(accessToken);
        DateTime last_cron_run = DateTime.Today;
        //string dt = "5/01/2015 12:00:00 AM";
        //DateTime last_cron_run = Convert.ToDateTime(dt);

        SqlCommand cmd = new SqlCommand("sp_user_get_BrandCampaignList");// for Campaign type 9 ,10
        cmd.Parameters.AddWithValue("@Action", "Post");
        ConnObj.GetDataSet(cmd);
        dynamic posts = client.Get("me/posts?since=" + last_cron_run);

            foreach (var i in posts["data"])
            {
                if (i.ContainsKey("message") && i["type"] != "link")
                {
                foreach (DataRow item in ConnObj.DataSet.Tables[0].Rows)
                {
                    if (i["message"].Contains(item["val_4"].ToString()) && i["message"].Contains(item["val_3"].ToString()) && i["message"].Contains(item["val_2"].ToString()))
                     {
                         decimal points = 0;
                         if (Convert.ToDecimal(item["reward_user"]) > 0)
                         {
                             points += Convert.ToDecimal(item["reward_user"]);
                         }
                         if (Convert.ToDecimal(item["reward_per_friend"]) > 0)
                         {
                             points += (Convert.ToDecimal(item["reward_per_friend"]) * no_of_friends);
                         }
                         if (Convert.ToDecimal(item["reward_per_like"]) > 0)
                         {
                             decimal no_of_likes = i.ContainsKey("likes") ? i["likes"]["data"].Count : 0;
                             points += (no_of_likes * Convert.ToDecimal(item["reward_per_like"]));
                         }
                         if (Convert.ToDecimal(item["reward_per_share"]) > 0)
                         {
                             decimal no_of_shares = i.ContainsKey("shares") ? i["shares"]["count"] : 0;
                             points += (no_of_shares * Convert.ToDecimal(item["reward_per_share"]));
                         }


                         decimal reward_amount = (points > Convert.ToDecimal(item["max_brandyy_points"])) ? Convert.ToDecimal(item["max_brandyy_points"]) : points;

                         SqlCommand cmd1 = new SqlCommand("sp1_brandyy_User_Activities_Insert");
                         cmd1.Parameters.AddWithValue("@reg_uid", Convert.ToInt64(reg_uid));
                         cmd1.Parameters.AddWithValue("@brand_id", Convert.ToInt32(item["brand_id"]));
                         cmd1.Parameters.AddWithValue("@campaign_id", Convert.ToInt64(item["campaign_id"]));
                         cmd1.Parameters.AddWithValue("@action_id", Convert.ToInt64(item["action_id"]));
                         cmd1.Parameters.AddWithValue("@created_on", Convert.ToDateTime(i["created_time"]));
                         cmd1.Parameters.AddWithValue("@pid", i["id"]);
                         cmd1.Parameters.AddWithValue("@reward_amount", reward_amount);
                         cmd1.Parameters.AddWithValue("@no_of_friends", no_of_friends);
                         cmd1.Parameters.AddWithValue("@no_of_likes", i.ContainsKey("likes") ? i["likes"]["data"].Count : 0);
                         cmd1.Parameters.AddWithValue("@no_of_shares", i.ContainsKey("shares") ? i["shares"]["count"] : 0);
                         cmd1.Parameters.AddWithValue("@reward_per_user", Convert.ToDecimal(item["reward_user"]));
                         cmd1.Parameters.AddWithValue("@reward_on_friend", Convert.ToDecimal(item["reward_per_friend"]));
                         cmd1.Parameters.AddWithValue("@reward_on_likes", Convert.ToDecimal(item["reward_per_like"]));
                         cmd1.Parameters.AddWithValue("@reward_on_shares", Convert.ToDecimal(item["reward_per_share"]));
                         cmd1.Parameters.AddWithValue("@action_url", Convert.ToString(i.ContainsKey("actions") ? i["actions"][0]["link"] : ""));
                         cmd1.Parameters.AddWithValue("@activity_text", Convert.ToString(i.ContainsKey("message") ? i["message"] : ""));
                         cmd1.Parameters.AddWithValue("@acitivity_img", Convert.ToString(i.ContainsKey("picture") ? i["picture"] : ""));
                         cmd1.Parameters.AddWithValue("@returnid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                         ConnObj.GetDataTab(cmd1);
                         string id = cmd1.Parameters["@returnid"].Value.ToString();
                         if (id == "")
                         {
                             continue;
                         }

                         Int64 activity_id = Convert.ToInt64(id);

                         #region save to IChooseIT_User_Activities_Tw_Post

                         // insert twitter post
                         SqlCommand cmd3 = new SqlCommand("sp1_brandyy_User_FB_Activities_Insert");
                         cmd3.Parameters.AddWithValue("@activity_id", activity_id);
                         cmd3.Parameters.AddWithValue("@reg_uid", reg_uid);
                         cmd3.Parameters.AddWithValue("@brand_id", Convert.ToInt32(item["brand_id"]));
                         cmd3.Parameters.AddWithValue("@campaign_id", Convert.ToInt32(item["campaign_id"]));
                         cmd3.Parameters.AddWithValue("@action_id", Convert.ToInt32(item["action_id"]));
                         cmd3.Parameters.AddWithValue("@created_on", Convert.ToDateTime(i["created_time"]));
                         cmd3.Parameters.AddWithValue("@updated_time", Convert.ToDateTime(i["updated_time"]));
                         cmd3.Parameters.AddWithValue("@sm_img_link", i.ContainsKey("picture") ? i["picture"] : "");
                         cmd3.Parameters.AddWithValue("@sm_desc", i.ContainsKey("message") ? i["message"] : "");
                         //cmd3.Parameters.AddWithValue("@link", i.ContainsKey("link") ? i["link"] : "");
                         cmd3.Parameters.AddWithValue("@link", i.ContainsKey("actions") ? i["actions"][0]["link"] : "");
                         cmd3.Parameters.AddWithValue("@fb_post_id", i["id"]);
                         cmd3.Parameters.AddWithValue("@Action", "Facebook");
                         ConnObj.GetDataTab(cmd3);
                         #endregion
                     }
                   }
                }
            }
    }

    public void getAllProfileDetails(string reg_uid, string accessToken, string sm_uid)
    {
        Int64 no_of_friends = 0;
        var client = new FacebookClient(accessToken);
        dynamic posts = client.Get("/me?fields=id,name,email,first_name,last_name,gender,link,location,picture,friends,updated_time,likes,tagged_places,links");
       
        //Profile Details 
        SqlCommand cmd1 = new SqlCommand("sp_user_update_UserProfileDetails");
        cmd1.Parameters.AddWithValue("@reg_uid", Convert.ToInt64(reg_uid));
        cmd1.Parameters.AddWithValue("@sm_id", 1);
        cmd1.Parameters.AddWithValue("@name", posts.ContainsKey("name") ? posts["name"] : "");
        cmd1.Parameters.AddWithValue("@fname", posts.ContainsKey("first_name") ? posts["first_name"] : "");
        cmd1.Parameters.AddWithValue("@lname", posts.ContainsKey("last_name") ? posts["last_name"] : "");
        cmd1.Parameters.AddWithValue("@email", posts.ContainsKey("email") ? posts["email"] : "");
        cmd1.Parameters.AddWithValue("@gender", posts.ContainsKey("gender") ? posts["gender"] : "");
        cmd1.Parameters.AddWithValue("@profile_img_link", posts.ContainsKey("picture") ? posts["picture"]["data"]["url"] : "");
        cmd1.Parameters.AddWithValue("@no_of_friends", posts.ContainsKey("friends") ? posts["friends"]["summary"]["total_count"] : 0);
        cmd1.Parameters.AddWithValue("@sm_uid", sm_uid);
        cmd1.Parameters.AddWithValue("@token", accessToken);
        ConnObj.ExecuteNonQuery(cmd1);

        //Assign no of Friends 
        no_of_friends = posts.ContainsKey("friends") ? posts["friends"]["summary"]["total_count"] : 0;
        
        //tagged places
        if (posts.ContainsKey("tagged_places"))
        {
            foreach (var tag in posts["tagged_places"]["data"])
            {
                SqlCommand cmd2 = new SqlCommand("sp_user_Insert_UserTaggedLocations");
                cmd2.Parameters.AddWithValue("@reg_uid", Convert.ToInt64(reg_uid));
                cmd2.Parameters.AddWithValue("@fb_data_id", tag["id"]);
                cmd2.Parameters.AddWithValue("@place_id", tag["place"]["id"]);
                cmd2.Parameters.AddWithValue("@loc_lat", tag["place"]["location"].ContainsKey("latitude")? tag["place"]["location"]["latitude"] : "0");
                cmd2.Parameters.AddWithValue("@loc_long", tag["place"]["location"].ContainsKey("longitude") ? tag["place"]["location"]["longitude"] : "0");
                cmd2.Parameters.AddWithValue("@name", tag["place"]["name"]);
                cmd2.Parameters.AddWithValue("@created_time", Convert.ToDateTime(tag["created_time"]));
                ConnObj.ExecuteNonQuery(cmd2);
            }
        }

        //Brand Page Likes
        SqlCommand cmd3 = new SqlCommand("sp_user_get_BrandCampaignList");// for Campaign type 1
        cmd3.Parameters.AddWithValue("@Action", "PageLike");
          ConnObj.GetDataSet(cmd3);
        if (posts.ContainsKey("likes"))
        {
            foreach (var like in posts["likes"]["data"]) // all likes
            {
                SqlCommand cmd2 = new SqlCommand("sp_user_Insert_UserPageLikes");
                cmd2.Parameters.AddWithValue("@reg_uid", Convert.ToInt64(reg_uid));
                cmd2.Parameters.AddWithValue("@category", like["category"]);
                cmd2.Parameters.AddWithValue("@name", like["name"]);
                cmd2.Parameters.AddWithValue("@page_id", like["id"]);
                cmd2.Parameters.AddWithValue("@liked_on", Convert.ToDateTime(like["created_time"]));
                ConnObj.ExecuteNonQuery(cmd2);

                foreach (DataRow item in ConnObj.DataSet.Tables[0].Rows) // campaign likes
                {
                    if (like["id"] == item["val_2"].ToString())
                    {
                        decimal points = 0;
                        if (Convert.ToDecimal(item["reward_user"]) > 0)
                        {
                            points += Convert.ToDecimal(item["reward_user"]);
                        }
                        if (Convert.ToDecimal(item["reward_per_friend"]) > 0)
                        {
                            points += (Convert.ToDecimal(item["reward_per_friend"]) * no_of_friends);
                        }

                        decimal reward_amount = (points > Convert.ToDecimal(item["max_brandyy_points"])) ? Convert.ToDecimal(item["max_brandyy_points"]) : points;

                        SqlCommand cmd4 = new SqlCommand("sp1_brandyy_User_Activities_Insert");
                        cmd4.Parameters.AddWithValue("@reg_uid", Convert.ToInt64(reg_uid));
                        cmd4.Parameters.AddWithValue("@brand_id", Convert.ToInt32(item["brand_id"]));
                        cmd4.Parameters.AddWithValue("@campaign_id", Convert.ToInt64(item["campaign_id"]));
                        cmd4.Parameters.AddWithValue("@action_id", Convert.ToInt64(item["action_id"]));
                        cmd4.Parameters.AddWithValue("@created_on", Convert.ToDateTime(like["created_time"]));
                        cmd4.Parameters.AddWithValue("@pid", "");
                        cmd4.Parameters.AddWithValue("@reward_amount", reward_amount);
                        cmd4.Parameters.AddWithValue("@no_of_friends", no_of_friends);
                        cmd4.Parameters.AddWithValue("@no_of_likes", 0);
                        cmd4.Parameters.AddWithValue("@no_of_shares", 0);
                        cmd4.Parameters.AddWithValue("@reward_per_user", Convert.ToDecimal(item["reward_user"]));
                        cmd4.Parameters.AddWithValue("@reward_on_friend", Convert.ToDecimal(item["reward_per_friend"]));
                        cmd4.Parameters.AddWithValue("@reward_on_likes", Convert.ToDecimal(item["reward_per_like"]));
                        cmd4.Parameters.AddWithValue("@reward_on_shares", Convert.ToDecimal(item["reward_per_share"]));
                        cmd4.Parameters.AddWithValue("@action_url", Convert.ToString(item["val_4"]));
                        cmd4.Parameters.AddWithValue("@activity_text", Convert.ToString(item["action_post_text"]));
                        cmd4.Parameters.AddWithValue("@acitivity_img", Convert.ToString(item["action_post_image_url"]));
                        cmd4.Parameters.AddWithValue("@returnid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        ConnObj.ExecuteNonQuery(cmd4);
                    }
                }
            }
        }

        //Shared links
        SqlCommand cmd5 = new SqlCommand("sp_user_get_BrandCampaignList");// for Campaign type 5
        cmd5.Parameters.AddWithValue("@Action", "PostShare");
        ConnObj.GetDataSet(cmd5);
        if (posts.ContainsKey("links"))
        {
            foreach (var lnk in posts["links"]["data"])
            {
                foreach (DataRow item in ConnObj.DataSet.Tables[0].Rows) // campaign links
                {
                    if (lnk["link"].Contains(item["val_2"].ToString())) //Check id in Link
                    {
                        decimal points = 0;
                        if (Convert.ToDecimal(item["reward_user"]) > 0)
                        {
                            points += Convert.ToDecimal(item["reward_user"]);
                        }
                        if (Convert.ToDecimal(item["reward_per_friend"]) > 0)
                        {
                            points += (Convert.ToDecimal(item["reward_per_friend"]) * no_of_friends);
                        }

                        decimal reward_amount = (points > Convert.ToDecimal(item["max_brandyy_points"])) ? Convert.ToDecimal(item["max_brandyy_points"]) : points;

                        SqlCommand cmd6 = new SqlCommand("sp1_brandyy_User_Activities_Insert");
                        cmd6.Parameters.AddWithValue("@reg_uid", Convert.ToInt64(reg_uid));
                        cmd6.Parameters.AddWithValue("@brand_id", Convert.ToInt32(item["brand_id"]));
                        cmd6.Parameters.AddWithValue("@campaign_id", Convert.ToInt64(item["campaign_id"]));
                        cmd6.Parameters.AddWithValue("@action_id", Convert.ToInt64(item["action_id"]));
                        cmd6.Parameters.AddWithValue("@created_on", Convert.ToDateTime(lnk["created_time"]));
                        cmd6.Parameters.AddWithValue("@pid", "");
                        cmd6.Parameters.AddWithValue("@reward_amount", reward_amount);
                        cmd6.Parameters.AddWithValue("@no_of_friends", no_of_friends);
                        cmd6.Parameters.AddWithValue("@no_of_likes", 0);
                        cmd6.Parameters.AddWithValue("@no_of_shares", 0);
                        cmd6.Parameters.AddWithValue("@reward_per_user", Convert.ToDecimal(item["reward_user"]));
                        cmd6.Parameters.AddWithValue("@reward_on_friend", Convert.ToDecimal(item["reward_per_friend"]));
                        cmd6.Parameters.AddWithValue("@reward_on_likes", Convert.ToDecimal(item["reward_per_like"]));
                        cmd6.Parameters.AddWithValue("@reward_on_shares", Convert.ToDecimal(item["reward_per_share"]));
                        cmd6.Parameters.AddWithValue("@action_url", Convert.ToString(item["val_4"]));
                        cmd6.Parameters.AddWithValue("@activity_text", Convert.ToString(item["action_post_text"]));
                        cmd6.Parameters.AddWithValue("@acitivity_img", Convert.ToString(item["action_post_image_url"]));
                        cmd6.Parameters.AddWithValue("@returnid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        ConnObj.ExecuteNonQuery(cmd6);
                    }
                }
            }
        }
        getAllPotsDetails(reg_uid, accessToken, no_of_friends); // Call User Post
        getBrandPostLikes(reg_uid,accessToken, sm_uid, no_of_friends);
    }

    public void getBrandPostLikes(string reg_uid, string accessToken, string sm_uid, Int64 no_of_friends) // for Campaign type 6
    {
        SqlCommand cmd = new SqlCommand("sp_user_get_BrandCampaignList");
        cmd.Parameters.AddWithValue("@Action", "PostLike");
        ConnObj.GetDataSet(cmd);
        foreach (DataRow item in ConnObj.DataSet.Tables[0].Rows) // campaign Post likes
        {
            var client = new FacebookClient(accessToken);
            string postid = Convert.ToString(item["val_2"]);
            dynamic posts = client.Get(postid + "/likes");
            foreach (var i in posts["data"])
            {
                if (i["id"] == sm_uid) 
                {
                    decimal points = 0;
                    if (Convert.ToDecimal(item["reward_user"]) > 0)
                    {
                        points += Convert.ToDecimal(item["reward_user"]);
                    }
                    if (Convert.ToDecimal(item["reward_per_friend"]) > 0)
                    {
                        points += (Convert.ToDecimal(item["reward_per_friend"]) * no_of_friends);
                    }

                    decimal reward_amount = (points > Convert.ToDecimal(item["max_brandyy_points"])) ? Convert.ToDecimal(item["max_brandyy_points"]) : points;
                    DateTime dt = DateTime.Today;
                    SqlCommand cmd6 = new SqlCommand("sp1_brandyy_User_Activities_Insert");
                    cmd6.Parameters.AddWithValue("@reg_uid", Convert.ToInt64(reg_uid));
                    cmd6.Parameters.AddWithValue("@brand_id", Convert.ToInt32(item["brand_id"]));
                    cmd6.Parameters.AddWithValue("@campaign_id", Convert.ToInt64(item["campaign_id"]));
                    cmd6.Parameters.AddWithValue("@action_id", Convert.ToInt64(item["action_id"]));
                    cmd6.Parameters.AddWithValue("@created_on", dt);
                    cmd6.Parameters.AddWithValue("@pid", "");
                    cmd6.Parameters.AddWithValue("@reward_amount", reward_amount);
                    cmd6.Parameters.AddWithValue("@no_of_friends", no_of_friends);
                    cmd6.Parameters.AddWithValue("@no_of_likes", 0);
                    cmd6.Parameters.AddWithValue("@no_of_shares", 0);
                    cmd6.Parameters.AddWithValue("@reward_per_user", Convert.ToDecimal(item["reward_user"]));
                    cmd6.Parameters.AddWithValue("@reward_on_friend", Convert.ToDecimal(item["reward_per_friend"]));
                    cmd6.Parameters.AddWithValue("@reward_on_likes", Convert.ToDecimal(item["reward_per_like"]));
                    cmd6.Parameters.AddWithValue("@reward_on_shares", Convert.ToDecimal(item["reward_per_share"]));
                    cmd6.Parameters.AddWithValue("@action_url", Convert.ToString(item["val_4"]));
                    cmd6.Parameters.AddWithValue("@activity_text", Convert.ToString(item["action_post_text"]));
                    cmd6.Parameters.AddWithValue("@acitivity_img", Convert.ToString(item["action_post_image_url"]));
                    cmd6.Parameters.AddWithValue("@returnid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                    ConnObj.ExecuteNonQuery(cmd6);
                }
            }
        }
    }

    public void getUserFriends(string accessToken, Int64 reg_uid)
    {
        var client = new FacebookClient(accessToken);

        dynamic posts = client.Get("/me/friends");

        String insert_stmt = ""; String insert_stmt_comma = "";
        string uid = Convert.ToString(reg_uid);

        string data_id; string sm_id; string data_name ; Int64 no_of_friends ;
        sm_id = "1";
        no_of_friends = posts["summary"]["total_count"];
        //Loop through the returned locations
        foreach (var i in posts["data"])
        {
            data_id = i["id"];
            data_name = i["name"];
            insert_stmt += insert_stmt_comma + "(" + sm_id + ", " + uid + ", '" + data_id + "','" + data_name + "'  )";
            insert_stmt_comma = ",";
        }

        if (insert_stmt != "")
        {
            SqlCommand cmd = new SqlCommand("sp1_brandyy_UseR_SM_Friends");
            cmd.Parameters.AddWithValue("@insert_stmt", insert_stmt);
            cmd.Parameters.AddWithValue("@reg_uid", SessionState._SignInUser.reg_uid);
            cmd.Parameters.AddWithValue("@sm_id", 1);
            cmd.Parameters.AddWithValue("@no_of_friends", no_of_friends);
            ConnObj.ExecuteNonQuery(cmd);
        }


    }


    public string getUserLongLivedAccessToken(string shortlivedtoken, Int64 reg_uid)
    {
        string atoken = "";
        var fbcl = new FacebookClient();
        //try to get longer token
        try
        {
            dynamic result = fbcl.Get("oauth/access_token?client_id=" + System.Configuration.ConfigurationManager.AppSettings["FacebookAppId"]
                + "&client_secret=" + System.Configuration.ConfigurationManager.AppSettings["FacebookAppSecret"]
                + "&grant_type=fb_exchange_token&fb_exchange_token=" + shortlivedtoken);
            atoken = result.access_token;
        }
        catch
        {
            dynamic result = fbcl.Get("oauth/access_token?client_id=" + System.Configuration.ConfigurationManager.AppSettings["FacebookAppId"]
                + "&client_secret=" + System.Configuration.ConfigurationManager.AppSettings["FacebookAppSecret"]
                + "&grant_type=fb_exchange_token&fb_exchange_token=" + shortlivedtoken);
            atoken = result.access_token;
        }
        return atoken;
    }

}