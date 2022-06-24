using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace EsbaBlazorAppAuth.Services
{
//https://stackoverflow.com/questions/32260/sending-email-in-net-through-gmail
    public class SmtpEmailSender : IEmailSender
    {
        private IConfiguration _configuration;
        private string _smtpServer;
        private int _smtpPort;

        public SmtpEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _smtpServer = _configuration.GetValue<string>("MailConfiguracion:Smtp");
            _smtpPort = _configuration.GetValue<int>("MailConfiguracion:Port");
        }

        /*public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var client = new SmtpClient(_smtpServer, _smtpPort)
                {
                    //                UseDefaultCredentials = false,
                    //                Credentials = new NetworkCredential("yourusername", "yourpassword")
                };
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("drouco@sig2k.com")
                };
                mailMessage.To.Add(email);
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = htmlMessage;

                await client.SendMailAsync(mailMessage);
            }
            catch (Exception e)
            {
                Console.Write(e);
                throw;
            }
        }*/

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var client = new SmtpClient(_smtpServer, _smtpPort)
                {
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential("rdj4634@gmail.com", "daiarwbrksphjrum")
                };
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("rdj4634@gmail.com")
                    //From = new MailAddress("drouco@sig2k.com")
                };
                mailMessage.To.Add(email);
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;

                htmlMessage = $@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">
                <HTML><HEAD><META http-equiv=Content-Type content=""text/html; charset=iso-8859-1"">
                </HEAD><BODY>{htmlMessage}</BODY></HTML>";

                AlternateView alterView = ContentToAlternateView(htmlMessage);
                mailMessage.AlternateViews.Add(alterView);

                await client.SendMailAsync(mailMessage);
            }
            catch (Exception e)
            {
                Console.Write(e);
                throw;
            }
        }

        private static AlternateView ContentToAlternateView(string content)
        {
            var imgCount = 0;
            List<LinkedResource> resourceCollection = new List<LinkedResource>();
            foreach (Match m in Regex.Matches(content, "<img(?<value>.*?)>"))
            {
                imgCount++;
                var imgContent = m.Groups["value"].Value;
                imgContent = "src=\"" + Regex.Match(imgContent, "src=\"(?<base_64>.*?)\"").Groups["base_64"].Value + "\"";
                string type = Regex.Match(imgContent, ":(?<type>.*?);base64,").Groups["type"].Value;

                string base64 = Regex.Match(imgContent, "base64,(?<base64>.*?)\"").Groups["base64"].Value;
                if (String.IsNullOrEmpty(type) || String.IsNullOrEmpty(base64))
                {
                    //ignore replacement when match normal <img> tag
                    continue;
                }
                var replacement = " src=\"cid:" + imgCount + "\"";
                if (content.IndexOf(imgContent) >= 0)
                {
                    content = content.Replace(imgContent, replacement);
                    var tempResource = new LinkedResource(Base64ToImageStream(base64), new ContentType(type))
                    {
                        ContentId = imgCount.ToString()
                    };
                    resourceCollection.Add(tempResource);
                }
            }
            //AlternateView alternateView = AlternateView.CreateAlternateViewFromString(content, System.Text.Encoding.UTF8, MediaTypeNames.Text.Html);
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(content, new ContentType("text/html"));
            foreach (var item in resourceCollection)
            {
                alternateView.LinkedResources.Add(item);
            }

            return alternateView;
        }

        private static Stream Base64ToImageStream(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            return ms;
        }
    }
}