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
    /// <summary>
    /// Handles saving and loading files within the "SaveData" directory.
    /// </summary>
    static class SaveFile
    {
        /// <summary>
        /// The location of the SaveData Folder.
        /// </summary>
        static public string SaveData = "SaveData/";
        /// <summary>
        /// The location of the computer settings folder.
        /// </summary>
        static public string SettingsLocation = "Settings/";
                
        static Stream ObjectStream;
        static BinaryFormatter Formatter = new BinaryFormatter();

        /// <summary>
        /// Save an object.
        /// </summary>
        /// <param name="InputObject">The object to save.</param>
        /// <param name="Location">The file location within the "SaveData" directory.</param>
        /// <param name="FileName">The file name.</param>
        static public void Save(object InputObject, string Location, string FileName)
        {
            //The full destination of the file.
            string Destination = SaveData + Location + FileName;
            
            //If the directory doesn't exist, create it.
            if (!Directory.Exists(SaveData + Location))
                Directory.CreateDirectory(SaveData + Location);

            //Save the object.
            using (ObjectStream = File.Open(Destination, FileMode.Create))            
                Formatter.Serialize(ObjectStream, InputObject);            
        }

        /// <summary>
        /// Load an object. Make sure to convert it to the correct object type after loading.
        /// </summary>
        /// <param name="FilePath">The location of the file within the "SaveData" directory.</param>
        /// <returns></returns>
        static public object Load(string FilePath)
        {
            //The full destination of the file.
            string Destination = SaveData + FilePath;
            
            //If the file exists, load and return it. If not return null.
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
