using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpTalk;

namespace DevStomp
{
    class Voice
    {
        FonixTalkEngine voice = new FonixTalkEngine();
        string VoiceDesigner;



        byte Gender;
        byte MainVoice;
        byte Breathiness;
        byte LaxBreathiness;
        byte Smoothness;
        byte Richness;
        byte Nopen;
        byte Laryngealization;
        byte BaselineFall;
        byte HatRise;
        byte Assertiveness;
        byte Quickness;
        byte AveragePitch;
        byte PitchRange;
        short Rate;


        public void GenerateRandomVoice()
        {
            voice.Reset();
            Random r = new Random();

            Gender = (byte)r.Next(0, 2);

            if (Gender == 0)
                MainVoice = (byte)r.Next(0, 5);
            else
                MainVoice = (byte)r.Next(5, 9);



            switch (MainVoice)
            {
                case 0:
                    voice.Voice = TtsVoice.Frank;
                    break;
                case 1:
                    voice.Voice = TtsVoice.Paul;
                    break;
                case 2:
                    voice.Voice = TtsVoice.Dennis;
                    break;
                case 3:
                    voice.Voice = TtsVoice.Harry;
                    break;
                case 4:
                    voice.Voice = TtsVoice.Kit;
                    break;
                case 5:
                    voice.Voice = TtsVoice.Betty;
                    break;
                case 6:
                    voice.Voice = TtsVoice.Rita;
                    break;
                case 7:
                    voice.Voice = TtsVoice.Ursula;
                    break;
                case 8:
                    voice.Voice = TtsVoice.Wendy;
                    break;
            }


            //voice.Voice = TtsVoice.Frank;


            if (r.Next(0, 3) == 0)
                Breathiness = (byte)r.Next(0, 71);
            else
                Breathiness = 0;

            if (r.Next(0, 2) == 0)
                Laryngealization = (byte)r.Next(0, 101);
            else
                Laryngealization = 0;

            if (r.Next(0, 4) == 0)
                Smoothness = (byte)r.Next(0, 101);
            else
                Smoothness = 0;


            LaxBreathiness = (byte)r.Next(0, 101);
            Richness = (byte)r.Next(0, 101);
            Nopen = (byte)r.Next(0, 101);
            HatRise = (byte)r.Next(0, 101);
            Assertiveness = (byte)r.Next(0, 101);
            Quickness = (byte)r.Next(0, 101);
            PitchRange = (byte)r.Next(0, 101);

            BaselineFall = (byte)r.Next(0, 61);
            AveragePitch = (byte)r.Next(30, 200);

            Rate = (short)r.Next(75, 500);

            VoiceDesigner = "[:dv" +
                " br " + Breathiness +
                " lx " + LaxBreathiness +
                //" sm " + Smoothness +
                " ri " + Richness +
                " nf " + Nopen +
                " la " + Laryngealization +
                " bf " + BaselineFall +
                " hr " + HatRise +
                " as " + Assertiveness +
                " qu " + Quickness +
                " ap " + AveragePitch +
                " pr " + PitchRange +
                "]";


            //voice.Voice = TtsVoice.Wendy;


            //VoiceDesigner = "[:dv" +
            //    " br " + Breathiness +
            //    " lx " + LaxBreathiness +
            //    " ri " + Richness +
            //    " nf " + Nopen +
            //    " la " + Laryngealization +
            //    " bf " + BaselineFall +
            //    " hr " + HatRise +
            //    " as " + Assertiveness +
            //    " qu " + Quickness +
            //    " ap " + AveragePitch +
            //    " pr " + PitchRange +
            //    "]";

            //VoiceDesigner = "";


        }


        public void Speak(string Text, bool Singing)
        {
            int rate = Rate;

            if (Singing)
                rate = 200;

            voice.Speak(
                "[:phoneme arpabet speak on]  " +
                "[:rate " + rate + "]" +
                VoiceDesigner +
                " " + Text);
        }

        public void Stop()
        {
            voice.Reset();
        }
    }
}
