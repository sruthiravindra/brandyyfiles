using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IchooseIT.DAL;
using Facebook;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
public partial class brands_uc_create_campaign_3 : System.Web.UI.UserControl
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
        lblValidationErrors.Visible = false;
        alreadyexistingactions = new Dictionary<byte, Int64>();
        LoadCampaignTypeForm();

        if (SessionState.EditId > 0)
        {
            FillActionControls();
        }
    }
    private void FillActionControls()
    {
        SetAlreadyExists(1, chk_Action_1);
        SetAlreadyExists(5, chk_Action_2);
        SetAlreadyExists(6, chk_Action_3);

        if (SessionState._Campaign.actions[5] != null)
        {
            SqlCommand cmd = new SqlCommand("sp_select_brands_pages");
            cmd.Parameters.AddWithValue("@page_id", Convert.ToInt16(SessionState._Campaign.actions[5].val1));
            ConnObj.GetDataSet(cmd);
            drpPages2.SelectedValue = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["page_id"]);
            LoadFBPosts( Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["page_url"]));


            drpPages2.SelectedValue = SessionState._Campaign.actions[5].val1;
            txtSelectedPost.Text = SessionState._Campaign.actions[5].val3;

            SetActionSession(1, SessionState._Campaign.actions[5].val1, Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["sm_page_id"]), "", "", "");            
            SetActionSession(6, SessionState._Campaign.actions[5].val1, SessionState._Campaign.actions[5].val2, SessionState._Campaign.actions[5].val3, SessionState._Campaign.actions[5].val4, "");
            return;
        }
        if (SessionState._Campaign.actions[6] != null)
        {
            SqlCommand cmd = new SqlCommand("sp_select_brands_pages");
            cmd.Parameters.AddWithValue("@page_id", Convert.ToInt16(SessionState._Campaign.actions[6].val1));
            ConnObj.GetDataSet(cmd);
            drpPages2.SelectedValue = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["page_id"]);
            LoadFBPosts(Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["page_url"]));

            drpPages2.SelectedValue = SessionState._Campaign.actions[6].val1;
            txtSelectedPost.Text = SessionState._Campaign.actions[6].val3;

            SetActionSession(1, SessionState._Campaign.actions[6].val1, Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["sm_page_id"]), "", "", "");            
            SetActionSession(5, SessionState._Campaign.actions[6].val1, SessionState._Campaign.actions[6].val2, SessionState._Campaign.actions[6].val3, SessionState._Campaign.actions[6].val4, "");
        }
    }
    private void SetAlreadyExists( byte campaign_type, CheckBox chk)
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
        FB_Post_Share(SessionState._Campaign.campaign_objective);
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


    #region updatepanel-2

    private void FB_Post_Share(byte campaign_type)
    {
        // get pages
        SqlCommand cmd = new SqlCommand("sp_Get_Brands_Pages");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@sm_id", 1);
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            drpPages2.DataSource = ConnObj.DataSet.Tables[0];
            drpPages2.DataTextField = "page_name";
            drpPages2.DataValueField = "page_id";
            drpPages2.DataBind();
            drpPages2.Items.Insert(0, new ListItem("--- Select ---", "0"));
        }
        else
        {
            drpPages2.Visible = false;
            lblNoPages2.Visible = true;
        }
    }
    private void LoadFBPosts(String page_url)
    {

        var start = page_url.LastIndexOf("/") + 1;
        var end = page_url.Length;
        String page_url1 = page_url.Substring(start, end - start);
        Panel2_Clear();
        try
        {
            var accessToken = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["FB_access_token"]);
            var client = new FacebookClient(accessToken);
            dynamic posts;
            posts = client.Get("/" + page_url1 + "/posts");

            int cnt = 0;
            mypost p;
            List<mypost> myposts = new List<mypost>();
            //Loop through the returned friends
            foreach (var i in posts["data"])
            {
                p = new mypost();
                foreach (var j in i["actions"])
                {
                    p.post_url = j["link"];
                    break;
                }


                if (i["type"] == "photo")
                {
                    p.post_id = i["object_id"];
                    p.img_url = (i.ContainsKey("picture")) ? i["picture"] : "";
                }
                else
                {
                    string post_id_string= Convert.ToString(i["id"]);
                    p.post_id = post_id_string.Split('_')[1];                    
                }

                if (i.ContainsKey("message"))
                {                   
                    p.post = i["message"].Substring(0, ((i["message"].Length < 200) ? i["message"].Length : 200));
                    p.img_url = (i.ContainsKey("picture")) ? i["picture"] : "";
                    
                }


                p.created_on = i["created_time"];
                
                myposts.Add(p);
                cnt++;
                if (cnt > 10) break;
            }

            rep_PagePosts2.DataSource = myposts;
            rep_PagePosts2.DataBind();

            hiddenPageURL2.Value = page_url;
        }
        catch (Exception ex)
        {
            Panel2_Clear();
            lblPagePostsMsg2.Text = " Not a valid Page URL ";
            lblPagePostsMsg2.Visible = true;
        }
    }
    protected void drpPages2_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSelectedPost.Visible = false;

        if (drpPages2.SelectedValue == "0")
        {
            Panel2_Clear();
            return;
        }

        SetActionPageLikeSession();

        txtSelectedPost.Text = "";
        divPagePostsList.Visible = true;

        String page_url = SessionState._Campaign.actions[1].displayval1;
        LoadFBPosts(page_url);
    }
    private void Panel2_Clear()
    {
        rep_PagePosts2.DataSource = null;
        rep_PagePosts2.DataBind();
    }    
    private void SetActionPageLikeSession()
    {
        SqlCommand cmd = new SqlCommand("sp_select_brands_pages");
        cmd.Parameters.AddWithValue("@page_id", Convert.ToInt16(drpPages2.SelectedValue));
        ConnObj.GetDataSet(cmd);

        SetActionSession(1, Convert.ToString(drpPages2.SelectedValue), Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["sm_page_id"]), "", "", Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["page_url"]));                    
    }
    private void SetActionSession(byte campaign_type, string val1, string post_id, string post_text, string post_url, string displayval1)
    {
        campaign_action ca = new campaign_action();

        ca = new campaign_action();
        ca.campaign_type = campaign_type;
        if (SessionState._Campaign.actions[ca.campaign_type] != null)
        {
            ca.action_id = SessionState._Campaign.actions[ca.campaign_type].action_id;
        }
        ca.val1 = val1;
        ca.val2 = post_id;
        ca.val3 = post_text;
        ca.val4 = post_url;
        ca.displayval1 = displayval1;
        SessionState._Campaign.actions[ca.campaign_type] = ca;
    }
    protected void rep_PagePosts2_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "SetPostSession")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            //var start = Convert.ToString(commandArgs[0]).IndexOf("_") + 1;
            //var end = Convert.ToString(commandArgs[0]).Length;
            string post_id = Convert.ToString(commandArgs[0]);
            string post_url = Convert.ToString(commandArgs[1]);

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlContainerControl dynamic1 = (HtmlContainerControl)e.Item.FindControl("mypostsdiv");
                string post_text = dynamic1.InnerHtml;

                txtSelectedPost.Visible = true;
                txtSelectedPost.Text = post_text;
                divPagePostsList.Visible = false;

                SetActionSession(5, Convert.ToString(drpPages2.SelectedValue), post_id, post_text, post_url, "");
                SetActionSession(6, Convert.ToString(drpPages2.SelectedValue), post_id, post_text, post_url, "");
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
            case 1: checked_status = chk_Action_1.Checked; break;
            case 5: checked_status = chk_Action_2.Checked; break;
            case 6: checked_status = chk_Action_3.Checked; break;
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
            case 1:
                cmd_across.Parameters.AddWithValue("@campaign_type", Convert.ToByte(campaign_type));
                cmd_across.Parameters.AddWithValue("@val_1", SessionState._Campaign.actions[campaign_type].val1);
                cmd_across.Parameters.AddWithValue("@val_2", SessionState._Campaign.actions[campaign_type].val2);
                cmd_across.Parameters.AddWithValue("@val_3", "");
                cmd_across.Parameters.AddWithValue("@val_4", "");
                break;
            // Like Our Post On Facebook
            case 5:
            case 6:
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
        byte[] campaign_type_arr = { 1,5,6 };
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
            if ((drpPages2.SelectedValue == "") || (drpPages2.SelectedValue == "0"))
            {
                drpPages2.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
                RequiredFieldValidator13.IsValid = false;
                return;
            }
            args.IsValid = true;

        }
        
        

        // case : checkbox 2 or 3 is selected
        if ((chk_Action_2.Checked)||(chk_Action_3.Checked))
        {
            if(txtSelectedPost.Text == "") 
            {
                txtSelectedPost.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
                RequiredFieldValidator1.IsValid = false;
                return;
            }
            args.IsValid = true;

        }        
    }
}