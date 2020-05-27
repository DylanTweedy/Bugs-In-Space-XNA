using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DevStomp
{
    static class Serializer
    {
        static public string SaveLocation = "C:\\test\\";
        static string ChunkData = "ChunkData\\";
        static string ChunkDataType = ".chunk";


        static public void SerializeObject(string Folder, string filename, Chunk objectToSerialize)
        {
            filename = SaveLocation + ChunkData + Folder + "\\" + filename + ChunkDataType;

            Directory.CreateDirectory(SaveLocation + ChunkData + Folder);

            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        static public Chunk DeSerializeObject(string filename)
        {
            filename = SaveLocation + ChunkData + filename + ChunkDataType;

            Chunk objectToSerialize;
            if (File.Exists(filename))
            {
                Stream stream = File.Open(filename, FileMode.Open);
                BinaryFormatter bFormatter = new BinaryFormatter();
                objectToSerialize = (Chunk)bFormatter.Deserialize(stream);
                stream.Close();
            }
            else
                return null;

            return objectToSerialize;
        }
    }
}
