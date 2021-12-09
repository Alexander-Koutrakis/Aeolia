using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[System.Serializable]
public class SavedGame 
{
    private string playerGender;
    public string PlayerGender { get { return this.playerGender; } }
    private string lastScene;
    public string LastScene { get { return this.lastScene;} }
    private SceneSave[] sceneSaves=new SceneSave[0];
    private string[] itemNames = new string[0];
    private string[] availableScenes = new string[0];
    public SceneSave LoadSceneSave(string sceneName)
    {
        for(int i = 0; i < sceneSaves.Length; i++)
        {
            if (sceneSaves[i].SceneName == sceneName)
            {
                return sceneSaves[i];
            }
        }

        return null;
    }
    public bool ContainsScene(string sceneName)
    {
        for (int i = 0; i < sceneSaves.Length; i++)
        {
            if (sceneSaves[i].SceneName == sceneName)
            {

                return true;
            }
        }

        return false;
    }
    private void SaveNewScene(SceneSave newSceneSave)
    {
        List<SceneSave> newSceneSaves = sceneSaves.ToList();
        newSceneSaves.Add(newSceneSave);
        sceneSaves = newSceneSaves.ToArray();
    }
    public void SaveScene(SceneSave sceneSave)
    {
        if (!ContainsScene(sceneSave.SceneName))
        {
            SaveNewScene(sceneSave);
        }
        else
        {
            for (int i = 0; i < sceneSaves.Length; i++)
            {
                if (sceneSaves[i].SceneName == sceneSave.SceneName)
                {
                    sceneSaves[i].ChangeData(sceneSave);
                }
            }
        }
    }
    public void SaveInventory(List<Item> items)
    {
        itemNames = new string[items.Count];
        for(int i = 0; i < itemNames.Length; i++)
        {
            itemNames[i] = items[i].Name;
        }
    }
    public string[] LoadItems()
    {
        return this.itemNames;
    }
    public void SaveAvailableScenes(string[] scenes)
    {
        availableScenes = scenes;
    }
    public string[] LoadAvailableScenes()
    {
        return availableScenes;
    }

   public void SaveLastScene(string lastScene)
    {
        this.lastScene = lastScene;
    }

    public void SavePlayerGender(PlayerGender playerGender)
    {
        this.playerGender = playerGender.ToString();
    }
}
