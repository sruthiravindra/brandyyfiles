using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brands_registration : System.Web.UI.Page
{
    ConnectionClass ConnObj = null;

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
    }
    #endregion

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("sp_insert_brands_master");
        cmd.Parameters.AddWithValue("@name", txtbrandname.Text.Trim());
        cmd.Parameters.AddWithValue("@active", true);
        cmd.Parameters.AddWithValue("@category", 0);
        cmd.Parameters.AddWithValue("@useremail", txtbrandusername.Text.Trim());
        cmd.Parameters.AddWithValue("@password", txtbranduserpswd1.Text.Trim());
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && (ConnObj.DataSet.Tables.Count > 0) && (ConnObj.DataSet.Tables[0].Rows.Count > 0) && (Convert.ToInt32(ConnObj.DataSet.Tables[0].Rows[0]["id"]) != 0))
        {
            DataRow dr = ConnObj.DataSet.Tables[1].Rows[0];
            SessionState._BrandAdmin = new BrandAdmin(Convert.ToInt32(dr["id"]), Convert.ToString(dr["name"]));
            SessionState._BrandAdmin.user_id = Convert.ToInt64(dr["user_id"]);
            SessionState._BrandAdmin.username = Convert.ToString(dr["username"]);
            SessionState._BrandAdmin.useremail = Convert.ToString(dr["useremail"]);
            SessionState._BrandAdmin.password = Convert.ToString(dr["password"]);
            SessionState._BrandAdmin.created_date_time = Convert.ToString(dr["created_date_time"]);
            UploadImage(SessionState._BrandAdmin.brand_id);
            Response.Redirect(SessionState.WebsiteURLBrand + "homepage.aspx");
        }
        else
        {
            divAlert.Visible = true;
            lblErrMsg.Text = "Brand already exists";
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
    }
    private bool CheckImage(String extension)
    {
        if (extension == ".png")
        {
            return true;
        }
        return false;
    }
}