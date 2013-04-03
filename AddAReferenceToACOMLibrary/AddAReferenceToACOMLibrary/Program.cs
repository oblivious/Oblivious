using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeechLib;

namespace AddAReferenceToACOMLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            SpVoice voice = new SpVoice();
            voice.Speak("Hello, world!", SpeechVoiceSpeakFlags.SVSFDefault);
        }
    }
}
