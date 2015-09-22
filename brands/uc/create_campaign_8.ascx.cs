using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IchooseIT.DAL;
using System.IO;
using System.Data.SqlClient;

public partial class brands_uc_create_campaign_8 : System.Web.UI.UserControl
{
    public string doc_name = "";
    public string video_name = "";

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
        string test = video_name;
        // case : form loaded for edit
        if (SessionState.EditId > 0)
        {
            FillActionControls();
        }
    }

    private void FillActionControls()
    {
        SetAlreadyExists(13, chk_Action_1);
        SetAlreadyExists(14, chk_Action_2);
        SetAlreadyExists(15, chk_Action_3);
        SetAlreadyExists(16, chk_Action_4);
        if (SessionState._Campaign.actions[13] != null)
        {
            DrpPages1.SelectedValue = SessionState._Campaign.actions[13].val1;
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
        LoadWebsites(SessionState._Campaign.campaign_objective);

        LoadImageGallery();

        if( SessionState._Campaign.actions[15] != null )
        {
            doc_name = SessionState._Campaign.actions[15].val1;
        }
        if (SessionState._Campaign.actions[14] != null)
        {
            tr_video_1.Visible = false;
            tr_video_2.Visible = false;
            tr_video_3.Visible = false; 
            drpVideoSource.SelectedValue = SessionState._Campaign.actions[14].val2;
            switch (Convert.ToByte(SessionState._Campaign.actions[14].val2))
            {
                case 1: tr_video_1.Visible = true; video_name = SessionState._Campaign.actions[14].val1; break;
                case 2: txtVideoLink.Text = SessionState._Campaign.actions[14].val1; tr_video_2.Visible = true; break;
                case 3: txtVideoLiveStreamLink.Text = SessionState._Campaign.actions[14].val1; tr_video_3.Visible = true; break;
            }
            
        }
    }
    #region updatepanel-1
    private void LoadImageGallery()
    {
        
        string name = ""; string tilda = "";
        string path = Server.MapPath("~/brands/campaigns/16/" + Convert.ToString(SessionState._BrandAdmin.brand_id) + "/" + Convert.ToString(SessionState.EditId));
        if (Directory.Exists(path))
        {
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/brands/campaigns/16/" + Convert.ToString(SessionState._BrandAdmin.brand_id) + "/" + Convert.ToString(SessionState.EditId)));
            List<ListItem> files = new List<ListItem>();
            foreach (string filePath in filePaths)
            {
                string fileName = Path.GetFileName(filePath);
                files.Add(new ListItem(fileName, "campaigns/16/" + Convert.ToString(SessionState._BrandAdmin.brand_id) + "/" + Convert.ToString(SessionState.EditId) + "/" + fileName));

                name += tilda + fileName;
                tilda = "~";
            }

            SetActionSession(16, name, "", "", "", "");
            repSlideShow.DataSource = files;
            repSlideShow.DataBind();
        }
        
    }
    private void LoadWebsites(byte campaign_type)
    {
        // get pages
        SqlCommand cmd = new SqlCommand("sp_Get_Brands_Pages");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@sm_id", 4);
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            DrpPages1.DataSource = ConnObj.DataSet.Tables[0];
            DrpPages1.DataTextField = "page_name";
            DrpPages1.DataValueField = "page_id";
            DrpPages1.DataBind();
            DrpPages1.Items.Insert(0, new ListItem("--- Select ---", "0"));

            if (SessionState._Campaign.actions[13] != null)
            {
                if (Convert.ToInt16(SessionState._Campaign.actions[13].val1) > 0)
                {
                    DrpPages1.SelectedValue = SessionState._Campaign.actions[13].val1;
                }
            }
        }
        else
        {
            DrpPages1.Visible = false;
            lblNoPages1.Visible = true;
        }
    }
    
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
    #endregion

    #region insert update delete
    private void CreateOrUpdateActions(byte campaign_type)
    {
        bool checked_status = false;
        switch (campaign_type)
        {
            case 13: checked_status = chk_Action_1.Checked; break;
            case 14: checked_status = chk_Action_2.Checked; break;
            case 15: checked_status = chk_Action_3.Checked; break;
            case 16: checked_status = chk_Action_4.Checked; break;
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
            case 13:
                cmd_across.Parameters.AddWithValue("@campaign_type", Convert.ToByte(campaign_type));
                cmd_across.Parameters.AddWithValue("@val_1", SessionState._Campaign.actions[campaign_type].val1);
                cmd_across.Parameters.AddWithValue("@val_2", SessionState._Campaign.actions[campaign_type].val2);
                cmd_across.Parameters.AddWithValue("@val_3", "");
                cmd_across.Parameters.AddWithValue("@val_4", "");
                MoveImagesToCampaignFolder();
                break;
            case 16:
                cmd_across.Parameters.AddWithValue("@campaign_type", Convert.ToByte(campaign_type));
                cmd_across.Parameters.AddWithValue("@val_1", SessionState._Campaign.actions[campaign_type].val1);
                cmd_across.Parameters.AddWithValue("@val_2", SessionState._Campaign.actions[campaign_type].val2);
                cmd_across.Parameters.AddWithValue("@val_3", "");
                cmd_across.Parameters.AddWithValue("@val_4", "");
                MoveImagesToCampaignFolder();
                break;
            case 14:
                switch (drpVideoSource.SelectedValue)
                {
                    case "1":break;
                    case "2":                        
                        SetActionSession(14, txtVideoLink.Text.Trim(), "2", "", "", "");
                        break;
                    case "3":SetActionSession(14, txtVideoLiveStreamLink.Text.Trim(), "3", "", "", "");break;
                }
                cmd_across.Parameters.AddWithValue("@campaign_type", Convert.ToByte(campaign_type));
                cmd_across.Parameters.AddWithValue("@val_1", SessionState._Campaign.actions[campaign_type].val1);
                cmd_across.Parameters.AddWithValue("@val_2", SessionState._Campaign.actions[campaign_type].val2);
                cmd_across.Parameters.AddWithValue("@val_3", "");
                cmd_across.Parameters.AddWithValue("@val_4", "");              
                break;
            case 15:
                cmd_across.Parameters.AddWithValue("@campaign_type", Convert.ToByte(campaign_type));
                cmd_across.Parameters.AddWithValue("@val_1", SessionState._Campaign.actions[campaign_type].val1);
                cmd_across.Parameters.AddWithValue("@val_2", SessionState._Campaign.actions[campaign_type].val2);
                cmd_across.Parameters.AddWithValue("@val_3", "");
                cmd_across.Parameters.AddWithValue("@val_4", "");
                break;
        }
    }
    private void MoveImagesToCampaignFolder()
    {
        string path1 = Server.MapPath("~/brands/campaigns/16/" + Convert.ToString(SessionState._BrandAdmin.brand_id) + "/0");
        if (Directory.Exists(path1))
        {
            string path2 = Server.MapPath("~/brands/campaigns/16/" + Convert.ToString(SessionState._BrandAdmin.brand_id) + "/" + Convert.ToString(SessionState._Campaign.campaign_id));
            Directory.Move(path1, path2);
        }
    }
    #endregion
    protected void DrpPages1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("sp_select_brands_pages");
        cmd.Parameters.AddWithValue("@page_id", Convert.ToInt16(DrpPages1.SelectedValue));
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            SetActionSession(13, Convert.ToString(DrpPages1.SelectedValue), Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["sm_page_id"]), "", Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["page_name"]), Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["page_url"]));
        }
    }

    protected void lnkUploadDoc_Click(object sender, EventArgs e)
    {
        
        string extension = System.IO.Path.GetExtension(fileDoc.FileName);
        if (extension == ".pdf")
        {
            string name = Convert.ToString(SessionState._BrandAdmin.brand_id) + '_' + fileDoc.FileName;
            fileDoc.SaveAs(Server.MapPath("~/brands/campaigns/15/" + name));
                        
            SetActionSession(15, name, "", "", "", name);
            doc_name = SessionState._Campaign.actions[15].val1;
            Label1.Visible = false;
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Please select a valid PDF document";
            Label1.ForeColor = System.Drawing.Color.Red;
        }
    }
    
    protected void lnkUploadToGallery_Click(object sender, EventArgs e)
    {
        string path = Server.MapPath("~/brands/campaigns/16/" + Convert.ToString(SessionState._BrandAdmin.brand_id) + "/" + Convert.ToString(SessionState.EditId));
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string extension = System.IO.Path.GetExtension(fileSlideShow.FileName);
        if (extension == ".png")
        {
            string name =  fileSlideShow.FileName;
            fileSlideShow.SaveAs(Server.MapPath("~/brands/campaigns/16/" + Convert.ToString(SessionState._BrandAdmin.brand_id) + "/" + Convert.ToString(SessionState.EditId) + '/' + name));

            if (SessionState._Campaign.actions[16] != null)
            {
                if (SessionState._Campaign.actions[16].val1 != "")
                {
                    name = SessionState._Campaign.actions[16].val1 + "~" + name;
                }
            }
            LoadImageGallery();
        }
        else
        {
            ;
        }
    }
    protected void btn_RemovePhoto_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)(sender);
        string fileName = lnk.CommandArgument;
        fileName = Server.MapPath("~/brands/" + fileName); 
        if (fileName != null || fileName != string.Empty)
        {
            if ((System.IO.File.Exists(fileName)))
            {
                System.IO.File.Delete(fileName);
            }

            SetActionSession(16, fileName, "", "", "", "");
            LoadImageGallery();
            //Page.ClientScript.RegisterStartupScript(GetType(),"hwa","$('#flexslider').removeData('flexslider');",true);
            //Page.ClientScript.RegisterStartupScript(GetType(), "hwa1", "$('#flexslider').flexslider();", true);            
            //Page.RegisterStartupScript("starScript", "alert('hi');");
            
        }
    }
    protected void lnkUploadVideo_Click(object sender, EventArgs e)
    {
        string path = Server.MapPath("~/brands/campaigns/14/" + Convert.ToString(SessionState._BrandAdmin.brand_id) + "/" + Convert.ToString(SessionState.EditId));
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string extension = System.IO.Path.GetExtension(fileVideo.FileName);
        if (extension == ".mp4")
        {
            string name = "video.mp4";
            fileVideo.SaveAs(Server.MapPath("~/brands/campaigns/14/" + Convert.ToString(SessionState._BrandAdmin.brand_id) + "/" + Convert.ToString(SessionState.EditId) + '/' + name));

            video_name = name;
            SetActionSession(14, video_name, "1", "", "", "");
        }
        else
        {
            ;
        }
    }

    protected void drpVideoSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        tr_video_1.Visible = false;
        tr_video_2.Visible = false;
        tr_video_3.Visible = false;

        switch (Convert.ToByte(drpVideoSource.SelectedValue))
        {
            case 1: tr_video_1.Visible = true; break;
            case 2: tr_video_2.Visible = true; break;
            case 3: tr_video_3.Visible = true; break;
        }
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
        byte[] campaign_type_arr = { 13,14,15,16 };
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
        if ((!chk_Action_1.Checked) && (!chk_Action_2.Checked) && (!chk_Action_3.Checked) && (!chk_Action_4.Checked))
        {
            args.IsValid = false;
            chk_Action_1.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
            chk_Action_2.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
            chk_Action_3.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
            chk_Action_4.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
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

        
        // case : checkbox 2 is selected
        if (chk_Action_3.Checked)
        {
            if((doc_name == null)|| (doc_name == ""))
            {
                fileDoc.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
                RequiredFieldValidator1.IsValid = false;
                return;
            }
            args.IsValid = true;
        }




        // case : checkbox 3 is selected
        if (chk_Action_2.Checked)
        {
            // case : upload video option selected
            if ((drpVideoSource.SelectedValue == "1") && ((video_name == "") || (video_name == null)))
            {
                fileVideo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
                RequiredFieldValidator1.IsValid = false;
                return;
            }

            if ((drpVideoSource.SelectedValue == "2") && (txtVideoLink.Text.Trim()==""))
            {                
                txtVideoLink.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
                RequiredFieldValidator1.IsValid = false;
                return;
            }
            if ((drpVideoSource.SelectedValue == "2") && (txtVideoLink.Text.Trim() != ""))
            {

                txtVideoLink.Text = txtVideoLink.Text.Replace("https", "http");
                if (txtVideoLink.Text.Trim().IndexOf("http://www.youtube.com/") == 0)
                {
                    ;
                }
                else
                {
                    txtVideoLink.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
                    RequiredFieldValidator1.IsValid = false;
                    return;
                }
            }
            if ((drpVideoSource.SelectedValue == "3") && (txtVideoLiveStreamLink.Text.Trim() == ""))
            {
                txtVideoLiveStreamLink.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
                RequiredFieldValidator1.IsValid = false;
                return;
            }

            args.IsValid = true;
        }

        
        // case : checkbox 4 is selected
        if (chk_Action_4.Checked)
        {
            if ((SessionState._Campaign.actions[16] == null) || (SessionState._Campaign.actions[16].val1 == ""))
            {
                fileSlideShow.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFAAAA");
                RequiredFieldValidator1.IsValid = false;
                return;
            }
            args.IsValid = true;
        }
    }

    protected void lnkLoadVideo2_Click(object sender, EventArgs e)
    {        
        
    }
}
