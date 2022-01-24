using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Managers
{
    public class SaveManager
    {
        public static T SaveData<T> (T data, string key)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            string path = Application.persistentDataPath + "/" + key + ".data";
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

            formatter.Serialize(stream, data);
            stream.Close();

            return data;
        }

        public static T LoadData<T> (string key)
        {
            string path = Application.persistentDataPath + "/" + key + ".data";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                T data = (T)formatter.Deserialize(stream);
                stream.Close();

                return data;
            }
            else
                return default(T);
        }

        public static void RemoveData (string key)
        {
            string path = Application.persistentDataPath + "/" + key + ".data";

            if (File.Exists(path))
                File.Delete(path);
        }
    }
}