using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brands_transactions : System.Web.UI.Page
{
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
        RunCashTransaction();
        getPointsTrans();
        getCashTrans();
    }

    private void RunCashTransaction()
    {
        cashtransaction obj = new cashtransaction();
    }
    private void getPointsTrans()
    {
        SqlCommand cmd = new SqlCommand("sp_select_brandLatestTransactions");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            rpTransactions.DataSource = ConnObj.DataSet.Tables[0];
            rpTransactions.DataBind();
        }
    }

    private void getCashTrans()
    {
        SqlCommand cmd = new SqlCommand("sp_select_brandyycashTransactions");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {
            rpAcitivies.DataSource = ConnObj.DataSet.Tables[0];
            rpAcitivies.DataBind();
        }

    }
    protected void rpAcitivies_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            SessionState.EditId = Convert.ToInt64(commandArgs[0]);
            SessionState._Campaign = new Campaign(SessionState.EditId, SessionState._BrandAdmin.brand_id);
            SessionState._Campaign.reward_date = Convert.ToString(commandArgs[1]);
            Response.Redirect(SessionState.WebsiteURL + "brands/campaignview.aspx");
        }
    }
}