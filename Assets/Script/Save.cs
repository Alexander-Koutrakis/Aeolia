using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public static class SaveSystem
{
    public static void Save()
    {

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/GameSave";
        FileStream stream = new FileStream(path, FileMode.Create);
        SavedGame savedGame;
        if (Gamemaster.SavedGame == null)
        {
            Debug.Log("New Save");
            savedGame = new SavedGame();
            Gamemaster.SavedGame = savedGame;
        }
        else
        {
            Debug.Log("Save Game");
            savedGame = Gamemaster.SavedGame;
        }
        binaryFormatter.Serialize(stream, savedGame);
        stream.Close();
    }

    public static SavedGame LoadGame()
    {
        string path = Application.persistentDataPath + "/GameSave";
        if (File.Exists(path))
        {
            Debug.Log("Load Game");
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SavedGame savedGame = binaryFormatter.Deserialize(stream) as SavedGame;
            stream.Close();
            return savedGame;
        }
        else
        {

            Debug.LogWarning("ERROR IN SAVE");
            return null;
        }
    }

    public static void Delete_Save()
    {
        string path = Application.persistentDataPath + "/GameSave";
        File.Delete(path);
    }
}