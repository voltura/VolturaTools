using System;
using System.Globalization;
using System.IO;
using System.Net;
using DiskSpace.Forms;
using DiskSpace.Properties;
using System.Windows.Forms;

namespace DiskSpace
{
    /// <summary>
    ///     Application update logic
    /// </summary>
    public static class Updater
    {
        /// <summary>
        ///     Check if application update is availble
        /// </summary>
        /// <returns>true|false</returns>
        public static bool UpdateAvailable()
        {
            var result = false;
            try
            {
                using (var webClient = new WebClient())
                {
                    var versionFileUri = new Uri(Resources.CurrentVersionFileBaseUrl +
                                                 Resources.CurrentVersionFileName);
                    var localFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        Resources.CurrentVersionFileName);
                    webClient.DownloadFile(versionFileUri, localFile);
                    var infoDocument = File.ReadAllText(localFile);
                    var currentVer = Application.ProductVersion;
                    var version = infoDocument.Substring(
                        infoDocument.LastIndexOf(
                            Resources.AssemblyVersion, StringComparison.Ordinal)
                    ).Substring(17, currentVer.Length);
                    result = currentVer != version;
                    if (!int.TryParse(version.Replace(".", ""),
                        NumberStyles.Integer, new NumberFormatInfo(), out int _))
                    {
                        Log.ErrorString = "Could not retrieve version online.";
                        MessageForm.DisplayMessage("Could not retrieve version online.");
                    }
                    else
                    {
                        MessageForm.LogAndDisplayMessage(result
                            ? "New version available! Version " + version
                            : "Latest version already installed, version " + currentVer);
                    }
                }
            }
            catch (WebException webException)
            {
                Log.Error = webException;
                MessageForm.DisplayMessage("Could not check version online, see log for details.");
            }
            catch (IndexOutOfRangeException indexOutOfRangeException)
            {
                Log.Error = indexOutOfRangeException;
                MessageForm.LogAndDisplayMessage("Could not check version online, invalid content.");
            }
            catch (Exception e)
            {
                Log.Error = e;
                MessageForm.DisplayMessage("Could not check version online, fatal error. Restart application.");
                throw;
            }
            return result;
        }
    }
}