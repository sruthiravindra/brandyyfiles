using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brands_socialmedias : System.Web.UI.Page
{
    public string[] sm_text = new string[5];
    
    public static ConnectionClass ConnObj = null;
    public int Cnt;

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
        SessionState.EditId_2 = 0;
        SessionState.ActivityID = 0;
        sm_text[1] = "Facebook pages";
        sm_text[2] = "Twitter pages";
        sm_text[3]="Instagram pages";
        sm_text[4] = "Websites";
        LoadSocialMedias();
    }

    private void LoadSocialMedias()
    {
        SqlCommand cmd = new SqlCommand("sp_Get_Brands_Social_Media_Accounts_And_Pages");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        ConnObj.GetDataSet(cmd);

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {

            
            foreach (DataRow dr in ConnObj.DataSet.Tables[0].Rows)
            {
                if (dr["brand_id"] == DBNull.Value)
                {
                    if (Convert.ToString(dr["id"]) == "1")
                    {
                        dr["name"] = "<div class=\"socialbtn twitter_login\" >"
                            + "<a href='#' onclick=\"activate('" + SessionState.WebsiteURLBrand + "activatefb.aspx')\" style='cursor:pointer'>Activate Facebbok</a>"
                            
                            + "</div>";
                    }
                    else if (Convert.ToString(dr["id"]) == "2")
                    {
                        dr["name"] = "<div class=\"socialbtn twitter_login\" >"
                            + "<a href='" + SessionState.WebsiteURLBrand + "activatetw.aspx' target='_blank' style='cursor:pointer'><img src='" + SessionState.WebsiteURL + "images/activatetwitter.png' /></a>"                            
                            + "</div>";                        
                    }
                    else if (Convert.ToString(dr["id"]) == "3")
                    {
                        dr["name"] = "<div class=\"socialbtn twitter_login\" >"
                            + "<a href='" + "https://api.instagram.com/oauth/authorize/?client_id=" + System.Configuration.ConfigurationManager.AppSettings["instagram.clientid"].ToString() + "&redirect_uri=" + System.Configuration.ConfigurationManager.AppSettings["instagram.brands.redirecturi"].ToString() + "&response_type=code' style='cursor:pointer'>Activate Instagram</a>"                            
                            + "</div>";
                    }
                    else
                    {
                        dr["name"] = "";
                    }
                }
                else
                {
                    TimeSpan t = (DateTime.Now - Convert.ToDateTime(dr["last_verified_on"]));
                    Int64 NrOfDaysSinceVerification = Convert.ToInt64( t.TotalDays );
                    Int64 TotalNoOfDays = 20;
                    string ticker = "";
                    if (NrOfDaysSinceVerification >= TotalNoOfDays)
                    {
                        ticker = "<span class='text-red'> Expired since " + (NrOfDaysSinceVerification - TotalNoOfDays) + " Days</span>";
                    }
                    else
                    {
                        ticker = "<span class='text-green'>Will expire in " + (TotalNoOfDays - NrOfDaysSinceVerification) + " Days</span>";
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
                        dr["name"] = "<div class=\"socialbtn twitter_login\" >"
                            + "<a href='" + "https://api.instagram.com/oauth/authorize/?client_id=" + System.Configuration.ConfigurationManager.AppSettings["instagram.clientid"].ToString() + "&redirect_uri=" + System.Configuration.ConfigurationManager.AppSettings["instagram.brands.redirecturi"].ToString() + "&response_type=code' style='cursor:pointer' class='btn btn-primary'>Authorize</a>"
                            + " " + Convert.ToString(ticker)
                            + "</div>";
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
            }

            Repeater1.DataSource = ConnObj.DataSet.Tables[0];
            Repeater1.DataBind();
        }
        else
        {
            ;
        }

    }

    protected void btnAddPages_Click(object sender, EventArgs e)
    {
        Button btn = (Button)(sender);
        string[] parametrs = Convert.ToString(btn.CommandArgument).Split(',');
        SessionState.EditId_2=Convert.ToInt64(parametrs[0]);
        SessionState.ActivityID = Convert.ToInt64(parametrs[1]);
        Response.Redirect(SessionState.WebsiteURLBrand+"socialmediapage-create.aspx");
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //HiddenField hdn = (HiddenField)e.Item.FindControl("hdnSmID");
        //Repeater rpt = (Repeater)e.Item.FindControl("Repeater2");

        //SqlCommand cmd = new SqlCommand("sp_Get_Brands_SM_Acc_Pages");
        //cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        //cmd.Parameters.AddWithValue("@brand_sm_id", Convert.ToInt16(hdn.Value));
        //ConnObj.GetDataSet(cmd);

        //if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        //{
        //    rpt.DataSource = ConnObj.DataSet.Tables[0];
        //    rpt.DataBind();
        //}

    }
}