﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IchooseIT.DAL;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;

public partial class brands_uc_create_campaign_10 : System.Web.UI.UserControl
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
        SetAlreadyExists(17, chk_Action_1);
        SetAlreadyExists(18, chk_Action_2);

        if (SessionState._Campaign.actions[18] != null)
        {
            SqlCommand cmd = new SqlCommand("sp_select_brands_pages");
            cmd.Parameters.AddWithValue("@page_id", Convert.ToInt16(SessionState._Campaign.actions[18].val1));
            ConnObj.GetDataSet(cmd);
            drpPages2.SelectedValue = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["page_id"]);
            LoadInstaPosts(Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["sm_page_id"]));           


            drpPages2.SelectedValue = SessionState._Campaign.actions[18].val1;
            txtSelectedPost.Text = SessionState._Campaign.actions[18].val3;
                        
            SetActionSession(17, SessionState._Campaign.actions[18].val1, SessionState._Campaign.actions[18].val2, SessionState._Campaign.actions[18].val3, SessionState._Campaign.actions[18].val4, "");
            
            return;
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
        Insta_Post_Share(SessionState._Campaign.campaign_objective);
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

    private void Insta_Post_Share(byte campaign_type)
    {
        // get pages
        SqlCommand cmd = new SqlCommand("sp_Get_Brands_Pages");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@sm_id", 3);
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
    private void LoadInstaPosts(String page_url)
    {
        Panel2_Clear();
        try
        {
            var accessToken = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Insta_access_token"]);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.instagram.com/v1/users/" + page_url + "/media/recent/?access_token=" + accessToken);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            WebResponse response = request.GetResponse();
            string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var tmpResult = JObject.Parse(responseData);

            var resultObject = tmpResult["data"].Values<JObject>()
               .Select(n => new { img_url = n["images"]["thumbnail"]["url"], post_id = n["id"], Context = n.Parent, created_on = ConvertFromUnixTimestamp( Convert.ToDouble( n["created_time"] ) ), post_url = n["link"], post = n["images"]["thumbnail"]["url"] }).ToArray();
            this.rep_PagePosts2.DataSource = resultObject;
            this.rep_PagePosts2.DataBind();

            hiddenPageURL2.Value = page_url;
        }
        catch (Exception ex)
        {
            Panel2_Clear();
            lblPagePostsMsg2.Text = " Not a valid Page URL ";
            lblPagePostsMsg2.Visible = true;
        }
    }
    private DateTime ConvertFromUnixTimestamp(double timestamp)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return origin.AddSeconds(timestamp);
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

        String sm_page_id = SessionState._Campaign.actions[17].val2;
        LoadInstaPosts(sm_page_id);
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

        SetActionSession(17, Convert.ToString(drpPages2.SelectedValue), Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["sm_page_id"]), "", "", Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["page_url"]));                            
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

                SetActionSession(18, Convert.ToString(drpPages2.SelectedValue), post_id, post_text, post_url, "");
                
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
            case 17: checked_status = chk_Action_1.Checked; break;
            case 18: checked_status = chk_Action_2.Checked; break;            
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
            case 17:
                cmd_across.Parameters.AddWithValue("@campaign_type", Convert.ToByte(campaign_type));
                cmd_across.Parameters.AddWithValue("@val_1", SessionState._Campaign.actions[campaign_type].val1);
                cmd_across.Parameters.AddWithValue("@val_2", SessionState._Campaign.actions[campaign_type].val2);
                cmd_across.Parameters.AddWithValue("@val_3", "");
                cmd_across.Parameters.AddWithValue("@val_4", "");
                break;
            // Like Our Post On Facebook
            case 18:            
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
        byte[] campaign_type_arr = { 17, 18 };
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
        if ((!chk_Action_1.Checked) && (!chk_Action_2.Checked))
        {
            args.IsValid = false;
            chk_Action_1.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
            chk_Action_2.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");            
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
        if ((chk_Action_2.Checked))
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