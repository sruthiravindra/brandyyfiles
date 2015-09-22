using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using oAuthExample;
using System.Xml;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// this class is called when user has not logged in and the scheduler is syncing data
/// </summary>
public class importtwitteruserdetails
{
    oAuthTwitter oAuth = new oAuthTwitter();
    ConnectionClass ConnObj = new ConnectionClass();
    public importtwitteruserdetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void getUserPosts(string reg_uid, string sm_uid, string username) // User recent posts
    {
        Int64 no_of_friends = 0;
        SqlCommand cmd = new SqlCommand("sp_user_get_Twitter_BrandCampaignList");// for Campaign type 9 ,10
        cmd.Parameters.AddWithValue("@Action", "Post");
        ConnObj.GetDataSet(cmd);

        string str = "&screen_name=" + username;
        string url = "https://api.twitter.com/1.1/statuses/user_timeline.json?q=" + str;

        string posts = Search(url, str);
        JArray jsonDat = JArray.Parse(posts);
        for (int x = 0; x < 1; x++) // User Profile details
        {
            SqlCommand cmd1 = new SqlCommand("sp_user_update_UserProfileDetails");
            cmd1.Parameters.AddWithValue("@reg_uid", Convert.ToInt64(reg_uid));
            cmd1.Parameters.AddWithValue("@sm_id", 2);
            cmd1.Parameters.AddWithValue("@name", Convert.ToString(jsonDat[x]["user"]["name"]));
            cmd1.Parameters.AddWithValue("@email", Convert.ToString(jsonDat[x]["user"]["screen_name"]));
            cmd1.Parameters.AddWithValue("@profile_img_link", Convert.ToString(jsonDat[x]["user"]["profile_image_url"]));
            cmd1.Parameters.AddWithValue("@no_of_friends", Convert.ToString(jsonDat[x]["user"]["followers_count"]));
            ConnObj.ExecuteNonQuery(cmd1);
            //Assign no of Friends 
            no_of_friends = Convert.ToInt64(jsonDat[x]["user"]["followers_count"]);
        }

        for (int x = 0; x < jsonDat.Count; x++) // User Recent Posts
        {
            if (Convert.ToString(jsonDat[x]["retweeted_status"]) == "")
            {
            foreach (DataRow item in ConnObj.DataSet.Tables[0].Rows)
            {
                string txt = Convert.ToString(jsonDat[x]["text"]);
                if (txt.Contains(item["val_4"].ToString()) && txt.Contains(item["val_3"].ToString()) && txt.Contains(item["val_2"].ToString()))
                    {
                        SqlCommand cmd2 = new SqlCommand("sp1_brandyy_User_Activities_Insert");
                        cmd2.Parameters.AddWithValue("@reg_uid", reg_uid);
                        cmd2.Parameters.AddWithValue("@brand_id", Convert.ToInt32(item["brand_id"]));
                        cmd2.Parameters.AddWithValue("@campaign_id", Convert.ToInt32(item["campaign_id"]));
                        cmd2.Parameters.AddWithValue("@action_id", Convert.ToInt32(item["action_id"]));
                        cmd2.Parameters.AddWithValue("@created_on", ParseDateTime(Convert.ToString(jsonDat[x]["created_at"])));
                        cmd2.Parameters.AddWithValue("@pid", Convert.ToString(jsonDat[x]["id"]));
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
                            decimal no_of_likes = Convert.ToDecimal(jsonDat[x]["favorite_count"]);
                            points += (no_of_likes * Convert.ToDecimal(item["reward_per_like"]));
                        }
                        if (Convert.ToDecimal(item["reward_per_share"]) > 0)
                        {
                            decimal no_of_shares = Convert.ToDecimal(jsonDat[x]["retweet_count"]);
                            points += (no_of_shares * Convert.ToDecimal(item["reward_per_share"]));
                        }
                        decimal reward_amount = (points > Convert.ToDecimal(item["max_brandyy_points"])) ? Convert.ToDecimal(item["max_brandyy_points"]) : points;
                        cmd2.Parameters.AddWithValue("@reward_amount", reward_amount);
                        cmd2.Parameters.AddWithValue("@no_of_friends", no_of_friends);
                        cmd2.Parameters.AddWithValue("@no_of_likes", Convert.ToString(jsonDat[x]["favorite_count"]));
                        cmd2.Parameters.AddWithValue("@no_of_shares", Convert.ToString(jsonDat[x]["retweet_count"]));
                        cmd2.Parameters.AddWithValue("@reward_per_user", Convert.ToDecimal(item["reward_user"]));
                        cmd2.Parameters.AddWithValue("@reward_on_friend", Convert.ToDecimal(item["reward_per_friend"]));
                        cmd2.Parameters.AddWithValue("@reward_on_likes", Convert.ToDecimal(item["reward_per_like"]));
                        cmd2.Parameters.AddWithValue("@reward_on_shares", Convert.ToDecimal(item["reward_per_share"]));
                        string link1 = "https://twitter.com/" + Convert.ToString(jsonDat[x]["user"]["screen_name"]) + "/status/" + Convert.ToString(jsonDat[x]["id"]);
                        cmd2.Parameters.AddWithValue("@action_url",Convert.ToString(link1));
                        cmd2.Parameters.AddWithValue("@activity_text", Convert.ToString(jsonDat[x]["text"]));
                        cmd2.Parameters.AddWithValue("@acitivity_img", Convert.ToString(jsonDat[x]["entities"]["media"]) != "" ? Convert.ToString(jsonDat[x]["entities"]["media"][0]["media_url"]) : "");
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
                        cmd3.Parameters.AddWithValue("@created_on", ParseDateTime(Convert.ToString(jsonDat[x]["created_at"])));
                        cmd3.Parameters.AddWithValue("@updated_time", ParseDateTime(Convert.ToString(jsonDat[x]["created_at"])));
                        cmd3.Parameters.AddWithValue("@sm_img_link", Convert.ToString(jsonDat[x]["entities"]["media"]) != "" ? Convert.ToString(jsonDat[x]["entities"]["media"][0]["media_url"]) : "");
                        cmd3.Parameters.AddWithValue("@sm_desc", Convert.ToString(jsonDat[x]["text"]));
                        string link = "https://twitter.com/" + Convert.ToString(jsonDat[x]["user"]["screen_name"]) + "/status/" + Convert.ToString(jsonDat[x]["id"]);
                        cmd3.Parameters.AddWithValue("@link", link);
                        cmd3.Parameters.AddWithValue("@fb_post_id", Convert.ToString(jsonDat[x]["id"]));
                        cmd3.Parameters.AddWithValue("@Action", "Twitter");
                        ConnObj.GetDataTab(cmd3);
                        #endregion
                    }
                }

            }

        }

        getUserFavorities(reg_uid, sm_uid, username, no_of_friends);
        getUserFollowers(reg_uid, sm_uid, username, no_of_friends);
        getUserRetweet(reg_uid, sm_uid, username, no_of_friends);
    }


    public void getUserFavorities(string reg_uid, string sm_uid, string username, Int64 no_of_friends) //user recent favorities (Post like)
    {
        SqlCommand cmd = new SqlCommand("sp_user_get_Twitter_BrandCampaignList");// for Campaign type 4
        cmd.Parameters.AddWithValue("@Action", "PostLike");
        ConnObj.GetDataSet(cmd);
        string str = "&screen_name=" + username;
        string url = "https://api.twitter.com/1.1/favorites/list.json?q=" + str;

        string posts = Search(url, str);
        JArray jsonDat = JArray.Parse(posts);
        for (int x = 0; x < jsonDat.Count; x++) 
        {
                foreach (DataRow item in ConnObj.DataSet.Tables[0].Rows)
                {
                    string strid = Convert.ToString(jsonDat[x]["id"]);
                    if (strid.Contains(item["val_2"].ToString()))
                    {
                        SqlCommand cmd2 = new SqlCommand("sp1_brandyy_User_Activities_Insert");
                        cmd2.Parameters.AddWithValue("@reg_uid", reg_uid);
                        cmd2.Parameters.AddWithValue("@brand_id", Convert.ToInt32(item["brand_id"]));
                        cmd2.Parameters.AddWithValue("@campaign_id", Convert.ToInt32(item["campaign_id"]));
                        cmd2.Parameters.AddWithValue("@action_id", Convert.ToInt32(item["action_id"]));
                        cmd2.Parameters.AddWithValue("@created_on", ParseDateTime(Convert.ToString(jsonDat[x]["created_at"])));
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
                        cmd2.Parameters.AddWithValue("@no_of_likes",0);
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


    public void getUserFollowers(string reg_uid, string sm_uid, string username, Int64 no_of_friends) //user Page Like (Following)
    {
        SqlCommand cmd = new SqlCommand("sp_user_get_Twitter_BrandCampaignList");// for Campaign type 2
        cmd.Parameters.AddWithValue("@Action", "PageLike");
        ConnObj.GetDataSet(cmd);

    //https://api.twitter.com/1.1/friends/ids.json
    //https://api.twitter.com/1.1/followers/ids.json
        string str = "&screen_name=" + username;
        string url = "https://api.twitter.com/1.1/followers/ids.json?q=" + str;

        string posts = Search(url, str);
        var jsonDat = (JObject.Parse(posts));
            foreach (DataRow item in ConnObj.DataSet.Tables[0].Rows)
            {

                var strid = Convert.ToString(jsonDat["ids"]);
                if (strid.Contains(item["val_2"].ToString()))
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


    public void getUserRetweet(string reg_uid, string sm_uid, string username, Int64 no_of_friends) //user recent Shares (Retweets)
    {
        SqlCommand cmd = new SqlCommand("sp_user_get_Twitter_BrandCampaignList");// for Campaign type 3
        cmd.Parameters.AddWithValue("@Action", "PostShare");
        ConnObj.GetDataSet(cmd);
        foreach (DataRow item in ConnObj.DataSet.Tables[0].Rows)
        {
            string post_id = item["val_2"].ToString();
        string str = "";
        string url = "https://api.twitter.com/1.1/statuses/retweets/" + post_id + ".json?q=" + str;

        string posts = Search(url, str);
        JArray jsonDat = JArray.Parse(posts);

        for (int x = 0; x < jsonDat.Count; x++)
        {
                string strid = Convert.ToString(jsonDat[x]["user"]["id"]);
                if (strid == sm_uid)
                {
                    SqlCommand cmd2 = new SqlCommand("sp1_brandyy_User_Activities_Insert");
                    cmd2.Parameters.AddWithValue("@reg_uid", reg_uid);
                    cmd2.Parameters.AddWithValue("@brand_id", Convert.ToInt32(item["brand_id"]));
                    cmd2.Parameters.AddWithValue("@campaign_id", Convert.ToInt32(item["campaign_id"]));
                    cmd2.Parameters.AddWithValue("@action_id", Convert.ToInt32(item["action_id"]));
                    cmd2.Parameters.AddWithValue("@created_on", ParseDateTime(Convert.ToString(jsonDat[x]["created_at"])));
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

    public string Search(string url, string str)
    {
        string oauthconsumerkey = System.Configuration.ConfigurationManager.AppSettings["consumerKey"];
        string oauthtoken = System.Configuration.ConfigurationManager.AppSettings["oauth_token"];
        string oauthconsumersecret = System.Configuration.ConfigurationManager.AppSettings["consumerSecret"];
        string oauthtokensecret = System.Configuration.ConfigurationManager.AppSettings["oauth_token_secret"];

        string oauthsignaturemethod = "HMAC-SHA1";
        string oauthversion = "1.0";
        string oauthnonce = Convert.ToBase64String(
          new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
        TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        string oauthtimestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();
        SortedDictionary<string, string> basestringParameters = new SortedDictionary<string, string>();
        basestringParameters.Add("q", str);
        //basestringParameters.Add("screen_name", str);
        basestringParameters.Add("oauth_version", oauthversion);
        basestringParameters.Add("oauth_consumer_key", oauthconsumerkey);
        basestringParameters.Add("oauth_nonce", oauthnonce);
        basestringParameters.Add("oauth_signature_method", oauthsignaturemethod);
        basestringParameters.Add("oauth_timestamp", oauthtimestamp);
        basestringParameters.Add("oauth_token", oauthtoken);
        //Build the signature string
        string baseString = String.Empty;
        baseString += "GET" + "&";
        baseString += Uri.EscapeDataString(url.Split('?')[0]) + "&";
        foreach (KeyValuePair<string, string> entry in basestringParameters)
        {
            baseString += Uri.EscapeDataString(entry.Key + "=" + entry.Value + "&");
        }

        //Remove the trailing ambersand char last 3 chars - %26
        baseString = baseString.Substring(0, baseString.Length - 3);

        //Build the signing key
        string signingKey = Uri.EscapeDataString(oauthconsumersecret) +
          "&" + Uri.EscapeDataString(oauthtokensecret);

        //Sign the request
        HMACSHA1 hasher = new HMACSHA1(new ASCIIEncoding().GetBytes(signingKey));
        string oauthsignature = Convert.ToBase64String(
          hasher.ComputeHash(new ASCIIEncoding().GetBytes(baseString)));

        //Tell Twitter we don't do the 100 continue thing
        ServicePointManager.Expect100Continue = false;

        //authorization header
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@url);
        string authorizationHeaderParams = String.Empty;
        authorizationHeaderParams += "OAuth ";
        authorizationHeaderParams += "oauth_nonce=" + "\"" +
          Uri.EscapeDataString(oauthnonce) + "\",";
        authorizationHeaderParams += "oauth_signature_method=" + "\"" +
          Uri.EscapeDataString(oauthsignaturemethod) + "\",";
        authorizationHeaderParams += "oauth_timestamp=" + "\"" +
          Uri.EscapeDataString(oauthtimestamp) + "\",";
        authorizationHeaderParams += "oauth_consumer_key=" + "\"" +
          Uri.EscapeDataString(oauthconsumerkey) + "\",";
        authorizationHeaderParams += "oauth_token=" + "\"" +
          Uri.EscapeDataString(oauthtoken) + "\",";
        authorizationHeaderParams += "oauth_signature=" + "\"" +
          Uri.EscapeDataString(oauthsignature) + "\",";
        authorizationHeaderParams += "oauth_version=" + "\"" +
          Uri.EscapeDataString(oauthversion) + "\"";
        webRequest.Headers.Add("Authorization", authorizationHeaderParams);

        webRequest.Method = "GET";
        webRequest.ContentType = "application/x-www-form-urlencoded";

        //Allow us a reasonable timeout in case Twitter's busy
        webRequest.Timeout = 3 * 60 * 1000;
        try
        {
            //Proxy settings
            webRequest.Proxy = new WebProxy();
            HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;
            Stream dataStream = webResponse.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            return responseFromServer;
            //CheckAndInsert(responseFromServer);
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public DateTime ParseDateTime(string date)
    {
        string dayOfWeek = date.Substring(0, 3).Trim();
        string month = date.Substring(4, 3).Trim();
        string dayInMonth = date.Substring(8, 2).Trim();
        string time = date.Substring(11, 9).Trim();
        string offset = date.Substring(20, 5).Trim();
        string year = date.Substring(25, 5).Trim();
        string dateTime = string.Format("{0}-{1}-{2} {3}", dayInMonth, month, year, time);
        DateTime ret = DateTime.Parse(dateTime);
        return ret;
    }

   
}