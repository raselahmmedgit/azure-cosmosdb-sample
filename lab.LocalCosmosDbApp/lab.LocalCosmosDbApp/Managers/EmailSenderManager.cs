using lab.LocalCosmosDbApp.Config;
using lab.LocalCosmosDbApp.Helpers;
using lab.LocalCosmosDbApp.Models;
using lab.LocalCosmosDbApp.Utility;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.Managers
{
    public class EmailSenderManager : IEmailSenderManager
    {
        private readonly AppEmailConfig _appEmailConfig;
        private readonly AppContactUsConfig _appContactUsConfig;

        public EmailSenderManager(IOptions<AppEmailConfig> appEmailConfig, IOptions<AppContactUsConfig> appContactUsConfig)
        {
            _appEmailConfig = appEmailConfig.Value;
            _appContactUsConfig = appContactUsConfig.Value;
        }

        public async Task<EmailSentResult> ContactSendEmailToAdmin(ContactUsModel contactUsModel)
        {
            try
            {
                string emailTitle = $"App - Contact Us - {contactUsModel.ContactSubject}";
                string emailSubject = emailTitle;
                string emailTemplate = EmailTemplateHelper.GetContactSendMessageEmailTemplate(emailTitle, contactUsModel: contactUsModel);

                string emailAddressDisplayName = _appContactUsConfig.EmailAddressDisplayName;
                string emailAddress = contactUsModel.RecipientEmail;

                EmailMessage emailMessage = new EmailMessage();
                emailMessage.ReceiverName = emailAddressDisplayName;
                emailMessage.ReceiverEmail = emailAddress;
                emailMessage.Subject = emailSubject;
                emailMessage.IsHtml = true;
                emailMessage.Body = emailTemplate;

                var emailSentResult = await SendEmailMessage(emailMessage, "contactUsUser");

                if (!string.IsNullOrEmpty(_appEmailConfig.TestEmailAddress))
                {
                    string[] testEmailAddressList = _appEmailConfig.TestEmailAddress.Split(",");
                    foreach (var testEmailAddress in testEmailAddressList)
                    {
                        string[] testEmailAddressReceiverEmailAndNameList = testEmailAddress.Split("_");
                        EmailMessage testEmailMessage = new EmailMessage();
                        testEmailMessage.ReceiverName = testEmailAddressReceiverEmailAndNameList[0].ToString();
                        testEmailMessage.ReceiverEmail = testEmailAddressReceiverEmailAndNameList[1].ToString();
                        testEmailMessage.Subject = emailSubject;
                        testEmailMessage.IsHtml = true;
                        testEmailMessage.Body = emailTemplate;

                        var testEmailSentResult = await SendEmailMessage(testEmailMessage, "contactUsUser");
                    }
                }

                return emailSentResult;
            }
            catch (Exception ex)
            {
                return new EmailSentResult { Success = false, Ex = ex };
            }
        }
        public async Task<EmailSentResult> SendEmailMessage(EmailMessage emailMessage, string userId)
        {

            EmailSentResult emailSentResult = new EmailSentResult() { Success = false };
            try
            {
                var smtp = new SmtpClient
                {
                    Host = _appEmailConfig.Host,
                    Port = Convert.ToInt32(_appEmailConfig.Port),
                    EnableSsl = Convert.ToBoolean(_appEmailConfig.Ssl),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_appEmailConfig.FromEmailAddress, _appEmailConfig.Password)
                };
                using (var smtpMessage = new MailMessage(new MailAddress(_appEmailConfig.FromEmailAddress, _appEmailConfig.FromEmailAddressDisplayName)
                    , new MailAddress(emailMessage.ReceiverEmail, emailMessage.ReceiverName)))
                {
                    smtpMessage.Subject = emailMessage.Subject;
                    smtpMessage.Body = emailMessage.Body;
                    smtpMessage.IsBodyHtml = emailMessage.IsHtml;
                    smtpMessage.Priority = MailPriority.Normal;
                    smtpMessage.SubjectEncoding = Encoding.UTF8;
                    smtpMessage.BodyEncoding = Encoding.UTF8;
                    smtpMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

                    smtp.Send(smtpMessage);

                    emailSentResult.Success = true;
                    emailSentResult.Id = userId;

                    smtp.Dispose();
                }
            }
            catch (Exception ex)
            {
                emailSentResult.Ex = ex;
            }
            await Task.FromResult(0);
            return emailSentResult;
        }
    }

    public interface IEmailSenderManager
    {
        Task<EmailSentResult> ContactSendEmailToAdmin(ContactUsModel contactSendMessage);
        Task<EmailSentResult> SendEmailMessage(EmailMessage emailMessage, string userId);
    }
}
