#region Using statements

using DiskSpace.Properties;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using System.Net.Mail;

#endregion

namespace DiskSpace
{
    /// <summary>
    ///     Mail class
    /// </summary>
    internal static class Mail
    {
        #region Internal functions

        /// <summary>
        ///     Send email over SMTP using settings
        /// </summary>
        /// <param name="subject">Subject text</param>
        /// <param name="body">Body text</param>
        /// <param name="settings">Settings class</param>
        /// <returns>Success</returns>
        [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
        internal static bool Send(string subject,
            string body,
            Settings settings)
        {
            bool result = false;
            SmtpClient smtpClient = null;
            MailMessage mailMessage = null;

            try
            {
                smtpClient = InitMailClientFromSettings(settings);
                mailMessage = CreateMail(subject, body, settings);
                smtpClient.Send(mailMessage);
                Log.Info = "Mail sent from " + settings.FromEmailAddress + " to " +
                           settings.ToEmailAddress;
                result = true;
            }
            catch (SmtpException ex)
            {
                Log.Error = ex;
            }
            catch (InvalidOperationException ex)
            {
                Log.Error = ex;
            }
            finally
            {
                mailMessage?.Dispose();
                smtpClient?.Dispose();
            }
            return result;
        }

        #endregion

        #region Static private functions

        private static MailMessage CreateMail(string subject, string body, Settings settings)
        {
            MailMessage mailMessage = null;
            try
            {
#pragma warning disable IDE0017 // Simplify object initialization
                // ReSharper disable once UseObjectOrCollectionInitializer
                mailMessage = new MailMessage();
#pragma warning restore IDE0017 // Simplify object initialization
                mailMessage.From = new MailAddress(settings.FromEmailAddress);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.To.Add(settings.ToEmailAddress);
            }
            catch (Exception)
            {
                mailMessage?.Dispose();
                throw;
            }
            return mailMessage;
        }

        private static SmtpClient InitMailClientFromSettings(Settings settings)
        {
            SmtpClient smtpClient = null;
            try
            {
#pragma warning disable IDE0017 // Simplify object initialization
                // ReSharper disable once UseObjectOrCollectionInitializer
                smtpClient = new SmtpClient(settings.smtpServer);
#pragma warning restore IDE0017 // Simplify object initialization
                smtpClient.Port = Convert.ToInt32(settings.smtpPort, CultureInfo.InvariantCulture);
                smtpClient.Credentials = new NetworkCredential(
                    settings.emailUserName,
                    settings.emailPassword);
                smtpClient.EnableSsl = settings.useSSL;
            }
            catch (Exception)
            {
                smtpClient?.Dispose();
                throw;
            }
            return smtpClient;
        }

        #endregion
    }
}