using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IchooseIT.DAL;
using System.Data.SqlClient;
public partial class brands_uc_create_campaign_5 : System.Web.UI.UserControl
{
    static Dictionary<byte, Int64> alreadyexistingactions = new Dictionary<byte, Int64>();   
    ConnectionClass ConnObj = null;
    public string page_id = "";    
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
    #region purchase in store functions
    private void FillActionControls()
    {
        SetAlreadyExists(8);

        if (SessionState._Campaign.actions[8] != null)
        {
            txtCheckinUrl.Text = SessionState._Campaign.actions[8].val2;
            page_id = SessionState._Campaign.actions[8].val3;
        }
    }
    private void SetAlreadyExists(byte campaign_type)
    {
        if (SessionState._Campaign.actions[campaign_type] != null)
        {
            if (SessionState._Campaign.actions[campaign_type].action_id > 0)
            {
                alreadyexistingactions.Add(campaign_type, SessionState._Campaign.actions[campaign_type].action_id);
            }
        }
    }

    private void LoadCampaignTypeForm()
    {
       

    }
    
    private bool CheckImage(String extension)
    {
        if ((extension == ".xls") || (extension == ".xlsx"))
        {
            return true;
        }
        return false;
    }
    #endregion

    #region insert update delete
    private void CreateOrUpdateActions(byte campaign_type)
    {
        bool checked_status = false;
        switch (campaign_type)
        {
            //case 7: checked_status = chk_Action_1.Checked; break;
            case 8: checked_status = chk_Action_2.Checked; break;
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
         case 8:
                SetActionSession(8, "", txtCheckinUrl.Text.Trim(), page_id, "", "");

                cmd_across.Parameters.AddWithValue("@campaign_type", Convert.ToByte(campaign_type));
                cmd_across.Parameters.AddWithValue("@val_1", "");
                cmd_across.Parameters.AddWithValue("@val_2", SessionState._Campaign.actions[campaign_type].val2);
                cmd_across.Parameters.AddWithValue("@val_3", SessionState._Campaign.actions[campaign_type].val3);
                cmd_across.Parameters.AddWithValue("@val_4", "");
                break;
        }
    }
    #endregion

    private void SetActionSession(byte campaign_type, string val1, string val2, string val3, string val4, string displayval1)
    {
        campaign_action ca = new campaign_action();

        ca = new campaign_action();
        ca.campaign_type = campaign_type;
        if (SessionState._Campaign.actions[ca.campaign_type] != null)
        {
            ca.action_id = SessionState._Campaign.actions[ca.campaign_type].action_id;
        }
        ca.val1 = val1;
        ca.val2 = val2;
        ca.val3 = val3;
        ca.val4 = val4;
        ca.displayval1 = displayval1;
        SessionState._Campaign.actions[ca.campaign_type] = ca;
    }
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

        byte[] campaign_type_arr = { 8 };
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
        

        if (chk_Action_2.Checked)
        {
            if (txtCheckinUrl.Text.Trim() == "")
            {
                RequiredFieldValidator1.IsValid = false;
                return;
            }

            importfbpagedetails fb = new importfbpagedetails();
            page_id = fb.getPageDetails(txtCheckinUrl.Text);
            if (page_id == "")
            {
                RequiredFieldValidator1.IsValid = false;
                return;
            }
            args.IsValid = true;

        }
        else
        {
            args.IsValid = false;
            chk_Action_2.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
        }
    }
}