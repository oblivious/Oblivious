using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Text.RegularExpressions;

namespace EmailTesting
{
    /// <summary>
    /// Sending email via smtp using the .Net classes
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Email test program:");
            try
            {
                string toAddress = "dobyrne@ezetop.com; <Stephen Walker> swalker@ezetop.com".Replace(';', ',');

                toAddress = Regex.Replace(toAddress, @"<[^>]+?>", string.Empty);

                using (MailMessage message = new MailMessage("integrations-noreply@ezetop.com", toAddress))
                {
                    message.BodyEncoding = Encoding.UTF8;
                    message.To.Add(new MailAddress("dobyrne@ezetop.com"));
                    message.IsBodyHtml = true;
                    message.Attachments.Add(new Attachment(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources", "footer.png")));
                    message.Attachments[0].ContentId = "footer";
                    message.Subject = "Smtp mail test...";

                    message.Body = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd""><html xmlns:v=""urn:schemas-microsoft-com:vml"" xmlns:o=""urn:schemas-microsoft-com:office:office"" xmlns:w=""urn:schemas-microsoft-com:office:word"" xmlns:m=""http://schemas.microsoft.com/office/2004/12/omml"" xmlns=""http://www.w3.org/TR/REC-html40""><head><meta http-equiv=""Content-Type"" content=""text/html; charset=us-ascii""><meta name=""Generator"" content=""Microsoft Word 12 (filtered medium)""><!--[if !mso]><style>v\:* {behavior:url(#default#VML);} o\:* {behavior:url(#default#VML);} w\:* {behavior:url(#default#VML);} .shape {behavior:url(#default#VML);} </style><![endif]--><style><!--/* Font Definitions */@font-face	{font-family:Calibri; panose-1:2 15 5 2 2 2 4 3 2 4;} @font-face {font-family:Tahoma; panose-1:2 11 6 4 3 5 4 4 2 4;} /* Style Definitions */ p.MsoNormal, li.MsoNormal, div.MsoNormal {margin:0cm; margin-bottom:.0001pt; font-size:11.0pt; font-family:""Calibri"",""sans-serif"";} p.MsoListParagraph, li.MsoListParagraph, div.MsoListParagraph {mso-style-priority:34; margin-top:0cm; margin-right:0cm; margin-bottom:0cm; margin-left:36.0pt; margin-bottom:.0001pt; font-size:11.0pt; font-family:""Calibri"",""sans-serif"";} a:link, span.MsoHyperlink {mso-style-priority:99; color:blue; text-decoration:underline;} a:visited, span.MsoHyperlinkFollowed {mso-style-priority:99; color:purple; text-decoration:underline;} @page WordSection1 {size:612.0pt 792.0pt; margin:72.0pt 72.0pt 72.0pt 72.0pt;} div.WordSection1 {page:WordSection1;} /* List Definitions */ @list l0 {mso-list-id:1435049563; mso-list-type:hybrid; mso-list-template-ids:2117487192 403243009 403243011 403243013 403243009 403243011 403243013 403243009 403243011 403243013;} @list l0:level1 {mso-level-number-format:bullet; mso-level-text:\F0B7; mso-level-tab-stop:none; mso-level-number-position:left; text-indent:-18.0pt; font-family:Symbol;} @list l1 {mso-list-id:1507792911; mso-list-type:hybrid; mso-list-template-ids:1567228508 403243009 403243011 403243013 403243009 403243011 403243013 403243009 403243011 403243013;} @list l1:level1 {mso-level-number-format:bullet; mso-level-text:\F0B7; mso-level-tab-stop:none; mso-level-number-position:left; text-indent:-18.0pt; font-family:Symbol;} ol {margin-bottom:0cm;} ul {margin-bottom:0cm;}--></style><!--[if gte mso 9]><xml><o:shapedefaults v:ext=""edit"" spidmax=""2050"" /></xml><![endif]--><!--[if gte mso 9]><xml><o:shapelayout v:ext=""edit""><o:idmap v:ext=""edit"" data=""1"" /></o:shapelayout></xml><![endif]--></head><body lang=""EN"" link=""blue"" vlink=""purple""><div class=""WordSection1""><p class=""MsoNormal"">Hi Dude,<o:p></o:p></p><p class=""MsoNormal""><o:p>&nbsp;</o:p></p><p class=""MsoNormal"">For any questions or concerns you may have, please contact your ezetop Account Manager.<o:p></o:p></p><br/><p class=""MsoNormal"">Regards,<o:p></o:p></p><br/><p class=""MsoNormal""><b><span lang=""CS"" style='font-size: 10.0pt; font-family: ""Arial"",""sans-serif""'>Integrations<o:p></o:p></span></b></p><p class=""MsoNormal""><i><span lang=""CS"" style='font-size: 10.0pt; font-family: ""Arial"",""sans-serif""'>ezetop</span></i><span lang=""CS"" style='font-size: 10.0pt; font-family: ""Arial"",""sans-serif""'>,<br />Brooklawn House, Shelbourne Road,<br />Dublin 4, Ireland<br />Phone&nbsp;&nbsp;+353 1 630 6300<br />Web&nbsp;&nbsp;&nbsp;&nbsp;<span style='color: navy'> <a href=""http://www.ezetop.com/"" title=""blocked::http://www.ezetop.com/"">www.ezetop.com</a></span></span><span lang=""CS"" style='font-size: 1.0pt; font-family: ""Times New Roman"",""serif""; color: black; border: none windowtext 1.0pt; padding: 0cm; background: black; layout-grid-mode: line'></span><span style='font-size: 1.0pt; font-family: ""Times New Roman"",""serif""; color: black; border: none windowtext 1.0pt; padding: 0cm; background: black; layout-grid-mode: line'><o:p></o:p></span></p><p class=""MsoNormal""><span style='color: #1F497D'><img border=""0"" width=""624"" height=""66"" id=""Picture_x0020_23"" src=""cid:footer"" alt=""mail_footer.png""></span><span lang=""CS"" style='font-size: 10.0pt; font-family: ""Arial"",""sans-serif""; color: navy'><o:p></o:p></span></p><p class=""MsoNormal""><span style='font-size: 8.0pt; font-family: ""Arial"",""sans-serif""; color: #A6A6A6'>ezetop is a company incorporated in Ireland with registration number 422514.The information in this email is private &amp; confidential and is intended solely for the addressee only. If you are not the intended recipient, any disclosure, copying, distribution or any action taken or omitted to be taken in reliance on it, is prohibited and may be unlawful.</span><o:p></o:p></p><br/></div></body></html>";


                    using (SmtpClient client = new SmtpClient())
                    {
                        client.Host = "10.24.124.11"; //Server URL
                        client.Port = 25;
                        client.UseDefaultCredentials = false;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Credentials = new NetworkCredential("integrationsnoreply", "No!Reply!1");
                        client.Send(message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadKey();
        }
    }
}
