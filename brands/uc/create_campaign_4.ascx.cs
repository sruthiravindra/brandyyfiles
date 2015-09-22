using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using IchooseIT.DAL;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
public partial class brands_uc_create_campaign_4 : System.Web.UI.UserControl
{
    static Dictionary<byte, Int64> alreadyexistingactions = new Dictionary<byte, Int64>();
    ConnectionClass ConnObj = null;
    SqlCommand cmd_across = null;
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

        if ((!Page.IsPostBack) && (SessionState._BrandAdmin != null))
        {
            FirstPos();
        }
        else if (SessionState._BrandAdmin != null)
        {
            ;
        }
        else
        {
            Response.Redirect(SessionState.WebsiteURLBrand);
        }

    }
    #endregion

    private void FirstPos()
    {
        alreadyexistingactions = new Dictionary<byte, Int64>();
        LoadCampaignTypeForm();

        if (SessionState.EditId > 0)
        {
            FillActionControls();
        }
    }
    private void FillActionControls()
    {
        SetAlreadyExists(2, chk_Action_1);
        SetAlreadyExists(3, chk_Action_2);
        SetAlreadyExists(4, chk_Action_3);
        if (SessionState._Campaign.actions[3] != null)
        {
            SqlCommand cmd = new SqlCommand("sp_select_brands_pages");
            cmd.Parameters.AddWithValue("@page_id", Convert.ToInt16(SessionState._Campaign.actions[3].val1));
            ConnObj.GetDataSet(cmd);
            drpPages5.SelectedValue = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["page_id"]);
            LoadTwitterPosts(Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["page_name"]));


            drpPages5.SelectedValue = SessionState._Campaign.actions[3].val1;
            txtSelectedPost.Text = SessionState._Campaign.actions[3].val3;

            SetActionSession(2, SessionState._Campaign.actions[3].val1, Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["sm_page_id"]), "", "", "");
            SetActionSession(4, SessionState._Campaign.actions[3].val1, SessionState._Campaign.actions[3].val2, SessionState._Campaign.actions[3].val3, SessionState._Campaign.actions[3].val4, "");
            return;
        }
        if (SessionState._Campaign.actions[4] != null)
        {
            SqlCommand cmd = new SqlCommand("sp_select_brands_pages");
            cmd.Parameters.AddWithValue("@page_id", Convert.ToInt16(SessionState._Campaign.actions[4].val1));
            ConnObj.GetDataSet(cmd);
            drpPages5.SelectedValue = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["page_id"]);
            LoadTwitterPosts(Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["page_name"]));

            drpPages5.SelectedValue = SessionState._Campaign.actions[4].val1;
            txtSelectedPost.Text = SessionState._Campaign.actions[4].val3;

            SetActionSession(2, SessionState._Campaign.actions[4].val1, Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["sm_page_id"]), "", "", "");
            SetActionSession(3, SessionState._Campaign.actions[4].val1, SessionState._Campaign.actions[4].val2, SessionState._Campaign.actions[4].val3, SessionState._Campaign.actions[4].val4, "");
        }
    }
    private void SetAlreadyExists(byte campaign_type, CheckBox chk)
    {
        if (SessionState._Campaign.actions[campaign_type] != null)
        {
            if (SessionState._Campaign.actions[campaign_type].action_id > 0)
            {
                alreadyexistingactions.Add(campaign_type, SessionState._Campaign.actions[campaign_type].action_id);
            }
        }
        else
        {
            chk.Checked = false;
        }
    }
    private void LoadCampaignTypeForm()
    {
        Twitter_Post_Retweet_Follow(SessionState._Campaign.campaign_objective);
    }
    private void HandleTextOnFocus(string ctrlName, string args)
    {
        //Since this will get called for every postback, we only
        // want to handle a specific combination of control
        // and argument.
        if (ctrlName == txtSelectedPost.UniqueID && args == "OnFocus")
        {
            divPagePostsList.Visible = true;
        }
    }

    #region twitter post follow/retweet

    private void Twitter_Post_Retweet_Follow(byte campaign_type)
    {       

        // get pages
        SqlCommand cmd = new SqlCommand("sp_Get_Brands_Pages");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@sm_id", 2);
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            drpPages5.DataSource = ConnObj.DataSet.Tables[0];
            drpPages5.DataTextField = "page_name";
            drpPages5.DataValueField = "page_id";
            drpPages5.DataBind();
            drpPages5.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }
        else
        {
            drpPages5.Visible = false;
            lblNoPages5.Visible = true;
        }
    }
    private void Panel5_Clear()
    {
        rep_PagePost5.DataSource = null;
        rep_PagePost5.DataBind();
        //drpPages5.Items.Clear();        
    }

    protected void drpPages5_SelectedIndexChanged(object sender, EventArgs e)
    {

        txtSelectedPost.Visible = false;
        if (drpPages5.SelectedValue == "0")
        {
            Panel5_Clear();
            return;
        }

        SetActionPageLikeSession();

        divPagePostsList.Visible = true;

        String page_url = SessionState._Campaign.actions[2].displayval1;
        String page_url1 = SessionState._Campaign.actions[2].val3;

        try
        {
            LoadTwitterPosts(page_url1);
            hiddenPageURL5.Value = page_url;
        }
        catch (Exception ex)
        {
            Panel5_Clear();
            lblPagePostsMsg5.Text = " Not a valid Page URL ";
            lblPagePostsMsg5.Visible = true;
        }
    }
    private void SetActionPageLikeSession()
    {
        SqlCommand cmd = new SqlCommand("sp_select_brands_pages");
        cmd.Parameters.AddWithValue("@page_id", Convert.ToInt16(drpPages5.SelectedValue));
        ConnObj.GetDataSet(cmd);

        SetActionSession(2, Convert.ToString(drpPages5.SelectedValue), Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["sm_page_id"]), Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["page_name"]), "", Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["page_url"]));
    }
    public void LoadTwitterPosts(string page_name)
    {
      
        string str = "%20from:" + page_name + "&result_type=mixed";

        string url = "https://api.twitter.com/1.1/search/tweets.json?q=" + str;

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
            CreatePostslist(responseFromServer, page_name);
        }
        catch (Exception ex)
        {
            Panel5_Clear();
            lblPagePostsMsg5.Text = " Not a valid Page URL ";
            lblPagePostsMsg5.Visible = true;
        }
    }
    private void CreatePostslist(string xml, string page_name)
    {
        try
        {
            int cnt = 0;
            mypost p;
            List<mypost> myposts = new List<mypost>();
            Dictionary<string, string> posts_dict = new Dictionary<string, string>();
            var jsonDat = (JObject.Parse(xml));
            foreach (var key in jsonDat)
            {
                if (key.Key.Equals("statuses"))
                {
                    for (int x = 0; x < key.Value.Count(); x++)
                    {
                        if (!posts_dict.ContainsKey(Convert.ToString(key.Value[x]["id"])))
                        {
                            posts_dict.Add(Convert.ToString(key.Value[x]["id"]), Convert.ToString(key.Value[x]["text"]));

                            p = new mypost();
                            p.post = Convert.ToString(key.Value[x]["text"]);
                            p.post_id = Convert.ToString(key.Value[x]["id"]);
                            p.created_on = "";
                            p.post_url = "https://twitter.com/" + page_name + "/status/" + p.post_id;
                            if (Convert.ToString(key.Value[x]["entities"]["media"]) != "")
                            {
                                p.img_url = Convert.ToString(key.Value[x]["entities"]["media"][0]["media_url"]);
                            }
                            else { p.img_url = ""; }
                            myposts.Add(p);
                            cnt++;
                        }
                        if (cnt > 10) break;
                    }
                }

            }

            if (cnt == 0)
            {
                p = new mypost();
                p.post = "No recent posts found";
                p.post_id = "0";
                p.created_on = "";
                p.img_url = ""; 
                myposts.Add(p);
            }
            rep_PagePost5.DataSource = myposts;
            rep_PagePost5.DataBind();
        }
        catch (Exception ex)
        {
            Panel5_Clear();
            lblPagePostsMsg5.Text = " Error Parsing the result ";
            lblPagePostsMsg5.Visible = true;
        }
    }
    private void SetActionSession(byte campaign_type, string val1, string post_id, string post_text, string post_url, string displayval1)
    {
        campaign_action ca = new campaign_action();

        ca = new campaign_action();
        ca.campaign_type = campaign_type;
        if (SessionState._Campaign.actions[ca.campaign_type] != null)
        {
            ca.action_id = SessionState._Campaign.actions[ca.campaign_type].action_id;
        } ca.val1 = val1;
        ca.val2 = post_id;
        ca.val3 = post_text;
        ca.val4 = post_url;
        ca.displayval1 = displayval1;
        SessionState._Campaign.actions[ca.campaign_type] = ca;
    }
    
    protected void rep_PagePosts5_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "SetPostSession")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            var start = Convert.ToString(commandArgs[0]).IndexOf("_") + 1;
            var end = Convert.ToString(commandArgs[0]).Length;
            string post_id = Convert.ToString(commandArgs[0]);
            string post_url = Convert.ToString(commandArgs[1]);

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlContainerControl dynamic1 = (HtmlContainerControl)e.Item.FindControl("mypostsdiv");
                string post_text = dynamic1.InnerHtml;

                txtSelectedPost.Text = post_text;
                txtSelectedPost.Visible = true;
                divPagePostsList.Visible = false;

                SetActionSession(3, Convert.ToString(drpPages5.SelectedValue), post_id, post_text, post_url, "");
                SetActionSession(4, Convert.ToString(drpPages5.SelectedValue), post_id, post_text, post_url, "");
            }
        }

    }
    #endregion

    #region insert update delete
    private void CreateOrUpdateActions(byte campaign_type)
    {
        bool checked_status = false;
        switch (campaign_type)
        {
            case 2: checked_status = chk_Action_1.Checked; break;
            case 3: checked_status = chk_Action_2.Checked; break;
            case 4: checked_status = chk_Action_3.Checked; break;
        }

        if (checked_status == true)
        {
            cmd_across = new SqlCommand("sp_insert_brands_campaigns_action");
            SetActionSpecificData(campaign_type);

            // check if this action already exists for the campaign
            if (alreadyexistingactions.ContainsKey(campaign_type) != true)
            {
                SessionState.EditId_2 = 0;
                CreateAction(campaign_type);
            }
            else
            {
                SessionState.EditId_2 = alreadyexistingactions[campaign_type];
                UpdateAction();
                SessionState.EditId_2 = 0;
            }

        }
        else
        {
            // check if this action already exists for the campaign
            if (alreadyexistingactions.ContainsKey(campaign_type) == true)
            {
                SessionState.EditId_2 = alreadyexistingactions[campaign_type];
                DeleteAction();
                SessionState.EditId_2 = 0;
                SessionState._Campaign.actions[campaign_type].action_id = 0;
            }
        }

    }
    private void CreateAction(byte campaign_type)
    {
        cmd_across.Parameters.AddWithValue("@action_id", 0);
        cmd_across.Parameters.AddWithValue("@campaign_id", SessionState._Campaign.campaign_id);
        cmd_across.Parameters.AddWithValue("@brand_id", SessionState._Campaign.brand_id);
        cmd_across.Parameters.AddWithValue("@name", SessionState._Campaign.campaign_name);
        cmd_across.Parameters.AddWithValue("@action_status", SessionState._Campaign.campaign_status);
        ConnObj.GetDataTab(cmd_across);

        if (ConnObj.IsSuccess)
        {
            SessionState._Campaign.actions[campaign_type].action_id = Convert.ToInt64(ConnObj.DataTab.Rows[0]["action_id"]);
        }
        else
        {
            lblErrorMsg.Text = ConnObj.Message;
            lblErrorMsg.ForeColor = System.Drawing.Color.Red;
            lblErrorMsg.Visible = true;
        }
    }
    private void UpdateAction()
    {
        cmd_across.Parameters.AddWithValue("@brand_id", SessionState._Campaign.brand_id);
        cmd_across.Parameters.AddWithValue("@action_id", SessionState.EditId_2);
        cmd_across.Parameters.AddWithValue("@campaign_id", SessionState._Campaign.campaign_id);
        cmd_across.Parameters.AddWithValue("@name", SessionState._Campaign.campaign_name);
        cmd_across.Parameters.AddWithValue("@action_status", SessionState._Campaign.campaign_status);

        ConnObj.GetDataSet(cmd_across);

        if (ConnObj.IsSuccess)
        {
            ;
        }
        else
        {
            lblErrorMsg.Text = ConnObj.Message;
            lblErrorMsg.ForeColor = System.Drawing.Color.Red;
            lblErrorMsg.Visible = true;
        }
    }
    private void DeleteAction()
    {
        cmd_across = new SqlCommand("sp_delete_brands_campaigns_action");
        cmd_across.Parameters.AddWithValue("@action_id", SessionState.EditId_2);
        cmd_across.Parameters.AddWithValue("@campaign_id", SessionState._Campaign.campaign_id);
        ConnObj.GetDataSet(cmd_across);
    }
    private void SetActionSpecificData(byte campaign_type)
    {
        switch (campaign_type)
        {
            // like page
            case 2:
                cmd_across.Parameters.AddWithValue("@campaign_type", Convert.ToByte(campaign_type));
                cmd_across.Parameters.AddWithValue("@val_1", SessionState._Campaign.actions[campaign_type].val1);
                cmd_across.Parameters.AddWithValue("@val_2", SessionState._Campaign.actions[campaign_type].val2);
                cmd_across.Parameters.AddWithValue("@val_3", "");
                cmd_across.Parameters.AddWithValue("@val_4", "");
                break;
            // like/share twitter post
            case 3:
            case 4:
                cmd_across.Parameters.AddWithValue("@campaign_type", Convert.ToByte(campaign_type));
                cmd_across.Parameters.AddWithValue("@val_1", SessionState._Campaign.actions[campaign_type].val1);
                cmd_across.Parameters.AddWithValue("@val_2", SessionState._Campaign.actions[campaign_type].val2);
                cmd_across.Parameters.AddWithValue("@val_3", SessionState._Campaign.actions[campaign_type].val3);
                cmd_across.Parameters.AddWithValue("@val_4", SessionState._Campaign.actions[campaign_type].val4);
                break;
        }
    }
    #endregion

    protected void btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect(SessionState.WebsiteURL + "brands/brand-create-campaign-1.aspx");
    }
    protected void btn_Next_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            lblValidationErrors.Text = "Check page For Incomplete Data";
            lblValidationErrors.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "HideStatusNotification1", "HideStatusNotification()", true);
            return;
        }
        byte[] campaign_type_arr = { 2,3,4 };
        foreach (byte campaign_type in campaign_type_arr)
        {
            CreateOrUpdateActions(campaign_type);
        }

        if (lblErrorMsg.Text == "")
        {
            Response.Redirect(SessionState.WebsiteURL + "brands/brand-create-campaign-3ii.aspx");
        }
    }
    protected void valCheckbox1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // case : no action selected
        if ((!chk_Action_1.Checked) && (!chk_Action_2.Checked) && (!chk_Action_3.Checked))
        {
            args.IsValid = false;
            chk_Action_1.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
            chk_Action_2.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
            chk_Action_3.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
            return;
        }


        // case: checkbox 1 has been selected
        if (chk_Action_1.Checked)
        {
            if ((drpPages5.SelectedValue == "") || (drpPages5.SelectedValue == "0"))
            {
                drpPages5.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
                RequiredFieldValidator13.IsValid = false;
                return;
            }
            args.IsValid = true;

        }



        // case : checkbox 2 or 3 is selected
        if ((chk_Action_2.Checked) || (chk_Action_3.Checked))
        {
            if (txtSelectedPost.Text == "")
            {
                txtSelectedPost.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
                RequiredFieldValidator1.IsValid = false;
                return;
            }
            args.IsValid = true;

        }
    }
}
