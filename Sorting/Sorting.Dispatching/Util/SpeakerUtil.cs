using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Synthesis;
using System.Threading;

namespace Sorting.Dispatching.Util
{
    public class SpeakerUtil
    {
        public static void SpeakerInfo(string message)
        {
            if (message.Trim().Length > 0)
            {
                SpeechSynthesizer speaker = new SpeechSynthesizer();
                speaker.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult, 2, System.Globalization.CultureInfo.CurrentCulture);
                speaker.Rate = -2;
                speaker.Volume = 100;
                speaker.SpeakAsync(message);

            }

        }
    }
}
