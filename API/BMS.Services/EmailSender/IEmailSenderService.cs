using BMS.Data.Entities;
using System.Collections.Generic;
using System.Net.Mail;

namespace BMS.Services.EmailSender
{
    public partial interface IEmailSenderService
    {
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
        void SendEmail(string subject, string body, string toAddress, string toName,
            IEnumerable<string> bcc = null, IEnumerable<string> cc = null, IEnumerable<string> attachments = null);

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="from">From address</param>
        /// <param name="to">To address</param>
        /// <param name="bcc">BCC addresses list</param>
        /// <param name="cc">CC addresses ist</param>
        void SendEmail(string subject, string body,MailAddress to,
            IEnumerable<string> bcc = null, IEnumerable<string> cc = null, IEnumerable<string> attachments = null);
    }
}
