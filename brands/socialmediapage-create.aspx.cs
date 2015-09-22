using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brands_socialmediapage_create : System.Web.UI.Page
{
    public static ConnectionClass ConnObj = null;


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
        if ((SessionState.EditId_2 == 2) || (SessionState.EditId_2 == 3))
        {
            AddPageToDB();
        }
    }

    private void AddPageToDB()
    {
        string sm_uid = GetPageSMID();
        if ( sm_uid != "ERROR")
        {
            SqlCommand cmd = new SqlCommand("sp_insert_brands_pages");

            cmd.Parameters.AddWithValue("@page_id", 0);
            cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
            cmd.Parameters.AddWithValue("@brand_sm_id", SessionState.ActivityID);            
            cmd.Parameters.AddWithValue("@sm_id", SessionState.EditId_2);
            cmd.Parameters.AddWithValue("@page_name", txtPageName.Text.Trim());
            cmd.Parameters.AddWithValue("@page_url", txtPageUrl.Text.Trim());
            cmd.Parameters.AddWithValue("@sm_page_id", sm_uid);
            cmd.Parameters.AddWithValue("@created_by", SessionState._BrandAdmin.user_id);
            cmd.Parameters.AddWithValue("@updated_by", SessionState._BrandAdmin.user_id);

            ConnObj.GetDataTab(cmd);

            if (ConnObj.IsSuccess)
            {
                Response.Redirect(SessionState.WebsiteURLBrand + "socialmedias.aspx");
            }
        }
    }
    private string CreateInstaPage()
    {
        SqlCommand cmd = new SqlCommand("sp_Get_Brands_Social_Media_Accounts");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@brand_sm_id", SessionState.ActivityID);

        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            string page_url = Convert.ToString( ConnObj.DataSet.Tables[0].Rows[0]["profile_url"] );
            var start = page_url.LastIndexOf("/") + 1;
            var end = page_url.Length;
            txtPageName.Text = page_url.Substring(start, end - start);
            txtPageUrl.Text = page_url;
            return Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["sm_uid"]);            
        }
        return "ERROR";        
    }
    
    private string GetPageSMID()
    {
        string page_url = txtPageUrl.Text.Trim();
        string sm_page_id = "";
        if (SessionState.EditId_2 == 1)
        {
            var start = page_url.LastIndexOf("/") + 1;
            var end = page_url.Length;
            String page_url1 = page_url.Substring(start, end - start);
            importfbpagedetails obj = new importfbpagedetails();
            sm_page_id = obj.getPageDetails(page_url1);
            txtPageName.Text = obj.page_tag;
            
            if (sm_page_id == "")
            {
                lblErrorMsg.Text = "Invalid page";
                return "ERROR";
            }
        }
        else if (SessionState.EditId_2 == 2)
        {            
            //var start = page_url.LastIndexOf("/") + 1;
            //var end = page_url.Length;
            //sm_page_id = page_url.Substring(start, end - start);

            //importtwitterbranddetails obj = new importtwitterbranddetails();
            //sm_page_id = obj.getPageDetails(sm_page_id);
            //txtPageName.Text = obj.page_tag;
            
            //if (sm_page_id == "")
            //{
            //    lblErrorMsg.Text = "Invalid page";
            //    return "ERROR";
            //}

            var start = page_url.LastIndexOf("/") + 1;
            var end = page_url.Length;
            sm_page_id = page_url.Substring(start, end - start);

            return CreateInstaPage();
        }
        else if (SessionState.EditId_2 == 3)
        {
            var start = page_url.LastIndexOf("/") + 1;
            var end = page_url.Length;
            sm_page_id = page_url.Substring(start, end - start);

            return CreateInstaPage();
        }
        else if (SessionState.EditId_2 == 4)
        {
            var start = page_url.LastIndexOf("/") + 1;
            var end = page_url.Length;
            sm_page_id = page_url.Substring(start, end - start);
            txtPageName.Text = sm_page_id;
            if (sm_page_id == "")
            {
                lblErrorMsg.Text = "Invalid page";
                return "ERROR";
            }
        }
        return sm_page_id;
    }

    protected void btn_Panel2_Click(object sender, EventArgs e)
    {
        AddPageToDB();
    }
    protected void btn_Update_Click(object sender, EventArgs e)
    {

    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect(SessionState.WebsiteURLBrand+"socialmedias.aspx");
    }
}