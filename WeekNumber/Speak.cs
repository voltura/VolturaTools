#region Using statement

using SpeechLib;

#endregion Using statement

namespace WeekNumber
{
    internal class Speak
    {
        #region Private variable

        private readonly SpVoice _voice = new SpVoice();

        #endregion Private variable

        #region Constructor

        internal Speak()
        {
            _voice = new SpVoice();
        }

        #endregion Constructor

        #region Internal method

        internal void Sentence(string sentence)
        {
            _voice.Speak(sentence, SpeechVoiceSpeakFlags.SVSFlagsAsync);
        }

        #endregion Internal method
    }
}