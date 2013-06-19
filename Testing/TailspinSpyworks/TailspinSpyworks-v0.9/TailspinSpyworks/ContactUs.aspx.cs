﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace TailspinSpyworks
{
    public partial class ContactUs : System.Web.UI.Page
    {
        protected void ImageButton_Submit_Click(object sender, ImageClickEventArgs e)
        {
            try 
            {
                MailMessage mMailMessage = new MailMessage();

                mMailMessage.From = new MailAddress(HttpUtility.HtmlEncode(TextBoxEmail.Text));
                mMailMessage.To.Add(new MailAddress("Joe@Stagner.net")); 

                // mMailMessage.Bcc.Add(new MailAddress(bcc));
                // mMailMessage.CC.Add(new MailAddress(cc));

                mMailMessage.Subject = "From:" + HttpUtility.HtmlEncode(TextBoxYourName.Text) + "-" + HttpUtility.HtmlEncode(TextBoxSubject.Text);
                mMailMessage.Body = HttpUtility.HtmlEncode(EditorEmailMessageBody.Content); 
                mMailMessage.IsBodyHtml = true;
                mMailMessage.Priority = MailPriority.Normal;
                SmtpClient mSmtpClient = new SmtpClient();
                mSmtpClient.Send(mMailMessage);
                LabelMessage.Text = "Thank You - Your Message was sent.";
                
            }
            catch (Exception exp)
            {
                throw new Exception("ERROR: Unable to Send Contact - " + exp.Message.ToString(), exp);
            }
        
        }
    }
}
