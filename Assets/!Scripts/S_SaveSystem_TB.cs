using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class S_SaveSystem_TB
{
    public static void Save(S_Player_TB player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/progress.data";
        Debug.Log(path);
        FileStream stream = new FileStream(path, FileMode.Create);

        S_ProgressData_TB data = new S_ProgressData_TB(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static S_ProgressData_TB Load()
    {
        string path = Application.persistentDataPath + "/progress.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            S_ProgressData_TB data = formatter.Deserialize(stream) as S_ProgressData_TB;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("No save at " + path);
            return null;
        }
    }
}
