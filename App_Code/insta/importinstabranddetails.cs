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
/// Summary description for importinstabranddetails
/// </summary>
public class importinstabranddetails
{
    public string page_tag;
    ConnectionClass ConnObj = new ConnectionClass();
    CommonVariableCodes _CommonVariableCodes = new CommonVariableCodes();
    string accessToken_1 = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Insta_access_token"]);
	public importinstabranddetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string getPageDetails(string screen_name)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.instagram.com/v1/users/search?q=" +screen_name +"&access_token=" + accessToken_1);
        request.Method = "GET";
        request.ContentType = "application/x-www-form-urlencoded";
        WebResponse response = request.GetResponse();
        string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();
        var tmpResult = JObject.Parse(responseData);
        string id = "";
        var resultObject = tmpResult["data"].Values<JObject>()
             .Select(n => new { username = n["username"], id = n["id"], Context = n.Parent }).ToArray();
        return "";
        //return resultObject;
    }
    public void checkIfUserLikesAPage(string accessToken, List<fbuser> user, string page_id, Int32 brand_id, Int64 campaign_id, Int64 action_id,
                        decimal max_brandyy_points, Int32 no_of_friends,
                        Int32 no_of_likes, Int32 no_of_shares, decimal reward_per_user, decimal reward_on_friend,
                        decimal reward_on_likes, decimal reward_on_shares, string actionUrl, string action_post_text, string action_post_image_url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.instagram.com/v1/users/" + page_id + "/followed-by?access_token=" + accessToken);
        request.Method = "GET";
        request.ContentType = "application/x-www-form-urlencoded";
        WebResponse response = request.GetResponse();
        string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();
        var tmpResult = JObject.Parse(responseData);


        foreach (fbuser t in user)
        {
            var resultObject = tmpResult["data"].Values<JObject>()
             .Where(n => n["id"].Value<string>().Contains(t.sm_uid))
             .Select(n => new { username = n["username"], id = n["id"], Context = n.Parent }).ToArray();

            try
            {

                if (resultObject.Count() > 0)
                {

                    #region save to sp1_brandyy_User_Activities_Insert

                    ConnObj = new ConnectionClass();
                    SqlCommand cmd = new SqlCommand("sp1_brandyy_User_Activities_Insert");
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
                    #endregion

                }

            }
            catch (Exception ex)
            {
            }
        }
    
    }

    public void checkIfUserLikedAPost(string accessToken, List<fbuser> user, string post_id, Int32 brand_id, Int64 campaign_id, Int64 action_id,
                       decimal max_brandyy_points, Int32 no_of_friends,
                       Int32 no_of_likes, Int32 no_of_shares, decimal reward_per_user, decimal reward_on_friend,
                       decimal reward_on_likes, decimal reward_on_shares, string actionUrl, string action_post_text, string action_post_image_url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.instagram.com/v1/media/" + post_id + "/likes?access_token=" + accessToken);
        request.Method = "GET";
        request.ContentType = "application/x-www-form-urlencoded";
        WebResponse response = request.GetResponse();
        string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();
        var tmpResult = JObject.Parse(responseData);

        foreach (fbuser t in user)
        {
            var resultObject = tmpResult["data"].Values<JObject>()
             .Where(n => n["id"].Value<string>().Contains(t.sm_uid))
             .Select(n => new { username = n["username"], id = n["id"], Context = n.Parent }).ToArray();

            try
            {

                if (resultObject.Count() > 0)
                {

                    #region save to sp1_brandyy_User_Activities_Insert

                    ConnObj = new ConnectionClass();
                    SqlCommand cmd = new SqlCommand("sp1_brandyy_User_Activities_Insert");
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
                    #endregion

                }

            }
            catch (Exception ex)
            {
            }
        }


    }

    public void checkIfUserHasAddedAnyPost(string accessToken, List<fbuser> user, string hashtag, string defaulttext, string tag, String last_cron_run, Int32 brand_id, Int64 campaign_id, Int64 action_id,
                        decimal max_brandyy_points, Int32 no_of_friends,
                        Int32 no_of_likes, Int32 no_of_shares, decimal reward_per_user, decimal reward_on_friend,
                        decimal reward_on_likes, decimal reward_on_shares)
    {
        foreach (fbuser t in user)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.instagram.com/v1/users/" + t.sm_uid + "/media/recent/?access_token=" + accessToken);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            WebResponse response = request.GetResponse();
            string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var tmpResult = JObject.Parse(responseData);

            var resultObject = tmpResult["data"].Values<JObject>()
            .Where(n => n["caption"].Count() > 0)
            .Where(n => n["caption"]["text"].Value<string>().Contains(tag))
            .Select(n => new { img = n["images"]["thumbnail"]["url"], link = n["link"], text = n["caption"]["text"], mediaID = n["id"], created_time = n["created_time"], likes = n["likes"]["count"], Context = n.Parent }).ToArray();

            //Loop through the returned users
            foreach (var i in resultObject)
            {

                Int32 brand_id1; Int64 campaign_id1; Int64 action_id1;

                string message = Convert.ToString(i.text).ToLower();                
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
                SqlCommand cmd = new SqlCommand("sp1_brandyy_User_Activities_Insert");

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
                        if (Convert.ToInt32(i.likes) > 0)
                        {
                            no_of_likes = Convert.ToInt32(i.likes);
                        }
                        points += (no_of_likes * reward_on_likes);
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
                    cmd.Parameters.AddWithValue("@action_url", Convert.ToString(i.link));
                    cmd.Parameters.AddWithValue("@activity_text", Convert.ToString(i.text));
                    cmd.Parameters.AddWithValue("@acitivity_img", Convert.ToString(i.img));
                    cmd.Parameters.AddWithValue("@returnid", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                    ConnObj.GetDataTab(cmd);
                    #endregion
                }
                else
                {
                    brand_id1 = _CommonVariableCodes.brandyy_brand_id; campaign_id1 = _CommonVariableCodes.brandyy_campaign_id; action_id1 = _CommonVariableCodes.brandyy_action_id_fb;
                    cmd.Parameters.AddWithValue("@reg_uid", t.reg_uid);
                    cmd.Parameters.AddWithValue("@brand_id", brand_id1);
                    cmd.Parameters.AddWithValue("@campaign_id", campaign_id1);
                    cmd.Parameters.AddWithValue("@action_id", action_id1);
                    cmd.Parameters.AddWithValue("@created_on", Convert.ToDateTime(ConvertFromUnixTimestamp(Convert.ToDouble(i.created_time))));//
                    cmd.Parameters.AddWithValue("@pid", Convert.ToString(i.mediaID));
                    cmd.Parameters.AddWithValue("@reward_amount", 0);
                    cmd.Parameters.AddWithValue("@no_of_friends", 0);
                    cmd.Parameters.AddWithValue("@no_of_likes", 0);
                    cmd.Parameters.AddWithValue("@no_of_shares", 0);
                    cmd.Parameters.AddWithValue("@reward_per_user", 0);
                    cmd.Parameters.AddWithValue("@reward_on_friend", 0);
                    cmd.Parameters.AddWithValue("@reward_on_likes", 0);
                    cmd.Parameters.AddWithValue("@reward_on_shares", 0);
                    cmd.Parameters.AddWithValue("@action_url", Convert.ToString(i.link));
                    cmd.Parameters.AddWithValue("@activity_text", Convert.ToString(i.text));
                    cmd.Parameters.AddWithValue("@acitivity_img", Convert.ToString(i.img));
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
                cmd.Parameters.AddWithValue("@Action", "Insta");
                cmd.Parameters.AddWithValue("@activity_id", activity_id);
                cmd.Parameters.AddWithValue("@reg_uid", t.reg_uid);
                cmd.Parameters.AddWithValue("@brand_id", brand_id1);
                cmd.Parameters.AddWithValue("@campaign_id", campaign_id1);
                cmd.Parameters.AddWithValue("@action_id", action_id1);
                cmd.Parameters.AddWithValue("@created_on", ConvertFromUnixTimestamp(Convert.ToDouble(i.created_time)));
                cmd.Parameters.AddWithValue("@updated_time", ConvertFromUnixTimestamp(Convert.ToDouble(i.created_time)));
                cmd.Parameters.AddWithValue("@sm_img_link", Convert.ToString(i.img));
                cmd.Parameters.AddWithValue("@link", Convert.ToString(i.link));
                cmd.Parameters.AddWithValue("@sm_desc", Convert.ToString(i.text));

                cmd.Parameters.AddWithValue("@fb_post_id", Convert.ToString(i.mediaID));

                ConnObj.GetDataTab(cmd);
                ConnObj.ReleaseConnection();

            }
        }

    }

    private DateTime ConvertFromUnixTimestamp(double timestamp)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return origin.AddSeconds(timestamp);
    }

    public void testcheckIfUserHasAddedAnyPost(string accessToken)
    {
        
        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.instagram.com/v1/users/search?q=ichooseit&access_token=" + accessToken_1);
        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.instagram.com/v1/users/1817688593/media/recent/?access_token=" + accessToken_1);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.instagram.com/developer/endpoints/tags/ichooseit?access_token=" + accessToken);
        request.Method = "GET";
        request.ContentType = "application/x-www-form-urlencoded";
        WebResponse response = request.GetResponse();
        string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();
        var tmpResult = JObject.Parse(responseData);
        // {"pagination":{},"meta":{"code":200},"data":[{"attribution":null,"tags":["benz"],"type":"image","location":null,"comments":{"count":0,"data":[]},"filter":"Normal","created_time":"1429603750","link":"https:\/\/instagram.com\/p\/1us_0UOYYu\/","likes":{"count":1,"data":[{"username":"averagerakhil","profile_picture":"https:\/\/igcdn-photos-c-a.akamaihd.net\/hphotos-ak-xaf1\/t51.2885-19\/11049301_400196463494898_279106775_a.jpg","id":"1336093309","full_name":"Averager Akhil"}]},"images":{"low_resolution":{"url":"https:\/\/scontent.cdninstagram.com\/hphotos-xfa1\/t51.2885-15\/s306x306\/e15\/11116832_1435465376765516_765000073_n.jpg","width":306,"height":306},"thumbnail":{"url":"https:\/\/scontent.cdninstagram.com\/hphotos-xfa1\/t51.2885-15\/s150x150\/e15\/11116832_1435465376765516_765000073_n.jpg","width":150,"height":150},"standard_resolution":{"url":"https:\/\/scontent.cdninstagram.com\/hphotos-xfa1\/t51.2885-15\/e15\/11116832_1435465376765516_765000073_n.jpg","width":640,"height":640}},"users_in_photo":[],"caption":{"created_time":"1429603750","text":"Vishu @ichooseit #benz","from":{"username":"sruthi.ravindran","profile_picture":"https:\/\/igcdn-photos-g-a.akamaihd.net\/hphotos-ak-xaf1\/t51.2885-19\/11055942_414116508750246_2035013149_a.jpg","id":"1818058680","full_name":"sruthi ravindran"},"id":"967908870694995530"},"user_has_liked":false,"id":"967908869478647342_1818058680","user":{"username":"sruthi.ravindran","profile_picture":"https:\/\/igcdn-photos-g-a.akamaihd.net\/hphotos-ak-xaf1\/t51.2885-19\/11055942_414116508750246_2035013149_a.jpg","id":"1818058680","full_name":"sruthi ravindran"}},{"attribution":null,"tags":[],"type":"image","location":null,"comments":{"count":0,"data":[]},"filter":"Normal","created_time":"1428476532","link":"https:\/\/instagram.com\/p\/1NHAAZuYdX\/","likes":{"count":1,"data":[{"username":"ichooseit","profile_picture":"https:\/\/instagramimages-a.akamaihd.net\/profiles\/anonymousUser.jpg","id":"1817688593","full_name":"Ichoose"}]},"images":{"low_resolution":{"url":"https:\/\/scontent.cdninstagram.com\/hphotos-xaf1\/t51.2885-15\/s306x306\/e15\/11143048_372291216309540_1353832968_n.jpg","width":306,"height":306},"thumbnail":{"url":"https:\/\/scontent.cdninstagram.com\/hphotos-xaf1\/t51.2885-15\/s150x150\/e15\/11143048_372291216309540_1353832968_n.jpg","width":150,"height":150},"standard_resolution":{"url":"https:\/\/scontent.cdninstagram.com\/hphotos-xaf1\/t51.2885-15\/e15\/11143048_372291216309540_1353832968_n.jpg","width":640,"height":640}},"users_in_photo":[{"position":{"y":0.5536094,"x":0.77916664},"user":{"username":"ichooseit","profile_picture":"https:\/\/instagramimages-a.akamaihd.net\/profiles\/anonymousUser.jpg","id":"1817688593","full_name":"Ichoose"}}],"caption":null,"user_has_liked":true,"id":"958453082456950615_1818058680","user":{"username":"sruthi.ravindran","profile_picture":"https:\/\/igcdn-photos-g-a.akamaihd.net\/hphotos-ak-xaf1\/t51.2885-19\/11055942_414116508750246_2035013149_a.jpg","id":"1818058680","full_name":"sruthi ravindran"}},{"attribution":null,"tags":[],"type":"image","location":null,"comments":{"count":0,"data":[]},"filter":"Normal","created_time":"1428307689","link":"https:\/\/instagram.com\/p\/1IE9SmOYQn\/","likes":{"count":0,"data":[]},"images":{"low_resolution":{"url":"https:\/\/scontent.cdninstagram.com\/hphotos-xfa1\/t51.2885-15\/s306x306\/e15\/11085038_930848140312407_1690374074_n.jpg","width":306,"height":306},"thumbnail":{"url":"https:\/\/scontent.cdninstagram.com\/hphotos-xfa1\/t51.2885-15\/s150x150\/e15\/11085038_930848140312407_1690374074_n.jpg","width":150,"height":150},"standard_resolution":{"url":"https:\/\/scontent.cdninstagram.com\/hphotos-xfa1\/t51.2885-15\/e15\/11085038_930848140312407_1690374074_n.jpg","width":640,"height":640}},"users_in_photo":[],"caption":{"created_time":"1428307689","text":"Ayush first day at school, doa academy dubai","from":{"username":"sruthi.ravindran","profile_picture":"https:\/\/igcdn-photos-g-a.akamaihd.net\/hphotos-ak-xaf1\/t51.2885-19\/11055942_414116508750246_2035013149_a.jpg","id":"1818058680","full_name":"sruthi ravindran"},"id":"957036725068728128"},"user_has_liked":false,"id":"957036724859012135_1818058680","user":{"username":"sruthi.ravindran","profile_picture":"https:\/\/igcdn-photos-g-a.akamaihd.net\/hphotos-ak-xaf1\/t51.2885-19\/11055942_414116508750246_2035013149_a.jpg","id":"1818058680","full_name":"sruthi ravindran"}}]}* /

    }
}