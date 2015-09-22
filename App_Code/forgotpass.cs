using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;

/// <summary>
/// Summary description for IntrestedProperty
/// </summary>
public class forgotpass : MailSendFields
{

    public string email = "";
    public string pass = "";
    public string[] StrTTo = null;
    public static ConnectionClass ConnObj = null;
    public class EnquiryTemplates : forgotpass
    {
        public EnquiryTemplates( string email, string pass, string TemplatePath)
        {
            try
            {
                this.Subject = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Subject"]);
                this.TemplatePath = TemplatePath;
                this.TTo = email;
                this.FFrom = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["From"]);              
                this.CC = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["CC"]);
                this.Bcc = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["BCC"]);
                this.email = email;
                this.pass = pass;
                GetIntresterEnquiryTemplate();
                SendEMail();
            }
            catch (Exception ex)
            {

            }
        }
        public void GetIntresterEnquiryTemplate()
        {
            try
            {
                this.IsSuccess = false;
                using (StreamReader sr = new StreamReader(TemplatePath))
                {
                    FinaltemplateStr = sr.ReadToEnd();
                    sr.Close();
                    FinaltemplateStr = FinaltemplateStr.Replace("@email@", this.email);
                    FinaltemplateStr = FinaltemplateStr.Replace("@pass@", this.pass);
                    this.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Message = ex.ToString();
            }
        }

        public void SendEMail()
        {
            IsSuccess = false;
            Message = string.Empty;
            MailMessage mm = new MailMessage(FFrom, TTo);
            mm.Subject = Subject;
            mm.Body = FinaltemplateStr;
            mm.IsBodyHtml = true;
            mm.Bcc.Add(Bcc);
            mm.CC.Add(this.CC);
            SmtpClient smtp = new SmtpClient();
            try
            {
                smtp.Timeout = 20000;
                smtp.Send(mm);
                IsSuccess = true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }    

    }
}
