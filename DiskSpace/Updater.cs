using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Xml;
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
                    var version = infoDocument.Substring(
                        infoDocument.LastIndexOf(
                            Resources.AssemblyVersion, StringComparison.Ordinal)
                    ).Substring(69, 7);
                    using (var message = new MessageForm())
                    {
                        var currentVer = Application.ProductVersion;
                        result = currentVer != version;
                        message.SetMessage(result
                            ? "New version available! Version " + version
                            : "Latest version already installed, version " + currentVer);
                        message.ShowDialog();
                    }
                }
            }
            catch (IndexOutOfRangeException indexOutOfRangeException)
            {
                Log.Error = indexOutOfRangeException;
            }
            catch (Exception e)
            {
                Log.Error = e;
                throw;
            }
            return result;
        }
    }
}