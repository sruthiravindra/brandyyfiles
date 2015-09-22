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
/// Summary description for importtwprofiledetails
/// </summary>
public class importtwprofiledetails
{
    oAuthTwitter oAuth = new oAuthTwitter();
    ConnectionClass ConnObj = new ConnectionClass();
	public importtwprofiledetails()
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
        cmd.Parameters.AddWithValue("@sm_id", 2);
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

    public void checkIfUserSharedAPost(string accessToken, List<fbuser> user, string post_id, Int32 brand_id, Int64 campaign_id, Int64 action_id,
                        decimal reward_amount, decimal reward_amount_max, decimal reward_percent, decimal reward_percent_max, string reward_product, decimal reward_point, Int32 no_of_friends,
                        Int32 no_of_likes, Int32 no_of_shares, decimal reward_per_user, decimal reward_on_friend,
                        decimal reward_on_likes, decimal reward_on_shares)
    {

        string str = "";

        string url = "https://api.twitter.com/1.1/statuses/retweets/" + post_id + ".json?q=" + str;

        string posts = Search(url, str);

        fbuser t = user[0];

        try
        {

            JArray jsonDat = JArray.Parse(posts);
            for (int x = 0; x < jsonDat.Count; x++)
            {                
                // check if the post link contains the post id
                if (t.sm_uid == Convert.ToString(jsonDat[x]["user"]["id"]))
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

        }
        catch (Exception ex)
        {
        }

    }

    public void checkIfUserLikedAPost(string accessToken, List<fbuser> user, string post_id, Int32 brand_id, Int64 campaign_id, Int64 action_id,
                        decimal reward_amount, decimal reward_amount_max, decimal reward_percent, decimal reward_percent_max, string reward_product, decimal reward_point, Int32 no_of_friends,
                        Int32 no_of_likes, Int32 no_of_shares, decimal reward_per_user, decimal reward_on_friend,
                        decimal reward_on_likes, decimal reward_on_shares)
    {
        foreach (fbuser t in user)
        {

            string str = "&user_id=" + Convert.ToString(t.sm_uid);

            string url = "https://api.twitter.com/1.1/favorites/list.json?q=" + str;

            string posts = Search(url, str);

            try
            {

                JArray jsonDat = JArray.Parse(posts);
                for (int x = 0; x < jsonDat.Count; x++)
                {

                    //fbuser t = user.FirstOrDefault(item => item.sm_uid == Convert.ToString(jsonDat[x]["user"]["id"]));
                    // check if the post link contains the post id
                    //if (t != null)
                    if (Convert.ToString(jsonDat[x]["id"]) == post_id)
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
        hashtag = hashtag.Replace("@" + System.Configuration.ConfigurationManager.AppSettings["TwitterIchooseItHashtag"], "");
        string[] hashtag_arr = hashtag.Split(' ');
        hashtag = "";
        foreach (string hash in hashtag_arr)
        {
            string hash1 = hash.Replace('#', ' ').Trim();
            if (hash1 != "")
            {
                hashtag += "%23" + hash1;
            }
        }

        //
        //string str = "@ichooseit256%20from:" + SessionState._SignInUser.reg_email + "&result_type=mixed";
        string str = "@ichooseit256" + hashtag + "&result_type=mixed";

        string url = "https://api.twitter.com/1.1/search/tweets.json?q=" + str;

        string posts = Search(url, str);



        fbuser t = user[0];
        try
        {
            var jsonDat = (JObject.Parse(posts));
            foreach (var key in jsonDat)
            {
                if (key.Key.Equals("statuses"))
                {

                    for (int x = 0; x < key.Value.Count(); x++)
                    {
                        // case : default text defined for campaign 
                        if (defaulttext != "")
                        {
                            // check if tweet contains the default text
                            if (!Convert.ToString(key.Value[x]["text"]).Contains(defaulttext))
                            {
                                continue;
                            }
                        }


                        // check if the post link contains the post id
                        if (t.sm_uid == Convert.ToString(key.Value[x]["user"]["id"]))
                        {
                            #region save to sp1_IChooseIT_User_Activities_Insert

                            ConnObj = new ConnectionClass();
                            SqlCommand cmd = new SqlCommand("sp1_IChooseIT_User_Activities_Insert");
                            cmd.Parameters.AddWithValue("@reg_uid", t.reg_uid);
                            cmd.Parameters.AddWithValue("@brand_id", brand_id);
                            cmd.Parameters.AddWithValue("@campaign_id", campaign_id);
                            cmd.Parameters.AddWithValue("@action_id", action_id);
                            string temp = Convert.ToString(key.Value[x]["created_at"]);
                            cmd.Parameters.AddWithValue("@created_on", ParseDateTime(Convert.ToString(key.Value[x]["created_at"])));
                            cmd.Parameters.AddWithValue("@pid", Convert.ToString(key.Value[x]["id"]));



                            decimal points = 0;
                            no_of_friends = 0;
                            no_of_likes = 0;
                            no_of_shares = 0;

                            // reward table details
                            if (reward_per_user > 0)
                            {
                                points = reward_per_user;
                            }
                            if (reward_on_friend > 0)
                            {
                                // get number of friends for the user
                                no_of_friends = t.no_of_friends;
                                points += (no_of_friends * reward_on_friend);
                            }
                            if (reward_on_likes > 0)
                            {
                                if (Convert.ToInt32(key.Value[x]["favorite_count"]) != 0)
                                {
                                    no_of_likes = Convert.ToInt32(key.Value[x]["favorite_count"]);
                                }
                                points += (no_of_likes * reward_on_likes);
                            }
                            if (reward_on_shares > 0)
                            {
                                if (Convert.ToInt32(key.Value[x]["retweeted"]) != 0)
                                {
                                    no_of_shares = Convert.ToInt32(key.Value[x]["retweeted"]);
                                }
                                points += (no_of_shares * reward_on_shares);
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

                            string id = cmd.Parameters["@returnid"].Value.ToString();

                            if (id == "")
                            {
                                continue;
                            }

                            Int64 activity_id = Convert.ToInt64(id);
                            #endregion

                            #region save to IChooseIT_User_Activities_Tw_Post

                            // insert twitter post
                            cmd = new SqlCommand("sp1_IChooseIT_User_Tw_Activities_Insert");
                            cmd.Parameters.AddWithValue("@activity_id", activity_id);
                            cmd.Parameters.AddWithValue("@reg_uid", t.reg_uid);
                            cmd.Parameters.AddWithValue("@brand_id", brand_id);
                            cmd.Parameters.AddWithValue("@campaign_id", campaign_id);
                            cmd.Parameters.AddWithValue("@action_id", action_id);
                            cmd.Parameters.AddWithValue("@created_on", ParseDateTime(Convert.ToString(key.Value[x]["created_at"])));
                            cmd.Parameters.AddWithValue("@updated_time", ParseDateTime(Convert.ToString(key.Value[x]["created_at"])));

                            if (Convert.ToString(key.Value[x]["entities"]["media"]) != "")
                            {
                                cmd.Parameters.AddWithValue("@sm_img_link", Convert.ToString(key.Value[x]["entities"]["media"][0]["media_url"]));
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@sm_img_link", "");
                            }
                            cmd.Parameters.AddWithValue("@sm_desc", Convert.ToString(key.Value[x]["text"]));
                            string link = "https://twitter.com/" + Convert.ToString(key.Value[x]["user"]["screen_name"]) + "/status/" + Convert.ToString(key.Value[x]["id"]);
                            cmd.Parameters.AddWithValue("@link", link);
                            cmd.Parameters.AddWithValue("@fb_post_id", Convert.ToString(key.Value[x]["id"]));

                            ConnObj.GetDataTab(cmd);
                            ConnObj.ReleaseConnection();

                            #endregion
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
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