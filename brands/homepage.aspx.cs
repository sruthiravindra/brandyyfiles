using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brands_homepage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SessionState._BrandAdmin.callCount = SessionState._BrandAdmin.callCount + 1;
    }
    [System.Web.Services.WebMethod(true)]
    public static string LoadCampaignView(Int64 campaign_id)
    {
        SessionState.EditId = campaign_id;
        SessionState._Campaign = new Campaign(SessionState.EditId, SessionState._BrandAdmin.brand_id);
        return SessionState.WebsiteURL + "brands/campaignview.aspx";
    }
    
}