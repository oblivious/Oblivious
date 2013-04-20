using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace MailExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            MailMessage m = new MailMessage("jane@contoso.com", "ben@contoso.com", "Blah blah blah", "Blah blah body.");
            MailMessage n = new MailMessage(new MailAddress("lance@contoso.com", "Lance Tucker"),
                new MailAddress("ben@contoso.com", "Ben Miller"));

            // Bored now because I've done all this before.

            m.Attachments.Add(new Attachment(@"C:\windows\win.ini"));

            Stream sr = new FileStream(@"C:\Attachment.txt", FileMode.Open, FileAccess.Read);
            m.Attachments.Add(new Attachment(sr, "myfile.txt", MediaTypeNames.Application.Octet));

            string htmlBody = "<html><body><h1>MyMessage</h1|><br>This is an HTML message.<img src=\"cid:Pic1\"></body></html>";
            //m.IsBodyHtml = true;

            AlternateView avHtml = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

            LinkedResource pic1 = new LinkedResource("pic.jpg", MediaTypeNames.Image.Jpeg);
            pic1.ContentId = "Pic1";
            avHtml.LinkedResources.Add(pic1);

            string textBody = "You must use an e-mail client that supports HTML messages";
            AlternateView avText = AlternateView.CreateAlternateViewFromString(textBody, null, MediaTypeNames.Text.Plain);

            m.AlternateViews.Add(avHtml);
            m.AlternateViews.Add(avText);

            SmtpClient client = new SmtpClient("smtp.contoso.com");
            client.Send(m);


        }
    }
}
