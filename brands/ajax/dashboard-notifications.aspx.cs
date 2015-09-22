using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class brands_ajax_dashboard_notifications : System.Web.UI.Page
{
    public static ConnectionClass ConnObj = null;
    public int Cnt;
    public Int64 pending_activities_verification = 0;

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
        GetSocialMediaAccountsExpiry();
        CheckIfActivitiesWaitingVerification();
    }

    private void GetSocialMediaAccountsExpiry()
    {
        SqlCommand cmd = new SqlCommand("sp_Brand_GeExpiringSocialMediaAccounts");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ConnObj.DataSet.Tables[0].Rows)
            {

                TimeSpan t = (DateTime.Now - Convert.ToDateTime(dr["last_verified_on"]));
                Int64 NrOfDaysSinceVerification = Convert.ToInt64(t.TotalDays);
                Int64 TotalNoOfDays = 20;
                string ticker = "";
                if (NrOfDaysSinceVerification >= TotalNoOfDays)
                {
                    ticker = "<span class='text-red'> Expired since " + (NrOfDaysSinceVerification - TotalNoOfDays) + " Days</span> ";
                }
                else
                {
                    ticker = "<span class='text-green'>Will expire in " + (TotalNoOfDays - NrOfDaysSinceVerification) + " Days</span> ";
                }
                dr["name"] = "";
                if (Convert.ToString(dr["id"]) == "1")
                {
                    dr["name"] = "<div>"
                       + "<a href='#' onclick=\"activate('" + SessionState.WebsiteURLBrand + "activatefb.aspx')\" style='cursor:pointer' class='btn btn-primary'>Authorize</a>"
                       + " " + Convert.ToString(ticker)
                       + "</div>"
                       ;
                }
                else if (Convert.ToString(dr["id"]) == "2")
                {
                    dr["name"] = "<div class=\"socialbtn twitter_login\" >"
                        + "<a href='" + SessionState.WebsiteURLBrand + "activatetw.aspx' style='cursor:pointer' class='btn btn-primary'>Authorize</a>"
                        + " " + Convert.ToString(ticker)
                        + "</div>";
                }
                else if (Convert.ToString(dr["id"]) == "3")
                {
                    dr["name"] = ""
                        + " " + Convert.ToString(ticker)
                        + "<a href='" + "https://api.instagram.com/oauth/authorize/?client_id=" + System.Configuration.ConfigurationManager.AppSettings["instagram.clientid"].ToString() + "&redirect_uri=" + System.Configuration.ConfigurationManager.AppSettings["instagram.brands.redirecturi"].ToString() + "&response_type=code' style='cursor:pointer' class='btn btn-primary'>Authorize</a>"                        
                        + "";
                }
                else if (Convert.ToString(dr["id"]) == "4")
                {
                    dr["name"] = "&nbsp;";
                }
                else
                {
                    dr["name"] = "";
                }
            }

            Repeater1.DataSource = ConnObj.DataSet.Tables[0];
            Repeater1.DataBind();
        }
        else
        {
            ;
        }

    }

    private void CheckIfActivitiesWaitingVerification()
    {
        Int32 b_id = SessionState._BrandAdmin.brand_id;
        SqlCommand cmd = new SqlCommand("sp_brandyy_GetAllPendingActivitiesOverview");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@verification_status", 0);


        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ConnObj.DataSet.Tables[0].Rows)
            {
                if (Convert.ToInt64(dr["num_of_pending_activities"]) > 0)
                {
                    pending_activities_verification += Convert.ToInt64(dr["num_of_pending_activities"]);                    
                }
            }
        }        
    }
}