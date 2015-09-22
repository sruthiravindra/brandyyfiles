using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
/// <summary>
/// Summary description for ProjectInitUnloadCalling
/// </summary>
public class ProjectInitUnloadCalling
{
    public void Page_Init(string PageName="")
    {
        SessionState.WebsiteURL = System.Configuration.ConfigurationManager.AppSettings["WebsiteURL"];
        SessionState.WesiteImagesLoadURL = System.Configuration.ConfigurationManager.AppSettings["WesiteImagesLoadURL"];
        SessionState.WebsiteURLAdmin = System.Configuration.ConfigurationManager.AppSettings["WebsiteURLAdmin"];
    }
    public void Page_Unload()
    {
        if (SessionState._IchooseITConnection != null)
        {
            SessionState._IchooseITConnection.Close();
        }
       
    }
}