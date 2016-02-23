using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web.Mail;
using System.Diagnostics;
using System.IO;

/// <summary>
/// USAGE:
/// EmailUtil eu = new EmailUtil(); 
/// eu.HtmlFilePath=@"c:\temp\file.htm";
/// eu.Subject ="test message";
/// eu.SmtpServer="mail.myserver.com";
/// eu.FromEmail="you@yourserver.com";
/// (loop here through your datatable etc. of email recipients ---)
/// eu.SendEmailAsync("daddy longlegs", "dlonglegs@yahoo.com");
/// eu.SendEmailAsync("Mama Bear", "mamabear@mindless.com");
/// eu.SendEmailAsync("Bill Clinton", "billy@clinton.org");
/// eu.SendEmailAsync("Jack Spratt", "sprattsksy@yayhey.com");
/// </summary>
public class Email
{
    public static void send(String[] emails, String subject, String content)
    {
        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(System.Configuration.ConfigurationManager.AppSettings["smtp.host"]);
        client.SendCompleted += new System.Net.Mail.SendCompletedEventHandler(client_SendCompleted);

        foreach (String email in emails)
        {
            try
            {
                client.SendAsync(System.Configuration.ConfigurationManager.AppSettings["from.email"], email, subject, content, null);
            }
            catch (System.Web.HttpException ehttp)
            {
            }
        }
    }

    static void client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        //Do Something if we want
    }
} // end class EmailUtil 
