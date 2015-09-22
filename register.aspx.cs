using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;




public partial class _register : System.Web.UI.Page
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
        if ((!Page.IsPostBack))
        {
            FirstPos();
        }
    }
    #endregion


    protected void FirstPos()
    {
        FillDate();
        FillCountry();
        lblregError.Text = "";
    }


    protected void btnRegister_Click(object sender, EventArgs e)
    {
        lblregError.Text = "";
        SqlCommand cmd = new SqlCommand("sp_select_user_master_Registration");
        cmd.Parameters.AddWithValue("@reg_name", txtFName.Text.Trim() + " " + txtLName.Text.Trim());
        cmd.Parameters.AddWithValue("@reg_fname", txtFName.Text.Trim());
        cmd.Parameters.AddWithValue("@reg_lname", txtLName.Text.Trim());
        cmd.Parameters.AddWithValue("@reg_email", txtRegEmail.Text.Trim());
        cmd.Parameters.AddWithValue("@profile_image_url", "images/no_profile.png");
        cmd.Parameters.AddWithValue("@gender", drpGender.SelectedValue);
        cmd.Parameters.AddWithValue("@dob", Convert.ToDateTime(drpYear.SelectedValue + "-" + drpMonth.SelectedValue + "-" + drpDay.SelectedValue));
        cmd.Parameters.AddWithValue("@password", txtRegPass.Text.Trim());
        cmd.Parameters.AddWithValue("@country", drpCountry.SelectedValue);
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && (ConnObj.DataSet.Tables.Count > 0) && (ConnObj.DataSet.Tables[0].Rows.Count > 0))
        {
            DataRow dr = ConnObj.DataSet.Tables[0].Rows[0];
            SessionState._SignInUser = new SignInUser(Convert.ToInt64(dr["id"]),
                  Convert.ToString(txtRegEmail.Text.Trim()),
                  Convert.ToString(txtFName.Text.Trim()),
                  Convert.ToString(txtLName.Text.Trim()),
                  Convert.ToString(""),
                  Convert.ToString("images/no_profile.png"));
            Response.Redirect(SessionState.WebsiteURL + "myprofile.aspx");
        }
        else
        {
            lblregError.Text = "* " + txtRegEmail.Text.Trim() + " already registered";
        }
    }

    private void FillDate()
    {
        Int16 i = 1;
        DateTime month = Convert.ToDateTime("2000-01-01");
        drpDay.Items.Insert(drpDay.Items.Count, new ListItem("Day", "0"));
        while (i <= 31)
        {
            if (i < 10)
            {
                drpDay.Items.Insert(drpDay.Items.Count, new ListItem("0" + i, Convert.ToString(i)));

            }
            else
            {
                drpDay.Items.Insert(drpDay.Items.Count, new ListItem(Convert.ToString(i), Convert.ToString(i)));

            }
            i++;
        }
        i = 1;
        drpMonth.Items.Insert(drpMonth.Items.Count, new ListItem("Month", "0"));
        while (i <= 12)
        {
            drpMonth.Items.Add(new ListItem(Convert.ToString(month.AddMonths(i - 1).ToString("MMM")), Convert.ToString(i)));
            i++;
        }
        drpYear.Items.Insert(drpYear.Items.Count, new ListItem("Year", "0"));
        i = 1901;
        do
        {
            drpYear.Items.Add(new ListItem(Convert.ToString(i), Convert.ToString(i)));
            i += 1;
        }
        while (i <= DateTime.Today.Year - 16);


    }
    protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        Int16 i = 1;
        if (drpMonth.SelectedIndex == 0) drpMonth.SelectedIndex = 1;
        if (drpYear.SelectedIndex == 0) drpYear.SelectedIndex = 1;
        Int16 j = Convert.ToInt16(DateTime.DaysInMonth(Convert.ToInt32(drpYear.Items[drpYear.SelectedIndex].Value), Convert.ToInt32(drpMonth.Items[drpMonth.SelectedIndex].Value)));
        drpDay.Items.Clear();
        do
        {
            if (i < 10)
            {
                drpDay.Items.Insert(drpDay.Items.Count, new ListItem("0" + i, Convert.ToString(i)));

            }
            else
            {
                drpDay.Items.Insert(drpDay.Items.Count, new ListItem(Convert.ToString(i), Convert.ToString(i)));

            }
            i += 1;
        } while (i <= j);
        drpDay.SelectedIndex = 0;
        i = 0;
        drpYear.Focus();
    }
    protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpMonth.SelectedIndex == 0) drpMonth.SelectedIndex = 1;
        if (drpYear.SelectedIndex == 0) drpYear.SelectedIndex = 1;
        Int16 i = 1;
        Int16 j = Convert.ToInt16(DateTime.DaysInMonth(Convert.ToInt32(drpYear.Items[drpYear.SelectedIndex].Value), Convert.ToInt32(drpMonth.Items[drpMonth.SelectedIndex].Value)));
        drpDay.Items.Clear();
        do
        {
            if (i < 10)
            {
                drpDay.Items.Insert(drpDay.Items.Count, new ListItem("0" + i, Convert.ToString(i)));

            }
            else
            {
                drpDay.Items.Insert(drpDay.Items.Count, new ListItem(Convert.ToString(i), Convert.ToString(i)));

            }
            i += 1;
        } while (i <= j);
        drpDay.SelectedIndex = 0;
        i = 0;
    }

    protected void FillCountry()
    {
        SqlCommand cmd = new SqlCommand("sp_select_user_master_Country");
        ConnObj.GetDataSet(cmd);
        drpCountry.DataSource = ConnObj.DataSet.Tables[0];
        drpCountry.DataTextField = "country_name";
        drpCountry.DataValueField = "country_id";
        drpCountry.DataBind();
        drpCountry.Items.Insert(0, new ListItem("Country", "0"));
        drpCountry.SelectedIndex = 0;
    }
}