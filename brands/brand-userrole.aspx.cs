using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

public partial class brands_brand_userrole : System.Web.UI.Page
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
        FillUserRole();
        FillBrandPanel();
        hdnRoleid.Value = "0";
        hdnrolName.Value = "";
        Permisison.Visible = false;        
        lblName.Text = "* Select 'View Permission' to view/set the permissions for a role";
        divWarning.Visible = true;
    }

    protected void FillUserRole()
    {
        SqlCommand cmd = new SqlCommand("sp_select_brand_userrole");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        ConnObj.GetDataSet(cmd); 
        rpRoles.DataSource = ConnObj.DataSet.Tables[0];
        rpRoles.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("sp_insert_brand_userrole");
        cmd.Parameters.AddWithValue("@brand_id", SessionState._BrandAdmin.brand_id);
        cmd.Parameters.AddWithValue("@role_name", txtRole.Text.Trim());
        ConnObj.GetDataSet(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataSet.Tables.Count > 0)
        {
            lblError.Text = "";
            FillUserRole();
            txtRole.Text = "";
            hdnRoleid.Value = "0";
            hdnrolName.Value = "";
        }
        else
        {
            lblError.Text = "* User Role already exists";
            hdnRoleid.Value = "0";
            hdnrolName.Value = "";
        }
        Permisison.Visible = false;
        lblName.Text = "* Select 'View Permission' to view/set the permissions for a role";
        divWarning.Visible = true;
        ResetColor();
    }
    protected void rpRoles_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        ResetColor();
        if (e.CommandName == "Delete")
        {
            SqlCommand cmd = new SqlCommand("sp_delete_brand_userrole");
            cmd.Parameters.AddWithValue("@role_id", e.CommandArgument);
            ConnObj.ExecuteNonQuery(cmd);
            FillUserRole();
            hdnRoleid.Value = "0";
            hdnrolName.Value = "";
            Permisison.Visible = false;
            lblName.Text = "* Select 'View Permission' to view/set the permissions for a role";
            divWarning.Visible = true;
        }
        if (e.CommandName == "Permission")
        {
            string[] val = Convert.ToString(e.CommandArgument).Split(',');
            hdnRoleid.Value = Convert.ToString(val[0]);
             hdnrolName.Value = Convert.ToString(val[1]);
            HtmlGenericControl info = e.Item.FindControl("info") as HtmlGenericControl;
            info.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#bce8f1");
            lblName.Text = "";
            Permisison.Visible = true;
            divWarning.Visible = false;
            FillPermission();
            setSpaccing();
        }
        txtRole.Text = "";
        lblError.Text = "";
    }

    protected void ResetColor()
    {
        foreach (RepeaterItem rp in rpRoles.Items)
        {
            HtmlGenericControl info = rp.FindControl("info") as HtmlGenericControl;
            info.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#fff");
        }

    }
    protected void FillBrandPanel()
    {
        SqlCommand cmd = new SqlCommand("sp_select_brand_panel");
        ConnObj.GetDataTab(cmd);
        chkPermission.DataSource = ConnObj.DataTab;
        chkPermission.DataTextField = "panel_name";
        chkPermission.DataValueField = "panel_id";
        chkPermission.DataBind();
        setSpaccing();
    }

    protected void FillPermission()
    {
        chkPermission.Items.Cast<ListItem>().Select(n => n).ToList().ForEach(n => n.Selected = false);
        SqlCommand cmd = new SqlCommand("sp_select_brand_userpermission");
        cmd.Parameters.AddWithValue("@role_id", hdnRoleid.Value);
        ConnObj.GetDataSet(cmd);
        List<int> list = ConnObj.DataSet.Tables[0].AsEnumerable().Select(dr => dr.Field<int>("panel_id")).ToList();
        chkPermission.Items.Cast<ListItem>().Where(n => list.Contains(Convert.ToInt32(n.Value))).Select(n => n).ToList().ForEach(n => n.Selected = true);
        setSpaccing();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (hdnRoleid.Value != "0")
        {
            SqlCommand cmd = new SqlCommand("sp_insert_brand_UserPermission");
            cmd.Parameters.AddWithValue("@role_id", hdnRoleid.Value);
            cmd.Parameters.AddWithValue("@panel_id", String.Join(",", (chkPermission.Items.Cast<ListItem>().Where(li => li.Selected).ToList()).Select(v => v.Value).ToList()));
            ConnObj.ExecuteNonQuery(cmd);
            if (ConnObj.IsSuccess)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage",
    "alert('Permission applied.');", true);
            }
        }
        else
        {
            lblName.Text = "* Select 'View Permission' to view/set the permissions for a role";
            divWarning.Visible = true;
        }
        setSpaccing();

    }
    protected void chkPermission_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Inedex
        string value = string.Empty;
        string result = Request.Form["__EVENTTARGET"];
        string[] checkedBox = result.Split('$'); ;
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);
        //Index

        SqlCommand cmd = new SqlCommand("sp_select_brand_panelChildList"); //Check for menu Level
        cmd.Parameters.AddWithValue("@panel_id", chkPermission.Items[index].Value);
        cmd.Parameters.AddWithValue("@panel_name", chkPermission.Items[index].Text);
        ConnObj.GetDataTab(cmd);
        if (ConnObj.IsSuccess && ConnObj.DataTab.Rows.Count > 0 && chkPermission.Items[index].Selected)
        {
            foreach (DataRow dr in ConnObj.DataTab.Rows)
            {
                chkPermission.Items.Cast<ListItem>().Where(n => Convert.ToString(dr["panel_name"]).Equals(Convert.ToString(n.Text))).Select(n => n).ToList().ForEach(n => n.Selected = true);
                chkPermission.Items.Cast<ListItem>().Where(n => Convert.ToString(dr["mainparent_name"]).Equals(Convert.ToString(n.Text))).Select(n => n).ToList().ForEach(n => n.Selected = true);
                chkPermission.Items.Cast<ListItem>().Where(n => Convert.ToString(dr["parent_name"]).Equals(Convert.ToString(n.Text))).Select(n => n).ToList().ForEach(n => n.Selected = true);
            }
        }
        else
        {
            foreach (DataRow dr in ConnObj.DataTab.Rows)
            {
                chkPermission.Items.Cast<ListItem>().Where(n => Convert.ToString(dr["panel_name"]).Equals(Convert.ToString(n.Text))).Select(n => n).ToList().ForEach(n => n.Selected = false);
                //chkPermission.Items.Cast<ListItem>().Where(n => Convert.ToString(dr["mainparent_name"]).Equals(Convert.ToString(n.Text))).Select(n => n).ToList().ForEach(n => n.Selected = false);
                //chkPermission.Items.Cast<ListItem>().Where(n => Convert.ToString(dr["parent_name"]).Equals(Convert.ToString(n.Text))).Select(n => n).ToList().ForEach(n => n.Selected = false);
            }
        }
        setSpaccing();
    }
    protected void setSpaccing()
    {
        foreach (ListItem itm in chkPermission.Items)
        {
            SqlCommand cmd1 = new SqlCommand("sp_select_brand_panelid"); //Check for menu Level
            cmd1.Parameters.AddWithValue("@panel_id", Convert.ToInt64(itm.Value));
            ConnObj.GetDataTab(cmd1);
            if (ConnObj.IsSuccess && Convert.ToString(ConnObj.DataTab.Rows[0]["menu_level"]) == "1")
            {
                itm.Attributes.Add("style", "margin-left:0px;");
            }
            if (ConnObj.IsSuccess && Convert.ToString(ConnObj.DataTab.Rows[0]["menu_level"]) == "2")
            {
                itm.Attributes.Add("style", "margin-left:40px;");
            }
            if (ConnObj.IsSuccess && Convert.ToString(ConnObj.DataTab.Rows[0]["menu_level"]) == "3")
            {
                itm.Attributes.Add("style", "margin-left:80px;");
            }
        }
    }

}