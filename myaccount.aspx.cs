using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Text;
using Facebook;
using System.Security.Cryptography;
using System.Web.UI.HtmlControls;

public partial class myaccount : System.Web.UI.Page
{
    public static ConnectionClass ConnObj = null;
    public static int Total = 0;

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
        if ((!Page.IsPostBack) && (SessionState._SignInUser != null))
        {
            FirstPos();
        }
        else if (SessionState._SignInUser != null)
        {
            ;
        }
        else
        {
            Response.Redirect(SessionState.WebsiteURL + "login.aspx");
        }

    }
    #endregion

    private void FirstPos()
    {
        ProfileImg.Src = SessionState._SignInUser.profileurl;
        Total = 0;
        SqlCommand cmd = new SqlCommand("sp_brandyy_GetUserActivities");
        cmd.Parameters.AddWithValue("@reg_uid", SessionState._SignInUser.reg_uid);
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            RepTab.DataSource = ConnObj.DataSet.Tables[0];
            RepTab.DataBind();
        }
        else
        {

        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        lblLoading.Text = "InProgress..";
        syncusercampaignactivities obj = new syncusercampaignactivities();
        Response.Redirect(SessionState.WebsiteURL + "myaccount.aspx");
    }


    protected void RepTab_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "UpdateCode")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showpopup", "showpopup()", true);
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            Int64 id = Convert.ToInt64(commandArgs[0]);
            string code = commandArgs[1];
        }
    }

}
