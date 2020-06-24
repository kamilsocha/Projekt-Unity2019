using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Class responsible for loading and saving objects.
/// In this specific project saves only information about levels.
/// </summary>
public class SerializationManager
{
    public static bool Save(string name, object data)
    {
        var formatter = GetBinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        var path = Application.persistentDataPath + "/saves/" + name + ".save";

        FileStream file = File.Create(path);

        formatter.Serialize(file, data);

        file.Close();

        return true;
    }

    public static object Load(string path)
    {

        if (!File.Exists(Application.persistentDataPath + "/saves/" + path + ".save"))
        {
            Save(path, SaveData.Current);
            return SaveData.Current;
        }

        var formatter = GetBinaryFormatter();

        path = Application.persistentDataPath + "/saves/" + path + ".save";

        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat($"Failed to load file at {path}");
            file.Close();
            return null;
        }

    }


    public static BinaryFormatter GetBinaryFormatter()
    {
        return new BinaryFormatter();
    }
}
