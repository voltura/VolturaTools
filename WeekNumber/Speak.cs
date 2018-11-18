#region Using statements

using System.Speech.Synthesis;
using System;

#endregion Using statements

namespace WeekNumber
{
    internal class Speak : IDisposable
    {
        #region Private variables

        private readonly SpeechSynthesizer _synth;
        private bool _disposedValue;

        #endregion Private variables

        #region Constructor

        internal Speak()
        {
            _synth = new SpeechSynthesizer();
            _synth.SetOutputToDefaultAudioDevice();
        }

        #endregion Constructor

        #region Internal methods & properties

        internal void Sentence(string sentence)
        {
            _synth.SpeakAsyncCancelAll();
            _synth.SpeakAsync(sentence);
        }

        internal void Cancel()
        {
            _synth.SpeakAsyncCancelAll();
        }

        #endregion Internal methods

        #region IDisposable Support

        internal virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Cancel();
                    _synth.Dispose();
                }
                _disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}