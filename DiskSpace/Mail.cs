#region Using statements

using System;
using System.Net.Mail;
using DiskSpace.Properties;

#endregion

namespace DiskSpace
{
    /// <summary>
    /// Mail class
    /// </summary>
    internal static class Mail
    {
        #region Internal functions

        /// <summary>
        /// Send email over SMTP using settings
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        internal static bool Send(string subject, string body)
        {
            bool result = false;
            SmtpClient smtpClient = null;
            MailMessage mailMessage = null;

            try
            {
#pragma warning disable IDE0017 // Simplify object initialization
                smtpClient = new SmtpClient(Settings.Default.smtpServer);
#pragma warning restore IDE0017 // Simplify object initialization
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential(
                    Settings.Default.emailUserName,
                    Settings.Default.emailPassword);
                smtpClient.EnableSsl = Settings.Default.useSSL;
#pragma warning disable IDE0017 // Simplify object initialization
                mailMessage = new MailMessage();
#pragma warning restore IDE0017 // Simplify object initialization
                mailMessage.From = new MailAddress(Settings.Default.FromEmailAddress);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.To.Add(Settings.Default.ToEmailAddress);
                smtpClient.Send(mailMessage);
                Log.Info = "Mail sent from " + Settings.Default.FromEmailAddress + " to " +
                           Settings.Default.ToEmailAddress;
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
    }
}