using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Canada2DCode.Models;
using System.Text.RegularExpressions;
using System.Text;
using System;

namespace Canada2DCode.Controllers.API
{
    [RoutePrefix("api/email")]
    public class EmailController : ApiController
    {
        [HttpPost]
        [Route("send-email")]
        public void SendEmail(EmailModel objData)
        {

            int port = 587;
            string smtpServer = "mail.canada2d.com";
            string smtpUserName = "contact@canada2d.com";
            string smtpUserPass = "Canada2d123";

            try
            {

                using (SmtpClient smtpSend = new SmtpClient())
                {


                    MailMessage messagea = new MailMessage();
                    messagea.From = new MailAddress("contact@canada2d.com");

                    messagea.To.Add(new MailAddress("info@canada2d.com"));

                    messagea.Subject = objData.subject; 
                    messagea.Body = objData.toname + " <" + objData.toemail + "> : " + objData.message; 

                    SmtpClient client = new SmtpClient();
                    client.Send(messagea);


                    smtpSend.Host = smtpServer;
                    smtpSend.Port = port;

                    smtpSend.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpUserPass);

                    smtpSend.EnableSsl = false;

                    MailMessage emailMessage = new System.Net.Mail.MailMessage();

                    emailMessage.To.Add(new MailAddress("Admin Info" + " <" + "info@canada2d.com" + ">"));
                    emailMessage.From = new MailAddress("contact@canada2d.com <contact@canada2d.com>");
                    emailMessage.Subject = objData.subject;
                    emailMessage.Body = objData.toname + " <" + objData.toemail + "> : " + objData.message;

                    if (!Regex.IsMatch(emailMessage.Body, @"^([0-9a-z!@#\$\%\^&\*\(\)\-=_\+])", RegexOptions.IgnoreCase) ||
                            !Regex.IsMatch(emailMessage.Subject, @"^([0-9a-z!@#\$\%\^&\*\(\)\-=_\+])", RegexOptions.IgnoreCase))
                    {
                        emailMessage.BodyEncoding = Encoding.Unicode;
                    }

                    smtpSend.Send(emailMessage);
                }
  
            }

            catch (Exception e)
            {

                var sss = e.Message;


            }

        }

        private string createEmailBody(string userName, string message)
        {
            string body = string.Empty;

            string pathString = HttpContext.Current.Server.MapPath("~/");
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/htmlTemplate.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            body = body.Replace("{message}", message);
            return body;
        }
    }
}
