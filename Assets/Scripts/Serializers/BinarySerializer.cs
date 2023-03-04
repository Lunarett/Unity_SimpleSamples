using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class BinarySerializer
{
    public static void SerializeData(object data, string filePath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static object DeserializeData(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("File does not exist at path: " + filePath);
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Open);

        object data = formatter.Deserialize(stream);
        stream.Close();

        return data;
    }
}