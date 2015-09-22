using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brands_brandprofile_update : System.Web.UI.Page
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
        FillBrandDetails();        
    }

    #region private methods
    private void FillBrandDetails()
    {
        SqlCommand cmd = new SqlCommand("sp_select_brands_master");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);        
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess == true && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            txtBrandName.Text = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["name"]);
            rdStatus.SelectedValue = Convert.ToString( Convert.ToByte(ConnObj.DataSet.Tables[0].Rows[0]["active_flag"]) );
        }
        else
        {
            Response.Redirect(SessionState.WebsiteURLBrand + "brandusers.aspx");
        }
    }
    private void UploadImage(int id)
    {
        string extension = System.IO.Path.GetExtension(FileUpload1.FileName);
        if (extension == "") return;
        string logoname = "";
        if (CheckImage(extension) == true)
        {
            logoname = id + extension;
            FileUpload1.SaveAs(Server.MapPath("~/brands/uploads/logos/" + logoname));
        }
        else
        {
            lblErrorMsg.Text = "Please select a valid Image type";
            lblErrorMsg.ForeColor = System.Drawing.Color.Red;
        }
    }
    private bool CheckImage(String extension)
    {
        if (extension == ".png")
        {
            return true;
        }
        return false;
    }
    private void UpdateBrand()
    {
        SqlCommand cmd = new SqlCommand("sp_update_brands_master");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@name", SessionState._BrandAdmin.brand_name);
        cmd.Parameters.AddWithValue("@active_flag", Convert.ToString( rdStatus.SelectedValue ));    
        cmd.Parameters.AddWithValue("@updated_by", SessionState._BrandAdmin.user_id);
        ConnObj.GetDataTab(cmd);
        if (ConnObj.IsSuccess)
        {
            lblErrorMsg.Text = "Saved Successfully";
        }
    }
    #endregion

    #region protected methods
    protected void btn_Update_Click(object sender, EventArgs e)
    {
        UpdateBrand();
    }
    protected void btn_UploadLogo_Click(object sender, EventArgs e)
    {
        UploadImage(SessionState._BrandAdmin.brand_id);
    }
    #endregion
}