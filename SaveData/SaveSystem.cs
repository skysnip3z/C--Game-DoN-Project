using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    static BinaryFormatter binaryFormatter = new BinaryFormatter();
    static string path = Application.persistentDataPath + "/stats.narik";
    static FileStream stream;

    public static void SaveStats(PlayerSaveData stats) 
    {
        stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(stats);
        binaryFormatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadStats() 
    {
        if (File.Exists(path))
        {
            stream = new FileStream(path, FileMode.Open);

            SaveData stats = binaryFormatter.Deserialize(stream) as SaveData;
            stream.Close();
            
            return stats;
        }
        else 
        {
            return null;
        }
    }
}
