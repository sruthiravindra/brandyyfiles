using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for syncusercampaignactivities
/// </summary>
public class syncusercampaignactivities
{
    public ConnectionClass ConnObj = new ConnectionClass();
    Dictionary<Int64, string> page_tokens = new Dictionary<Int64, string>();
    List<fbuser> fbsm_id = new List<fbuser>();
    List<fbuser> twsm_id = new List<fbuser>();
    List<fbuser> instasm_id = new List<fbuser>();
    CommonVariableCodes _CommonVariableCodes = new CommonVariableCodes();
	public syncusercampaignactivities()
	{
        getFacebookAccessToken();
        getTwitterAccessToken();
        getInstaAccessToken();
	}

    #region private functions

    private void getFacebookAccessToken()
    {
        string reg_uid = Convert.ToString(SessionState._SignInUser.reg_uid);
        string sm_id = "1";
        string token = "";
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
        string reg_uid = Convert.ToString(SessionState._SignInUser.reg_uid);
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
        string reg_uid = Convert.ToString(SessionState._SignInUser.reg_uid);
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