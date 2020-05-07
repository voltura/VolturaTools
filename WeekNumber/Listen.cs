using System;
using System.Globalization;
using System.Speech.Recognition;

namespace WeekNumber
{
    internal class Listen : IDisposable
    {
        private SpeechRecognitionEngine _recognizer;
        private readonly Speak _speak;

        internal Listen()
        {
            _speak = new Speak();
            SetupSpeechMonitoring();
        }

        /// <summary>
        /// Lets user say 'week number' to get the current week number spoken
        /// </summary>
        internal void SetupSpeechMonitoring()
        {
            _recognizer = new SpeechRecognitionEngine(new CultureInfo("en-US"));
            Choices services = new Choices(new string[] { "number" });
            GrammarBuilder weekServices = new GrammarBuilder("week") { Culture = CultureInfo.CurrentUICulture };
            weekServices.Append(services);
            Grammar servicesGrammar = new Grammar(weekServices);
            _recognizer.LoadGrammarAsync(servicesGrammar);
            _recognizer.SpeechRecognized += (s, e) => _speak.Sentence(Convert.ToString(Week.Current(), CultureInfo.InvariantCulture));
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        public void Dispose()
        {
            _recognizer?.Dispose();
        }
    }
}