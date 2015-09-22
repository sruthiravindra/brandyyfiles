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
/// Summary description for importinstauserdetails
/// </summary>
public class importinstauserdetails
{
    public string page_tag;
    ConnectionClass ConnObj = new ConnectionClass();
	public importinstauserdetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void getUserProfileDetails(string reg_uid, string sm_uid, string username, string token) // User recent posts
    {
        Int64 no_of_friends = 0;
        HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create("https://api.instagram.com/v1/users/" + sm_uid + "?access_token=" + token);
        request1.Method = "GET";
        request1.ContentType = "application/x-www-form-urlencoded";
        WebResponse response1 = request1.GetResponse();
        string responseData1 = new StreamReader(response1.GetResponseStream()).ReadToEnd();
        var jsResult = JObject.Parse(responseData1);
        SqlCommand cmd1 = new SqlCommand("sp_user_update_UserProfileDetails");
        cmd1.Parameters.AddWithValue("@reg_uid", Convert.ToInt64(reg_uid));
        cmd1.Parameters.AddWithValue("@sm_id", 3);
        cmd1.Parameters.AddWithValue("@name", (string)jsResult["data"]["full_name"]);
        cmd1.Parameters.AddWithValue("@email", (string)jsResult["data"]["username"]);
        cmd1.Parameters.AddWithValue("@profile_img_link", (string)jsResult["data"]["profile_picture"]);
        cmd1.Parameters.AddWithValue("@no_of_friends", (Int64)jsResult["data"]["counts"]["followed_by"]);
        ConnObj.ExecuteNonQuery(cmd1);
        no_of_friends = (Int64)jsResult["data"]["counts"]["followed_by"];
        getUserPosts(reg_uid, sm_uid, username, token, no_of_friends);
        getUserPostLike(reg_uid, sm_uid, username, no_of_friends, token);
        getUserFollowers(reg_uid, sm_uid, username, no_of_friends, token);

    }

    public void getUserPosts(string reg_uid, string sm_uid, string username, string token, Int64 no_of_friends) // User recent posts
    {
        SqlCommand cmd = new SqlCommand("sp_user_get_Insta_BrandCampaignList");// for Campaign type 19
        cmd.Parameters.AddWithValue("@Action", "Post");
        ConnObj.GetDataSet(cmd);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.instagram.com/v1/users/" + sm_uid + "/media/recent/?access_token=" + token);
        request.Method = "GET";
        request.ContentType = "application/x-www-form-urlencoded";
        WebResponse response = request.GetResponse();
        string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();
        var tmpResult = JObject.Parse(responseData);
        var resultObject = tmpResult["data"].Values<JObject>().ToArray();


        foreach (var i in resultObject) // User Recent Posts
        {
                foreach (DataRow item in ConnObj.DataSet.Tables[0].Rows)
                {

                    string txt = Convert.ToString(i["caption"]) != "" ? Convert.ToString(i["caption"]["text"]) : "";
                    if (txt.Contains(item["val_4"].ToString()) && txt.Contains(item["val_3"].ToString()) && txt.Contains(item["val_2"].ToString()))
                    {
                        SqlCommand cmd2 = new SqlCommand("sp1_brandyy_User_Activities_Insert");
                        cmd2.Parameters.AddWithValue("@reg_uid", reg_uid);
                        cmd2.Parameters.AddWithValue("@brand_id", Convert.ToInt32(item["brand_id"]));
                        cmd2.Parameters.AddWithValue("@campaign_id", Convert.ToInt32(item["campaign_id"]));
                        cmd2.Parameters.AddWithValue("@action_id", Convert.ToInt32(item["action_id"]));
                        cmd2.Parameters.AddWithValue("@created_on", Convert.ToDateTime(ConvertFromUnixTimestamp(Convert.ToDouble(i["created_time"]))));
                        cmd2.Parameters.AddWithValue("@pid", Convert.ToString(i["id"]));
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
                            decimal no_of_likes = Convert.ToDecimal(i["likes"]["count"]) ;
                            points += (no_of_likes * Convert.ToDecimal(item["reward_per_like"]));
                        }
                        decimal reward_amount = (points > Convert.ToDecimal(item["max_brandyy_points"])) ? Convert.ToDecimal(item["max_brandyy_points"]) : points;
                        cmd2.Parameters.AddWithValue("@reward_amount", reward_amount);
                        cmd2.Parameters.AddWithValue("@no_of_friends", no_of_friends);
                        cmd2.Parameters.AddWithValue("@no_of_likes", Convert.ToString(i["likes"]["count"]));
                        cmd2.Parameters.AddWithValue("@no_of_shares", 0);
                        cmd2.Parameters.AddWithValue("@reward_per_user", Convert.ToDecimal(item["reward_user"]));
                        cmd2.Parameters.AddWithValue("@reward_on_friend", Convert.ToDecimal(item["reward_per_friend"]));
                        cmd2.Parameters.AddWithValue("@reward_on_likes", Convert.ToDecimal(item["reward_per_like"]));
                        cmd2.Parameters.AddWithValue("@reward_on_shares", Convert.ToDecimal(item["reward_per_share"]));
                        cmd2.Parameters.AddWithValue("@action_url", Convert.ToString(i["link"]));
                        cmd2.Parameters.AddWithValue("@activity_text", Convert.ToString(i["caption"]["text"]));
                        cmd2.Parameters.AddWithValue("@acitivity_img", Convert.ToString(i["images"]["thumbnail"]) != "" ? Convert.ToString(i["images"]["thumbnail"]["url"]) : "");
                        cmd2.Parameters.AddWithValue("@returnid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                        ConnObj.GetDataTab(cmd2);
                        string id = cmd2.Parameters["@returnid"].Value.ToString();

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
                        cmd3.Parameters.AddWithValue("@created_on", Convert.ToDateTime(ConvertFromUnixTimestamp(Convert.ToDouble(i["created_time"]))));
                        cmd3.Parameters.AddWithValue("@updated_time", Convert.ToDateTime(ConvertFromUnixTimestamp(Convert.ToDouble(i["created_time"]))));
                        cmd3.Parameters.AddWithValue("@sm_img_link", Convert.ToString(i["images"]["thumbnail"]) != "" ? Convert.ToString(i["images"]["thumbnail"]["url"]) : "");
                        cmd3.Parameters.AddWithValue("@sm_desc", Convert.ToString(i["caption"]["text"]));
                        cmd3.Parameters.AddWithValue("@link", Convert.ToString(i["link"]));
                        cmd3.Parameters.AddWithValue("@fb_post_id", Convert.ToString(i["id"]));
                        cmd3.Parameters.AddWithValue("@Action", "Insta");
                        ConnObj.GetDataTab(cmd3);
                        #endregion
                    }
                }
        }

    }

    public void getUserPostLike(string reg_uid, string sm_uid, string username, Int64 no_of_friends, string token) //user recent Post like
    {
        SqlCommand cmd = new SqlCommand("sp_user_get_Insta_BrandCampaignList");// for Campaign type 18
        cmd.Parameters.AddWithValue("@Action", "PostLike");
        ConnObj.GetDataSet(cmd);
        foreach (DataRow item in ConnObj.DataSet.Tables[0].Rows)
            {
                string post_id = Convert.ToString(item["val_2"]);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.instagram.com/v1/media/" + post_id + "/likes?access_token=" + token);
        request.Method = "GET";
        request.ContentType = "application/x-www-form-urlencoded";
        WebResponse response = request.GetResponse();
        string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();
        var tmpResult = JObject.Parse(responseData);
        var resultObject = tmpResult["data"].Values<JObject>().ToArray();
        foreach (var i in resultObject) 
        {
                string strid = Convert.ToString(i["id"]);
                if (strid.Contains(sm_uid))
                {
                    SqlCommand cmd2 = new SqlCommand("sp1_brandyy_User_Activities_Insert");
                    cmd2.Parameters.AddWithValue("@reg_uid", reg_uid);
                    cmd2.Parameters.AddWithValue("@brand_id", Convert.ToInt32(item["brand_id"]));
                    cmd2.Parameters.AddWithValue("@campaign_id", Convert.ToInt32(item["campaign_id"]));
                    cmd2.Parameters.AddWithValue("@action_id", Convert.ToInt32(item["action_id"]));
                    cmd2.Parameters.AddWithValue("@created_on", Convert.ToDateTime(ConvertFromUnixTimestamp(Convert.ToDouble(i["created_time"]))));
                    cmd2.Parameters.AddWithValue("@pid", "");
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
                    cmd2.Parameters.AddWithValue("@reward_amount", reward_amount);
                    cmd2.Parameters.AddWithValue("@no_of_friends", no_of_friends);
                    cmd2.Parameters.AddWithValue("@no_of_likes", 0);
                    cmd2.Parameters.AddWithValue("@no_of_shares", 0);
                    cmd2.Parameters.AddWithValue("@reward_per_user", Convert.ToDecimal(item["reward_user"]));
                    cmd2.Parameters.AddWithValue("@reward_on_friend", Convert.ToDecimal(item["reward_per_friend"]));
                    cmd2.Parameters.AddWithValue("@reward_on_likes", Convert.ToDecimal(item["reward_per_like"]));
                    cmd2.Parameters.AddWithValue("@reward_on_shares", Convert.ToDecimal(item["reward_per_share"]));
                    cmd2.Parameters.AddWithValue("@action_url", Convert.ToString(item["val_4"]));
                    cmd2.Parameters.AddWithValue("@activity_text", Convert.ToString(item["action_post_text"]));
                    cmd2.Parameters.AddWithValue("@acitivity_img", Convert.ToString(item["action_post_image_url"]));
                    cmd2.Parameters.AddWithValue("@returnid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                    ConnObj.ExecuteNonQuery(cmd2);
                }
            }
        }
    }


    public void getUserFollowers(string reg_uid, string sm_uid, string username, Int64 no_of_friends, string token) //user Page Like 
    {
        SqlCommand cmd = new SqlCommand("sp_user_get_Insta_BrandCampaignList");// for Campaign type 17
        cmd.Parameters.AddWithValue("@Action", "PageLike");
        ConnObj.GetDataSet(cmd);
        foreach (DataRow item in ConnObj.DataSet.Tables[0].Rows)
        {
            string page_id = Convert.ToString(item["val_2"]);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.instagram.com/v1/users/" + page_id + "/followed-by?access_token=" + token);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            WebResponse response = request.GetResponse();
            string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var tmpResult = JObject.Parse(responseData);
            var resultObject = tmpResult["data"].Values<JObject>().ToArray();
            foreach (var i in resultObject)
            {

                var strid = Convert.ToString(i["id"]);
                if (strid.Contains(sm_uid))
                {
                    SqlCommand cmd2 = new SqlCommand("sp1_brandyy_User_Activities_Insert");
                    cmd2.Parameters.AddWithValue("@reg_uid", reg_uid);
                    cmd2.Parameters.AddWithValue("@brand_id", Convert.ToInt32(item["brand_id"]));
                    cmd2.Parameters.AddWithValue("@campaign_id", Convert.ToInt32(item["campaign_id"]));
                    cmd2.Parameters.AddWithValue("@action_id", Convert.ToInt32(item["action_id"]));
                    DateTime dt = DateTime.Today;
                    cmd2.Parameters.AddWithValue("@created_on", dt);
                    cmd2.Parameters.AddWithValue("@pid", "");
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
                    cmd2.Parameters.AddWithValue("@reward_amount", reward_amount);
                    cmd2.Parameters.AddWithValue("@no_of_friends", no_of_friends);
                    cmd2.Parameters.AddWithValue("@no_of_likes", 0);
                    cmd2.Parameters.AddWithValue("@no_of_shares", 0);
                    cmd2.Parameters.AddWithValue("@reward_per_user", Convert.ToDecimal(item["reward_user"]));
                    cmd2.Parameters.AddWithValue("@reward_on_friend", Convert.ToDecimal(item["reward_per_friend"]));
                    cmd2.Parameters.AddWithValue("@reward_on_likes", Convert.ToDecimal(item["reward_per_like"]));
                    cmd2.Parameters.AddWithValue("@reward_on_shares", Convert.ToDecimal(item["reward_per_share"]));
                    cmd2.Parameters.AddWithValue("@action_url", Convert.ToString(item["val_4"]));
                    cmd2.Parameters.AddWithValue("@activity_text", Convert.ToString(item["action_post_text"]));
                    cmd2.Parameters.AddWithValue("@acitivity_img", Convert.ToString(item["action_post_image_url"]));
                    cmd2.Parameters.AddWithValue("@returnid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                    ConnObj.ExecuteNonQuery(cmd2);
                }
            }
        }
    }


    private DateTime ConvertFromUnixTimestamp(double timestamp)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return origin.AddSeconds(timestamp);
    }

}