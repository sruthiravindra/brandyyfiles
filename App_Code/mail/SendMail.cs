using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Net.Mail;

public class MailSendFields
{
    public string TemplatePath = "";
    public string FinaltemplateStr = "";
    public string Subject = "";
    public string TTo = "";
    public string FFrom = "";
    public string CC = "";
    public string Bcc = "";
    public bool IsSuccess = true;
    public string Message = "";

    public MailSendFields()
    {
        this.TemplatePath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Template"]);
        this.FFrom = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["From"]);
        this.Subject = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Subject"]);
        this.CC = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["CC"]);
        this.Bcc = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["BCC"]);
    }
}

/// <summary>
/// Summary description for SendMail
/// </summary>
public class SendMail : MailSendFields
{
    public SendMail()
    {
        ;
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