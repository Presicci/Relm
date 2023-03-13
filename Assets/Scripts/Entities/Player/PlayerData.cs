using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public Inventory inventory;
    public int gold;
    
    PlayerData(Player player)
    {
        inventory = player.GetInventory();
        gold = player.Gold;
    }
    
    
    /**
     * Serialization
     */
    public static void Save(Player data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/PlayerName.save";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData(data);

        formatter.Serialize(stream, playerData);
        stream.Close();
    }
    
    public static PlayerData Load()
    {
        string path = Application.persistentDataPath + "/PlayerName.save";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();

            return data;
        } 
        else
        {
            Debug.LogError("Error: Save file not found in " + path);
            return null;
        }
    }
}