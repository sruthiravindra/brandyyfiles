using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for importinstaprofiledetails
/// </summary>
public class importinstaprofiledetails
{
    ConnectionClass ConnObj = new ConnectionClass();
    string accessToken_1 = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Insta_access_token"]);
	public importinstaprofiledetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void checkIfUserLikesAPage(string accessToken, List<fbuser> user, string page_id, Int32 brand_id, Int64 campaign_id, Int64 action_id,
                       decimal reward_amount, decimal reward_amount_max, decimal reward_percent, decimal reward_percent_max, string reward_product, decimal reward_point, Int32 no_of_friends,
                       Int32 no_of_likes, Int32 no_of_shares, decimal reward_per_user, decimal reward_on_friend,
                       decimal reward_on_likes, decimal reward_on_shares)
    {
        // database will have the list of pages that a user likes already
        // we only need to cross check with the database


        fbuser t = user[0];

        // get the list of user who like the given page
        ConnObj = new ConnectionClass();
        SqlCommand cmd = new SqlCommand("sp_IChooseIT_Get_Users_WhoLikeAPage");
        cmd.Parameters.AddWithValue("@page_id", page_id);
        cmd.Parameters.AddWithValue("@sm_id", 3);
        ConnObj.GetDataSet(cmd);
        decimal points = 0;
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            ConnectionClass ConnObj2 = new ConnectionClass();
            foreach (DataRow dr in ConnObj.DataSet.Tables[0].Rows)
            {
                if (Convert.ToInt64(dr["reg_uid"]) != t.reg_uid) continue;

                SqlCommand cmd1 = new SqlCommand("sp1_IChooseIT_User_Activities_Insert");
                cmd1.Parameters.AddWithValue("@reg_uid", t.reg_uid);
                cmd1.Parameters.AddWithValue("@brand_id", brand_id);
                cmd1.Parameters.AddWithValue("@campaign_id", campaign_id);
                cmd1.Parameters.AddWithValue("@action_id", action_id);
                cmd1.Parameters.AddWithValue("@created_on", DateTime.Now.ToString());
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

            ConnObj2.ReleaseConnection();
            ConnObj.ReleaseConnection();
        }
    }

    public void checkIfUserLikedAPost(string accessToken, List<fbuser> user, string post_id, Int32 brand_id, Int64 campaign_id, Int64 action_id,
                       decimal reward_amount, decimal reward_amount_max, decimal reward_percent, decimal reward_percent_max, string reward_product, decimal reward_point, Int32 no_of_friends,
                       Int32 no_of_likes, Int32 no_of_shares, decimal reward_per_user, decimal reward_on_friend,
                       decimal reward_on_likes, decimal reward_on_shares)
    {
        foreach (fbuser t in user)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.instagram.com/v1/media/" + post_id + "/likes?access_token=" + accessToken_1);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            WebResponse response = request.GetResponse();
            string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var tmpResult = JObject.Parse(responseData);

            var resultObject = tmpResult["data"].Values<JObject>()
             .Where(n => n["id"].Value<string>().Contains(t.sm_uid))
             .Select(n => new { username = n["username"], id = n["id"], Context = n.Parent }).ToArray();

            try
            {

                if (resultObject.Count() > 0)
                {

                    #region save to sp1_IChooseIT_User_Activities_Insert

                    ConnObj = new ConnectionClass();
                    SqlCommand cmd = new SqlCommand("sp1_IChooseIT_User_Activities_Insert");
                    cmd.Parameters.AddWithValue("@reg_uid", t.reg_uid);
                    cmd.Parameters.AddWithValue("@brand_id", brand_id);
                    cmd.Parameters.AddWithValue("@campaign_id", campaign_id);
                    cmd.Parameters.AddWithValue("@action_id", action_id);
                    cmd.Parameters.AddWithValue("@created_on", "");
                    cmd.Parameters.AddWithValue("@pid", "");

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
                    #endregion

                }

            }
            catch (Exception ex)
            {
            }
        }


    }

    public void checkIfUserHasAddedAnyPost(string accessToken, List<fbuser> user, string hashtag, string defaulttext, String last_cron_run, Int32 brand_id, Int64 campaign_id, Int64 action_id,
                        decimal reward_amount, decimal reward_amount_max, decimal reward_percent, decimal reward_percent_max, string reward_product, decimal reward_point, Int32 no_of_friends,
                        Int32 no_of_likes, Int32 no_of_shares, decimal reward_per_user, decimal reward_on_friend,
                        decimal reward_on_likes, decimal reward_on_shares)
    {
        fbuser t = user[0];

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.instagram.com/v1/users/" + t.sm_uid + "/media/recent/?access_token=" + accessToken_1);
        request.Method = "GET";
        request.ContentType = "application/x-www-form-urlencoded";
        WebResponse response = request.GetResponse();
        string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();
        var tmpResult = JObject.Parse(responseData);

        var resultObject = tmpResult["data"].Values<JObject>()
        .Where(n => n["caption"].Count() > 0)
        .Where(n => n["caption"]["text"].Value<string>().Contains("@ichooseit"))
        .Select(n => new { img = n["images"]["thumbnail"]["url"], link = n["link"], text = n["caption"]["text"], mediaID = n["id"], created_time = n["created_time"], likes = n["likes"]["count"], Context = n.Parent }).ToArray();
        
        //Loop through the returned users
        foreach (var i in resultObject)
        {

            Int32 brand_id1; Int64 campaign_id1; Int64 action_id1;

            string message = Convert.ToString( i.text ).ToLower();
            hashtag = hashtag.Replace("@" + System.Configuration.ConfigurationManager.AppSettings["InstaIchooseItHashtag"], "").ToLower();
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
            

                ConnObj = new ConnectionClass();
                SqlCommand cmd = new SqlCommand("sp1_IChooseIT_User_Activities_Insert");

                // case : valid
                if (valid == true)
                {
                    #region for brand post
                    brand_id1 = brand_id; campaign_id1 = campaign_id; action_id1 = action_id;
                    cmd.Parameters.AddWithValue("@reg_uid", t.reg_uid);
                    cmd.Parameters.AddWithValue("@brand_id", brand_id);
                    cmd.Parameters.AddWithValue("@campaign_id", campaign_id);
                    cmd.Parameters.AddWithValue("@action_id", action_id);
                    cmd.Parameters.AddWithValue("@created_on", ConvertFromUnixTimestamp(Convert.ToDouble(i.created_time)));
                    cmd.Parameters.AddWithValue("@pid", Convert.ToString(i.mediaID));
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
                        if ( Convert.ToInt32( i.likes ) > 0 )
                        {
                            no_of_likes = Convert.ToInt32( i.likes );
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
                    #endregion
                }
                else
                {                    
                    brand_id1 = 2; campaign_id1 = 8; action_id1 = 27;
                    cmd.Parameters.AddWithValue("@reg_uid", t.reg_uid);
                    cmd.Parameters.AddWithValue("@brand_id", brand_id1);
                    cmd.Parameters.AddWithValue("@campaign_id", campaign_id1);
                    cmd.Parameters.AddWithValue("@action_id", action_id1);
                    cmd.Parameters.AddWithValue("@created_on", Convert.ToDateTime(ConvertFromUnixTimestamp(Convert.ToDouble(i.created_time))));//
                    cmd.Parameters.AddWithValue("@pid", Convert.ToString( i.mediaID ));
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
                cmd = new SqlCommand("sp1_IChooseIT_User_Insta_Activities_Insert");
                cmd.Parameters.AddWithValue("@activity_id", activity_id);
                cmd.Parameters.AddWithValue("@reg_uid", t.reg_uid);
                cmd.Parameters.AddWithValue("@brand_id", brand_id1);
                cmd.Parameters.AddWithValue("@campaign_id", campaign_id1);
                cmd.Parameters.AddWithValue("@action_id", action_id1);
                cmd.Parameters.AddWithValue("@created_on", ConvertFromUnixTimestamp(Convert.ToDouble( i.created_time)) );
                cmd.Parameters.AddWithValue("@updated_time", ConvertFromUnixTimestamp(Convert.ToDouble(i.created_time)));
                cmd.Parameters.AddWithValue("@sm_img_link", Convert.ToString(i.img));
                cmd.Parameters.AddWithValue("@link", Convert.ToString(i.link));
                cmd.Parameters.AddWithValue("@sm_desc", Convert.ToString(i.text));

                cmd.Parameters.AddWithValue("@fb_post_id", Convert.ToString(i.mediaID));

                ConnObj.GetDataTab(cmd);
                ConnObj.ReleaseConnection();
            
        }


    }
    private DateTime ConvertFromUnixTimestamp(double timestamp)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return origin.AddSeconds(timestamp);
    }
}