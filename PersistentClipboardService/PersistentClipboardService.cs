using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Windows;

namespace PersistentClipboard
{
    public partial class PersistentClipboardService : ServiceBase
    {
        private readonly string _localFolder, _persistantClipboardFile;
        private Thread _startThread, _endThread;

        public PersistentClipboardService()
        {
            InitializeComponent();
            _localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _persistantClipboardFile = Path.Combine(_localFolder, "PersistentClipboard.txt");
        }

        protected override void OnStart(string[] args)
        {
            SetTextToClipboardFromSTAAppartmentState();
        }

        protected override void OnStop()
        {
            GetTextFromClipboardFromSTAAppartmentState();
            _endThread.Join(new TimeSpan(0, 1, 0));

            #region Todo
            /*
            IDataObject theObject = System.Windows.Clipboard.GetDataObject();
            DataObject laObject = (DataObject) Clipboard.GetDataObject();
            string[] theFormats = theObject.GetFormats();
            foreach (string theFormat in theFormats)
            {
                object theData = laObject.GetData(theFormat);
                /*Bitmap	"Bitmap"	Specifies a Microsoft Windows bitmap data format.
CommaSeparatedValue	"CSV"	Specifies a comma-separated value (CSV) data format.
Dib	"DeviceIndependentBitmap"	Specifies the device-independent bitmap (DIB) data format.
Dif	"DataInterchangeFormat"	Specifies the Windows Data Interchange Format (DIF) data format.
EnhancedMetafile	"EnhancedMetafile"	Specifies the Windows enhanced metafile format.
FileDrop	"FileDrop"	Specifies the Windows file drop format.
Html	"HTML Format"	Specifies the HTML data format.
Locale	"Locale"	Specifies the Windows locale (culture) data format.
MetafilePicture	"MetaFilePict"	Specifies the Windows metafile picture data format.
OemText	"OEMText"	Specifies the standard Windows OEM text data format.
Palette	"Palette"	Specifies the Windows palette data format.
PenData	"PenData"	Specifies the Windows pen data format.
Riff	"RiffAudio"	Specifies the Resource Interchange File Format (RIFF) audio data format.
Rtf	"Rich Text Format"	Specifies the Rich Text Format (RTF) data format.
Serializable	"PersistentObject"	Specifies a data format that encapsulates any type of serializable data objects.
StringFormat	"System.String"	Specifies the common language runtime (CLR) string class data format.
SymbolicLink	"SymbolicLink"	Specifies the Windows symbolic link data format.
Text	"Text"	Specifies the ANSI text data format.
Tiff	"TaggedImageFileFormat"	Specifies the Tagged Image File Format (TIFF) data format.
UnicodeText	"UnicodeText"	Specifies the Unicode text data format.
WaveAudio	"WaveAudio"	Specifies the wave audio data format.
Xaml	"Xaml"	Specifies the Extensible Application Markup Language (XAML) data format.
XamlPackage	"XamlPackage"	Specifies the Extensible Application Markup Language (XAML) package data format.*/

            /* }*/
            #endregion Todo
        }

        internal void SetTextToClipboardFromSTAAppartmentState()
        {
            _startThread = new Thread(SetTextToClipboard);
            _startThread.SetApartmentState(ApartmentState.STA);
            _startThread.Start();
        }

        internal void SetTextToClipboard()
        {
            if (!File.Exists(_persistantClipboardFile))
            {
                return;
            }
            Clipboard.SetText(File.ReadAllText(_persistantClipboardFile));
            Thread.CurrentThread.Abort();
        }

        internal void GetTextFromClipboardFromSTAAppartmentState()
        {
            _endThread = new Thread(GetTextFromClipboard);
            _endThread.SetApartmentState(ApartmentState.STA);
            _endThread.Start();
        }

        internal void GetTextFromClipboard()
        {
            if (!Clipboard.ContainsText())
            {
                return;
            }
            string text = Clipboard.GetText();
            File.WriteAllText(_persistantClipboardFile, text);
            Thread.CurrentThread.Abort();
        }
    }
}
