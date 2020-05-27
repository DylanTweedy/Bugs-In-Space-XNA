using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;

namespace BugsInSpace
{
    public class SavePreferences
    {
        StorageDevice device;
        public string containerName = "PreferencesData";
        public string fileName;

        public int backBufferHeight;
        public int backBufferWidth;
        public bool fullScreen;
        public bool showHealthBars;
        public float musicVolume;
        public float soundVolume;

        public bool Available;

        public struct SaveGame
        {
            public int BackBufferHeight;
            public int BackBufferWidth;
            public bool FullScreen;
            public bool ShowHealthBars;
            public float MusicVolume;
            public float SoundVolume;
        }

        public void Initialize()
        {
            Available = false;
            fileName = "Preferences.sav";
        }

        public void InitiateSave()
        {
            //if (!Guide.IsVisible)
            {
                device = null;
                StorageDevice.BeginShowSelector(PlayerIndex.One, this.SaveToDevice, null);
            }
        }

        void SaveToDevice(IAsyncResult result)
        {
            device = StorageDevice.EndShowSelector(result);
            if (device != null && device.IsConnected)
            {
                SaveGame SaveData = new SaveGame()
                {
                    BackBufferHeight = backBufferHeight,
                    BackBufferWidth = backBufferWidth,
                    FullScreen = fullScreen,
                    MusicVolume = musicVolume,
                    SoundVolume = soundVolume,
                    ShowHealthBars = showHealthBars,
                };

                IAsyncResult r = device.BeginOpenContainer(containerName, null, null);
                result.AsyncWaitHandle.WaitOne();
                StorageContainer container = device.EndOpenContainer(r);
                if (container.FileExists(fileName))
                    container.DeleteFile(fileName);
                Stream stream = container.CreateFile(fileName);
                XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));
                serializer.Serialize(stream, SaveData);
                stream.Close();
                container.Dispose();
                result.AsyncWaitHandle.Close();
            }
        }

        public void InitiateLoad()
        {
            //if (!Guide.IsVisible)
            {
                device = null;
                StorageDevice.BeginShowSelector(PlayerIndex.One, this.LoadFromDevice, null);
            }
        }
        void LoadFromDevice(IAsyncResult result)
        {
            device = StorageDevice.EndShowSelector(result);
            IAsyncResult r = device.BeginOpenContainer(containerName, null, null);
            result.AsyncWaitHandle.WaitOne();
            StorageContainer container = device.EndOpenContainer(r);
            result.AsyncWaitHandle.Close();

            string[] FileList = container.GetFileNames();

            if (container.FileExists(fileName))
            {
                Stream stream = container.OpenFile(fileName, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));
                SaveGame SaveData = (SaveGame)serializer.Deserialize(stream);
                stream.Close();
                container.Dispose();

                backBufferHeight = SaveData.BackBufferHeight;
                backBufferWidth = SaveData.BackBufferWidth;
                fullScreen = SaveData.FullScreen;
                showHealthBars = SaveData.ShowHealthBars;
                musicVolume = SaveData.MusicVolume;
                soundVolume = SaveData.SoundVolume;
                Available = true;
            }
        }
    }
}
