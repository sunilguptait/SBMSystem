using BMS.Data.Entities;
using BMS.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace BMS.Services.EmailSender
{
    public partial class EmailSenderService : IEmailSenderService
    {
        ICommonService _commonService;
        public EmailSenderService(ICommonService commonService)
        {
            _commonService = commonService;
        }
        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="fromAddress">From address</param>
        /// <param name="fromName">From display name</param>
        /// <param name="toAddress">To address</param>
        /// <param name="toName">To display name</param>
        /// <param name="bcc">BCC addresses list</param>
        /// <param name="cc">CC addresses ist</param>
        public void SendEmail(string subject, string body, string toAddress, string toName,
            IEnumerable<string> bcc = null, IEnumerable<string> cc = null, IEnumerable<string> attachments = null)
        {
            SendEmail(subject, body,
                 new MailAddress(toAddress, toName),
                bcc, cc, attachments);
        }

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="from">From address</param>
        /// <param name="to">To address</param>
        /// <param name="bcc">BCC addresses list</param>
        /// <param name="cc">CC addresses ist</param>
        public virtual void SendEmail(string subject, string body, MailAddress to,
            IEnumerable<string> bcc = null, IEnumerable<string> cc = null, IEnumerable<string> attachments = null)
        {
            var emailAccount = _commonService.GetEmailAccount();
            if (emailAccount != null)
            {
                var from = new MailAddress(emailAccount.Email, emailAccount.DisplayName);
                var message = new MailMessage();
                message.From = from;
                message.To.Add(to);
                if (null != bcc)
                {
                    foreach (var address in bcc.Where(bccValue => !String.IsNullOrWhiteSpace(bccValue)))
                    {
                        message.Bcc.Add(address.Trim());
                    }
                }
                if (null != cc)
                {
                    foreach (var address in cc.Where(ccValue => !String.IsNullOrWhiteSpace(ccValue)))
                    {
                        message.CC.Add(address.Trim());
                    }
                }
                if (null != attachments)
                {
                    foreach (string file in attachments)
                    {
                        if (file != null)
                        {
                            if (!String.IsNullOrEmpty(file.Trim()))
                            {
                                string[] arrfile = file.Split('$');
                                if (arrfile.Length > 1)
                                {
                                    var attachment = new Attachment(arrfile[0], arrfile[1]);
                                    message.Attachments.Add(attachment);
                                }
                            }
                        }
                    }
                }
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.UseDefaultCredentials = emailAccount.UseDefaultCredentials;
                    smtpClient.Host = emailAccount.Host;
                    smtpClient.Port = emailAccount.Port;
                    smtpClient.EnableSsl = emailAccount.EnableSsl;
                    if (emailAccount.UseDefaultCredentials)
                        smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                    else
                        smtpClient.Credentials = new NetworkCredential(emailAccount.Username, emailAccount.Password);
                    smtpClient.Send(message);
                }
            }
        }
    }
}
