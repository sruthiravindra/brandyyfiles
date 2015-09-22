using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for VoucherMailSend
/// </summary>
public class VoucherMailSend : SendMail
{
    public ConnectionClass ConnObj = null;
    public string username = "";
    public string reward = "";
    public string couponnum = "";
    public string terms = "";
    public VoucherMailSend(string To, string LocalSubject = "You Have received a voucher")
	{
        this.TTo = To;
        this.Subject = LocalSubject;
        this.TemplatePath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TemplateVoucherSend"]);
	}

    public bool SetActivityCoupon(Int64 activity_id, Int64 campaign_id)
    {
        #region set the coupon for the activity and get the email details for the campaign
        SqlCommand cmd = new SqlCommand("sp_Win_Voucher_Details");
        cmd.Parameters.AddWithValue("@activity_id", activity_id);
        cmd.Parameters.AddWithValue("@campaign_id", campaign_id);
        ConnObj.GetDataSet(cmd);
        #endregion

        if ((ConnObj.IsSuccess) && (ConnObj.DataSet.Tables.Count > 0) && (ConnObj.DataSet.Tables[0].Rows.Count > 0))
        {
            DataRow dr = ConnObj.DataSet.Tables[0].Rows[0];
            this.TTo = Convert.ToString(dr["reg_email"]);
            this.username = Convert.ToString(dr["reg_name"]);
            //this.reward = (reward_type == 1) ? Convert.ToString(dr["reward_amount"]) + " AED OFF" : Convert.ToString(dr["reward_percent"]) + " % OFF";
            this.reward = Convert.ToString(dr["reward_amount"]) + " BP ";
            this.couponnum = (Convert.ToByte(dr["coupon_source"]) != 3) ? Convert.ToString(dr["coupon_code"]) : "<a>click here to view coupon</a>";
            this.terms = System.Web.HttpContext.Current.Server.HtmlDecode(Convert.ToString(dr["terms"]));
        }
        else
        {
            return false;
        }

        return true;
    }

    public void ReadyTemplate()
    {
        try
        {
            IsSuccess = false;
            using (StreamReader sr = new StreamReader(TemplatePath))
            {
                FinaltemplateStr = sr.ReadToEnd();
                sr.Close();
                FinaltemplateStr = FinaltemplateStr.Replace("~~Val_UserName~~", username);
                FinaltemplateStr = FinaltemplateStr.Replace("~~Val_Reward~~", reward);
                FinaltemplateStr = FinaltemplateStr.Replace("~~Val_CouponNumber~~", couponnum);
                FinaltemplateStr = FinaltemplateStr.Replace("~~Val_Terms~~", terms);
                FinaltemplateStr = FinaltemplateStr.Replace("~~Val_AdminMailID~~", Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["AdminEmailID"]));
                FinaltemplateStr = FinaltemplateStr.Replace("~~Val_WebsiteUrl~~", Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["WebsiteURL"]));
                IsSuccess = true;
            }
        }
        catch (Exception ex)
        {
            Message = ex.ToString();
        }
    }
}