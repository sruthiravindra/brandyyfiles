using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class brands_logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        SessionState._BrandyyAdmin = null;

        if (SessionState._IchooseITConnection != null)
        {
            SessionState._IchooseITConnection.Close();
            SessionState._IchooseITConnection.Dispose();
        }

        Response.Redirect(SessionState.WebsiteURLAdmin);
    }
}
