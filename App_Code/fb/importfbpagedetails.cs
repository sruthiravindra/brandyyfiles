using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Facebook;
/// <summary>
/// Summary description for importfbpagedetails
/// </summary>
public class importfbpagedetails
{
    public string page_id;
    public string page_tag;
	public importfbpagedetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string getPageDetails(string pagename)
    {
        var client = new FacebookClient(System.Configuration.ConfigurationManager.AppSettings["FB_access_token"]);

        try
        {
            dynamic posts = client.Get("/" + pagename);

            if (posts != null)
            {
                page_id = posts["id"];
                page_tag = posts["username"]; 
                return posts["id"];
            }
        }
        catch
        {
            return "";
        }

        
        return "";
    }
}