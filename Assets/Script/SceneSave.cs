using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneSave
{
    public string SceneName { get; private set; }
    public bool SceneVisited { get; private set; }
    public ItemSave[] ItemSaves { get; private set; }

    public SceneSave(string sceneName, bool sceneVisited, ItemSave[] itemSaves)
    {
        this.SceneName = sceneName;
        this.SceneVisited = sceneVisited;
        this.ItemSaves = itemSaves;
    }

    public void ChangeData(SceneSave newSceneSave)
    {
        this.SceneVisited = newSceneSave.SceneVisited;
        this.ItemSaves = newSceneSave.ItemSaves;
    }

    public ItemSave LoadItem(string itemName)
    {
        for(int i = 0; i < ItemSaves.Length; i++)
        {
            if (ItemSaves[i].Name == itemName)
            {
                return ItemSaves[i];
            }
        }

        return new ItemSave();
    }
}

[System.Serializable]
public struct ItemSave {
    public string Name;
    public bool Active;
    public bool Used;

    public ItemSave(string name,bool active,bool Used)
    {
        this.Name = name;
        this.Active = active;
        this.Used = Used;
    }
}

