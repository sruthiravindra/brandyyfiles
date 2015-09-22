using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IchooseIT.DAL;

public partial class syncusercampaignactivities : System.Web.UI.Page
{

    public ConnectionClass ConnObj = null; 
    Dictionary<Int64, string> page_tokens = new Dictionary<Int64, string>();
    List<fbuser> fbsm_id = new List<fbuser>();
    List<fbuser> twsm_id = new List<fbuser>();
    List<fbuser> instasm_id = new List<fbuser>();
    CommonVariableCodes _CommonVariableCodes = new CommonVariableCodes();
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

        if (!Page.IsPostBack)
        {
            getFacebookAccessToken();
            getTwitterAccessToken();
            getInstaAccessToken();
        }

    }
    #endregion

    #region private functions

    private void getFacebookAccessToken()
    {
        string reg_uid = "4";
        string sm_id = "1";
        string token="";
        string sm_uid = "";
        {
            // get user access token
            SqlCommand cmd = new SqlCommand("sp_user_get_Token");
            cmd.Parameters.AddWithValue("@reg_uid", reg_uid);
            cmd.Parameters.AddWithValue("@sm_id", sm_id);
            ConnObj.GetDataSet(cmd);
            if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
            {
                token = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["token"]);
                sm_uid = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["sm_uid"]);
                importfbuserdetails obj = new importfbuserdetails();
                obj.getAllProfileDetails(reg_uid, token, sm_uid);
            }
        }
    }

    private void getTwitterAccessToken()
    {
        string reg_uid = "4";
        string sm_id = "2";
        string sm_uid = "";
        string username = "";
        {
            // get user access token
            SqlCommand cmd = new SqlCommand("sp_user_get_Token");
            cmd.Parameters.AddWithValue("@reg_uid", reg_uid);
            cmd.Parameters.AddWithValue("@sm_id", sm_id);
            ConnObj.GetDataSet(cmd);
            if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
            {
                sm_uid = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["sm_uid"]);
                username = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["email"]);
                importtwitteruserdetails obj = new importtwitteruserdetails();
                obj.getUserPosts(reg_uid, sm_uid, username);
            }
        }
    }
    private void getInstaAccessToken()
    {
        string token = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Insta_access_token"]);
        string reg_uid = "1";
        string sm_id = "3";
        string sm_uid = "";
        string username = "";
        {
            // get user access token
            SqlCommand cmd = new SqlCommand("sp_user_get_Token");
            cmd.Parameters.AddWithValue("@reg_uid", reg_uid);
            cmd.Parameters.AddWithValue("@sm_id", sm_id);
            ConnObj.GetDataSet(cmd);
            if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
            {
                sm_uid = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["sm_uid"]);
                username = Convert.ToString(ConnObj.DataSet.Tables[0].Rows[0]["email"]);
                importinstauserdetails obj = new importinstauserdetails();
                obj.getUserProfileDetails(reg_uid, sm_uid, username, token);
            }
        }
    }
    #endregion

}
