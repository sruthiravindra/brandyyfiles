using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SessionState
/// </summary>
public class SessionState
{
    public static string WebsiteURL
    {
        get
        {
            if (HttpContext.Current.Session["WebsiteURL"] != null)
                return (string)(HttpContext.Current.Session["WebsiteURL"]);
            else
                return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["WebsiteURL"]);
        }
        set { HttpContext.Current.Session["WebsiteURL"] = value; }
    }
    public static string WesiteImagesLoadURL
    {
        get
        {
            if (HttpContext.Current.Session["WesiteImagesLoadURL"] != null)
                return (string)(HttpContext.Current.Session["WesiteImagesLoadURL"]);
            else
                return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["WesiteImagesLoadURL"]);
        }
        set { HttpContext.Current.Session["WesiteImagesLoadURL"] = value; }
    }
    public static string WebsiteURLAdmin
    {
        get
        {
            if (HttpContext.Current.Session["WebsiteURLAdmin"] != null)
                return (string)(HttpContext.Current.Session["WebsiteURLAdmin"]);
            else
                return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["WebsiteURLAdmin"]);
        }
        set { HttpContext.Current.Session["WebsiteURLAdmin"] = value; }
    }
    public static string WebsiteURLBrand
    {
        get
        {
            if (HttpContext.Current.Session["WebsiteURLBrand"] != null)
                return (string)(HttpContext.Current.Session["WebsiteURLBrand"]);
            else
                return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["WebsiteURLBrand"]);
        }
        set { HttpContext.Current.Session["WebsiteURLBrand"] = value; }
    }
    public static SignInUser _SignInUser
    {
        get
        {
            if (HttpContext.Current.Session["_SignInUser"] != null)
                return (SignInUser)(HttpContext.Current.Session["_SignInUser"]);
            else
                return null;
        }
        set { HttpContext.Current.Session["_SignInUser"] = value; }
    }
    public static BrandAdmin _BrandAdmin
    {
        get
        {
            if (HttpContext.Current.Session["_BrandAdmin"] != null)
                return (BrandAdmin)(HttpContext.Current.Session["_BrandAdmin"]);
            else
                return null;
        }
        set { HttpContext.Current.Session["_BrandAdmin"] = value; }
    }
    public static BrandyyAdmin _BrandyyAdmin
    {
        get
        {
            if (HttpContext.Current.Session["_BrandyyAdmin"] != null)
                return (BrandyyAdmin)(HttpContext.Current.Session["_BrandyyAdmin"]);
            else
                return null;
        }
        set { HttpContext.Current.Session["_BrandyyAdmin"] = value; }
    }
    public static SqlConnection _IchooseITConnection
    {
        get
        {
            if (HttpContext.Current.Session["_IchooseITConnection"] == null || (HttpContext.Current.Session["_IchooseITConnection"] != null && ((SqlConnection)(HttpContext.Current.Session["_IchooseITConnection"])).ConnectionString.Length == 0))
            {
                HttpContext.Current.Session["_IchooseITConnection"] = new SqlConnection(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["conn"]));
                return (SqlConnection)(HttpContext.Current.Session["_IchooseITConnection"]);
            }
            else
            {
                return (SqlConnection)(HttpContext.Current.Session["_IchooseITConnection"]);
            }
        }
        set { HttpContext.Current.Session["_IchooseITConnection"] = value; }
    }
    public static Int64 EditId
    {
        get
        {
            if (HttpContext.Current.Session["EditId"] != null)
                return (Int64)(HttpContext.Current.Session["EditId"]);
            else
                return 0;
        }
        set { HttpContext.Current.Session["EditId"] = value; }
    }
    public static Int64 EditId_2
    {
        get
        {
            if (HttpContext.Current.Session["EditId_2"] != null)
                return (Int64)(HttpContext.Current.Session["EditId_2"]);
            else
                return 0;
        }
        set { HttpContext.Current.Session["EditId_2"] = value; }
    }
    
    public static Int64 UserID
    {
        get
        {
            if (HttpContext.Current.Session["UserID"] != null)
                return (Int64)(HttpContext.Current.Session["UserID"]);
            else
                return 0;
        }
        set { HttpContext.Current.Session["UserID"] = value; }
    }
    public static Int64 ActivityID
    {
        get
        {
            if (HttpContext.Current.Session["ActivityID"] != null)
                return (Int64)(HttpContext.Current.Session["ActivityID"]);
            else
                return 0;
        }
        set { HttpContext.Current.Session["ActivityID"] = value; }
    }
    public static string TwitterAuthToken
    {
        get
        {
            if (HttpContext.Current.Session["TwitterAuthToken"] != null)
                return (string)(HttpContext.Current.Session["TwitterAuthToken"]);
            else
                return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TwitterAuthToken"]);
        }
        set { HttpContext.Current.Session["TwitterAuthToken"] = value; }
    }
    public static string TwitterAuthKey
    {
        get
        {
            if (HttpContext.Current.Session["TwitterAuthKey"] != null)
                return (string)(HttpContext.Current.Session["TwitterAuthKey"]);
            else
                return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TwitterAuthKey"]);
        }
        set { HttpContext.Current.Session["TwitterAuthKey"] = value; }
    }

    public static Campaign _Campaign
    {
        get
        {
            if (HttpContext.Current.Session["_Campaign"] != null)
                return (Campaign)(HttpContext.Current.Session["_Campaign"]);
            else
                return null;
        }
        set { HttpContext.Current.Session["_Campaign"] = value; }
    }

    public static UserActivity _UserActivity
    {
        get
        {
            if (HttpContext.Current.Session["_UserActivity"] != null)
                return (UserActivity)(HttpContext.Current.Session["_UserActivity"]);
            else
                return null;
        }
        set { HttpContext.Current.Session["_UserActivity"] = value; }
    }
    public static PaginationSession _PaginationSession
    {
        get
        {
            if (HttpContext.Current.Session["_PaginationSession"] != null)
                return (PaginationSession)(HttpContext.Current.Session["_PaginationSession"]);
            else
                return null;
        }
        set { HttpContext.Current.Session["_PaginationSession"] = value; }
    }
   
}