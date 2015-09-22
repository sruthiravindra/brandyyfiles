using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Data.SqlClient;
using Facebook;
using System.Data;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for importfbprofiledetails
/// </summary>
public class importfbprofiledetails
{
    ConnectionClass ConnObj = new ConnectionClass();
    public importfbprofiledetails()
    {
    }    

    public void getAllProfileDetails(string accessToken)
	{        
        var client = new FacebookClient(accessToken);
        
        //http://blog.prabir.me/posts/facebook-csharp-sdk-making-requests

        dynamic posts = (IDictionary<string, object>)client.Get("/me");

        String insert_stmt = ""; String insert_stmt_comma = "";
        //string reg_uid = Convert.ToString( SessionState._SignInUser.reg_uid );
        string reg_uid = "1";
        int verified_status=0;
        foreach (KeyValuePair<string, object> admObj in posts)
        {
            var id = Convert.ToString(admObj.Key);
            var value = Convert.ToString(admObj.Value);
            insert_stmt += insert_stmt_comma + "(1," + reg_uid + ",'" + id + "', '" + value + "', '/me' )";
            insert_stmt_comma = ",";

            if (id == "verified")
            {
                verified_status = (value == "True") ? 1 : 0;
            }
        }

        if (insert_stmt != "")
        {
            ConnObj = new ConnectionClass();


            SqlCommand cmd = new SqlCommand("sp1_Insert_IChooseIT_User_SM_All_Details");
            cmd.Parameters.AddWithValue("@reg_uid", reg_uid);
            cmd.Parameters.AddWithValue("@insert_stmt", insert_stmt);
            cmd.Parameters.AddWithValue("@sm_id", 1);
            ConnObj.GetDataTab(cmd);

            cmd = new SqlCommand("sp1_Update_IChooseIT_Registrations_FB_Verify");
            cmd.Parameters.AddWithValue("@reg_uid", reg_uid);
            cmd.Parameters.AddWithValue("@verified_status", verified_status);            
            cmd.Parameters.AddWithValue("@sm_id", 1);
            ConnObj.GetDataTab(cmd);

            ConnObj.ReleaseConnection();
        }
                

	}

    public void getUserPageLikes(string accessToken, Int64 reg_uid)
    {
        var client = new FacebookClient(accessToken);
        
        dynamic posts = client.Get("/me/likes");

        String insert_stmt = ""; String insert_stmt_comma = "";
        string uid = Convert.ToString( reg_uid );
        
        string category; string page_name; DateTime created_time; string page_id;

        //Loop through the returned friends
        foreach (var i in posts["data"])
        {
            category = System.Web.HttpUtility.HtmlEncode(i["category"]);
            page_name = System.Web.HttpUtility.HtmlEncode(i["name"]);
            created_time = Convert.ToDateTime(  i["created_time"] );
            page_id = i["id"];

            insert_stmt += insert_stmt_comma + "(" + uid + ",'" + category + "', '" + page_name + "', '" + created_time.ToString() + "','" + page_id + "' )";
            insert_stmt_comma = ",";

        }

        if (insert_stmt != "")
        {
            ConnObj = new ConnectionClass();
            SqlCommand cmd = new SqlCommand("sp1_brandyy_User_FB_Likes_Insert");
            cmd.Parameters.AddWithValue("@insert_stmt", insert_stmt);
            cmd.Parameters.AddWithValue("@reg_uid", reg_uid);
            ConnObj.GetDataTab(cmd);
            ConnObj.ReleaseConnection();
        }
    }
    
    public void getUserTaggedPlaces(string accessToken, Int64 reg_uid)
    {
        var client = new FacebookClient(accessToken);

        dynamic posts = client.Get("/me/tagged_places");

        String insert_stmt = ""; String insert_stmt_comma = "";
        string uid = Convert.ToString(reg_uid);

        string location; string place_name; DateTime created_time; string data_id; string place_id;

        //Loop through the returned locations
        foreach (var i in posts["data"])
        {           
            
            created_time = Convert.ToDateTime(i["created_time"]);
            data_id = i["id"];

            location = System.Web.HttpUtility.HtmlEncode(i["place"]["location"]);
            place_id = System.Web.HttpUtility.HtmlEncode(i["place"]["id"]);
            place_name = System.Web.HttpUtility.HtmlEncode(i["place"]["name"]);

            insert_stmt += insert_stmt_comma + "(" + uid + ", '" + data_id + "', '" + place_id + "','" + location + "', '" + place_name + "', '" + created_time.ToString() + "' )";
            insert_stmt_comma = ",";

        }

        if (insert_stmt != "")
        {
            ConnObj = new ConnectionClass();
            SqlCommand cmd = new SqlCommand("sp1_IChooseIT_User_FB_Locations_Insert");
            cmd.Parameters.AddWithValue("@insert_stmt", insert_stmt);
            cmd.Parameters.AddWithValue("@reg_uid", reg_uid);
            ConnObj.GetDataTab(cmd);
            ConnObj.ReleaseConnection();
        }


    }

    public void getUserFriends(string accessToken, Int64 reg_uid)
    {
        var client = new FacebookClient(accessToken);

        dynamic posts = client.Get("/me/friends");

        String insert_stmt = ""; String insert_stmt_comma = "";
        string uid = Convert.ToString(reg_uid);

        string data_id; string sm_id;
        sm_id="1";

        //Loop through the returned locations
        foreach (var i in posts["data"])
        {
            data_id = i["id"];

            insert_stmt += insert_stmt_comma + "(" + sm_id + ", " + uid + ", '" + data_id + "' )";
            insert_stmt_comma = ",";

        }

        if (insert_stmt != "")
        {
            ConnObj = new ConnectionClass();
            SqlCommand cmd = new SqlCommand("sp1_Insert_IChooseIT_UseR_SM_Friends");
            cmd.Parameters.AddWithValue("@insert_stmt", insert_stmt);
            cmd.Parameters.AddWithValue("@reg_uid", reg_uid);
            cmd.Parameters.AddWithValue("@sm_id", sm_id);
            ConnObj.GetDataTab(cmd);
            ConnObj.ReleaseConnection();
        }


    }

    public void checkIfUserHasAddedAnyPost(string accessToken, List<fbuser> user, string hashtag, string defaulttext, String last_cron_run, Int32 brand_id, Int64 campaign_id, Int64 action_id,
                        decimal reward_amount, decimal reward_amount_max, decimal reward_percent, decimal reward_percent_max, string reward_product, decimal reward_point, Int32 no_of_friends,
                        Int32 no_of_likes, Int32 no_of_shares, decimal reward_per_user, decimal reward_on_friend,
                        decimal reward_on_likes, decimal reward_on_shares)
    {
        var client = new FacebookClient(accessToken);
        dynamic posts;
        if ((last_cron_run == ""))
        {
            posts = client.Get("/cooseit/tagged");
        }
        else
        {
            posts = client.Get("/cooseit/tagged?since=" + last_cron_run);
        }

        fbuser t = user[0];

        //Loop through the returned users
        foreach (var i in posts["data"])
        {

            Int32 brand_id1; Int64 campaign_id1; Int64 action_id1;

            string message = i["message"];
            hashtag = hashtag.Replace("@ichoose", "");
            string[] hashtag_arr = hashtag.Split(' ');
            bool valid = true;
            foreach (string hash in hashtag_arr)
            {
                if (!message.Contains(hash.Trim()))
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


            
            // check if the post link contains the post id            
            if (t.sm_uid == i["from"].id)
            {

                ConnObj = new ConnectionClass();
                SqlCommand cmd = new SqlCommand("sp1_IChooseIT_User_Activities_Insert");

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
                    if (reward_point > 0)
                    {
                        reward_amount = (reward_amount / reward_point) * points;
                        reward_percent = (reward_percent / reward_point) * points;

                        reward_amount = (reward_amount > reward_amount_max) ? reward_amount_max : reward_amount;
                        reward_percent = (reward_percent > reward_percent_max) ? reward_percent_max : reward_percent;
                    }

                    cmd.Parameters.AddWithValue("@reward_amount", reward_amount);
                    cmd.Parameters.AddWithValue("@reward_percent", reward_percent);
                    cmd.Parameters.AddWithValue("@reward_product", reward_product);
                    cmd.Parameters.AddWithValue("@no_of_friends", no_of_friends);
                    cmd.Parameters.AddWithValue("@no_of_likes", no_of_likes);
                    cmd.Parameters.AddWithValue("@no_of_shares", no_of_shares);
                    cmd.Parameters.AddWithValue("@reward_per_user", reward_per_user);
                    cmd.Parameters.AddWithValue("@reward_on_friend", reward_on_friend);
                    cmd.Parameters.AddWithValue("@reward_on_likes", reward_on_likes);
                    cmd.Parameters.AddWithValue("@reward_on_shares", reward_on_shares);
                    cmd.Parameters.AddWithValue("@returnid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                    ConnObj.GetDataTab(cmd);
                }
                else
                {
                    brand_id1 = 2; campaign_id1 = 8; action_id1 = 16;
                    cmd.Parameters.AddWithValue("@reg_uid", t.reg_uid);
                    cmd.Parameters.AddWithValue("@brand_id", brand_id1);
                    cmd.Parameters.AddWithValue("@campaign_id", campaign_id1);
                    cmd.Parameters.AddWithValue("@action_id", action_id1);
                    cmd.Parameters.AddWithValue("@created_on", Convert.ToDateTime(i["created_time"]));
                    cmd.Parameters.AddWithValue("@pid", i["id"]);
                    cmd.Parameters.AddWithValue("@reward_amount", 0);
                    cmd.Parameters.AddWithValue("@reward_percent", 0);
                    cmd.Parameters.AddWithValue("@reward_product", "");
                    cmd.Parameters.AddWithValue("@no_of_friends", 0);
                    cmd.Parameters.AddWithValue("@no_of_likes", 0);
                    cmd.Parameters.AddWithValue("@no_of_shares", 0);
                    cmd.Parameters.AddWithValue("@reward_per_user", 0);
                    cmd.Parameters.AddWithValue("@reward_on_friend", 0);
                    cmd.Parameters.AddWithValue("@reward_on_likes", 0);
                    cmd.Parameters.AddWithValue("@reward_on_shares", 0);
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
                cmd = new SqlCommand("sp1_IChooseIT_User_FB_Activities_Insert");
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
    public void getUserPosts(string accessToken, Int64 reg_uid)
    {
        var client = new FacebookClient(accessToken);

        dynamic posts = client.Get("/me/posts");

        //String insert_stmt = ""; String insert_stmt_comma = "";
        //string uid = Convert.ToString(reg_uid);

        //string data_id; string sm_id;
        //sm_id = "1";

        ////Loop through the returned locations
        //foreach (var i in posts["data"])
        //{
        //    data_id = i["id"];

        //    insert_stmt += insert_stmt_comma + "(" + sm_id + ", " + uid + ", '" + data_id + "' )";
        //    insert_stmt_comma = ",";

        //}

        //if (insert_stmt != "")
        //{
        //    ConnObj = new ConnectionClass();
        //    SqlCommand cmd = new SqlCommand("sp1_Insert_IChooseIT_UseR_SM_Friends");
        //    cmd.Parameters.AddWithValue("@insert_stmt", insert_stmt);
        //    cmd.Parameters.AddWithValue("@reg_uid", reg_uid);
        //    cmd.Parameters.AddWithValue("@sm_id", sm_id);
        //    ConnObj.GetDataTab(cmd);
        //    ConnObj.ReleaseConnection();
        //}


    }

    public void checkIfUserLikesAPost(string accessToken, List<fbuser> user, string post_id, Int32 brand_id, Int64 campaign_id, Int64 action_id,
                        decimal reward_amount, decimal reward_amount_max, decimal reward_percent, decimal reward_percent_max, string reward_product, decimal reward_point, Int32 no_of_friends,
                        Int32 no_of_likes, Int32 no_of_shares, decimal reward_per_user, decimal reward_on_friend,
                        decimal reward_on_likes, decimal reward_on_shares)        
    {
        var client = new FacebookClient(accessToken);

        dynamic posts = client.Get("/" + post_id + "/likes");

        fbuser t = user[0];

        string json = Newtonsoft.Json.JsonConvert.SerializeObject(posts);
        JObject tmpResult = JObject.Parse(json);
        var i = tmpResult["data"].Values<JObject>()
        .Where(n => n["id"].Value<string>().Contains(t.sm_uid))
        .Select(n => new { link = n }).ToArray();

        // check if the post link contains the post id
        if (i.Count() > 0)
        {
            decimal points = 0;
            ConnObj = new ConnectionClass();
            SqlCommand cmd = new SqlCommand("sp1_IChooseIT_User_Activities_Insert");
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
            if (reward_point > 0)
            {
                reward_amount = (reward_amount / reward_point) * points;
                reward_percent = (reward_percent / reward_point) * points;

                reward_amount = (reward_amount > reward_amount_max) ? reward_amount_max : reward_amount;
                reward_percent = (reward_percent > reward_percent_max) ? reward_percent_max : reward_percent;
            }
            cmd.Parameters.AddWithValue("@reward_amount", reward_amount);
            cmd.Parameters.AddWithValue("@reward_percent", reward_percent);
            cmd.Parameters.AddWithValue("@reward_product", reward_product);
            cmd.Parameters.AddWithValue("@no_of_friends", no_of_friends);
            cmd.Parameters.AddWithValue("@no_of_likes", no_of_likes);
            cmd.Parameters.AddWithValue("@no_of_shares", no_of_shares);
            cmd.Parameters.AddWithValue("@reward_per_user", reward_per_user);
            cmd.Parameters.AddWithValue("@reward_on_friend", reward_on_friend);
            cmd.Parameters.AddWithValue("@reward_on_likes", reward_on_likes);
            cmd.Parameters.AddWithValue("@reward_on_shares", reward_on_shares);
            cmd.Parameters.AddWithValue("@returnid", SqlDbType.BigInt).Direction = ParameterDirection.Output;

            ConnObj.GetDataTab(cmd);
            ConnObj.ReleaseConnection();
        }
    }

    public void checkIfUserLikesAPage(string accessToken, List<fbuser> user, string page_id, Int32 brand_id, Int64 campaign_id, Int64 action_id,
                        decimal reward_amount, decimal reward_amount_max, decimal reward_percent, decimal reward_percent_max, string reward_product, decimal reward_point, Int32 no_of_friends,
                        Int32 no_of_likes, Int32 no_of_shares, decimal reward_per_user, decimal reward_on_friend,
                        decimal reward_on_likes, decimal reward_on_shares)        
    {
        var client = new FacebookClient(accessToken);

        dynamic posts = client.Get("/me/likes/" + page_id);

        //Loop through the returned locations
        foreach (var i in posts["data"])
        {
            decimal points = 0;
            fbuser t = user[0];
            ConnectionClass ConnObj2 = new ConnectionClass();
            SqlCommand cmd1 = new SqlCommand("sp1_IChooseIT_User_Activities_Insert");
            cmd1.Parameters.AddWithValue("@reg_uid", t.reg_uid);
            cmd1.Parameters.AddWithValue("@brand_id", brand_id);
            cmd1.Parameters.AddWithValue("@campaign_id", campaign_id);
            cmd1.Parameters.AddWithValue("@action_id", action_id);
            cmd1.Parameters.AddWithValue("@created_on", "");
            cmd1.Parameters.AddWithValue("@pid", "");

            // reward table details
            if (reward_per_user > 0)
            {
                points = reward_per_user;
            }
            if (reward_on_friend > 0)
            {
                
                // get number of friends for the user
                no_of_friends = t.no_of_friends;
                points += (reward_on_friend * no_of_friends);
            }

            if (reward_point > 0)
            {
                reward_amount = (reward_amount / reward_point) * points;
                reward_percent = (reward_percent / reward_point) * points;

                reward_amount = (reward_amount > reward_amount_max) ? reward_amount_max : reward_amount;
                reward_percent = (reward_percent > reward_percent_max) ? reward_percent_max : reward_percent;
            }

            cmd1.Parameters.AddWithValue("@reward_amount", reward_amount);
            cmd1.Parameters.AddWithValue("@reward_percent", reward_percent);
            cmd1.Parameters.AddWithValue("@reward_product", reward_product);
            cmd1.Parameters.AddWithValue("@no_of_friends", no_of_friends);
            cmd1.Parameters.AddWithValue("@no_of_likes", no_of_likes);
            cmd1.Parameters.AddWithValue("@no_of_shares", no_of_shares);
            cmd1.Parameters.AddWithValue("@reward_per_user", reward_per_user);
            cmd1.Parameters.AddWithValue("@reward_on_friend", reward_on_friend);
            cmd1.Parameters.AddWithValue("@reward_on_likes", reward_on_likes);
            cmd1.Parameters.AddWithValue("@reward_on_shares", reward_on_shares);
            cmd1.Parameters.AddWithValue("@returnid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
            ConnObj2.GetDataTab(cmd1);
        }
    }

    public void checkIfUserSharedAPost(string accessToken, List<fbuser> user, string post_id, Int32 brand_id, Int64 campaign_id, Int64 action_id,
                        decimal reward_amount, decimal reward_amount_max, decimal reward_percent, decimal reward_percent_max, string reward_product, decimal reward_point, Int32 no_of_friends,
                        Int32 no_of_likes, Int32 no_of_shares, decimal reward_per_user, decimal reward_on_friend,
                        decimal reward_on_likes, decimal reward_on_shares)        
    {

        var client = new FacebookClient(accessToken);

        dynamic posts = client.Get("/me/links/");
        fbuser u = user[0];

        //Loop through the returned link
        decimal points = 0;
        string post1 = post_id;
        if (post_id.Split('_').Length > 1)
        {
            post1 = post_id.Split('_')[1];
        }

        string json = Newtonsoft.Json.JsonConvert.SerializeObject(posts);
        JObject tmpResult = JObject.Parse(json);         
        var i = tmpResult["data"].Values<JObject>()
        .Where(n => n["link"].Value<string>().Contains(post1))
        .Select(n => new { link = n["created_time"] }).ToArray();

            // check if the post link contains the post id
            if (i.Count() > 0 )
            {                
                ConnObj = new ConnectionClass();
                SqlCommand cmd = new SqlCommand("sp1_IChooseIT_User_Activities_Insert");
                cmd.Parameters.AddWithValue("@reg_uid", u.reg_uid);
                cmd.Parameters.AddWithValue("@brand_id", brand_id);
                cmd.Parameters.AddWithValue("@campaign_id", campaign_id);
                cmd.Parameters.AddWithValue("@action_id", action_id);
                cmd.Parameters.AddWithValue("@created_on", Convert.ToDateTime(i[0].link));
                cmd.Parameters.AddWithValue("@pid", "");

                // reward table details
                if (reward_per_user > 0)
                {
                    points = reward_per_user;
                }
                if (reward_on_friend > 0)
                {
                    // get number of friends for the user
                    no_of_friends = u.no_of_friends;
                    points += (reward_on_friend * no_of_friends);
                }

                if (reward_point > 0)
                {
                    reward_amount = (reward_amount / reward_point) * points;
                    reward_percent = (reward_percent / reward_point) * points;

                    reward_amount = (reward_amount > reward_amount_max) ? reward_amount_max : reward_amount;
                    reward_percent = (reward_percent > reward_percent_max) ? reward_percent_max : reward_percent;
                }

                cmd.Parameters.AddWithValue("@reward_amount", reward_amount);
                cmd.Parameters.AddWithValue("@reward_percent", reward_percent);
                cmd.Parameters.AddWithValue("@reward_product", reward_product);
                cmd.Parameters.AddWithValue("@no_of_friends", no_of_friends);
                cmd.Parameters.AddWithValue("@no_of_likes", no_of_likes);
                cmd.Parameters.AddWithValue("@no_of_shares", no_of_shares);
                cmd.Parameters.AddWithValue("@reward_per_user", reward_per_user);
                cmd.Parameters.AddWithValue("@reward_on_friend", reward_on_friend);
                cmd.Parameters.AddWithValue("@reward_on_likes", reward_on_likes);
                cmd.Parameters.AddWithValue("@reward_on_shares", reward_on_shares);
                cmd.Parameters.AddWithValue("@returnid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                ConnObj.GetDataTab(cmd);
                ConnObj.ReleaseConnection();                            
        }
    }

    

}