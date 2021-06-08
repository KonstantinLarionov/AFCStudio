using System;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace AFCStudio.Models.Helpers
{
    public class Letters
    {
        public static async Task Send(string email, string subject, string content )
            {
            /*
            string letter = null;
            switch (op)
            {
                case Forms.Registration:
                    letter = ;
                    break;
                case Forms.CreatingOrder:
                    letter = CreatingOrder();
                    break;
                case Forms.UnreadMessages:
                    letter = UnreadMessages();
                    break;
                case Forms.AssigningToManager:
                    letter = AssigningToManager();
                    break;
                case Forms.ChangingStatus:
                    letter = ChangingStatus();
                    break;
                case Forms.InvoiceIssued:
                    letter = InvoiceIssued();
                    break;
            }
            */

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("AFCStudio", "afc.studio@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = CreateLatter(subject,content)
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await client.ConnectAsync("smtp.yandex.ru", 587, false);
                await client.AuthenticateAsync("afc.studio@yandex.ru", "lollipop321123");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }

        public static string CreateLatter(string subject, string content) 
        {
            string letter= String.Format("<!DOCTYPE html> <html> <head> <link rel=\"stylesheet\" type=\"text / css\"> </head> <body style=\"margin: 0; padding: 0; \"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100 % \"> <tr><td style=\"background - color:#000000; height: 5vh;\" ></td></tr> </table> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr><td style=\"background-color:#ffffff; height: 100%; width: 50%; padding: 20px 10px 0px 0px;\" align=\"right\"> <img src=\"/spacehome/assets/images/Mlogo.png/\" width=\"100px\" height=\"100px\"> </td> <td style=\"background-color:#ffffff; height: 100%; width: 50%; padding: 20px 0px 0px 10px;\"> <p><font size=\"4\" face=\"Quattrocento\">AFCSTUDIO</font></p> <p><font size=\"4\" face=\"Quattrocento\">DEV. STD.</font></p> </td> </tr> </table> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <td style=\"background-color:#ffffff; height: 100%; width: 100%; padding: 20px 0px 20px 0px;\" align=\"center\"> <p><font size=\"3\" face=\"Source Serif Pro\">Студия разработки основана в г. Оренбурге в 2015 году.</font></p> </td> </table><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100 % \"> <td style=\"background - color:#ffffff; height: 100%; width: 100%; padding: 20px 0px 20px 0px;\" align=\"center\"><p><font align=\"center\" size=\"4\" face=\"Source Serif Pro\"><strong> {0} </strong></font></p></td></table> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <td style=\"background-color:#ffffff; height: 100%; width: 100%; padding: 20px 40px 20px 40px;\" align=\"left\"> {1} </td> </table> <div style=\" position:absolute; bottom:0; width:100%;\"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" height=\"100%\" > <td style=\"background-color:#000000; height: 100%; width: 100%; padding: 20px 40px 20px 40px; \" align=\"left\"> <p><font size=\"2\" color=\"white\" face=\"Source Serif Pro\">tel. 8 (903) 367-50-03 | *.net, *.ru, *.com</font></p> <p><font size=\"2\" color=\"white\" face=\"Source Serif Pro\" style=\"text-decoration: none; color: white;\">afc.studio@yandex.ru</font></p> <p><font size=\"2\" color=\"white\" face=\"Source Serif Pro\" style=\"text-decoration: none; color: white;\">www.afcstudio.ru</font></p> </td> </table> </div> </body> </html>", subject, content);
            return letter;
        }
    }
}
