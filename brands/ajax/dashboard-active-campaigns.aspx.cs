﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brands_ajax_dashboard_active_campaigns : System.Web.UI.Page
{
    public static ConnectionClass ConnObj = null;
    public int Cnt;
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
        GetLatestProfileUpdates();
    }

    private void GetLatestProfileUpdates()
    {
        SqlCommand cmd = new SqlCommand("sp_Brand_GetCampaigns");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@campaign_status", _CommonVariableCodes.campaign_status_active);
        ConnObj.GetDataSet(cmd);        

        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0 && ConnObj.DataSet.Tables[0].Rows.Count > 0)
        {           

            Repeater1.DataSource = ConnObj.DataSet.Tables[0];
            Repeater1.DataBind();
        }
        else
        {
            lblNoActiveCampaigns.Visible = true;
        }
    }    
}