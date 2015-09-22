using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class brands_uc2_create_campaign_19 : System.Web.UI.UserControl
{
    static Dictionary<byte, Int64> alreadyexistingactions = new Dictionary<byte, Int64>();

    ConnectionClass ConnObj = null;
    SqlCommand cmd_across = null;
    byte campaign_type_1 = 1;
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
            FirstPos();
        }
        else
        {
            Response.Redirect(SessionState.WebsiteURLBrand);
        }

    }
    #endregion

    private void FirstPos()
    {
        Int64 id = SessionState._Campaign.campaign_id;
        alreadyexistingactions = new Dictionary<byte, Int64>();

        DefaultHastagsFB();


        if (SessionState.EditId > 0)
        {
            FillActionControls();
        }

        SetPanelFBSession();

    }

    private void FillActionControls()
    {

        SetAlreadyExists(SessionState._Campaign.campaign_objective, chk_Action_1);

        if (SessionState._Campaign.actions[SessionState._Campaign.campaign_objective] != null)
        {
            DrpPages1.SelectedValue = SessionState._Campaign.actions[SessionState._Campaign.campaign_objective].val1;

            txtHashtags4.Text = SessionState._Campaign.actions[SessionState._Campaign.campaign_objective].val2;
            txtDefaultText.Text = SessionState._Campaign.actions[SessionState._Campaign.campaign_objective].val3;
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
            else
            {
                chk.Checked = false;
            }
        }
        else
        {
            chk.Checked = false;
        }
    }
    #region Default pics and promos #hashtags

    private void DefaultHastagsFB()
    {
        SqlCommand cmd = new SqlCommand("sp_Get_Brands_Pages");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@sm_id", 3);
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            DrpPages1.DataSource = ConnObj.DataSet.Tables[0];
            DrpPages1.DataTextField = "page_name";
            DrpPages1.DataValueField = "page_id";
            DrpPages1.DataBind();
        }
        else
        {
            DrpPages1.Visible = false;
            lblNoPages1.Visible = true;
        }


    }


    private void SetPanelFBSession()
    {
        campaign_action ca = new campaign_action();
        ca.campaign_type = SessionState._Campaign.campaign_objective;
        if (SessionState._Campaign.actions[ca.campaign_type] != null)
        {
            ca.action_id = SessionState._Campaign.actions[ca.campaign_type].action_id;
        }

        if (DrpPages1.Items.Count <= 0)
        {
            ca.val1 = "0";
            ca.val4 = "";
        }
        else
        {
            SqlCommand cmd = new SqlCommand("sp_select_brands_pages");
            cmd.Parameters.AddWithValue("@page_id", Convert.ToInt16(DrpPages1.SelectedValue));
            ConnObj.GetDataSet(cmd);
            if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
            {
                ca.val1 = Convert.ToString(DrpPages1.SelectedValue);
                ca.displayval1 = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["page_url"]);
                ca.val4 = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["page_name"]);
                lblTag1.Text = "@" + ca.val4;
            }
        }

        ca.val2 = txtHashtags4.Text.Trim();
        ca.val3 = txtDefaultText.Text.Trim();


        SessionState._Campaign.actions[ca.campaign_type] = ca;

    }

    #endregion

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
    protected void DrpPages1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetPanelFBSession();
    }
    protected void txtHashtags4_TextChanged(object sender, EventArgs e)
    {
        SessionState._Campaign.actions[SessionState._Campaign.campaign_objective].val2 = txtHashtags4.Text.Trim();
    }
    protected void txtDefaultText_TextChanged(object sender, EventArgs e)
    {
        SessionState._Campaign.actions[SessionState._Campaign.campaign_objective].val3 = txtDefaultText.Text.Trim();
    }

    #region insert update delete
    private void CreateOrUpdateActions(byte campaign_type)
    {
        bool checked_status = false;
        switch (campaign_type)
        {
            case 19: checked_status = chk_Action_1.Checked; break;
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
            case 19:
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
        SessionState._Campaign.create_campaign_step = 3;
        HiddenField up = this.Page.Master.FindControl("current_step") as HiddenField;
        up.Value = "3";
        /*
        byte[] campaign_type_arr = { 9, 10, 19 };
        foreach (byte campaign_type in campaign_type_arr)
        {
            CreateOrUpdateActions(campaign_type);
        }

        if (lblErrorMsg.Text == "")
        {
            Response.Redirect(SessionState.WebsiteURL + "brands/brand-create-campaign-3ii.aspx");
        }*/
    }
    protected void valCheckbox1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // case : no action selected
        if ((!chk_Action_1.Checked))
        {
            args.IsValid = false;
            chk_Action_1.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
            return;
        }
        // case: checkbox 1 has been selected
        if (chk_Action_1.Checked)
        {
            if ((DrpPages1.SelectedValue == "") || (DrpPages1.SelectedValue == "0"))
            {
                DrpPages1.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
                RequiredFieldValidator13.IsValid = false;
                return;
            }
            args.IsValid = true;
        }


        args.IsValid = true;
    }

}