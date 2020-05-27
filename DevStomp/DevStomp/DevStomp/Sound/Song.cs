using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevStomp
{
    class Note
    {
        public byte N;
        public float T;
        public string Word;

        public Note(byte note, float time, string word)
        {
            N = note;
            T = time;
            Word = word;
        }
    }

    class Song
    {
        public byte BPM = 60;
        List<Note> Notes = new List<Note>();

        public void AddNote(byte note, float time)
        {
            Notes.Add(new Note(note, time, "llao")); 
        }

        public void AddNote(string note, float time)
        {
            byte n = 0;

            #region Notes

            switch (note)
            {
                case "C2":
                    n = 1;
                    break;
                case "C#2":
                    n = 2;
                    break;
                case "D2":
                    n = 3;
                    break;
                case "D#2":
                    n = 4;
                    break;
                case "E2":
                    n = 5;
                    break;
                case "F2":
                    n = 6;
                    break;
                case "F#2":
                    n = 7;
                    break;
                case "G2":
                    n = 8;
                    break;
                case "G#2":
                    n = 9;
                    break;
                case "A2":
                    n = 10;
                    break;
                case "A#2":
                    n = 11;
                    break;
                case "B2":
                    n = 12;
                    break;
                case "C3":
                    n = 13;
                    break;
                case "C#3":
                    n = 14;
                    break;
                case "D3":
                    n = 15;
                    break;
                case "D#3":
                    n = 16;
                    break;
                case "E3":
                    n = 17;
                    break;
                case "F3":
                    n = 18;
                    break;
                case "F#3":
                    n = 19;
                    break;
                case "G3":
                    n = 20;
                    break;
                case "G#3":
                    n = 21;
                    break;
                case "A3":
                    n = 22;
                    break;
                case "A#3":
                    n = 23;
                    break;
                case "B3":
                    n = 24;
                    break;
                case "C4":
                    n = 25;
                    break;
                case "C#4":
                    n = 26;
                    break;
                case "D4":
                    n = 27;
                    break;
                case "D#4":
                    n = 28;
                    break;
                case "E4":
                    n = 29;
                    break;
                case "F4":
                    n = 30;
                    break;
                case "F#4":
                    n = 31;
                    break;
                case "G4":
                    n = 32;
                    break;
                case "G#4":
                    n = 33;
                    break;
                case "A4":
                    n = 34;
                    break;
                case "A#4":
                    n = 35;
                    break;
                case "B4":
                    n = 36;
                    break;
                case "C5":
                    n = 37;
                    break;
            }

            #endregion

            Notes.Add(new Note(n, time, "llao"));
        }

        public void AddNote(string note, float time, string word)
        {
            byte n = 0;

            #region Notes

            switch (note)
            {
                case "C2":
                    n = 1;
                    break;
                case "C#2":
                    n = 2;
                    break;
                case "D2":
                    n = 3;
                    break;
                case "D#2":
                    n = 4;
                    break;
                case "E2":
                    n = 5;
                    break;
                case "F2":
                    n = 6;
                    break;
                case "F#2":
                    n = 7;
                    break;
                case "G2":
                    n = 8;
                    break;
                case "G#2":
                    n = 9;
                    break;
                case "A2":
                    n = 10;
                    break;
                case "A#2":
                    n = 11;
                    break;
                case "B2":
                    n = 12;
                    break;
                case "C3":
                    n = 13;
                    break;
                case "C#3":
                    n = 14;
                    break;
                case "D3":
                    n = 15;
                    break;
                case "D#3":
                    n = 16;
                    break;
                case "E3":
                    n = 17;
                    break;
                case "F3":
                    n = 18;
                    break;
                case "F#3":
                    n = 19;
                    break;
                case "G3":
                    n = 20;
                    break;
                case "G#3":
                    n = 21;
                    break;
                case "A3":
                    n = 22;
                    break;
                case "A#3":
                    n = 23;
                    break;
                case "B3":
                    n = 24;
                    break;
                case "C4":
                    n = 25;
                    break;
                case "C#4":
                    n = 26;
                    break;
                case "D4":
                    n = 27;
                    break;
                case "D#4":
                    n = 28;
                    break;
                case "E4":
                    n = 29;
                    break;
                case "F4":
                    n = 30;
                    break;
                case "F#4":
                    n = 31;
                    break;
                case "G4":
                    n = 32;
                    break;
                case "G#4":
                    n = 33;
                    break;
                case "A4":
                    n = 34;
                    break;
                case "A#4":
                    n = 35;
                    break;
                case "B4":
                    n = 36;
                    break;
                case "C5":
                    n = 37;
                    break;
            }

            #endregion

            Notes.Add(new Note(n, time, word));
        }

        public string ReturnVocalSong()
        {
            string song = "";
            int time = 0;

            for (int i = 0; i < Notes.Count; i++)
            {
                time = (int)(((60f * Notes[i].T) / BPM) * 1000);

                if (Notes[i].N != 0)
                    song += "[" + Notes[i].Word + "<" + time + "," + Notes[i].N + ">]";
                else
                    song += "[:comma " + time + "]";

            }

            return song;
        }
    }
}
