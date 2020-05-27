using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;

namespace SkeletonEngine
{
    static class SaveFile
    {
        static public string SaveLocation = "SaveData/";
        static public string SettingsLocation = "Settings/";
        static public string ScreenDataName = "ScreenData";


        static Stream ObjectStream;
        static BinaryFormatter Formatter = new BinaryFormatter();

        static public void Save(object InputObject, string Location, string FileName)
        {
            string Destination = SaveLocation + Location + FileName;
            
            if (!Directory.Exists(SaveLocation + Location))
                Directory.CreateDirectory(SaveLocation + Location);

            using (ObjectStream = File.Open(Destination, FileMode.Create))            
                Formatter.Serialize(ObjectStream, InputObject);
            
        }

        static public object Load(string FileName)
        {
            string Destination = SaveLocation + FileName;
            
            if (File.Exists(Destination))
            {
                using (ObjectStream = File.Open(Destination, FileMode.Open))
                    return Formatter.Deserialize(ObjectStream);
            }
            else
                return null;
        }
    }
}
